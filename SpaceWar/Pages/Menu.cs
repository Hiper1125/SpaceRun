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
      ___                       ___           ___                       ___           ___           ___       ___           ___     
     /\  \          ___        /\  \         /\  \          ___        /\  \         /\__\         /\__\     /\  \         |\__\    
    /::\  \        /\  \      /::\  \       /::\  \        /\  \      /::\  \       /:/  /        /:/  /     \:\  \        |:|  |   
   /:/\:\  \       \:\  \    /:/\:\  \     /:/\:\  \       \:\  \    /:/\:\  \     /:/  /        /:/  /       \:\  \       |:|  |   
  /:/  \:\__\      /::\__\  /::\~\:\  \   /::\~\:\  \      /::\__\  /:/  \:\  \   /:/  /  ___   /:/  /        /::\  \      |:|__|__ 
 /:/__/ \:|__|  __/:/\/__/ /:/\:\ \:\__\ /:/\:\ \:\__\  __/:/\/__/ /:/__/ \:\__\ /:/__/  /\__\ /:/__/        /:/\:\__\     /::::\__\
 \:\  \ /:/  / /\/:/  /    \/__\:\ \/__/ \/__\:\ \/__/ /\/:/  /    \:\  \  \/__/ \:\  \ /:/  / \:\  \       /:/  \/__/    /:/~~/~   
  \:\  /:/  /  \::/__/          \:\__\        \:\__\   \::/__/      \:\  \        \:\  /:/  /   \:\  \     /:/  /        /:/  /     
   \:\/:/  /    \:\__\           \/__/         \/__/    \:\__\       \:\  \        \:\/:/  /     \:\  \    \/__/         \/__/      
    \::/__/      \/__/                                   \/__/        \:\__\        \::/  /       \:\__\                            
     ~~                                                                \/__/         \/__/         \/__/                        ", "" };

        public static void Draw(int speed = 15, int selectedIndex = 0)
        {
            //split the game name into lines
            string[] gameNameLines = gameName.Split('\n');

            Utility.SkipLines(4);

            //draw each line and wait 1 second
            foreach (string line in gameNameLines)
            {
                Utility.WriteLine(line, true);
                System.Threading.Thread.Sleep(speed);
            }

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
            //split the game name into lines
            string[] menuNameLines = menuNames[index].Split('\n');

            //draw each line and wait 1 second
            foreach (string line in menuNameLines)
            {
                Utility.WriteLine(line, true);
                System.Threading.Thread.Sleep(speed);
            }

            Utility.SkipLines(menuNameLines.Length);

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Utility.InvertColors();
                }

                Utility.WriteLine(menuItems[i], true);
                Console.ResetColor();
                Utility.SkipLines(2);
            }

            Utility.SkipLines(menuNameLines.Length);
        }
    }
}
