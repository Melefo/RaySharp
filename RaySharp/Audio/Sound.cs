using System;
using System.Runtime.InteropServices;

namespace RaySharp.Audio
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Sound : IDisposable
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Sound LoadSound(string fileName);
        [DllImport(Constants.dllName)]
        private static extern Sound LoadSoundFromWave(Wave wave);
        [DllImport(Constants.dllName)]
        private static extern void UpdateSound(Sound sound, IntPtr data, int samplesCount);
        [DllImport(Constants.dllName)]
        private static extern void UnloadSound(Sound sound);
        [DllImport(Constants.dllName)]
        private static extern void PlaySound(Sound sound);
        [DllImport(Constants.dllName)]
        private static extern void StopSound(Sound sound);
        [DllImport(Constants.dllName)]
        private static extern void PauseSound(Sound sound);
        [DllImport(Constants.dllName)]
        private static extern void ResumeSound(Sound sound);
        [DllImport(Constants.dllName)]
        private static extern void PlaySoundMulti(Sound sound);
        [DllImport(Constants.dllName)]
        private static extern int GetSoundsPlaying();
        [DllImport(Constants.dllName)]
        private static extern bool IsSoundPlaying(Sound sound);
        [DllImport(Constants.dllName)]
        private static extern void SetSoundVolume(Sound sound, float volume);
        [DllImport(Constants.dllName)]
        private static extern void SetSoundPitch(Sound sound, float pitch);

        /// <summary>
        /// Check if a sound is currently playing
        /// </summary>
        public bool IsPlaying => IsSoundPlaying(this);
        /// <summary>
        /// Set volume for a sound (1.0 is max level)
        /// </summary>
        public float Volume
        {
            set => SetSoundVolume(this, value);
        }
        /// <summary>
        /// Set pitch for a sound (1.0 is base level)
        /// </summary>
        public float Pitch
        {
            set => SetSoundPitch(this, value);
        }
        /// <summary>
        /// Get number of sounds playing in the multichannel
        /// </summary>
        public static int MultiPlaying => GetSoundsPlaying();
        /// <summary>
        /// Audio stream
        /// </summary>
        public AudioStream Stream { get; }
        /// <summary>
        /// Total number of samples
        /// </summary>
        public uint SampleCount { get; private set; }

        /// <summary>
        /// Load sound from file
        /// </summary>
        /// <param name="fileName">Filepath</param>
        public Sound(string fileName)
        {
            var sound = LoadSound(fileName);

            Stream = sound.Stream;
            SampleCount = sound.SampleCount;
        }

        /// <summary>
        /// Load sound from wave data
        /// </summary>
        /// <param name="wave">Wave</param>
        public Sound(Wave wave)
        {
            var sound = LoadSoundFromWave(wave);

            Stream = sound.Stream;
            SampleCount = sound.SampleCount;
        }

        /// <summary>
        /// Unload sound
        /// </summary>
        public void Dispose()
        {
            UnloadSound(this);
            Stream.Dispose();
            SampleCount = 0;
        }

        /// <summary>
        /// Stop any sound playing (using multichannel buffer pool)
        /// </summary>
        [DllImport(Constants.dllName, EntryPoint = "StopSoundMulti")]
        public static extern void StopMulti();

        /// <summary>
        /// Update sound buffer with new data
        /// </summary>
        /// <param name="data">New data</param>
        /// <param name="samplesCount">Number of samples inside data</param>
        public void Update(IntPtr data, int samplesCount) => UpdateSound(this, data, samplesCount);

        /// <summary>
        /// Play a sound
        /// </summary>
        public void Play() => PlaySound(this);
        /// <summary>
        /// Pause a sound
        /// </summary>
        public void Pause() => PauseSound(this);
        /// <summary>
        /// Stop playing a sound
        /// </summary>
        public void Stop() => StopSound(this);
        /// <summary>
        /// Resume a paused sound
        /// </summary>
        public void Resume() => ResumeSound(this);

        /// <summary>
        /// Play a sound (using multichannel buffer pool)
        /// </summary>
        public void PlayMulti() => PlaySoundMulti(this);
    }
}
