//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using

//namespace Przyblizony
//{
//    class Program
//    {
//        Graph Ga;
//        Graph Gb;

//        int vsize = 0;
//        GraphsIsomorphism vsolution = new GraphsIsomorphism();

//        int vesize = 0;
//        GraphsIsomorphism vesolution = new GraphsIsomorphism();

//        int e = 0;

//        bool[] galeft;
//        bool[] gbleft;

//        GraphsIsomorphism W = new GraphsIsomorphism();

//        static void Main(string[] args)
//        {
//            Graph Ga = new Graph("../../../p.csv");
//            Graph Gb = new Graph("../../../p.csv");

//            new Program().Run(Ga, Gb);
//        }

//        void Run(Graph Ga, Graph Gb)
//        {
//            this.Ga = Ga;
//            this.Gb = Gb;
//            for (int u = 0; u < Ga.NumberOfVertices; u++)
//                for (int v = 0; v < Gb.NumberOfVertices; v++)
//                {
//                    W.Clear();
//                    W.Add(new Tuple<int, int>(u, v));
//                    e = 0;
//                    galeft = new bool[Ga.NumberOfVertices];
//                    gbleft = new bool[Gb.NumberOfVertices];
//                    for (int x = 0; x < galeft.Length; x++)
//                        galeft[x] = true;
//                    for (int x = 0; x < gbleft.Length; x++)
//                        gbleft[x] = true;
//                    galeft[u] = false;
//                    gbleft[v] = false;
//                    rek();
//                }
//            Console.WriteLine(string.Join(",", vsolution.Select(t => t.Item1)));
//            Console.WriteLine(string.Join(",", vsolution.Select(t => t.Item2)));
//            Console.WriteLine();
//            Console.WriteLine(string.Join(",", vesolution.Select(t => t.Item1)));
//            Console.WriteLine(string.Join(",", vesolution.Select(t => t.Item2)));
//        }

//        void rek()
//        {
//            bool check = true;
//            for (int a = 0; a < galeft.Length; a++)
//                for (int b = 0; b < gbleft.Length; b++)
//                    if (galeft[a] && gbleft[b])
//                    {
//                        int counter = 0;
//                        bool symmetric = true;
//                        foreach (var t in W)
//                        {
//                            int c = t.Item1;
//                            int d = t.Item2;
//                            if (Ga.IsLink(a, c))
//                            {
//                                if (Gb.IsLink(b, d))
//                                    counter++;
//                                else
//                                    symmetric = false;
//                            }
//                            else
//                            {
//                                if (Gb.IsLink(b, d))
//                                    symmetric = false;
//                            }

//                        }
//                        if (symmetric && counter > 0)
//                        {
//                            check = false;
//                            galeft[a] = false;
//                            gbleft[b] = false;
//                            W.Add(new Tuple<int, int>(a, b));
//                            e += counter;
//                            rek();
//                            galeft[a] = true;
//                            gbleft[b] = true;
//                            W.RemoveAt(W.Count - 1);
//                            e -= counter;
//                        }
//                    }
//            if (check)
//            {
//                if (W.Count > vsize)
//                {
//                    vsize = W.Count;
//                    vsolution = new GraphsIsomorphism(W);
//                }
//                if (W.Count + e > vesize)
//                {
//                    vesize = W.Count + e;
//                    vesolution = new GraphsIsomorphism(W);
//                }
//            }
//        }
//    }
//}
