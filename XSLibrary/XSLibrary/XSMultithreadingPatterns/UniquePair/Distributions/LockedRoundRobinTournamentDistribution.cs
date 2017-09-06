namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class LockedRRTDistribution<PartType, GlobalDataType> : LockedResourceDistribution<PartType, GlobalDataType>
    {
        RRTPairing m_pairingLogic = new RRTPairing();

        int CurrentElementCount { get; set; }
        bool m_even;

        public LockedRRTDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
            CurrentElementCount = -1;
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            if (CurrentElementCount != elements.Length)
            {
                m_even = elements.Length % 2 == 0;

                if (m_even)
                    m_pairingLogic.GenerateMatrix(elements.Length);
                else
                    m_pairingLogic.GenerateMatrix(elements.Length + 1);
            }

            CurrentElementCount = elements.Length;

            base.Calculate(elements, globalData);
        }

        protected override void Distribution(int threadID)
        {
            for (int step = 0; step < m_pairingLogic.StepCount; step++)
            {
                for (int pair = threadID; pair < m_pairingLogic.PairCount; pair += CorePool.CoreCount)
                {
                    int id1 = m_pairingLogic.PairMatrix[step][pair].ID1;
                    int id2 = m_pairingLogic.PairMatrix[step][pair].ID2;

                    if (!m_even && (id1 == CurrentElementCount || id2 == CurrentElementCount))
                        continue;

                    CalculatePair(threadID, id1, id2);
                }
            }
        }
    }
}
