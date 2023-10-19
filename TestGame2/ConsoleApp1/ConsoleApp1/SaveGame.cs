using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsoleApp1
{
    internal class SaveGame
    {
        public static void Save()
        {
            Console.WriteLine("Введите имя для сохранения:");

            string saveName = Console.ReadLine();
            string connectionString = "Data Source=C:\\Users\\N\\OneDrive\\Desktop\\Новая папка\\виселица\\bin\\test.db; Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string checkSql = "SELECT COUNT(*) FROM saves WHERE name = @name";
                SQLiteCommand checkCommand = new SQLiteCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@name", saveName);

                int existingSaves = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingSaves > 0)
                {
                    Console.WriteLine("Имя уже существует. Попробуйте другое имя для сохранения игры.");
                    return;
                }

                string insertSql = "INSERT INTO saves (name, word, hidden_word, lives) VALUES (@name, @word, @hiddenWord, @lives)";
                SQLiteCommand command = new SQLiteCommand(insertSql, connection);
                command.Parameters.AddWithValue("@name", saveName);
                command.Parameters.AddWithValue("@word", Hangman.Word);
                command.Parameters.AddWithValue("@hiddenWord", new string(HangmanGame.HiddenWord));
                command.Parameters.AddWithValue("@lives", HangmanGame.Lives);
                command.ExecuteNonQuery();
                Console.WriteLine("Игра успешно сохранена!");
            }
        }
    }
}
