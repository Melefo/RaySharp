using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Input
{
    public static class Gesture
    {
        [DllImport(Constants.dllName)]
        private static extern Gestures GetGestureDetected();

        [DllImport(Constants.dllName)]
        private static extern float GetGestureHoldDuration();
        [DllImport(Constants.dllName)]
        private static extern Vector2 GetGestureDragVector();
        [DllImport(Constants.dllName)]
        private static extern float GetGestureDragAngle();
        [DllImport(Constants.dllName)]
        private static extern Vector2 GetGesturePinchVector();
        [DllImport(Constants.dllName)]
        private static extern float GetGesturePinchAngle();

        /// <summary>
        /// Gestures
        /// </summary>
        /// <remarks>
        /// NOTE: It could be used as flags to enable only some gestures
        /// </remarks>
        public enum Gestures : int
        {
            /// <summary>
            /// No gesture
            /// </summary>
            NONE = 0,
            /// <summary>
            /// Tap gesture
            /// </summary>
            TAP = 1,
            /// <summary>
            /// Double tap gesture
            /// </summary>
            DOUBLETAP = 2,
            /// <summary>
            /// Hold gesture
            /// </summary>
            HOLD = 4,
            /// <summary>
            /// Drag gesture
            /// </summary>
            DRAG = 8,
            /// <summary>
            /// Swipe right gesture
            /// </summary>
            SWIPE_RIGHT = 16,
            /// <summary>
            /// Swipe left gesture
            /// </summary>
            SWIPE_LEFT = 32,
            /// <summary>
            /// Swipe up gesture
            /// </summary>
            SWIPE_UP = 64,
            /// <summary>
            /// Swipe down gesture
            /// </summary>
            SWIPE_DOWN = 128,
            /// <summary>
            /// Pinch in gesture
            /// </summary>
            PINCH_IN = 256,
            /// <summary>
            /// Pinch out gesture
            /// </summary>
            PINCH_OUT = 512
        }

        /// <summary>
        /// Get latest detected gesture
        /// </summary>
        public static Gestures GestureDetected => GetGestureDetected();

        /// <summary>
        /// Get gesture hold time in milliseconds
        /// </summary>
        public static float HoldDuration => GetGestureHoldDuration();
        /// <summary>
        /// Get gesture drag vector
        /// </summary>
        public static Vector2 DragVector => GetGestureDragVector();
        /// <summary>
        /// Get gesture drag angle
        /// </summary>
        public static float DragAngle => GetGestureDragAngle();
        /// <summary>
        /// Get gesture pinch delta
        /// </summary>
        public static Vector2 PinchVector => GetGesturePinchVector();
        /// <summary>
        /// Get gesture pinch angle
        /// </summary>
        public static float PinchAngle => GetGesturePinchAngle();

        /// <summary>
        /// Enable a set of gestures using flags
        /// </summary>
        /// <param name="flags">A set of gestures</param>
        [DllImport(Constants.dllName, EntryPoint = "SetGesturesEnabled")]
        public static extern void SetEnabled(Gestures flags);

        /// <summary>
        /// heck if a gesture have been detected
        /// </summary>
        /// <param name="gesture">Gesture to check</param>
        /// <returns>true if gesture have been detected</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsGestureDetected")]
        public static extern bool IsDetected(Gestures gesture);
    }
}
