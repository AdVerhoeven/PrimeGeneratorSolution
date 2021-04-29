using System;
using System.Collections.Generic;

namespace PrimeGenerator
{
    public class Sieve
    {
        private static bool[] sieve;

        public Sieve(int limit)
        {
            if (limit < 100)
            {
                Console.WriteLine($"{limit} is to small, using 100 instead.");
                limit = 100;
            }
            try
            {
                sieve = new bool[limit];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create a sieve of size: {limit}\n" +
                    $"Caught exception {ex}.");
                sieve = new bool[int.MaxValue - 100];
                Console.WriteLine($"Made a sieve of size {int.MaxValue - 100} instead");
            }
        }

        /// <summary>
        /// A list of all primes, generates the sieve if it has not been made yet.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> Primes()
        {
            if (sieve[1])
            {
                MakeSieve();
            }
            yield return 2;
            for (int i = 1; i < sieve.Length; i += 2)
            {
                if (!sieve[i])
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Fills the prime sieve.
        /// </summary>
        public void MakeSieve()
        {
            //Eliminate one
            sieve[1] = true;
            //Eliminate even numbers
            for (int i = 0; i < sieve.Length; i += 2)
            {
                sieve[i] = true;
            }
            //Two is the only exception.
            sieve[2] = false;
            //Eliminate all odd non-prime numbers
            int root = (int)Math.Sqrt(sieve.Length);
            for (int i = 3; i < root; i += 2)
            {
                if (!sieve[i])
                {
                    //Found a prime, eliminate all (odd) multiples.
                    long mult = i * i;
                    while (mult < sieve.Length)
                    {
                        sieve[mult] = true;
                        //odd + odd = even.
                        mult += i + i;
                    }
                }
            }
        }
    }
}
