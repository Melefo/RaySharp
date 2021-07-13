using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Image : IDisposable
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Image LoadImage(string fileName);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Image LoadImageRaw(string fileName, int width, int height, PixelFormat format, int headerSize);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Image LoadImageAnim(string fileName, ref int frames);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Image LoadImageFromMemory(string fileType, IntPtr fileData, int dataSize);
        [DllImport(Constants.dllName)]
        private static extern void UnloadImage(Image image);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern bool ExportImage(Image image, string fileName);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern bool ExportImageAsCode(Image image, string fileName);

        [DllImport(Constants.dllName)]
        private static extern Image GenImageColor(int width, int height, Color color);
        [DllImport(Constants.dllName)]
        private static extern Image GenImageGradientV(int width, int height, Color top, Color bottom);
        [DllImport(Constants.dllName)]
        private static extern Image GenImageGradientH(int width, int height, Color left, Color right);
        [DllImport(Constants.dllName)]
        private static extern Image GenImageGradientRadial(int width, int height, float density, Color inner, Color outer);
        [DllImport(Constants.dllName)]
        private static extern Image GenImageChecked(int width, int height, int checksX, int checksY, Color col1, Color col2);
        [DllImport(Constants.dllName)]
        private static extern Image GenImageWhiteNoise(int width, int height, float factor);
        [DllImport(Constants.dllName)]
        private static extern Image GenImagePerlinNoise(int width, int height, int offsetX, int offsetY, float scale);
        [DllImport(Constants.dllName)]
        private static extern Image GenImageCellular(int width, int height, int tileSize);

        /// <summary>
        /// Pixel formats
        /// </summary>
        /// <remarks>
        /// NOTE: Support depends on OpenGL version and platform
        /// </remarks>
        public enum PixelFormat : int
        {
            /// <summary>
            /// 8 bit per pixel (no alpha)
            /// </summary>
            UNCOMPRESSED_GRAYSCALE = 1,
            /// <summary>
            /// 8*2 bpp (2 channels)
            /// </summary>
            UNCOMPRESSED_GRAY_ALPHA = 2,
            /// <summary>
            /// 16 bpp
            /// </summary>
            UNCOMPRESSED_R5G6B5 = 3,
            /// <summary>
            /// 24 bpp
            /// </summary>
            UNCOMPRESSED_R8G8B8 = 4,
            /// <summary>
            /// 16 bpp (1 bit alpha)
            /// </summary>
            UNCOMPRESSED_R5G5B5A1 = 5,
            /// <summary>
            /// 16 bpp (4 bit alpha)
            /// </summary>
            UNCOMPRESSED_R4G4B4A4 = 6,
            /// <summary>
            /// 32 bpp
            /// </summary>
            UNCOMPRESSED_R8G8B8A8 = 7,
            /// <summary>
            /// 32 bpp (1 channel - float)
            /// </summary>
            UNCOMPRESSED_R32 = 8,
            /// <summary>
            /// 32*3 bpp (3 channels - float)
            /// </summary>
            UNCOMPRESSED_R32G32B32 = 9,
            /// <summary>
            /// 32*4 bpp (4 channels - float)
            /// </summary>
            UNCOMPRESSED_R32G32B32A32 = 10,
            /// <summary>
            /// 4 bpp (no alpha)
            /// </summary>
            COMPRESSED_DXT1_RGB = 11,
            /// <summary>
            /// 4 bpp (1 bit alpha)
            /// </summary>
            COMPRESSED_DXT1_RGBA = 12,
            /// <summary>
            /// 8 bpp
            /// </summary>
            COMPRESSED_DXT3_RGBA = 13,
            /// <summary>
            /// 8 bpp
            /// </summary>
            COMPRESSED_DXT5_RGBA = 14,
            /// <summary>
            /// 4 bpp
            /// </summary>
            COMPRESSED_ETC1_RGB = 15,
            /// <summary>
            /// 4 bpp
            /// </summary>
            COMPRESSED_ETC2_RGB = 16,
            /// <summary>
            /// 8 bpp
            /// </summary>
            COMPRESSED_ETC2_EAC_RGBA = 17,
            /// <summary>
            /// 4 bpp
            /// </summary>
            COMPRESSED_PVRT_RGB = 18,
            /// <summary>
            /// 4 bpp
            /// </summary>
            COMPRESSED_PVRT_RGBA = 19,
            /// <summary>
            /// 8 bpp
            /// </summary>
            COMPRESSED_ASTC_4x4_RGBA = 20,
            /// <summary>
            /// 2 bpp
            /// </summary>
            COMPRESSED_ASTC_8x8_RGBA = 21
        }

        /// <summary>
        /// Image raw data
        /// </summary>
        public IntPtr Data { get; private set; }
        /// <summary>
        /// Image base width & height
        /// </summary>
        public Vector2 Dimensions { get; private set; }
        /// <summary>
        /// Mipmap levels, 1 by default
        /// </summary>
        public int Mipmaps { get; private set; }
        /// <summary>
        /// Data format (PixelFormat type)
        /// </summary>
        public PixelFormat Format { get; private set; }

        /// <summary>
        /// Load image from file into CPU memory (RAM)
        /// </summary>
        /// <param name="filename">File path</param>
        public Image(string filename)
        {
            var image = LoadImage(filename);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Load image from RAW file data
        /// </summary>
        /// <param name="filename">File path</param>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="format">Image format</param>
        /// <param name="headerSize">File header size</param>
        public Image(string filename, Vector2 dimensions, PixelFormat format, int headerSize)
        {
            var image = LoadImageRaw(filename, (int)dimensions.X, (int)dimensions.Y, format, headerSize);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Load image sequence from file (frames appended to image.data)
        /// </summary>
        /// <param name="fileName">File path</param>
        /// <param name="frames">Number of frames loaded</param>
        public Image(string fileName, ref int frames)
        {
            var image = LoadImageAnim(fileName, ref frames);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Load image from memory buffer
        /// </summary>
        /// <param name="fileType">Image Type</param>
        /// <param name="fileData">Image Data</param>
        /// <param name="dataSize">Image Size</param>
        public Image(string fileType, IntPtr fileData, int dataSize)
        {
            var image = LoadImageFromMemory(fileType, fileData, dataSize);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Generate image: cellular algorithm. Bigger tileSize means bigger cells
        /// </summary>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="tileSize">Size of tile</param>
        public Image(Vector2 dimensions, int tileSize)
        {
            var image = GenImageCellular((int)dimensions.X, (int)dimensions.Y, tileSize);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Generate image: perlin noise
        /// </summary>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="offset">Offset of perlin noise</param>
        /// <param name="scale">Amount of noise</param>
        public Image(Vector2 dimensions, Vector2 offset, float scale)
        {
            var image = GenImagePerlinNoise((int)dimensions.X, (int)dimensions.Y, (int)offset.X, (int)offset.Y, scale);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Generate image: white noise
        /// </summary>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="factor">Amount of noise</param>
        public Image(Vector2 dimensions, float factor)
        {
            var image = GenImageWhiteNoise((int)dimensions.X, (int)dimensions.Y, factor);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Generate image: checked
        /// </summary>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="checksPosition">Check position</param>
        /// <param name="col1">First color</param>
        /// <param name="col2">Second color</param>
        public Image(Vector2 dimensions, Vector2 checksPosition, Color col1, Color col2)
        {
            var image = GenImageChecked((int)dimensions.X, (int)dimensions.Y, (int)checksPosition.X, (int)checksPosition.Y, col1, col2);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Generate image: radial gradient
        /// </summary>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="density">Density of gradient</param>
        /// <param name="inner">Inner color</param>
        /// <param name="outer">Outer color</param>
        public Image(Vector2 dimensions, float density, Color inner, Color outer)
        {
            var image = GenImageGradientRadial((int)dimensions.X, (int)dimensions.Y, density, inner, outer);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Generate image: gradient
        /// </summary>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="color1">First color of gradient</param>
        /// <param name="color2">Second color of gradient</param>
        /// <param name="vertical">Is gradient vertical (otherwhise horizontal)</param>
        public Image(Vector2 dimensions, Color color1, Color color2, bool vertical = false)
        {
            Image image;

            if (vertical)
                image = GenImageGradientV((int)dimensions.X, (int)dimensions.Y, color1, color2);
            else
                image = GenImageGradientH((int)dimensions.X, (int)dimensions.Y, color1, color2);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Generate image: plain color
        /// </summary>
        /// <param name="dimensions">Image dimensions</param>
        /// <param name="color">Image color</param>
        public Image(Vector2 dimensions, Color color)
        {
            var image = GenImageColor((int)dimensions.X, (int)dimensions.Y, color);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Unload image from CPU memory (RAM)
        /// </summary>
        public void Dispose()
        {
            UnloadImage(this);
            Data = IntPtr.Zero;
            Dimensions = Vector2.Zero;
            Mipmaps = 0;
            Format = 0;
        }

        /// <summary>
        /// Export image data to file, returns true on success
        /// </summary>
        /// <param name="filename">File path</param>
        /// <param name="asCode">Export image as code file defining an array of bytes</param>
        /// <returns>true on success</returns>
        public bool Export(string filename, bool asCode = false)
        {
            if (asCode)
                return ExportImageAsCode(this, filename);
            return ExportImage(this, filename);
        }
    }
}
