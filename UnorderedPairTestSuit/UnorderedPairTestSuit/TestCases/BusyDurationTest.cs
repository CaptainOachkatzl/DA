using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class BusyDuration : UniquePairTest<int, int>
    {
        int Difficulty { get; set; }
        int LastSum { get; set; }

        public BusyDuration(UniquePairDistribution<int, int> distribution, int dummyCount, int difficulty) : base(distribution)
        {
            Difficulty = difficulty;

            // initialize elements
            m_elements = new int[dummyCount];
            for (int i = 0; i < dummyCount; i++)
            {
                m_elements[i] = i;
            }
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
