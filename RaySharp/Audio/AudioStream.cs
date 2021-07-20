using System;
using System.Runtime.InteropServices;

namespace RaySharp.Audio
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioStream : IDisposable
    {

        [DllImport(Constants.dllName)]
        private static extern AudioStream InitAudioStream(uint sampleRate, uint sampleSize, uint channels);
        [DllImport(Constants.dllName)]
        private static extern unsafe void UpdateAudioStream(AudioStream stream, void* data, int samplesCount);
        [DllImport(Constants.dllName)]
        private static extern void CloseAudioStream(AudioStream stream);
        [DllImport(Constants.dllName)]
        private static extern bool IsAudioStreamProcessed(AudioStream stream);
        [DllImport(Constants.dllName)]
        private static extern void PlayAudioStream(AudioStream stream);
        [DllImport(Constants.dllName)]
        private static extern void PauseAudioStream(AudioStream stream);
        [DllImport(Constants.dllName)]
        private static extern void ResumeAudioStream(AudioStream stream);
        [DllImport(Constants.dllName)]
        private static extern bool IsAudioStreamPlaying(AudioStream stream);
        [DllImport(Constants.dllName)]
        private static extern void StopAudioStream(AudioStream stream);
        [DllImport(Constants.dllName)]
        private static extern void SetAudioStreamVolume(AudioStream stream, float volume);
        [DllImport(Constants.dllName)]
        private static extern void SetAudioStreamPitch(AudioStream stream, float pitch);

        /// <summary>
        /// Pointer to internal data used by the audio system
        /// </summary>
        public IntPtr AudioBuffer { get; private set; }
        /// <summary>
        /// Frequency (samples per second)
        /// </summary>
        public uint SampleRate { get; private set; }
        /// <summary>
        /// Bit depth (bits per sample): 8, 16, 32 (24 not supported)
        /// </summary>
        public uint SampleSize { get; private set; }
        /// <summary>
        /// Number of channels (1-mono, 2-stereo)
        /// </summary>
        public uint Channels { get; private set; }
        /// <summary>
        /// Set volume for audio stream (1.0 is max level)
        /// </summary>
        public float Volume
        {
            set => SetAudioStreamVolume(this, value);
        }

        /// <summary>
        /// Set pitch for audio stream (1.0 is base level)
        /// </summary>
        public float Pitch
        {
            set => SetAudioStreamPitch(this, value);
        }

        /// <summary>
        /// Init audio stream (to stream raw audio pcm data)
        /// </summary>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="sampleSize">Sample size</param>
        /// <param name="channels">Number of channels</param>
        public AudioStream(uint sampleRate, uint sampleSize, uint channels)
        {
            if (sampleSize != 8 && sampleSize != 16 && sampleSize != 32)
                throw new ArgumentException();
            var stream = InitAudioStream(sampleRate, sampleSize, channels);

            AudioBuffer = stream.AudioBuffer;
            SampleRate = stream.SampleRate;
            SampleSize = stream.SampleSize;
            Channels = stream.Channels;
        }

        /// <summary>
        /// Close audio stream and free memory
        /// </summary>
        public void Dispose()
        {
            CloseAudioStream(this);
            AudioBuffer = IntPtr.Zero;
            SampleRate = 0;
            SampleSize = 0;
            Channels = 0;
        }

        /// <summary>
        /// Update audio stream buffers with data
        /// </summary>
        /// <param name="data">New data</param>
        /// <param name="samplesCount">Number of samples inside data</param>
        public unsafe void Update(float[] data, int samplesCount)
        {
            if (SampleSize != 32)
                throw new ArgumentException();
            fixed (float* ptr = data)
                UpdateAudioStream(this, ptr, samplesCount);
        }

        /// <summary>
        /// Update audio stream buffers with data
        /// </summary>
        /// <param name="data">New data</param>
        /// <param name="samplesCount">Number of samples inside data</param>
        public unsafe void Update(short[] data, int samplesCount)
        {
            if (SampleSize != 16)
                throw new ArgumentException();
            fixed (short* ptr = data)
                UpdateAudioStream(this, ptr, samplesCount);
        }

        /// <summary>
        /// Update audio stream buffers with data
        /// </summary>
        /// <param name="data">New data</param>
        /// <param name="samplesCount">Number of samples inside data</param>
        public unsafe void Update(char[] data, int samplesCount)
        {
            if (SampleSize != 8)
                throw new ArgumentException();
            fixed (char* ptr = data)
                UpdateAudioStream(this, ptr, samplesCount);
        }

        /// <summary>
        /// Check if any audio stream buffers requires refill
        /// </summary>
        public bool IsProcessed => IsAudioStreamProcessed(this);
        /// <summary>
        /// Check if audio stream is playing
        /// </summary>
        public bool IsPlaying => IsAudioStreamPlaying(this);

        /// <summary>
        /// Play audio stream
        /// </summary>
        public void Play() => PlayAudioStream(this);
        /// <summary>
        /// Pause audio stream
        /// </summary>
        public void Pause() => PauseAudioStream(this);
        /// <summary>
        /// Resume audio stream
        /// </summary>
        public void Resume() => ResumeAudioStream(this);
        /// <summary>
        /// Stop audio stream
        /// </summary>
        public void Stop() => StopAudioStream(this);

        /// <summary>
        /// Default size for new audio streams
        /// </summary>
        /// <param name="size">Buffer size</param>
        [DllImport(Constants.dllName, EntryPoint = "SetAudioStreamBufferSizeDefault")]
        public static extern void SetBufferSizeDefault(int size);
    }
}
