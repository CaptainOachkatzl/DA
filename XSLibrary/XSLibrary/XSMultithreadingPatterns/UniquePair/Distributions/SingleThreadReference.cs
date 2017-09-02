namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class SingleThreadReference<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        public SingleThreadReference(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
        }

        public override void Calculate(PartType[] parts, GlobalDataType globalData)
        {
            PartType[] part1 = new PartType[1];
            PartType[] part2 = new PartType[1];
            PairingData<PartType, GlobalDataType> pair;

            for (int i = 0; i < parts.Length; i++)
            {
                for (int j = i + 1; j < parts.Length; j++)
                {
                    part1[0] = parts[i];
                    part2[0] = parts[j];
                    pair = new PairingData<PartType, GlobalDataType>(part1, part2, globalData, false);
                    m_corePool.DistributeCalculation(0, pair);
                    m_corePool.Synchronize();
                }
            }
        }
    }
}
