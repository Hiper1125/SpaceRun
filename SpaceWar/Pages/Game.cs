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
        private Thread moveThread = null;
        private int height = 0;
        private int xPos = 0;

        public Meteor()
        {
            Random rnd = new Random();

            xPos = rnd.Next(0, Console.WindowWidth - meteor.Length);


            moveThread = new Thread(() =>
            {
                while (height < Console.WindowHeight + 1)
                {
                    if(height > 0)
                    {
                        Console.SetCursorPosition(xPos, height - 1);
                        Console.Write("\b    ");
                    }

                    if(height < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(xPos, height);
                        Console.Write(meteor);

                        Utility.Sleep(0.1);
                        height++;
                    }
                }
            });
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

            //Create a thread that spawns meteors based on difficulty on top of the screen, each meteor then keep moving down by a certain speed


            Thread spacecraftMovements = new Thread(() =>
            {
                while (true)
                {
                    if (prevPadding != padding)
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

                            string newLine = line.Trim();
                            newLine = newLine.Insert(0, spaces);
                            Console.WriteLine(newLine);

                            i++;
                        }

                        prevPadding = padding;
                    }
                }
            });
            spacecraftMovements.Start();

            //create a thred to increase the score every second
            Thread scoreThread = new Thread(() =>
            {
                while (true)
                {
                    Utility.Sleep(0.1);
                    score++;
                    Console.Title = "Space Run: " + score;

                    //draw the score on top of the screen at the middle using setcursor
                    Console.SetCursorPosition(Console.WindowWidth / 2 - score.ToString().Length / 2, 2);
                    Console.Write(score);
                }
            });
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
