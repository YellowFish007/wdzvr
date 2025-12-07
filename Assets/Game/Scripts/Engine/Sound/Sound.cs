using UnityEngine;
using Engine;

namespace Engine
{
    public static class Sound
    {
        public static void PlayMusic(string audioName)
        {
            SoundManager.Instance.PlayMusic(audioName);
        }

        public static void StopMusic()
        {
            SoundManager.Instance.StopMusic();
        }

        public static void PlayShot(string audioName)
        {
            SoundManager.Instance.PlayShot(audioName);
        }
        
        public static bool IsGameMusicOpen()
        {
            return SoundManager.Instance.IsGameMusicOpen();
        }

        public static bool IsGameSoundOpen()
        {
            return SoundManager.Instance.IsGameSoundOpen();
        }

        public static void OpenGameMusic()
        {
            SoundManager.Instance.OpenGameMusic();
        }

        public static void CloseGameMusic()
        {
            SoundManager.Instance.CloseGameMusic();
        }

        public static void OpenGameSound()
        {
            SoundManager.Instance.OpenGameSound();
        }

        public static void CloseGameSound()
        {
            SoundManager.Instance.CloseGameSound();
        }

        public static void SetMusicVolume(float volume)
        {
            SoundManager.Instance.SetMusicVolume(volume);
        }

        public static float GetMusicVolume()
        {
            return SoundManager.Instance.GetMusicVolume();
        }

        public static void SetSoundVolume(float volume)
        {
            SoundManager.Instance.SetSoundVolume(volume);
        }

        public static float GetSoundVolume()
        {
            return SoundManager.Instance.GetSoundVolume();
        }
    }
}