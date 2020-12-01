using System.Collections.Generic;
using System.Threading.Tasks;

namespace Methodbrary.System.Threading.Tasks
{
    public static class TaskExtensions
    {
        public static IEnumerable<Task> StartAll(this IEnumerable<Task> tasks)
        {
            foreach (var task in tasks)
            {
                task.Start();
            }

            return tasks;
        }

        public static IEnumerable<Task> WaitAll(this IEnumerable<Task> tasks)
        {
            Task.WaitAll((Task[])tasks);

            return tasks;
        }
    }
}