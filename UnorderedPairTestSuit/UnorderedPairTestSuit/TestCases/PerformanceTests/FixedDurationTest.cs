namespace UnorderedPairTestSuit
{
    class FixedDurationTest : DurationTest
    {
        int m_duration;

        protected override int Duration { get { return m_duration; } }

        public FixedDurationTest(int dummyCount, int durationMilliSeconds) : base(dummyCount)
        {
            TestName = "Fixed Duration";
            m_duration = durationMilliSeconds;
        }
    }
}