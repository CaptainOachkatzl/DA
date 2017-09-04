using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    class LockedRoundRobinTournamentDistribution<PartType, GlobalDataType> : UniquePairDistribution<PartType, GlobalDataType>
    {
        public LockedRoundRobinTournamentDistribution(CorePool<PartType, GlobalDataType> pool) : base(pool)
        {
        }

        public override void Calculate(PartType[] elements, GlobalDataType globalData)
        {
            throw new NotImplementedException();
        }
    }
}
