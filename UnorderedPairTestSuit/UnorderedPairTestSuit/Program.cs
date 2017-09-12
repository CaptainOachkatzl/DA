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
            CorePool<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);

            ValidationTest validationTest = new ValidationTest(validationPool);
            validationTest.RunValidation();
            validationPool.Dispose();



            const int loopCount = 100;
            const int countRun1 = 16;
            const int countRun2 = 128;
            const int countRun3 = 1024;

            CorePool<int, int> performancePool = new SystemHandledThreadPool<int, int>(4);
            PerformanceTest<int, int> performanceTest = new PerformanceTest<int, int>(loopCount, performancePool);

            // overhead
            performanceTest.Run(new OverheadTest(countRun1));
            performanceTest.Run(new OverheadTest(countRun2));
            performanceTest.Run(new OverheadTest(countRun3));

            // fixed duration
            const int duration = 2;
            performanceTest.Run(new FixedDurationTest(countRun1, duration));
            performanceTest.Run(new FixedDurationTest(countRun2, duration));

            // random duration
            const int rndAverage = 2;
            const int rndVarianz = 2;
            performanceTest.Run(new RandomDurationTest(countRun1, rndAverage, rndVarianz));
            performanceTest.Run(new RandomDurationTest(countRun2, rndAverage, rndVarianz));

            // busy duration
            const int difficulty = 1024;
            performanceTest.Run(new BusyDuration(countRun1, difficulty));
            performanceTest.Run(new BusyDuration(countRun2, difficulty));
            performanceTest.Run(new BusyDuration(countRun3, difficulty));

            performanceTest.Dispose();

            System.Console.In.ReadLine();
        }
    }
}