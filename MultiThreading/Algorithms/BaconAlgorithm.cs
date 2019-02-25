using System;
using System.Threading.Tasks;

namespace MultiThreading.Algorithms
{
    public class BaconAlgorithm : BreakfastAlgorithm
    {
        /// <summary>
        /// Algorithm to make breakfast.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public override void Make(string name)
        {
            GrabIngredients(name);
            SprayAndHeatPan(name);
            CookBacon(name);

            Console.WriteLine($"> {name} has gotten the bacon ready!");
        }

        /// <summary>
        /// Algorithm to make breakfast asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public async override Task MakeAsync(string name)
        {
            SprayAndHeatPanAsync(name);
            GrabIngredients(name);
            CookBacon(name);

            Console.WriteLine($"> {name} has gotten the bacon ready!");
        }

        /// <summary>
        /// Spray the pan and begin heating it.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void SprayAndHeatPan(string name)
        {
            // It takes 2 seconds to spray and heat up the pan.
            Console.WriteLine($"> {name} sprays pan with oil and turns on stove to medium heat.");
            Task.Delay(2000).Wait();
        }

        /// <summary>
        /// Spray the pan and begin heating it asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        private async Task SprayAndHeatPanAsync(string name)
        {
            // It takes 2 seconds to spray and heat up the pan.
            Console.WriteLine($"> {name} sprays pan with oil and turns on stove to medium heat.");
            await Task.Delay(2000);
        }

        /// <summary>
        /// Grab the ingredients.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void GrabIngredients(string name)
        {
            // Imagine this is a call to a repository (cache and/or DB). Each
            // item is a table from the "fridge" DB and it takes a second to get
            // each.
            Console.WriteLine($"> {name} grabs bacon.");
            Task.Delay(1000).Wait();
        }

        /// <summary>
        /// Cook the bacon.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void CookBacon(string name)
        {
            // It takes 3 seconds to cook the bacon.
            Console.WriteLine($"> {name} places bacon on pan and lets it cook...");
            Task.Delay(3000).Wait();
            PutBaconOntoAPlate(name);
        }

        /// <summary>
        /// Put the bacon onto a plate.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void PutBaconOntoAPlate(string name)
        {
            // It takes a second to put the bacon on a plate.
            Console.WriteLine($"> {name} puts the bacon onto a plate.");
            Task.Delay(1000).Wait();
        }
    }
}
