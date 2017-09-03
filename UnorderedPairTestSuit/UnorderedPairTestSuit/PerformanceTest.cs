using System;
using XSLibrary.UnitTests;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    class PerformanceTest<PartType, GlobalType> : IDisposable
    {
        UniquePairTest<PartType, GlobalType> m_test;
        int m_loopCount;
        Logger Log = new LoggerConsole();

        public string TestName { get; set; } = "Default";

        public PerformanceTest(int loopCount, UniquePairTest<PartType, GlobalType> test)
        {
            m_loopCount = loopCount;
            m_test = test;
            m_test.m_log = new NoLog();
        }

        public void Run()
        {
            Log.Log("Starting performance test \""+ TestName + "\"");

            TimeSpan duration = new TimeSpan(0);
            TimeSpan minimum = new TimeSpan(long.MaxValue);
            TimeSpan maximum = new TimeSpan(0);

            for (int i = 0; i < m_loopCount; i++)
            {
                TestResult result = m_test.Run();
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
            m_test.Dispose();
        }
    }
}
