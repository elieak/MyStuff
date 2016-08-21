using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBuilder
{
    class Program2
    {
        public static void Main(string[] args)
        {
            var dependencies = new Dictionary<int, IEnumerable<int>>();

            dependencies[1] = Enumerable.Empty<int>();
            dependencies[2] = Enumerable.Empty<int>();
            dependencies[3] = Enumerable.Empty<int>();
            dependencies[4] = new[] { 1 };
            dependencies[5] = new[] { 4, 3 };
            dependencies[6] = new[] { 3, 2, 1 };
            dependencies[7] = new[] { 6, 5 };
            dependencies[8] = new[] { 5 };

            RunProjects(dependencies);
        }

        static void RunProjects(Dictionary<int, IEnumerable<int>> dependencies)
        {
            var tasks = new Dictionary<int, Task>();

            foreach (var key in dependencies.Keys)
            {
                BuildGraph(key, dependencies, tasks);
            }

            foreach (var dependency in tasks)
            {
                if (dependency.Value.Status == TaskStatus.Created)
                {
                    dependency.Value.Start();
                }
            }
        }
        static Task BuildGraph(int current, Dictionary<int, IEnumerable<int>> dependencies, Dictionary<int, Task> projects)
        {
            if (projects.ContainsKey(current))
            {
                return projects[current];
            }
            foreach (var dependency in dependencies[current])
            {
                BuildGraph(current, dependencies, projects);
            }

            Task task;

            if (dependencies[current].Any())
            {
                task = Task.Factory.ContinueWhenAll(dependencies[current].Select(d => projects[d]).ToArray(), (t) =>
                {
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    Console.WriteLine($"Task {current} finished.");
                });
            }
            else
            {
                task = new Task(() =>
                {
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    Console.WriteLine($"Task {current} finished.");
                });
            }
            projects[current] = task;
            return task;
        }
    }
}
