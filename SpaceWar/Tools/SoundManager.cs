using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWar.Tools
{
    internal class SoundManager
    {

        static int[] fq = { 261, 277, 293, 311, 329, 349, 370, 392, 415, 440, 466, 493 };
        static int duration = 250;

        static string song = "fhfhaafhfhaafdsajsajhgff";

        public static void PlayMusic()
        {
            var index = 0;
            var notes = song.ToCharArray();

            while (true)
            {
                switch (notes[index])
                {
                    case 'a':
                        Console.Beep(fq[7], duration);
                        break;
                    case 's':
                        Console.Beep(fq[9], duration);
                        break;
                    case 'd':
                        Console.Beep(fq[11], duration);
                        break;
                    case 'f':
                        Console.Beep(fq[0], duration);
                        break;
                    case 'g':
                        Console.Beep(fq[2], duration);
                        break;
                    case 'h':
                        Console.Beep(fq[4], duration);
                        break;
                    case 'j':
                        Console.Beep(fq[5], duration);
                        break;
                }

                index++;
                if (index == notes.Length)
                    index = 0;
            }
        }
    }
}
