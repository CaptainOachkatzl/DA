using System.Threading;
using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    abstract class DurationTest : UniquePairTest<int, int>
    {
        protected abstract int Duration { get; }

        public DurationTest(UniquePairDistribution<int, int> distribution, int dummyCount) : base(distribution)
        {
            // initialize elements
            m_elements = new int[dummyCount];
            for (int i = 0; i < dummyCount; i++)
            {
                m_elements[i] = i;
            }
        }

        protected override void CalculationFunction(int part1, int part2, int global)
        {
            // fixed duration of the execution
            Thread.Sleep(Duration);
        }
    }
}
