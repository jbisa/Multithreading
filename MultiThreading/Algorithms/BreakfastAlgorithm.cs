using System.Threading.Tasks;

namespace MultiThreading.Algorithms
{
    public abstract class BreakfastAlgorithm
    {
        /// <summary>
        /// Algorithm to make breakfast.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public abstract void Make(string name);

        /// <summary>
        /// Algorithm to make breakfast asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        public abstract Task MakeAsync(string name);
    }
}
