namespace TaioCore
{
    public abstract class AlgorithmEstimatedBase : AlgorithmBase
    {
        protected int Size = 0;

        protected GraphsIsomorphism Solution = new GraphsIsomorphism();

        protected int BestMetrics;

        protected int BestA;
        protected int BestB;

        public AlgorithmEstimatedBase(Graph G1, Graph G2) : base(G1, G2)
        {
        }

        public GraphsIsomorphism GetSolution() => Solution;

        protected override void BeforeFindSolutionFrom()
        {
            BestMetrics = -1;

            BestA = 0;
            BestB = 0;
        }

        protected override void PairInFindSolutionFrom(int a, int b, int counter)
        {
            int m = GetMetrics(a, b, counter);

            if(m > BestMetrics)
            {
                BestMetrics = m;

                BestA = a;
                BestB = b;
            }
        }

        protected override void AfterFindSolutionFrom()
        {
            if(BestMetrics == -1)
                TryToUpdateSolution();
            else
            {
                CurrentIsomorphism.AddAtEnd(BestA, BestB);

                G1Left[BestA] = false;
                G2Left[BestB] = false;

                FindSolutionFrom();
            }
        }

        protected abstract void TryToUpdateSolution();

        protected abstract int GetMetrics(int a, int b, int counter);
    }
}
