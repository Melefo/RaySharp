
using System.Runtime.InteropServices;

namespace RaySharp.Input
{
    public class Gamepad
    {
        [DllImport(Constants.dllName)]
        private static extern bool IsGamepadAvailable(int gamepad);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern string GetGamepadName(int gamepad);
        [DllImport(Constants.dllName)]
        private static extern bool IsGamepadButtonPressed(int gamepad, GamepadButton button);
        [DllImport(Constants.dllName)]
        private static extern bool IsGamepadButtonDown(int gamepad, GamepadButton button);
        [DllImport(Constants.dllName)]
        private static extern bool IsGamepadButtonReleased(int gamepad, GamepadButton button);
        [DllImport(Constants.dllName)]
        private static extern bool IsGamepadButtonUp(int gamepad, GamepadButton button);
        [DllImport(Constants.dllName)]
        private static extern GamepadButton GetGamepadButtonPressed();
        [DllImport(Constants.dllName)]
        private static extern int GetGamepadAxisCount(int gamepad);
        [DllImport(Constants.dllName)]
        private static extern float GetGamepadAxisMovement(int gamepad, GamepadAxis axis);

        /// <summary>
        /// Gamepad buttons
        /// </summary>
        public enum GamepadButton : int
        {
            /// <summary>
            /// Unknown button, just for error checking
            /// </summary>
            UNKNOWN = 0,
            /// <summary>
            /// Gamepad left DPAD up button
            /// </summary>
            LEFT_FACE_UP,
            /// <summary>
            /// Gamepad left DPAD right button
            /// </summary>
            LEFT_FACE_RIGHT,
            /// <summary>
            /// Gamepad left DPAD down button
            /// </summary>
            LEFT_FACE_DOWN,
            /// <summary>
            /// Gamepad left DPAD left button
            /// </summary>
            LEFT_FACE_LEFT,
            /// <summary>
            /// Gamepad right button up (i.e. PS3: Triangle, Xbox: Y)
            /// </summary>
            RIGHT_FACE_UP,
            /// <summary>
            /// Gamepad right button right (i.e. PS3: Square, Xbox: X)
            /// </summary>
            RIGHT_FACE_RIGHT,
            /// <summary>
            /// Gamepad right button down (i.e. PS3: Cross, Xbox: A)
            /// </summary>
            RIGHT_FACE_DOWN,
            /// <summary>
            /// Gamepad right button left (i.e. PS3: Circle, Xbox: B)
            /// </summary>
            RIGHT_FACE_LEFT,
            /// <summary>
            /// Gamepad top/back trigger left (first), it could be a trailing button
            /// </summary>
            LEFT_TRIGGER_1,
            /// <summary>
            /// Gamepad top/back trigger left (second), it could be a trailing button
            /// </summary>
            LEFT_TRIGGER_2,
            /// <summary>
            /// Gamepad top/back trigger right (one), it could be a trailing button
            /// </summary>
            RIGHT_TRIGGER_1,
            /// <summary>
            /// Gamepad top/back trigger right (second), it could be a trailing button
            /// </summary>
            RIGHT_TRIGGER_2,
            /// <summary>
            /// Gamepad center buttons, left one (i.e. PS3: Select)
            /// </summary>
            MIDDLE_LEFT,
            /// <summary>
            /// Gamepad center buttons, middle one (i.e. PS3: PS, Xbox: XBOX)
            /// </summary>
            MIDDLE,
            /// <summary>
            /// Gamepad center buttons, right one (i.e. PS3: Start)
            /// </summary>
            MIDDLE_RIGHT,
            /// <summary>
            /// Gamepad joystick pressed button left
            /// </summary>
            LEFT_THUMB,
            /// <summary>
            /// Gamepad joystick pressed button right
            /// </summary>
            RIGHT_THUMB
        }

        /// <summary>
        /// Gamepad axis
        /// </summary>
        public enum GamepadAxis : int
        {
            /// <summary>
            /// Gamepad left stick X axis
            /// </summary>
            LEFT_X = 0,
            /// <summary>
            /// Gamepad left stick Y axis
            /// </summary>
            LEFT_Y = 1,
            /// <summary>
            /// Gamepad right stick X axis
            /// </summary>
            RIGHT_X = 2,
            /// <summary>
            /// Gamepad right stick Y axis
            /// </summary>
            RIGHT_Y = 3,
            /// <summary>
            /// Gamepad back trigger left, pressure level: [1..-1]
            /// </summary>
            LEFT_TRIGGER = 4,
            /// <summary>
            /// Gamepad back trigger right, pressure level: [1..-1]
            /// </summary>
            RIGHT_TRIGGER = 5
        }

        /// <summary>
        /// Get the last gamepad button pressed
        /// </summary>
        public static GamepadButton LastButtonPressed => GetGamepadButtonPressed();

        public int Id { get; }

        /// <summary>
        /// Detect if a gamepad is available
        /// </summary>
        public bool Availabe => IsGamepadAvailable(Id);
        /// <summary>
        /// Return gamepad internal name id
        /// </summary>
        public string Name => GetGamepadName(Id);

        /// <summary>
        /// Return gamepad axis count for a gamepad
        /// </summary>
        public int AxisCount => GetGamepadAxisCount(Id);

        public Gamepad(int id) => Id = id;

        /// <summary>
        /// Set internal gamepad mappings (SDL_GameControllerDB)
        /// </summary>
        /// <param name="mappings">The string containing the gamepad mappings</param>
        /// <returns>1 if successful, 0 if failure</returns>
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi, EntryPoint = "SetGamepadMappings")]
        private static extern int SetMappings(string mappings);

        /// <summary>
        /// Detect if a gamepad button has been pressed once
        /// </summary>
        /// <param name="button">Gamepad Button</param>
        /// <returns>true if button has been pressed once</returns>
        public bool IsPressed(GamepadButton button) => IsGamepadButtonPressed(Id, button);
        /// <summary>
        /// Detect if a gamepad button is being pressed
        /// </summary>
        /// <param name="button">Gamepad Button</param>
        /// <returns>true if button is being pressed</returns>
        public bool IsDown(GamepadButton button) => IsGamepadButtonDown(Id, button);
        /// <summary>
        /// Detect if a gamepad button has been released once
        /// </summary>
        /// <param name="button">Gamepad Button</param>
        /// <returns>true if button has been released once</returns>
        public bool IsReleased(GamepadButton button) => IsGamepadButtonReleased(Id, button);
        /// <summary>
        /// Detect if a gamepad button is NOT being pressed
        /// </summary>
        /// <param name="button">Gamepad Button</param>
        /// <returns>true if button is not being pressed</returns>
        public bool IsUp(GamepadButton button) => IsGamepadButtonUp(Id, button);

        /// <summary>
        /// Return axis movement value for a gamepad axis
        /// </summary>
        /// <param name="axis">Gamepad Axis</param>
        /// <returns>Movement value of axix</returns>
        public float GetAxisMovement(GamepadAxis axis) => GetGamepadAxisMovement(Id, axis);
    }
}
