using RaySharp.Shapes;
using System;
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

        private TextureFilter _filter;
        private TextureWrap _wrap;

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
        /// Get/Set texture scaling filter mode
        /// </summary>
        public TextureFilter Filter 
        {
            get => _filter;
            set
            {
                SetTextureFilter(this, value);
                _filter = value;
            }
        }
        /// <summary>
        /// Get/Set texture wrapping mode
        /// </summary>
        public TextureWrap Wrap
        {
            get => _wrap;
            set
            {
                SetTextureWrap(this, value);
                _wrap = value;
            }
        }

        /// <summary>
        /// Load texture from file into GPU memory (VRAM)
        /// </summary>
        /// <param name="filename">File path</param>
        public Texture2D(string filename)
        {
            var texture = LoadTexture(filename);

            _filter = TextureFilter.POINT;
            _wrap = TextureWrap.REPEAT;
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

            _filter = TextureFilter.POINT;
            _wrap = TextureWrap.REPEAT;
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
    }
}
