using System.Runtime.InteropServices;

namespace RaySharp.Input
{
    public static class Keyboard
    {
        [DllImport(Constants.dllName)]
        private static extern KeyboardKey GetKeyPressed();                                                // Get key pressed (keycode), call it multiple times for keys queued
        [DllImport(Constants.dllName)]
        private static extern char GetCharPressed();                                               // Get char pressed (unicode), call it multiple times for chars queued

        /// <summary>
        /// Keyboard keys (US keyboard layout)
        /// </summary>
        /// <remarks>
        /// NOTE: Use GetKeyPressed() to allow redefining required keys for alternative layouts
        /// </remarks>
        public enum KeyboardKey : int
        {
            /// <summary>
            /// Key: NULL, used for no key pressed
            /// </summary>
            NULL = 0,
            /// <summary>
            /// Key: '
            /// </summary>
            APOSTROPHE = 39,
            /// <summary>
            /// Key: ,
            /// </summary>
            COMMA = 44,
            /// <summary>
            /// Key: -
            /// </summary>
            MINUS = 45,
            /// <summary>
            /// Key: .
            /// </summary>
            PERIOD = 46,
            /// <summary>
            /// Key: /
            /// </summary>
            SLASH = 47,
            /// <summary>
            /// Key: 0
            /// </summary>
            ZERO = 48,
            /// <summary>
            /// Key: 1
            /// </summary>
            ONE = 49,
            /// <summary>
            /// Key: 2
            /// </summary>
            TWO = 50,
            /// <summary>
            /// Key: 3
            /// </summary>
            THREE = 51,
            /// <summary>
            /// Key: 4
            /// </summary>
            FOUR = 52,
            /// <summary>
            /// Key: 5
            /// </summary>
            FIVE = 53,
            /// <summary>
            /// Key: 6
            /// </summary>
            SIX = 54,
            /// <summary>
            /// Key: 7
            /// </summary>
            SEVEN = 55,
            /// <summary>
            /// Key: 8
            /// </summary>
            EIGHT = 56,
            /// <summary>
            /// Key: 9
            /// </summary>
            NINE = 57,
            /// <summary>
            /// Key: ;
            /// </summary>
            SEMICOLON = 59,
            /// <summary>
            /// Key: =
            /// </summary>
            EQUAL = 61,
            /// <summary>
            /// Key: A | a
            /// </summary>
            A = 65,
            /// <summary>
            /// Key: B | b
            /// </summary>
            B = 66,
            /// <summary>
            /// Key: C | c
            /// </summary>
            C = 67,
            /// <summary>
            /// Key: D | d
            /// </summary>
            D = 68,
            /// <summary>
            /// Key: E | e
            /// </summary>
            E = 69,
            /// <summary>
            /// Key: F | f
            /// </summary>
            F = 70,
            /// <summary>
            /// Key: G | g
            /// </summary>
            G = 71,
            /// <summary>
            /// Key: H | h
            /// </summary>
            H = 72,
            /// <summary>
            /// Key: I | i
            /// </summary>
            I = 73,
            /// <summary>
            /// Key: J | j
            /// </summary>
            J = 74,
            /// <summary>
            /// Key: K | k
            /// </summary>
            K = 75,
            /// <summary>
            /// Key: L | l
            /// </summary>
            L = 76,
            /// <summary>
            /// Key: M | m
            /// </summary>
            M = 77,
            /// <summary>
            /// Key: N | n
            /// </summary>
            N = 78,
            /// <summary>
            /// Key: O | o
            /// </summary>
            O = 79,
            /// <summary>
            /// Key: P | p
            /// </summary>
            P = 80,
            /// <summary>
            /// Key: Q | q
            /// </summary>
            Q = 81,
            /// <summary>
            /// Key: R | r
            /// </summary>
            R = 82,
            /// <summary>
            /// Key: S | s
            /// </summary>
            S = 83,
            /// <summary>
            /// Key: T | t
            /// </summary>
            T = 84,
            /// <summary>
            /// Key: U | u
            /// </summary>
            U = 85,
            /// <summary>
            /// Key: V | v
            /// </summary>
            V = 86,
            /// <summary>
            /// Key: W | w
            /// </summary>
            W = 87,
            /// <summary>
            /// Key: X | x
            /// </summary>
            X = 88,
            /// <summary>
            /// Key: Y | y
            /// </summary>
            Y = 89,
            /// <summary>
            /// Key: Z | z
            /// </summary>
            Z = 90,
            /// <summary>
            /// Key: [
            /// </summary>
            LEFT_BRACKET = 91,
            /// <summary>
            /// Key: '\'
            /// </summary>
            BACKSLASH = 92,
            /// <summary>
            /// Key: ]
            /// </summary>
            RIGHT_BRACKET = 93,
            /// <summary>
            /// Key: `
            /// </summary>
            GRAVE = 96,
            /// <summary>
            /// Key: Space
            /// </summary>
            SPACE = 32,
            /// <summary>
            /// Key: Esc
            /// </summary>
            ESCAPE = 256,
            /// <summary>
            /// Key: Enter
            /// </summary>
            ENTER = 257,
            /// <summary>
            /// Key: Tab
            /// </summary>
            TAB = 258,
            /// <summary>
            /// Key: Backspace
            /// </summary>
            BACKSPACE = 259,
            /// <summary>
            /// Key: Ins
            /// </summary>
            INSERT = 260,
            /// <summary>
            /// Key: Del
            /// </summary>
            DELETE = 261,
            /// <summary>
            /// Key: Cursor right
            /// </summary>
            RIGHT = 262,
            /// <summary>
            /// Key: Cursor left
            /// </summary>
            LEFT = 263,
            /// <summary>
            /// Key: Cursor down
            /// </summary>
            DOWN = 264,
            /// <summary>
            /// Key: Cursor up
            /// </summary>
            UP = 265,
            /// <summary>
            /// Key: Page up
            /// </summary>
            PAGE_UP = 266,
            /// <summary>
            /// Key: Page down
            /// </summary>
            PAGE_DOWN = 267,
            /// <summary>
            /// Key: Home
            /// </summary>
            HOME = 268,
            /// <summary>
            /// Key: End
            /// </summary>
            END = 269,
            /// <summary>
            /// Key: Caps lock
            /// </summary>
            CAPS_LOCK = 280,
            /// <summary>
            /// Key: Scroll down
            /// </summary>
            SCROLL_LOCK = 281,
            /// <summary>
            /// Key: Num lock
            /// </summary>
            NUM_LOCK = 282,
            /// <summary>
            /// Key: Print screen
            /// </summary>
            PRINT_SCREEN = 283,
            /// <summary>
            /// Key: Pause
            /// </summary>
            PAUSE = 284,
            /// <summary>
            /// Key: F1
            /// </summary>
            F1 = 290,
            /// <summary>
            /// Key: F2
            /// </summary>
            F2 = 291,
            /// <summary>
            /// Key: F3
            /// </summary>
            F3 = 292,
            /// <summary>
            /// Key: F4
            /// </summary>
            F4 = 293,
            /// <summary>
            /// Key: F5
            /// </summary>
            F5 = 294,
            /// <summary>
            /// Key: F6
            /// </summary>
            F6 = 295,
            /// <summary>
            /// Key: F7
            /// </summary>
            F7 = 296,
            /// <summary>
            /// Key: F8
            /// </summary>
            F8 = 297,
            /// <summary>
            ///  Key: F9
            /// </summary>
            F9 = 298,
            /// <summary>
            /// Key: F10
            /// </summary>
            F10 = 299,
            /// <summary>
            /// Key: F11
            /// </summary>
            F11 = 300,
            /// <summary>
            /// Key: F12
            /// </summary>
            F12 = 301,
            /// <summary>
            /// Key: Shift left
            /// </summary>
            LEFT_SHIFT = 340,
            /// <summary>
            /// Key: Control left
            /// </summary>
            LEFT_CONTROL = 341,
            /// <summary>
            /// Key: Alt left
            /// </summary>
            LEFT_ALT = 342,
            /// <summary>
            /// Key: Super left
            /// </summary>
            LEFT_SUPER = 343,
            /// <summary>
            /// Key: Shift right
            /// </summary>
            RIGHT_SHIFT = 344,
            /// <summary>
            /// Key: Control right
            /// </summary>
            RIGHT_CONTROL = 345,
            /// <summary>
            /// Key: Alt right
            /// </summary>
            RIGHT_ALT = 346,
            /// <summary>
            /// Key: Super right
            /// </summary>
            RIGHT_SUPER = 347,
            /// <summary>
            /// Key: KB menu
            /// </summary>
            KB_MENU = 348,
            /// <summary>
            /// Key: Keypad 0
            /// </summary>
            KP_0 = 320,
            /// <summary>
            /// Key: Keypad 1
            /// </summary>
            KP_1 = 321,
            /// <summary>
            /// Key: Keypad 2
            /// </summary>
            KP_2 = 322,
            /// <summary>
            /// Key: Keypad 3
            /// </summary>
            KP_3 = 323,
            /// <summary>
            /// Key: Keypad 4
            /// </summary>
            KP_4 = 324,
            /// <summary>
            /// Key: Keypad 5
            /// </summary>
            KP_5 = 325,
            /// <summary>
            /// Key: Keypad 6
            /// </summary>
            KP_6 = 326,
            /// <summary>
            /// Key: Keypad 7
            /// </summary>
            KP_7 = 327,
            /// <summary>
            /// Key: Keypad 8
            /// </summary>
            KP_8 = 328,
            /// <summary>
            /// Key: Keypad 9
            /// </summary>
            KP_9 = 329,
            /// <summary>
            /// Key: Keypad .
            /// </summary>
            KP_DECIMAL = 330,
            /// <summary>
            /// Key: Keypad /
            /// </summary>
            KP_DIVIDE = 331,
            /// <summary>
            /// Key: Keypad *
            /// </summary>
            KP_MULTIPLY = 332,
            /// <summary>
            /// Key: Keypad -
            /// </summary>
            KP_SUBTRACT = 333,
            /// <summary>
            /// Key: Keypad +
            /// </summary>
            KP_ADD = 334,
            /// <summary>
            /// Key: Keypad Enter
            /// </summary>
            KP_ENTER = 335,
            /// <summary>
            /// Key: Keypad =
            /// </summary>
            KP_EQUAL = 336,
            /// <summary>
            /// Key: Android back button
            /// </summary>
            BACK = 4,
            /// <summary>
            /// Key: Android menu button
            /// </summary>
            MENU = 82,
            /// <summary>
            /// Key: Android volume up button
            /// </summary>
            VOLUME_UP = 24,
            /// <summary>
            /// Key: Android volume down button
            /// </summary>
            VOLUME_DOWN = 25
        }

