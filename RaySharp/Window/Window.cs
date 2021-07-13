using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp
{
    public static partial class Window
    {
        [DllImport(Constants.dllName)]
        private static extern bool WindowShouldClose();

        [DllImport(Constants.dllName)]
        private static extern bool IsWindowReady();
        [DllImport(Constants.dllName)]
        private static extern bool IsWindowFullscreen();
        [DllImport(Constants.dllName)]
        private static extern bool IsWindowHidden();
        [DllImport(Constants.dllName)]
        private static extern bool IsWindowMinimized();
        [DllImport(Constants.dllName)]
        private static extern bool IsWindowMaximized();
        [DllImport(Constants.dllName)]
        private static extern bool IsWindowFocused();
        [DllImport(Constants.dllName)]
        private static extern bool IsWindowResized();

        [DllImport(Constants.dllName)]
        private static extern int GetScreenWidth();
        [DllImport(Constants.dllName)]
        private static extern int GetScreenHeight();
        [DllImport(Constants.dllName)]
        private static extern void SetWindowSize(int width, int height);
        [DllImport(Constants.dllName)]
        private static extern void SetWindowMinSize(int width, int height);

        [DllImport(Constants.dllName)]
        private static extern void ToggleFullscreen();
        [DllImport(Constants.dllName)]
        private static extern void MaximizeWindow();
        [DllImport(Constants.dllName)]
        private static extern void MinimizeWindow();
        [DllImport(Constants.dllName)]
        private static extern void RestoreWindow();

        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void SetWindowTitle(string title);

        [DllImport(Constants.dllName)]
        private static extern void SetWindowPosition(int x, int y);
        [DllImport(Constants.dllName)]
        private static extern Vector2 GetWindowPosition();

        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void SetClipboardText(string text);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern string GetClipboardText();

        [DllImport(Constants.dllName)]
        private static extern void SetWindowMonitor(int monitor);
        [DllImport(Constants.dllName)]
        private static extern int GetCurrentMonitor();

        [DllImport(Constants.dllName)]
        private static extern IntPtr GetWindowHandle();

        [DllImport(Constants.dllName)]
        private static extern Vector2 GetWindowScaleDPI();

        private static string _title;
        private static Vector2 _minSize;

        /// <summary>
        /// Check if KEY_ESCAPE pressed or Close icon pressed
        /// </summary>
        public static bool ShouldClose => WindowShouldClose();

        /// <summary>
        /// Check if window has been initialized successfully
        /// </summary>
        public static bool Ready => IsWindowReady();

        /// <summary>
        /// Get window scale DPI factor
        /// </summary>
        public static Vector2 ScaleDPI => GetWindowScaleDPI();

        /// <summary>
        /// Check if window is currently hidden
        /// </summary>
        public static bool Hidden => IsWindowHidden();

        /// <summary>
        /// Check if window is currently focused
        /// </summary>
        public static bool Focused => IsWindowFocused();

        /// <summary>
        /// Check if window has been resized last frame
        /// </summary>
        public static bool Resized => IsWindowResized();

        /// <summary>
        /// Check and set if window is in fullscreen
        /// </summary>
        public static bool Fullscreen
        {
            get => IsWindowFullscreen();
            set
            {
                if (Fullscreen != value)
                    ToggleFullscreen();
            }
        }

        /// <summary>
        /// Get/Set window dimensions
        /// </summary>
        public static Vector2 Size
        {
            get => new Vector2(GetScreenWidth(), GetScreenHeight());
            set => SetWindowSize((int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Check and set if window is minimized
        /// </summary>
        public static bool Minimized
        {
            get => IsWindowMinimized();
            set
            {
                if (value)
                    MinimizeWindow();
                else if (Minimized)
                    RestoreWindow();
            }
        }

        /// <summary>
        /// Check and set if window is maximized
        /// </summary>
        public static bool Maximized
        {
            get => IsWindowMaximized();
            set
            {
                if (value)
                    MaximizeWindow();
                else if (Maximized)
                    RestoreWindow();
            }
        }

        /// <summary>
        /// Get/Set current window title
        /// </summary>
        public static string Title
        {
            get => _title;
            set
            {
                SetWindowTitle(value);
                _title = value;
            }
        }

        /// <summary>
        /// Get/Set window position on screen 
        /// </summary>
        public static Vector2 Position
        {
            get => GetWindowPosition();
            set => SetWindowPosition((int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Get/Set clipboard text content
        /// </summary>
        public static string Clipboard
        {
            get => GetClipboardText();
            set => SetClipboardText(value);
        }

        /// <summary>
        /// Get/Set Monitor on which the window is displayed
        /// </summary>
        public static int Monitor
        {
            get => GetCurrentMonitor();
            set => SetWindowMonitor(value);
        }

        /// <summary>
        /// Get/Set window minimum dimensions
        /// </summary>
        public static Vector2 MinSize
        {
            get => _minSize;
            set
            {
                SetWindowMinSize((int)value.X, (int)value.Y);
                _minSize = value;
            }
        }

        /// <summary>
        /// Set icon for window
        /// </summary>
        /// <param name="image">Icon</param>
        [DllImport(Constants.dllName, EntryPoint = "SetWindowIcon")]
        public static extern void SetIcon(Image image);
    }
}
