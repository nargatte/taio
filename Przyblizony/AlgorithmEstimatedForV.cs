using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaioCore;

namespace Przyblizony
{
    public class AlgorithmEstimatedForV : AlgorithmEstimatedBase
    {
        public AlgorithmEstimatedForV(Graph G1, Graph G2) : base(G1, G2)
        {
        }

        protected override int GetMetrics(int a, int b, int counter)
        {
            int level1 = 0;
            int level2 = 0;

            for (int x = 0; x < G1.NumberOfVertices; x++)
                if (G1Left[x] && G1.IsLink(a, x))
                    level1++;

            for (int x = 0; x < G2.NumberOfVertices; x++)
                if (G2Left[x] && G2.IsLink(a, x))
                    level2++;

            return Math.Min(level1, level2);
        }

        protected override void TryToUpdateSolution()
        {
            if(CurrentIsomorphism.Size > Size)
            {
                Size = CurrentIsomorphism.Size;
                Solution = CurrentIsomorphism.Clone();
            }
        }
    }
}
