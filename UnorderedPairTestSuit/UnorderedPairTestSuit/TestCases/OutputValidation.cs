using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.UnitTests;

namespace UnorderedPairTestSuit
{
    class OutputValidation : UniquePairTest<ValidationDummy, int>
    {
        public OutputValidation(UniquePairDistribution<ValidationDummy, int> distribution, int elementCount) : base(distribution)
        {
            m_parts = new ValidationDummy[elementCount];
            for (int i = 0; i < elementCount; i++)
                m_parts[i] = new ValidationDummy(elementCount, i);
        }

        protected override void CalculationFunction(ValidationDummy part1, ValidationDummy part2, int global)
        {
            part1.SetCalculatedWithElement(part2.ElementIndex);
            part2.SetCalculatedWithElement(part1.ElementIndex);
        }

        protected override void TestRoutine(TestResult result)
        {
            base.TestRoutine(result);

            result.Successful = Valid();
        }

        private bool Valid()
        {
            for (int i = 0; i < m_parts.Length; i++)
            {
                if (!m_parts[i].Valid())
                    return false;
            }

            return true;
        }
    }
}
