using System.Drawing;
using System.Runtime.InteropServices;

namespace RaySharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NPatchInfo
    {
        /// <summary>
        /// N-patch layout
        /// </summary>
        public enum NPatchLayout : int
        {
            /// <summary>
            /// Npatch layout: 3x3 tiles
            /// </summary>
            NINE_PATCH = 0,
            /// <summary>
            /// Npatch layout: 1x3 tiles
            /// </summary>
            THREE_PATCH_VERTICAL = 1,
            /// <summary>
            /// Npatch layout: 3x1 tiles
            /// </summary>
            THREE_PATCH_HORIZONTAL = 2
        }

        /// <summary>
        /// Texture source rectangle
        /// </summary>
        public Rectangle Source { get; }
        /// <summary>
        /// Left border offset
        /// </summary>
        public int Left { get; }
        /// <summary>
        /// Top border offset
        /// </summary>
        public int Top { get; }
        /// <summary>
        /// Right border offset
        /// </summary>
        public int Right { get; }
        /// <summary>
        /// Bottom border offset
        /// </summary>
        public int Bottom { get; }
        /// <summary>
        /// Layout of the n-patch: 3x3, 1x3 or 3x1
        /// </summary>
        public NPatchLayout Layout { get; }

        /// <summary>
        /// Construct a new NPathInfo
        /// </summary>
        /// <param name="source">Texture source rectangle</param>
        /// <param name="left">Left border offset</param>
        /// <param name="top">Top border offset</param>
        /// <param name="right">Right border offset</param>
        /// <param name="bottom">Bottom border offset</param>
        /// <param name="layout">Layout of the n-patch: 3x3, 1x3 or 3x1</param>
        public NPatchInfo(Rectangle source, int left, int top, int right, int bottom, NPatchLayout layout)
        {
            Source = source;
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
            Layout = layout;
        }
    }
}
