using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Ray
    {
        /// <summary>
        /// Ray position (origin)
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// Ray direction
        /// </summary>
        public Vector3 Direction;

        public Ray(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}
