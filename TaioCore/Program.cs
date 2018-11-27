using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TaioCore
{
    public static class Program
    {

        static bool isAccurate = true;
        static bool isV = true;
        static Graph G1;
        static Graph G2;
        private static GraphsIsomorphism sol;

        public static void Main(string[] args)
        {
            Random r = new Random();

            try
            {
                if (args[0] == "a") isAccurate = true;
                else if (args[0] == "e") isAccurate = false;
                else throw new ArgumentException($"Wrong symbol: {args[0]}");

                if (args[1] == "v") isV = true;
                else if (args[1] == "ve") isV = false;
                else throw new ArgumentException($"Wrong symbol: {args[1]}");

                if (args[2] == "g2")
                {
                    int nv1 = Int32.Parse(args[3]);
                    int nv2 = Int32.Parse(args[4]);
                    int nvb = Int32.Parse(args[5]);
                    double f = Double.Parse(args[6].Replace('.', ','));

                    if (Math.Min(nv1, nv2) < nvb)
                        throw new ArgumentException($"Must be {Math.Min(nv1, nv2)}>={nvb}");

                    nv1 -= nvb;
                    nv2 -= nvb;

                    Graph gb = new Graph(nvb, f, r.Next());

                    G1 = new Graph(gb, nv1, f, r.Next());
                    G2 = new Graph(gb, nv2, f, r.Next());

                    RunAndDisplaySingle();
                }
                else if (args[2] == "gl")
                {
                    int numberOfIterations = Int32.Parse(args[3]);
                    int[] sizeB = args[4].Split('-').Select(Int32.Parse).ToArray();
                    double f = Double.Parse(args[5].Replace('.', ','));
                    if (sizeB.Length != 2)
                        throw new ArgumentException($"Must be instead {args[4]} Vmin-Vmax");
                    Console.WriteLine($"{"G1 V",5}{"G2 V",5}{"Sol",5}{"Time",6}");
                    for (int x = 0; x < numberOfIterations; x++)
                    {
                        G1 = new Graph(r.Next(sizeB[0], sizeB[1] + 1), f, r.Next());
                        G2 = new Graph(r.Next(sizeB[0], sizeB[1] + 1), f, r.Next());
                        long t = GetSolution();
                        Console.WriteLine($"{G1.NumberOfVertices,5}{G2.NumberOfVertices,5}{sol.Size,5}{t,6}");
                    }
                }
                else
                {
                    G1 = new Graph(args[2]);
                    G2 = new Graph(args[3]);

                    RunAndDisplaySingle();
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error: " + e.Message);
                PrintInstruction();
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Not enough of arguments.");
                PrintInstruction();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public static void PrintInstruction()
        {
            Console.WriteLine("Program  must run with following arguments:\n" +
                              "a or e - a: accurate, e: estimated\n" +
                              "v or ve - v: maximum vertices number, ve maximum vertices and edges sum\n" +
                              "g2 or gl or fileName1.csv fileName2.csv\n" +
                              "where g2 will generate two random graphs (g2 V1 V2 G E)\n" +
                              "gl will generate list of random graphs (gl N Vmin-Vmax E)\n" +
                              "last one is for load graphs form files\n" +
                              "V1, V2 - numbers of vertices in graphs\n" +
                              "G - number of guaranteed vertices in correct solution\n" +
                              "E - probability of edge occur between 0 and 1\n" +
                              "N - number of examples in generated list\n" +
                              "Vmin, Vmax - lower and upper bound of number of vertices in example graphs\n" +
                              "Examples:\n" +
                              "a v g2 5 6 4 0.5\n" +
                              "e v gl 30 10-15 0.1\n" +
                              "a ve 10_12_A_Name.csv 10_12_B_Name.csv");
        }

        public static void RunAndDisplaySingle()
        {
            int min;
            int max;
            double avr;

            G1.Deg(out min, out avr, out max);
            Console.WriteLine($"G1 vertexNum:{G1.NumberOfVertices} minDeg:{min} avrDeg:{avr:F} maxDeg:{max}");
            G1.Print();

            G2.Deg(out min, out avr, out max);
            Console.WriteLine($"G2 vertexNum:{G2.NumberOfVertices} minDeg:{min} avrDeg:{avr:F} maxDeg:{max}");
            G2.Print();

            long t = GetSolution();

            Console.WriteLine("G1 solution:");
            G1.Print(sol.GetPermutation1);

            Console.WriteLine("G2 solution:");
            G2.Print(sol.GetPermutation2);

            Console.WriteLine($"Solution size: {sol.Size}\nTime: {t}");
        }

        public static long GetSolution()
        {
            long t;
            if (isAccurate)
            {
                AlgorithmAccurate a = new AlgorithmAccurate(G1, G2);
                t = AlgorithmStopwatch.RunningTime(a);
                sol = isV ? a.VSolution : a.VESolution;
            }
            else
            {
                if (isV)
                {
                    AlgorithmEstimatedForV a = new AlgorithmEstimatedForV(G1, G2);
                    t = AlgorithmStopwatch.RunningTime(a);
                    sol = a.VSolution;
                }
                else
                {
                    AlgorithmEstimatedForVE a = new AlgorithmEstimatedForVE(G1, G2);
                    t = AlgorithmStopwatch.RunningTime(a);
                    sol = a.VESolution;
                }
            }

            return t;
        }
    }
}
