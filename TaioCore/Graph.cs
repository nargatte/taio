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

        public int NumberOfVertices => adjacencyMatrix.GetLength(0);

        public bool IsLink(int a, int b) => adjacencyMatrix[a, b];

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                {
                    stringBuilder.Append(adjacencyMatrix[x, y] ? "1" : "0");
                    stringBuilder.Append(" ");
                }
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}
