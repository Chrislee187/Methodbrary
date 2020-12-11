using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Methodbrary.System.Threading.Tasks
{
    public static class TaskExtensions
    {
        public static IEnumerable<Task> StartAll(this IEnumerable<Task> tasks)
        {
            var startAll = tasks as Task[] ?? tasks.ToArray();

            foreach (var task in startAll)
            {
                task.Start();
            }

            return startAll;
        }

        public static IEnumerable<Task> WaitAll(this IEnumerable<Task> tasks)
        {
            var waitAll = tasks as Task[] ?? tasks.ToArray();
            Task.WaitAll(waitAll.ToArray());

            return waitAll;
        }
    }
}