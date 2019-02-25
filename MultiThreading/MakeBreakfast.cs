using System;
using MultiThreading.Factories;
using MultiThreading.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MultiThreading
{
    class MakeBreakfast
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Assign a chef in charge of making a bacon, egg, and cheese sandwich
            var breakfastAlgorithmFactory = new BreakfastAlgorithmFactory();
            var breakfastAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("BaconEggAndCheese");
            var chefA = new Chef("Jay", breakfastAlgorithm);

            // 1. See how fast the chef can make breakfast synchrosnously
            Console.WriteLine($"{chefA.Name} begins to prepare breakfast synchronously...");
            var stopWatch = Stopwatch.StartNew();
            chefA.PrepareBreakfast();
            stopWatch.Stop();
            BreakfastIsReady(stopWatch.ElapsedMilliseconds);

            // 2. See how fast the chef can make breakfast asynchrosnously
            Console.WriteLine($"{chefA.Name} begins to prepare breakfast asynchronously...");          
            stopWatch = Stopwatch.StartNew();
            await chefA.PrepareBreakfastAsync();
            stopWatch.Stop();
            BreakfastIsReady(stopWatch.ElapsedMilliseconds);

            // 3. Now, compare how fast breakfast can be made with TWO chefs
            // cooking synchronously (multithreaded programming)

            // Assign one chef to cook the eggs, cheese, and toast
            var eggAndCheeseAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("EggAndCheese");
            var chefB = new Chef("Chris", eggAndCheeseAlgorithm);

            // Assign another chef to cook the bacon
            var baconAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("Bacon");
            var chefC = new Chef("Ian", baconAlgorithm);

            var chefs = new List<Chef>();
            chefs.Add(chefB);
            chefs.Add(chefC);

            Console.WriteLine($"{chefB.Name} and {chefC.Name} begin to prepare breakfast together synchronously...");
            stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(chefs, chef =>
            {
                chef.PrepareBreakfast();
            });
            stopWatch.Stop();
            BreakfastIsReady(stopWatch.ElapsedMilliseconds);

            // 4. Now, see how fast breakfast can be made with TWO chefs
            // cooking asynchronously (async-multithreaded programming)
            Console.WriteLine($"{chefB.Name} and {chefC.Name} begin to prepare breakfast together asynchronously...");
            stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(chefs, chef =>
            {
                chef.PrepareBreakfastAsync();
            });
            stopWatch.Stop();
            BreakfastIsReady(stopWatch.ElapsedMilliseconds);

            Console.WriteLine("End of program...");
        }

        /// <summary>
        /// Message that breakfast is ready.
        /// </summary>
        /// <param name="timeItTookToCookInMs">The time in took to cook in milliseconds.</param>
        private static void BreakfastIsReady(long timeItTookToCookInMs)
        {
            Console.WriteLine("> Hooray, breakfast is ready!");

            // Use some arbitrary threshold to dictate that breakfast took too long to make
            if (timeItTookToCookInMs > 20000)
            {
                Console.WriteLine($"> Yikes! Breakfast is COLD, it look this long to make: {timeItTookToCookInMs} ms!\n");
            }
            else
            {
                if (timeItTookToCookInMs < 15000)
                {
                    Console.WriteLine($"> Awesome!! Breakfast is HOT, it look this long to make: {timeItTookToCookInMs} ms!\n");
                }
                else
                {
                    Console.WriteLine($"> Nice! Breakfast is WARM, it look this long to make: {timeItTookToCookInMs} ms!\n");
                }
            }
        }
    }
}
