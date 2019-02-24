using System.Threading.Tasks;

namespace MultiThreading.Algorithms
{
    public abstract class BreakfastAlgorithm
    {
        /// <summary>
        /// Algorithm to make breakfast.
        /// </summary>
        public abstract void Make();

        /// <summary>
        /// Algorithm to make breakfast asynchronously.
        /// </summary>
        public abstract Task MakeAsync();
    }
}
