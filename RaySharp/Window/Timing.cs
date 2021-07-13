using System.Runtime.InteropServices;

namespace RaySharp
{
    public static partial class Window
    {
        [DllImport(Constants.dllName)]
        private static extern void SetTargetFPS(int fps);
        [DllImport(Constants.dllName)]
        private static extern int GetFPS();
        [DllImport(Constants.dllName)]
        private static extern float GetFrameTime();
        [DllImport(Constants.dllName)]
        private static extern double GetTime();


        /// <summary>
        /// Get current FPS
        /// Set target FPS (maximum)
        /// </summary>
        public static int FPS
        {
            get => GetFPS();
            set => SetTargetFPS(value);
        }

        /// <summary>
        /// Returns elapsed time in seconds since InitWindow()
        /// </summary>
        public static double Time => GetTime();

        /// <summary>
        /// Returns time in seconds for last frame drawn (delta time)
        /// </summary>
        public static double FrameTime => GetFrameTime();
    }
}
