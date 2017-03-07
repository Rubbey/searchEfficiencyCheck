using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchEfficiencyCheck
{
    class Program
    {
        static ulong counter;
        static int half;
        static int lookingForNumber;

        static void Main(string[] args)
        {
            int iterationCounter = 10;
            int iterationCounter2 = 28;

            int[] tablica = new int[(int)Math.Pow(2, 28)];

            for (int i = 0; i < tablica.Length; i++)
            {
                tablica[i] = i;
            }

            
            //1
            lookingForNumber = 0;
            Console.WriteLine("PRÓBA\tILOŚĆ KROKÓW\tSZUKANA LICZBA\tTEST");
            for (int i = 0; i < iterationCounter + 1; i++)
            {
                counter = 0;
                bool test = IsPresent_InstrumentalLineSearch(tablica, lookingForNumber);
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", i + 1, counter, lookingForNumber, test);
                lookingForNumber += tablica.Max() / iterationCounter;
            }
            Console.WriteLine("\n---===#########===---\n");


            //2
            lookingForNumber = 0;
            Console.WriteLine("PRÓBA\tt[s]\tSZUKANA LICZBA");

            for (int i = 0; i < iterationCounter + 1; i++)
            {
                double ElapsedSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int n = 0; n < (iterationCounter + 1 + 1); ++n) // odejmujemy wartości skrajne
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    IsPresent_TimeStampLineSearch(tablica, lookingForNumber);
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    //Console.Write("Iter[" + n + "]:" + IterationElapsedTime + "\t");
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = ElapsedTime * (1.0 / (iterationCounter * Stopwatch.Frequency));
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, ElapsedSeconds.ToString("F4"), lookingForNumber);
                lookingForNumber += tablica.Max() / iterationCounter;
            }

            Console.WriteLine("\n---===#########===---\n");

            

            //3
            lookingForNumber = tablica.Length / 2;
            Console.WriteLine("PRÓBA\tILOŚĆ KROKÓW\tSZUKANA LICZBA");

            for (int i = 0; i <= iterationCounter2; i++)
            {
                counter = 0;
                IsPresent_InstrumentalBinaryTreeSearch(tablica, lookingForNumber);
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, counter, lookingForNumber);
                //lookingForNumber += (tablica.Length - lookingForNumber) / 2;
                lookingForNumber = lookingForNumber / 2;
            }
            Console.WriteLine("\n---===#########===---\n");


            //4
            lookingForNumber = tablica.Length / 2;
            Console.WriteLine("PRÓBA\tt[s]\tSZUKANA LICZBA");

            for (int i = 0; i <= iterationCounter2; i++)
            {
                double ElapsedSeconds2;
                long ElapsedTime2 = 0, MinTime2 = long.MaxValue, MaxTime2 = long.MinValue, IterationElapsedTime2;
                for (int n = 0; n < (iterationCounter2 + 1 + 1); ++n) // odejmujemy wartości skrajne
                {
                    long StartingTime2 = Stopwatch.GetTimestamp();
                    IsPresent_TimeStampBinaryTreeSearch(tablica, lookingForNumber);
                    long EndingTime2 = Stopwatch.GetTimestamp();
                    IterationElapsedTime2 = EndingTime2 - StartingTime2;
                    ElapsedTime2 += IterationElapsedTime2;
                    //Console.Write("Iter[" + n + "]:" + IterationElapsedTime + "\t");
                    if (IterationElapsedTime2 < MinTime2) MinTime2 = IterationElapsedTime2;
                    if (IterationElapsedTime2 > MaxTime2) MaxTime2 = IterationElapsedTime2;
                }
                ElapsedTime2 -= (MinTime2 + MaxTime2);
                ElapsedSeconds2 = ElapsedTime2 * (1.0 / (iterationCounter2 * Stopwatch.Frequency));
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, ElapsedSeconds2.ToString("F8"), lookingForNumber);
                //lookingForNumber += (tablica.Length - lookingForNumber) / 2;
                lookingForNumber = lookingForNumber / 2;
            }

            Console.WriteLine("\n---===#########===---\n");


        }

        static bool IsPresent_InstrumentalLineSearch(int[] vector, int number)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                counter++;
                if (vector[i] == number) return true;
            }
            return false;
        }

        static bool IsPresent_TimeStampLineSearch(int[] vector, int number)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] == number) return true;
            }
            return false;
        }

        static bool IsPresent_InstrumentalBinaryTreeSearch(int[] vector, int number)
        {
            half = vector.Length / 2;
            while (half >= 0)
            {
                counter++;
                if (vector[half] == number) return true;
                else if (number > half) half += half / 2;
                else half = half / 2;
            }
            return false;
        }

        static bool IsPresent_TimeStampBinaryTreeSearch(int[] vector, int number)
        {
            half = vector.Length / 2;
            while (half >= 0)
            {
                if (vector[half] == number) return true;
                else if (number > half) half += half / 2;
                else half = half / 2;
            }
            return false;
        }

    }
}
