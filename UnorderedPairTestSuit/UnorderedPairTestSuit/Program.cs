using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            // validation
            const int valdiationSize = 144;

            SharedMemoryCores<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);

            UniquePairDistribution<ValidationDummy, int> evenlyLocked = new EvenlyLockedDistribution<ValidationDummy, int>(validationPool);
            OutputValidation evenlyLockedValidation = new OutputValidation(evenlyLocked, valdiationSize);
            evenlyLockedValidation.TestName = "Evenly Locked Validation";

            UniquePairDistribution<ValidationDummy, int> lockedRoundRobin = new LockedRRTDistribution<ValidationDummy, int>(validationPool);
            OutputValidation lockedRRTValdiation = new OutputValidation(lockedRoundRobin, valdiationSize);
            lockedRRTValdiation.TestName = "Locked Round Robin Validation";

            UniquePairDistribution<ValidationDummy, int> roundRobin = new RRTDistribution<ValidationDummy, int>(validationPool);
            OutputValidation RRTValdiation = new OutputValidation(roundRobin, valdiationSize);
            RRTValdiation.TestName = "Round Robin Validation";

            evenlyLockedValidation.Run();
            lockedRRTValdiation.Run();
            RRTValdiation.Run();

            validationPool.Dispose();


            // fixed duration

            SharedMemoryCores<int, int> performancePool = new ActorPool<int, int>(4, false);

            const int loopCount = 100;
            const int elementCount = 16;
            const int duration = 10;

            UniquePairDistribution<int, int> singleThreadDistribution = new SingleThreadReference<int, int>(performancePool);
            UniquePairDistribution<int, int> lockedResourceDistribution = new LockedResourceDistribution<int, int>(performancePool);
            UniquePairDistribution<int, int> evenlyLockedDistribution = new EvenlyLockedDistribution<int, int>(performancePool);
            UniquePairDistribution<int, int> lockedRRTperformance = new LockedRRTDistribution<int, int>(performancePool);
            UniquePairDistribution<int, int> RRTperformance = new RRTDistribution<int, int>(performancePool);

            PerformanceTest<int, int> performanceTestSingleThread = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(singleThreadDistribution, elementCount, duration));
            performanceTestSingleThread.TestName = "Single Thread Reference";

            PerformanceTest<int, int> performanceTestResourceLock = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(lockedResourceDistribution, elementCount, duration));
            performanceTestResourceLock.TestName = "Locked Resource";

            PerformanceTest<int, int> performanceTestEvenlyLocked = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(lockedResourceDistribution, elementCount, duration));
            performanceTestEvenlyLocked.TestName = "Evenly Locked";

            PerformanceTest<int, int> performanceTestlockedRRT = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(lockedRRTperformance, elementCount, duration));
            performanceTestlockedRRT.TestName = "Locked Round Robin Tournament";

            PerformanceTest<int, int> performanceTestRoundRobin = new PerformanceTest<int, int>(
                loopCount, new FixedDurationTest(RRTperformance, elementCount, duration));
            performanceTestRoundRobin.TestName = "Round Robin Tournament";

            //performanceTestSingleThread.Run();
            performanceTestResourceLock.Run();
            performanceTestEvenlyLocked.Run();
            //performanceTestlockedRRT.Run();
            //performanceTestRoundRobin.Run();


            // random duration

            const int rndAverage = 10;
            const int rndVarianz = 10;

            PerformanceTest<int, int> rndlockedRRT = new PerformanceTest<int, int>(
                loopCount, 
                new RandomDurationTest(lockedRRTperformance, elementCount, rndAverage, rndVarianz));
            rndlockedRRT.TestName = "Random Duration Locked RRT";

            PerformanceTest<int, int> rndRRT = new PerformanceTest<int, int>(
                loopCount,
                new RandomDurationTest(RRTperformance, elementCount, rndAverage, rndVarianz));
            rndRRT.TestName = "Random Duration RRT";

            //rndlockedRRT.Run();
            //rndRRT.Run();


            // busy duration
            const int difficulty = 0;

            PerformanceTest<int, int> busyLockedRRT = new PerformanceTest<int, int>(
               loopCount,
               new BusyDuration(lockedRRTperformance, elementCount, difficulty));
            busyLockedRRT.TestName = "Busy Duration Locked RRT";

            PerformanceTest<int, int> busyRRT = new PerformanceTest<int, int>(
               loopCount,
               new BusyDuration(RRTperformance, elementCount, difficulty));
            busyRRT.TestName = "Busy Duration RRT";

            busyLockedRRT.Run();
            busyRRT.Run();

            performancePool.Dispose();

            System.Console.In.ReadLine();
        }
    }
}