using System.Threading;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public class ResourceLockDistribution<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        class DataWrap
        {
            public int threadID;
            public PartType[] parts;
            public GlobalDataType global;
            public Semaphore[] locks;
            public ManualResetEvent[] waitHandles;
        }

        public ResourceLockDistribution(DistributionPool<PartType, GlobalDataType> pool) : base(pool)
        {
        }

        public override void Calculate(PartType[] parts, GlobalDataType globalData)
        {
            Semaphore[] locks = new Semaphore[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                locks[i] = new Semaphore(1, 1);

            ManualResetEvent[] waitHandles = new ManualResetEvent[m_distributionPool.NodeCount];
            for (int i = 0; i < m_distributionPool.NodeCount; i++)
                waitHandles[i] = new ManualResetEvent(false);

            for (int i = 0; i < m_distributionPool.NodeCount; i++)
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

        private void ThreadExecution(object wrappedData)
        {
            DataWrap data = wrappedData as DataWrap;

            for (int i = 0; i < data.parts.Length; i++)
            {
                if (i % m_distributionPool.NodeCount != data.threadID)
                    continue;

                data.locks[i].WaitOne();

                for (int j = i + 1; j < data.parts.Length; j++)
                {
                    data.locks[j].WaitOne();

                    m_distributionPool.DistributeCalculation(
                        data.threadID, 
                        new CalculationPair<PartType, GlobalDataType>(new PartType[1] { data.parts[i] } , new PartType[1] { data.parts[j] }, data.global, false));

                    m_distributionPool.Synchronize(data.threadID);
                    data.locks[j].Release();
                }
               
                data.locks[i].Release();
            }

            data.waitHandles[data.threadID].Set();
        }

        public override void Dispose()
        {
            m_distributionPool.Dispose();
        }
    }
}
