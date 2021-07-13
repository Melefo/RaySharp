using RaySharp.Textures;
using System;
using System.Runtime.InteropServices;

namespace RaySharp.Text
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Font : IDisposable
    {
        [DllImport(Constants.dllName)]
        private static extern Font GetFontDefault();
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Font LoadFont(string fileName);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Font LoadFontEx(string fileName, int fontSize, IntPtr fontChars, int charsCount);
        [DllImport(Constants.dllName)]
        private static extern Font LoadFontFromImage(Image image, Color key, int firstChar);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern Font LoadFontFromMemory(string fileType, IntPtr fileData, int dataSize, int fontSize, IntPtr fontChars, int charsCount);

        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void UnloadFont(Font font);

        /// <summary>
        /// Get the default Font
        /// </summary>
        public static Font Default => GetFontDefault();

        /// <summary>
        /// Base size (default chars height)
        /// </summary>
        public int BaseSize { get; private set; }
        /// <summary>
        /// Number of characters
        /// </summary>
        public int CharsCount { get; private set; }
        /// <summary>
        /// Padding around the chars
        /// </summary>
        public int CharsPadding { get; private set; }
        /// <summary>
        /// Characters texture atals
        /// </summary>
        public Texture2D Texture { get; private set; }
        /// <summary>
        /// Characters rectangles in texture
        /// </summary>
        public IntPtr Recs { get; private set; }
        /// <summary>
        /// Characters info data
        /// </summary>
        public IntPtr Chars { get; private set; }

        /// <summary>
        /// Load font from file into GPU memory (VRAM)
        /// </summary>
        /// <param name="filename">File path</param>
        public Font(string filename)
        {
            var font = LoadFont(filename);

            BaseSize = font.BaseSize;
            CharsCount = font.CharsCount;
            CharsPadding = font.CharsPadding;
            Texture = font.Texture;
            Recs = font.Recs;
            Chars = font.Chars;
        }

        /// <summary>
        /// Load font from file with extended parameters
        /// </summary>
        /// <param name="filename">File path</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="fontChars">Font chars</param>
        /// <param name="charsCount">Chars count</param>
        public Font(string filename, int fontSize, IntPtr fontChars, int charsCount)
        {
            var font = LoadFontEx(filename, fontSize, fontChars, charsCount);

            BaseSize = font.BaseSize;
            CharsCount = font.CharsCount;
            CharsPadding = font.CharsPadding;
            Texture = font.Texture;
            Recs = font.Recs;
            Chars = font.Chars;
        }

        /// <summary>
        /// Load font from Image (XNA style)
        /// </summary>
        /// <param name="image">Image containing font</param>
        /// <param name="key">Color of font in image</param>
        /// <param name="firstChar">First char</param>
        public Font(Image image, Color key, int firstChar)
        {
            var font = LoadFontFromImage(image, key, firstChar);

            BaseSize = font.BaseSize;
            CharsCount = font.CharsCount;
            CharsPadding = font.CharsPadding;
            Texture = font.Texture;
            Recs = font.Recs;
            Chars = font.Chars;
        }

        /// <summary>
        /// Load font from memory buffer
        /// </summary>
        /// <param name="fileType">File type</param>
        /// <param name="fileData">File data</param>
        /// <param name="dataSize">Data size</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="fontChars">Font chars</param>
        /// <param name="charsCount">Chars count</param>
        public Font(string fileType, IntPtr fileData, int dataSize, int fontSize, IntPtr fontChars, int charsCount)
        {
            var font = LoadFontFromMemory(fileType, fileData, dataSize, fontSize, fontChars, charsCount);

            BaseSize = font.BaseSize;
            CharsCount = font.CharsCount;
            CharsPadding = font.CharsPadding;
            Texture = font.Texture;
            Recs = font.Recs;
            Chars = font.Chars;
        }

        /// <summary>
        /// Unload Font from GPU memory (VRAM)
        /// </summary>
        public void Dispose()
        {
            UnloadFont(this);
            BaseSize = 0;
            CharsCount = 0;
            CharsPadding = 0;
            Texture = new Texture2D();
            Recs = IntPtr.Zero;
            Chars = IntPtr.Zero;
        }
    }
}
