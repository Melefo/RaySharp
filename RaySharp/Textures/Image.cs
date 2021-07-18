using RaySharp.Shapes;
using RaySharp.Text;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Textures
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

        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Image ImageCopy(Image image);
        [DllImport(Constants.dllName)]
        private static extern Image ImageFromImage(Image image, Rectangle rec);
        [DllImport(Constants.dllName)]
        private static extern Image ImageText(string text, int fontSize, Color color);
        [DllImport(Constants.dllName)]
        private static extern Image ImageTextEx(Font font, string text, float fontSize, float spacing, Color tint);

        [DllImport(Constants.dllName)]
        private static extern void ImageFormat(ref Image image, PixelFormat newFormat);
        [DllImport(Constants.dllName)]
        private static extern void ImageToPOT(ref Image image, Color fill);
        [DllImport(Constants.dllName)]
        private static extern void ImageCrop(ref Image image, Rectangle crop);
        [DllImport(Constants.dllName)]
        private static extern void ImageAlphaCrop(ref Image image, float threshold);
        [DllImport(Constants.dllName)]
        private static extern void ImageAlphaClear(ref Image image, Color color, float threshold);
        [DllImport(Constants.dllName)]
        private static extern void ImageAlphaMask(ref Image image, Image alphaMask);
        [DllImport(Constants.dllName)]
        private static extern void ImageAlphaPremultiply(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageResize(ref Image image, int newWidth, int newHeight);
        [DllImport(Constants.dllName)]
        private static extern void ImageResizeNN(ref Image image, int newWidth, int newHeight);
        [DllImport(Constants.dllName)]
        private static extern void ImageResizeCanvas(ref Image image, int newWidth, int newHeight, int offsetX, int offsetY, Color fill);
        [DllImport(Constants.dllName)]
        private static extern void ImageMipmaps(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageDither(ref Image image, int rBpp, int gBpp, int bBpp, int aBpp);
        [DllImport(Constants.dllName)]
        private static extern void ImageFlipVertical(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageFlipHorizontal(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageRotateCW(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageRotateCCW(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageColorTint(ref Image image, Color color);
        [DllImport(Constants.dllName)]
        private static extern void ImageColorInvert(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageColorGrayscale(ref Image image);
        [DllImport(Constants.dllName)]
        private static extern void ImageColorContrast(ref Image image, float contrast);
        [DllImport(Constants.dllName)]
        private static extern void ImageColorBrightness(ref Image image, int brightness);
        [DllImport(Constants.dllName)]
        private static extern void ImageColorReplace(ref Image image, Color color, Color replace);

        [DllImport(Constants.dllName)]
        private static extern void ImageClearBackground(ref Image dst, Color color);
        [DllImport(Constants.dllName)]
        private static extern void ImageDrawPixelV(ref Image dst, Vector2 position, Color color);
        [DllImport(Constants.dllName)]
        private static extern void ImageDrawLineV(ref Image dst, Vector2 start, Vector2 end, Color color);
        [DllImport(Constants.dllName)]
        private static extern void ImageDrawCircleV(ref Image dst, Vector2 center, int radius, Color color);
        [DllImport(Constants.dllName)]
        private static extern void ImageDrawRectangleRec(ref Image dst, Rectangle rec, Color color);
        [DllImport(Constants.dllName)]
        private static extern void ImageDrawRectangleLines(ref Image dst, Rectangle rec, int thick, Color color);
        [DllImport(Constants.dllName)]
        private static extern void ImageDraw(ref Image dst, Image src, Rectangle srcRec, Rectangle dstRec, Color tint);
        [DllImport(Constants.dllName)]
        private static extern void ImageDrawTextEx(ref Image dst, Font font, string text, Vector2 position, float fontSize, float spacing, Color tint);

        [DllImport(Constants.dllName)]
        private static extern IntPtr LoadImageColors(Image image);
        [DllImport(Constants.dllName)]
        private static extern IntPtr LoadImagePalette(Image image, int maxPaletteSize, ref int colorsCount);

        [DllImport(Constants.dllName)]
        private static extern Rectangle GetImageAlphaBorder(Image image, float threshold);

        [DllImport(Constants.dllName)]
        private static extern Texture2D LoadTextureCubemap(Image image, int layout);



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
        /// Create an image duplicate (useful for transformations)
        /// </summary>
        /// <param name="copy">Image to copy</param>
        public Image(Image copy)
        {
            var image = ImageCopy(copy);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Create an image from another image piece
        /// </summary>
        /// <param name="copy">Image to copy</param>
        /// <param name="piece">Piece to copy</param>
        public Image(Image copy, Rectangle piece)
        {
            var image = ImageFromImage(copy, piece);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Create an image from text (default font)
        /// </summary>
        /// <param name="text">Text to transform</param>
        /// <param name="fontSize">Size of text</param>
        /// <param name="color">Color of text</param>
        public Image(string text, int fontSize, Color color)
        {
            var image = ImageText(text, fontSize, color);

            Data = image.Data;
            Dimensions = image.Dimensions;
            Mipmaps = image.Mipmaps;
            Format = image.Format;
        }

        /// <summary>
        /// Create an image from text (custom sprite font)
        /// </summary>
        /// <param name="font">Font of text</param>
        /// <param name="text">Text to transform</param>
        /// <param name="fontSize">Size of text</param>
        /// <param name="spacing">Spacing of text</param>
        /// <param name="tint">Color of text</param>
        public Image(Font font, string text, float fontSize, float spacing, Color tint)
        {
            var image = ImageTextEx(font, text, fontSize, spacing, tint);

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

        /// <summary>
        /// Convert image data to desired format
        /// </summary>
        /// <param name="newFormat">Desired Format</param>
        public void ConvertFormat(PixelFormat newFormat) => ImageFormat(ref this, newFormat);

        /// <summary>
        /// Convert image to POT (power-of-two)
        /// </summary>
        /// <param name="fill">Color to fill with</param>
        public void ToPOT(Color fill) => ImageToPOT(ref this, fill);
        /// <summary>
        /// Crop an image to a defined rectangle
        /// </summary>
        /// <param name="crop">Position and size of crop</param>
        public void Crop(Rectangle crop) => ImageCrop(ref this, crop);
        /// <summary>
        /// Crop image depending on alpha value
        /// </summary>
        /// <param name="threshold">Alpha value</param>
        public void AlphaCrop(float threshold) => ImageAlphaCrop(ref this, threshold);
        /// <summary>
        /// Clear alpha channel to desired color
        /// </summary>
        /// <param name="color">Desired color</param>
        /// <param name="threshold">Alpha value</param>
        public void AlphaClear(Color color, float threshold) => ImageAlphaClear(ref this, color, threshold);
        /// <summary>
        /// Apply alpha mask to image
        /// </summary>
        /// <param name="alphaMask">Alpha mask</param>
        public void AlphaMask(Image alphaMask) => ImageAlphaMask(ref this, alphaMask);
        /// <summary>
        /// Premultiply alpha channel
        /// </summary>
        /// <param name="image"></param>
        public void AlphaPremultiply() => ImageAlphaPremultiply(ref this);
        /// <summary>
        /// Resize image (Bicubic scaling algorithm)
        /// </summary>
        /// <param name="dimensions">New dimensions</param>
        public void Resize(Vector2 dimensions) => ImageResize(ref this, (int)dimensions.X, (int)dimensions.Y);
        /// <summary>
        ///  Resize image (Nearest-Neighbor scaling algorithm)
        /// </summary>
        /// <param name="dimensions">New dimensions</param>
        public void ResizeNN(Vector2 dimensions) => ImageResizeNN(ref this, (int)dimensions.X, (int)dimensions.Y);
        /// <summary>
        /// Resize canvas and fill with color
        /// </summary>
        /// <param name="dimensions">New dimensions</param>
        /// <param name="offset">Offset possition</param>
        /// <param name="fill">Color to fill with</param>
        public void ResizeCanvas(Vector2 dimensions, Vector2 offset, Color fill) => ImageResizeCanvas(ref this, (int)dimensions.X, (int)dimensions.Y, (int)offset.X, (int)offset.Y, fill);
        /// <summary>
        /// Generate all mipmap levels for a provided image
        /// </summary>
        public void GenerateMipmaps() => ImageMipmaps(ref this);

        /// <summary>
        /// Dither image data to 16bpp or lower (Floyd-Steinberg dithering)
        /// </summary>
        /// <param name="rBpp">Red Bpp</param>
        /// <param name="gBpp">Green Bpp</param>
        /// <param name="bBpp">Blue Bpp</param>
        /// <param name="aBpp">alpha Bpp</param>
        public void Dither(int rBpp, int gBpp, int bBpp, int aBpp) => ImageDither(ref this, rBpp, gBpp, bBpp, aBpp);
        /// <summary>
        /// Flip image vertically
        /// </summary>
        public void FlipVertical() => ImageFlipVertical(ref this);
        /// <summary>
        /// Flip image horizontally
        /// </summary>
        public void FlipHorizontal() => ImageFlipHorizontal(ref this);
        /// <summary>
        /// Rotate image clockwise 90deg
        /// </summary>
        public void RotateCW() => ImageRotateCW(ref this);
        /// <summary>
        /// Rotate image counter-clockwise 90deg
        /// </summary>
        public void RotateCCW() => ImageRotateCCW(ref this);
        /// <summary>
        /// Modify image color: tint
        /// </summary>
        /// <param name="color">Tint color</param>
        public void ColorTint(Color color) => ImageColorTint(ref this, color);
        /// <summary>
        /// Modify image color: invert
        /// </summary>
        public void ColorInvert() => ImageColorInvert(ref this);
        /// <summary>
        /// Modify image color: grayscale
        /// </summary>
        public void ColorGrayscale() => ImageColorGrayscale(ref this);
        /// <summary>
        /// Modify image color: contrast (-100 to 100)
        /// </summary>
        /// <param name="contrast">Contrast value</param>
        public void ColorContrast(float contrast) => ImageColorContrast(ref this, contrast);
        /// <summary>
        /// Modify image color: brightness (-255 to 255)
        /// </summary>
        /// <param name="brightness">Brightness value</param>
        public void ColorBrightness(int brightness) => ImageColorBrightness(ref this, brightness);
        /// <summary>
        /// Modify image color: replace color
        /// </summary>
        /// <param name="color">Color to replace</param>
        /// <param name="replace">Overriding color</param>
        public void ColorReplace(Color color, Color replace) => ImageColorReplace(ref this, color, replace);

        /// <summary>
        /// Clear image background with given color
        /// </summary>
        /// <param name="color">New background color</param>
        public void ClearBackground(Color color) => ImageClearBackground(ref this, color);
        /// <summary>
        /// Draw pixel within an image
        /// </summary>
        /// <param name="pixel">Pixel to draw</param>
        public void DrawPixel(Pixel pixel) => ImageDrawPixelV(ref this, pixel.Position, pixel.Color);
        /// <summary>
        /// Draw line within an image
        /// </summary>
        /// <param name="line">Line to draw</param>
        /// <param name="color">Color of line</param>
        public void DrawLine(Line line, Color color) => ImageDrawLineV(ref this, line.Start, line.End, color);
        /// <summary>
        /// Draw circle within an image
        /// </summary>
        /// <param name="circle">Circle to draw</param>
        /// <param name="color">Color of Circle</param>
        public void DrawCircle(Circle circle, Color color) => ImageDrawCircleV(ref this, circle.Center, (int)circle.Radius, color);
        /// <summary>
        /// Draw rectangle within an image
        /// </summary>
        /// <param name="rec">Rectangle to draw</param>
        /// <param name="color">Color of Rectangle</param>
        public void DrawRectangle(Rectangle rec, Color color) => ImageDrawRectangleRec(ref this, rec, color);
        /// <summary>
        /// Draw rectangle lines within an image
        /// </summary>
        /// <param name="rec">Rectangle to draw</param>
        /// <param name="thick">Thickness of lines</param>
        /// <param name="color">Color of Rectangle lines</param>
        public void DrawRectangleLines(Rectangle rec, int thick, Color color) => ImageDrawRectangleLines(ref this, rec, thick, color);
        /// <summary>
        /// Draw a source image within a destination image (tint applied to source)
        /// </summary>
        /// <param name="src">Image to draw</param>
        /// <param name="srcRec">Selection of image to draw</param>
        /// <param name="dstRec">Selection where to draw</param>
        /// <param name="tint">Tint to apply to src</param>
        public void DrawImage(Image src, Rectangle srcRec, Rectangle dstRec, Color tint) => ImageDraw(ref this, src, srcRec, dstRec, tint);
        /// <summary>
        /// Draw text (custom sprite font) within an image (destination)
        /// </summary>
        /// <param name="font">Font</param>
        /// <param name="text">Text to draw</param>
        /// <param name="position">Where to draw Text</param>
        /// <param name="fontSize">Text size</param>
        /// <param name="spacing">Spacing size</param>
        /// <param name="tint">Color of text</param>
        public void DrawText(Font font, string text, Vector2 position, float fontSize, float spacing, Color tint) => ImageDrawTextEx(ref this, font, text, position, fontSize, spacing, tint);

        /// <summary>
        /// Load color data from image as a Color array (RGBA - 32bit)
        /// </summary>
        /// <returns>Array of colors</returns>
        public IntPtr LoadColors() => LoadImageColors(this);

        /// <summary>
        /// Load colors palette from image as a Color array (RGBA - 32bit)
        /// </summary>
        /// <param name="maxPaletteSize">Maximum number of colors</param>
        /// <param name="colorsCount">Number of colors found</param>
        /// <returns>Array of colors</returns>
        public IntPtr LoadPalette(int maxPaletteSize, ref int colorsCount) => LoadImagePalette(this, maxPaletteSize, ref colorsCount);

        /// <summary>
        /// Get image alpha border rectangle
        /// </summary>
        /// <param name="threshold">Alpha thresold</param>
        /// <returns>Alpha border rectangle</returns>
        public Rectangle GetAlphaBorder(float threshold) => GetImageAlphaBorder(this, threshold);

        /// <summary>
        /// Load cubemap from image, multiple image cubemap layouts supported
        /// </summary>
        /// <param name="layout">Layout slelected</param>
        /// <returns>Texture of Cubemap</returns>
        public Texture2D LoadCubemap(int layout) => LoadTextureCubemap(this, layout);

    }
}
