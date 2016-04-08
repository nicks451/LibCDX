using System;
using System.Collections.Generic;

namespace libcdx
{
    public interface IInput
    {
        float GetAccelerometerX();

        /** @return The rate of rotation around the y axis. (rad/s) */
        float GetAccelerometerY();

        /** @return The rate of rotation around the z axis. (rad/s) */
        float GetAccelerometerZ();

        float GetGyroscopeX();

        float GetGyroscopeY();

        float GetGyroscopeZ();

        /** @return The x coordinate of the last touch on touch screen devices and the current mouse position on desktop for the first
         *         pointer in screen coordinates. The screen origin is the top left corner. */
        int GetX();

        /** Returns the x coordinate in screen coordinates of the given pointer. Pointers are indexed from 0 to n. The pointer id
         * identifies the order in which the fingers went down on the screen, e.g. 0 is the first finger, 1 is the second and so on.
         * When two fingers are touched down and the first one is lifted the second one keeps its index. If another finger is placed on
         * the touch screen the first free index will be used.
         * 
         * @param pointer the pointer id.
         * @return the x coordinate */
        int GetX(int pointer);

        /** @return the different between the current pointer location and the last pointer location on the x-axis. */
        int GetDeltaX();

        /** @return the different between the current pointer location and the last pointer location on the x-axis. */
        int GetDeltaX(int pointer);

        /** @return The y coordinate of the last touch on touch screen devices and the current mouse position on desktop for the first
         *         pointer in screen coordinates. The screen origin is the top left corner. */
        int GetY();

        /** Returns the y coordinate in screen coordinates of the given pointer. Pointers are indexed from 0 to n. The pointer id
         * identifies the order in which the fingers went down on the screen, e.g. 0 is the first finger, 1 is the second and so on.
         * When two fingers are touched down and the first one is lifted the second one keeps its index. If another finger is placed on
         * the touch screen the first free index will be used.
         * 
         * @param pointer the pointer id.
         * @return the y coordinate */
        int GetY(int pointer);

        /** @return the different between the current pointer location and the last pointer location on the y-axis. */
        int GetDeltaY();

        /** @return the different between the current pointer location and the last pointer location on the y-axis. */
        int GetDeltaY(int pointer);

        /** @return whether the screen is currently touched. */
        bool IsTouched();

        /** @return whether a new touch down event just occurred. */
        bool JustTouched();

        /** Whether the screen is currently touched by the pointer with the given index. Pointers are indexed from 0 to n. The pointer
         * id identifies the order in which the fingers went down on the screen, e.g. 0 is the first finger, 1 is the second and so on.
         * When two fingers are touched down and the first one is lifted the second one keeps its index. If another finger is placed on
         * the touch screen the first free index will be used.
         * 
         * @param pointer the pointer
         * @return whether the screen is touched by the pointer */
        bool IsTouched(int pointer);

        /** Whether a given button is pressed or not. Button constants can be found in {@link Buttons}. On Android only the Button#LEFT
         * constant is meaningful before version 4.0.
         * @param button the button to check.
         * @return whether the button is down or not. */
        bool IsButtonPressed(int button);

        /** Returns whether the key is pressed.
         * 
         * @param key The key code as found in {@link Input.Keys}.
         * @return true or false. */
        bool IsKeyPressed(int key);

        /** Returns whether the key has just been pressed.
         * 
         * @param key The key code as found in {@link Input.Keys}.
         * @return true or false. */
        bool IsKeyJustPressed(int key);

        /** System dependent method to input a string of text. A dialog box will be created with the given title and the given text as a
         * message for the user. Once the dialog has been closed the provided {@link TextInputListener} will be called on the rendering
         * thread.
         * 
         * @param listener The TextInputListener.
         * @param title The title of the text input dialog.
         * @param text The message presented to the user. */
        void GetTextInput(ITextInputListener listener, String title, String text, String hint);

        /** Sets the on-screen keyboard visible if available.
         * 
         * @param visible visible or not */
        void SetOnscreenKeyboardVisible(bool visible);

