using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            // validation
            SharedMemoryCores<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);

            ValidationTest validationTest = new ValidationTest(validationPool);
            //validationTest.RunValidation();
            validationPool.Dispose();



            const int loopCount = 1;
            const int elementCount = 100;

            SharedMemoryCores<int, int> performancePool = new ActorPool<int, int>(4, false);
            PerformanceTest<int, int> performanceTest = new PerformanceTest<int, int>(loopCount, performancePool);

            // overhead
            performanceTest.Run(new OverheadTest(elementCount));

            // fixed duration
            const int duration = 3;
            performanceTest.Run(new FixedDurationTest(elementCount, duration));

            // random duration
            const int rndAverage = 3;
            const int rndVarianz = 2;
            performanceTest.Run(new RandomDurationTest(elementCount, rndAverage, rndVarianz));

            // busy duration
            const int difficulty = 1000;
            performanceTest.Run(new BusyDuration(elementCount, difficulty));

            performanceTest.Dispose();

            System.Console.In.ReadLine();
        }
    }
}