        /// <summary>
        /// Detect if a key has been pressed once
        /// </summary>
        /// <param name="key">Keyboard Key</param>
        /// <returns>true if key has been precced once</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsKeyPressed")]
        public static extern bool IsPressed(KeyboardKey key);
        /// <summary>
        /// Detect if a key is being pressed
        /// </summary>
        /// <param name="key">Keyboard Key</param>
        /// <returns>true if key is being pressed</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsKeyDown")]
        public static extern bool IsDown(KeyboardKey key);
        /// <summary>
        /// Detect if a key has been released once
        /// </summary>
        /// <param name="key">Keyboard Key</param>
        /// <returns>true if key has been released once</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsKeyReleased")]
        public static extern bool IsReleased(KeyboardKey key);
        /// <summary>
        /// Detect if a key is NOT being pressed
        /// </summary>
        /// <param name="key">Keyboard Key</param>
        /// <returns>true if key is not being pressed</returns>
        [DllImport(Constants.dllName, EntryPoint = "IsKeyUp")]
        public static extern bool IsUp(KeyboardKey key);
        /// <summary>
        /// Set a custom key to exit program (default is ESC)
        /// </summary>
        /// <param name="key">Keyboard Key</param>
        [DllImport(Constants.dllName)]
        public static extern void SetExitKey(KeyboardKey key);
    }
}