        /** Vibrates for the given amount of time. Note that you'll need the permission
         * <code> <uses-permission android:name="android.permission.VIBRATE" /></code> in your manifest file in order for this to work.
         * 
         * @param milliseconds the number of milliseconds to vibrate. */
        void Vibrate(int milliseconds);

        /** Vibrate with a given pattern. Pass in an array of ints that are the times at which to turn on or off the vibrator. The first
         * one is how long to wait before turning it on, and then after that it alternates. If you want to repeat, pass the index into
         * the pattern at which to start the repeat.
         * @param pattern an array of longs of times to turn the vibrator on or off.
         * @param repeat the index into pattern at which to repeat, or -1 if you don't want to repeat. */
        void Vibrate(long[] pattern, int repeat);

        /** Stops the vibrator */
        void CancelVibrate();

        /** The azimuth is the angle of the device's orientation around the z-axis. The positive z-axis points towards the earths
         * center.
         * 
         * @see <a
         *      href="http://developer.android.com/reference/android/hardware/SensorManager.html#getRotationMatrix(float[], float[], float[], float[])">http://developer.android.com/reference/android/hardware/SensorManager.html#getRotationMatrix(float[], float[], float[], float[])</a>
         * @return the azimuth in degrees */
        float GetAzimuth();

        /** The pitch is the angle of the device's orientation around the x-axis. The positive x-axis roughly points to the west and is
         * orthogonal to the z- and y-axis.
         * @see <a
         *      href="http://developer.android.com/reference/android/hardware/SensorManager.html#getRotationMatrix(float[], float[], float[], float[])">http://developer.android.com/reference/android/hardware/SensorManager.html#getRotationMatrix(float[], float[], float[], float[])</a>
         * @return the pitch in degrees */
        float GetPitch();

        /** The roll is the angle of the device's orientation around the y-axis. The positive y-axis points to the magnetic north pole
         * of the earth.
         * @see <a
         *      href="http://developer.android.com/reference/android/hardware/SensorManager.html#getRotationMatrix(float[], float[], float[], float[])">http://developer.android.com/reference/android/hardware/SensorManager.html#getRotationMatrix(float[], float[], float[], float[])</a>
         * @return the roll in degrees */
        float GetRoll();

        /** Returns the rotation matrix describing the devices rotation as per <a href=
         * "http://developer.android.com/reference/android/hardware/SensorManager.html#getRotationMatrix(float[], float[], float[], float[])"
         * >SensorManager#getRotationMatrix(float[], float[], float[], float[])</a>. Does not manipulate the matrix if the platform
         * does not have an accelerometer.
         * @param matrix */
        void GetRotationMatrix(float[] matrix);

        /** @return the time of the event currently reported to the {@link InputProcessor}. */
        long GetCurrentEventTime();

        /** Sets whether the BACK button on Android should be caught. This will prevent the app from being paused. Will have no effect
         * on the desktop.
         * 
         * @param catchBack whether to catch the back button */
        void SetCatchBackKey(bool catchBack);

        /** @return whether the back button is currently being caught */
        bool IsCatchBackKey();

        /** Sets whether the MENU button on Android should be caught. This will prevent the onscreen keyboard to show up. Will have no
         * effect on the desktop.
         * 
         * @param catchMenu whether to catch the menu button */
        void SetCatchMenuKey(bool catchMenu);

        /** @return whether the menu button is currently being caught */
        bool IsCatchMenuKey();

        /** Sets the {@link InputProcessor} that will receive all touch and key input events. It will be called before the
         * {@link ApplicationListener#render()} method each frame.
         * 
         * @param processor the InputProcessor */
        void SetInputProcessor(IInputProcessor processor);

        /** @return the currently set {@link InputProcessor} or null. */
        IInputProcessor GetInputProcessor();

