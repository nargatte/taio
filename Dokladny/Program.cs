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
            Graph Ga = new Graph("../../../7_8_A_Krzysiak.csv");
            Graph Gb = new Graph("../../../7_8_B_Krzysiak.csv");

            new AlgorithmBase(Ga, Gb).Run();

        }

    }
}
