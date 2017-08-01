namespace UnorderedPairTestSuit
{
    class Program
    {
        static void Main(string[] args)
        {
            UnorderedPairEnvironment m_environment = new UnorderedPairEnvironment(10);

            m_environment.Run();

            System.Console.In.Read();

            m_environment.Dispose();
        }
    }
}
