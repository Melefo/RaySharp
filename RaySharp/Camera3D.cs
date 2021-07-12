using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Camera3D
    {
        [DllImport(Constants.dllName)]
        private static extern void BeginMode3D(Camera3D camera);
        [DllImport(Constants.dllName)]
        private static extern void EndMode3D();
        [DllImport(Constants.dllName)]
        private static extern void UpdateCamera(ref Camera3D camera);
        [DllImport(Constants.dllName)]
        private static extern Matrix4x4 GetCameraMatrix(Camera3D camera);
        [DllImport(Constants.dllName)]
        private static extern void SetCameraMode(Camera3D camera, int mode);

        private CameraMode _mode;

        public enum CameraMode : int
        {
            CUSTOM = 0,
            FREE = 1,
            ORBITAL = 2,
            FIRST_PERSON = 3,
            THIRD_PERSON = 4
        }

        public enum CameraProjection : int
        {
            PERSPECTIVE = 0,
            ORTHOGRAPHIC = 1
        }

        /// <summary>
        /// Camera position
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// Camera looking at point
        /// </summary>
        public Vector3 Target;
        /// <summary>
        /// Camera up vector (rotation towards target)
        /// </summary>
        public Vector3 Up;
        /// <summary>
        /// Camera field-of-view Y
        /// </summary>
        public float Fovy;
        /// <summary>
        /// Camera mode type
        /// </summary>
        public CameraProjection Projection;
        /// <summary>
        /// Camera transform matrix (view matrix)
        /// </summary>
        public Matrix4x4 Matrix => GetCameraMatrix(this);
        /// <summary>
        /// Set camera mode (multiple camera modes available)
        /// </summary>
        public CameraMode Mode
        {
            get => _mode;
            set
            {
                SetCameraMode(this, (int)value);
                _mode = value;
            }
        }

        /// <summary>
        /// Construct a new Camera3D
        /// </summary>
        /// <param name="position">Camera position</param>
        /// <param name="target">Camera looking at point</param>
        /// <param name="up">Camera up vector (rotation towards target)</param>
        /// <param name="fovy">Camera field-of-view Y</param>
        /// <param name="projection">Camera mode type</param>
        public Camera3D(Vector3 position, Vector3 target, Vector3 up, float fovy, CameraProjection projection)
        {
            Position = position;
            Target = target;
            Up = up;
            Fovy = fovy;
            Projection = projection;
            _mode = CameraMode.CUSTOM;
        }

        /// <summary>
        /// Initializes 3D mode with custom camera (3D)
        /// </summary>
        public void Begin()
        {
            BeginMode3D(this);
        }

        /// <summary>
        /// Ends 3D mode and returns to default 2D orthographic mode
        /// </summary>
        public void End()
        {
            EndMode3D();
        }

        /// <summary>
        /// Update camera position for selected mode
        /// </summary>
        public void Update()
        {
            UpdateCamera(ref this);
        }
    }
}
