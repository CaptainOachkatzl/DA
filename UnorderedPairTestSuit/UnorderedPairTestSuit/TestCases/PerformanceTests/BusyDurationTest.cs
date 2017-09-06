using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class BusyDuration : TimeMeasurementTest
    {
        int Difficulty { get; set; }
        int LastSum { get; set; }

        public BusyDuration(int dummyCount, int difficulty) : base(dummyCount)
        {
            TestName = "Busy Duration";
            Difficulty = difficulty;
        }

        protected override void CalculationFunction(int element1, int element2, int global)
        {
            int sum = 0;
            for (int i = 0; i < Difficulty; i++)
            {
                sum += element1 * element2;
            }

            LastSum = sum;
        }
    }
}
