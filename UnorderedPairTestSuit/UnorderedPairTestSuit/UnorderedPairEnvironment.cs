using XSLibrary.UnitTests;
using XSLibrary.MultithreadingPatterns.UniquePair;
using System.Collections.Generic;
using System.Threading;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    class UnorderedPairEnvironment : UnitTest
    {
        UniquePairDistribution<CalculationDummy, double> m_distribution;
        SharedMemoryPool<CalculationDummy, double> m_pool;

        List<CalculationDummy> m_dummies;

        public UnorderedPairEnvironment(int dummyCount)
        {
            m_log = new LoggerConsole();

            m_dummies = new List<CalculationDummy>();

            for (int i = 0; i < dummyCount; i++)
            {
                m_dummies.Add(new CalculationDummy());
            }
        }

        protected override void Initializing()
        {
            m_pool = new ActorPool<CalculationDummy, double>(4, false);
            m_distribution = new ResourceLockDistribution<CalculationDummy, double>(4, DummyCalculation);

            m_pool.SetCalculationFunction(DummyCalculation);
        }

        protected override void TestRoutine(TestResult result)
        {
            m_distribution.Calculate(m_dummies.ToArray(), 0);

            result.Successful = true;
        }

        protected override void PostProcessing()
        {
        }

        private void DummyCalculation(CalculationDummy part1, CalculationDummy part2, double global)
        {
            Thread.Sleep(100);
        }

        public override void Dispose()
        {
            m_distribution.Dispose();
        }
    }
}
