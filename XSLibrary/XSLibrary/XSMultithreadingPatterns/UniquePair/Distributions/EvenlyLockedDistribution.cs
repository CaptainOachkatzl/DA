namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class EvenlyLockedDistribution<PartType, GlobalDataType> : LockedResourceDistribution<PartType, GlobalDataType>
    {
        public EvenlyLockedDistribution(int coreCount) : base(coreCount)
        {
        }

        protected override void Distribution(int threadID)
        {
            int coreSelect = 0;

            for (int i = 0; i < m_elements.Length - 1; i++)
            {
                for (int j = i + 1; j < m_elements.Length; j++)
                {
                    coreSelect++;   // increment counter

                    // execute calulation on selected core
                    if (coreSelect % CoreCount == threadID)    
                        CalculatePair(threadID, i, j);
                }
            }
        }
    }
}