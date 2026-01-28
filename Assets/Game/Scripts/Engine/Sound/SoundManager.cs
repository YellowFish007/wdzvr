using Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public class SoundManager : SingletonGameObject<SoundManager>
    {
        //音乐源
        AudioSource m_MusicAudioSource;
        //音效源
        AudioSource m_ShotAudioSource;

        //4个Key
        private const string KEY_MUSIC = "MUSIC_TOOGLE";
        private const string KEY_SOUND = "MUSIC_SOUND";
        private const string KEY_MUSIC_VOLUME = "KEY_MUSIC_VOLUME";
        private const string KEY_SHOT_VOLUME = "KEY_SHOT_VOLUME";

        AudioListener m_AudioListener;

        public void Init()
        {

        }

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(KEY_MUSIC))
            {
                PlayerPrefs.SetInt(KEY_MUSIC, 1);
            }
            if (!PlayerPrefs.HasKey(KEY_SOUND))
            {
                PlayerPrefs.SetInt(KEY_SOUND, 1);
            }
            if (!PlayerPrefs.HasKey(KEY_MUSIC_VOLUME))
            {
                PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, 1.0f);
            }
            if (!PlayerPrefs.HasKey(KEY_SHOT_VOLUME))
            {
                PlayerPrefs.SetFloat(KEY_SHOT_VOLUME, 1.0f);
            }
            GameObject obj = new GameObject("SoundRoot");
            Object.DontDestroyOnLoad(obj);

            m_MusicAudioSource = obj.AddComponent<AudioSource>();
            m_ShotAudioSource = obj.AddComponent<AudioSource>();

            m_AudioListener = obj.AddComponent<AudioListener>();
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="audioName"></param>
        public void PlayMusic(string audioName)
        {
            if (!IsGameMusicOpen())
            {
                return;
            }

            if (m_MusicAudioSource.isPlaying)
            {
                m_MusicAudioSource.Stop();
            }
            m_MusicAudioSource.loop = true;

            Asset.LoadAudioClipAsync("RawAssets/Audio/" + audioName, delegate (AudioClip audioClip)
            {
                m_MusicAudioSource.clip = audioClip;
                m_MusicAudioSource.Play();
            });
        }
        /// <summary>
        /// 停止音乐
        /// </summary>
        public void StopMusic()
        {
            m_MusicAudioSource.Stop();
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioName"></param>
        public void PlayShot(string audioName)
        {
            if (!IsGameSoundOpen())
            {
                return;
            }
            Asset.LoadAudioClipAsync(audioName, delegate (AudioClip audioClip)
            {
                m_ShotAudioSource.PlayOneShot(audioClip);
            });
        }

        //----------------设置方法----------------

        /// <summary>
        /// 是否开启音乐
        /// </summary>
        /// <returns></returns>
        public bool IsGameMusicOpen()
        {
            return PlayerPrefs.GetInt(KEY_MUSIC) == 1;
        }
        /// <summary>
        /// 是否开启音效
        /// </summary>
        /// <returns></returns>
        public bool IsGameSoundOpen()
        {
            return PlayerPrefs.GetInt(KEY_SOUND) == 1;
        }

        /// <summary>
        /// 开启游戏音乐
        /// </summary>
        public void OpenGameMusic()
        {
            PlayerPrefs.SetInt(KEY_MUSIC, 1);
        }
        /// <summary>
        /// 关闭游戏音乐
        /// </summary>
        public void CloseGameMusic()
        {
            PlayerPrefs.SetInt(KEY_MUSIC, 0);
            StopMusic();
        }
        /// <summary>
        /// 开启游戏音效
        /// </summary>
        public void OpenGameSound()
        {
            PlayerPrefs.SetInt(KEY_SOUND, 1);
        }
        /// <summary>
        /// 关闭游戏音效
        /// </summary>
        public void CloseGameSound()
        {
            PlayerPrefs.SetInt(KEY_SOUND, 0);
        }

        /// <summary>
        /// 设置音乐音量大小
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, volume);
            m_MusicAudioSource.volume = volume;
        }
        //获取音乐音量大小
        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME);
        }

        /// <summary>
        /// 设置音效音量大小
        /// </summary>
        /// <param name="volume"></param>
        public void SetSoundVolume(float volume)
        {
            PlayerPrefs.SetFloat(KEY_SHOT_VOLUME, volume);
            m_ShotAudioSource.volume = volume;
        }
        //获取音效音量大小
        public float GetSoundVolume()
        {
            return PlayerPrefs.GetFloat(KEY_SHOT_VOLUME);
        }
    }
}