using System;

namespace SpaceWar.Tools
{
    internal class Utility
    {
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
    }

}