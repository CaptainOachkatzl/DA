using System.Threading;

namespace UnorderedPairTestSuit
{
    abstract class DurationTest : SinglePerformanceTest
    {
        protected abstract int Duration { get; }

        public DurationTest(int dummyCount) : base(dummyCount)
        {
        }

        protected override void CalculationFunction(int part1, int part2, int global)
        {
            // fixed duration of the execution
            Thread.Sleep(Duration);
        }
    }
}
