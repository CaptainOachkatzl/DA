namespace UnorderedPairTestSuit
{
    class ValidationDummy
    {
        int[] m_wasCalculatedWith;
        int m_elementCount;
        public int ElementIndex { get; private set; }

        public ValidationDummy(int elementCount, int index)
        {
            m_elementCount = elementCount;
            ElementIndex = index;
            m_wasCalculatedWith = new int[elementCount];

            for (int i = 0; i < m_elementCount; i++)
            {
                m_wasCalculatedWith[i] = 0;
            }
        }

        public void SetCalculatedWithElement(int otherElementIndex)
        {
            m_wasCalculatedWith[otherElementIndex]++;
        }

        public bool Valid()
        {
            for (int i = 0; i < m_elementCount; i++)
            {
                if (m_wasCalculatedWith[i] > 1)
                    return false;

                if ((i == ElementIndex) == (m_wasCalculatedWith[i] == 1))
                    return false;
            }

            return true;
        }
    }
}
