using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelWriter excelWriter = new ExcelWriter("../../../../testresults.xlsx");

            MatrixPrinter matrixPrinter = new MatrixPrinter();
            matrixPrinter.PrintMatrix(4, 2);
            System.Console.In.ReadLine();

            // validation
            CorePool<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);

            ValidationTest validationTest = new ValidationTest(validationPool);
            validationTest.RunValidation();
            validationPool.Dispose();



            const int loopCount = 100;

            CorePool<int, int> performancePool = new SystemHandledThreadPool<int, int>(4);
            PerformanceTest<int, int> performanceTest = new PerformanceTest<int, int>(loopCount, performancePool);
            performanceTest.excelWriter = excelWriter;

            performanceTest.Run();

            performanceTest.Dispose();

            System.Console.In.ReadLine();
        }
    }
}