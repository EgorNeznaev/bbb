using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace виселица
{
    public class Hangman
    {
        private static string word;
        private static char[] hiddenWord;
        private static int lives;

        public static string Word
        {
            get { return word; }
        }

        public static char[] HiddenWord
        {
            get { return hiddenWord; }
        }

        public static int Lives
        {
            get { return lives; }
        }

        public static void StartNewGame()
        {
            word = GetRandomWordFromDB();
            hiddenWord = new char[word.Length];
            lives = 6;

            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord[i] = '_';
            }

            PlayGame();

            if (lives > 0)
            {
                UpdateLeaderboard();
            }
        }

        private static void UpdateLeaderboard()
        {
            string connectionString = "Data Source=C:\\Users\\N\\OneDrive\\Desktop\\Новая папка\\виселица\\bin\\test.db; Version=3;";
            string insertSql = "INSERT INTO Leaderboard (Name, WinsInRow) VALUES (@name, @winsInRow)";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("Поздравляем! Введите ваше имя для добавления в список лидеров:");
                string playerName = Console.ReadLine();

                SQLiteCommand insertCommand = new SQLiteCommand(insertSql, connection);
                insertCommand.Parameters.AddWithValue("@name", playerName);
                insertCommand.Parameters.AddWithValue("@winsInRow", HangmanGame.Lives);
                insertCommand.ExecuteNonQuery();

                Console.WriteLine("Результаты добавлены в список лидеров!");
            }
        }

        public static void ContinueGame()
        {
            // Код продолжения игры из сохранения
        }

        private static void PlayGame()
        {
            Output(lives, hiddenWord);

            while (lives > 0 && new string(hiddenWord) != word)
            {
                char letter = char.ToLower(Console.ReadKey().KeyChar);

                Console.Clear();

                bool letterFound = false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (letter == hiddenWord[i])
                    {
                        Console.WriteLine("Вы уже вводили {0}\n", letter);

                        letterFound = true;
                        break;
                    }

                    if (letter == word[i])
                    {
                        hiddenWord[i] = letter;
                        letterFound = true;
                    }
                }

                if (!letterFound)
                {
                    lives--;
                }

                Output(lives, letter, hiddenWord);

                if (lives > 0 && new string(hiddenWord) != word)
                {
                    Console.WriteLine("\n\nНажмите 1, чтобы сохранить игру...");
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        SaveGame.Save(name, word, hiddenWord, lives);
                        Console.WriteLine("Игра сохранена!");
                        Console.WriteLine();
                        Output(name, lives, letter, hiddenWord);
                    }
                }
            }

            if (lives == 0)
            {
                Console.WriteLine("Вы проиграли");
            }
            else
            {
                Console.WriteLine("Вы выиграли");
            }
        }

        static void Output(int lives, char[] hiddenWord)
        {
            Graphics.Hearts(lives);
            Console.WriteLine("Вы ввели: -\n");
            Graphics.Hangman(lives);
            Console.WriteLine("\n{0}", string.Join(" ", hiddenWord));
        }

        static void Output(int lives, char letter, char[] hiddenWord)
        {
            Graphics.Hearts(lives);
            Console.WriteLine("Вы ввели: {0}\n", letter);
            Graphics.Hangman(lives);
            Console.WriteLine("\n{0}", string.Join(" ", hiddenWord));
        }

        static string GetRandomWordFromDB()
        {
            string connectionString = "Data Source=C:\\Users\\N\\OneDrive\\Desktop\\Новая папка\\виселица\\bin\\test.db; Version=3;";
            List<string> words = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectSql = "SELECT slovo FROM words";
                SQLiteCommand selectCommand = new SQLiteCommand(selectSql, connection);

                using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        words.Add(reader.GetString(0));
                    }
                }
            }

            Random random = new Random();
            int index = random.Next(words.Count);

            return words[index];
        }
    }
}
