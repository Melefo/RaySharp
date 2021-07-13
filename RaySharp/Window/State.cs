using System.Runtime.InteropServices;

namespace RaySharp
{
    public static partial class Window
    {
        [DllImport(Constants.dllName)]
        private static extern bool IsWindowState(uint flag);
        [DllImport(Constants.dllName)]
        private static extern void SetWindowState(uint flags);
        [DllImport(Constants.dllName)]
        private static extern void ClearWindowState(uint flags);

        /// <summary>
        /// System/Window config flags
        /// </summary>
        /// <remarks>
        /// NOTE: Every bit registers one state (use it with bit masks)
        /// By default all flags are set to 0
        /// </remarks>
        public enum Flags : uint
        {
            /// <summary>
            /// Set to try enabling V-Sync on GPU
            /// </summary>
            VSYNC_HINT = 0x00000040,
            /// <summary>
            /// Set to run program in fullscreen
            /// </summary>
            FULLSCREEN_MODE = 0x00000002,
            /// <summary>
            /// Set to allow resizable window
            /// </summary>
            WINDOW_RESIZABLE = 0x00000004,
            /// <summary>
            /// Set to disable window decoration (frame and buttons)
            /// </summary>
            WINDOW_UNDECORATED = 0x00000008,
            /// <summary>
            /// Set to hide window
            /// </summary>
            WINDOW_HIDDEN = 0x00000080,
            /// <summary>
            /// Set to minimize window (iconify)
            /// </summary>
            WINDOW_MINIMIZED = 0x00000200,
            /// <summary>
            /// Set to maximize window (expanded to monitor)
            /// </summary>
            WINDOW_MAXIMIZED = 0x00000400,
            /// <summary>
            /// Set to window non focused
            /// </summary>
            WINDOW_UNFOCUSED = 0x00000800,
            /// <summary>
            /// Set to window always on top
            /// </summary>
            WINDOW_TOPMOST = 0x00001000,
            /// <summary>
            /// Set to allow windows running while minimized
            /// </summary>
            WINDOW_ALWAYS_RUN = 0x00000100,
            /// <summary>
            /// Set to allow transparent framebuffer
            /// </summary>
            WINDOW_TRANSPARENT = 0x00000010,
            /// <summary>
            /// Set to support HighDPI
            /// </summary>
            WINDOW_HIGHDPI = 0x00002000,
            /// <summary>
            /// Set to try enabling MSAA 4X
            /// </summary>
            MSAA_4X_HINT = 0x00000020,
            /// <summary>
            /// Set to try enabling interlaced video format (for V3D)
            /// </summary>
            INTERLACED_HINT = 0x00010000
        }

        /// <summary>
        /// Check if one specific window flag is enabled
        /// </summary>
        /// <param name="flag">Flag to check</param>
        /// <returns>true if flag is enabled</returns>
        public static bool IsState(Flags flag) => IsWindowState((uint)flag);
        /// <summary>
        /// Set window configuration state using flags
        /// </summary>
        /// <param name="flags">Flags to set</param>
        public static void SetState(Flags flags) => SetWindowState((uint)flags);
        /// <summary>
        /// Clear window configuration state flags
        /// </summary>
        /// <param name="flags">Flags to clear the window with</param>
        public static void ClearState(Flags flags) => ClearWindowState((uint)flags);
    }
}
