using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public partial class RoundRobinTournamentDistribution<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        RoundRobinTournamentPairing PairLogic { get; set; }
        public int StepCount { get { return PairLogic.StepCount; } }
        int StackCount { get { return PairLogic.StackCount; } }

        PartType[][] Stacks { get; set; }
        GlobalDataType GlobalData { get; set; }

        public RoundRobinTournamentDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
            PairLogic = new RoundRobinTournamentPairing(pool.CoreCount); // needs to be initialized first so all the variables used are intiialized as well
        }

        public override void Calculate(PartType[] parts, GlobalDataType globalData)
        {
            PairLogic.SetElementCount(parts.Length);

            CreateStacks(parts);
            GlobalData = globalData;

            for (int i = 0; i < StepCount; i++)
            {
                CalculateStep(i);
            }
        }

        public override void Dispose()
        {
            m_corePool.Dispose();
        }

        private void CreateStacks(PartType[] parts)
        {
            Stacks = new PartType[StackCount][];

            int stackSize = parts.Length / StackCount;
            int leftover = parts.Length % StackCount;

            for (int i = 0; i < StackCount; i++)
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
            for (int i = 0; i < PairLogic.UsableCoreCount; i++)
            {
                m_corePool.DistributeCalculation(i, CreateCalculationPair(i, step));
            }

            m_corePool.Synchronize();
        }

        private PairingData<PartType, GlobalDataType> CreateCalculationPair(int coreID, int step)
        {
            PairingData <PartType, GlobalDataType> data = new PairingData<PartType, GlobalDataType>(
                Stacks[PairLogic.PairMatrix[step][coreID].ID1],
                Stacks[PairLogic.PairMatrix[step][coreID].ID2],
                GlobalData,
                step == 0);

            return data;
        }
    }
}
