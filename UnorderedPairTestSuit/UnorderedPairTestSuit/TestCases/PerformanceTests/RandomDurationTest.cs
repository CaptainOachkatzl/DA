using System;

namespace UnorderedPairTestSuit
{
    class RandomDurationTest : DurationTest
    {
        Random rnd = new Random();

        int m_average;
        int m_varianz;

        protected override int Duration { get { return rnd.Next(m_average - m_varianz / 2, m_average + m_varianz / 2); } }

        public RandomDurationTest(int dummyCount, int average, int varianz) : base(dummyCount)
        {
            TestName = "Random Duration";

            m_average = average;
            m_varianz = varianz;
        }
    }
}
