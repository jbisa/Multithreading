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
            Console.Write($"{Name} begins to prepare breakfast...\n");
            _breakfastAlgorithm.Make(Name);
        }

        /// <summary>
        /// Algorithm to prepare breakfast asynchrosnously.
        /// </summary>
        /// <returns></returns>
        public async Task PrepareBreakfastAsync()
        {
            Console.Write($"{Name} begins to prepare breakfast asynchronously...\n");
            await _breakfastAlgorithm.MakeAsync(Name);
        }
    }
}
