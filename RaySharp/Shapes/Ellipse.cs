using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Ellipse
    {
        [DllImport(Constants.dllName)]
        private extern static void DrawEllipse(int centerX, int centerY, float radiusH, float radiusV, Color color);
        [DllImport(Constants.dllName)]
        private extern static void DrawEllipseLines(int centerX, int centerY, float radiusH, float radiusV, Color color);

        /// <summary>
        /// Center of ellipse
        /// </summary>
        public Vector2 Center { get; set; }
        /// <summary>
        /// Horizontal radius of ellipse
        /// </summary>
        public float RadiusHorizontal { get; }
        /// <summary>
        /// Vetical radius of ellipse
        /// </summary>
        public float RadiusVertical { get; }

        public Ellipse(Vector2 center, float radiusH, float radiusV)
        {
            Center = center;
            RadiusHorizontal = radiusH;
            RadiusVertical = radiusV;
        }

        /// <summary>
        /// Draw ellipse
        /// </summary>
        /// <param name="color">Color of ellipse</param>
        public void Draw(Color color) => DrawEllipse((int)Center.X, (int)Center.Y, RadiusHorizontal, RadiusVertical, color);
        /// <summary>
        /// Draw ellipse outline
        /// </summary>
        /// <param name="color">Color of ellipse</param>
        public void DrawLines(Color color) => DrawEllipseLines((int)Center.X, (int) Center.Y, RadiusHorizontal, RadiusVertical, color);
    }
}
