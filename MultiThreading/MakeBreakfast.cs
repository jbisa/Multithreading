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
            // Assign a chef in charge of making a Bacon Egg and Cheese Sandwich
            var breakfastAlgorithmFactory = new BreakfastAlgorithmFactory();
            var breakfastAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("BaconEggAndCheese");
            var chefA = new Chef("Jay", breakfastAlgorithm);

            // Compare how fast the chef can make breakfast synchronously vs.
            // asynchronously
            chefA.PrepareBreakfast();
            await chefA.PrepareBreakfastAsync();

            // Now, compare how fast breakfast can be made with two chefs
            // cooking asynchronously (async-multithreaded programming)
            var eggAndCheeseAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("EggAndCheese");
            var chefB = new Chef("Chris", eggAndCheeseAlgorithm);

            var baconAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("Bacon");
            var chefC = new Chef("Ian", baconAlgorithm);

            var chefs = new List<Chef>();
            chefs.Add(chefB);
            chefs.Add(chefC);

            var stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(chefs, chef =>
            {
                chef.PrepareBreakfastAsync();
            });
            stopWatch.Stop();

            Console.WriteLine($"{chefB.Name} and {chefC.Name} cooked breakast in: {stopWatch.ElapsedMilliseconds} ms!\n");
            Console.WriteLine("End of program...");
        }
    }
}
