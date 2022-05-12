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
    }
}