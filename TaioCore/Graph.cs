using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaioCore
{
    public class Graph
    {
        private readonly bool[,] adjacencyMatrix;

        public Graph(string csvPath)
        {
            string line;
            List<bool[]> dataList = new List<bool[]>();

            System.IO.StreamReader file = new System.IO.StreamReader(csvPath);
            while ((line = file.ReadLine()) != null)
            {
                dataList.Add(line.Split(new[] { ',' }).Select(s =>
                {
                    if (s == "0")
                        return false;
                    if (s == "1")
                        return true;
                    throw new Exception($"Invalid csv file {csvPath}: {s} must by 0 or 1.");

                }).ToArray());
            }
            file.Close();

            if (dataList.Count == 0)
                throw new Exception($"Invalid csv file {csvPath}: file must contain at least one line.");

            if (dataList.Any(r => r.Length != dataList[0].Length))
                throw new Exception($"Invalid csv file {csvPath}: rows must be the same length.");

            if(dataList[0].Length != dataList.Count)
                throw new Exception($"Invalid csv file {csvPath}: rows and columns must be the same.");

            adjacencyMatrix = new bool[dataList.Count, dataList.Count];

            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++ )
            {
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    adjacencyMatrix[x, y] = dataList[x][y];
                }
            }

            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                for(int y = 0; y < adjacencyMatrix.GetLength(1); y++ )
                {
                    if (adjacencyMatrix[x, y] != adjacencyMatrix[y, x])
                        throw new Exception($"Invalid csv file {csvPath}: graph must be symmetric");
                }
            }
        }

        public bool IsSymmetric()
        {
            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    if (adjacencyMatrix[x, y] != adjacencyMatrix[y, x])
                        return false;
                }
            }

            return true;
        }

        public bool IsConnected()
        {
            bool[] visited = new bool[NumberOfVertices];
            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            while (stack.Count > 0)
            {
                int a = stack.Pop();
                visited[a] = true;
                for (int b = 0; b < NumberOfVertices; b++)
                {
                    if(adjacencyMatrix[a, b] && !visited[b])
                        stack.Push(b);
                }
            }

            return visited.All(a => a);
        }

        public void Deg(out int min, out double avr, out int max)
        {
            min = Int32.MaxValue;
            max = Int32.MinValue;
            long sum = 0;
            for (int v = 0; v < NumberOfVertices; v++)
            {
                int degv = DegBy(v);
                if (degv > max) max = degv;
                if (degv < min) min = degv;
                sum += degv;
            }

            avr = (double)sum / NumberOfVertices;
        }

        private int DegBy(int v)
        {
            int sum = 0;
            for(int x=0;x<NumberOfVertices;x++)
                if (adjacencyMatrix[x, v])
                    sum++;
            return sum;
        }

        public Graph(int numberOfVertices, double edgeChance, int seed)
        {
            Random r = new Random(seed);
            adjacencyMatrix = new bool[numberOfVertices, numberOfVertices];

            int[] path = Enumerable.Range(0, numberOfVertices).ToArray();
            for (int x = 0; x < path.Length-1; x++)
            {
                int index = r.Next(x, path.Length);
                int tmp = path[x];
                path[x] = path[index];
                path[index] = tmp;
            }

            for (int a = 0, b = 1; b < path.Length; a++, b++)
            {
                adjacencyMatrix[path[a], path[b]] = true;
                adjacencyMatrix[path[b], path[a]] = true;
            }

            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    if(adjacencyMatrix[x, y])
                        continue;
                    if (x < y)
                        adjacencyMatrix[x, y] = r.NextDouble() < edgeChance;
                    else
                        adjacencyMatrix[x, y] = adjacencyMatrix[y, x];
                }
            }
        }

        public Graph(Graph graph, int numberOfAditionalVertices, double edgeChance, int seed)
        {
            Random r = new Random(seed);
            Graph aditionalGraph = new Graph(numberOfAditionalVertices, edgeChance, r.Next());

            int totalSize = numberOfAditionalVertices + graph.NumberOfVertices;

            adjacencyMatrix = new bool[totalSize, totalSize];

            int a = r.Next(aditionalGraph.NumberOfVertices);
            int b = r.Next(graph.NumberOfVertices) + aditionalGraph.NumberOfVertices;

            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    if (x == a && y == b)
                        adjacencyMatrix[x, y] = true;
                    else if (x < y)
                    {
                        if (x < aditionalGraph.NumberOfVertices && y < aditionalGraph.NumberOfVertices)
                            adjacencyMatrix[x, y] = aditionalGraph.IsLink(x, y);
                        else if (x < aditionalGraph.NumberOfVertices && y >= aditionalGraph.NumberOfVertices)
                            adjacencyMatrix[x, y] = r.NextDouble() < edgeChance;
                        else
                            adjacencyMatrix[x, y] = graph.IsLink(x - aditionalGraph.NumberOfVertices,
                                y - aditionalGraph.NumberOfVertices);
                    }
                    else
                        adjacencyMatrix[x, y] = adjacencyMatrix[y, x];
                }
            }

            int[] permutation = Enumerable.Range(0, adjacencyMatrix.GetLength(0)).ToArray();
            for (int x = 0; x < permutation.Length -1; x++)
            {
                int index = r.Next(x, permutation.Length);
                int tmp = permutation[x];
                permutation[x] = permutation[index];
                permutation[index] = tmp;
            }

            bool[,] tmpM = new bool[adjacencyMatrix.GetLength(0), adjacencyMatrix.GetLength(0)];

            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    tmpM[x, y] = adjacencyMatrix[permutation[x], permutation[y]];
                }
            }

            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    adjacencyMatrix[x, y] = tmpM[x, y];
                }
            }
        }

        public int NumberOfVertices => adjacencyMatrix.GetLength(0);

        public bool IsLink(int a, int b) => adjacencyMatrix[a, b];

        public void Print(int[] permutation = null)
        {
            if (permutation == null) permutation = new int[0];

            int[] fullPermutation = permutation
                .Concat(Enumerable.Range(0, adjacencyMatrix.GetLength(0)).Where(x => !permutation.Contains(x)))
                .ToArray();

            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
            {
                if (y == permutation.Length)
                    Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{fullPermutation[y],3}");
            }

            Console.Write("\n");
            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                if(x < permutation.Length)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{fullPermutation[x],2} ");
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    if (y == permutation.Length)
                        Console.ForegroundColor = ConsoleColor.White;
                    if(x != y) Console.Write(adjacencyMatrix[fullPermutation[x], fullPermutation[y]] ? "#  " : ".  ");
                    else Console.Write("   ");
                }
                Console.Write("\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
