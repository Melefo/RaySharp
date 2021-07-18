using System;
using System.Numerics;
using System.Runtime.InteropServices;
using static RaySharp.Textures.Image;

namespace RaySharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        [DllImport(Constants.dllName)]
        private static extern Color Fade(Color color, float alpha);
        [DllImport(Constants.dllName)]
        private static extern int ColorToInt(Color color);
        [DllImport(Constants.dllName)]
        private static extern Vector4 ColorNormalize(Color color);
        [DllImport(Constants.dllName)]
        private static extern Color ColorFromNormalized(Vector4 normalized);
        [DllImport(Constants.dllName)]
        private static extern Vector3 ColorToHSV(Color color);
        [DllImport(Constants.dllName)]
        private static extern Color ColorFromHSV(float hue, float saturation, float value);
        [DllImport(Constants.dllName)]
        private static extern Color ColorAlphaBlend(Color dst, Color src, Color tint);
        [DllImport(Constants.dllName)]
        private static extern Color GetColor(int hexValue);
        [DllImport(Constants.dllName)]
        private static extern Color GetPixelColor(IntPtr srcPtr, PixelFormat format);


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
        /// Construct a new Color
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
        /// Construst a new Color from hexadecimal value
        /// </summary>
        /// <param name="hex">Hexadecimal value</param>
        public Color(int hex)
        {
            var color = GetColor(hex);

            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        /// <summary>
        /// Construst a new Color from normalized values [0..1]
        /// </summary>
        /// <param name="normalized">Normalized value</param>
        public Color(Vector4 normalized)
        {
            var color = ColorFromNormalized(normalized);

            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        /// <summary>
        /// Construct a new Color from HSV values, hue [0..360], saturation/value [0..1]
        /// </summary>
        /// <param name="hsv">HSV Value</param>
        public Color(Vector3 hsv)
        {
            var color = ColorFromHSV(hsv.X, hsv.Y, hsv.Z);

            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        /// <summary>
        /// Get Color from a source pixel pointer of certain format
        /// </summary>
        /// <param name="srcPtr">Source pixel pointer</param>
        /// <param name="format">Pixel format</param>
        public Color(IntPtr srcPtr, PixelFormat format)
        {
            var color = GetPixelColor(srcPtr, format);

            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        /// <summary>
        /// Linearly interpolate Color to target by t
        /// </summary>
        /// <param name="target">Color to interpolate</param>
        /// <param name="t">multiplier</param>
        /// <returns>Color with Lerp applied</returns>
        public Color Lerp(Color target, float t)
        {
            t = Math.Clamp(t, 0, 1);
            float bk = (1 - t);

            return new Color((byte)(A * bk + target.A * t), (byte)(R * bk + target.R * t), (byte)(G * bk + target.G * t), (byte)(B * bk + target.B * t));
        }

        /// <summary>
        /// Returns color with alpha applied
        /// </summary>
        /// <param name="alpha">Alpha value, goes from 0.0f to 1.0f</param>
        /// <returns>Color with alpha applied</returns>
        public Color Fade(float alpha) => Fade(this, alpha);

        /// <summary>
        /// Returns src alpha-blended into current color with tint
        /// </summary>
        /// <param name="src">Source color</param>
        /// <param name="tint">Color tint</param>
        /// <returns>Color alpha-blended</returns>
        public Color Blend(Color src, Color tint) => ColorAlphaBlend(this, src, tint);

        /// <summary>
        /// Returns Color normalized as float [0..1]
        /// </summary>
        /// <param name="color">A RaySharp color</param>
        public static implicit operator Vector4(Color color) => ColorNormalize(color);
        /// <summary>
        /// Returns Color from normalized values [0..1]
        /// </summary>
        /// <param name="normalized">Normalized value</param>
        public static implicit operator Color(Vector4 normalized) => ColorFromNormalized(normalized);
        /// <summary>
        /// Returns HSV values for a Color, hue [0..360], saturation/value [0..1]
        /// </summary>
        /// <param name="color">A RaySharp Color</param>
        public static implicit operator Vector3(Color color) => ColorToHSV(color);
        /// <summary>
        /// Returns a Color from HSV values, hue [0..360], saturation/value [0..1]
        /// </summary>
        /// <param name="hsv">HSV Value</param>
        public static implicit operator Color(Vector3 hsv) => ColorFromHSV(hsv.X, hsv.Y, hsv.Z);

        /// <summary>
        /// Returns hexadecimal value for a Color
        /// </summary>
        /// <param name="color">A RaySharp color</param>
        public static implicit operator int(Color color) => ColorToInt(color);
        /// <summary>
        /// Get Color structure from hexadecimal value
        /// </summary>
        /// <param name="hex">Hexadecimal value</param>
        public static implicit operator Color(int hex) => GetColor(hex);

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
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern void SetPixelColor(IntPtr dstPtr, Color color, int format);


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

        /// <summary>
        /// Set color formatted into destination pixel pointer
        /// </summary>
        /// <param name="pixel">Pixel pointer</param>
        /// <param name="color">New Color</param>
        /// <param name="format">Color format</param>
        public static void SetPixelColor(this IntPtr pixel, Color color, PixelFormat format) => SetPixelColor(pixel, color, format);

    }
}
