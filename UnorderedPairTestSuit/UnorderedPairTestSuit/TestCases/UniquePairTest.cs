﻿using XSLibrary.UnitTests;
using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    abstract class UniquePairTest<PartType, GlobalType> : UnitTest
    {
        UniquePairDistribution<PartType, GlobalType> Distribution { get; set; }
        SharedMemoryCores<PartType, GlobalType> Pool { get { return Distribution.CorePool as SharedMemoryCores<PartType, GlobalType>; } }

        protected PartType[] m_elements { get; set; }
        protected GlobalType m_globalData { get; set; }

        public UniquePairTest(UniquePairDistribution<PartType, GlobalType> distribution)
        {
            m_log = new LoggerConsole();

            Distribution = distribution;
        }

        protected abstract void CalculationFunction(PartType part1, PartType part2, GlobalType global);

        protected override void Initializing()
        {
            Pool.SetCalculationFunction(CalculationFunction);
        }

        protected override void TestRoutine(TestResult result)
        {
            // distribute calculations and execute them with the injected algorithm
            Distribution.Calculate(m_elements, m_globalData);

            result.Successful = true;
        }

        protected override void PostProcessing()
        {
        }

        public override void Dispose()
        {
            Distribution.Dispose();
        }
    }
}
