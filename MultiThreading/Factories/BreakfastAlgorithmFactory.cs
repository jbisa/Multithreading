using MultiThreading.Algorithms;

namespace MultiThreading.Factories
{
    public class BreakfastAlgorithmFactory
    {
        public BreakfastAlgorithm GetBreakfastAlgorithm(string breakast)
        {
            switch (breakast)
            {
                case "Bacon Egg And Cheese":
                    return new BaconEggAndCheeseAlgorithm();
                default:
                    return null;
            }
        }
    }
}
