using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Ex._2.Algorithms
{
    class Program
    {
        public static void Main(string[] args)
        {
            //ZADANIE 1
            //a
            var inputForFirstEx = ReadFileForFirstEx($"{Directory.GetCurrentDirectory()}\\Resources\\dane.txt");
            var maxValueForLenghtOfString = MaxValueForLenghtOfString(inputForFirstEx);
            var minValueForLenghtOfString = MinValueForLenghtOfString(inputForFirstEx);

            Console.WriteLine($"Max Value = {maxValueForLenghtOfString}, Min Value = {minValueForLenghtOfString}");
            //b
            var maxValueForASCII = MaxValueForASCIICount(inputForFirstEx);
            var minValueForASCII = MinValueForASCIICount(inputForFirstEx);

            Console.WriteLine($"Max Value for ASCII: {maxValueForASCII}, Min Value for ASCII: {minValueForASCII}");


            //ZADANIE 2
            // czytamy dane z pliku (gdy chcemy inny plik podmieniamy jego ścieżkę)
            var inputToSort_bubble = ReadFileForSecondEx($"{Directory.GetCurrentDirectory()}\\Resources\\sort2.txt");
            // klonujemy zawartosci tablic zeby nie wykonywac operacji sortowania cały czas na tej samej tablicy
            var inputToSort_selection = inputToSort_bubble.ToArray();
            var inputToSort_insertion = inputToSort_bubble.ToArray();
            var inputToSort_merge = inputToSort_bubble.ToArray();
            var inputToSort_counting = inputToSort_bubble.ToArray();

            //słownik do przechowywania czasów z poszczególnych sortowań
            var timeResults = new Dictionary<string, long>();

            var stopwatch = new Stopwatch();
            var sortingAlgorithms = new SortingAlgorithms();
            //BubbleSort
            stopwatch.Start();
            sortingAlgorithms.BubbleSort(inputToSort_bubble);
            stopwatch.Stop();

            timeResults.Add("BubbleSort", stopwatch.ElapsedMilliseconds);

            //reset zegara
            stopwatch.Reset();

            //SelectionSort
            stopwatch.Start();
            sortingAlgorithms.SelectionSort(inputToSort_selection);
            stopwatch.Stop();

            timeResults.Add("SelectionSort", stopwatch.ElapsedMilliseconds);

            //reset zegara
            stopwatch.Reset();

            //InsertionSort
            stopwatch.Start();
            sortingAlgorithms.InsertionSort(inputToSort_insertion);
            stopwatch.Stop();

            timeResults.Add("InsertionSort", stopwatch.ElapsedMilliseconds);

            //reset zegara
            stopwatch.Reset();

            //MergeSort
            stopwatch.Start();
            sortingAlgorithms.MergeSort(inputToSort_merge);
            stopwatch.Stop();

            timeResults.Add("MergeSort", stopwatch.ElapsedMilliseconds);

            //reset zegara
            stopwatch.Reset();


            foreach(var timeResult in timeResults)
            {
                Console.WriteLine($"{timeResult.Key} {timeResult.Value} ms");
            }
        }

        private static string MaxValueForLenghtOfString(string[] input)
        {
            var maxValue = "";
            foreach(var value in input)
            {
                if(value.Length > maxValue.Length)
                {
                    maxValue = value;
                }
            }
            return maxValue;
        }

        private static int MaxValueForASCIICount(string[] input)
        {
            var maxValue = 0;
            foreach (var value in input)
            {
                var result = SumAscii(value, value.Length);
                if(result > maxValue)
                {
                    maxValue = result;
                }
            }
            return maxValue;
        }

        private static string MinValueForLenghtOfString(string[] input)
        {
            var minValue = input[0];
            foreach (var value in input)
            {
                if (value.Length < minValue.Length)
                {
                    minValue = value;
                }
            }
            return minValue;
        }

        private static int MinValueForASCIICount(string[] input)
        {
            var minValue = SumAscii(input[0], input[0].Length);
            foreach (var value in input)
            {
                var result = SumAscii(value, value.Length);
                if (result < minValue)
                {
                    minValue = result;
                }
            }
            return minValue;
        }


        private static bool IsPrime(int n)
        {
            if (n == 0 || n == 1)
            {
                return false;
            }

            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        // Function to return the sum 
        // of the ascii values of the characters 
        // which are present at prime positions 
        private static int SumAscii(string str, int n)
        {
            // To store the sum 
            int sum = 0;

            // For every character 
            for (int i = 0; i < n; i++)
            {

                // If current position is prime 
                // then add the ASCII value of the 
                // character at the current position 
                if (IsPrime(i + 1))
                {
                    sum += (int)(str[i]);
                }
            }

            return sum;
        }

        private static string[] ReadFileForFirstEx(string pathToFile)
        {
            return File.ReadAllLines(pathToFile);
        }

        private static int[] ReadFileForSecondEx(string pathToFile)
        {
            var output = new List<int>();
            var content = File.ReadAllLines(pathToFile);
            for(var index = 0; index < content.Length; index++)
            {
                var delimited = content[0].Split('\t');
                foreach(var value in delimited)
                {
                    output.Add(int.Parse(value));
                }
            }
            return output.ToArray();
        }
    }

    public class SortingAlgorithms
    {
        public void BubbleSort(int[] inputArray)
        {
            int arrayLenght = inputArray.Length;
            do
            {
                for (int index = 0; index < arrayLenght - 1; index++)
                {
                    if (inputArray[index] > inputArray[index + 1])
                    {
                        int tmp = inputArray[index];
                        inputArray[index] = inputArray[index + 1];
                        inputArray[index + 1] = tmp;
                    }
                }
                arrayLenght--;
            }
            while (arrayLenght > 1);
        }

        public void SelectionSort(int[] inputArray)
        {
            for (var index = 0; index < inputArray.Length - 1; index++)
            {
                //szukaj elementu najmniejszego w nieposortowanej czesci tablicy
                var min = inputArray[index];
                var minIndex = index;
                for (var secondIndex = index; secondIndex < inputArray.Length; secondIndex++)
                {
                    if (inputArray[secondIndex] < min)
                    {
                        min = inputArray[secondIndex];
                        minIndex = secondIndex;
                    }
                }
                //wstaw element najmniejszy na swoje miejsce
                var temp = inputArray[index];
                inputArray[index] = inputArray[minIndex];
                inputArray[minIndex] = temp;
            }
        }

        public void InsertionSort(int[] inputArray)
        {
            int helpingVar, secondIndex;
            for (int index = 1; index < inputArray.Length; index++)
            {
                //wstawienie elementu w odpowiednie miejsce
                helpingVar = inputArray[index]; //ten element będzie wstawiony w odpowiednie miejsce
                secondIndex = index - 1;

                //przesuwanie elementów większych od pom
                while (secondIndex >= 0 && inputArray[secondIndex] > helpingVar)
                {
                    inputArray[secondIndex + 1] = inputArray[secondIndex]; //przesuwanie elementów
                    --secondIndex;
                }
                inputArray[secondIndex + 1] = helpingVar; //wstawienie pom w odpowiednie miejsce
            }
        }

        public int[] MergeSort(int[] array)
        {
            int[] left;
            int[] right;
            //As this is a recursive algorithm, we need to have a base case to 
            //avoid an infinite recursion and therfore a stackoverflow
            if (array.Length <= 1)
                return array;
            // The exact midpoint of our array  
            int midPoint = array.Length / 2;
            //Will represent our 'left' array
            left = new int[midPoint];

            //if array has an even number of elements, the left and right array will have the same number of 
            //elements
            if (array.Length % 2 == 0)
                right = new int[midPoint];
            //if array has an odd number of elements, the right array will have one more element than left
            else
                right = new int[midPoint + 1];
            //populate left array
            for (int i = 0; i < midPoint; i++)
                left[i] = array[i];
            //populate right array   
            int x = 0;
            //We start our index from the midpoint, as we have already populated the left array from 0 to midpoint
            
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            //Recursively sort the left array
            left = MergeSort(left);
            //Recursively sort the right array
            right = MergeSort(right);
            //Merge our two sorted arrays
            int[] result = Merge(left, right);
            return result;
        }

        //This method will be responsible for combining our two sorted arrays into one giant array
        public static int[] Merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            //
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            //while either array still has an element
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                //if both arrays have elements  
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    //If item on left array is less than item on right array, add that item to the result array 
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    // else the item in the right array wll be added to the results array
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //if only the left array still has elements, add all its items to the results array
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //if only the right array still has elements, add all its items to the results array
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }
    }

}
