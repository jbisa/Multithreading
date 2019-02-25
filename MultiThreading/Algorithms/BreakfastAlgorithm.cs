using System.Threading.Tasks;

namespace MultiThreading.Algorithms
{
    public abstract class BreakfastAlgorithm
    {
        /// <summary>
        /// Algorithm to make breakfast.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        public abstract bool Make(string name);

        /// <summary>
        /// Algorithm to make breakfast asynchronously.
        /// </summary>
        /// <param name="name">The name of the person making breakfast.</param>
        /// <returns></returns>
        public abstract Task<bool> MakeAsync(string name);
    }
}
