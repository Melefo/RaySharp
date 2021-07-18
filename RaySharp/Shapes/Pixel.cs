using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Pixel
    {
        [DllImport(Constants.dllName)]
        private static extern void DrawPixelV(Vector2 position, Color color);
        [DllImport(Constants.dllName)]
        private static extern bool CheckCollisionPointRec(Vector2 point, Rectangle rec);
        [DllImport(Constants.dllName)]
        private static extern bool CheckCollisionPointCircle(Vector2 point, Vector2 center, float radius);
        [DllImport(Constants.dllName)]
        private static extern bool CheckCollisionPointTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3);

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

        /// <summary>
        /// Check if point is inside a rectangle
        /// </summary>
        /// <param name="rec">A rectangle</param>
        /// <returns>true if point if inside rectangle</returns>
        public bool CheckCollisionRec(Rectangle rec) => CheckCollisionPointRec(Position, rec);
        /// <summary>
        /// Check if point is inside a circle
        /// </summary>
        /// <param name="circle">A circle</param>
        /// <returns>true if point if inside circle</returns>
        public bool CheckCollisionCircle(Circle circle) => CheckCollisionPointCircle(Position, circle.Center, circle.Radius);
        /// <summary>
        /// Check if point is inside a triangle
        /// </summary>
        /// <param name="triangle">A triangle</param>
        /// <returns>true if point is inside triangle</returns>
        public bool CheckCollisionTriangle(Triangle triangle) => CheckCollisionPointTriangle(Position, triangle.Point1, triangle.Point2, triangle.Point3);
    }
}
