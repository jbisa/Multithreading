using System;
using System.Threading.Tasks;
using MultiThreading.Algorithms;

namespace MultiThreading.Models
{
    public class Chef
    {
        string Name { get; }

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
            _breakfastAlgorithm.Make();
        }

        public async Task PrepareBreakfastAsync()
        {
            Console.Write($"{Name} begins to prepare breakfast asynchronously...\n");
            await _breakfastAlgorithm.MakeAsync();
        }
    }
}
