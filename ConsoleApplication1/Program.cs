using System;

namespace ConsoleApplication1
{
    public class Program
    {
        private readonly IMineSequenceProvider m_mineSequenceProvider;
        private readonly IResultHandler m_resultHandler;

        public Program(IMineSequenceProvider mineSequenceProvider, IResultHandler resultHandler)
        {
            m_mineSequenceProvider = mineSequenceProvider;
            m_resultHandler = resultHandler;
        }

        public static void Main(string[] args)
        {
            new Program(new ReadFromConsoleProvider(), new WriteToConsoleProvider()).Run();
        }

        public void Run()
        {
            var input = m_mineSequenceProvider.GetSequence();

            var chars = input.ToCharArray();

            var result = new char[chars.Length];

            for (var i = 0; i < chars.Length; ++i)
            {
                if (IsMine(chars[i]))
                {
                    result[i] = '*';
                    continue;
                }

                int count = 0;
                if (i > 0 && IsMine(chars[i - 1]))
                {
                    count ++;
                }
                if (i < chars.Length - 1 && IsMine(chars[i + 1]))
                {
                    count ++;
                }

                result[i] = count.ToString()[0];
            }

            m_resultHandler.HandleResult(new string(result));
        }

        private static bool IsMine(char character)
        {
            return character == '*';
        }
    }

    internal class WriteToConsoleProvider : IResultHandler
    {
        public void HandleResult(string result)
        {
            Console.WriteLine(result);
        }
    }

    internal class ReadFromConsoleProvider : IMineSequenceProvider
    {
        public string GetSequence()
        {
            return Console.ReadLine();
        }
    }

    public interface IResultHandler
    {
        void HandleResult(string result);
    }

    public interface IMineSequenceProvider
    {
        string GetSequence();
    }
}
