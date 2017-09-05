namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class LockedRRTDistribution<PartType, GlobalDataType> : LockedResourceDistribution<PartType, GlobalDataType>
    {
        RRTPairing m_pairingLogic = new RRTPairing();

        int CurrentElementCount { get; set; }

        public LockedRRTDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
            CurrentElementCount = -1;
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            if (CurrentElementCount != elements.Length)
            {
                bool even = elements.Length % 2 == 0;

                if (even)
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
                    CalculatePair(
                        threadID,
                        m_pairingLogic.PairMatrix[step][pair].ID1,
                        m_pairingLogic.PairMatrix[step][pair].ID2);
                }
            }
        }
    }
}
