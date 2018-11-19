using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaioCore;

namespace Dokladny
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramLoader.LoadGraphs(args, (G1, G2) =>
            {
                var a = new AlgorithmAccurate(G1, G2);
                var l = AlgorithmStopwatch.RunningTime(a);

                Console.WriteLine($"Czas: {l} ms");
                Console.WriteLine("Rozwiązanie dla metryki liczby wierzchołków");
                Console.WriteLine(a.GetSolutionForVertexCounter());
                Console.WriteLine("Rozwiązanie dla metryki liczby wierzchołków i liczby krawędzi");
                Console.WriteLine(a.GetSolutionForVertexAndEdgesCounter());
            });
        }

    }
}
