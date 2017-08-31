﻿using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public abstract partial class CorePool<PartType, GlobalDataType> : IDisposable
    {
        public delegate void NodesChangedHandler(object sender, EventArgs e);
        public event NodesChangedHandler OnNodesChanged;

        public abstract int CoreCount { get; }

        public abstract void DistributeCalculation(int nodeIndex, CalculationPair<PartType, GlobalDataType> calculationPair);

        public abstract void SetUsableCores(int coreCount);

        public abstract void Synchronize();
        public abstract void Synchronize(int nodeIndex);

        public abstract void Dispose();
    }
}
