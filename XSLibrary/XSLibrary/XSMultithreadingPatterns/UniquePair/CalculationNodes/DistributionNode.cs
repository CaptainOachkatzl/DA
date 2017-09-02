namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public partial class CorePool<PartType, GlobalDataType>
    {
        public abstract class DistributionNode
        {
            public abstract void CalculateStacks(PairingData<PartType, GlobalDataType> calculationPair);
        }
    }
}