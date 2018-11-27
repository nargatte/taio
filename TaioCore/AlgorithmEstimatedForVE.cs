namespace TaioCore
{
    public class AlgorithmEstimatedForVE : AlgorithmEstimatedBase, IReturnVESolution
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

        public GraphsIsomorphism VESolution => Solution;
    }
}
