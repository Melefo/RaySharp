using System;
using System.Runtime.InteropServices;

namespace RaySharp.Audio
{
    public static class AudioDevice
    {
        [DllImport(Constants.dllName)]
        private static extern bool IsAudioDeviceReady();
        [DllImport(Constants.dllName)]
        private static extern void SetMasterVolume(float volume);

        private static float _masterVolume = 1;

        /// <summary>
        /// Check if audio device has been initialized successfully
        /// </summary>
        public static bool Ready => IsAudioDeviceReady();

        /// <summary>
        /// Get/Set master volume (listener)
        /// </summary>
        public static float MasterVolume
        {
            get => _masterVolume;
            set
            {
                value = Math.Clamp(value, 0, 1);

                SetMasterVolume(value);
                _masterVolume = value;
            }
        }

        /// <summary>
        /// Initialize audio device and context
        /// </summary>
        [DllImport(Constants.dllName, EntryPoint = "InitAudioDevice")]
        public static extern void Init();
        /// <summary>
        /// Close the audio device and context
        /// </summary>
        [DllImport(Constants.dllName, EntryPoint = "CloseAudioDevice")]
        public static extern void Close();

    }
}
