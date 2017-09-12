namespace UnorderedPairTestSuit
{
    class OverheadTest : TimeMeasurementTest
    {
        public OverheadTest(int dummyCount) : base(dummyCount)
        {
            TestName = "Overhead";
        }

        protected override void CalculationFunction(int element1, int element2, int global)
        {
            // dont do anything to keep the load at a minimum
        }
    }
}