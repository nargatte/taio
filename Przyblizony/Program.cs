using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaioCore;

namespace Przyblizony
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramLoader.LoadGraphs(args, (G1, G2) =>
            {
                var av = new AlgorithmEstimatedForV(G1, G2);
                var ave = new AlgorithmEstimatedForVE(G1, G2);

                var lv = AlgorithmStopwatch.RunningTime(av);
                var lve = AlgorithmStopwatch.RunningTime(ave);

                Console.WriteLine($"Czas: {lv} ms");
                Console.WriteLine("Rozwiązanie dla metryki liczby wierzchołków");
                Console.WriteLine(av.GetSolution());
                Console.WriteLine($"Czas: {lve} ms");
                Console.WriteLine("Rozwiązanie dla metryki liczby wierzchołków i liczby krawędzi");
                Console.WriteLine(ave.GetSolution());
            });
        }

    }
}