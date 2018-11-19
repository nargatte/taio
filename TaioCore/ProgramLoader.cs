using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaioCore
{
    public static class ProgramLoader
    {
        public static void LoadGraphs(string[] args, Action<Graph, Graph> action)
        {
            Graph G1 = null;
            Graph G2 = null;
            try
            {
                if (args.Length != 2) throw new Exception("Program must run with two arguments");
                G1 = new Graph(args[0]);
                G2 = new Graph(args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(e.Message);
                return;
            }
            action(G1, G2);
        }
    }
}
