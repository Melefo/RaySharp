using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp
{
    public static class Monitor
    {
        [DllImport(Constants.dllName)]
        private static extern int GetMonitorWidth(int monitor);
        [DllImport(Constants.dllName)]
        private static extern int GetMonitorHeight(int monitor);
        [DllImport(Constants.dllName)]
        private static extern int GetMonitorPhysicalWidth(int monitor);
        [DllImport(Constants.dllName)]
        private static extern int GetMonitorPhysicalHeight(int monitor);
        [DllImport(Constants.dllName)]
        private static extern int GetMonitorCount();

        /// <summary>
        /// Get number of connected monitors
        /// </summary>
        public static int Count => GetMonitorCount();

        /// <summary>
        /// Get specified monitor position
        /// </summary>
        /// <param name="monitor">Monitor ID</param>
        /// <returns>Monitor position</returns>
        [DllImport(Constants.dllName, EntryPoint = "GetMonitorPosition")]
        public static extern Vector2 GetPosition(int monitor);

        /// <summary>
        /// Get specified monitor refresh rate
        /// </summary>
        /// <param name="monitor">Monitor ID</param>
        /// <returns>Monitor refresh rate</returns>
        [DllImport(Constants.dllName, EntryPoint = "GetMonitorRefreshRate")]
        public static extern int GetRefreshRate(int monitor);
        /// <summary>
        /// Get the human-readable, UTF-8 encoded name of the primary monitor
        /// </summary>
        /// <param name="monitor">Monitor ID</param>
        /// <returns>Monitor name</returns>
        [DllImport(Constants.dllName, EntryPoint = "GetMonitorName")]
        public static extern string GetName(int monitor);

        /// <summary>
        /// Get specified monitor size
        /// </summary>
        /// <param name="monitor">Monitor ID</param>
        /// <returns>Monitor size</returns>
        public static Vector2 GetSize(int monitor) => new Vector2(GetMonitorWidth(monitor), GetMonitorHeight(monitor));
        /// <summary>
        /// Get specified monitor physical size in millimetres
        /// </summary>
        /// <param name="monitor">Monitor ID</param>
        /// <returns>Monitor physical size</returns>
        public static Vector2 GetPhysicalSize(int monitor) => new Vector2(GetMonitorPhysicalWidth(monitor), GetMonitorPhysicalHeight(monitor));
    }
}
