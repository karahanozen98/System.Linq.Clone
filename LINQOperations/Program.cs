using LINQOperations.Test;
using System;

namespace LINQOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var testLinq = new TestLinq();

            Console.WriteLine(testLinq.TestAll());

            Console.ReadLine();
        }
    }
}
