namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public abstract class StaticUniquePairDistribution<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        protected PairCalculationFunction CalculationFunction { get; private set; }
        private int m_coreCount;

        public override int CoreCount { get { return m_coreCount; } }

        public StaticUniquePairDistribution(int coreCount)
        {
            m_coreCount = coreCount;
        }

        public override void SetCalculationFunction(PairCalculationFunction function)
        {
            CalculationFunction = function;
        }
    }
}
