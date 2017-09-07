﻿namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class LockedRRTDistribution<PartType, GlobalDataType> : LockedResourceDistribution<PartType, GlobalDataType>
    {
        RRTPairing PairLogic { get; set; }

        int CurrentElementCount { get; set; }
        bool m_even;

        public LockedRRTDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
            PairLogic = new RRTPairing();

            CurrentElementCount = -1;
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            if (CurrentElementCount != elements.Length)
            {
                m_even = elements.Length % 2 == 0;

                if (m_even)
                    PairLogic.GenerateMatrix(elements.Length);
                else
                    PairLogic.GenerateMatrix(elements.Length + 1);
            }

            CurrentElementCount = elements.Length;

            base.Calculate(elements, globalData);
        }

        protected override void Distribution(int threadID)
        {
            for (int step = 0; step < PairLogic.StepCount; step++)
            {
                for (int pair = threadID; pair < PairLogic.PairCount; pair += CorePool.CoreCount)
                {
                    int id1 = PairLogic.PairMatrix[step][pair].ID1;
                    int id2 = PairLogic.PairMatrix[step][pair].ID2;

                    if (!m_even && (id1 == CurrentElementCount || id2 == CurrentElementCount))
                        continue;

                    CalculatePair(threadID, id1, id2);
                }
            }
        }
    }
}