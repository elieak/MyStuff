using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = 2;
            var end = 20;
            var watch = new Stopwatch();

            watch.Start();
            CalcPrimes(start, end, 1).ToList().ForEach(Console.WriteLine);
            watch.Stop();

            Console.WriteLine(watch.Elapsed);

            CalcPrimes(start, end, -1).ToList().ForEach(Console.WriteLine);

            //Console.WriteLine(CalcPrimes(start, end, 1).Select(i => i.ToString()).Aggregate((a, b) => $"{a}, {b}"));
            //Console.WriteLine(CalcPrimes(start, end, -1).Select(i => i.ToString()).Aggregate((a, b) => $"{a}, {b}"));
        }

        public static IEnumerable<int> CalcPrimes(int start, int end, int maxDegree)
        {
            var bag = new ConcurrentBag<int>();
            //var list = new List<int>();
            //var syncLock = new object();

            Parallel.For(start, end, new ParallelOptions { MaxDegreeOfParallelism = maxDegree },
                (i) =>
                {
                    if (IsPrime(i))
                    {
                        bag.Add(i);
                    };

                });
            return bag;
        }

        public static bool IsPrime(int number)
        {
            for (int i = 2; i <= (int)Math.Sqrt(number); i++)
            {
                if (0 == number % i)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
