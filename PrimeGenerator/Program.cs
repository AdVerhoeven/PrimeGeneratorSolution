using System;
using System.Diagnostics;

namespace PrimeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Sieve sieve = new Sieve(int.MaxValue/4);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            sieve.MakeSieve();
            stopwatch.Stop();
            Console.WriteLine($"This sieve took {stopwatch.ElapsedMilliseconds} milliseconds to create.");
            var count = 0;
            foreach (var prime in sieve.Primes())
            {
                count++;
            }
            Console.WriteLine($"This sieve contains {count} prime numbers.");
            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
