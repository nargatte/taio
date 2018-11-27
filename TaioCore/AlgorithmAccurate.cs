namespace TaioCore
{
    class AlgorithmAccurate : AlgorithmBase, IReturnVESolution, IReturnVSolution
    {
        private int VSize = 0;
        private GraphsIsomorphism VSolutionIso = new GraphsIsomorphism();

        private int VeSize = 0;
        private GraphsIsomorphism VeSolutionIso = new GraphsIsomorphism();

        private bool Check;

        public AlgorithmAccurate(Graph G1, Graph G2) : base(G1, G2)
        {
        }

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
                    VSolutionIso = CurrentIsomorphism.Clone();
                }
                if (CurrentIsomorphism.Size + EdgeCounter > VeSize)
                {
                    VeSize = CurrentIsomorphism.Size + EdgeCounter;
                    VeSolutionIso = CurrentIsomorphism.Clone();
                }
            }
        }

        public GraphsIsomorphism VESolution => VeSolutionIso;
        public GraphsIsomorphism VSolution => VSolutionIso;
    }
}
