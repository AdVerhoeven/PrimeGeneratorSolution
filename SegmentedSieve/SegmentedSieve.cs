using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SegmentedSieve
{
    class SegmentedSieve
    {
        private static bool[][] Sieves;

        private static int sieveSize;

        private static int limit;

        /// <summary>
        /// Creates a segmented sieve of a given size and eliminates all non-primes.
        /// The number of segments is equal to the square root of the total size.
        /// </summary>
        /// <param name="size">The upper limit of the sieve</param>
        public SegmentedSieve(int size)
        {
            limit = size;
            int sieveCount = (int)Math.Ceiling(Math.Sqrt(size));
            //Make the required number of sieves
            Sieves = new bool[sieveCount][];
            for (int i = 0; i < sieveCount; i++)
            {
                //The size of each segment is equal to the sievecount since sqrt(n)*sqrt(n)=n
                Sieves[i] = new bool[sieveCount];
            }
            sieveSize = sieveCount;
            MakeSieve();
        }

        private void MakeSieve()
        {
            //TODO: create tasks to delegate the elimination workflow to
            var baseSieve = Sieves[0];
            baseSieve[1] = true;
            //Parallelization of eliminating all even numbers in the sieve.
            Parallel.For(0, sieveSize, (segment) =>
              {
                  var start = segment * sieveSize % 2;
                  for (int i = start; i < sieveSize; i+=2)
                  {
                      Sieves[segment][i] = true;
                  }
              });
            baseSieve[2] = false;
            for (int i = 3; i < baseSieve.Length; i+=2)
            {
                if (!baseSieve[i])
                {
                    // i is prime!
                    var mult = i * i;
                    while(mult < limit)
                    {
                        var segment = mult / sieveSize;
                        var pos = mult % sieveSize;
                        Sieves[segment][pos] = true;
                        mult += i + i;
                    }
                }
            }
        }

        public IEnumerable<int> Primes(int lim = -1)
        {
            if(lim < 0)
            {
                lim = limit;
            }
            for (int i = 0; i < Math.Min(limit,lim); i++)
            {
                var segment = i / sieveSize;
                var pos = i % sieveSize;
                if (!Sieves[segment][pos])
                {
                    yield return i;
                }
            }
        }
    }
}
