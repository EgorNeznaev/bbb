using System;

namespace ConsoleApp1 { 
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("ВИСЕЛИЦА\n\n1 - начать новую игру\n2 - список лидеров\n3 - сохранить игру\n4 - продолжить игру\n5 - выход");

                int menuChoice = int.Parse(Console.ReadLine());

                Console.Clear();

                switch (menuChoice)
                {
                    case 1:
                        Hangman.StartNewGame();
                        break;
                    case 2:
                        Leaderboard.ShowLeaderboard();
                        break;
                    case 3:
                        SaveGame.Save();
                        break;
                    case 4:
                        Hangman.ContinueGame();
                        break;
                    case 5:
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }

                Console.WriteLine("\n\nНажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}