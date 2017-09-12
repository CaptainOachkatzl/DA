using XSLibrary.UnitTests;
using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    abstract class UniquePairTest<PartType, GlobalType> : UnitTest
    {
        protected UniquePairDistribution<PartType, GlobalType> Distribution { get; set; }

        public PartType[] m_elements { get; protected set; }
        public GlobalType m_globalData { get; protected set; }

        public UniquePairTest() : this(null) { }
        public UniquePairTest(UniquePairDistribution<PartType, GlobalType> distribution)
        {
            m_log = new LoggerConsole();

            Distribution = distribution;
        }

        public void SetDistribution(UniquePairDistribution<PartType, GlobalType> distribution)
        {
            Distribution = distribution;
        }

        protected abstract void CalculationFunction(PartType element1, PartType element2, GlobalType global);

        protected override void Initializing()
        {
            if (Distribution == null)
                throw new System.Exception("Distribution is null.");

            Distribution.SetCalculationFunction(CalculationFunction);
        }

        protected override void TestRoutine(TestResult result)
        {
            if (Distribution == null)
                throw new System.Exception("Distribution is null.");

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
