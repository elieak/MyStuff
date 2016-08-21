//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProjectBuilder
//{
//    class Program
//    {
//        static void Main2(string[] args)
//        {
//            ParallelProjectBuilder();
//        }

//        static void BuildProject(int projectNumber)
//        {
//            Console.WriteLine($"Building project {projectNumber}:");
//            Thread.Sleep(1000);
//            Console.WriteLine("=========Done=========");
//        }
//        static void ParallelProjectBuilder()
//        {
//            new List<List<Task>>();

//            Task Project1Task = Task.Factory.StartNew(() => BuildProject(1));
//            Task Project2Task = Task.Factory.StartNew(() => BuildProject(2));
//            Task Project3Task = Task.Factory.StartNew(() => BuildProject(3));
//            Task Project4Task = Project1Task.ContinueWith(x => BuildProject(4));
//            Task Project5Task = Task.Factory.ContinueWhenAll(new Task[] { Project1Task, Project2Task, Project3Task }, x => BuildProject(5));
//            Task Project6Task = Task.Factory.ContinueWhenAll(new Task[] { Project3Task, Project4Task }, x => BuildProject(6));
//            Task Project7Task = Task.Factory.ContinueWhenAll(new Task[] { Project5Task, Project6Task }, x => BuildProject(7));
//            Task Project8Task = Project5Task.ContinueWith(x => BuildProject(8));
//            Task.WaitAll(Project7Task, Project8Task);
//        }
//    }
//}