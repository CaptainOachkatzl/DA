using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            // validation

            SharedMemoryCores<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);
            UniquePairDistribution<ValidationDummy, int> validationDistribution = new RoundRobinTournamentDistribution<ValidationDummy, int>(validationPool);
            OutputValidation validation = new OutputValidation(validationDistribution, 4);

            SharedMemoryCores<int, int> cores = new ActorPool<int, int>(4, false);
            UniquePairDistribution<int, int> singleThreadDistribution = new SingleThreadReference<int, int>(cores);
            UniquePairDistribution<int, int> resourceLockDistribution = new ResourceLockDistribution<int, int>(cores);
            UniquePairDistribution<int, int> roundRobinDistribution = new RoundRobinTournamentDistribution<int, int>(cores);


            // performance

            const int loopCount = 100;
            const int elementCount = 16;

            PerformanceTest<int, int> performanceTestSingleThread = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(singleThreadDistribution, elementCount));
            performanceTestSingleThread.TestName = "Single Thread Reference";

            PerformanceTest<int, int> performanceTestResourceLock = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(resourceLockDistribution, elementCount));
            performanceTestResourceLock.TestName = "Resource Lock";

            PerformanceTest<int, int> performanceTestRoundRobin = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(roundRobinDistribution, elementCount));
            performanceTestRoundRobin.TestName = "Round Robin";

            validation.Run();
            //performanceTestSingleThread.Run();
            performanceTestResourceLock.Run();
            //performanceTestRoundRobin.Run();

            cores.Dispose();

            System.Console.In.ReadLine();
        }
    }
}