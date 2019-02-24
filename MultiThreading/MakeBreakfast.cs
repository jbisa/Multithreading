using System;
using MultiThreading.Factories;
using MultiThreading.Models;
using System.Threading.Tasks;

namespace MultiThreading
{
    class MakeBreakfast
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var breakfastAlgorithmFactory = new BreakfastAlgorithmFactory();
            var breakfastAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("Bacon Egg And Cheese");

            var chef = new Chef("Jay", breakfastAlgorithm);

            // Sync vs. async
            chef.PrepareBreakfast();
            await chef.PrepareBreakfastAsync();

            // Async multithreaded
            var assistantChef = new Chef("Chris", breakfastAlgorithm);

            //Parallel.For()

            Console.WriteLine("End of program...");
        }
    }
}
