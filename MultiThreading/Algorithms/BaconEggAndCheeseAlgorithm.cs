﻿using System;
using System.Threading.Tasks;

namespace MultiThreading.Algorithms
{
    public class BaconEggAndCheeseAlgorithm : BreakfastAlgorithm
    {
        /// <summary>
        /// Algorithm to make breakfast.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public override void Make(string name)
        {
            GrabIngredients(name);
            SprayAndHeatPan(name);
            CookEggs(name);
            CookBacon(name);
            ToastBread(name);

            Console.WriteLine($"> {name} has gotten the bacon, eggs, cheese, and toast ready!");
        }

        /// <summary>
        /// Algorithm to make breakfast asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public async override Task MakeAsync(string name)
        {
            SprayAndHeatPanAsync(name);
            GrabIngredients(name);
            ToastBreadAsync(name);
            await CookEggsAsync(name);
            CookBacon(name);

            Console.WriteLine($"> {name} has gotten the bacon, eggs, cheese, and toast ready!");
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
            Console.WriteLine($"> {name} grabs eggs.");
            Task.Delay(1000).Wait();
            Console.WriteLine($"> {name} grabs cheese.");
            Task.Delay(1000).Wait();
            Console.WriteLine($"> {name} grabs bread.");
            Task.Delay(1000).Wait();
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
        /// Cook the eggs.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void CookEggs(string name)
        {
            // It takes 5 seconds to cook the eggs.
            Console.WriteLine($"> {name} cracks two eggs onto the pan, adds cheese, and lets it cook...");
            Task.Delay(5000).Wait();
            PutEggsOntoAPlate(name);
        }

        /// <summary>
        /// Cook the eggs asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        private async Task CookEggsAsync(string name)
        {
            // It takes 5 seconds to cook the eggs.
            Console.WriteLine($"> {name} cracks two eggs onto the pan, adds cheese, and lets it cook...");
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
            Console.WriteLine($"> {name} puts the eggs onto a plate.");
            Task.Delay(1000).Wait();
        }

        /// <summary>
        /// Cook the bacon.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void CookBacon(string name)
        {
            // It takes 3 seconds to cook the bacon.
            Console.WriteLine($"> {name} places the bacon on the pan and lets it cook...");
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

        /// <summary>
        /// Toast the bread.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void ToastBread(string name)
        {
            // It takes 7 seconds to toast the bread.
            Console.WriteLine($"> {name} toasts the bread...");
            Task.Delay(7000).Wait();
            PutToastOntoAPlate(name);
        }

        /// <summary>
        /// Toast the bread asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        private async Task ToastBreadAsync(string name)
        {
            // It takes 7 seconds to toast the bread.
            Console.WriteLine($"> {name} toasts the bread...");
            await Task.Delay(7000);
            PutToastOntoAPlate(name);
        }

        /// <summary>
        /// Put the toast onto a plate.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        private void PutToastOntoAPlate(string name)
        {
            // It takes a second to put the toast on a plate.
            Console.WriteLine($"> {name} puts the toast onto a plate.");
            Task.Delay(1000).Wait();
        }
    }
}
