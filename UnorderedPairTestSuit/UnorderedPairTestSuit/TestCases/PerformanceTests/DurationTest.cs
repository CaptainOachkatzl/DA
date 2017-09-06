using System.Threading;

namespace UnorderedPairTestSuit
{
    abstract class DurationTest : TimeMeasurementTest
    {
        protected abstract int Duration { get; }

        public DurationTest(int dummyCount) : base(dummyCount)
        {
        }

        protected override void CalculationFunction(int element1, int element2, int global)
        {
            // fixed duration of the execution
            Thread.Sleep(Duration);
        }
    }
}
