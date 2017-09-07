using UnorderedPairTestSuit;

namespace UnorderedPairTestSuit
{
    abstract class TimeMeasurementTest : UniquePairTest<int, int>
    {
        public TimeMeasurementTest(int elementCount) : base()
        {
            // initialize elements
            m_elements = new int[elementCount];
            for (int i = 0; i < elementCount; i++)
            {
                m_elements[i] = i;
            }
        }
    }
}