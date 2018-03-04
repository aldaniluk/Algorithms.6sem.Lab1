using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1_input = Directory.GetCurrentDirectory() + @"\..\..\screens_data_input_values.txt";
            string path1_output = Directory.GetCurrentDirectory() + @"\..\..\screens_data_output_values.txt";
            int M = 0, n = 0, x = 0, p = 0;
            List<Screen> screens = new List<Screen>();

            using (StreamReader reader = new StreamReader(path1_input))
            {
                string[] line;
                while (reader.Peek() >= 0)
                {
                    line = reader.ReadLine().Split(new char[] { '=' });
                    string variable = line[0];
                    int value = int.Parse(line[1]);
                    if (variable == "M")
                    {
                        M = value;
                    }
                    else if (variable == "n")
                    {
                        n = value;
                    }
                    else if (variable.Contains("x"))
                    {
                        x = value;
                    }
                    else if (variable.Contains("p"))
                    {
                        p = value;
                        screens.Add(new Screen(x, p));
                    }
                }
            }

            ScreenCounterResult maxPriceAndScreensNumbers = ScreenCounter.Count(M, n, screens);

            StringBuilder screensResult = new StringBuilder();
            screensResult.Append($"Max price: {maxPriceAndScreensNumbers.MaxPrice}, \nScreens numbers: ");
            maxPriceAndScreensNumbers.ScreensNumbers.ForEach(s => screensResult.Append($"{s} "));

            using (StreamWriter writer = new StreamWriter(path1_output))
            {
                writer.Write(screensResult);
            }

            Console.WriteLine(screensResult);

            Console.Write("\n--------------------\n");

            string path2_input = Directory.GetCurrentDirectory() + @"\..\..\vertices_data_input_values.txt";
            string path2_output = Directory.GetCurrentDirectory() + @"\..\..\vertices_data_output_values.txt";
            List<Vertex> vertices = new List<Vertex>();

            using (StreamReader reader = new StreamReader(path2_input))
            {
                int vertexNumber = 1;
                while (reader.Peek() >= 0)
                {
                    vertices.Add(new Vertex(vertexNumber++, int.Parse(reader.ReadLine())));
                }
            }

            IndependentVerticesSetResult verticesSet = GraphCounter.GetIndependentVerticesSetWithMaxWeight(vertices);

            StringBuilder verticesResult = new StringBuilder();
            verticesResult.Append($"Max weight: {verticesSet.MaxWeight}, \nVertices numbers: ");
            verticesSet.Vertices.ForEach(v => verticesResult.Append($"{v} "));

            using (StreamWriter writer = new StreamWriter(path2_output))
            {
                writer.Write(verticesResult);
            }

            Console.WriteLine(verticesResult);
        }
    }
}
