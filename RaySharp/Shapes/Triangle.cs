using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle
    {
        [DllImport(Constants.dllName)]
        private extern static void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, Color color);
        [DllImport(Constants.dllName)]
        private extern static void DrawTriangleLines(Vector2 v1, Vector2 v2, Vector2 v3, Color color);

        /// <summary>
        /// First point of triangle
        /// </summary>
        public Vector2 Point1 { get; }
        /// <summary>
        /// Second point of triangle
        /// </summary>
        public Vector2 Point2 { get; }
        /// <summary>
        /// Third point of triangle
        /// </summary>
        public Vector2 Point3 { get; }

        public Triangle(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            Point1 = v1;
            Point2 = v2;
            Point3 = v3;
        }

        /// <summary>
        /// Draw a color-filled triangle (vertex in counter-clockwise order!)
        /// </summary>
        /// <param name="color">Color of Triangle</param>
        public void Draw(Color color) => DrawTriangle(Point1, Point2, Point3, color);

        /// <summary>
        ///  Draw triangle outline (vertex in counter-clockwise order!)
        /// </summary>
        /// <param name="color">Color of Triangle</param>
        public void DrawLines(Color color) => DrawTriangleLines(Point1, Point2, Point3, color);
    }

    public static class TriangleExtension
    {
        [DllImport(Constants.dllName)]
        private extern static void DrawTriangleFan(Vector2[] points, int pointsCount, Color color);
        [DllImport(Constants.dllName)]
        private extern static void DrawTriangleStrip(Vector2[] points, int pointsCount, Color color);

        /// <summary>
        /// Draw a triangle strip defined by points
        /// </summary>
        /// <param name="triangles">Triangles to draw</param>
        /// <param name="color">Color to draw triangles</param>
        public static void DrawStrip(this IEnumerable<Triangle> triangles, Color color)
        {
            var array = new List<Vector2>();

            foreach (var triangle in triangles)
            {
                array.Add(triangle.Point1);
                array.Add(triangle.Point2);
                array.Add(triangle.Point3);
            }

            DrawTriangleStrip(array.ToArray(), array.Count / 3, color);
        }

        /// <summary>
        /// Draw a triangle fan defined by points (first vertex is the center)
        /// </summary>
        /// <param name="triangles">Triangles to draw</param>
        /// <param name="color">Color to draw triangles</param>
        public static void DrawFan(this IEnumerable<Triangle> triangles, Color color)
        {
            var array = new List<Vector2>();

            foreach (var triangle in triangles)
            {
                array.Add(triangle.Point1);
                array.Add(triangle.Point2);
                array.Add(triangle.Point3);
            }

            DrawTriangleFan(array.ToArray(), array.Count / 3, color);
        }
    }
}
