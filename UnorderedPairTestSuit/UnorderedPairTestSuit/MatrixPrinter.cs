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
            Log = new LoggerConsolePeriodic()
            {
                Prefix = "\t"
            };
        }

        public void PrintMatrix(int elementCount, int cores = 0)
        {
            PairLogic.GenerateMatrix(elementCount);

            if (cores > 0)
                PrintCores(elementCount, cores);

            for (int step = 0; step < PairLogic.StepCount; step++)
            {
                PrintStep(step);
            }
        }

        private void PrintCores(int elementCount, int cores)
        {
            for (int i = 0; i < elementCount / 2; i++)
            {
                if (i == 0)
                    Log.Log("\n\t");

                Log.Log("\tCore " + i % cores);
            }
        }

        private void PrintStep(int step)
        {
            Log.Log("\n");
            Log.Log("step " + step + "\t");

            for (int pair = 0; pair < PairLogic.PairCount; pair++)
            {
                int id1 = PairLogic.PairMatrix[step][pair].ID1;
                int id2 = PairLogic.PairMatrix[step][pair].ID2;

                if (id1 < id2)
                    PrintPair(id1, id2);
                else
                    PrintPair(id2, id1);
            }
        }

        private void PrintPair(int id1, int id2)
        {
            Log.Log("{ " + id1 + "-" + id2 + " }   ");
        }
    }
}