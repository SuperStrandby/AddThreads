using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddThreadProgram
{
    internal class Program
    {

        private static readonly int[][] Data = new int[][]
        {
            new[] {1, 5, 4, 2},
            new[] {3, 2, 4, 11, 4},
            new[] {33, 2, 3, -1, 10},
            new[] {3, 2, 8, 9, -1},
            new[] {1, 22, 1, 9, -3, 5}
        };

        private static int FindSmallest(int[] numbers)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }

            int smallestSoFar = numbers[0];
            foreach (int number in numbers)
            {
                if (number < smallestSoFar)
                {
                    smallestSoFar = number;
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Timer: {0}", stopwatch.Elapsed);
            return smallestSoFar;




        }

        private static void Main()
        {

            List<Task<int>> alltasks = new List<Task<int>>();



            foreach (int[] data in Data)
            {
                Task<int> tasks = new Task<int>(() =>
                {
                    int smallest = FindSmallest(data);
                    Console.WriteLine(String.Join(", ", data) + ": " + smallest);

                    //Task<int> needs to return a value
                    return smallest;
                });

                alltasks.Add(tasks);
                tasks.Start();
            }

            Console.WriteLine("Smallest of all arrays: \n");

            //A list to hold all of the smallest ints from each array
            List<int> smallestOfAllInts = new List<int>();

            foreach (var task in alltasks)
            {
                Console.WriteLine(task.Result + " ");
                smallestOfAllInts.Add(task.Result);
            }
            Console.WriteLine("Smallest of all: " + FindSmallest(smallestOfAllInts.ToArray()));

        }
    }
}
