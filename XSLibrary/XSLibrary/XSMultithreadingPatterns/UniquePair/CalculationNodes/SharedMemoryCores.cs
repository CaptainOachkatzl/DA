using System.Threading;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    abstract public class SharedMemoryCores<PartType, GlobalDataType> : CorePool<PartType, GlobalDataType>
    {
        protected ManualResetEvent[] ResetEvents { get; set; }
        protected SharedMemoryStackCalculation<PartType, GlobalDataType> CalculationLogic { get; private set; }

        public SharedMemoryCores(int coreCount)
        {
            CalculationLogic = new SharedMemoryStackCalculation<PartType, GlobalDataType>();

            ResetEvents = new ManualResetEvent[coreCount];
            for (int i = 0; i < coreCount; i++)
            {
                ResetEvents[i] = new ManualResetEvent(true);
            }
        }

        public void SetCalculationFunction(SharedMemoryStackCalculation<PartType, GlobalDataType>.PairCalculationFunction calculationFunction)
        {
            CalculationLogic.SetCalculationFunction(calculationFunction);
        }

        public override void Synchronize()
        {
            WaitHandle.WaitAll(ResetEvents);
        }

        public override void Synchronize(int nodeIndex)
        {
            ResetEvents[nodeIndex].WaitOne();
        }
    }
}
