using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public partial class SynchronizedRRTDistribution<PartType, GlobalDataType> : DynamicUniquePairDistribution<PartType, GlobalDataType>
    {
        RRTPairing PairLogic { get; set; }
        public int StepCount { get { return PairLogic.StepCount; } }

        int ElementCount { get; set; }
        int UsableCoreCount { get; set; }

        PartType[][] Stacks { get; set; }
        GlobalDataType GlobalData { get; set; }

        public SynchronizedRRTDistribution(SharedMemoryCores<PartType, GlobalDataType> pool) : base(pool)
        {
            PairLogic = new RRTPairing();
        }

        public override void Calculate(PartType[] parts, GlobalDataType globalData)
        {
            ElementCount = parts.Length;

            int previouslyUsableCores = UsableCoreCount;
            UsableCoreCount = CalculateUsableCoreCount(ElementCount);

            CreateStacks(parts);

            if(previouslyUsableCores != UsableCoreCount)
                PairLogic.GenerateMatrix(UsableCoreCount * 2);

            GlobalData = globalData;

            for (int i = 0; i < StepCount; i++)
            {
                CalculateStep(i);
            }
        }

        private int CalculateUsableCoreCount(int elementCount)
        {
            return Math.Min(CoreCount, elementCount / 2);
        }

        private void CreateStacks(PartType[] parts)
        {
            int stackCount = UsableCoreCount * 2;

            Stacks = new PartType[stackCount][];

            int stackSize = ElementCount / stackCount;
            int leftover = ElementCount % stackCount;

            for (int i = 0; i < stackCount; i++)
            {
                // as there might be numbers of parts which are not divideable cleanly
                // the leftovers get added one by one to the first few stacks

                // e.g. 6 parts divided by 4 stacks would mean Stacks[0] and Stacks[1] would hold 2 values 
                // while Stacks[2] and Stacks[3] hold only one
                if (i < leftover)
                {
                    Stacks[i] = new PartType[stackSize + 1];
                    Array.Copy(parts, i * stackSize + i, Stacks[i], 0, stackSize + 1);
                }
                else
                {
                    Stacks[i] = new PartType[stackSize];
                    Array.Copy(parts, i * stackSize + leftover, Stacks[i], 0, stackSize);
                }
            }
        }

        private void CalculateStep(int step)
        {
            for (int i = 0; i < UsableCoreCount; i++)
            {
                m_corePool.DistributeCalculation(i, CreateCalculationPair(i, step));
            }

            m_corePool.Synchronize();
        }

        private PairingData<PartType, GlobalDataType> CreateCalculationPair(int coreID, int step)
        {
            return new PairingData<PartType, GlobalDataType>(
                Stacks[PairLogic.PairMatrix[step][coreID].ID1],
                Stacks[PairLogic.PairMatrix[step][coreID].ID2],
                GlobalData,
                step == 0);
        }

        public override void Dispose()
        {
            m_corePool.Dispose();
        }
    }
}
