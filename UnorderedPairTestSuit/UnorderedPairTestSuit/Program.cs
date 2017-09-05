using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            // validation

            SharedMemoryCores<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);
            UniquePairDistribution<ValidationDummy, int> lockedRoundRobin = new LockedRRTDistribution<ValidationDummy, int>(validationPool);
            OutputValidation lockedRRTValdiation = new OutputValidation(lockedRoundRobin, 16);
            lockedRRTValdiation.TestName = "Locked Round Robin Validation";

            UniquePairDistribution<ValidationDummy, int> roundRobin = new RRTDistribution<ValidationDummy, int>(validationPool);
            OutputValidation RRTValdiation = new OutputValidation(roundRobin, 16);
            RRTValdiation.TestName = "Round Robin Validation";

            lockedRRTValdiation.Run();
            RRTValdiation.Run();

            // performance

            const int loopCount = 100;
            const int elementCount = 16;

            SharedMemoryCores<int, int> cores = new ActorPool<int, int>(4, false);
            
            UniquePairDistribution<int, int> singleThreadDistribution = new SingleThreadReference<int, int>(cores);
            PerformanceTest<int, int> performanceTestSingleThread = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(singleThreadDistribution, elementCount));
            performanceTestSingleThread.TestName = "Single Thread Reference";

            UniquePairDistribution<int, int> resourceLockDistribution = new ResourceLockDistribution<int, int>(cores);
            PerformanceTest<int, int> performanceTestResourceLock = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(resourceLockDistribution, elementCount));
            performanceTestResourceLock.TestName = "Resource Lock";

            UniquePairDistribution<int, int> lockedRRTperformance = new RRTDistribution<int, int>(cores);
            PerformanceTest<int, int> performanceTestlockedRRT = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(lockedRRTperformance, elementCount));
            performanceTestlockedRRT.TestName = "Locked Round Robin Tournament";

            UniquePairDistribution<int, int> roundRobinDistribution = new RRTDistribution<int, int>(cores);
            PerformanceTest<int, int> performanceTestRoundRobin = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(roundRobinDistribution, elementCount));
            performanceTestRoundRobin.TestName = "Round Robin Tournament";

            //performanceTestSingleThread.Run();
            //performanceTestResourceLock.Run();
            performanceTestlockedRRT.Run();
            performanceTestRoundRobin.Run();

            cores.Dispose();

            System.Console.In.ReadLine();
        }
    }
}