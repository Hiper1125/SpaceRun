using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWar.Pages
{
    internal class Game
    {
        private static string character =
            @"";

        private string[,] GameScreen;


        public Game(int difficulty)
        {
            GameScreen = new string[Console.WindowWidth, Console.WindowHeight];
        }

            public void Draw()
        {
            //draw game screen
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                for (int j = 0; j < Console.WindowHeight; j++)
                {
                    GameScreen[i, j] = " ";
                }
            }
        }
    }
}
