using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaioCore;

namespace Przyblizony
{
    public class AlgorithmEstimatedForVE : AlgorithmEstimatedBase
    {
        public AlgorithmEstimatedForVE(Graph G1, Graph G2) : base(G1, G2)
        {
        }

        protected override int GetMetrics(int a, int b, int counter) => counter;

        protected override void TryToUpdateSolution()
        {
            if (CurrentIsomorphism.Size + EdgeCounter > Size)
            {
                Size = CurrentIsomorphism.Size + EdgeCounter;
                Solution = CurrentIsomorphism.Clone();
            }
        }
    }
}
