﻿using XSLibrary.MultithreadingPatterns.UniquePair;

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



            const int loopCount = 100;
            const int elementCount = 16;

            SharedMemoryCores<int, int> performancePool = new ActorPool<int, int>(4, false);
            PerformanceTest<int, int> performanceTest = new PerformanceTest<int, int>(loopCount, performancePool);

            // fixed duration
            const int duration = 10;
            performanceTest.Run(new FixedDurationTest(elementCount, duration));

            // random duration
            const int rndAverage = 10;
            const int rndVarianz = 10;
            performanceTest.Run(new RandomDurationTest(elementCount, rndAverage, rndVarianz));

            // busy duration
            const int difficulty = 0;
            performanceTest.Run(new BusyDuration(elementCount, difficulty));

            performanceTest.Dispose();

            System.Console.In.ReadLine();
        }
    }
}