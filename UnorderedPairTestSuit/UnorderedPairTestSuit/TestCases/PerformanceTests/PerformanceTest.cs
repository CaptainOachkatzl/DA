using System;
using System.Collections.Generic;
using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.UnitTests;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    class PerformanceTest<PartType, GlobalType> : MultiAlgorithmTest<int, int>, IDisposable
    {
        int m_loopCount;
        public ExcelWriter excelWriter { get; set; }

        List<UniquePairTest<int, int>> m_tests = new List<UniquePairTest<int, int>>();

        public PerformanceTest(int loopCount, CorePool<int, int> corePool) : base(corePool)
        {
            m_loopCount = loopCount;
            CreateTests();
        }

        private void CreateTests()
        {
            const int countRun1 = 16;
            const int countRun2 = 128;
            const int countRun3 = 1024;

            m_tests.Add(new OverheadTest(countRun1));
            m_tests.Add(new OverheadTest(countRun2));
            m_tests.Add(new OverheadTest(countRun3));

            // fixed duration
            const int duration = 2;
            m_tests.Add(new FixedDurationTest(countRun1, duration));
            m_tests.Add(new FixedDurationTest(countRun2, duration));

            // random duration
            const int rndAverage = 2;
            const int rndVarianz = 2;
            m_tests.Add(new RandomDurationTest(countRun1, rndAverage, rndVarianz));
            m_tests.Add(new RandomDurationTest(countRun2, rndAverage, rndVarianz));

            // busy duration
            const int difficulty = 1024;
            m_tests.Add(new BusyDuration(countRun1, difficulty));
            m_tests.Add(new BusyDuration(countRun2, difficulty));
            m_tests.Add(new BusyDuration(countRun3, difficulty));
        }

        public virtual void Run()
        {
            Log.Log("\nStarting Performance tests...\nTests are repeated {0} times.", m_loopCount);

            foreach (var distribution in m_distributions)
            {
                Log.Log("\n---------------------------------------------------------------------------------");
                Log.Log("Initializing test run with \"{0}\" distribution.\n", distribution.Key);
                
                foreach (UniquePairTest<int, int> test in m_tests)
                {
                    Log.Log("Starting \"" + test.TestName + "\" test with \"" + test.m_elements.Length + "\" elements.");
                    test.SetDistribution(distribution.Value);
                    RunSingleTest(test);
                }
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

            if(excelWriter != null)
            {
                excelWriter.WriteTestData(
                    new ExcelWriter.TestData
                    {
                        m_testName = test.TestName,

                        m_average = duration,
                        m_maximum = maximum,
                        m_minimum = minimum
                    });
            }
        }

        public void Dispose()
        {
            m_corePool.Dispose();
        }
    }
}
