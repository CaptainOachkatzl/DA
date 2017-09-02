using XSLibrary.MultithreadingPatterns.UniquePair;
using System.Threading;

namespace UnorderedPairTestSuit
{
    class FixedDurationTest : UniquePairTest<int, int>
    {
        public FixedDurationTest(UniquePairDistribution<int, int> distribution, int dummyCount) : base(distribution)
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
            Thread.Sleep(100);
        }
    }
}