using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Circle
    {
        [DllImport(Constants.dllName)]
        private static extern void DrawCircleV(Vector2 center, float radius, Color color);
        [DllImport(Constants.dllName)]
        private static extern void DrawCircleLines(int centerX, int centerY, float radius, Color color);
        [DllImport(Constants.dllName)]
        private static extern void DrawCircleGradient(int centerX, int centerY, float radius, Color color1, Color color2);
        [DllImport(Constants.dllName)]
        private static extern void DrawCircleSector(Vector2 center, float radius, float startAngle, float endAngle, int segments, Color color);
        [DllImport(Constants.dllName)]
        private static extern void DrawCircleSectorLines(Vector2 center, float radius, float startAngle, float endAngle, int segments, Color color);

        /// <summary>
        /// Center of circle
        /// </summary>
        public Vector2 Center;
        /// <summary>
        /// Radius of circle
        /// </summary>
        public float Radius;

        /// <summary>
        /// Construct a new Circle
        /// </summary>
        /// <param name="center">Center of circle</param>
        /// <param name="radius">Radius of circle</param>
        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Draw a color-filled circle
        /// </summary>
        /// <param name="color">Color to fill circle with</param>
        public void Draw(Color color) => DrawCircleV(Center, Radius, color);

        /// <summary>
        /// Draw circle outline
        /// </summary>
        /// <param name="color">Color of outline</param>
        public void DrawLines(Color color) => DrawCircleLines((int)Center.X, (int)Center.Y, Radius, color);

        /// <summary>
        /// Draw a gradient-filled circle
        /// </summary>
        /// <param name="color1">Center color</param>
        /// <param name="color2">Outline color</param>
        public void DrawGradient(Color color1, Color color2) => DrawCircleGradient((int)Center.X, (int)Center.Y, Radius, color1, color2);

        /// <summary>
        /// Draw a piece of a circle
        /// </summary>
        /// <param name="color">Color of piece</param>
        /// <param name="startAngle">Start of piece</param>
        /// <param name="engAngle">End of piece</param>
        /// <param name="segments">Number of segments to make piece</param>
        public void DrawSector(Color color, float startAngle, float engAngle, int segments) => DrawCircleSector(Center, Radius, startAngle, engAngle, segments, color);

        /// <summary>
        /// Draw circle sector outline
        /// </summary>
        /// <param name="color">Color of piece</param>
        /// <param name="startAngle">Start of piece</param>
        /// <param name="engAngle">End of piece</param>
        /// <param name="segments">Number of segments to make piece</param>
        public void DrawSectorLines(Color color, float startAngle, float engAngle, int segments) => DrawCircleSectorLines(Center, Radius, startAngle, engAngle, segments, color);
    }
}
