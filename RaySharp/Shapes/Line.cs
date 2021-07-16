using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Line
    {
        [DllImport(Constants.dllName)]
        private extern static void DrawLineEx(Vector2 startPos, Vector2 endPos, float thick, Color color);
        [DllImport(Constants.dllName)]
        private extern static void DrawLineBezier(Vector2 startPos, Vector2 endPos, float thick, Color color);
        [DllImport(Constants.dllName)]
        private extern static void DrawLineBezierQuad(Vector2 startPos, Vector2 endPos, Vector2 controlPos, float thick, Color color);

        /// <summary>
        /// Start position of line
        /// </summary>
        public Vector2 Start { get; }
        /// <summary>
        /// End position of line
        /// </summary>
        public Vector2 End { get; }

        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Draw a line
        /// </summary>
        /// <param name="color">Color of line</param>
        /// <param name="thickness">Thickness of line</param>
        public void Draw(Color color, float thickness = 1) => DrawLineEx(Start, End, thickness, color);

        /// <summary>
        /// Draw a line using cubic-bezier curves in-out
        /// </summary>
        /// <param name="color">Color of line</param>
        /// <param name="thickness">Thickness of line</param>
        public void DrawBezier(Color color, float thickness = 1) => DrawLineBezier(Start, End, thickness, color);
        /// <summary>
        /// Draw line using quadratic bezier curves with a control point
        /// </summary>
        /// <param name="controlPosition">Control point</param>
        /// <param name="color">Color of line</param>
        /// <param name="thickness">Thickness of line</param>
        public void DrawBezierQuad(Vector2 controlPosition, Color color, float thickness = 1) => DrawLineBezierQuad(Start, End, controlPosition, thickness, color);
    }

    public static class LineExtension
    {
        [DllImport(Constants.dllName)]
        private extern static void DrawLineStrip(Vector2[] points, int pointsCount, Color color);

        /// <summary>
        /// Draw lines sequence
        /// </summary>
        /// <param name="lines">Lines to draw</param>
        /// <param name="color">Color to draw lines</param>
        public static void DrawStrip(this IEnumerable<Line> lines, Color color)
        {
            var array = new List<Vector2>();

            foreach (var line in lines)
            {
                array.Add(line.Start);
                array.Add(line.End);
            }

            DrawLineStrip(array.ToArray(), array.Count / 2, color);
        }
    }
}
