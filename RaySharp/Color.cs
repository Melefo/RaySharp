using System.Runtime.InteropServices;

namespace RaySharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        public static Color LightGray = new Color(200, 200, 200, 255);
        public static Color Gray = new Color(130, 130, 130, 255);
        public static Color DarkGray = new Color(80, 80, 80, 255);
        public static Color Yellow = new Color(253, 249, 0, 255);
        public static Color Gold = new Color(255, 203, 0, 255);
        public static Color Orange = new Color(255, 161, 0, 255);
        public static Color Pink = new Color(255, 109, 194, 255);
        public static Color Red = new Color(230, 41, 55, 255);
        public static Color Maroon = new Color(190, 33, 55, 255);
        public static Color Green = new Color(0, 228, 48, 255);
        public static Color Lime = new Color(0, 158, 47, 255);
        public static Color DarkGreen = new Color(0, 117, 44, 255);
        public static Color SkyBlue = new Color(102, 191, 255, 255);
        public static Color Blue = new Color(0, 121, 241, 255);
        public static Color DarkBlue = new Color(0, 82, 172, 255);
        public static Color Purple = new Color(200, 122, 255, 255);
        public static Color Violet = new Color(135, 60, 190, 255);
        public static Color DarkPurple = new Color(112, 31, 126, 255);
        public static Color Beige = new Color(211, 176, 131, 255);
        public static Color Brown = new Color(127, 106, 79, 255);
        public static Color DarkBrown = new Color(76, 63, 47, 255);
        public static Color White = new Color(255, 255, 255, 255);
        public static Color Black = new Color(0, 0, 0, 255);
        public static Color Blank = new Color(0, 0, 0, 0);
        public static Color Magenta = new Color(255, 0, 255, 255);
        public static Color RayWhite = new Color(245, 245, 245, 255);

        /// <summary>
        /// Red value
        /// </summary>
        public byte R { get; }
        /// <summary>
        /// Green value
        /// </summary>
        public byte G { get; }
        /// <summary>
        /// Blue value
        /// </summary>
        public byte B { get; }
        /// <summary>
        /// Alpha value
        /// </summary>
        public byte A { get; }

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
    }
}
