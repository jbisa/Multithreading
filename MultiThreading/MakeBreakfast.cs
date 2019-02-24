using System;
using MultiThreading.Factories;
using MultiThreading.Models;

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

            chef.PrepareBreakfast();
            await chef.PrepareBreakfastAsync();

            Console.WriteLine("End of program...");
        }
    }
}
