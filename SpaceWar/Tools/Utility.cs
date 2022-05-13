using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SpaceWar.Tools
{
    internal class Utility
    {
        // Clear this if window size changes
        protected static string clearBuffer = null;

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;//resize

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public static void DisableResize()
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
        }

        public static void Write(string text, bool center = false)
        {
            if (center)
            {
                int centerX = (Console.WindowWidth / 2) - (text.Length / 2);
                Console.SetCursorPosition(centerX, Console.CursorTop);
            }

            Console.Write(text);
            Utility.ResetColors();
        }

        public static void WriteLine(string text, bool center = false)
        {
            Write(text, center);
            Console.WriteLine();
        }

        public static void SkipLines(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine();
            }
        }

        public static void InvertColors()
        {
            var fontColor = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = fontColor;
        }

        public static void SetColors(ConsoleColor fontColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = fontColor;
            Console.BackgroundColor = backgroundColor;
        }

        public static void Clear(bool animate = false)
        {
            if (clearBuffer == null)
            {
                var line = "".PadLeft(Console.WindowWidth, ' ');
                var lines = new StringBuilder();

                for (var i = 0; i < Console.WindowHeight; i++)
                {
                    lines.AppendLine(line);
                }

                clearBuffer = lines.ToString();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(clearBuffer);
            Console.SetCursorPosition(0, 0);

            if (animate)
            {
                ResetColors();
            }
        }

        public static void Sleep(double durationSeconds)
        {
            var durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < durationTicks)
            {

            }
        }

        public static void ResetColors(ConsoleColor fontColor = ConsoleColor.Yellow, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            SetColors(fontColor, backgroundColor);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadConsoleOutputCharacter(
            IntPtr hConsoleOutput,
            [Out] StringBuilder lpCharacter,
            uint length,
            COORD bufferCoord,
            out uint lpNumberOfCharactersRead);

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        public static char ReadAt(int x, int y)
        {
            IntPtr consoleHandle = GetStdHandle(-11);
            if (consoleHandle == IntPtr.Zero)
            {
                return '\0';
            }
            COORD position = new COORD
            {
                X = (short)x,
                Y = (short)y
            };
            StringBuilder result = new StringBuilder(1);
            uint read = 0;
            if (ReadConsoleOutputCharacter(consoleHandle, result, 1, position, out read))
            {
                return result[0];
            }
            else
            {
                return '\0';
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;

        private static Size GetScreenSize() => new Size(GetSystemMetrics(0), GetSystemMetrics(1));

        private struct Size
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public Size(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }

        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(HandleRef hWnd, out Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        private static Size GetWindowSize(IntPtr window)
        {
            if (!GetWindowRect(new HandleRef(null, window), out Rect rect))
                throw new Exception("Unable to get window rect!");

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            return new Size(width, height);
        }

        public static void MoveWindowToCenter()
        {
            IntPtr window = GetConsoleWindow();

            if (window == IntPtr.Zero)
                throw new Exception("Couldn't find a window to center!");

            Size screenSize = GetScreenSize();
            Size windowSize = GetWindowSize(window);

            int x = (screenSize.Width - windowSize.Width) / 2;
            int y = (screenSize.Height - windowSize.Height) / 2;

            SetWindowPos(window, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
        }
    }
}