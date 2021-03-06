﻿using System.Collections.Generic;
using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    abstract class MultiAlgorithmTest<PartType, GlobalDataType>
    {
        protected Dictionary<string, UniquePairDistribution<PartType, GlobalDataType>> m_distributions = new Dictionary<string, UniquePairDistribution<PartType, GlobalDataType>>();
        protected CorePool<PartType, GlobalDataType> m_corePool;

        protected Logger Log = new LoggerConsole();

        public MultiAlgorithmTest(CorePool<PartType, GlobalDataType> corePool)
        {
            m_corePool = corePool;

            InitializeTests();
        }

        private void InitializeTests()
        {
            m_distributions.Add("Single Thread Reference", new SingleThreadReference<PartType, GlobalDataType>());
            m_distributions.Add("Locked Resource", new ParallelLoopDistribution<PartType, GlobalDataType>(m_corePool.CoreCount));
            m_distributions.Add("Evenly Locked", new EvenlyLockedDistribution<PartType, GlobalDataType>(m_corePool.CoreCount));
            m_distributions.Add("Locked Round Robin Tournament", new LockedRRTDistribution<PartType, GlobalDataType>(m_corePool.CoreCount));
            m_distributions.Add("Synchronized Round Robin Tournament", new SynchronizedRRTDistribution<PartType, GlobalDataType>(m_corePool));
        }
    }
}
