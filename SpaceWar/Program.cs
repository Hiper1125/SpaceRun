using SpaceWar.Pages;
using SpaceWar.Tools;
using System;
using System.Threading;

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


            //create a thread and play music
            Thread musicThread = new Thread(() =>
            {
                Utility.Sleep(1.3);
                SoundManager.PlayMusic();
            });
            musicThread.Start();


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

        private static void SetupConsole(int width = 100, int height = 41)
        {
            //set console width and height
            Console.SetWindowSize(width, height);

            //lock console size
            Console.SetBufferSize(width, height);

            //set console title
            Console.Title = "Space Wars";

            Console.CursorVisible = false;

            Utility.SetColors(ConsoleColor.Yellow, ConsoleColor.Black);
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

            Utility.Clear(true);

            Menu.Draw(35, selectedIndex);

            while (isInMenu)
            { 
                if (Console.KeyAvailable)
                {
                    Utility.Clear();

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
                    Console.Beep();
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

            Utility.Clear(true);

            switch (selectedIndex)
            {
                case 0: isInSubMenu = false; isInGame = true; break;
                case 1: Menu.DrawSub(selectedIndex, 0, selectedSubIndex); break;
                case 2: isInSubMenu = false; break;
            }

            while (isInSubMenu)
            {
                if (Console.KeyAvailable)
                {
                    Utility.Clear();

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
                        difficulty = selectedSubIndex + 1;
                    }

                    Menu.DrawSub(selectedIndex, 0, selectedSubIndex);
                    Console.Beep();
                }
            }
        }

        private static void GameLoop()
        {
            Menu.DrawSub(3, 0, selectedSubIndex);

            Console.ReadKey();

            Utility.Clear();

            var game = new Pages.Game(difficulty);

            Console.ReadKey();
        }
    }
}
