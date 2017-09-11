using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public abstract class UniquePairDistribution<PartType, GlobalDataType> : IDisposable
    {
        public delegate void PairCalculationFunction(PartType element1, PartType element2, GlobalDataType globalData);

        public abstract int CoreCount { get; }

        public UniquePairDistribution()
        {
        }

        public abstract void SetCalculationFunction(PairCalculationFunction function);

        public abstract void Calculate(PartType[] elements, GlobalDataType globalData);

        public virtual void Dispose()
        {
        }
    }
}
