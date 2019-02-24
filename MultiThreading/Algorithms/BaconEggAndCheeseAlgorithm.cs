using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MultiThreading.Algorithms
{
    public class BaconEggAndCheeseAlgorithm : BreakfastAlgorithm
    {
        /// <summary>
        /// Algorithm to make breakfast.
        /// </summary>
        public override void Make()
        {
            var stopWatch = Stopwatch.StartNew();

            GrabIngredients();
            SprayAndHeatPan();
            CookEggs();
            CookBacon();
            ToastBread();

            stopWatch.Stop();

            BreakfastIsReady(stopWatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Algorithm to make breakfast asynchronously.
        /// </summary>
        public async override Task MakeAsync()
        {
            var stopWatch = Stopwatch.StartNew();

            SprayAndHeatPanAsync();
            GrabIngredients();
            ToastBreadAsync();
            await CookEggsAsync();
            CookBacon();

            stopWatch.Stop();

            BreakfastIsReady(stopWatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Grab the ingredients.
        /// </summary>
        private void GrabIngredients()
        {
            Console.WriteLine("Grab bacon, eggs, cheese and bread from the fridge.");
            // Let's imagine this takes 1 second to do...
            Task.Delay(2000).Wait();
        }

        /// <summary>
        /// Spray the pan and begin heating it.
        /// </summary>
        private void SprayAndHeatPan()
        {
            Console.WriteLine("Spray pan with oil and turn on stove to medium heat.");
            Task.Delay(3000).Wait();
        }

        /// <summary>
        /// Spray the pan and begin heating it asynchronously.
        /// </summary>
        /// <returns></returns>
        private async Task SprayAndHeatPanAsync()
        {
            Console.WriteLine("Spray pan with oil and turn on stove to medium heat.");
            await Task.Delay(3000);
        }

        /// <summary>
        /// Cook the eggs.
        /// </summary>
        private void CookEggs()
        {
            Console.WriteLine("Crack three eggs onto the pan and let them cook...");
            Task.Delay(5000).Wait();
            PutEggsOntoAPlate();
        }

        /// <summary>
        /// Cook the eggs asynchronously.
        /// </summary>
        /// <returns></returns>
        private async Task CookEggsAsync()
        {
            Console.WriteLine("Crack three eggs onto the pan and let them cook...");
            await Task.Delay(5000);
            PutEggsOntoAPlate();
        }

        /// <summary>
        /// Put the eggs onto a plate.
        /// </summary>
        private void PutEggsOntoAPlate()
        {
            Console.WriteLine("Put the eggs onto a plate.");
            Task.Delay(1000).Wait();
        }

        /// <summary>
        /// Cook the bacon.
        /// </summary>
        private void CookBacon()
        {
            Console.WriteLine("Place bacon on pan and let it cook...");
            Task.Delay(3000).Wait();
            PutBaconOntoAPlate();
        }

        /// <summary>
        /// Put the bacon onto a plate.
        /// </summary>
        private void PutBaconOntoAPlate()
        {
            Console.WriteLine("Put the bacon onto a plate.");
            Task.Delay(1000).Wait();
        }

        /// <summary>
        /// Toast the bread.
        /// </summary>
        private void ToastBread()
        {
            Console.WriteLine("Toast bread...");
            Task.Delay(7000).Wait();
            PutToastOntoAPlate();
        }

        /// <summary>
        /// Toast the bread asynchronously.
        /// </summary>
        /// <returns></returns>
        private async Task ToastBreadAsync()
        {
            Console.WriteLine("Toast bread...");
            await Task.Delay(7000);
            PutToastOntoAPlate();
        }

        /// <summary>
        /// Put the toast onto a plate.
        /// </summary>
        private void PutToastOntoAPlate()
        {
            Console.WriteLine("Put toast onto a plate");
            Task.Delay(1000).Wait();
        }

        /// <summary>
        /// Message that breakfast is ready.
        /// </summary>
        /// <param name="timeItTookToCookInMs">The time in took to cook in milliseconds.</param>
        private void BreakfastIsReady(long timeItTookToCookInMs)
        {
            Console.WriteLine("Hooray, breakfast is ready!");

            if (timeItTookToCookInMs > 20000)
            {
                Console.WriteLine($"Yikes! Breakfast is cold, it look this long to make: {timeItTookToCookInMs} ms!\n");
            }
            else
            {
                Console.WriteLine($"Nice! Breakfast is hot, it look this long to make: {timeItTookToCookInMs} ms!\n");
            }
        }
    }
}
