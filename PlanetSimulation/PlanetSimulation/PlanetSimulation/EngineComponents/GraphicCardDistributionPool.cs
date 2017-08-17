using System;
using Microsoft.Xna.Framework;
using XSLibrary.MultithreadingPatterns.UniquePair.DistributionNodes;
using Cloo;
using XSLibrary.MultithreadingPatterns.UniquePair;
using System.IO;
using System.Threading;

namespace PlanetSimulation.EngineComponents
{
    class GraphicCardDistributionPool : DistributionPool<Planet, GameTime>
    {
        const string FUNCTION_NAME = "CalculateGravity";

        SharedMemoryStackCalculation<Planet, GameTime> m_stackCalculation;

        ComputePlatform m_platform;
        ComputeContext m_context;
        ComputeDevice m_graphicsCard;
        ComputeKernel m_kernelProgram;
        ComputeCommandQueue m_queue;

        ManualResetEvent m_resetEvent;

        int m_vectorCount;
        ComputeBuffer<float>[] m_vectorBuffers;
        float[][] m_vectorArrays;


        public override int NodeCount
        {
            get
            {
                return 2; //(int)(m_graphicsCard.MaxComputeUnits * m_graphicsCard.MaxWorkGroupSize);
            }
        }

        public GraphicCardDistributionPool()
        {
            m_vectorCount = 4;

            m_stackCalculation = new SharedMemoryStackCalculation<Planet, GameTime>(CalculateSinglePair);

            m_resetEvent = new ManualResetEvent(false);

            m_platform = ComputePlatform.Platforms[0];

            m_context = new ComputeContext(ComputeDeviceTypes.Gpu,
            new ComputeContextPropertyList(m_platform), null, IntPtr.Zero);

            m_graphicsCard = m_context.Devices[0];

            m_queue = new ComputeCommandQueue(m_context, m_graphicsCard, ComputeCommandQueueFlags.None);

            ComputeProgram program = new ComputeProgram(m_context, GetKernelSource());
            program.Build(null, null, null, IntPtr.Zero);

            m_kernelProgram = program.CreateKernel(FUNCTION_NAME);

            m_vectorArrays = new float[m_vectorCount][];
            m_vectorBuffers = new ComputeBuffer<float>[m_vectorCount];
            for (int i = 0; i < m_vectorCount; i++)
            {
                m_vectorArrays[i] = new float[2];
                m_vectorBuffers[i] = new ComputeBuffer<float>(m_context, ComputeMemoryFlags.UseHostPointer, m_vectorArrays[i]);
                m_kernelProgram.SetMemoryArgument(i, m_vectorBuffers[i]);
            }
        }

        private string GetKernelSource()
        {
            // load opencl source
            StreamReader streamReader = new StreamReader("../../../../../../kernel.cl");
            string clSource = streamReader.ReadToEnd();
            streamReader.Close();
            return clSource;
        }

        public override void DistributeCalculation(int nodeIndex, CalculationPair<Planet, GameTime> calculationPair)
        {
            m_resetEvent.Reset();

            //new Thread( () => {
            m_kernelProgram.SetValueArgument(6, GameGlobals.SimulationSpeedMuliplicator);
            m_kernelProgram.SetValueArgument(7, (float)calculationPair.GlobalData.ElapsedGameTime.TotalSeconds);

            m_stackCalculation.Calculate(calculationPair);

            m_resetEvent.Set();
            //}).Start();
        }

        private void CalculateSinglePair(Planet planet1, Planet planet2, GameTime gameTime)
        {
            WriteVectorParameter(0, planet1.Position);
            WriteVectorParameter(1, planet2.Position);
            WriteVectorParameter(2, planet1.Direction);
            WriteVectorParameter(3, planet2.Direction);
            m_kernelProgram.SetValueArgument(4, planet1.Mass);
            m_kernelProgram.SetValueArgument(5, planet2.Mass);

            m_queue.ExecuteTask(m_kernelProgram, null);

            m_queue.Finish();

            m_queue.ReadFromBuffer(m_vectorBuffers[2], ref m_vectorArrays[2], true, null);
            m_queue.ReadFromBuffer(m_vectorBuffers[3], ref m_vectorArrays[3], true, null);

            planet1.Direction = new Vector2(m_vectorArrays[2][0], m_vectorArrays[2][1]);
            planet2.Direction = new Vector2(m_vectorArrays[3][0], m_vectorArrays[3][1]);
        }

        private void WriteVectorParameter(int arrayIndex, Vector2 vector)
        {
            m_vectorArrays[arrayIndex][0] = vector.X;
            m_vectorArrays[arrayIndex][1] = vector.Y;
            m_queue.WriteToBuffer(m_vectorArrays[arrayIndex], m_vectorBuffers[arrayIndex], true, null);
        }

        private float[] AddVectorParameter(int index, ComputeMemoryFlags flags, Vector2 vector)
        {
            float[] allocatedValues = new float[2] { vector.X, vector.Y };
            ComputeBuffer<float> buffer = new ComputeBuffer<float>(m_context, flags, allocatedValues);
            m_kernelProgram.SetMemoryArgument(index, buffer);
            return allocatedValues;
        }

        public override void Synchronize()
        {
            m_resetEvent.WaitOne();
        }

        public override void Dispose()
        {
        }
    }
}
