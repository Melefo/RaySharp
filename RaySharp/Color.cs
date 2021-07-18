using System;
using System.Runtime.InteropServices;

namespace RaySharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        public static readonly Color LightGray = new Color(200, 200, 200, 255);
        public static readonly Color Gray = new Color(130, 130, 130, 255);
        public static readonly Color DarkGray = new Color(80, 80, 80, 255);
        public static readonly Color Yellow = new Color(253, 249, 0, 255);
        public static readonly Color Gold = new Color(255, 203, 0, 255);
        public static readonly Color Orange = new Color(255, 161, 0, 255);
        public static readonly Color Pink = new Color(255, 109, 194, 255);
        public static readonly Color Red = new Color(230, 41, 55, 255);
        public static readonly Color Maroon = new Color(190, 33, 55, 255);
        public static readonly Color Green = new Color(0, 228, 48, 255);
        public static readonly Color Lime = new Color(0, 158, 47, 255);
        public static readonly Color DarkGreen = new Color(0, 117, 44, 255);
        public static readonly Color SkyBlue = new Color(102, 191, 255, 255);
        public static readonly Color Blue = new Color(0, 121, 241, 255);
        public static readonly Color DarkBlue = new Color(0, 82, 172, 255);
        public static readonly Color Purple = new Color(200, 122, 255, 255);
        public static readonly Color Violet = new Color(135, 60, 190, 255);
        public static readonly Color DarkPurple = new Color(112, 31, 126, 255);
        public static readonly Color Beige = new Color(211, 176, 131, 255);
        public static readonly Color Brown = new Color(127, 106, 79, 255);
        public static readonly Color DarkBrown = new Color(76, 63, 47, 255);
        public static readonly Color White = new Color(255, 255, 255, 255);
        public static readonly Color Black = new Color(0, 0, 0, 255);
        public static readonly Color Blank = new Color(0, 0, 0, 0);
        public static readonly Color Magenta = new Color(255, 0, 255, 255);
        public static readonly Color RayWhite = new Color(245, 245, 245, 255);

        /// <summary>
        /// Red value
        /// </summary>
        public byte R { get; internal set; }
        /// <summary>
        /// Green value
        /// </summary>
        public byte G { get; internal set; }
        /// <summary>
        /// Blue value
        /// </summary>
        public byte B { get; internal set; }
        /// <summary>
        /// Alpha value
        /// </summary>
        public byte A { get; internal set; }

        /// <summary>
        /// Construct a new color
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        public Color(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Linearly interpolate Color to target by t
        /// </summary>
        /// <param name="target">Color to interpolate</param>
        /// <param name="t">multiplier</param>
        public void Lerp(Color target, float t)
        {
            t = Math.Clamp(t, 0, 1);
            float bk = (1 - t);

            A = (byte)(A * bk + target.A * t);
            R = (byte)(R * bk + target.R * t);
            G = (byte)(G * bk + target.G * t);
            B = (byte)(B * bk + target.B * t);
        }

        /// <summary>
        /// Convert a RaySharp Color to a System.Drawing Color
        /// </summary>
        /// <param name="color">A RaySharp color</param>
        public static implicit operator System.Drawing.Color(Color color) => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

        /// <summary>
        /// Convert a System.Drawing Color to a RaySharp Color
        /// </summary>
        /// <param name="color">a System.Drawing color</param>
        public static implicit operator Color(System.Drawing.Color color) => new Color(color.R, color.G, color.B, color.A);
    }

    public static class ColorExtension
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void UnloadImageColors(IntPtr colors);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void UnloadImagePalette(IntPtr colors);

        /// <summary>
        /// Unload color data loaded with LoadColors()
        /// </summary>
        /// <param name="colors">Array of colors</param>
        public static void UnloadColors(this IntPtr colors) => UnloadImageColors(colors);
        /// <summary>
        /// Unload colors palette loaded with LoadPalette()
        /// </summary>
        /// <param name="colors">Array of colors</param>
        public static void UnloadPalette(this IntPtr colors) => UnloadImagePalette(colors);

    }
}
