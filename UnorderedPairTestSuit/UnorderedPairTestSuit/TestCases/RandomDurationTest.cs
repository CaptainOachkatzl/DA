using System;
using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class RandomDurationTest : DurationTest
    {
        Random rnd = new Random();

        int m_average;
        int m_varianz;

        protected override int Duration { get { return rnd.Next(m_average - m_varianz / 2, m_average + m_varianz / 2); } }

        public RandomDurationTest(UniquePairDistribution<int, int> distribution, int dummyCount, int average, int varianz) : base(distribution, dummyCount)
        {
            m_average = average;
            m_varianz = varianz;
        }
    }
}
