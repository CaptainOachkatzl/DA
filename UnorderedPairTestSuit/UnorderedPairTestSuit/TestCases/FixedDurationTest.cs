using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class FixedDurationTest : DurationTest
    {
        int m_duration;

        protected override int Duration { get { return m_duration; } }

        public FixedDurationTest(UniquePairDistribution<int, int> distribution, int dummyCount, int durationMilliSeconds) : base(distribution, dummyCount)
        {
            m_duration = durationMilliSeconds;
        }
    }
}