using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp
{
    public static partial class Window
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void InitWindow(int width, int height, string title);
        [DllImport(Constants.dllName)]
        private static extern void CloseWindow();

        private static bool _initialized;

        /// <summary>
        /// Initialize window and OpenGL context
        /// </summary>
        /// <param name="width">Width of the window</param>
        /// <param name="height">Height of the window</param>
        /// <param name="title">title of the window</param>
        public static void Init(Vector2 size, string title)
        {
            if (_initialized)
                return;
            InitWindow((int)size.X, (int)size.Y, title);
            _title = title;
            _initialized = true;
        }

        /// <summary>
        /// Close window and unload OpenGL context
        /// </summary>
        public static void Close()
        {
            if (_initialized)
                CloseWindow();
            _initialized = false;
        }
    }
}
