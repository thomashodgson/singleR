using System;

namespace ConsoleApplication1
{
    public class Program
    {
        private readonly IBoardProvider m_boardProvider;
        private readonly IResultHandler m_resultHandler;

        public Program(IBoardProvider boardProvider, IResultHandler resultHandler)
        {
            m_boardProvider = boardProvider;
            m_resultHandler = resultHandler;
        }

        public static void Main(string[] args)
        {
            new Program(new ReadFromConsoleProvider(), new WriteToConsoleProvider()).Run();
        }

        public void Run()
        {
            var board = m_boardProvider.GetBoard();

            var result = "";

            for (var i = 0; i < board.Size; ++i)
            {
                result = result + IndexCounter.CountAtIndex(board, i);
            }

            m_resultHandler.HandleResult(result);
        }
    }

    public class IndexCounter
    {
        public static char CountAtIndex(Board board, int i)
        {
            if (board.IsMine(i))
            {
                return '*';
            }

            var count = 0;
            if (board.IsMine(i - 1))
            {
                count++;
            }
            if (board.IsMine(i + 1))
            {
                count++;
            }

            return count.ToString()[0];
        }
    }

    internal class WriteToConsoleProvider : IResultHandler
    {
        public void HandleResult(string result)
        {
            Console.WriteLine(result);
        }
    }

    internal class ReadFromConsoleProvider : IBoardProvider
    {
        public Board GetBoard()
        {
            return new Board(Console.ReadLine());
        }
    }

    public interface IResultHandler
    {
        void HandleResult(string result);
    }

    public interface IBoardProvider
    {
        Board GetBoard();
    }

    public class Board
    {
        private readonly string m_input;

        public Board(string input)
        {
            m_input = input;
            Size = input.Length;
        }

        public int Size { get; set; }

        public bool IsMine(int i)
        {
            return i > -1 && i < Size && m_input[i] == '*';
        }
    }
}
