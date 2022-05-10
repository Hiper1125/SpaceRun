using System;
using SpaceWar.Tools;

namespace SpaceWar.Pages
{
    internal class Menu
    {
        static string gameName = @"      
      ___           ___           ___           ___           ___                    ___           ___           ___     
     /\  \         /\  \         /\  \         /\  \         /\  \                  /\__\         /\  \         /\  \    
    /::\  \       /::\  \       /::\  \       /::\  \       /::\  \                /:/ _/_       /::\  \       /::\  \   
   /:/\ \  \     /:/\:\  \     /:/\:\  \     /:/\:\  \     /:/\:\  \              /:/ /\__\     /:/\:\  \     /:/\:\  \  
  _\:\~\ \  \   /::\~\:\  \   /::\~\:\  \   /:/  \:\  \   /::\~\:\  \            /:/ /:/ _/_   /::\~\:\  \   /::\~\:\  \ 
 /\ \:\ \ \__\ /:/\:\ \:\__\ /:/\:\ \:\__\ /:/__/ \:\__\ /:/\:\ \:\__\          /:/_/:/ /\__\ /:/\:\ \:\__\ /:/\:\ \:\__\
 \:\ \:\ \/__/ \/__\:\/:/  / \/__\:\/:/  / \:\  \  \/__/ \:\~\:\ \/__/          \:\/:/ /:/  / \/__\:\/:/  / \/_|::\/:/  /
  \:\ \:\__\        \::/  /       \::/  /   \:\  \        \:\ \:\__\             \::/_/:/  /       \::/  /     |:|::/  / 
   \:\/:/  /         \/__/        /:/  /     \:\  \        \:\ \/__/              \:\/:/  /        /:/  /      |:|\/__/  
    \::/  /                      /:/  /       \:\__\        \:\__\                 \::/  /        /:/  /       |:|  |    
   \/__/                       \/__/         \/__/         \/__/                  \/__/         \/__/         \|__|   ";

        static string[] menuItems = { "Play game", "Set difficulty", "Quit" };

        static string[] menuNames = { "", @"
  _____  _  __  __ _            _ _         
 |  __ \(_)/ _|/ _(_)          | | |        
 | |  | |_| |_| |_ _  ___ _   _| | |_ _   _ 
 | |  | | |  _|  _| |/ __| | | | | __| | | |
 | |__| | | | | | | | (__| |_| | | |_| |_| |
 |_____/|_|_| |_| |_|\___|\__,_|_|\__|\__, |
                                       __/ |
                                      |___/ ", "" };
        static string[] subMenuItems = { "Easy", "Normal", "Hard" };

        public static void Draw(int speed = 15, int selectedIndex = 0)
        {
            System.Threading.Thread.Sleep(speed);

            //split the game name into lines
            string[] gameNameLines = gameName.Split('\n');

            Utility.SkipLines(4);

            //draw each line and wait 1 second
            foreach (string line in gameNameLines)
            {
                Utility.SetColors(ConsoleColor.Yellow, ConsoleColor.Black);
                Utility.WriteLine(line, true);
                System.Threading.Thread.Sleep(speed);
            }
            
            Utility.SetColors(ConsoleColor.White, ConsoleColor.Black);

            Utility.SkipLines(gameNameLines.Length);
            
            for(int i = 0; i < menuItems.Length; i++)
            {
                if(i == selectedIndex)
                {
                    Utility.InvertColors();
                }

                Utility.WriteLine(menuItems[i], true);
                Console.ResetColor();
                Utility.SkipLines(2);
            }

            Utility.SkipLines(gameNameLines.Length);

        }

        public static void DrawSub(int index = 0, int speed = 15, int selectedIndex = 0)
        {
            System.Threading.Thread.Sleep(speed);

            //split the game name into lines
            string[] menuNameLines = menuNames[index].Split('\n');

            Utility.SkipLines(menuNameLines.Length * 2);

            //draw each line and wait 1 second
            foreach (string line in menuNameLines)
            {
                Utility.SetColors(ConsoleColor.DarkYellow, ConsoleColor.Black);
                Utility.WriteLine(line, true);
                System.Threading.Thread.Sleep(speed);
            }

            Utility.SkipLines(menuNameLines.Length);

            for (int i = 0; i < subMenuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Utility.InvertColors();
                }

                Utility.WriteLine(subMenuItems[i], true);
                Console.ResetColor();
                Utility.SkipLines(2);
            }

            Utility.SkipLines(menuNameLines.Length);
        }
    }
}
