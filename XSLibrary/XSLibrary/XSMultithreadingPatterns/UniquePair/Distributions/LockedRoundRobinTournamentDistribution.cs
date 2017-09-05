namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class LockedRRTDistribution<PartType, GlobalDataType> : ResourceLockDistribution<PartType, GlobalDataType>
    {
        RRTPairing m_pairingLogic = new RRTPairing();

        public LockedRRTDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            bool even = elements.Length % 2 == 0;

            if (even)
                m_pairingLogic.Update(elements.Length);
            else
                m_pairingLogic.Update(elements.Length + 1);

            base.Calculate(elements, globalData);
        }

        protected override void ThreadExecution(object wrappedData)
        {
            DataWrap data = wrappedData as DataWrap;
            for (int step = 0; step < m_pairingLogic.StepCount; step++)
            {
                for (int i = data.threadID; i < m_pairingLogic.PairCount; i += CorePool.CoreCount)
                {
                    CalculatePair(
                        data,
                        m_pairingLogic.PairMatrix[step][i].ID1,
                        m_pairingLogic.PairMatrix[step][i].ID2);
                }
            }

            data.waitHandles[data.threadID].Set();
        }
    }
}
