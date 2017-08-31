using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            SharedMemoryCores<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);
            UniquePairDistribution<ValidationDummy, int> validationDistribution = new RoundRobinTournamentDistribution<ValidationDummy, int>(validationPool);
            OutputValidation validation = new OutputValidation(validationDistribution, 100);


            SharedMemoryCores<CalculationDummy, int> cores = new ActorPool<CalculationDummy, int>(4, false);
            UniquePairDistribution<CalculationDummy, int> distribution = new RoundRobinTournamentDistribution<CalculationDummy, int>(cores);

            ArithmeticAverageTest<CalculationDummy, int> averageTest = new ArithmeticAverageTest<CalculationDummy, int>(4, new UniquePairTest_Dummy(distribution, 16));

            validation.Run();
            averageTest.Run();      

            System.Console.In.Read();
        }
    }
}
