using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

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

                result[i] =  count.ToString()[0];
            }

            Console.WriteLine(result);
        }

        private static bool IsMine(char character)
        {
            return character == '*';
        }
    }
}
