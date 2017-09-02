using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public abstract class UniquePairDistribution<PartType, GlobalDataType> : IDisposable
    {
        protected CorePool<PartType, GlobalDataType> m_corePool;
        public CorePool<PartType, GlobalDataType> CorePool { get { return m_corePool; } }

        public UniquePairDistribution(CorePool<PartType, GlobalDataType> pool)
        {
            m_corePool = pool;
        }

        public abstract void Calculate(PartType[] parts, GlobalDataType globalData);

        public virtual void Dispose()
        {
            m_corePool.Dispose();
        }
    }
}
