using System.Collections.Generic;
using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class ValidationTest : MultiAlgorithmTest<ValidationDummy, int>
    {
        List<int> m_elementCountList = new List<int>();

        public ValidationTest(CorePool<ValidationDummy, int> corePool) : base(corePool)
        {
            InitializeTests();
        }

        private void InitializeTests()
        {
            m_elementCountList.Add(4);
            m_elementCountList.Add(16);
            m_elementCountList.Add(100);
            m_elementCountList.Add(59);
        }



        public void RunValidation()
        {
            OutputValidation validation = new OutputValidation();

            foreach (int elementCount in m_elementCountList)
            {
                foreach (var distribution in m_distributions)
                {
                    validation.TestName = distribution.Key + " validation with " + elementCount + " elements";

                    validation.SetElementCount(elementCount);
                    validation.SetDistribution(distribution.Value);
                    validation.Run();
                }
            }
        }

        protected override void RunSingleTest(UniquePairTest<ValidationDummy, int> test)
        {
            test.Run();
        }
    }
}
