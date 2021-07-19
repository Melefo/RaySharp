using System;
using System.Runtime.InteropServices;

namespace RaySharp.Audio
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Wave : IDisposable
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Wave LoadWave(string fileName);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Wave LoadWaveFromMemory(string fileType, IntPtr fileData, int dataSize);
        [DllImport(Constants.dllName)]
        private static extern void UnloadWave(Wave wave);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern bool ExportWave(Wave wave, string fileName);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern bool ExportWaveAsCode(Wave wave, string fileName);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void WaveFormat(ref Wave wave, int sampleRate, int sampleSize, int channels);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Wave WaveCopy(Wave wave);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void WaveCrop(ref Wave wave, int initSample, int finalSample);

        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadWaveSamples(Wave wave);

        /// <summary>
        /// Number of samples
        /// </summary>
        public uint SampleCount { get; private set; }
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
        /// Buffer data pointer (void *)
        /// </summary>
        public IntPtr Data { get; private set; }

        /// <summary>
        ///Load wave data from file
        /// </summary>
        /// <param name="fileName">Filepath</param>
        public Wave(string fileName)
        {
            var wave = LoadWave(fileName);

            SampleCount = wave.SampleCount;
            SampleRate = wave.SampleRate;
            SampleSize = wave.SampleSize;
            Channels = wave.Channels;
            Data = wave.Data;
        }

        /// <summary>
        /// Load wave from memory buffer
        /// </summary>
        /// <param name="fileType">File type</param>
        /// <param name="data">Wave data</param>
        /// <param name="dataSize">Data size</param>
        public Wave(string fileType, IntPtr data, int dataSize)
        {
            var wave = LoadWaveFromMemory(fileType, data, dataSize);

            SampleCount = wave.SampleCount;
            SampleRate = wave.SampleRate;
            SampleSize = wave.SampleSize;
            Channels = wave.Channels;
            Data = wave.Data;
        }

        /// <summary>
        /// Copy a wave to a new wave
        /// </summary>
        /// <param name="copy">Wave to copy</param>
        public Wave(Wave copy)
        {
            var wave = WaveCopy(copy);

            SampleCount = wave.SampleCount;
            SampleRate = wave.SampleRate;
            SampleSize = wave.SampleSize;
            Channels = wave.Channels;
            Data = wave.Data;
        }

        /// <summary>
        /// Unload wave data
        /// </summary>
        public void Dispose()
        {
            UnloadWave(this);
            SampleCount = 0;
            SampleRate = 0;
            SampleSize = 0;
            Channels = 0;
            Data = IntPtr.Zero;
        }

        /// <summary>
        /// Export wave data to file, returns true on success
        /// </summary>
        /// <param name="fileName">File path</param>
        /// <param name="asCode">Export wave sample data to code (.h)</param>
        /// <returns>true on success</returns>
        public bool Export(string fileName, bool asCode = false) => asCode ? ExportWaveAsCode(this, fileName) : ExportWave(this, fileName);

        /// <summary>
        /// Convert wave data to desired format
        /// </summary>
        /// <param name="sampleRate">Desired sample rate</param>
        /// <param name="sampleSize">Desired sample size</param>
        /// <param name="channels">Desired channels number</param>
        public void Format(int sampleRate, int sampleSize, int channels) => WaveFormat(ref this, sampleRate, sampleSize, channels);

        /// <summary>
        /// Crop a wave to defined samples range
        /// </summary>
        /// <param name="initSample">Current sample</param>
        /// <param name="finalSample">Desired sample</param>
        public void Crop(int initSample, int finalSample) => WaveCrop(ref this, initSample, finalSample);

        /// <summary>
        /// Load samples data from wave as a floats array
        /// </summary>
        /// <returns>floats array</returns>
        public IntPtr LoadSamples() => LoadWaveSamples(this);
    }

    public static class WaveExtension
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void UnloadWaveSamples(IntPtr samples);

        /// <summary>
        /// Unload samples data loaded with LoadSamples()
        /// </summary>
        /// <param name="samples">Samples from Wave</param>
        public static void DisposeWaveSamples(this IntPtr samples) => UnloadWaveSamples(samples);
    }
}
