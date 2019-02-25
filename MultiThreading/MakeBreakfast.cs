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
        private static ConcurrentQueue<string> CustomerOrders;
        private static ConcurrentQueue<string> PlacedOrders;
        private static ConcurrentQueue<string> CompletedOrders;
        private static int numberOfCustomerOrders;
        private static int numberOfDeliveredOrders;
        private static Thread waiter1;
        private static Thread waiter2;
        private static Thread server1;
        private static Thread server2;
        private static Thread cook1;
        private static Thread cook2;
        private static Thread cook3;
        private static Thread manager;


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
            Console.WriteLine($"Invoking everything in parallel finished in: {stopWatch.ElapsedMilliseconds}... Thread ID: {Thread.CurrentThread.ManagedThreadId}\n");

            // 5. How would we solve the synchronization problem in 4?
            // The following is an example of synchronized or Managed Task Parallelization
            var orders = new List<string>
            {
                "EggsAndBacon",
                "Pancakes",
                "FrenchToast",
                "Waffles",
                "VeganOmelette",
                "Burrito",
                "BiscuitsAndGravy",
                "Grits",
                "FruitSalad",
                "Crepes",
                "CreamOfWheat",
                "CinnamonRoll",
                "CoffeeCake",
                "EnglishMuffin",
                "Sausage",
                "Scone",
                "Yogurt",
                "Toast",
                "Turnover",
                "Strudel",
                "SteakAndEggs",
                "BranMuffin",
                "HomeFries",
                "Hashbrowns",
                "Ham",
                "EggsOverEasy",
                "EggSandwich"
            };

            CustomerOrders = new ConcurrentQueue<string>();
            PlacedOrders = new ConcurrentQueue<string>();
            CompletedOrders = new ConcurrentQueue<string>();

            foreach (var order in orders)
            {
                CustomerOrders.Enqueue(order);
            }

            numberOfCustomerOrders = CustomerOrders.Count;

            waiter1 = new Thread(new ThreadStart(SendOrderToKitchen));
            waiter2 = new Thread(new ThreadStart(SendOrderToKitchen));
            cook1 = new Thread(new ThreadStart(SendOrderToServer));
            cook2 = new Thread(new ThreadStart(SendOrderToServer));
            cook3 = new Thread(new ThreadStart(SendOrderToServer));
            server1 = new Thread(new ThreadStart(SendOrderToCustomer));
            server2 = new Thread(new ThreadStart(SendOrderToCustomer));
            manager = new Thread(new ThreadStart(FinishBreakfast));
            manager.Start();
            waiter1.Start();
            waiter2.Start();
            cook1.Start();
            cook2.Start();
            cook3.Start();
            server1.Start();
            server2.Start();
            manager.Join();

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
            string currentOrder = string.Empty;
            //Loop through orders
            while (manager.IsAlive || !CustomerOrders.IsEmpty)
            {
                Console.WriteLine("Getting customer order...");
                bool retrieved;
                if (retrieved = CustomerOrders.TryDequeue(out currentOrder))
                {
                    //Make sure order is sent to cook
                    PlacedOrders.Enqueue(currentOrder);
                }
                Console.WriteLine("Order was retrieved? {0}", retrieved);
                Thread.Sleep(200);
            }
        }

        private static void SendOrderToServer()
        {
            Console.WriteLine("Cook started");
            var orderToCook = string.Empty;
            while (manager.IsAlive || !PlacedOrders.IsEmpty)
            {
                if (PlacedOrders.TryDequeue(out orderToCook))
                {
                    Thread.Sleep(new Random().Next(0, 500));
                    CompletedOrders.Enqueue(orderToCook);
                }
            }
        }
        private static void SendOrderToCustomer()
        {
            string completedOrder = string.Empty;
            while (manager.IsAlive || !CompletedOrders.IsEmpty)
            {
                if (CompletedOrders.TryDequeue(out completedOrder))
                {
                    Console.WriteLine(completedOrder + "has been delivered to customer");
                    numberOfDeliveredOrders++;
                }
            }
        }
        private static void FinishBreakfast()
        {
            while (numberOfCustomerOrders != numberOfDeliveredOrders)
            {
                Console.WriteLine("Come on team, we've got {0} more orders to go",
                    numberOfCustomerOrders - numberOfDeliveredOrders);
                Thread.Sleep(100);
            }
            Console.WriteLine("All Orders have been completed, good work team!");
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
