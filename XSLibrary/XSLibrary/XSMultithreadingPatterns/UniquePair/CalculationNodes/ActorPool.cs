﻿using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public partial class ActorPool<PartType, GlobalDataType> : SharedMemoryCores<PartType, GlobalDataType>
    {
        public override int CoreCount { get { return PoolSize; } }
        private int PoolSize { get; set; }
        public bool FixedCores { get; private set; }
        ActorNode[] Actors { get; set; }

        public ActorPool(int size, bool fixedCores) : base(size)
        {
            PoolSize = size;
            FixedCores = fixedCores;
            InitializeActors();
        }

        public override void DistributeCalculation(int nodeIndex, CalculationPair<PartType, GlobalDataType> calculationPair)
        {
            Actors[nodeIndex].CalculateStacks(calculationPair);
        }

        public override void SetUsableCores(int coreCount)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            Close(true);
        }

        public void Close(bool join = false)
        {
            foreach (ActorNode actor in Actors)
            {
                actor.Stop();
            }

            if (join)
                Synchronize();
        }

        private void InitializeActors()
        {
            Actors = new ActorNode[PoolSize];
            for (int i = 0; i < PoolSize; i++)
            {
                Actors[i] = new ActorNode(CalculationLogic, ResetEvents[i], FixedCores ? i : -1);
            }
        }
    }
}
