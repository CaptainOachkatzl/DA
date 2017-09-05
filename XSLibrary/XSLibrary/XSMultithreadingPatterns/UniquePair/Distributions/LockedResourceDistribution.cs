using System.Threading;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class LockedResourceDistribution<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        protected PartType[] m_elements;
        protected GlobalDataType m_global;
        protected Semaphore[] m_locks;
        protected ManualResetEvent[] m_waitHandles;

        public LockedResourceDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
            m_waitHandles = new ManualResetEvent[m_corePool.CoreCount];
            for (int i = 0; i < m_corePool.CoreCount; i++)
                m_waitHandles[i] = new ManualResetEvent(false);
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            for (int i = 0; i < m_corePool.CoreCount; i++)
                m_waitHandles[i].Reset();

            if (m_elements == null || m_elements.Length != elements.Length)
            {
                m_locks = new Semaphore[elements.Length];
                for (int i = 0; i < elements.Length; i++)
                    m_locks[i] = new Semaphore(1, 1);
            }

            m_elements = elements;
            m_global = globalData;

            for (int i = 0; i < m_corePool.CoreCount; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadExecution, i);
            }

            WaitHandle.WaitAll(m_waitHandles);
        }

        private void ThreadExecution(object wrappedData)
        {
            int threadID = (int)wrappedData;

            Distribution(threadID);

            m_waitHandles[threadID].Set();
        }

        protected virtual void Distribution(int threadID)
        {
            for (int i = threadID; i < m_elements.Length - 1; i += m_corePool.CoreCount)
            {
                for (int j = i + 1; j < m_elements.Length; j++)
                {
                    CalculatePair(threadID, i, j);
                }
            }
        }

        protected void CalculatePair(int threadID, int id1, int id2)
        {
            m_locks[id1].WaitOne();
            m_locks[id2].WaitOne();

            m_corePool.DistributeCalculation(
                        threadID,
                        new PairingData<PartType, GlobalDataType>(
                            new PartType[1] { m_elements[id1] }, 
                            new PartType[1] { m_elements[id2] }, 
                            m_global, false));

            m_corePool.Synchronize(threadID);

            m_locks[id2].Release();
            m_locks[id1].Release();
        }

        public override void Dispose()
        {
            m_corePool.Dispose();
        }
    }
}