using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace praktika7
{
    class Program
    {
        static bool[,] grid = new bool[,] {{false, true, false, false, false, false, false, false, false, false },
                                           {true, false, false, false, true, false, true, true, false, false },
                                           {false, true, false, false, false, true, false, false, false, true },
                                           {false, false, false, true, false, false, false, true, false, false },
                                           {false, true, false, false , false, false, false, false, false, false },
                                           {false, false, true, false, false, true, true, false, false, true } };

        static void PrintGrid()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                    Console.Write((grid[i, j] ? " " : "█"));
                Console.WriteLine();
            }
        }

        static void displayProgress(char[,] matrix)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                    Console.Write(matrix[i, j] == 0 ? ' ' : matrix[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static string Encode(string text)
        {
            int counter = 0;
            char[,] matrix = new char[grid.GetLength(0), grid.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (grid[i,j])
                    {
                        matrix[i, j] = counter >= text.Length ?
                                       '\0' : text[counter];
                        
                        counter++;
                    }
            displayProgress(matrix);
            Console.WriteLine(text[counter]);
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
                for (int j = matrix.GetLength(1) - 1; j >=0; j--)
                    if (grid[i, j])
                    {
                        matrix[matrix.GetLength(0) - 1 - i, matrix.GetLength(1) - 1 - j] = counter >= text.Length ?
                                                                 '\0' : text[counter];
                        counter++;
                    }
            displayProgress(matrix);
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (grid[i, j])
                    {
                        matrix[matrix.GetLength(0) - 1 - i, j] = counter >= text.Length ?
                                                                 '\0' : text[counter];
                        counter++;
                    }
            displayProgress(matrix);
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = matrix.GetLength(1) - 1; j >= 0; j--)
                    if (grid[i, j])
                    {
                        matrix[i, matrix.GetLength(1) - 1 - j] = counter >= text.Length ?
                                                                 '\0' : text[counter];
                        counter++;
                    }
            displayProgress(matrix);
            return string.Join("", matrix.Cast<char>());
        }

        static string Decode(string text)
        {
            string result = "";
            int counter = 0;
            char[,] matrix = new char[grid.GetLength(0), grid.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = text[counter];
                    counter++;
                }
            displayProgress(matrix);
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (grid[i, j])
                    {
                        result += matrix[i, j];
                    }
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
                for (int j = matrix.GetLength(1) - 1; j >= 0; j--)
                    if (grid[i, j])
                    {
                        result += matrix[matrix.GetLength(0) - 1 - i, matrix.GetLength(1) - 1 - j];                        
                    }
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (grid[i, j])
                    {
                        result += matrix[matrix.GetLength(0) - 1 - i, j];
                        counter++;
                    }
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = matrix.GetLength(1) - 1; j >= 0; j--)
                    if (grid[i, j])
                    {
                        result += matrix[i, matrix.GetLength(1) - 1 - j];
                    }
            return result;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            PrintGrid();
            int answer = 0;
            string text;
            while (answer != 3)
            {
                Console.WriteLine("Выберите операцию:\n" +
                                  "1. Декодировать\n" +
                                  "2. Закодировать\n" +
                                  "3. Выйти\n");
                answer = int.Parse(Console.ReadLine());
                switch(answer)
                {
                    case 1:
                        Console.WriteLine("Введите сообщение для декодирования: ");
                        text = Console.ReadLine();
                        Console.WriteLine(Decode("ЕШАТСЕМЯНШИИОЙФПРРЧЕРЕАФЕШСРСЕТАТТНМАКЫАРАМСШЛРУНУОТЯВКВЛИЧЯ"));
                        break;
                    case 2:
                        Console.WriteLine("Введите сообщение для кодирования: ");
                        text = Console.ReadLine();
                        Console.WriteLine(Encode("ШИФРРЕШЕТКАЯВЛЯЕТСЯЧАСТНЫМСЛУЧАЕМШИФРАМАРШРУТНОЙПЕРЕСТАНОВКИ"));
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Неверный ввод, попробуйте снова");
                        break;
                }
            }
        }
    }
}
