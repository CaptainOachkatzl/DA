using System;
using XSLibrary.MultithreadingPatterns.UniquePair;
using XSLibrary.Utility;

namespace UnorderedPairTestSuit
{
    class MatrixPrinter
    {
        RRTPairing PairLogic { get; set; }
        Logger Log { get; set; }

        public MatrixPrinter()
        {
            PairLogic = new RRTPairing();
            Log = new LoggerConsole();
        }

        public void PrintMatrix(int elementCount)
        {
            PairLogic.GenerateMatrix(elementCount);

            for (int step = 0; step < PairLogic.StepCount; step++)
            {
                if (step > 0)
                    Log.Log("\n");

                Log.Log("step " + step + "\t\t");

                for (int pair = 0; pair < PairLogic.PairCount; pair++)
                {
                    int id1 = PairLogic.PairMatrix[step][pair].ID1;
                    int id2 = PairLogic.PairMatrix[step][pair].ID2;

                    if (id1 < id2)
                        Log.Log("{ " + id1 + ", " + id2 + " }\t");
                    else
                        Log.Log("{ " + id2 + ", " + id1 + " }\t");
                }
            }

            Console.Out.WriteLine();
        }
    }
}