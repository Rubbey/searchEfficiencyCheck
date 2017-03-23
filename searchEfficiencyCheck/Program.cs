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

        static int iterationCounter = 28;
        static ulong counter, maxSteps;
        static double AverageElapsedTime = 0, Temp;
        const int precisionFactor = 10;


        static void Main(string[] args)
        {
            //1
            Console.WriteLine("\nWyszukiwanie liniowe [ocena przy użyciu instrumentacji]\n");
            Console.WriteLine("TAB_SIZE:\tLOOKING_FOR:\tFOUND?\tMAX_STEPS:\tAVG_STEPS:");

            for (int i = 0; i <= iterationCounter; i++)
            {
                int[] tablica = new int[(int)Math.Pow(2, i)];
                for (int j = 0; j < tablica.Length; j++) tablica[j] = j;

                counter = 0;
                int lookingForNumber = tablica.Length - 1;
                bool test = InstrumentalLineSearch(tablica, lookingForNumber);

                Console.WriteLine("Tab[2^{0}]\t{1,-11}\t{2}\t{3,-10}\t{4,-11}", i, lookingForNumber, test, counter, (double)(1 + tablica.Length) / 2);
            }
            Console.WriteLine("---------------------------------------------\n\n");


            //2
            Console.WriteLine("\nWyszukiwanie liniowe [ocena przy wykorzystaniu pomiaru czasu]\n");
            Console.WriteLine("TAB_SIZE:\tLOOKING_FOR:\tFOUND?\tMAX_TIME [ms]:\tAVG_TIME [ms]:");

            Temp = 0;

            for (int i = 0; i <= iterationCounter; i++)
            {
                int[] tablica = new int[(int)Math.Pow(2, i)];
                for (int j = 0; j < tablica.Length; j++) tablica[j] = j;

                int lookingForNumber = tablica.Length - 1;
                bool test = false;
                double ElapsedSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int k = 0; k < (precisionFactor + 1 + 1); ++k) // odejmujemy wartości skrajne
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    test = TimeStampLineSearch(tablica, lookingForNumber);
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    Console.Write("{0}: [{1:P0}]\r", i + 1, (float)k / (precisionFactor + 1));  //wskaźnik postępu
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = ElapsedTime * ((1000000.0 / Stopwatch.Frequency) / precisionFactor);

                double ThisLapAverageElapsedTime;
                if (i == 0)
                {
                    Temp = ElapsedSeconds;
                    ThisLapAverageElapsedTime = Temp;
                }
                else ThisLapAverageElapsedTime = (Temp + ElapsedSeconds) / 2;
                Console.WriteLine("Tab[2^{0}]\t{1,-11}\t{2}\t{3,-11}\t{4,-11}", i, lookingForNumber, test, ElapsedSeconds.ToString("F4"), ThisLapAverageElapsedTime.ToString("F4"));
                ThisLapAverageElapsedTime = 0;


            }
            Console.WriteLine("---------------------------------------------\n\n");


            //3
            Console.WriteLine("\nWyszukiwanie binarne [ocena przy użyciu instrumentacji]\n");
            Console.WriteLine("TAB_SIZE:\tLOOKING_FOR:\tFOUND?\tMAX_STEPS:\tAVG_STEPS:");

            for (int i = 0; i <= iterationCounter; i++)
            {
                int[] tablica = new int[(int)Math.Pow(2, i)];
                for (int j = 0; j < tablica.Length; j++) tablica[j] = j;

                counter = 0;
                int lookingForNumber = tablica.Length - 1;
                bool test = InstrumentalBinaryTreeSearch(tablica, lookingForNumber);

                Console.WriteLine("Tab[2^{0}]\t{1,-11}\t{2}\t{3,-10}\t{4,-11}", i, lookingForNumber, test, maxSteps, (double)counter / tablica.Length);
            }

            Console.WriteLine("---------------------------------------------\n\n");


            

            

            //4
            Console.WriteLine("\nWyszukiwanie binarne [ocena przy wykorzystaniu pomiaru czasu]\n");
            Console.WriteLine("TAB_SIZE:\tLOOKING_FOR:\tFOUND?\tMAX_TIME [ms]:\tAVG_TIME [ms]:");

            Temp = 0;
            for (int i = 0; i <= iterationCounter; i++)
            {
                int[] tablica = new int[(int)Math.Pow(2, i)];
                for (int j = 0; j < tablica.Length; j++) tablica[j] = j;

                int lookingForNumber = tablica.Length - 1;
                bool test = false;
                double ElapsedSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int k = 0; k < (precisionFactor + 1 + 1); ++k) // odejmujemy wartości skrajne
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    test = TimeStampBinaryTreeSearch(tablica, lookingForNumber);
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    Console.Write("{0}: [{1:P0}]\r", i + 1, (float)k / (precisionFactor * (iterationCounter + 1)));  //wskaźnik postępu
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = ElapsedTime * (1000000.0 / (Stopwatch.Frequency * precisionFactor));

                
                if (i == 0) Temp = ElapsedSeconds;
                else
                {
                    AverageElapsedTime += (Temp * (int)Math.Pow(2, i - 1));
                    Temp = ElapsedSeconds;
                }
                AverageElapsedTime += Temp;
                double ThisLapAverageElapsedTime = AverageElapsedTime / tablica.Length;
                AverageElapsedTime -= Temp;
                Console.WriteLine("Tab[2^{0}]\t{1,-11}\t{2}\t{3,-11}\t{4,-11}", i, lookingForNumber, test, ElapsedSeconds.ToString("F4"), ThisLapAverageElapsedTime.ToString("F4"));


            }
            Console.WriteLine("---------------------------------------------\n\n");

            
            Console.ReadLine();

        }


        static bool InstrumentalLineSearch(int[] vector, int number)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                counter++;
                if (vector[i] == number) return true;
            }
            return false;
        }



        static bool TimeStampLineSearch(int[] vector, int number)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] == number) return true;
            }
            return false;
        }



        static bool InstrumentalBinaryTreeSearch(int[] vector, int number)
        {
            int tabLeftSide = 0, tabRightSide = vector.Length - 1, tabMiddle;
            ulong treeLevel = 0, power = 0;

            while (tabLeftSide <= tabRightSide)
            {
                treeLevel++;
                tabMiddle = (tabLeftSide + tabRightSide) / 2;
                if (number == vector[tabMiddle])
                {
                    counter += treeLevel;
                    maxSteps = treeLevel;
                    return true;
                }
                else if (number < vector[tabMiddle]) tabRightSide = tabMiddle - 1;
                else tabLeftSide = tabMiddle + 1;

                counter += treeLevel * (ulong)Math.Pow(2, power);
                power++;
            }
            maxSteps = treeLevel;
            return false;
        }



        static bool TimeStampBinaryTreeSearch(int[] vector, int number)
        {            
            int tabLeftSide = 0, tabRightSide = vector.Length - 1, tabMiddle;            

            while (tabLeftSide <= tabRightSide)
            {
                tabMiddle = (tabLeftSide + tabRightSide) / 2;
                if (number == vector[tabMiddle])
                {                    
                    return true;
                }
                else if (number < vector[tabMiddle]) tabRightSide = tabMiddle - 1;
                else tabLeftSide = tabMiddle + 1;
            }            
            return false;
        }
    }
}
