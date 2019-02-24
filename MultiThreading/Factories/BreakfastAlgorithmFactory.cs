using MultiThreading.Algorithms;

namespace MultiThreading.Factories
{
    public class BreakfastAlgorithmFactory
    {
        public BreakfastAlgorithm GetBreakfastAlgorithm(string breakast)
        {
            switch (breakast)
            {
                case "BaconEggAndCheese":
                    return new BaconEggAndCheeseAlgorithm();
                case "EggAndCheese":
                    return new EggAndCheeseAlgorithm();
                case "Bacon":
                    return new BaconAlgorithm();
                default:
                    return null;
            }
        }
    }
}
