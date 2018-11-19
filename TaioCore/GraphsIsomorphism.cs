using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaioCore
{
    public class GraphsIsomorphism
    {
        private readonly List<Tuple<int, int>> isomorphism;

        public GraphsIsomorphism()
        {
            isomorphism = new List<Tuple<int, int>>();
        }

        private GraphsIsomorphism(List<Tuple<int, int>> isomorphism)
        {
            this.isomorphism = new List<Tuple<int, int>>(isomorphism);
        }

        public void AddAtEnd(int a, int b) => isomorphism.Add(new Tuple<int, int>(a, b));

        public void RemoveFormEnd() => isomorphism.RemoveAt(isomorphism.Count - 1);

        public void Iterate(Action<int, int> bodyOfLoop)
        {
            foreach (var t in isomorphism)
                bodyOfLoop(t.Item1, t.Item2);
        }

        public int Size => isomorphism.Count;

        public GraphsIsomorphism Clone() => new GraphsIsomorphism(isomorphism);

        public override string ToString() 
            => 
            string.Join(",", isomorphism.Select(t => t.Item1).ToArray()) + 
            "\n" + 
            string.Join(",", isomorphism.Select(t => t.Item2).ToArray());

    }
}