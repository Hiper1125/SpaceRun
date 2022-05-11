using SpaceWar.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWar.Pages
{
    internal class Game
    {
        private static int padding = 0;
        private static int[] linePadding = { 2, 1, 1, 0, 0, 0 };

        private string earth =  @"
                                                ____
                                            .-'""p 8o""`-.
                                         .-'8888P'Y.`Y[ ' `-.
                                       ,']88888b.J8oo_      '`.
                                     ,' ,88888888888[""        Y`.
                                    /   8888888888P Y8\
                                   /    Y8888888P'             ]88\
                                  :     `Y88'   P              `888:
                                  :       Y8.oP '- >            Y88:
                                  |          `Yb __             `'|
                                  :            `'d8888bo.          :
                                  :             d88888888ooo.      ;
                                   \            Y88888888888P     /
                                    \            `Y88888888P     /
                                     `.            d88888P'    ,'
                                       `.          888PP'    ,'
                                         `-.      d8P'    ,-'   
                                            `-.,,_'__,,.-'";

        private string spacecraft = @"
                                                  .   
                                                 .'.
                                                 |o|
                                                .'o'.
                                                |.-.|
                                                '   '";


        public Game(int difficulty)
        {
            for (int i = 0; i < 25 / 2; i++)
            {
                Utility.SkipLines(10);
                Utility.WriteLine(spacecraft);
                Utility.WriteLine(earth);
                earth = earth.Insert(0, "\n ");

                if (i > 3 && !string.IsNullOrEmpty(earth))
                {
                    try
                    {
                        earth = earth.Substring(0, earth.Trim().LastIndexOf(Environment.NewLine));
                    }
                    catch
                    {
                        earth = "";
                    }
                }
                
                Utility.Sleep(0.1);
                Utility.Clear();
            }

            for (int i = 10; i < 30; i++)
            {
                Utility.SkipLines(i);
                Utility.WriteLine(spacecraft);
                Utility.Sleep(0.09);
                Utility.Clear();
            }

            Utility.SkipLines(30);
            Utility.WriteLine(spacecraft);

            //trim each space craft line and add the normal padding, 
            //then based on the current padding (calculated by the pressed keys) 
            //add or remove spaces from each line, so that you generate new lines
            //wich then you can draw in the correct position (that every frame)       
        }
    }
}
