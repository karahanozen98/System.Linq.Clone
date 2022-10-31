using LINQOperations.Extensions;
using System;
using System.Collections.Generic;

namespace LINQOperations.Test
{
    public class Car
    {
        public string Name { get; set; }

        public string Brand { get; set; }
    }

    public class TestLinq
    {
        private string results = string.Empty;

        public void AddResult(string result)
        {
            this.results += result + "\n";
        }

        public void AddResult<T>(IEnumerable<T> arr)
        {
            this.results += string.Join(", ", arr) + "\n";
        }

        public string TestAll()
        {
            int[] numbers = { 0, 30, 20, 15, 90, 85, 40, 75 };
            string[] fruits = { "Apple", "Mango", "Lemon", "Banana" };
            Car[] cars = new Car[] {
            new Car { Name = "Car-1", Brand = "B1" },
            new Car { Name = "Car-2", Brand = "B1"},
            new Car { Name = "Car-3", Brand= "B2"}};


            var testAllMethodResult = cars.All(x => x.Name.StartsWith("Car")).ToString();
            this.AddResult(testAllMethodResult);

            this.AddResult(fruits.ElementAtOrDefault(10));
            var predicate = new Func<Car, bool>(x => x.Name == "Car-1");
            this.AddResult(cars.FirstOrDefault(predicate).ToString());

            this.AddResult(fruits.Any().ToString());
            this.AddResult(fruits.Any(x => x == "Banana").ToString());

            this.AddResult(fruits.Count().ToString());

            // Test Concat
            var concat = fruits.Concat(new[] { "Grapes", "Avocado" });
            this.AddResult(string.Join(", ", concat) + "\n");

            // Test Where
            this.AddResult(cars.Where(car => car.Brand == "B2").Count().ToString());
            this.AddResult(numbers.Where((number, index) => number <= index * 10));

            this.AddResult(cars.Append(new Car { Name = "Car-4", Brand = "B3" }).Select(x => x.Name));

            this.TestUnion();
            var reverse = cars.Reverse();
            return this.results;

        }

        public void TestUnion()
        {
            string[] dataSource1 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource2 = { "India", "uk", "Canada", "France", "Japan" };
            //Method Syntax
            var MS = dataSource1.Union(dataSource2).ToList();
            //Query Syntax
            var QS = (from country in dataSource1
                      select country)
                      .Union(dataSource2).ToList();
            foreach (var item in MS)
            {
                Console.WriteLine(item);
            }

        }
    }
}
