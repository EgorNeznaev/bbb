﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Graphics
    {
        public static int Hangman(int lives)
        {
            char[,] hangman =
                    {
                    { ' ' , '*' , '-' , '-' , '-' , '-' , '*' , ' ' , ' ' },
                    { ' ' , '|' , ' ' , ' ' , ' ' , ' ' , '|' , ' ' , ' ' },
                    { ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , '|' , ' ' , ' ' },
                    { ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , '|' , ' ' , ' ' },
                    { ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , '|' , ' ' , ' ' },
                    { ' ' , ' ' , ' ' , ' ' , '=' , '=' , '=' , '=' , '=' }
                    };

            switch (lives)
            {
                case 6:
                    break;

                case 5:
                    hangman[2, 1] = 'O';
                    break;

                case 4:
                    hangman[2, 1] = 'O';
                    hangman[3, 1] = '|';
                    break;

                case 3:
                    hangman[2, 1] = 'O';
                    hangman[3, 1] = '|';
                    hangman[3, 0] = '/';
                    break;

                case 2:
                    hangman[2, 1] = 'O';
                    hangman[3, 1] = '|';
                    hangman[3, 0] = '/';
                    hangman[3, 2] = '\\';
                    break;

                case 1:
                    hangman[2, 1] = 'O';
                    hangman[3, 1] = '|';
                    hangman[3, 0] = '/';
                    hangman[3, 2] = '\\';
                    hangman[4, 0] = '/';
                    break;

                case 0:
                    hangman[2, 1] = 'O';
                    hangman[3, 1] = '|';
                    hangman[3, 0] = '/';
                    hangman[3, 2] = '\\';
                    hangman[4, 0] = '/';
                    hangman[4, 2] = '\\';
                    break;
            }

            for (int i = 0; i < hangman.GetLength(0); i++)
            {
                Console.Write("\t\t\t\t");
                for (int j = 0; j < hangman.GetLength(1); j++)
                {
                    Console.Write(hangman[i, j]);
                }
                Console.WriteLine();
            }
            return 0;
        }

        public static int Hearts(int lives)
        {
            for (int i = lives; i > 0; i--)
            {
                Console.Write('\u2665' + " ");
            }
            Console.WriteLine();
            return 0;
        }
    }
}
