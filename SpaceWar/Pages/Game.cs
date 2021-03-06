using SpaceWar.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpaceWar.Pages
{
    internal class Meteor
    {
        private string meteor = @"-O-";

        public Meteor()
        {
            Thread moveThread = null;

            Random rnd = new Random();

            int xPos = rnd.Next(0, Console.WindowWidth - meteor.Length);
            int height = 0;

            moveThread = new Thread(() =>
                {
                    while (height < Console.WindowHeight + 1 && Program.isInGame)
                    {
                        if (height > 0)
                        {
                            Console.SetCursorPosition(xPos, height - 1);
                            Console.Write("   ");
                        }

                        if (height < Console.WindowHeight)
                        {
                            //if meteor touch the spacecraft then game over

                            var overwrite = Utility.ReadAt(xPos, height);

                            if (overwrite.ToString() == " " || int.TryParse(overwrite.ToString(), out int number) == true)
                            {
                                Console.SetCursorPosition(xPos, height);
                                Console.Write(meteor);

                                System.Threading.Thread.Sleep(100);
                                height++;
                            }
                            else
                            {
                                Program.GameOver();   
                            }
                        }

                        System.Threading.Thread.Sleep(100);
                    }
                    
                    moveThread.Interrupt();

                    try
                    {
                        moveThread.Join();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                });
            moveThread.IsBackground = true;
            moveThread.Start();
        }
    }

    internal class Game
    {
        private static int padding = 48;
        private static int prevPadding = 48;
        private static int[] linePadding = { 0, 2, 1, 1, 0, 0, 0 };

        private static int score = 0;

        private string earth = @"
                                                b____r
                                            b.-'""gp 8o""`-.r
                                         b.-'g8888P'Y.`Y[d ' b`-.r
                                       b,']g88888b.J8oo_d      b'`.r
                                     b,'d g,88888888888[""d        bY`.r
                                    b/d   g8888888888P Y8\d          b\r 
                                   b/d    gY8888888P'd             b]88\r
                                  b:d     g`Y88'   Pd              b`888:r
                                  b:d       gY8.oP '- >d            bY88:r
                                  b|d          g`Yb __d             b`'|r
                                  b:d            g`'d8888bo.d          b:r
                                  b:d             gd88888888ooo.d      b;r
                                   b\d            gY88888888888Pd     b/r
                                    b\d            g`Y88888888Pd     b/r
                                     b`.d            gd88888P'd    b,'r
                                       b`.d          g888PP'd    b,'r
                                         b`-.d      gd8P'd    b,-'r   
                                            b`-.,,_'__,,.-'r";

        private string spacecraft = @"
                                                  .   
                                                 .'.
                                                 |o|
                                                .'o'.
                                                |.-.|
                                                '   '";


        public Game(int difficulty)
        {
            score = 0;
            
            for (int i = 0; i < 25 / 2; i++)
            {
                Utility.SkipLines(10);
                Console.WriteLine(spacecraft);

                //foreach char in earth, if char is b set color to blue skip it, if char is g set color to green skip it, if is d set background to dark blue skip it, else write
                foreach (char c in earth)
                {
                    if (c == 'b')
                    {
                        Utility.SetColors(ConsoleColor.Blue, ConsoleColor.Black);
                    }
                    else if (c == 'g')
                    {
                        Utility.SetColors(ConsoleColor.Green, ConsoleColor.Black);
                    }
                    else if (c == 'd')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (c == 'r')
                    {
                        Utility.ResetColors();
                    }
                    else
                    {
                        Console.Write(c.ToString());
                    }
                }

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

                System.Threading.Thread.Sleep(100);
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

            earth = null;

            //Create a thread that spawns meteors based on difficulty
            Thread meteorThread = null;

            meteorThread = new Thread(() =>
            {
                Random rnd = new Random();

                while (Program.isInGame)
                {
                    new Meteor();

                    switch (difficulty)
                    {
                        case 1: Utility.Sleep(2 + rnd.Next(1, 3)); break;
                        case 2: Utility.Sleep(2 + rnd.Next(1, 2)); break;
                        case 3: Utility.Sleep(3 + rnd.NextDouble()); break;
                        default: Utility.Sleep(rnd.Next(1, 3)); break;
                    }
                }

                meteorThread.Interrupt();

                try
                {
                    meteorThread.Join();
                }
                catch
                {
                    //Do nothing
                }
            });
            meteorThread.IsBackground = true;
            meteorThread.Start();


            Thread spacecraftMovements = null;

            spacecraftMovements = new Thread(() =>
            {
                while (Program.isInGame)
                {
                        Utility.Clear();
                        Utility.SkipLines(30);

                        string[] spaceCraftLines = spacecraft.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                        int i = 0;

                        foreach (var line in spaceCraftLines)
                        {
                            string spaces = "";

                            for (int j = 0; j < Math.Abs(padding + linePadding[i]); j++)
                            {
                                spaces += " ";
                            }

                            Console.SetCursorPosition(0, 30 + i);
                            string newLine = line.Trim();
                            newLine = newLine.Insert(0, spaces);
                            Console.WriteLine(newLine);

                            i++;
                        }

                        prevPadding = padding;

                    System.Threading.Thread.Sleep(100);
                }

                spacecraftMovements.Interrupt();

                try
                {
                    spacecraftMovements.Join();
                }
                catch
                {
                    //Do nothing
                }
            });
            spacecraftMovements.IsBackground = true;
            spacecraftMovements.Start();

            //create a thred to increase the score every second
            Thread scoreThread = null;

            scoreThread = new Thread(() =>
            {
                while (Program.isInGame)
                {
                    Utility.Sleep(0.08);
                    score++;
                    Console.Title = "Space Run: " + score;

                    Console.SetCursorPosition(0, 2);

                    string spaces = "";

                    for (int i = 0; i < Console.WindowWidth; i++)
                    {
                        spaces += " ";
                    }

                    Console.Write(spaces);

                    //draw the score on top of the screen at the middle using setcursor
                    Console.SetCursorPosition(Console.WindowWidth / 2 - score.ToString().Length / 2, 2);
                    Console.Write(score);
                }

                scoreThread.Interrupt();
                
                try
                {
                    spacecraftMovements.Join();
                }
                catch
                {
                    //Do nothing
                }
            });
            scoreThread.IsBackground = true;
            scoreThread.Start();
        }

        public void CheckControls()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    if (padding < (48 * 2) - 1)
                    {
                        padding++;
                    }
                }
                else if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    if (padding > 2)
                    {
                        padding--;
                    }
                }
            }
        }
    }
}