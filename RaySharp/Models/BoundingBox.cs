using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BoundingBox
    {
        /// <summary>
        /// Minimum vertex box-corner
        /// </summary>
        public Vector3 Min { get; }
        /// <summary>
        /// Maximum vertex box-corner
        /// </summary>
        public Vector3 Max { get; }

        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }
    }
}
