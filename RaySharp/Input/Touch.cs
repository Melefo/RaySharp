using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Input
{
    public static class Touch
    {
        [DllImport(Constants.dllName)]
        private static extern int GetTouchX();
        [DllImport(Constants.dllName)]
        private static extern int GetTouchY();

        /// <summary>
        /// Returns touch position XY for touch point 0 (relative to screen size)
        /// </summary>
        public static Vector2 Position => new Vector2(GetTouchX(), GetTouchY());

        /// <summary>
        /// Returns touch position XY for a touch point index (relative to screen size)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport(Constants.dllName, EntryPoint = "GetTouchPosition")]
        private static extern Vector2 GetPosition(int index);
    }
}
