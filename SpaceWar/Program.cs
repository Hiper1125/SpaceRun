using System;
using SpaceWar.Pages;

namespace SpaceWar
{
    internal class Program
    {
        private static int selectedIndex = 0;
        private static int selectedSubIndex = 0;
        private static int difficulty = 1;

        private static bool isInMenu = true;
        private static bool isInSubMenu = false;
        private static bool isInGame = false;


        static void Main(string[] args)
        {
            SetupConsole();

            while (true)
            {
                if (isInMenu)
                {
                    MenuLoop();
                }
                else if (isInSubMenu)
                {
                    SubMenuLoop();
                }
                else if (isInGame)
                {
                    GameLoop();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void SetupConsole(int width = 123, int height = 50)
        {
            //set console width and height
            Console.SetWindowSize(width, height);

            //set console title
            Console.Title = "Space War";

        }

        private static void MenuLoop(bool hardReset = false)
        {
            if (hardReset)
            {
                isInMenu = true;
                isInSubMenu = false;
                isInGame = false;
                selectedIndex = 0;
            }

            Menu.Draw(35, selectedIndex);

            while (isInMenu)
            { 
                if (Console.KeyAvailable)
                {
                    Console.Clear();

                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex < 0)
                        {
                            selectedIndex = 2;
                        }
                    }

                    if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex > 2)
                        {
                            selectedIndex = 0;
                        }
                    }

                    if (key == ConsoleKey.Enter)
                    {
                        isInSubMenu = true;
                        isInMenu = false;
                    }

                    Menu.Draw(0, selectedIndex);
                }
            }
        }

        private static void SubMenuLoop(bool hardReset = false)
        {
            if (hardReset)
            {
                isInMenu = false;
                isInSubMenu = true;
                isInGame = false;
                selectedIndex = 0;
            }

            Console.Clear();

            switch (selectedIndex)
            {
                case 0: isInSubMenu = false; isInGame = true; break;
                case 1: Menu.DrawSub(selectedSubIndex, 0, selectedSubIndex); break;
                case 2: isInSubMenu = false; break;
            }

            while (isInSubMenu)
            {
                if (Console.KeyAvailable)
                {
                    Console.Clear();
                    
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedSubIndex--;
                        if (selectedSubIndex < 0)
                        {
                            selectedSubIndex = 2;
                        }
                    }

                    if (key == ConsoleKey.DownArrow)
                    {
                        selectedSubIndex++;
                        if (selectedSubIndex > 2)
                        {
                            selectedSubIndex = 0;
                        }
                    }

                    if (key == ConsoleKey.Enter)
                    {
                        isInMenu = true;
                        isInSubMenu = false;
                    }

                    Console.Clear();

                    Menu.DrawSub(selectedIndex, 0, selectedSubIndex); break;
                }

                Menu.DrawSub(selectedIndex, 35, selectedSubIndex); break;
            }
        }

        private static void GameLoop()
        {

        }
    }
}
