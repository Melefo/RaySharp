using RaySharp.Shapes;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using static RaySharp.Textures.Image;

namespace RaySharp.Textures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Texture2D : IDisposable
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Texture2D LoadTexture(string fileName);
        [DllImport(Constants.dllName)]
        private static extern Texture2D LoadTextureFromImage(Image image);
        [DllImport(Constants.dllName)]
        private static extern void UnloadTexture(Texture2D texture);

        [DllImport(Constants.dllName)]
        private static extern void UpdateTexture(Texture2D texture, IntPtr pixels);
        [DllImport(Constants.dllName)]
        private static extern void UpdateTextureRec(Texture2D texture, Rectangle rec, IntPtr pixels);
        [DllImport(Constants.dllName)]
        private static extern void GenTextureMipmaps(ref Texture2D texture);
        [DllImport(Constants.dllName)]
        private static extern void SetTextureFilter(Texture2D texture, TextureFilter filter);
        [DllImport(Constants.dllName)]
        private static extern void SetTextureWrap(Texture2D texture, TextureWrap wrap);

        [DllImport(Constants.dllName)]
        private static extern void DrawTextureEx(Texture2D texture, Vector2 position, float rotation, float scale, Color tint);  // Draw a Texture2D
        [DllImport(Constants.dllName)]
        private static extern void DrawTextureRec(Texture2D texture, Rectangle source, Vector2 position, Color tint);            // Draw a part of a texture defined by a rectangle
        [DllImport(Constants.dllName)]
        private static extern void DrawTextureQuad(Texture2D texture, Vector2 tiling, Vector2 offset, Rectangle quad, Color tint);  // Draw texture quad with tiling and offset parameters
        [DllImport(Constants.dllName)]
        private static extern void DrawTextureTiled(Texture2D texture, Rectangle source, Rectangle dest, Vector2 origin, float rotation, float scale, Color tint);      // Draw part of a texture (defined by a rectangle) with rotation and scale tiled into dest.
        [DllImport(Constants.dllName)]
        private static extern void DrawTextureNPatch(Texture2D texture, NPatchInfo nPatchInfo, Rectangle dest, Vector2 origin, float rotation, Color tint);   // Draws a texture (or part of it) that stretches or shrinks nicely
        [DllImport(Constants.dllName)]
        private static extern void DrawTexturePoly(Texture2D texture, Vector2 center, Vector2[] points, Vector2[] texcoords, int pointsCount, Color tint);      // Draw a textured polygon

        /// <summary>
        /// Texture parameters: filter mode
        /// </summary>
        /// <remarks>
        /// NOTE 1: Filtering considers mipmaps if available in the texture
        /// NOTE 2: Filter is accordingly set for minification and magnification
        /// </remarks>
        public enum TextureFilter : int
        {
            /// <summary>
            /// No filter, just pixel aproximation
            /// </summary>
            POINT = 0,
            /// <summary>
            /// Linear filtering
            /// </summary>
            BILINEAR = 1,
            /// <summary>
            /// Trilinear filtering (linear with mipmaps)
            /// </summary>
            TRILINEAR = 2,
            /// <summary>
            /// Anisotropic filtering 4x
            /// </summary>
            ANISOTROPIC_4X = 3,
            /// <summary>
            /// Anisotropic filtering 8x
            /// </summary>
            ANISOTROPIC_8X = 4,
            /// <summary>
            /// Anisotropic filtering 16x
            /// </summary>
            ANISOTROPIC_16X = 5
        }

        /// <summary>
        /// Texture parameters: wrap mode
        /// </summary>
        public enum TextureWrap : int
        {
            /// <summary>
            /// Repeats texture in tiled mode
            /// </summary>
            REPEAT = 0,
            /// <summary>
            /// Clamps texture to edge pixel in tiled mode
            /// </summary>
            CLAMP = 1,
            /// <summary>
            /// Mirrors and repeats the texture in tiled mode
            /// </summary>
            MIRROR_REPEAT = 2,
            /// <summary>
            /// Mirrors and clamps to border the texture in tiled mode
            /// </summary>
            WRAP_MIRROR_CLAMP = 3
        }

        /// <summary>
        /// OpenGL texture id
        /// </summary>
        public uint Id { get; private set; }
        /// <summary>
        /// Texture base width
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Texture base height
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Mipmap levels, 1 by default
        /// </summary>
        public int Mipmaps { get; private set; }
        /// <summary>
        /// Data format (PixelFormat type)
        /// </summary>
        public PixelFormat Format { get; private set; }
        /// <summary>
        /// Set texture scaling filter mode
        /// </summary>
        public TextureFilter Filter
        {
            set => SetTextureFilter(this, value);
        }
        /// <summary>
        /// Set texture wrapping mode
        /// </summary>
        public TextureWrap Wrap
        {
            set => SetTextureWrap(this, value);
        }

        /// <summary>
        /// Load texture from file into GPU memory (VRAM)
        /// </summary>
        /// <param name="filename">File path</param>
        public Texture2D(string filename)
        {
            var texture = LoadTexture(filename);

            Id = texture.Id;
            Width = texture.Width;
            Height = texture.Height;
            Mipmaps = texture.Mipmaps;
            Format = texture.Format;
        }

        /// <summary>
        /// Load texture from image data
        /// </summary>
        /// <param name="image">Image data</param>
        public Texture2D(Image image)
        {
            var texture = LoadTextureFromImage(image);

            Id = texture.Id;
            Width = texture.Width;
            Height = texture.Height;
            Mipmaps = texture.Mipmaps;
            Format = texture.Format;
        }

        /// <summary>
        /// Unload texture from GPU memory (VRAM)
        /// </summary>
        public void Dispose()
        {
            UnloadTexture(this);
            Id = 0;
            Width = 0;
            Height = 0;
            Mipmaps = 0;
            Format = 0;
        }

        /// <summary>
        /// Update GPU texture with new data
        /// </summary>
        /// <param name="pixels">New data</param>
        public void Update(IntPtr pixels) => UpdateTexture(this, pixels);
        /// <summary>
        /// Update GPU texture rectangle with new data
        /// </summary>
        /// <param name="rec">Rectangle inside Texture</param>
        /// <param name="pixels">New Data</param>
        public void UpdateTextureRec(Rectangle rec, IntPtr pixels) => UpdateTextureRec(this, rec, pixels);

        /// <summary>
        /// Generate GPU mipmaps for current texture
        /// </summary>
        public void GenMipmaps() => GenTextureMipmaps(ref this);

        /// <summary>
        /// Draw a Texture2D
        /// </summary>
        /// <param name="tint">Texture color</param>
        /// <param name="position">Texture position</param>
        /// <param name="rotation">Texture rotation</param>
        /// <param name="scale">Texture scale</param>
        public void Draw(Color tint, Vector2 position, float rotation = 0, float scale = 1) => DrawTextureEx(this, position, rotation, scale, tint);
        /// <summary>
        /// Draw a part of a texture defined by a rectangle
        /// </summary>
        /// <param name="tint">Texture color</param>
        /// <param name="source">Texture part</param>
        /// <param name="position">Texture position</param>
        public void DrawRec(Color tint, Rectangle source, Vector2 position) => DrawTextureRec(this, source, position, tint);
        /// <summary>
        /// Draw texture quad with tiling and offset parameters
        /// </summary>
        /// <param name="tint">Texture color</param>
        /// <param name="tiling">Tiling</param>
        /// <param name="offset">Offset</param>
        /// <param name="quad">Quad</param>
        public void DrawQuad(Color tint, Vector2 tiling, Vector2 offset, Rectangle quad) => DrawTextureQuad(this, tiling, offset, quad, tint);
        /// <summary>
        /// Draw part of a texture (defined by a rectangle) with rotation and scale tiled into dest.
        /// </summary>
        /// <param name="tint">Texture color</param>
        /// <param name="source">Source position and dimension</param>
        /// <param name="dest">Destination position and dimension</param>
        /// <param name="origin">Origin position</param>
        /// <param name="rotation">Texture cotation</param>
        /// <param name="scale">Texture scale</param>
        public void DrawTiled(Color tint, Rectangle source, Rectangle dest, Vector2 origin, float rotation = 0, float scale = 1) => DrawTextureTiled(this, source, dest, origin, rotation, scale, tint);
        /// <summary>
        /// Draws a texture (or part of it) that stretches or shrinks nicely
        /// </summary>
        /// <param name="tint">Texture color</param>
        /// <param name="nPatchInfo">source information</param>
        /// <param name="dest">Destination position and dimension</param>
        /// <param name="origin">Origion position</param>
        /// <param name="rotation">Texture rotation</param>
        public void DrawNPatch(Color tint, NPatchInfo nPatchInfo, Rectangle dest, Vector2 origin, float rotation = 0) => DrawTextureNPatch(this, nPatchInfo, dest, origin, rotation, tint);
        /// <summary>
        /// Draw a textured polygon
        /// </summary>
        /// <param name="tint">Texture color</param>
        /// <param name="center">Center position</param>
        /// <param name="points">Array of points</param>
        /// <param name="texcoords">Array of Coordinates</param>
        /// <param name="pointsCount">Number of points</param>
        public void DrawPoly(Color tint, Vector2 center, Vector2[] points, Vector2[] texcoords, int pointsCount) => DrawTexturePoly(this, center, points, texcoords, pointsCount, tint);
    }
}
