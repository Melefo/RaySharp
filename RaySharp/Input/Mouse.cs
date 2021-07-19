using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Input
{
    public static class Mouse
    {
        [DllImport(Constants.dllName)]
        private static extern Vector2 GetMousePosition();
        [DllImport(Constants.dllName)]
        private static extern void SetMousePosition(int x, int y);
        [DllImport(Constants.dllName)]
        private static extern void SetMouseOffset(int offsetX, int offsetY);
        [DllImport(Constants.dllName)]
        private static extern void SetMouseScale(float scaleX, float scaleY);
        [DllImport(Constants.dllName)]
        private static extern float GetMouseWheelMove();
        [DllImport(Constants.dllName)]
        private static extern void SetMouseCursor(MouseCursor cursor);

        /// <summary>
        /// Mouse buttons
        /// </summary>
        public enum MouseButton : int
        {
            /// <summary>
            /// Mouse button left
            /// </summary>
            LEFT = 0,
            /// <summary>
            /// Mouse button right
            /// </summary>
            RIGHT = 1,
            /// <summary>
            /// Mouse button middle (pressed wheel)
            /// </summary>
            MIDDLE = 2,
            /// <summary>
            /// Mouse button side (advanced mouse device)
            /// </summary>
            SIDE = 3,
            /// <summary>
            /// Mouse button extra (advanced mouse device)
            /// </summary>
            EXTRA = 4,
            /// <summary>
            /// Mouse button fordward (advanced mouse device)
            /// </summary>
            FORWARD = 5,
            /// <summary>
            /// Mouse button back (advanced mouse device)
            /// </summary>
            BACK = 6
        }

        /// <summary>
        /// Mouse buttons
        /// </summary>
        public enum MouseCursor : int
        {
            /// <summary>
            /// Default pointer shape
            /// </summary>
            DEFAULT = 0,
            /// <summary>
            /// Arrow shape
            /// </summary>
            ARROW = 1,
            /// <summary>
            /// Text writing cursor shape
            /// </summary>
            IBEAM = 2,
            /// <summary>
            /// Cross shape
            /// </summary>
            CROSSHAIR = 3,
            /// <summary>
            /// Pointing hand cursor
            /// </summary>
            POINTING_HAND = 4,
            /// <summary>
            /// Horizontal resize/move arrow shape
            /// </summary>
            RESIZE_EW = 5,
            /// <summary>
            /// Vertical resize/move arrow shape
            /// </summary>
            RESIZE_NS = 6,
            /// <summary>
            /// Top-left to bottom-right diagonal resize/move arrow shape
            /// </summary>
            RESIZE_NWSE = 7,
            /// <summary>
            /// The top-right to bottom-left diagonal resize/move arrow shape
            /// </summary>
            RESIZE_NESW = 8,
            /// <summary>
            /// The omni-directional resize/move cursor shape
            /// </summary>
            RESIZE_ALL = 9,
            /// <summary>
            /// The operation-not-allowed shape
            /// </summary>
            NOT_ALLOWED = 10
        }

        private static Vector2 _offset = Vector2.Zero;
        private static Vector2 _scale = Vector2.Zero;
        public static MouseCursor _cursor = MouseCursor.DEFAULT;

        /// <summary>
        /// Get/Set mouse position XY
        /// </summary>
        public static Vector2 Position
        {
            get => GetMousePosition();
            set => SetMousePosition((int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Get/Set mouse offset
        /// </summary>
        public static Vector2 Offset
        {
            get => _offset;
            set
            {
                SetMouseOffset((int)value.X, (int)value.Y);
                _offset = value;
            }
        }

        /// <summary>
        /// Get/Set mouse scale
        /// </summary>
        public static Vector2 Scale
        {
            get => _scale;
            set
            {
                SetMouseScale((int)value.X, (int)value.Y);
                _scale = value;
            }
        }

        /// <summary>
        /// Get/Set mouse cursor
        /// </summary>
        public static MouseCursor Cursor
        {
            get => _cursor;
            set
            {
                SetMouseCursor(value);
                _cursor = value;
            }
        }

        /// <summary>
        /// Returns mouse wheel movement Y
        /// </summary>
        public static float WheelMove => GetMouseWheelMove();

        /// <summary>
        /// Detect if a mouse button has been pressed once
        /// </summary>
        /// <param name="button">Mouse button</param>
        /// <returns>true if mouse button has been pressed once</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsMouseButtonPressed")]
        public static extern bool IsPressed(MouseButton button);
        /// <summary>
        /// Detect if a mouse button is being pressed
        /// </summary>
        /// <param name="button">Mouse button</param>
        /// <returns>true if mouse button is being pressed</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsMouseButtonDown")]
        public static extern bool IsDown(MouseButton button);
        /// <summary>
        /// Detect if a mouse button has been released once
        /// </summary>
        /// <param name="button">Mouse button</param>
        /// <returns>true if mouse button has been released once</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsReleased")]
        public static extern bool IsMouseButtonReleased(MouseButton button);
        /// <summary>
        /// Detect if a mouse button is NOT being pressed
        /// </summary>
        /// <param name="button">Mouse button</param>
        /// <returns>true if mouse button is not being pressed</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsUp")]
        public static extern bool IsMouseButtonUp(MouseButton button);
    }
}
