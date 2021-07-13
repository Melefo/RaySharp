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
    }
}
