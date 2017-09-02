using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.UnitTests;

namespace UnorderedPairTestSuit
{
    class OutputValidation : UniquePairTest<ValidationDummy, int>
    {
        public OutputValidation(UniquePairDistribution<ValidationDummy, int> distribution, int elementCount) : base(distribution)
        {
            // initialize the validation objects
            m_elements = new ValidationDummy[elementCount];
            for (int i = 0; i < elementCount; i++)
                m_elements[i] = new ValidationDummy(elementCount, i);
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
            // check every validation object and return true if all are valid
            for (int i = 0; i < m_elements.Length; i++)
            {
                if (!m_elements[i].Valid())
                    return false;
            }

            return true;
        }
    }
}