        /** Queries whether a {@link Peripheral} is currently available. In case of Android and the {@link Peripheral#HardwareKeyboard}
         * this returns the whether the keyboard is currently slid out or not.
         * 
         * @param peripheral the {@link Peripheral}
         * @return whether the peripheral is available or not. */
        bool IsPeripheralAvailable(Peripheral peripheral);

        /** @return the rotation of the device with respect to its native orientation. */
        int GetRotation();

        /** @return the native orientation of the device. */
        Orientation GetNativeOrientation();

        /** Only viable on the desktop. Will confine the mouse cursor location to the window and hide the mouse cursor. X and y
         * coordinates are still reported as if the mouse was not catched.
         * @param catched whether to catch or not to catch the mouse cursor */
        void SetCursorCatched(bool catched);

        /** @return whether the mouse cursor is catched. */
        bool IsCursorCatched();

        /** Only viable on the desktop. Will set the mouse cursor location to the given window coordinates (origin top-left corner).
         * @param x the x-position
         * @param y the y-position */
        void SetCursorPosition(int x, int y);
    }

    public interface ITextInputListener
    {
        void Input(string text);

        void Canceled();
    }

    /** Mouse buttons.
	 * @author mzechner */

    public static class Buttons
    {
        public const int LEFT = 0;
        public const int RIGHT = 1;
        public const int MIDDLE = 2;
        public const int BACK = 3;
        public const int FORWARD = 4;
    }

    /** Keys.
	 * 
	 * @author mzechner */

    public static class Keys
    {
        public const int ANY_KEY = -1;
        public const int NUM_0 = 7;
        public const int NUM_1 = 8;
        public const int NUM_2 = 9;
        public const int NUM_3 = 10;
        public const int NUM_4 = 11;
        public const int NUM_5 = 12;
        public const int NUM_6 = 13;
        public const int NUM_7 = 14;
        public const int NUM_8 = 15;
        public const int NUM_9 = 16;
        public const int A = 29;
        public const int ALT_LEFT = 57;
        public const int ALT_RIGHT = 58;
        public const int APOSTROPHE = 75;
        public const int AT = 77;
        public const int B = 30;
        public const int BACK = 4;
        public const int BACKSLASH = 73;
        public const int C = 31;
        public const int CALL = 5;
        public const int CAMERA = 27;
        public const int CLEAR = 28;
        public const int COMMA = 55;
        public const int D = 32;
        public const int DEL = 67;
        public const int BACKSPACE = 67;
        public const int FORWARD_DEL = 112;
        public const int DPAD_CENTER = 23;
        public const int DPAD_DOWN = 20;
        public const int DPAD_LEFT = 21;
        public const int DPAD_RIGHT = 22;
        public const int DPAD_UP = 19;
        public const int CENTER = 23;
        public const int DOWN = 20;
        public const int LEFT = 21;
        public const int RIGHT = 22;
        public const int UP = 19;
        public const int E = 33;
        public const int ENDCALL = 6;
        public const int ENTER = 66;
        public const int ENVELOPE = 65;
        public const int EQUALS = 70;
        public const int EXPLORER = 64;
        public const int F = 34;
        public const int FOCUS = 80;
        public const int G = 35;
        public const int GRAVE = 68;
        public const int H = 36;
        public const int HEADSETHOOK = 79;
        public const int HOME = 3;
        public const int I = 37;
        public const int J = 38;
        public const int K = 39;
        public const int L = 40;
        public const int LEFT_BRACKET = 71;
        public const int M = 41;
        public const int MEDIA_FAST_FORWARD = 90;
        public const int MEDIA_NEXT = 87;
        public const int MEDIA_PLAY_PAUSE = 85;
        public const int MEDIA_PREVIOUS = 88;
        public const int MEDIA_REWIND = 89;
        public const int MEDIA_STOP = 86;
        public const int MENU = 82;
        public const int MINUS = 69;
        public const int MUTE = 91;
        public const int N = 42;
        public const int NOTIFICATION = 83;
        public const int NUM = 78;
        public const int O = 43;
        public const int P = 44;
        public const int PERIOD = 56;
        public const int PLUS = 81;
        public const int POUND = 18;
        public const int POWER = 26;
        public const int Q = 45;
        public const int R = 46;
        public const int RIGHT_BRACKET = 72;
        public const int S = 47;
        public const int SEARCH = 84;
        public const int SEMICOLON = 74;
        public const int SHIFT_LEFT = 59;
        public const int SHIFT_RIGHT = 60;
        public const int SLASH = 76;
        public const int SOFT_LEFT = 1;
        public const int SOFT_RIGHT = 2;
        public const int SPACE = 62;
        public const int STAR = 17;
        public const int SYM = 63;
        public const int T = 48;
        public const int TAB = 61;
        public const int U = 49;
        public const int UNKNOWN = 0;
        public const int V = 50;
        public const int VOLUME_DOWN = 25;
        public const int VOLUME_UP = 24;
        public const int W = 51;
        public const int X = 52;
        public const int Y = 53;
        public const int Z = 54;
        public const int META_ALT_LEFT_ON = 16;
        public const int META_ALT_ON = 2;
        public const int META_ALT_RIGHT_ON = 32;
        public const int META_SHIFT_LEFT_ON = 64;
        public const int META_SHIFT_ON = 1;
        public const int META_SHIFT_RIGHT_ON = 128;
        public const int META_SYM_ON = 4;
        public const int CONTROL_LEFT = 129;
        public const int CONTROL_RIGHT = 130;
        public const int ESCAPE = 131;
        public const int END = 132;
        public const int INSERT = 133;
        public const int PAGE_UP = 92;
        public const int PAGE_DOWN = 93;
        public const int PICTSYMBOLS = 94;
        public const int SWITCH_CHARSET = 95;
        public const int BUTTON_CIRCLE = 255;
        public const int BUTTON_A = 96;
        public const int BUTTON_B = 97;
        public const int BUTTON_C = 98;
        public const int BUTTON_X = 99;
        public const int BUTTON_Y = 100;
        public const int BUTTON_Z = 101;
        public const int BUTTON_L1 = 102;
        public const int BUTTON_R1 = 103;
        public const int BUTTON_L2 = 104;
        public const int BUTTON_R2 = 105;
        public const int BUTTON_THUMBL = 106;
        public const int BUTTON_THUMBR = 107;
        public const int BUTTON_START = 108;
        public const int BUTTON_SELECT = 109;
        public const int BUTTON_MODE = 110;

