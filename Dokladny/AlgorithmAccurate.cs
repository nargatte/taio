using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaioCore;

namespace Dokladny
{
    class AlgorithmAccurate : AlgorithmBase
    {
        private int VSize = 0;
        private GraphsIsomorphism VSolution = new GraphsIsomorphism();

        private int VeSize = 0;
        private GraphsIsomorphism VeSolution = new GraphsIsomorphism();

        private bool Check;

        public AlgorithmAccurate(Graph G1, Graph G2) : base(G1, G2)
        {
        }

        public string GetSolutionForVertexCounter() => VSolution.ToString();

        public string GetSolutionForVertexAndEdgesCounter() => VeSolution.ToString();

        protected override void BeforeFindSolutionFrom()
        {
            Check = true;
        }


        protected override void PairInFindSolutionFrom(int a, int b, int counter)
        {
            Check = false;

            G1Left[a] = false;
            G2Left[b] = false;

            CurrentIsomorphism.AddAtEnd(a, b);

            EdgeCounter += counter;

            FindSolutionFrom();

            G1Left[a] = true;
            G2Left[b] = true;

            CurrentIsomorphism.RemoveFormEnd();

            EdgeCounter -= counter;
        }

        protected override void AfterFindSolutionFrom()
        {
            if (Check)
            {
                if (CurrentIsomorphism.Size > VSize)
                {
                    VSize = CurrentIsomorphism.Size;
                    VSolution = CurrentIsomorphism.Clone();
                }
                if (CurrentIsomorphism.Size + EdgeCounter > VeSize)
                {
                    VeSize = CurrentIsomorphism.Size + EdgeCounter;
                    VeSolution = CurrentIsomorphism.Clone();
                }
            }
        }
    }
}
