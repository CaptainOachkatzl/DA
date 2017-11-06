using System;
using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelWriter excelWriter = new ExcelWriter("../../../../testresults.xlsx");

            // String combinations

            StringCombinations stringCombinations = new StringCombinations(4);

            const int stringEntryCount = 26;
            string[] inputStrings = new string[stringEntryCount];
            for (int i = 0; i < stringEntryCount; i++)
            {
                inputStrings[i] = ((char)('A' + i)).ToString();
            }

            string[] output = stringCombinations.GetCombinations(inputStrings);

            foreach(string str in output)
            {
                Console.Out.Write(str + "\t");
            }
            Console.In.ReadLine();

            // RRTA matrix
            MatrixPrinter matrixPrinter = new MatrixPrinter();
            matrixPrinter.PrintMatrix(8, 4);
            Console.In.ReadLine();

            // validation
            CorePool<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);

            ValidationTest validationTest = new ValidationTest(validationPool);
            validationTest.RunValidation();
            validationPool.Dispose();

            Console.In.ReadLine();

            const int loopCount = 100;

            CorePool<int, int> performancePool = new SystemHandledThreadPool<int, int>(2);
            PerformanceTest<int, int> performanceTest = new PerformanceTest<int, int>(loopCount, performancePool);
            performanceTest.excelWriter = excelWriter;

            performanceTest.Run();

            performanceTest.Dispose();

            Console.In.ReadLine();
        }
    }
}