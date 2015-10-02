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

            var result = new char[input.Length];

            for (var i = 0; i < input.Length; ++i)
            {
                result[i] = IndexCounter.CountAtIndex(input, i);
            }

            m_resultHandler.HandleResult(new string(result));
        }
    }


    public class IndexCounter
    {
        public static char CountAtIndex(string chars, int i)
        {
            if (IsMine(chars[i]))
            {
                return '*';
            }

            int count = 0;
            if (i > 0 && IsMine(chars[i - 1]))
            {
                count++;
            }
            if (i < chars.Length - 1 && IsMine(chars[i + 1]))
            {
                count++;
            }

            return count.ToString()[0];
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
