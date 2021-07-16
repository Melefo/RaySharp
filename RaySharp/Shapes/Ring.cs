using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Ring
    {
        [DllImport(Constants.dllName)]
        private extern static void DrawRing(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, Color color);
        [DllImport(Constants.dllName)]
        private extern static void DrawRingLines(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, Color color);

        /// <summary>
        /// Center of ring
        /// </summary>
        public Vector2 Center { get; }
        /// <summary>
        /// Radius of inner circle
        /// </summary>
        public float InnerRadius { get; }
        /// <summary>
        /// Radius of outer circle
        /// </summary>
        public float OuterRadius { get; }
        /// <summary>
        /// Start of ring angle
        /// </summary>
        public float StartAngle { get; }
        /// <summary>
        /// End of ring angle
        /// </summary>
        public float EndAngle { get; }
        /// <summary>
        /// Number of segments composing the ring
        /// </summary>
        public int Segments { get; }

        public Ring(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments)
        {
            Center = center;
            InnerRadius = innerRadius;
            OuterRadius = outerRadius;
            StartAngle = startAngle;
            EndAngle = endAngle;
            Segments = segments;
        }

        /// <summary>
        /// Draw ring
        /// </summary>
        /// <param name="color">Color of Ring</param>
        public void Draw(Color color) => DrawRing(Center, InnerRadius, OuterRadius, StartAngle, EndAngle, Segments, color);

        /// <summary>
        /// Draw ring outline
        /// </summary>
        /// <param name="color">Color of Ring</param>
        public void DrawLines(Color color) => DrawRingLines(Center, InnerRadius, OuterRadius, StartAngle, EndAngle, Segments, color);
    }
}
