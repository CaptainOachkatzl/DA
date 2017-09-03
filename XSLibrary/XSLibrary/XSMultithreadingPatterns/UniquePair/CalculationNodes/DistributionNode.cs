namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    public partial class CorePool<PartType, GlobalDataType>
    {
        public abstract class CalculationCore
        {
            public abstract void CalculatePairedData(PairingData<PartType, GlobalDataType> calculationPair);
        }
    }
}