using XSLibrary.UnitTests;

namespace UnorderedPairTestSuit
{
    class OutputValidation : UniquePairTest<ValidationDummy, int>
    {
        public OutputValidation() : base()
        {
            TestName = "Output Validation";
        }

        public void SetElementCount(int elementCount)
        {
            // initialize the validation objects
            m_elements = new ValidationDummy[elementCount];
            for (int i = 0; i < elementCount; i++)
                m_elements[i] = new ValidationDummy(elementCount, i);
        }

        protected override void CalculationFunction(ValidationDummy element1, ValidationDummy element2, int global)
        {
            element1.SetCalculatedWithElement(element2.ElementIndex);
            element2.SetCalculatedWithElement(element1.ElementIndex);
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
