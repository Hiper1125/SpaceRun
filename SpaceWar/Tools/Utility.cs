using System;
using System.Text;
using System.Diagnostics;

namespace SpaceWar.Tools
{
    internal class Utility
    {
        // Clear this if window size changes
        protected static string clearBuffer = null;

        public static void Write(string text, bool center = false)
        {
            if (center)
            {
                int centerX = (Console.WindowWidth / 2) - (text.Length / 2);
                Console.SetCursorPosition(centerX, Console.CursorTop);
            }

            Console.Write(text);
            Console.ResetColor();
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
                
            }
        }

        //draw a square at the screen border function without overlapping previous content
        public static void DrawSquare(int x, int y, int width, int height, ConsoleColor color)
        {
            SetColors(color);
            Console.SetCursorPosition(x, y);
            Console.WriteLine(new string('*', width));
            Console.SetCursorPosition(x, y + height - 1);
            Console.WriteLine(new string('*', width));
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("*");
                Console.SetCursorPosition(x + width - 1, y + i);
                Console.Write("*");
            }
            Console.ResetColor();
        }


        private static void Sleep(double durationSeconds)
        {
            var durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < durationTicks)
            {

            }
        } 
    }
}