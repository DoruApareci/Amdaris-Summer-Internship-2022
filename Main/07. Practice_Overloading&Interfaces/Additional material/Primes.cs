using System;
using System.Collections;
using System.Text;

namespace PrimeNumbers
{
    class Primes
    {
      private long min;
      private long max;

      public Primes() : this(2, 100)
      {
      }

      public Primes(long minimum, long maximum)
      {
         if (min < 2)
         {
            min = 2;
         }
         min = minimum;
         max = maximum;
      }

      public IEnumerator GetEnumerator()
      {
         for (long possiblePrime = min; possiblePrime <= max; possiblePrime++)
         {
            bool isPrime = true;
            for (long possibleFactor = 2; possibleFactor <= (long)Math.Floor(Math.Sqrt(possiblePrime)); possibleFactor++)
            {
               long remainderAfterDivision = possiblePrime % possibleFactor;
               if (remainderAfterDivision == 0)
               {
                  isPrime = false;
                  break;
               }
            }
            if (isPrime)
            {
               yield return possiblePrime;
            }
         }
      }
    }
}
