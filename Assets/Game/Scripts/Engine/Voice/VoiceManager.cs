using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Engine
{
    public class VoiceManager : Singleton<VoiceManager>
    {
        private AudioClip recordedClip;
        private int frequency = 44100;

        /// <summary>
        /// 开始录音
        /// </summary>
        public void StartRecord()
        {
            if (Microphone.devices.Length == 0)
            {
                Debug.LogError("没有找到麦克风设备！");
                return;
            }
            // 录制最长60秒，采样率44100
            recordedClip = Microphone.Start(null, false, 60, frequency);
        }

        /// <summary>
        /// 结束录音
        /// </summary>
        /// <returns>录制的音频数据 (PCM 16-bit)</returns>
        public byte[] StopRecord()
        {
            if (Microphone.IsRecording(null))
            {
                int position = Microphone.GetPosition(null);
                Microphone.End(null);

                if (recordedClip != null && position > 0)
                {
                    // 获取实际录制的音频数据
                    float[] samples = new float[position * recordedClip.channels];
                    recordedClip.GetData(samples, 0);
                    
                    // 转换为 byte[]
                    return AudioClipToBytes(samples);
                }
            }
            return null;
        }

        /// <summary>
        /// 播放录音
        /// </summary>
        /// <param name="data">音频数据 (PCM 16-bit)</param>
        public void PlayRecord(byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                // 转换为 AudioClip
                // 假设是单声道，频率为 44100
                AudioClip clip = BytesToAudioClip(data, 1, frequency);
                
                GameObject audioObj = new GameObject("RecordPlayer");
                AudioSource source = audioObj.AddComponent<AudioSource>();
                source.clip = clip;
                source.Play();
                GameObject.Destroy(audioObj, clip.length);
            }
            else
            {
                Debug.LogWarning("没有录音数据可播放");
            }
        }

        // --- Helper Methods ---

        private byte[] AudioClipToBytes(float[] samples)
        {
            // 16-bit PCM = 2 bytes per sample
            byte[] bytes = new byte[samples.Length * 2];
            int rescaleFactor = 32767; // to convert float to Int16

            for (int i = 0; i < samples.Length; i++)
            {
                short value = (short)(samples[i] * rescaleFactor);
                BitConverter.GetBytes(value).CopyTo(bytes, i * 2);
            }
            return bytes;
        }

        private AudioClip BytesToAudioClip(byte[] bytes, int channels, int frequency)
        {
            int sampleCount = bytes.Length / 2;
            float[] samples = new float[sampleCount];
            int rescaleFactor = 32767;

            for (int i = 0; i < sampleCount; i++)
            {
                short value = BitConverter.ToInt16(bytes, i * 2);
                samples[i] = value / (float)rescaleFactor;
            }

            AudioClip clip = AudioClip.Create("PlayedClip", sampleCount, channels, frequency, false);
            clip.SetData(samples, 0);
            return clip;
        }
    }
}
