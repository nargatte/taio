using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaioCore
{
    public abstract class AlgorithmBase
    {
        protected Graph G1;
        protected Graph G2;

        protected bool[] G1Left;
        protected bool[] G2Left;

        protected int EdgeCounter = 0;

        protected GraphsIsomorphism CurrentIsomorphism;

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
                    CurrentIsomorphism = new GraphsIsomorphism();
                    CurrentIsomorphism.AddAtEnd(u, v);

                    EdgeCounter = 0;

                    G1Left = new bool[G1.NumberOfVertices];
                    G2Left = new bool[G2.NumberOfVertices];

                    for (int x = 0; x < G1Left.Length; x++)
                        G1Left[x] = true;

                    for (int x = 0; x < G2Left.Length; x++)
                        G2Left[x] = true;

                    G1Left[u] = false;
                    G2Left[v] = false;

                    FindSolutionFrom();
                }
        }

        protected void FindSolutionFrom()
        {
            BeforeFindSolutionFrom();

            for (int a = 0; a < G1Left.Length; a++)
                for (int b = 0; b < G2Left.Length; b++)
                    if (G1Left[a] && G2Left[b])
                    {
                        int counter = 0;
                        bool symmetric = true;

                        CurrentIsomorphism.Iterate((c, d) =>
                        {
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
                            PairInFindSolutionFrom(a, b, counter);
                        }
                    }

            AfterFindSolutionFrom();
        }

        protected abstract void AfterFindSolutionFrom();

        protected abstract void PairInFindSolutionFrom(int a, int b, int counter);

        protected abstract void BeforeFindSolutionFrom();
    }
}
