using System.Threading;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class LockedResourceDistribution<PartType, GlobalDataType> : StandaloneDistribution<PartType, GlobalDataType>
    {
        protected PartType[] m_elements;
        protected GlobalDataType m_global;
        protected Semaphore[] m_locks;
        protected ManualResetEvent[] m_waitHandles;

        public LockedResourceDistribution(int coreCount) : base(coreCount)
        {
            m_waitHandles = new ManualResetEvent[CoreCount];
            for (int i = 0; i < CoreCount; i++)
                m_waitHandles[i] = new ManualResetEvent(false);
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            ResetWaitHandles();

            if (m_elements == null || m_elements.Length != elements.Length)
                ResetLocks(elements.Length);

            m_elements = elements;
            m_global = globalData;

            for (int i = 0; i < CoreCount; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadExecution, i);
            }

            WaitHandle.WaitAll(m_waitHandles);
        }

        private void ResetWaitHandles()
        {
            for (int i = 0; i < CoreCount; i++)
                m_waitHandles[i].Reset();
        }

        private void ResetLocks(int elementCount)
        {
            m_locks = new Semaphore[elementCount];
            for (int i = 0; i < elementCount; i++)
                m_locks[i] = new Semaphore(1, 1);
        }

        private void ThreadExecution(object wrappedData)
        {
            int threadID = (int)wrappedData;

            Distribution(threadID);

            m_waitHandles[threadID].Set();
        }

        protected virtual void Distribution(int threadID)
        {
            for (int i = threadID; i < m_elements.Length - 1; i += CoreCount)
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

            CalculationFunction(m_elements[id1], m_elements[id2], m_global);

            m_locks[id2].Release();
            m_locks[id1].Release();
        }

        public override void Dispose()
        {
        }
    }
}