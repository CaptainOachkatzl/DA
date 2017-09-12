namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class LockedResourceDistribution<PartType, GlobalDataType> : LockingDistribution<PartType, GlobalDataType>
    {
        public LockedResourceDistribution(int coreCount) : base(coreCount)
        {
        }

        protected override void Distribute(int coreID)
        {
            for (int i = coreID; i < m_elements.Length - 1; i += CoreCount)
            {
                for (int j = i + 1; j < m_elements.Length; j++)
                {
                    CalculatePair(i, j);
                }
            }
        }
    }
}