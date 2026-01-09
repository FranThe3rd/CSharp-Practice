using System;
using System.Collections.Generic;
using System.Linq;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}

public class Program
{
    public static void Main()
    {
        List<Person> people = new List<Person>()
        {
            new Person { Name = "Alice", Age = 25, City = "New York" },
            new Person {Name = "Bob", Age = 53, City = "Bethlehem"},
            new Person { Name = "Bob", Age = 30, City = "Los Angeles" },
            new Person { Name = "Charlie", Age = 35, City = "New York" },
            new Person { Name = "Diana", Age = 28, City = "Chicago" },
            new Person { Name = "Eve", Age = 40, City = "Los Angeles" },
            new Person { Name = "Frank", Age = 22, City = "Chicago" },
        };

        // 1. Filter people older than 30 and print their names
        var olderThan30 = people.Where(p => p.Age > 30);
        Console.WriteLine("People older than 30:");
        foreach (var p in olderThan30) Console.WriteLine(p.Name);
        Console.WriteLine();

        // 2. Select just the names of all people and print them
        var names = people.Select(p => p.Name);
        Console.WriteLine("All names:");
        foreach (var n in names) Console.WriteLine(n);
        Console.WriteLine();

        // 3. Sort people by age ascending and print name + age
        var sortedByAge = people.OrderBy(p => p.Age);
        Console.WriteLine("People sorted by age ascending:");
        foreach (var p in sortedByAge) Console.WriteLine($"{p.Name} - {p.Age}");
        Console.WriteLine();

        // 4. Sort people by age descending and print name + age
        var sortedByAgeDesc = people.OrderByDescending(p => p.Age);
        Console.WriteLine("People sorted by age descending:");
        foreach (var p in sortedByAgeDesc) Console.WriteLine($"{p.Name} - {p.Age}");
        Console.WriteLine();

        // 5. Find the first person from Chicago and print the name
        var firstChicago = people.FirstOrDefault(p => p.City == "Chicago");
        Console.WriteLine("First person from Chicago:");
        Console.WriteLine(firstChicago?.Name);
        Console.WriteLine();

        // 6. Check if any person is under 20 and print true/false
        bool anyUnder20 = people.Any(p => p.Age < 20);
        Console.WriteLine("Any person under 20?");
        Console.WriteLine(anyUnder20);
        Console.WriteLine();

        // 7. Check if all people are over 18 and print true/false
        bool allOver18 = people.All(p => p.Age > 18);
        Console.WriteLine("All people over 18?");
        Console.WriteLine(allOver18);
        Console.WriteLine();

        // 8. Group people by city and print each city with the names in it
        var groupedByCity = people.GroupBy(p => p.City);
        Console.WriteLine("People grouped by city:");
        foreach (var group in groupedByCity)
            Console.WriteLine($"{group.Key}: {string.Join(", ", group.Select(p => p.Name))}");
        Console.WriteLine();

        // 9. Average age of all people and print it
        double avgAge = people.Average(p => p.Age);
        Console.WriteLine("Average age:");
        Console.WriteLine(avgAge);
        Console.WriteLine();

        // 10. Total age of people in New York and print it
        int totalNYAge = people.Where(p => p.City == "New York").Sum(p => p.Age);
        Console.WriteLine("Total age of people in New York:");
        Console.WriteLine(totalNYAge);
    }
}
