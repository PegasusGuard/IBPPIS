using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika9
{
    class Program
    {
        static string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ,.?! ";
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = Encoding.GetEncoding(1251);

            string choice = null;
            while (choice != "5")
            {
                Console.WriteLine("Выберите действие:\n" +
                                  "1. Сгенерировать ключи\n" +
                                  "2. Зашифровать сообщение\n" +
                                  "3. Расшифровать сообщение\n" +
                                  "4. Очистить Ключи\n" +
                                  "5. Завершить работу");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        genKey();
                        Console.WriteLine("Ключи сгенерированы!");
                        break;
                    case "2":
                        Console.WriteLine("Введите сообщение: ");
                        string messageEn = Console.ReadLine();
                        if (!File.Exists("encodeKeys.txt") || (new FileInfo("encodeKeys.txt").Length == 0))
                        {
                            genKey();
                            Console.WriteLine("Не хватало ключей, поэтому было сгенерировано еще несколько ключей");
                        }

                        Console.WriteLine("Зашифрованное сообщение: " + Encode(messageEn));
                        break;
                    case "3":
                        Console.WriteLine("Введите сообщение: ");
                        string messageDe = Console.ReadLine();
                        if (!File.Exists("decodeKeys.txt") || (new FileInfo("decodeKeys.txt").Length == 0))
                        {
                            genKey();
                            Console.WriteLine("Не хватало ключей, поэтому было сгенерировано еще несколько ключей");
                        }

                        Console.WriteLine("Расшифрованное сообщение: " + Decode(messageDe));
                        break;
                    case "4":
                        File.Delete("encodeKeys.txt");
                        File.Delete("decodeKeys.txt");
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Неверный ввод, попробуйте снова");
                        break;
                }
            }
            string testing = Console.ReadLine();
            string temp = Encode(testing);
            Console.WriteLine(temp);
            Console.WriteLine(Decode(temp));
            Console.ReadKey();
        }

        static void genKey()
        {
            string newKey = null;
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                newKey += rnd.Next(0, alphabet.Length);
                newKey += ",";
            }
            using (StreamWriter encodeStream = new StreamWriter("encodeKeys.txt", true))
                encodeStream.WriteLineAsync(newKey);
            using (StreamWriter decodeStream = new StreamWriter("decodeKeys.txt", true))
                decodeStream.WriteLineAsync(newKey);
        }

        static string Encode(string message)
        {
            string result = null;
            string keys;
            message = message.ToUpper();
            using (StreamReader sr = new StreamReader("encodeKeys.txt"))
                keys = sr.ReadLine();
            if (keys != null)
                for (int i = 0; i < (message.Length < 100 ? message.Length : 100); i++)
                {
                    int nextKey = int.Parse(keys.Substring(0, keys.IndexOf(',')));
                    keys = keys.Remove(0, keys.IndexOf(',') + 1);
                    if (alphabet.Contains(message[i]))
                        result += alphabet[(alphabet.IndexOf(message[i]) + nextKey) % alphabet.Length];
                    else
                        return "Ошибка. Символ " + message[i] + " не определен в алфавите";
                }
            else
                return "Файл не содержит валидных ключей";
            File.WriteAllLines("encodeKeys.txt", File.ReadAllLines("encodeKeys.txt").Skip(1));
            return result;
        }
        static string Decode(string message)
        {
            string result = null;
            string keys;
            message = message.ToUpper();
            using (StreamReader sr = new StreamReader("decodeKeys.txt"))
                keys = sr.ReadLine();
            if (keys != null)
                for (int i = 0; i < (message.Length < 100 ? message.Length : 100); i++)
                {
                    int nextKey = int.Parse(keys.Substring(0, keys.IndexOf(',')));
                    keys = keys.Remove(0, keys.IndexOf(',') + 1);
                    if (alphabet.Contains(message[i]))
                        result += alphabet[Math.Abs(alphabet.IndexOf(message[i]) + alphabet.Length - nextKey) % alphabet.Length];
                    else
                        return "Ошибка. Символ " + message[i] + " не определен в алфавите";
                }
            else
                return "Файл не содержит валидных ключей";
            File.WriteAllLines("decodeKeys.txt", File.ReadAllLines("decodeKeys.txt").Skip(1));
            return result;
        }
    }
}
