namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class SingleThreadReference<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        public SingleThreadReference(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            // initialize arrays with just one entry
            PartType[] element1 = new PartType[1];
            PartType[] elememt2 = new PartType[1];

            PairingData<PartType, GlobalDataType> pair;

            for (int i = 0; i < elements.Length - 1; i++)
            {
                for (int j = i + 1; j < elements.Length; j++)
                {
                    // Set values in the arrays
                    element1[0] = elements[i];
                    elememt2[0] = elements[j];

                    // Create instruction for the core pool
                    pair = new PairingData<PartType, GlobalDataType>(element1, elememt2, globalData, false);

                    // Calculate the instruction on Core 0
                    m_corePool.DistributeCalculation(0, pair);

                    // Wait until Core 0 finished
                    m_corePool.Synchronize(0);
                }
            }
        }
    }
}