        public const int NUMPAD_0 = 144;
        public const int NUMPAD_1 = 145;
        public const int NUMPAD_2 = 146;
        public const int NUMPAD_3 = 147;
        public const int NUMPAD_4 = 148;
        public const int NUMPAD_5 = 149;
        public const int NUMPAD_6 = 150;
        public const int NUMPAD_7 = 151;
        public const int NUMPAD_8 = 152;
        public const int NUMPAD_9 = 153;

        // public const int BACKTICK = 0;
        // public const int TILDE = 0;
        // public const int UNDERSCORE = 0;
        // public const int DOT = 0;
        // public const int BREAK = 0;
        // public const int PIPE = 0;
        // public const int EXCLAMATION = 0;
        // public const int QUESTIONMARK = 0;

        // ` | VK_BACKTICK
        // ~ | VK_TILDE
        // : | VK_COLON
        // _ | VK_UNDERSCORE
        // . | VK_DOT
        // (break) | VK_BREAK
        // | | VK_PIPE
        // ! | VK_EXCLAMATION
        // ? | VK_QUESTION
        public const int COLON = 243;
        public const int F1 = 244;
        public const int F2 = 245;
        public const int F3 = 246;
        public const int F4 = 247;
        public const int F5 = 248;
        public const int F6 = 249;
        public const int F7 = 250;
        public const int F8 = 251;
        public const int F9 = 252;
        public const int F10 = 253;
        public const int F11 = 254;
        public const int F12 = 255;

