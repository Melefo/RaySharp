using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Camera
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Camera2D
    {
        [DllImport(Constants.dllName)]
        private static extern void BeginMode2D(Camera2D camera);
        [DllImport(Constants.dllName)]
        private static extern void EndMode2D();
        [DllImport(Constants.dllName)]
        private static extern Matrix4x4 GetCameraMatrix2D(Camera2D camera);
        [DllImport(Constants.dllName)]
        private static extern Vector2 GetWorldToScreen2D(Vector2 position, Camera2D camera);
        [DllImport(Constants.dllName)]
        private static extern Vector2 GetScreenToWorld2D(Vector2 position, Camera2D camera);

        /// <summary>
        /// Camera offset (displacement from target)
        /// </summary>
        public Vector2 Offset;
        /// <summary>
        /// Camera target (rotation and zoom origin)
        /// </summary>
        public Vector2 Target;
        /// <summary>
        /// Camera rotation in degrees
        /// </summary>
        public float Rotation;
        /// <summary>
        /// Camera zoom (scaling), should be 1.0f by default
        /// </summary>
        public float Zoom;

        /// <summary>
        /// Returns camera 2d transform matrix
        /// </summary>
        public Matrix4x4 Matrix => GetCameraMatrix2D(this);


        /// <summary>
        /// Construct a new Camera2D
        /// </summary>
        /// <param name="offset">Camera offset (displacement from target)</param>
        /// <param name="target">Camera target (rotation and zoom origin)</param>
        /// <param name="rotation">Camera rotation in degrees</param>
        /// <param name="zoom">Camera zoom (scaling), should be 1.0f by default</param>
        public Camera2D(Vector2 offset, Vector2 target, float rotation, float zoom)
        {
            Offset = offset;
            Target = target;
            Rotation = rotation;
            Zoom = zoom;
        }

        /// <summary>
        /// Initialize 2D mode with custom camera (2D)
        /// </summary>
        public void Begin() => BeginMode2D(this);

        /// <summary>
        /// Ends 2D mode with custom camera
        /// </summary>
        public void End() => EndMode2D();

        /// <summary>
        /// Returns the screen space position for a 2d camera world space position
        /// </summary>
        /// <param name="position">World space position</param>
        /// <returns>Screen space position</returns>
        public Vector2 GetWorldToScreen(Vector2 position) => GetWorldToScreen2D(position, this);

        /// <summary>
        /// Returns the world space position for a 2d camera screen space position
        /// </summary>
        /// <param name="position">Screen space position</param>
        /// <returns>World space position</returns>
        public Vector2 GetScreenToWorld(Vector2 position) => GetScreenToWorld2D(position, this);
    }
}
