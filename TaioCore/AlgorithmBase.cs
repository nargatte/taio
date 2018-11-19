using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaioCore
{
    public class AlgorithmBase
    {
        protected Graph G1;
        protected Graph G2;

        int vSize = 0;
        GraphsIsomorphism vsolution = new GraphsIsomorphism();

        int VeSize = 0;
        GraphsIsomorphism vesolution = new GraphsIsomorphism();

        int e = 0;

        bool[] g1Left;
        bool[] g2Left;

        GraphsIsomorphism W;

        public AlgorithmBase(Graph G1, Graph G2)
        {
            this.G1 = G1;
            this.G2 = G2;
        }

        public void Run()
        {
            for (int u = 0; u < G1.NumberOfVertices; u++)
                for (int v = 0; v < G2.NumberOfVertices; v++)
                {
                    W = new GraphsIsomorphism();
                    W.AddAtEnd(u, v);
                    e = 0;
                    g1Left = new bool[G1.NumberOfVertices];
                    g2Left = new bool[G2.NumberOfVertices];
                    for (int x = 0; x < g1Left.Length; x++)
                        g1Left[x] = true;
                    for (int x = 0; x < g2Left.Length; x++)
                        g2Left[x] = true;
                    g1Left[u] = false;
                    g2Left[v] = false;
                    rek();
                }

            Console.WriteLine(vsolution);
            Console.WriteLine();
            Console.WriteLine(vesolution);
        }

        void rek()
        {
            bool check = true;
            for (int a = 0; a < g1Left.Length; a++)
                for (int b = 0; b < g2Left.Length; b++)
                    if (g1Left[a] && g2Left[b])
                    {
                        int counter = 0;
                        bool symmetric = true;
                        W.Iterate((c, d)=> {
                            if (G1.IsLink(a, c))
                            {
                                if (G2.IsLink(b, d))
                                    counter++;
                                else
                                    symmetric = false;
                            }
                            else
                            {
                                if (G2.IsLink(b, d))
                                    symmetric = false;
                            }
                        });
                        if (symmetric && counter > 0)
                        {
                            check = false;
                            g1Left[a] = false;
                            g2Left[b] = false;
                            W.AddAtEnd(a, b);
                            e += counter;
                            rek();
                            g1Left[a] = true;
                            g2Left[b] = true;
                            W.RemoveFormEnd();
                            e -= counter;
                        }
                    }
            if (check)
            {

                if (W.Size > vSize)
                {
                    vSize = W.Size;
                    vsolution = W.Clone();
                }
                if (W.Size + e > VeSize)
                {
                    VeSize = W.Size + e;
                    vesolution = W.Clone();
                }
            }
        }
    }
}
