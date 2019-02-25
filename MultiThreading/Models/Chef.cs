using System;
using System.Threading.Tasks;
using MultiThreading.Algorithms;

namespace MultiThreading.Models
{
    public class Chef
    {
        public string Name { get; }

        private readonly BreakfastAlgorithm _breakfastAlgorithm;

        public Chef(string name, BreakfastAlgorithm breakfastAlgorithm)
        {
            Name = name;
            _breakfastAlgorithm = breakfastAlgorithm;
        }

        /// <summary>
        /// Algorithm to prepare breakfast.
        /// </summary>
        public void PrepareBreakfast()
        {
            _breakfastAlgorithm.Make(Name);
        }

        /// <summary>
        /// Algorithm to prepare breakfast asynchrosnously.
        /// </summary>
        /// <returns></returns>
        public async Task PrepareBreakfastAsync()
        {
            Console.WriteLine($"{Name} begins to prepare breakfast asynchronously...");
            await _breakfastAlgorithm.MakeAsync(Name);
        }
    }
}
