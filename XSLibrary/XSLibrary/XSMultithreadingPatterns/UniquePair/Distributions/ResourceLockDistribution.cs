using System.Threading;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class ResourceLockDistribution<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        protected class DataWrap
        {
            public int threadID;
            public PartType[] parts;
            public GlobalDataType global;
            public Semaphore[] locks;
            public ManualResetEvent[] waitHandles;
        }

        public ResourceLockDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
        }

        public override void Calculate(PartType[] parts, GlobalDataType globalData)
        {
            Semaphore[] locks = new Semaphore[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                locks[i] = new Semaphore(1, 1);

            ManualResetEvent[] waitHandles = new ManualResetEvent[m_corePool.CoreCount];
            for (int i = 0; i < m_corePool.CoreCount; i++)
                waitHandles[i] = new ManualResetEvent(false);

            for (int i = 0; i < m_corePool.CoreCount; i++)
            {
                DataWrap data = new DataWrap();
                data.threadID = i;
                data.parts = parts;
                data.global = globalData;
                data.locks = locks;
                data.waitHandles = waitHandles;
                ThreadPool.QueueUserWorkItem(ThreadExecution, data);
            }

            WaitHandle.WaitAll(waitHandles);
        }

        protected virtual void ThreadExecution(object wrappedData)
        {
            DataWrap data = wrappedData as DataWrap;

            for (int i = data.threadID; i < data.parts.Length - 1; i += m_corePool.CoreCount)
            {
                for (int j = i + 1; j < data.parts.Length; j++)
                {
                    CalculatePair(data, i, j);
                }
            }

            data.waitHandles[data.threadID].Set();
        }

        protected void CalculatePair(DataWrap data, int id1, int id2)
        {
            data.locks[id1].WaitOne();
            data.locks[id2].WaitOne();

            m_corePool.DistributeCalculation(
                        data.threadID,
                        new PairingData<PartType, GlobalDataType>(
                            new PartType[1] { data.parts[id1] }, 
                            new PartType[1] { data.parts[id2] }, 
                            data.global, false));

            m_corePool.Synchronize(data.threadID);

            data.locks[id2].Release();
            data.locks[id1].Release();
        }

        public override void Dispose()
        {
            m_corePool.Dispose();
        }
    }
}