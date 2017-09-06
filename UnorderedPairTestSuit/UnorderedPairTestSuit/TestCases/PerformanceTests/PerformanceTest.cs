using System;
using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.UnitTests;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    class PerformanceTest<PartType, GlobalType> : MultiAlgorithmTest<int, int>, IDisposable
    {
        int m_loopCount;

        //public string TestName { get; set; } = "Default";

        public PerformanceTest(int loopCount, CorePool<int, int> corePool) : base(corePool)
        {
            m_loopCount = loopCount;
        }

        public virtual void Run(UniquePairTest<int, int> test)
        {
            Log.Log("Initializing test run with {0} elements.", test.m_elements.Length);
            Log.Log("Tests are repeated {0} times.\n", m_loopCount);

            foreach (var distribution in m_distributions)
            {
                Log.Log("Starting \"" + test.TestName + "\" test with \"" + distribution.Key + "\" distribution.");
                test.SetDistribution(distribution.Value);
                RunSingleTest(test);
            }
        }

        protected override void RunSingleTest(UniquePairTest<int, int> test)
        {
            test.m_log = new NoLog();

            TimeSpan duration = new TimeSpan(0);
            TimeSpan minimum = new TimeSpan(long.MaxValue);
            TimeSpan maximum = new TimeSpan(0);

            for (int i = 0; i < m_loopCount; i++)
            {
                TestResult result = test.Run();
                duration += result.Duration;

                if (result.Duration > maximum)
                    maximum = result.Duration;

                if (result.Duration < minimum)
                    minimum = result.Duration;
            }

            duration = new TimeSpan(duration.Ticks / m_loopCount);

            Log.Log("Minimum duration: " + minimum.TotalMilliseconds + "ms");
            Log.Log("Maximum duration: " + maximum.TotalMilliseconds + "ms");
            Log.Log("Average duration: " + duration.TotalMilliseconds + "ms");
            Log.Log("");
        }

        public void Dispose()
        {
            m_corePool.Dispose();
        }
    }
}
