using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp
{
    public static partial class Window
    {
        [DllImport(Constants.dllName)]
        private static extern void BeginScissorMode(int x, int y, int width, int height);

        /// <summary>
        /// Color blending modes (pre-defined)
        /// </summary>
        public enum BlendMode : int
        {
            /// <summary>
            /// Blend textures considering alpha (default)
            /// </summary>
            BLEND_ALPHA = 0,
            /// <summary>
            /// Blend textures adding colors
            /// </summary>
            BLEND_ADDITIVE = 1,
            /// <summary>
            /// Blend textures multiplying colors
            /// </summary>
            BLEND_MULTIPLIED = 2,
            /// <summary>
            ///  Blend textures adding colors (alternative)
            /// </summary>
            BLEND_ADD_COLORS = 3,
            /// <summary>
            /// Blend textures subtracting colors (alternative)
            /// </summary>
            BLEND_SUBTRACT_COLORS = 4,
            /// <summary>
            /// Blend textures using custom src/dst factors (use SetBlendModeCustom())
            /// </summary>
            BLEND_CUSTOM = 5
        }

        /// <summary>
        /// Set background color (framebuffer clear color)
        /// </summary>
        /// <param name="color"></param>
        [DllImport(Constants.dllName)]
        public static extern void ClearBackground(Color color);

        /// <summary>
        /// Setup canvas (framebuffer) to start drawing
        /// </summary>
        [DllImport(Constants.dllName)]
        public static extern void BeginDrawing();
        /// <summary>
        /// End canvas drawing and swap buffers (double buffering)
        /// </summary>
        [DllImport(Constants.dllName)]
        public static extern void EndDrawing();

        /// <summary>
        /// Begin blending mode (alpha, additive, multiplied)
        /// </summary>
        /// <param name="mode">Blend mode to start</param>
        [DllImport(Constants.dllName)]
        public static extern void BeginBlendMode(BlendMode mode);

        /// <summary>
        /// End blending mode (reset to default: alpha blending)
        /// </summary>
        [DllImport(Constants.dllName)]
        public static extern void EndBlendMode();

        /// <summary>
        /// Begin scissor mode (define screen area for following drawing)
        /// </summary>
        /// <param name="position">Position of screen area</param>
        /// <param name="dimensions">Dimensions of screen area</param>
        public static void BeginScissorMode(Vector2 position, Vector2 dimensions) => BeginScissorMode((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
        /// <summary>
        /// End scissor mode
        /// </summary>
        /// <param name=""></param>
        [DllImport(Constants.dllName)]
        public static extern void EndScissorMode();

        //[DllImport(Constants.dllName)]
        //public static extern void BeginVrStereoMode(VrStereoConfig config);                          // Begin stereo rendering
        //[DllImport(Constants.dllName)]
        //public static extern void EndVrStereoMode(void);                                             // End stereo rendering
    }
}
