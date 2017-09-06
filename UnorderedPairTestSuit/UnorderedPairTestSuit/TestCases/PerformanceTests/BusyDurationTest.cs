using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class BusyDuration : SinglePerformanceTest
    {
        int Difficulty { get; set; }
        int LastSum { get; set; }

        public BusyDuration(int dummyCount, int difficulty) : base(dummyCount)
        {
            TestName = "Busy Duration";
            Difficulty = difficulty;
        }

        protected override void CalculationFunction(int part1, int part2, int global)
        {
            int sum = 0;
            for (int i = 0; i < Difficulty; i++)
            {
                sum += part1 * part2;
            }

            LastSum = sum;
        }
    }
}
