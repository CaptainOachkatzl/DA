using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            SharedMemoryCores<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);
            UniquePairDistribution<ValidationDummy, int> validationDistribution = new ResourceLockDistribution<ValidationDummy, int>(validationPool);
            OutputValidation validation = new OutputValidation(validationDistribution, 100);


            SharedMemoryCores<int, int> cores = new ActorPool<int, int>(4, false);
            UniquePairDistribution<int, int> distribution = new SingleThreadReference<int, int>(cores);

            PerformanceTest<int, int> performanceTest = new PerformanceTest<int, int>(4, new FixedDurationTest(distribution, 16));

            validation.Run();
            performanceTest.Run();      

            System.Console.In.Read();
        }
    }
}