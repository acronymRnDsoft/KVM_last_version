/*
 * keyMouSerial
 * Record keystrokes and mouse movement, and send them over serial
 * Peter Burkimsher 2015-06-25
 * peterburk@gmail.com
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using System.Drawing;
using System.Timers;
using System.Threading;

namespace keyMouSerial
{
    // Summary:
    //     Specifies key codes and modifiers.
    [ComVisible(true)]
    [Flags]

    // Key codes
    public enum Keys
    {
        // Summary:
        //     The bitmask to extract modifiers from a key value.
        Modifiers = -65536,
        //
        // Summary:
        //     No key pressed.
        None = 0,
        //
        // Summary:
        //     The left mouse button.
        LButton = 1,
        //
        // Summary:
        //     The right mouse button.
        RButton = 2,
        //
        // Summary:
        //     The CANCEL key.
        Cancel = 3,
        //
        // Summary:
        //     The middle mouse button (three-button mouse).
        MButton = 4,
        //
        // Summary:
        //     The first x mouse button (five-button mouse).
        XButton1 = 5,
        //
        // Summary:
        //     The second x mouse button (five-button mouse).
        XButton2 = 6,
        //
        // Summary:
        //     The BACKSPACE key.
        Back = 8,
        //
        // Summary:
        //     The TAB key.
        Tab = 9,
        //
        // Summary:
        //     The LINEFEED key.
        LineFeed = 10,
        //
        // Summary:
        //     The CLEAR key.
        Clear = 12,
        //
        // Summary:
        //     The ENTER key.
        Enter = 13,
        //
        // Summary:
        //     The RETURN key.
        Return = 13,
        //
        // Summary:
        //     The SHIFT key.
        ShiftKey = 16,
        //
        // Summary:
        //     The CTRL key.
        ControlKey = 17,
        //
        // Summary:
        //     The ALT key.
        Menu = 18,
        //
        // Summary:
        //     The PAUSE key.
        Pause = 19,
        //
        // Summary:
        //     The CAPS LOCK key.
        CapsLock = 20,
        //
        // Summary:
        //     The CAPS LOCK key.
        Capital = 20,
        //
        // Summary:
        //     The IME Kana mode key.
        KanaMode = 21,
        //
        // Summary:
        //     The IME Hanguel mode key. (maintained for compatibility; use HangulMode)
        HanguelMode = 21,
        //
        // Summary:
        //     The IME Hangul mode key.
        HangulMode = 21,
        //
        // Summary:
        //     The IME Junja mode key.
        JunjaMode = 23,
        //
        // Summary:
        //     The IME final mode key.
        FinalMode = 24,
        //
        // Summary:
        //     The IME Kanji mode key.
        KanjiMode = 25,
        //
        // Summary:
        //     The IME Hanja mode key.
        HanjaMode = 25,
        //
        // Summary:
        //     The ESC key.
        Escape = 27,
        //
        // Summary:
        //     The IME convert key.
        IMEConvert = 28,
        //
        // Summary:
        //     The IME nonconvert key.
        IMENonconvert = 29,
        //
        // Summary:
        //     The IME accept key. Obsolete, use System.Windows.Forms.Keys.IMEAccept instead.
        IMEAceept = 30,
        //
        // Summary:
        //     The IME accept key, replaces System.Windows.Forms.Keys.IMEAceept.
        IMEAccept = 30,
        //
        // Summary:
        //     The IME mode change key.
        IMEModeChange = 31,
        //
        // Summary:
        //     The SPACEBAR key.
        Space = 32,
        //
        // Summary:
        //     The PAGE UP key.
        Prior = 33,
        //
        // Summary:
        //     The PAGE UP key.
        PageUp = 33,
        //
        // Summary:
        //     The PAGE DOWN key.
        Next = 34,
        //
        // Summary:
        //     The PAGE DOWN key.
        PageDown = 34,
        //
        // Summary:
        //     The END key.
        End = 35,
        //
        // Summary:
        //     The HOME key.
        Home = 36,
        //
        // Summary:
        //     The LEFT ARROW key.
        Left = 37,
        //
        // Summary:
        //     The UP ARROW key.
        Up = 38,
        //
        // Summary:
        //     The RIGHT ARROW key.
        Right = 39,
        //
        // Summary:
        //     The DOWN ARROW key.
        Down = 40,
        //
        // Summary:
        //     The SELECT key.
        Select = 41,
        //
        // Summary:
        //     The PRINT key.
        Print = 42,
        //
        // Summary:
        //     The EXECUTE key.
        Execute = 43,
        //
        // Summary:
        //     The PRINT SCREEN key.
        PrintScreen = 44,
        //
        // Summary:
        //     The PRINT SCREEN key.
        Snapshot = 44,
        //
        // Summary:
        //     The INS key.
        Insert = 45,
        //
        // Summary:
        //     The DEL key.
        Delete = 46,
        //
        // Summary:
        //     The HELP key.
        Help = 47,
        //
        // Summary:
        //     The 0 key.
        D0 = 48,
        //
        // Summary:
        //     The 1 key.
        D1 = 49,
        //
        // Summary:
        //     The 2 key.
        D2 = 50,
        //
        // Summary:
        //     The 3 key.
        D3 = 51,
        //
        // Summary:
        //     The 4 key.
        D4 = 52,
        //
        // Summary:
        //     The 5 key.
        D5 = 53,
        //
        // Summary:
        //     The 6 key.
        D6 = 54,
        //
        // Summary:
        //     The 7 key.
        D7 = 55,
        //
        // Summary:
        //     The 8 key.
        D8 = 56,
        //
        // Summary:
        //     The 9 key.
        D9 = 57,
        //
        // Summary:
        //     The A key.
        A = 65,
        //
        // Summary:
        //     The B key.
        B = 66,
        //
        // Summary:
        //     The C key.
        C = 67,
        //
        // Summary:
        //     The D key.
        D = 68,
        //
        // Summary:
        //     The E key.
        E = 69,
        //
        // Summary:
        //     The F key.
        F = 70,
        //
        // Summary:
        //     The G key.
        G = 71,
        //
        // Summary:
        //     The H key.
        H = 72,
        //
        // Summary:
        //     The I key.
        I = 73,
        //
        // Summary:
        //     The J key.
        J = 74,
        //
        // Summary:
        //     The K key.
        K = 75,
        //
        // Summary:
        //     The L key.
        L = 76,
        //
        // Summary:
        //     The M key.
        M = 77,
        //
        // Summary:
        //     The N key.
        N = 78,
        //
        // Summary:
        //     The O key.
        O = 79,
        //
        // Summary:
        //     The P key.
        P = 80,
        //
        // Summary:
        //     The Q key.
        Q = 81,
        //
        // Summary:
        //     The R key.
        R = 82,
        //
        // Summary:
        //     The S key.
        S = 83,
        //
        // Summary:
        //     The T key.
        T = 84,
        //
        // Summary:
        //     The U key.
        U = 85,
        //
        // Summary:
        //     The V key.
        V = 86,
        //
        // Summary:
        //     The W key.
        W = 87,
        //
        // Summary:
        //     The X key.
        X = 88,
        //
        // Summary:
        //     The Y key.
        Y = 89,
        //
        // Summary:
        //     The Z key.
        Z = 90,
        //
        // Summary:
        //     The left Windows logo key (Microsoft Natural Keyboard).
        LWin = 91,
        //
        // Summary:
        //     The right Windows logo key (Microsoft Natural Keyboard).
        RWin = 92,
        //
        // Summary:
        //     The application key (Microsoft Natural Keyboard).
        Apps = 93,
        //
        // Summary:
        //     The computer sleep key.
        Sleep = 95,
        //
        // Summary:
        //     The 0 key on the numeric keypad.
        NumPad0 = 96,
        //
        // Summary:
        //     The 1 key on the numeric keypad.
        NumPad1 = 97,
        //
        // Summary:
        //     The 2 key on the numeric keypad.
        NumPad2 = 98,
        //
        // Summary:
        //     The 3 key on the numeric keypad.
        NumPad3 = 99,
        //
        // Summary:
        //     The 4 key on the numeric keypad.
        NumPad4 = 100,
        //
        // Summary:
        //     The 5 key on the numeric keypad.
        NumPad5 = 101,
        //
        // Summary:
        //     The 6 key on the numeric keypad.
        NumPad6 = 102,
        //
        // Summary:
        //     The 7 key on the numeric keypad.
        NumPad7 = 103,
        //
        // Summary:
        //     The 8 key on the numeric keypad.
        NumPad8 = 104,
        //
        // Summary:
        //     The 9 key on the numeric keypad.
        NumPad9 = 105,
        //
        // Summary:
        //     The multiply key.
        Multiply = 106,
        //
        // Summary:
        //     The add key.
        Add = 107,
        //
        // Summary:
        //     The separator key.
        Separator = 108,
        //
        // Summary:
        //     The subtract key.
        Subtract = 109,
        //
        // Summary:
        //     The decimal key.
        Decimal = 110,
        //
        // Summary:
        //     The divide key.
        Divide = 111,
        //
        // Summary:
        //     The F1 key.
        F1 = 112,
        //
        // Summary:
        //     The F2 key.
        F2 = 113,
        //
        // Summary:
        //     The F3 key.
        F3 = 114,
        //
        // Summary:
        //     The F4 key.
        F4 = 115,
        //
        // Summary:
        //     The F5 key.
        F5 = 116,
        //
        // Summary:
        //     The F6 key.
        F6 = 117,
        //
        // Summary:
        //     The F7 key.
        F7 = 118,
        //
        // Summary:
        //     The F8 key.
        F8 = 119,
        //
        // Summary:
        //     The F9 key.
        F9 = 120,
        //
        // Summary:
        //     The F10 key.
        F10 = 121,
        //
        // Summary:
        //     The F11 key.
        F11 = 122,
        //
        // Summary:
        //     The F12 key.
        F12 = 123,
        //
        // Summary:
        //     The F13 key.
        F13 = 124,
        //
        // Summary:
        //     The F14 key.
        F14 = 125,
        //
        // Summary:
        //     The F15 key.
        F15 = 126,
        //
        // Summary:
        //     The F16 key.
        F16 = 127,
        //
        // Summary:
        //     The F17 key.
        F17 = 128,
        //
        // Summary:
        //     The F18 key.
        F18 = 129,
        //
        // Summary:
        //     The F19 key.
        F19 = 130,
        //
        // Summary:
        //     The F20 key.
        F20 = 131,
        //
        // Summary:
        //     The F21 key.
        F21 = 132,
        //
        // Summary:
        //     The F22 key.
        F22 = 133,
        //
        // Summary:
        //     The F23 key.
        F23 = 134,
        //
        // Summary:
        //     The F24 key.
        F24 = 135,
        //
        // Summary:
        //     The NUM LOCK key.
        NumLock = 144,
        //
        // Summary:
        //     The SCROLL LOCK key.
        Scroll = 145,
        //
        // Summary:
        //     The left SHIFT key.
        LShiftKey = 160,
        //
        // Summary:
        //     The right SHIFT key.
        RShiftKey = 161,
        //
        // Summary:
        //     The left CTRL key.
        LControlKey = 162,
        //
        // Summary:
        //     The right CTRL key.
        RControlKey = 163,
        //
        // Summary:
        //     The left ALT key.
        LMenu = 164,
        //
        // Summary:
        //     The right ALT key.
        RMenu = 165,
        //
        // Summary:
        //     The browser back key (Windows 2000 or later).
        BrowserBack = 166,
        //
        // Summary:
        //     The browser forward key (Windows 2000 or later).
        BrowserForward = 167,
        //
        // Summary:
        //     The browser refresh key (Windows 2000 or later).
        BrowserRefresh = 168,
        //
        // Summary:
        //     The browser stop key (Windows 2000 or later).
        BrowserStop = 169,
        //
        // Summary:
        //     The browser search key (Windows 2000 or later).
        BrowserSearch = 170,
        //
        // Summary:
        //     The browser favorites key (Windows 2000 or later).
        BrowserFavorites = 171,
        //
        // Summary:
        //     The browser home key (Windows 2000 or later).
        BrowserHome = 172,
        //
        // Summary:
        //     The volume mute key (Windows 2000 or later).
        VolumeMute = 173,
        //
        // Summary:
        //     The volume down key (Windows 2000 or later).
        VolumeDown = 174,
        //
        // Summary:
        //     The volume up key (Windows 2000 or later).
        VolumeUp = 175,
        //
        // Summary:
        //     The media next track key (Windows 2000 or later).
        MediaNextTrack = 176,
        //
        // Summary:
        //     The media previous track key (Windows 2000 or later).
        MediaPreviousTrack = 177,
        //
        // Summary:
        //     The media Stop key (Windows 2000 or later).
        MediaStop = 178,
        //
        // Summary:
        //     The media play pause key (Windows 2000 or later).
        MediaPlayPause = 179,
        //
        // Summary:
        //     The launch mail key (Windows 2000 or later).
        LaunchMail = 180,
        //
        // Summary:
        //     The select media key (Windows 2000 or later).
        SelectMedia = 181,
        //
        // Summary:
        //     The start application one key (Windows 2000 or later).
        LaunchApplication1 = 182,
        //
        // Summary:
        //     The start application two key (Windows 2000 or later).
        LaunchApplication2 = 183,
        //
        // Summary:
        //     The OEM 1 key.
        Oem1 = 186,
        //
        // Summary:
        //     The OEM Semicolon key on a US standard keyboard (Windows 2000 or later).
        OemSemicolon = 186,
        //
        // Summary:
        //     The OEM plus key on any country/region keyboard (Windows 2000 or later).
        Oemplus = 187,
        //
        // Summary:
        //     The OEM comma key on any country/region keyboard (Windows 2000 or later).
        Oemcomma = 188,
        //
        // Summary:
        //     The OEM minus key on any country/region keyboard (Windows 2000 or later).
        OemMinus = 189,
        //
        // Summary:
        //     The OEM period key on any country/region keyboard (Windows 2000 or later).
        OemPeriod = 190,
        //
        // Summary:
        //     The OEM question mark key on a US standard keyboard (Windows 2000 or later).
        OemQuestion = 191,
        //
        // Summary:
        //     The OEM 2 key.
        Oem2 = 191,
        //
        // Summary:
        //     The OEM tilde key on a US standard keyboard (Windows 2000 or later).
        Oemtilde = 192,
        //
        // Summary:
        //     The OEM 3 key.
        Oem3 = 192,
        //
        // Summary:
        //     The OEM 4 key.
        Oem4 = 219,
        //
        // Summary:
        //     The OEM open bracket key on a US standard keyboard (Windows 2000 or later).
        OemOpenBrackets = 219,
        //
        // Summary:
        //     The OEM pipe key on a US standard keyboard (Windows 2000 or later).
        OemPipe = 220,
        //
        // Summary:
        //     The OEM 5 key.
        Oem5 = 220,
        //
        // Summary:
        //     The OEM 6 key.
        Oem6 = 221,
        //
        // Summary:
        //     The OEM close bracket key on a US standard keyboard (Windows 2000 or later).
        OemCloseBrackets = 221,
        //
        // Summary:
        //     The OEM 7 key.
        Oem7 = 222,
        //
        // Summary:
        //     The OEM singled/double quote key on a US standard keyboard (Windows 2000
        //     or later).
        OemQuotes = 222,
        //
        // Summary:
        //     The OEM 8 key.
        Oem8 = 223,
        //
        // Summary:
        //     The OEM 102 key.
        Oem102 = 226,
        //
        // Summary:
        //     The OEM angle bracket or backslash key on the RT 102 key keyboard (Windows
        //     2000 or later).
        OemBackslash = 226,
        //
        // Summary:
        //     The PROCESS KEY key.
        ProcessKey = 229,
        //
        // Summary:
        //     Used to pass Unicode characters as if they were keystrokes. The Packet key
        //     value is the low word of a 32-bit virtual-key value used for non-keyboard
        //     input methods.
        Packet = 231,
        //
        // Summary:
        //     The ATTN key.
        Attn = 246,
        //
        // Summary:
        //     The CRSEL key.
        Crsel = 247,
        //
        // Summary:
        //     The EXSEL key.
        Exsel = 248,
        //
        // Summary:
        //     The ERASE EOF key.
        EraseEof = 249,
        //
        // Summary:
        //     The PLAY key.
        Play = 250,
        //
        // Summary:
        //     The ZOOM key.
        Zoom = 251,
        //
        // Summary:
        //     A constant reserved for future use.
        NoName = 252,
        //
        // Summary:
        //     The PA1 key.
        Pa1 = 253,
        //
        // Summary:
        //     The CLEAR key.
        OemClear = 254,
        //
        // Summary:
        //     The bitmask to extract a key code from a key value.
        KeyCode = 65535,
        //
        // Summary:
        //     The SHIFT modifier key.
        Shift = 65536,
        //
        // Summary:
        //     The CTRL modifier key.
        Control = 131072,
        //
        // Summary:
        //     The ALT modifier key.
        Alt = 262144,
    }

    // MouseMessageFilter: Handle mouse events
    class MouseMessageFilter : IMessageFilter
    {
        // MouseMove: Capture mouse movement events, but not clicks
        public static event MouseEventHandler MouseMove = delegate { };
        const int WM_MOUSEMOVE = 0x0200;

        /*
         * PreFilterMessage: When the mosue moves, this captures the new position in x,y coordinates. 
         * @param Message m: Message to tell if the mouse moved
         * @return bool: Always false
         */
        public bool PreFilterMessage(ref Message m)
        {
            // If the mouse moved
            if (m.Msg == WM_MOUSEMOVE)
            {
                // Capture the mouse position
                System.Drawing.Point mousePosition = Control.MousePosition;

                // Run the MouseMove function to send the motion vector over serial
                MouseMove(null, new MouseEventArgs(
                    MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));
            } // end if the mouse moved
            return false;
        } // end PreFilterMessage

    } // end class MouseMessageFilter


    /*
     * MouseHook: Handle mouse clicks
     */
    public class MouseHook
    {
        // Events: Mouse left click and right click delegates
        public static event EventHandler MouseAction = delegate { };
        public static event EventHandler MouseClicked = delegate { };
        public static event EventHandler MouseRightClicked = delegate { };

        // Start: Hook into the mouse movement
        public static void Start()
        {
            _hookID = SetHook(_proc);
        } // end Start()

        // stop(): Unhook from the mouse movement
        public static void stop()
        {
            UnhookWindowsHookEx(_hookID);
        } // end stop()

        // Low level mouse hook handlers
        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        /* 
         * SetHook: attach a mouse movement listener to the current process
         * @param LowLevelMouseProc proc: The mouse listener
         * @return IntPtr: The hook
         */
        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            // Attach to the current process
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                  GetModuleHandle(curModule.ModuleName), 0);
            } // end using curProcess
        } // end SetHook

        // LowLevelMouseProc: A mouse movement listener below the application level. 
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        /*
         * HookCallback: Handle mouse events when they happen
         * @param int nCode: Must be positive
         *        IntPtr wParam: The mouse button that was clicked
         *        IntPtr lParam: Not used
         * @return IntPtr: Next hook
         */
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Convert the hook to a structure
            MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

            // If the nCode is positive and the button clicked is left, then run MouseClicked. 
            if (nCode >= 0 && MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
            {
                MouseClicked(null, new EventArgs());
            } else {
                // If the nCode is positive and the button clicked is right, then run MouseRightClicked. 
                if (nCode >= 0 && MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
                {
                    MouseRightClicked(null, new EventArgs());
                }
                else
                {
                    // Otherwise another mouse activitiy occurred
                    MouseAction(null, new EventArgs());
                } // end if right click
            } // end if left click

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        } // end HookCallback

        // Mouse, not keyboard
        private const int WH_MOUSE_LL = 14;

        // Mouse messages: left click down + up, right click down + up, mouse movement, mouse wheel
        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        } // end enum MouseMessages

        [StructLayout(LayoutKind.Sequential)]
        // The point from which we calculate the movement
        public struct POINT
        {
            public int x;
            public int y;
        } // end struct POINT

        [StructLayout(LayoutKind.Sequential)]
        // Hook
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        } // end hook

        // Bind to the mouse events user32 DLL
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
          LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
          IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


    } 

    public static class Globals
    {
        public static volatile int lastXValue;
        public static volatile int lastYValue;
    }


    // Program: Where the actual action happens
    class Program
    {
        // Keyboard hook
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;






        private static Socket KeyboardMouse;

        private int timerx, timery, xDelta, yDelta;
        private char yChar, xChar;
        String xString, yString;


        static string comPort = string.Empty;

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            Program thisProgram = new Program();
            thisProgram.run(args);
        }


        public void run (String[] args)
        {
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            // Make a console window to capture inputs
            KeyboardMouse = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var handle = GetConsoleWindow();
            System.Net.IPAddress server = System.Net.IPAddress.Parse("192.168.43.166");
            try
            {
                KeyboardMouse.Connect(new IPEndPoint(server, 8000));
            }
            catch(SocketException e)
            {
                Console.WriteLine(e.Message);
            }
            KeyboardMouse.NoDelay = true;


            // Watch for mouse events
            MouseHook.Start();
            MouseHook.MouseAction += new EventHandler(mouseMovedResponse);
            MouseHook.MouseClicked += new EventHandler(mouseClickedResponse);
            MouseHook.MouseRightClicked += new EventHandler(mouseRightClickedResponse);
            view view = new view();
            view.Show();

            // Watch for keyboard events
            _hookID = SetHook(_proc);

            // Run the app
            Application.Run();

            // Close cleanly
            UnhookWindowsHookEx(_hookID);



        } // end function run
        int characterOffset = 64;
        //int icr = 0;
        //int tD = 0;
        //double t = 0;
        //int hD = 0;
        //double h = 0;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (xDelta != 0)
            {
                byte[] x = System.Text.Encoding.ASCII.GetBytes(@"x");
                SendToPort(x);
                xDelta += characterOffset;
                if (xDelta < 0)
                    xDelta = 0;
                if (xDelta > 128)
                    xDelta = 128;

                byte[] toSendX = new byte[] { (byte)xDelta };
                SendToPort(toSendX);
                xDelta = 0;
                byte[] terminator = System.Text.Encoding.ASCII.GetBytes(@"?");
                SendToPort(terminator);
            }
            if (yDelta != 0)
            {
                byte[] y = System.Text.Encoding.ASCII.GetBytes(@"y");
                SendToPort(y);
                yDelta += characterOffset;
                if (yDelta < 0)
                    yDelta = 0;
                if (yDelta > 128)
                    yDelta = 128;

                byte[] toSendY = new byte[] { (byte)yDelta };
                SendToPort(toSendY);
                yDelta = 0;
                byte[] terminator = System.Text.Encoding.ASCII.GetBytes(@"?");
                SendToPort(terminator);
            }
        }

        // mouseClickedResponse: Handle a left click
        private void mouseClickedResponse(object sender, EventArgs g)
        {
            // Write "b" (button) character to the serial port
            byte[] start = System.Text.Encoding.ASCII.GetBytes(@"b");
            SendToPort(start);

            // Write "l" (left click) character to the serial port
            byte[] LeftClick = System.Text.Encoding.ASCII.GetBytes(@"l");
            SendToPort(LeftClick);
        } // end mouseClickedResponse

        // mouseRightClickedResponse: Handle a right click
        private void mouseRightClickedResponse(object sender, EventArgs g)
        {
            // Write "b" (button) character to the serial port
            byte[] start = System.Text.Encoding.ASCII.GetBytes(@"b");
            SendToPort(start);

            // Write "r" (right click) character to the serial port
            byte[] RigthtClick = System.Text.Encoding.ASCII.GetBytes(@"r");
            SendToPort(RigthtClick);
        } // end mouseRightClickedResponse


        System.Timers.Timer timer = new System.Timers.Timer(33);


        private void mouseMovedResponse(object sender, EventArgs g)
        {
            int xValue = Cursor.Position.X;
            int yValue = Cursor.Position.Y;
            
            if (Globals.lastXValue == null)
            {
                Globals.lastXValue = Cursor.Position.X;
                Globals.lastYValue = Cursor.Position.Y;
            }
            
            // Compare the current mouse position to the last position
            int xMove = xValue -Globals.lastXValue;
            int yMove = yValue -Globals.lastYValue;

            xDelta += xMove;
            yDelta += yMove;

            //Console.WriteLine($"{xDelta},{yDelta}");
        } // mouseMovedResponse

        // LowLevelKeyboardProc: Keyboard event delegate
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr IParam);

        /*
         * HookCallback: Handle keyboard events
         * @param int nCode: Greater than zero
         *        IntPtr wParam: Is this a keystroke event?
         *        IntPtr IParam: The integer value of the keystroke
         */
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr IParam)
        {
            // If a key was pressed
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                // Find the key code of the pressed key
                int vkCode = Marshal.ReadInt32(IParam);
                //Console.WriteLine((Keys)vkCode+":"+vkCode);

                // Look up the string value of the key code
                String keys = @""+((Keys)vkCode);
                String keys2 = keys;
                byte keystroke = 0;
                // Substitute some key codes: space and return
                switch (vkCode)
                {
                    case 32:
                        keystroke = 32;
                        break;

                    case 13:
                        keystroke = 10;
                        break;
                    case 9:
                        keystroke = 179;
                        break;
                    case 20:
                        keystroke = 193;
                        break;
                    case 160:
                        keystroke = 129;
                        break;
                    case 162:
                        keystroke = 128;
                        break;
                    case 91:
                        keystroke = 131;
                        break;
                    case 92:
                        keystroke = 135;
                        break;
                    case 163:
                        keystroke = 132;
                        break;
                    case 161:
                        keystroke = 133;
                        break;
                    case 8:
                        keystroke = 178;
                        break;
                    case 112:
                        keystroke = 194;
                        break;
                    case 113:
                        keystroke = 195;
                        break;
                    case 114:
                        keystroke = 196;
                        break;
                    case 115:
                        keystroke = 197;
                        break;
                    case 116:
                        keystroke = 198;
                        break;
                    case 117:
                        keystroke = 199;
                        break;
                    case 118:
                        keystroke = 200;
                        break;
                    case 119:
                        keystroke = 201;
                        break;
                    case 120:
                        keystroke = 202;
                        break;
                    case 121:
                        keystroke = 203;
                        break;
                    case 122:
                        keystroke = 204;
                        break;
                    case 123:
                        keystroke = 205;
                        break;
                    case 27:
                        keystroke = 177;
                        break;
                    case 45:
                        keystroke = 209;
                        break;
                    case 36:
                        keystroke = 210;
                        break;
                    case 33:
                        keystroke = 211;
                        break;
                    case 35:
                        keystroke = 213;
                        break;
                    case 46:
                        keystroke = 212;
                        break;
                    case 38:
                        keystroke = 218;
                        break;
                    case 37:
                        keystroke = 216;
                        break;
                    case 40:
                        keystroke = 217;
                        break;
                    case 39:
                        keystroke = 215;
                        break;
                    case 34:
                        keystroke = 214;
                        break;
                    case 192:
                        keystroke = 96;
                        break;
                    case 164:
                        keystroke = 130;
                        break;
                    case 165:
                        keystroke = 134;
                        break;


                    case 48:
                        keystroke = 48;
                        break;
                    case 49:
                        keystroke = 49;
                        break;
                    case 50:
                        keystroke = 50;
                        break;
                    case 51:
                        keystroke = 51;
                        break;
                    case 52:
                        keystroke = 52;
                        break;
                    case 53:
                        keystroke = 53;
                        break;
                    case 54:
                        keystroke = 54;
                        break;
                    case 55:
                        keystroke = 55;
                        break;
                    case 56:
                        keystroke = 56;
                        break;
                    case 57:
                        keystroke = 57;
                        break;
                }
                // Write "k" (key) to the serial port
                byte[] k = System.Text.Encoding.ASCII.GetBytes(@"k");

                SendToPort(k);
                Console.Write(@"k");
                // Write the keystroke to the serial port
                if (keystroke != 0)
                {
                        SendToPort(new byte[] { keystroke });
                }
                else
                {
                        byte[] BitKeys = System.Text.Encoding.ASCII.GetBytes(keys);
                        SendToPort(BitKeys);
                }
            

                        //SendToPort(new byte[] { 0xE0, 0xE1, 0xE2 }, 0, 3, 0);
                if (keystroke!=0)
                    Console.WriteLine(keys + ":"+vkCode+ " byte = "+ keystroke);
                else
                    Console.WriteLine(keys + ":" + vkCode);
                // Also save the keystrokes to a log file
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\log.txt", true);
                sw.WriteLine(vkCode+":"+(Keys)vkCode);
                sw.Close();
            } // end if a key was pressed
            return CallNextHookEx(_hookID, nCode, wParam, IParam);
        } // end function HookCallback

        // SetHook: Attach the keyboard logging hook to this process
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            } // end curModule
        } // end function SetHook

        static void SendToPort (byte[] data)
        {
            try
            {
                KeyboardMouse.Send(data);
            }
            catch (Exception e)
            {


            }
        }

        // Bind to the keyboard events user32 DLL
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc Ipfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr IParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string IpModuleName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;


    } // end class Program
} // end namespace keyMouSerial
