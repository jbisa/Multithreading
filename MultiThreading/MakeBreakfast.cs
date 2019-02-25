using System;
using MultiThreading.Factories;
using MultiThreading.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Collections.Async;
using System.Collections.Concurrent;
using System.Threading;

namespace MultiThreading
{
    class MakeBreakfast
    {
        private static ConcurrentStack<string> PlacedOrders;
        private static ConcurrentStack<string> CompletedOrders;
        private static Thread waiter;
        private static Thread cook;

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Assign a chef in charge of making a bacon, egg, and cheese sandwich
            var breakfastAlgorithmFactory = new BreakfastAlgorithmFactory();
            var breakfastAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("BaconEggAndCheese");
            var chefA = new Chef("Jay", breakfastAlgorithm);

            // 1. See how fast the chef can make breakfast synchrosnously
            Console.WriteLine($"{chefA.Name} begins to prepare breakfast synchronously... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            var stopWatch = Stopwatch.StartNew();

            chefA.PrepareBreakfast();
            stopWatch.Stop();
            BreakfastIsReady(stopWatch.ElapsedMilliseconds);

            // 2. See how fast the chef can make breakfast asynchrosnously
            Console.WriteLine($"{chefA.Name} begins to prepare breakfast asynchronously... Thread ID: {Thread.CurrentThread.ManagedThreadId}");          
            stopWatch = Stopwatch.StartNew();
            await chefA.PrepareBreakfastAsync();
            stopWatch.Stop();
            BreakfastIsReady(stopWatch.ElapsedMilliseconds);

            // 3. Now, compare how fast breakfast can be made with TWO chefs
            // cooking synchronously (multithreaded programming)

            // Assign one chef to cook the eggs, cheese, and toast
            var eggAndCheeseAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("EggAndCheese");
            var chefB = new Chef("Chris", eggAndCheeseAlgorithm);

            // Assign another chef to cook the bacon
            var baconAlgorithm = breakfastAlgorithmFactory
                .GetBreakfastAlgorithm("Bacon");
            var chefC = new Chef("Ian", baconAlgorithm);

            var chefs = new List<Chef>();
            chefs.Add(chefB);
            chefs.Add(chefC);

            // The following is an example of Data Parallization
            Console.WriteLine($"{chefB.Name} and {chefC.Name} begin to prepare breakfast together synchronously... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            stopWatch = Stopwatch.StartNew();
            var numberOfWashedDishes = new ConcurrentBag<int>();
            Parallel.ForEach(chefs, chef =>
            {
                try
                {
                    chef.PrepareBreakfast();
                    var value = chef.WashDish();
                    numberOfWashedDishes.Add(value);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Uh Ohh, fire in the kitchen!!");
                }
            });

            stopWatch.Stop();
            var total = numberOfWashedDishes.Sum();
            BreakfastIsReady(stopWatch.ElapsedMilliseconds);
            Console.WriteLine($"Number of dishes washed: {total}... Thread ID: {Thread.CurrentThread.ManagedThreadId}\n");

            // 4. What if we ran everything in parallel?? MULTITHREADING FTW!!!
            // The following is an example of Task Parallization
            Console.WriteLine($"Now let's see what happens if EVERYTHING runs in parallel... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            stopWatch = Stopwatch.StartNew();
            Parallel.Invoke(HostTakeReservation, WaiterTakeOrder, CookMakeOrder);
            stopWatch.Stop();
            Console.WriteLine($"Invoking everything in parallel finished in: {stopWatch.ElapsedMilliseconds}... Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            /*
            var placedOrders = new ConcurrentBag<String>();

            placedOrders.Add("EggAndCheese");
            placedOrders.Add("Bacon");

            waiter = new Thread(new ThreadStart(SendOrderToKitchen));
            cook = new Thread(new ThreadStart(SendOrderToCustomer));
            foreach(var order in placedOrders)
            {
                PlacedOrders.Push(order);
                waiter.Start();
                cook.Start();
            }*/

            Console.WriteLine($"End of program... Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void HostTakeReservation()
        {
            Console.WriteLine($"Host greets customers and takes them to their seat: Thread ID = {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void WaiterTakeOrder()
        {
            Console.WriteLine($"Waiter takes order: Thread ID = {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void CookMakeOrder()
        {
            Console.WriteLine($"Cook makes order: Thread ID = {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void SendOrderToKitchen()
        {
            PlacedOrders.Push("foo");
        }

        private static void SendOrderToCustomer()
        {
            CompletedOrders.Push(PlacedOrders.First());
        }

        /// <summary>
        /// Message that breakfast is ready.
        /// </summary>
        /// <param name="timeItTookToCookInMs">The time in took to cook in milliseconds.</param>
        private static void BreakfastIsReady(long timeItTookToCookInMs)
        {
            Console.WriteLine("> Hooray, breakfast is ready!");

            // Use some arbitrary threshold to dictate that breakfast took too long to make
            if (timeItTookToCookInMs > 20000)
            {
                Console.WriteLine($"> Yikes! Breakfast is COLD, it look this long to make: {timeItTookToCookInMs} ms! Thread ID: {Thread.CurrentThread.ManagedThreadId}\n");
            }
            else
            {
                if (timeItTookToCookInMs < 15000)
                {
                    Console.WriteLine($"> Awesome!! Breakfast is HOT, it look this long to make: {timeItTookToCookInMs} ms! Thread ID: {Thread.CurrentThread.ManagedThreadId}\n");
                }
                else
                {
                    Console.WriteLine($"> Nice! Breakfast is WARM, it look this long to make: {timeItTookToCookInMs} ms! Thread ID: {Thread.CurrentThread.ManagedThreadId}\n");
                }
            }
        }
    }
}
