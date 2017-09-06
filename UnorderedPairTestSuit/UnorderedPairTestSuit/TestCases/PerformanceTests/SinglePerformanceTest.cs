namespace UnorderedPairTestSuit
{
    abstract class SinglePerformanceTest : UniquePairTest<int, int>
    {
        public SinglePerformanceTest(int dummyCount) : base()
        {
            // initialize elements
            m_elements = new int[dummyCount];
            for (int i = 0; i < dummyCount; i++)
            {
                m_elements[i] = i;
            }
        }
    }
}
