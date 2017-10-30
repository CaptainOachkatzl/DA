using XSLibrary.ThreadSafety.Containers;
using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class StringCombinations
    {
        UniquePairDistribution<string, SafeList<string>> m_distribution;

        public StringCombinations(int coreCount)
        {
            CorePool<string, SafeList<string>> corePool = new SystemHandledThreadPool<string, SafeList<string>>(coreCount);
            m_distribution = new SynchronizedRRTDistribution<string, SafeList<string>>(corePool);
            m_distribution.SetCalculationFunction(CombineStrings);
        }

        public string[] GetCombinations(string[] inputStrings)
        {
            SafeList<string> result = new SafeList<string>();

            m_distribution.Calculate(inputStrings, result);

            return result.Entries;
        }

        private void CombineStrings(string str1, string str2, SafeList<string> outputList)
        {
            outputList.Add(str1 + str2);
        }
    }
}
