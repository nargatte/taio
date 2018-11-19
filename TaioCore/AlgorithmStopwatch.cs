using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace TaioCore
{
    public static class AlgorithmStopwatch
    {
        public static long RunningTime(AlgorithmBase algorithm)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            algorithm.Run();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
