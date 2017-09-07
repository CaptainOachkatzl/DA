using System;
using XSLibrary.MultithreadingPatterns.UniquePair;

namespace UnorderedPairTestSuit
{
    class MatrixPrinter
    {
        RRTPairing PairLogic { get; set; }

        public MatrixPrinter()
        {
            PairLogic = new RRTPairing();
        }

        public void PrintMatrix(int elementCount)
        {
            PairLogic.GenerateMatrix(elementCount);

            for (int step = 0; step < PairLogic.StepCount; step++)
            {
                if (step > 0)
                    Console.Out.WriteLine();

                Console.Out.Write("step " + step + "\t\t");

                for (int pair = 0; pair < PairLogic.PairCount; pair++)
                {
                    int id1 = PairLogic.PairMatrix[step][pair].ID1;
                    int id2 = PairLogic.PairMatrix[step][pair].ID2;

                    if (id1 < id2)
                        Console.Out.Write("{ " + id1 + ", " + id2 + " }\t");
                    else
                        Console.Out.Write("{ " + id2 + ", " + id1 + " }\t");
                }
            }

            Console.Out.WriteLine();
        }
    }
}