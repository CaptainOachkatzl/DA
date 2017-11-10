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
            //Console.In.ReadLine();

            // RRTA matrix
            MatrixPrinter matrixPrinter = new MatrixPrinter();
            matrixPrinter.PrintMatrix(8, 4);
            //Console.In.ReadLine();

            // validation
            CorePool<ValidationDummy, int> validationPool = new SystemHandledThreadPool<ValidationDummy, int>(4);

            ValidationTest validationTest = new ValidationTest(validationPool);
            validationTest.RunValidation();
            validationPool.Dispose();
            //Console.In.ReadLine();

            // performance
            const int loopCount = 1;

            // dual core
            CorePool<int, int> dualCorePool = new SystemHandledThreadPool<int, int>(2);
            PerformanceTest<int, int> dualCoreTest = new PerformanceTest<int, int>(loopCount, dualCorePool);
            dualCoreTest.excelWriter = excelWriter;
            dualCoreTest.Run();
            dualCoreTest.Dispose();

            // tri core
            CorePool<int, int> triCorePool = new SystemHandledThreadPool<int, int>(3);
            PerformanceTest<int, int> triCoreTest = new PerformanceTest<int, int>(loopCount, triCorePool);
            triCoreTest.excelWriter = excelWriter;
            triCoreTest.Run();
            triCoreTest.Dispose();

            // quad core
            CorePool<int, int> quadCorePool = new SystemHandledThreadPool<int, int>(4);
            PerformanceTest<int, int> quadCoreTest = new PerformanceTest<int, int>(loopCount, quadCorePool);
            quadCoreTest.excelWriter = excelWriter;
            quadCoreTest.Run();
            quadCoreTest.Dispose();

            Console.In.ReadLine();
        }
    }
}