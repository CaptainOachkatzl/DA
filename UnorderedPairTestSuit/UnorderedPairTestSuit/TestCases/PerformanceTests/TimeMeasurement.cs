namespace UnorderedPairTestSuit
{
    abstract class TimeMeasurementTest : UniquePairTest<int, int>
    {
        public int ElementCount { get; private set; }

        public TimeMeasurementTest(int elementCount) : base()
        {
            ElementCount = elementCount;
        }

        protected override void Initializing()
        {
            base.Initializing();

            InitializeElements();
        }

        private void InitializeElements()
        {
            m_elements = new int[ElementCount];
            for (int i = 0; i < ElementCount; i++)
            {
                m_elements[i] = i;
            }
        }
    }
}