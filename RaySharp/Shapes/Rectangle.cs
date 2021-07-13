using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        [DllImport(Constants.dllName)]
        private static extern void DrawRectanglePro(Rectangle rec, Vector2 origin, float rotation, Color color);
        [DllImport(Constants.dllName)]
        private static extern void DrawRectangleGradientV(int posX, int posY, int width, int height, Color color1, Color color2); // Draw a vertical-gradient-filled rectangle
        [DllImport(Constants.dllName)]
        private static extern void DrawRectangleGradientH(int posX, int posY, int width, int height, Color color1, Color color2); // Draw a horizontal-gradient-filled rectangle
        [DllImport(Constants.dllName)]
        private static extern void DrawRectangleGradientEx(Rectangle rec, Color col1, Color col2, Color col3, Color col4);        // Draw a gradient-filled rectangle with custom vertex colors
        [DllImport(Constants.dllName)]
        private static extern void DrawRectangleRounded(Rectangle rec, float roundness, int segments, Color color);               // Draw rectangle with rounded edges
        [DllImport(Constants.dllName)]
        private static extern void DrawRectangleRoundedLines(Rectangle rec, float roundness, int segments, int lineThick, Color color); // Draw rectangle with rounded edges outline
        [DllImport(Constants.dllName)]
        private static extern void DrawRectangleLinesEx(Rectangle rec, int lineThick, Color color);

        /// <summary>
        /// Rectangle position
        /// </summary>
        public Vector2 Location { get; }
        /// <summary>
        /// Rectangle size
        /// </summary>
        public Vector2 Size { get; }

        public Rectangle(Vector2 location, Vector2 size)
        {
            Location = location;
            Size = size;
        }

        /// <summary>
        /// Draw a color-filled rectangle
        /// </summary>
        /// <param name="color">Color of rectangle</param>
        /// <param name="origin">Origin of rotation</param>
        /// <param name="rotation">Angle of rotation</param>
        public void Draw(Color color, Vector2 origin = new Vector2(), float rotation = 0) => DrawRectanglePro(this, origin, rotation, color);

        /// <summary>
        /// Draw rectangle outline
        /// </summary>
        /// <param name="color">Color of outlines</param>
        /// <param name="lineThick">Thickness of lines</param>
        public void DrawLines(Color color, int lineThick = 1) => DrawRectangleLinesEx(this, lineThick, color);

        /// <summary>
        /// Draw a vertical-gradient-filled rectangle
        /// </summary>
        /// <param name="color1">Top color</param>
        /// <param name="color2">Bottom color</param>
        public void DrawVerticalGradient(Color color1, Color color2) => DrawRectangleGradientV((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y, color1, color2);
        /// <summary>
        /// Draw a horizontal-gradient-filled rectangle
        /// </summary>
        /// <param name="color1">Left color</param>
        /// <param name="color2">Right color</param>
        public void DrawHorizontalGradient(Color color1, Color color2) => DrawRectangleGradientH((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y, color1, color2);
        /// <summary>
        /// Draw a gradient-filled rectangle with custom vertex colors
        /// </summary>
        /// <param name="color1">Upper-left angle color</param>
        /// <param name="color2">Bottom-left angle color</param>
        /// <param name="color3">Bottom-right angle color</param>
        /// <param name="color4">Upper-right color</param>
        public void DrawGradient(Color color1, Color color2, Color color3, Color color4) => DrawRectangleGradientEx(this, color1, color2, color3, color4);

        /// <summary>
        /// Draw rectangle with rounded edges
        /// </summary>
        /// <param name="color">Color of rectangle</param>
        /// <param name="roundness">Level of roundness</param>
        /// <param name="segments">Number of segments to make rounded angle</param>
        public void DrawRounded(Color color, float roundness, int segments) => DrawRectangleRounded(this, roundness, segments, color);
        /// <summary>
        /// Draw rectangle with rounded edges outline
        /// </summary>
        /// <param name="color">Color of outlines</param>
        /// <param name="roundness">Level of roundness</param>
        /// <param name="segments">Number of segments to make rounded angle</param>
        /// <param name="lineThick">Thickness of lines</param>
        public void DrawRoundedLines(Color color, float roundness, int segments, int lineThick = 1) => DrawRectangleRoundedLines(this, roundness, segments, lineThick, color);
    }
}
