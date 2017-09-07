using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            MatrixPrinter matrixPrinter = new MatrixPrinter();
            matrixPrinter.PrintMatrix(4);
            System.Console.In.ReadLine();

            // validation
            SharedMemoryCores<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);

            ValidationTest validationTest = new ValidationTest(validationPool);
            validationTest.RunValidation();
            validationPool.Dispose();



            const int loopCount = 10;
            const int elementCount = 100;

            SharedMemoryCores<int, int> performancePool = new ActorPool<int, int>(4, false);
            PerformanceTest<int, int> performanceTest = new PerformanceTest<int, int>(loopCount, performancePool);

            // overhead
            performanceTest.Run(new OverheadTest(1000));

            // fixed duration
            const int duration = 10;
            performanceTest.Run(new FixedDurationTest(16, duration));

            // random duration
            const int rndAverage = 10;
            const int rndVarianz = 10;
            performanceTest.Run(new RandomDurationTest(16, rndAverage, rndVarianz));

            // busy duration
            const int difficulty = 1000;
            performanceTest.Run(new BusyDuration(1000, difficulty));

            performanceTest.Dispose();

            System.Console.In.ReadLine();
        }
    }
}