using System;
using System.IO;
using System.Collections.Generic;
namespace MasswordPanager
{
    class Program
    {
        static void Main(string[] args)
        {

            var run = new Runner();
            Dictionary<string, string> PasswordManager = run.Setup();
            if (PasswordManager.ContainsKey("thisProgram"))
                run.CheckPassword(PasswordManager.GetValueOrDefault("thisProgram"));
            else
            {
                Console.WriteLine("Error reading password file, check file.");
                Console.WriteLine("Would you like to create a file? (Y/N)");
                var answer = Console.ReadLine();
                //CREATE PASSWORD FILE
            }

            if (run.SignedIn)
                run.Menu();

        }

    }

}
