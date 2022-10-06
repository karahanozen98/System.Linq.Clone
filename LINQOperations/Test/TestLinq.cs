
//using System.Linq;
using LINQOperations.Extensions;
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
            new Car { Name = "Car-3", Brand= "B2"}
        };

            this.AddResult(fruits.ElementAtOrDefault(10));

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


            return this.results;

        }
    }
}
