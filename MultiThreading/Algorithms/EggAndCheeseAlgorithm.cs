using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Algorithms
{
    public class EggAndCheeseAlgorithm : BreakfastAlgorithm
    {
        /// <summary>
        /// Algorithm to make breakfast.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public override bool Make(string name)
        {
            GrabIngredients(name);
            SprayAndHeatPan(name);
            CookEggs(name);
            ToastBread(name);

            Console.WriteLine($"> {name} has gotten the eggs, cheese, and toast ready! Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            return true;
        }

        /// <summary>
        /// Algorithm to make breakfast asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public async override Task<bool> MakeAsync(string name)
        {
            SprayAndHeatPanAsync(name);
            GrabIngredients(name);
            await ToastBreadAsync(name);
            await CookEggsAsync(name);

            Console.WriteLine($"> {name} has gotten the eggs, cheese, and toast ready! Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            return true;
        }

        /// <summary>
        /// Spray the pan and begin heating it.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void SprayAndHeatPan(string name)
        {
            // It takes 2 seconds to spray and heat up the pan.
            Console.WriteLine($"> {name} sprays pan with oil and turns on stove to medium heat. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(2000).Wait();
        }

        /// <summary>
        /// Cook the eggs.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void CookEggs(string name)
        {
            // It takes 5 seconds to cook the eggs.
            Console.WriteLine($"> {name} cracks two eggs onto the pan, adds cheese, and lets it cook... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(5000).Wait();
            PutEggsOntoAPlate(name);
        }

        /// <summary>
        /// Toast the bread.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void ToastBread(string name)
        {
            // It takes 7 seconds to toast the bread.
            Console.WriteLine($"> {name} toasts the bread... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(7000).Wait();
            PutToastOntoAPlate(name);
        }

        /// <summary>
        /// Spray the pan and begin heating it asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        private async Task SprayAndHeatPanAsync(string name)
        {
            // It takes 2 seconds to spray and heat up the pan.
            Console.WriteLine($"> {name} sprays pan with oil and turns on stove to medium heat. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
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
            Console.WriteLine($"> {name} grabs eggs. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(1000).Wait();
            Console.WriteLine($"> {name} grabs cheese. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(1000).Wait();
            Console.WriteLine($"> {name} grabs bread. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(1000).Wait();
        }

        /// <summary>
        /// Toast the bread asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        private async Task ToastBreadAsync(string name)
        {
            // It takes 7 seconds to toast the bread.
            Console.WriteLine($"> {name} toasts the bread... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(7000);
            PutToastOntoAPlate(name);
        }

        /// <summary>
        /// Put the toast onto a plate.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void PutToastOntoAPlate(string name)
        {
            Console.WriteLine($"> {name} puts the toast onto a plate. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            // It takes a second to put the toast on a plate.
            Task.Delay(1000).Wait();
        }

        /// <summary>
        /// Cook the eggs asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        private async Task CookEggsAsync(string name)
        {
            // It takes 5 seconds to cook the eggs.
            Console.WriteLine($"> {name} cracks two eggs onto the pan, add cheese, and lets it cook... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(5000);
            PutEggsOntoAPlate(name);
        }

        /// <summary>
        /// Put the eggs onto a plate.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void PutEggsOntoAPlate(string name)
        {
            // It takes a second to put the eggs on a plate.
            Console.WriteLine($"> {name} puts the eggs onto a plate. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(1000).Wait();
        }
    }
}
