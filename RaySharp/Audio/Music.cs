using System;
using System.Runtime.InteropServices;

namespace RaySharp.Audio
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Music : IDisposable
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Music LoadMusicStream(string fileName);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Music LoadMusicStreamFromMemory(string fileType, IntPtr data, int dataSize);
        [DllImport(Constants.dllName)]
        private static extern void UnloadMusicStream(Music music);
        [DllImport(Constants.dllName)]
        private static extern void PlayMusicStream(Music music);
        [DllImport(Constants.dllName)]
        private static extern bool IsMusicPlaying(Music music);
        [DllImport(Constants.dllName)]
        private static extern void UpdateMusicStream(Music music);
        [DllImport(Constants.dllName)]
        private static extern void StopMusicStream(Music music);
        [DllImport(Constants.dllName)]
        private static extern void PauseMusicStream(Music music);
        [DllImport(Constants.dllName)]
        private static extern void ResumeMusicStream(Music music);
        [DllImport(Constants.dllName)]
        private static extern void SetMusicVolume(Music music, float volume);
        [DllImport(Constants.dllName)]
        private static extern void SetMusicPitch(Music music, float pitch);
        [DllImport(Constants.dllName)]
        private static extern float GetMusicTimeLength(Music music);
        [DllImport(Constants.dllName)]
        private static extern float GetMusicTimePlayed(Music music);

        /// <summary>
        /// Audio stream
        /// </summary>
        public AudioStream Stream { get; }
        /// <summary>
        /// Total number of samples
        /// </summary>
        public uint SampleCount { get; private set; }
        /// <summary>
        /// Music looping enable
        /// </summary>
        public bool Looping { get; private set; }
        /// <summary>
        /// Type of music context (audio filetype)
        /// </summary>
        public int CtxType { get; private set; }
        /// <summary>
        /// Audio context data, depends on type
        /// </summary>
        public IntPtr CtxData { get; private set; }

        /// <summary>
        /// Check if music is playing
        /// </summary>
        public bool IsPlaying => IsMusicPlaying(this);
        /// <summary>
        /// Set volume for music (1.0 is max level)
        /// </summary>
        public float Volume
        {
            set => SetMusicVolume(this, value);
        }
        /// <summary>
        /// Set pitch for a music (1.0 is base level)
        /// </summary>
        public float Pitch
        {
            set => SetMusicPitch(this, value);
        }
        /// <summary>
        /// Get music time length (in seconds)
        /// </summary>
        public float Length => GetMusicTimeLength(this);
        /// <summary>
        /// Get current music time played (in seconds)
        /// </summary>
        public float Played => GetMusicTimePlayed(this);

        /// <summary>
        /// Load music stream from file
        /// </summary>
        /// <param name="fileName">Filepath</param>
        public Music(string fileName)
        {
            var music = LoadMusicStream(fileName);

            Stream = music.Stream;
            SampleCount = music.SampleCount;
            Looping = music.Looping;
            CtxType = music.CtxType;
            CtxData = music.CtxData;
        }

        /// <summary>
        /// Load music stream from data
        /// </summary>
        /// <param name="fileType">File type</param>
        /// <param name="data">Music data</param>
        /// <param name="dataSize">Data size</param>
        public Music(string fileType, IntPtr data, int dataSize)
        {
            var music = LoadMusicStreamFromMemory(fileType, data, dataSize);

            Stream = music.Stream;
            SampleCount = music.SampleCount;
            Looping = music.Looping;
            CtxType = music.CtxType;
            CtxData = music.CtxData;
        }

        /// <summary>
        /// Unload music stream
        /// </summary>
        public void Dispose()
        {
            UnloadMusicStream(this);
            Stream.Dispose();
            SampleCount = 0;
            Looping = false;
            CtxType = 0;
            CtxData = IntPtr.Zero;
        }

        /// <summary>
        /// Start music playing
        /// </summary>
        public void Play() => PlayMusicStream(this);
        /// <summary>
        /// Stop music playing
        /// </summary>
        public void Stop() => StopMusicStream(this);
        /// <summary>
        /// Pause music playing
        /// </summary>
        public void Pause() => PauseMusicStream(this);
        /// <summary>
        /// Resume playing paused music
        /// </summary>
        public void Resume() => ResumeMusicStream(this);
        /// <summary>
        /// Updates buffers for music streaming
        /// </summary>
        public void Update() => UpdateMusicStream(this);
    }
}
