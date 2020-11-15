using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace praktika8
{
    class Program
    {
        static String Decode(Color[,] image, int distance)
        {
            int counter = 0;
            byte[] byteMessage = new byte[distance * 8];
            for (int i = 0; i < image.GetLength(0); i++)
                for (int j = 0; j < image.GetLength(1); j++)
                    for (int rgb = 0; rgb < 3; rgb++)
                        if (counter < distance * 8)
                        {
                            byte mask = 1;
                            switch (rgb)
                            {
                                case 0:
                                    byteMessage[counter / 8] |= (image[i, j].R % 2 == 1) ? mask <<= (7 - (counter % 8)) : byteMessage[counter / 8];
                                    break;
                                case 1:
                                    byteMessage[counter / 8] |= (image[i, j].G % 2 == 1) ? mask <<= (7 - (counter % 8)) : byteMessage[counter / 8];
                                    break;
                                case 2:
                                    byteMessage[counter / 8] |= (image[i, j].B % 2 == 1) ? mask <<= (7 - (counter % 8)) : byteMessage[counter / 8];
                                    break;
                            }
                            counter++;
                        }
            string result = Encoding.ASCII.GetString(byteMessage);
            return result;
        }

        static void Encode(Bitmap image, byte[] text)
        {
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
                for (int j = 0; j < 8; j++)
                {
                    byte mask = 1;
                    int currentMove = 7 - j;
                    mask <<= currentMove;
                    mask &= text[i];
                    int addition;
                    Color temp;
                    switch (counter%3)
                    {
                        case 0:
                            if ((image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).R % 2 == 0 && mask == 0) ||
                                (image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).R % 2 != 0 && mask != 0))
                                addition = 0;
                            else
                                addition = 1 * (image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).R - 1) < 0 ? 1 : -1;

                            temp = Color.FromArgb(image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).R + addition,
                                                  image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).G,
                                                  image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).B);
                            image.SetPixel((counter / 3) % image.Width, (counter / 3) / image.Width, temp);
                            break;
                        case 1:
                            if ((image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).G % 2 == 0 && mask == 0) ||
                                (image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).G % 2 != 0 && mask != 0))
                                addition = 0;
                            else
                                addition = 1 * (image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).G - 1) < 0 ? 1 : -1;

                            temp = Color.FromArgb(image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).R,
                                                  image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).G + addition,
                                                  image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).B);
                            image.SetPixel((counter / 3) % image.Width, (counter / 3) / image.Width, temp);
                            break;
                        case 2:
                            if ((image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).B % 2 == 0 && mask == 0) ||
                                (image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).B % 2 != 0 && mask != 0))
                                addition = 0;
                            else
                                addition = 1 * (image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).B - 1) < 0 ? 1 : -1;

                            Color temp1 = Color.FromArgb(image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).R,
                                                         image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).G,
                                                         image.GetPixel((counter / 3) % image.Width, (counter / 3) / image.Width).B + addition);
                            image.SetPixel((counter / 3) % image.Width, (counter / 3) / image.Width, temp1);
                            break;
                    }
                    counter++;
                }
            image.Save("encoded.bmp");

        }
        static void Main(string[] args)
        {
            int ask = 0;
            while (ask != 3) try
                {
                    Console.WriteLine("Выберите операцию:\n" +
                                      "1. Декодировать\n" +
                                      "2. Закодировать\n" +
                                      "3. Выйти");
                    ask = int.Parse(Console.ReadLine());
                    string imageRoute;
                    Bitmap image;
                    switch (ask)
                    {
                        case 1:
                            Console.WriteLine("Введите адрес изображения:");
                            imageRoute = Console.ReadLine();
                            image = new Bitmap(imageRoute, true);
                            Console.WriteLine("Введите предполагаемое количество символов. " +
                                              "Количество символов может быть не больше " +
                                              (image.Width * image.Height * 3 / 8));
                            int distance = int.Parse(Console.ReadLine());
                            Color[,] pixelColors = new Color[image.Height, image.Width];
                            for (int i = 0; i < image.Height; i++)
                                for (int j = 0; j < image.Width; j++)
                                    pixelColors[i, j] = image.GetPixel(j, i);
                            Console.WriteLine(Decode(pixelColors, distance));
                            break;
                        case 2:
                            Console.WriteLine("Введите адрес изображения:");
                            imageRoute = Console.ReadLine();
                            image = new Bitmap(imageRoute, true);
                            Console.WriteLine("Введите текст для внедрения:");
                            string desantnik = Console.ReadLine();
                            byte[] bytes = Encoding.ASCII.GetBytes(desantnik);
                            Encode(image, bytes);
                            break;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("Неверный ввод, попробуйте снова");
                            break;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Неверно введен адрес изображения");
                }
        }        
    }
}
