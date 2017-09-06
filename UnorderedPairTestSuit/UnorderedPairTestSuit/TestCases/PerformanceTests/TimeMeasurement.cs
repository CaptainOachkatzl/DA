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

    protected abstract void CalculationFunction(int element1, int element2, int global);
}