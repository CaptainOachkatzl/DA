using System;

namespace XSLibrary.MultithreadingPatterns.UniquePair
{
    class RoundRobinTournamentPairing
    {
        public struct StackIDs
        {
            public int ID1;
            public int ID2;
        }

        public int CoreCount { get; private set; }
        public int StepCount { get; private set; }
        public int StackCount { get; private set; }
        private int ElementCount { get; set; }

        public int UsableCoreCount { get { return GetUsableCoreCount(ElementCount); } }

        public StackIDs[][] PairMatrix { get; private set; }

        public RoundRobinTournamentPairing(int coreCount)
        {
            ElementCount = -1;
            CoreCount = coreCount;
        }

        public void SetElementCount(int elementCount)
        {
            if (ElementCount == -1 || GetUsableCoreCount(elementCount) != UsableCoreCount)
            {
                ElementCount = elementCount;

                StepCount = (2 * UsableCoreCount) - 1;
                StackCount = 2 * UsableCoreCount;
                CreateMatrix();
            }
        }

        private int GetUsableCoreCount(int elementCount)
        {
            return Math.Min(elementCount / 2, CoreCount);
        }

        private void CreateMatrix()
        {
            PairMatrix = new StackIDs[StepCount][];

            int[] IDs = CreateBaseArray();

            for (int step = 0; step < StepCount; step++)
            {
                PairMatrix[step] = new StackIDs[UsableCoreCount];
                for (int coreID = 0; coreID < UsableCoreCount; coreID++)
                {
                    StackIDs ids;
                    ids.ID1 = IDs[coreID];
                    ids.ID2 = IDs[StackCount - 1 - coreID];
                    PairMatrix[step][coreID] = ids;
                }

                if(step + 1 < StepCount)
                    ShiftArray(IDs);
            }
        }

        private int[] CreateBaseArray()
        {
            int[] baseIDs = new int[StackCount];

            for (int i = 0; i < StackCount; i++)
            {
                baseIDs[i] = i;
            }

            return baseIDs;
        }

        private void ShiftArray(int[] array, int times)
        {
            for (int i = 0; i < times; i++)
                ShiftArray(array);
        }


        private void ShiftArray(int[] array)
        {
            for (int i = 1; i < StackCount; i++)
            {
                array[i] = CircleInt(array[i] + 1);
                if (array[i] == 0)
                    array[i]++;
            }
        }

        private int CircleInt(int value)
        {
            int cap = UsableCoreCount * 2;

            if (value < 0)
                return CircleInt(value + cap);

            if (value >= cap)
                return CircleInt(value - cap);

            return value;
        }
    }
}
