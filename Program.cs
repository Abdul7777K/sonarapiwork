using System;
using System.IO;
using System.Data.SqlClient;

namespace ExampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vulnerability: SQL Injection
            Console.WriteLine("Enter username:");
            var username = Console.ReadLine();
            var password = "secret"; // Hardcoded password (Code Smell: Hardcoded Credentials)
            var connectionString = $"Server=myServerAddress;Database=myDataBase;User Id={username};Password={password};";
            using (var connection = new SqlConnection(connectionString)) // Improper handling of database connections
            {
                try
                {
                    connection.Open(); // Attempt to open a connection without validation (Bug)
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to connect to database."); // Generic error handling (Code Smell)
                }
            }

            // Bug: Null Reference Exception
            string message = null;
            Console.WriteLine(message.Length); // Attempt to use a null object

            // Code Smell: Magic Numbers
            int retirementAge = 65;
            int userAge = 30; // Example user age
            if (userAge > retirementAge) // Use of a "magic number"
            {
                Console.WriteLine("You can retire now.");
            }

            // Code Smell: Duplicate Code
            LogActivity("User logged in.");
            LogActivity("User logged in."); // Duplicate logging (Bug: Incorrect Logic)

            // Vulnerability: Insecure Data Storage
            File.WriteAllText("userCredentials.txt", $"Username: {username}, Password: {password}"); // Writes sensitive data to a plain text file

            // Bug: Improper Exception Handling
            try
            {
                var result = DivideNumbers(10, 0); // Division by zero
                Console.WriteLine($"Division Result: {result}");
            }
            catch (Exception ex) // Catching generic exception instead of specific ones (Code Smell)
            {
                Console.WriteLine("An error occurred.");
            }
        }

        static double DivideNumbers(int dividend, int divisor)
        {
            return dividend / divisor; // Potential for division by zero exception
        }

        static void LogActivity(string message)
        {
            // Imagine this logs to a file or database
            Console.WriteLine($"Activity: {message}");
        }
    }
}
