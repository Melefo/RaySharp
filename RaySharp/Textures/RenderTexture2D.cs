using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Textures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RenderTexture2D : IDisposable
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern RenderTexture2D LoadRenderTexture(int width, int height);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void UnloadRenderTexture(RenderTexture2D target);
        [DllImport(Constants.dllName)]
        private static extern void BeginTextureMode(RenderTexture2D target);
        [DllImport(Constants.dllName)]
        private static extern void EndTextureMode();

        /// <summary>
        /// Render Id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Render tTexture
        /// </summary>
        public Texture2D Texture { get; private set; }
        /// <summary>
        /// Render depth
        /// </summary>
        public Texture2D Depth { get; private set; }

        /// <summary>
        /// Load texture for rendering (framebuffer)
        /// </summary>
        /// <param name="dimensions">Dimensions of texture</param>
        public RenderTexture2D(Vector2 dimensions)
        {
            var render = LoadRenderTexture((int)dimensions.X, (int)dimensions.Y);

            Id = render.Id;
            Texture = render.Texture;
            Depth = render.Depth;
        }

        /// <summary>
        /// Unload render texture from GPU memory (VRAM)
        /// </summary>
        public void Dispose()
        {
            UnloadRenderTexture(this);
            Id = 0;
            Texture.Dispose();
            Depth.Dispose();
        }

        /// <summary>
        /// Initializes render texture for drawing
        /// </summary>
        public void Begin() => BeginTextureMode(this);
        /// <summary>
        /// Ends drawing to render texture
        /// </summary>
        public void End() => EndTextureMode();
    }
}
