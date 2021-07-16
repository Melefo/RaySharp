using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Poly
    {
        [DllImport(Constants.dllName)]
        private extern static void DrawPoly(Vector2 center, int sides, float radius, float rotation, Color color);
        [DllImport(Constants.dllName)]
        private extern static void DrawPolyLines(Vector2 center, int sides, float radius, float rotation, Color color);

        /// <summary>
        /// Center of Polygon
        /// </summary>
        Vector2 Center { get; }
        /// <summary>
        /// Number of sides of Polygon
        /// </summary>
        int Sides { get; }
        /// <summary>
        /// Radius of Polygon
        /// </summary>
        float Radius { get; }
        /// <summary>
        /// Rotation of Polygon
        /// </summary>
        float Rotation { get; }

        public Poly(Vector2 center, int sides, float radius, float rotation)
        {
            Center = center;
            Sides = sides;
            Radius = radius;
            Rotation = rotation;
        }

        /// <summary>
        /// Draw a regular polygon
        /// </summary>
        /// <param name="color">Color of Poly</param>
        public void Draw(Color color) => DrawPoly(Center, Sides, Radius, Rotation, color);

        /// <summary>
        /// Draw a polygon outline of n sides
        /// </summary>
        /// <param name="color">Color of Poly</param>
        public void DrawLines(Color color) => DrawPolyLines(Center, Sides, Radius, Rotation, color);
    }
}