        /** @return a human readable representation of the keycode. The returned value can be used in
		 *         {@link Input.Keys#valueOf(String)} */

        public static string toString(int keycode)
        {
            if (keycode < 0) throw new ArgumentOutOfRangeException("keycode cannot be negative, keycode: " + keycode);
            if (keycode > 255)
                throw new ArgumentOutOfRangeException("keycode cannot be greater than 255, keycode: " + keycode);
            switch (keycode)
            {
                // META* variables should not be used with this method.
                case UNKNOWN:
                    return "Unknown";
                case SOFT_LEFT:
                    return "Soft Left";
                case SOFT_RIGHT:
                    return "Soft Right";
                case HOME:
                    return "Home";
                case BACK:
                    return "Back";
                case CALL:
                    return "Call";
                case ENDCALL:
                    return "End Call";
                case NUM_0:
                    return "0";
                case NUM_1:
                    return "1";
                case NUM_2:
                    return "2";
                case NUM_3:
                    return "3";
                case NUM_4:
                    return "4";
                case NUM_5:
                    return "5";
                case NUM_6:
                    return "6";
                case NUM_7:
                    return "7";
                case NUM_8:
                    return "8";
                case NUM_9:
                    return "9";
                case STAR:
                    return "*";
                case POUND:
                    return "#";
                case UP:
                    return "Up";
                case DOWN:
                    return "Down";
                case LEFT:
                    return "Left";
                case RIGHT:
                    return "Right";
                case CENTER:
                    return "Center";
                case VOLUME_UP:
                    return "Volume Up";
                case VOLUME_DOWN:
                    return "Volume Down";
                case POWER:
                    return "Power";
                case CAMERA:
                    return "Camera";
                case CLEAR:
                    return "Clear";
                case A:
                    return "A";
                case B:
                    return "B";
                case C:
                    return "C";
                case D:
                    return "D";
                case E:
                    return "E";
                case F:
                    return "F";
                case G:
                    return "G";
                case H:
                    return "H";
                case I:
                    return "I";
                case J:
                    return "J";
                case K:
                    return "K";
                case L:
                    return "L";
                case M:
                    return "M";
                case N:
                    return "N";
                case O:
                    return "O";
                case P:
                    return "P";
                case Q:
                    return "Q";
                case R:
                    return "R";
                case S:
                    return "S";
                case T:
                    return "T";
                case U:
                    return "U";
                case V:
                    return "V";
                case W:
                    return "W";
                case X:
                    return "X";
                case Y:
                    return "Y";
                case Z:
                    return "Z";
                case COMMA:
                    return ",";
                case PERIOD:
                    return ".";
                case ALT_LEFT:
                    return "L-Alt";
                case ALT_RIGHT:
                    return "R-Alt";
                case SHIFT_LEFT:
                    return "L-Shift";
                case SHIFT_RIGHT:
                    return "R-Shift";
                case TAB:
                    return "Tab";
                case SPACE:
                    return "Space";
                case SYM:
                    return "SYM";
                case EXPLORER:
                    return "Explorer";
                case ENVELOPE:
                    return "Envelope";
                case ENTER:
                    return "Enter";
                case DEL:
                    return "Delete"; // also BACKSPACE
                case GRAVE:
                    return "`";
                case MINUS:
                    return "-";
                case EQUALS:
                    return "=";
                case LEFT_BRACKET:
                    return "[";
                case RIGHT_BRACKET:
                    return "]";
                case BACKSLASH:
                    return "\\";
                case SEMICOLON:
                    return ";";
                case APOSTROPHE:
                    return "'";
                case SLASH:
                    return "/";
                case AT:
                    return "@";
                case NUM:
                    return "Num";
                case HEADSETHOOK:
                    return "Headset Hook";
                case FOCUS:
                    return "Focus";
                case PLUS:
                    return "Plus";
                case MENU:
                    return "Menu";
                case NOTIFICATION:
                    return "Notification";
                case SEARCH:
                    return "Search";
                case MEDIA_PLAY_PAUSE:
                    return "Play/Pause";
                case MEDIA_STOP:
                    return "Stop Media";
                case MEDIA_NEXT:
                    return "Next Media";
                case MEDIA_PREVIOUS:
                    return "Prev Media";
                case MEDIA_REWIND:
                    return "Rewind";
                case MEDIA_FAST_FORWARD:
                    return "Fast Forward";
                case MUTE:
                    return "Mute";
                case PAGE_UP:
                    return "Page Up";
                case PAGE_DOWN:
                    return "Page Down";
                case PICTSYMBOLS:
                    return "PICTSYMBOLS";
                case SWITCH_CHARSET:
                    return "SWITCH_CHARSET";
                case BUTTON_A:
                    return "A Button";
                case BUTTON_B:
                    return "B Button";
                case BUTTON_C:
                    return "C Button";
                case BUTTON_X:
                    return "X Button";
                case BUTTON_Y:
                    return "Y Button";
                case BUTTON_Z:
                    return "Z Button";
                case BUTTON_L1:
                    return "L1 Button";
                case BUTTON_R1:
                    return "R1 Button";
                case BUTTON_L2:
                    return "L2 Button";
                case BUTTON_R2:
                    return "R2 Button";
                case BUTTON_THUMBL:
                    return "Left Thumb";
                case BUTTON_THUMBR:
                    return "Right Thumb";
                case BUTTON_START:
                    return "Start";
                case BUTTON_SELECT:
                    return "Select";
                case BUTTON_MODE:
                    return "Button Mode";
                case FORWARD_DEL:
                    return "Forward Delete";
                case CONTROL_LEFT:
                    return "L-Ctrl";
                case CONTROL_RIGHT:
                    return "R-Ctrl";
                case ESCAPE:
                    return "Escape";
                case END:
                    return "End";
                case INSERT:
                    return "Insert";
                case NUMPAD_0:
                    return "Numpad 0";
                case NUMPAD_1:
                    return "Numpad 1";
                case NUMPAD_2:
                    return "Numpad 2";
                case NUMPAD_3:
                    return "Numpad 3";
                case NUMPAD_4:
                    return "Numpad 4";
                case NUMPAD_5:
                    return "Numpad 5";
                case NUMPAD_6:
                    return "Numpad 6";
                case NUMPAD_7:
                    return "Numpad 7";
                case NUMPAD_8:
                    return "Numpad 8";
                case NUMPAD_9:
                    return "Numpad 9";
                case COLON:
                    return ":";
                case F1:
                    return "F1";
                case F2:
                    return "F2";
                case F3:
                    return "F3";
                case F4:
                    return "F4";
                case F5:
                    return "F5";
                case F6:
                    return "F6";
                case F7:
                    return "F7";
                case F8:
                    return "F8";
                case F9:
                    return "F9";
                case F10:
                    return "F10";
                case F11:
                    return "F11";
                case F12:
                    return "F12";
                // BUTTON_CIRCLE unhandled, as it conflicts with the more likely to be pressed F12
                default:
                    // key name not found
                    return null;
            }
        }

        private static Dictionary<string, int> keyNames;

        /** @param keyname the keyname returned by the {@link Keys#toString(int)} method
		 * @return the int keycode */
        public static int ValueOf(String keyname)
        {
            int keyValue = -1;
            if (keyNames == null) InitializeKeyNames();
            if (keyNames != null) keyNames.TryGetValue(keyname, out keyValue);

            return keyValue;
        }

        /** lazily intialized in {@link Keys#valueOf(String)} */
        private static void InitializeKeyNames()
        {
            keyNames = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
            {
                String name = toString(i);
                if (name != null) keyNames.Add(name, i);
            }
        }
    }

    public enum Peripheral
    {
        HardwareKeyboard, OnscreenKeyboard, MultitouchScreen, Accelerometer, Compass, Vibrator, Gyroscope
    }

    public enum Orientation
    {
        Landscape, Portrait
    }
}