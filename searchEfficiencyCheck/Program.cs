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
        static int precisionFactor = 10000;

        static void Main(string[] args)
        {

            long programStartingTime = Stopwatch.GetTimestamp();


            int iterationCounter = 28;
            int iterationCounter2 = 28;

            int[] tablica = new int[(int)Math.Pow(2, 28)];

            for (int i = 0; i < tablica.Length; i++)
            {
                tablica[i] = i;
            }


            //1
            lookingForNumber = (tablica.Length - 1) / iterationCounter;
            Console.WriteLine("TRY\tSTEPS\t\tNUMBER\t\tFOUND?");
            for (int i = 0; i < iterationCounter; i++)
            {
                counter = 0;
                bool test = IsPresent_InstrumentalLineSearch(tablica, lookingForNumber);
                Console.WriteLine("{0}\t{1,-10}\t{2,-10}\t{3}", i + 1, counter, lookingForNumber, test);
                lookingForNumber += (tablica.Length-1) / iterationCounter;
            }
            Console.WriteLine("\n---===#########===---\n");


            //2
            lookingForNumber = (tablica.Length - 1) / iterationCounter;
            Console.WriteLine("TRY\tTIME[s]\t\tNUMBER");

            for (int i = 0; i < iterationCounter; i++)
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
                    Console.Write("{0}: [{1:P2}]\r", i + 1, (float)n / (iterationCounter + 1));  //wskaźnik postępu
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = ElapsedTime * (1.0 / (iterationCounter * Stopwatch.Frequency));
                Console.WriteLine("{0}\t{1}\t\t{2}", i + 1, ElapsedSeconds.ToString("F4"), lookingForNumber);
                lookingForNumber += tablica.Max() / iterationCounter;
            }

            Console.WriteLine("\n---===#########===---\n");



            //3
            lookingForNumber = (tablica.Length - 1) / 2;
            Console.WriteLine("TRY\tSTEPS\t\tNUMBER");

            for (int i = 0; i < iterationCounter2; i++)
            {
                counter = 0;
                IsPresent_InstrumentalBinaryTreeSearch2(tablica, lookingForNumber);
                Console.WriteLine("{0}\t{1}\t\t{2}", i + 1, counter, lookingForNumber);
                //lookingForNumber += (tablica.Length - lookingForNumber) / 2;
                lookingForNumber = lookingForNumber / 2;
            }
            Console.WriteLine("\n---===#########===---\n");


            //4
            lookingForNumber = (tablica.Length - 1) / 2;
            Console.WriteLine("TRY\tTIME[s]\t\tNUMBER");

            for (int i = 0; i < iterationCounter2; i++)
            {
                double ElapsedSeconds2;
                long ElapsedTime2 = 0, MinTime2 = long.MaxValue, MaxTime2 = long.MinValue, IterationElapsedTime2;
                for (int n = 0; n < (iterationCounter2 * precisionFactor + 1 + 1); ++n) // odejmujemy wartości skrajne
                {
                    long StartingTime2 = Stopwatch.GetTimestamp();
                    IsPresent_TimeStampBinaryTreeSearch2(tablica, lookingForNumber);
                    long EndingTime2 = Stopwatch.GetTimestamp();
                    IterationElapsedTime2 = EndingTime2 - StartingTime2;
                    ElapsedTime2 += IterationElapsedTime2;
                    Console.Write("{0}: [{1:P2}]\r", i + 1, (float)n / (iterationCounter2 * precisionFactor + 1));  //wskaźnik postępu                  
                    if (IterationElapsedTime2 < MinTime2) MinTime2 = IterationElapsedTime2;
                    if (IterationElapsedTime2 > MaxTime2) MaxTime2 = IterationElapsedTime2;
                }
                ElapsedTime2 -= (MinTime2 + MaxTime2);
                ElapsedSeconds2 = ElapsedTime2 * (1.0 / (precisionFactor * iterationCounter2 * Stopwatch.Frequency));
                Console.WriteLine("{0}:\t{1}\t{2}", i + 1, ElapsedSeconds2.ToString("F8"), lookingForNumber);
                //lookingForNumber += (tablica.Length - lookingForNumber) / 2;
                lookingForNumber = lookingForNumber / 2;
            }

            Console.WriteLine("\n---===#########===---\n");


            long programEndingTime = Stopwatch.GetTimestamp();
            double programTime = (programEndingTime - programStartingTime) * (1.0 / Stopwatch.Frequency);

            Console.WriteLine("Full time: {0}\nCPU startTimeStamp: {1}\nCPU stopTimeStamp: {2}", programTime, programStartingTime, programEndingTime);
            Console.ReadLine();

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

        static bool IsPresent_InstrumentalBinaryTreeSearch2(int[] vector, int number)
        {
            int left = 0, right = vector.Length - 1, mid = (left + right) / 2;

            do
            {
                counter++;
                mid = (left + right) / 2;
                if (vector[mid] == number) return true;
                else if (vector[mid] < number) left = mid + 1;
                else right = mid;
            }
            while (left != right);

            Console.WriteLine("W przeszukiwanym zbiorze nie ma liczby: " + number);
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

        static bool IsPresent_TimeStampBinaryTreeSearch2(int[] vector, int number)
        {
            int left = 0, right = vector.Length - 1, mid = (left + right) / 2;

            do
            {
                mid = (left + right) / 2;
                if (vector[mid] == number) return true;
                else if (vector[mid] < number) left = mid + 1;
                else right = mid;
            }
            while (left != right);

            Console.WriteLine("W przeszukiwanym zbiorze nie ma liczby: " + number);
            return false;
        }

    }
}
