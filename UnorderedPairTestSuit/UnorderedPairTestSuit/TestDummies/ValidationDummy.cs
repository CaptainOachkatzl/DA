namespace UnorderedPairTestSuit
{
    class ValidationDummy
    {
        int[] m_calculatedWith;
        public int ElementCount { get { return m_calculatedWith.Length; } }
        public int ElementIndex { get; private set; }

        public ValidationDummy(int elementCount, int index)
        {
            ElementIndex = index;
            m_calculatedWith = new int[elementCount];

            for (int i = 0; i < ElementCount; i++)
            {
                m_calculatedWith[i] = 0;
            }
        }

        public void SetCalculatedWithElement(int otherElementIndex)
        {
            m_calculatedWith[otherElementIndex]++;
        }

        public bool Valid()
        {
            for (int i = 0; i < ElementCount; i++)
            {
                if (m_calculatedWith[i] > 1)
                    return false;

                if ((i == ElementIndex) == (m_calculatedWith[i] == 1))
                    return false;
            }

            return true;
        }
    }
}