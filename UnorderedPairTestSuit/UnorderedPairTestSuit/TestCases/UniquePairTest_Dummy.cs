using XSLibrary.MultithreadingPatterns.UniquePair;
using System.Threading;

namespace UnorderedPairTestSuit
{
    class UniquePairTest_Dummy : UniquePairTest<CalculationDummy, int>
    {
        public UniquePairTest_Dummy(UniquePairDistribution<CalculationDummy, int> distribution, int dummyCount) : base(distribution)
        {
            m_parts = new CalculationDummy[dummyCount];
            for (int i = 0; i < dummyCount; i++)
            {
                m_parts[i] = new CalculationDummy();
            }
        }

        protected override void CalculationFunction(CalculationDummy part1, CalculationDummy part2, int global)
        {
            Thread.Sleep(100);
        }
    }
}
