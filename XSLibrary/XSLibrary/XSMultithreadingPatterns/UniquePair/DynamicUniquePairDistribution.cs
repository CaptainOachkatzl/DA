using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public abstract class DynamicUniquePairDistribution<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        protected SharedMemoryCores<PartType, GlobalDataType> m_corePool;
        public SharedMemoryCores<PartType, GlobalDataType> CorePool { get { return m_corePool; } }
        public override int CoreCount { get { return CorePool.CoreCount; } }

        public DynamicUniquePairDistribution(SharedMemoryCores<PartType, GlobalDataType> pool)
        {
            m_corePool = pool;
        }

        public override void SetCalculationFunction(PairCalculationFunction function)
        {
            CorePool.SetCalculationFunction(function);
        }

        public override void Dispose()
        {
            m_corePool.Dispose();
        }
    }
}
