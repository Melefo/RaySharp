using System.Runtime.InteropServices;

namespace RaySharp
{
    public static class Cursor
    {
        [DllImport(Constants.dllName)]
        private static extern void ShowCursor();
        [DllImport(Constants.dllName)]
        private static extern void HideCursor();

        [DllImport(Constants.dllName)]
        private static extern bool IsCursorHidden();

        [DllImport(Constants.dllName)]
        private static extern void EnableCursor();
        [DllImport(Constants.dllName)]
        private static extern void DisableCursor();

        [DllImport(Constants.dllName)]
        private static extern bool IsCursorOnScreen();

        private static bool _enabled;

        /// <summary>
        /// Set/Get if cursor is not visible
        /// </summary>
        public static bool Hidden
        {
            get => IsCursorHidden();
            set
            {
                if (value)
                    HideCursor();
                else
                    ShowCursor();
            }
        }

        /// <summary>
        /// Enables/Disables cursor
        /// </summary>
        public static bool Enabled
        {
            get => _enabled;
            set
            {
                if (value)
                    EnableCursor();
                else
                    DisableCursor();
                _enabled = value;
            }
        }

        /// <summary>
        /// Check if cursor is on the current screen
        /// </summary>
        public static bool IsOnScreen => IsCursorOnScreen();

    }
}
