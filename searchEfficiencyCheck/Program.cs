using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static ulong counter;
        static int lookingForNumber;
        static bool test;
        const int precisionFactor = 100;

        static void Main(string[] args)
        {


            int iterationCounter = 28;


            //1
            Console.WriteLine("\nWyszukiwanie liniowe [ocena przy użyciu instrumentacji]\n");
            Console.WriteLine("TRY\tSTEPS\t\tNUMBER\t\tFOUND?");

            ulong averagaCounter = 0;



            for (int i = 0; i <= iterationCounter; i++)
            {
                int[] tablica1 = new int[(int)Math.Pow(2, i)];
                for (int j = 0; j < tablica1.Length; j++)
                {
                    tablica1[j] = j;
                }

                counter = 0;
                lookingForNumber = tablica1.Max();
                test = IsPresent_InstrumentalLineSearch(tablica1, lookingForNumber);
                Console.WriteLine("{0}\t{1,-10}\t{2,-10}\t{3}", i + 1, counter, lookingForNumber, test);
                //if (i == iterationCounter - 2) lookingForNumber = tablica.Length - 1;
                //else lookingForNumber += (tablica.Length - 1) / iterationCounter;
                averagaCounter += counter;
            }

            Console.WriteLine("\nŚrednia złożoność wynosi: {0} kroków.", (double)averagaCounter / iterationCounter);
            Console.WriteLine("---------------------------------------------\n");

            /*
            //2
            Console.WriteLine("\nWyszukiwanie liniowe [ocena przy wykorzystaniu pomiaru czasu wykonania]\n");
            lookingForNumber = (tablica.Length - 1) / iterationCounter;
            double averagaCounter2 = 0;
            Console.WriteLine("TRY\tTIME[ms]\tNUMBER\t\tFOUND?");

            for (int i = 0; i < iterationCounter; i++)
            {
                double ElapsedSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int n = 0; n < (iterationCounter + 1 + 1); ++n) // odejmujemy wartości skrajne
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    test = IsPresent_TimeStampLineSearch(tablica, lookingForNumber);
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    Console.Write("{0}: [{1:P0}]\r", i + 1, (float)n / (iterationCounter + 1));  //wskaźnik postępu
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = ElapsedTime * (1000.0 / (iterationCounter * Stopwatch.Frequency));
                Console.WriteLine("{0}\t{1,-10}\t{2}\t{3}", i + 1, ElapsedSeconds.ToString("F4"), lookingForNumber, test);
                if (i == iterationCounter - 2) lookingForNumber = tablica.Length - 1;
                else lookingForNumber += (tablica.Length - 1) / iterationCounter;
                averagaCounter2 += ElapsedSeconds;
            }

            Console.WriteLine("\nŚrednia złożoność wynosi: {0}[ms]", averagaCounter2 / iterationCounter);
            Console.WriteLine("---------------------------------------------\n");
            */

            //3
            Console.WriteLine("\nWyszukiwanie binarne [ocena przy użyciu instrumentacji]\n");
            
            ulong averageCounter3 = 0;
            Console.WriteLine("TRY\tSTEPS\t\tNUMBER\t\tFOUND?");
            counter = 0;
            for (int i = 0; i < iterationCounter; i++)
            {
                int[] tablica2 = new int[(int)Math.Pow(2, i)];
                for (int j = 0; j < tablica2.Length; j++)
                {
                    tablica2[j] = j;
                }

                lookingForNumber = tablica2.Max();
                test = IsPresent_InstrumentalBinaryTreeSearch2(tablica2, lookingForNumber);
                Console.WriteLine("{0}\t{1}\t\t{2,-10}\t{3}", i + 1, counter, lookingForNumber, test);

                averageCounter3 += counter;
            }

            Console.WriteLine("\nŚrednia złożoność wynosi: {0} kroków.", (double)averageCounter3 / iterationCounter);
            Console.WriteLine("---------------------------------------------\n");


            /*

            

            //4
            Console.WriteLine("\nWyszukiwanie binarne [ocena przy wykorzystaniu pomiaru czasu wykonania]\n");
            lookingForNumber = (tablica.Length - 1) / 2;
            double averagaCounter4 = 0;
            Console.WriteLine("TRY\tTIME[µs]\tNUMBER\t\tFOUND?");

            for (int i = 0; i < iterationCounter2; i++)
            {
                double ElapsedSeconds2;
                long ElapsedTime2 = 0, MinTime2 = long.MaxValue, MaxTime2 = long.MinValue, IterationElapsedTime2;
                for (int n = 0; n < (iterationCounter2 * precisionFactor + 1 + 1); ++n) // odejmujemy wartości skrajne
                {
                    long StartingTime2 = Stopwatch.GetTimestamp();
                    test = IsPresent_TimeStampBinaryTreeSearch2(tablica, lookingForNumber);
                    long EndingTime2 = Stopwatch.GetTimestamp();
                    IterationElapsedTime2 = EndingTime2 - StartingTime2;
                    ElapsedTime2 += IterationElapsedTime2;
                    Console.Write("{0}: [{1:P0}]\r", i + 1, (float)n / (iterationCounter2 * precisionFactor + 1));  //wskaźnik postępu                  
                    if (IterationElapsedTime2 < MinTime2) MinTime2 = IterationElapsedTime2;
                    if (IterationElapsedTime2 > MaxTime2) MaxTime2 = IterationElapsedTime2;
                }
                ElapsedTime2 -= (MinTime2 + MaxTime2);
                ElapsedSeconds2 = ElapsedTime2 * (1000000.0 / (precisionFactor * iterationCounter2 * Stopwatch.Frequency));
                Console.WriteLine("{0}:\t{1}\t{2,-10}\t{3}", i + 1, ElapsedSeconds2.ToString("F8"), lookingForNumber, test);
                lookingForNumber = lookingForNumber / 2;
                averagaCounter4 += ElapsedSeconds2;
            }

            Console.WriteLine("\nŚrednia złożoność wynosi: {0}[µs]", (double)averagaCounter4 / iterationCounter2);
            Console.WriteLine("---------------------------------------------\n");

            */
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



        static bool IsPresent_InstrumentalBinaryTreeSearch2(int[] vector, int number)
        {
            int leftSide = 0, rightSide = vector.Length, middle = (leftSide + rightSide) / 2;

            do
            {
                counter++;
                middle = (leftSide + rightSide) / 2;
                if (vector[middle] == number) return true;
                else if (vector[middle] < number) leftSide = middle + 1;
                else rightSide = middle;
            }
            while (leftSide != rightSide);

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

            return false;
        }

    }
}
