
using System;

public class Program
{
    public static void Main()
    {
        // Ask for user's name
        Console.Write("Enter your name:  ");
        string name = Console.ReadLine(); // Reads input from the user

        // Ask for user's age
        Console.Write("Enter your age: ");
        string ageInput = Console.ReadLine(); // Reads input as string
        int age = int.Parse(ageInput);       // Converts string to integer

        // Print greeting
        Console.WriteLine($"Hello {name}! You are {age} years old.");

        // Wait for user to press a key before exiting
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
