using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Pixel
    {
        [DllImport(Constants.dllName)]
        private static extern void DrawPixelV(Vector2 position, Color color);

        /// <summary>
        /// Position of pixel
        /// </summary>
        public Vector2 Position { get; }
        /// <summary>
        /// Color of pixel
        /// </summary>
        public Color Color { get; }

        public Pixel(Vector2 position, Color color)
        {
            Position = position;
            Color = color;
        }

        /// <summary>
        /// Draw a pixel
        /// </summary>
        public void Draw() => DrawPixelV(Position, Color);
    }
}
