using System;
using System.Collections.Generic;

namespace SegmentedSieve
{
    class Program
    {
        static void Main(string[] args)
        {
            SegmentedSieve segmentedSieve = new SegmentedSieve(int.MaxValue/4);

            int count = 0;
            foreach (var prime in segmentedSieve.Primes())
            {
                count++;
            }
            Console.WriteLine($"This sieve contains {count} prime numbers");

            Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
}
