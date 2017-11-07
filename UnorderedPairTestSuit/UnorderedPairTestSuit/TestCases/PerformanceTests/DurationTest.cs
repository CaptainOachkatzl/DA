using System.Threading;

namespace UnorderedPairTestSuit
{
    abstract class DurationTest : TimeMeasurementTest
    {
        protected abstract int Duration { get; }
        ManualResetEvent m_resetEvent;

        public DurationTest(int dummyCount) : base(dummyCount)
        {
            m_resetEvent = new ManualResetEvent(false);
        }

        protected override void CalculationFunction(int element1, int element2, int global)
        {
            // fixed duration of the execution
            m_resetEvent.WaitOne(Duration);
        }
    }
}
