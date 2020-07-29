using System;
using System.IO;
using System.Collections.Generic;

namespace AdventofCodeDay8
{
    class Program
    {
        enum Colors
        {
            black = 0,
            white = 1,
            transparent = 2
        }

        static void Main(string[] args)
        {
            //obtain information from text file
            string text = ObtainTextInfo();
            //obtain information about width of picture
            int width = GetWidth();
            //obtain information about height of picture
            int height = GetHeight();
            //split text into layers
            List<int[,]> layers = SplitIntoLayers(text, width, height);
            //PartA(layers);
            PartB(layers);
        }

        static void PartA(List<int[,]> layers)
        {
            //get layer with most 0s
            List<int> zeroes = CountZeroes(layers);
            //remove layers with fewer 0s
            int[,] layer = RemoveMostZeroLayers(layers, zeroes);
            //get count of 1s times the number of twos in layer
            int result = GetCount(layer);
            Console.WriteLine(result);
        }

        static void PartB(List<int[,]> layers)
        {
            //Layer the layers on top of each other
            int[,] picture = LayerLayers(layers);
            //Print out picture
            PrintPicture(picture);
        }

        static string ObtainTextInfo()
        {
            string filePath = "D:\\Mykola\\Pictures\\inputDay8.txt";
            string text = File.ReadAllText(filePath);
            text = text.Replace("\n", "");
            return text;
        }

        static int GetWidth()
        {
            Console.Write("Please enter how wide(in pixels) your image is: ");
            return int.Parse(Console.ReadLine());
        }

        static int GetHeight()
        {
            Console.Write("Please enter how tall(in pixels) your image is: ");
            return int.Parse(Console.ReadLine());
        }

        static List<int[,]> SplitIntoLayers(string text, int width, int height)
        {
            List<int[,]> layers = new List<int[,]>();

            while (text.Length != 0)
            {
                int[,] layer = new int[width, height];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        layer[j, i] = int.Parse(text[0].ToString());
                        text = text.Substring(1);
                    }
                }
                layers.Add(layer);
            }

            /*foreach(int[,] layer in layers)
            {
                for (int i = 0; i < layer.GetLength(1); i++)
                {
                    for (int j = 0; j < layer.GetLength(0); j++)
                    {
                        Console.Write(layer[j, i]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }*/

            return layers;
        }

        static List<int> CountZeroes(List<int[,]> layers)
        {
            List<int> zeroCount = new List<int>();
            foreach (int[,] layer in layers)
            {
                int numberOfZeroes = 0;

                for (int i = 0; i < layer.GetLength(1); i++)
                {
                    for (int j = 0; j < layer.GetLength(0); j++)
                    {
                        if (layer[j, i] == 0)
                        {
                            numberOfZeroes++;
                        }
                    }
                }

                zeroCount.Add(numberOfZeroes);
            }

            /*foreach (int[,] layer in layers)
            {
                for (int i = 0; i < layer.GetLength(1); i++)
                {
                    for (int j = 0; j < layer.GetLength(0); j++)
                    {
                        Console.Write(layer[j, i]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(zeroCount[layers.IndexOf(layer)]);
            }*/

            return zeroCount;
        }

        static int[,] RemoveMostZeroLayers(List<int[,]> layers, List<int> zeroes)
        {
            int layerContainingFewestZeroes = -1;
            int minZeroCount = int.MaxValue;
            foreach (int zero in zeroes)
            {
                if (zero < minZeroCount)
                {
                    minZeroCount = zero;
                }
            }

            foreach(int[,] layer in layers)
            {
                layerContainingFewestZeroes++;
                int thisZeroCount = 0;
                for (int i = 0; i < layer.GetLength(1); i++)
                {
                    for (int j = 0; j < layer.GetLength(0); j++)
                    {
                        if (layer[j, i] == 0)
                        {
                            thisZeroCount++;
                        }
                    }
                }
                if (thisZeroCount == minZeroCount)
                {
                    break;
                }
            }

            return layers[layerContainingFewestZeroes];
        }

        static int GetCount(int[,] layer)
        {
            int numberOfOnes = 0;
            int numberOfTwos = 0;
            
            for (int i = 0; i < layer.GetLength(1); i++)
            {
                for (int j = 0; j < layer.GetLength(0); j++)
                {
                    if (layer[j, i] == 1)
                    {
                        numberOfOnes++;
                    }
                    else if(layer[j,i] == 2)
                    {
                        numberOfTwos++;
                    }
                }
            }

            return numberOfOnes * numberOfTwos;
        }

        static int[,] LayerLayers(List<int[,]> layers)
        {
            int[,] picture = new int[layers[0].GetLength(1),layers[0].GetLength(0)];

            for (int i = 0; i < picture.GetLength(0); i++)
            {
                for (int j = 0; j < picture.GetLength(1); j++)
                {
                    foreach(int[,] layer in layers)
                    {
                        if (layer[j,i] != (int)Colors.transparent)
                        {
                            picture[i, j] = layer[j, i];
                            break;
                        }
                    }
                }
            }

            return picture;
        }

        static void PrintPicture(int[,] picture)
        {
            for (int i = 0; i < picture.GetLength(0); i++)
            {
                for (int j = 0; j < picture.GetLength(1); j++)
                {
                    if (picture[i, j] == 0)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(picture[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
