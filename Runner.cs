using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace MasswordPanager
{
    class Runner
    {
        public bool SignedIn;
        public Dictionary<string, string> PasswordManager;
        public Runner()
        {
            SignedIn = false;
            PasswordManager = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Setup()
        {
            Console.WriteLine("Welcome to the password manager:");
            File.Decrypt("C:/Users/jlundy/Documents/PM/MasswordPanager/EPW.txt"); 
            var file = File.ReadAllText("C:/Users/jlundy/Documents/PM/MasswordPanager/EPW.txt");
            var password = file.Split(new char[] { '\n', '\r' });
            foreach (var item in password)
            {
                if (item != "")
                {
                    var site = item.Split(',')[0];
                    var pwrd = item.Split(',')[1];
                    PasswordManager.Add(site, pwrd);
                }
            }
            File.Delete("C:/Users/jlundy/Documents/PM/MasswordPanager/EPW.txt");
            return PasswordManager;

        }

        public void CheckPassword(string password)
        {
            while (!SignedIn)
            {
                Console.WriteLine();
                Console.WriteLine("Enter program password: ");
                string enteredPW = null;
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    enteredPW += key.KeyChar;
                }

                if (enteredPW == password)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome Back!");

                    SignedIn = true;
                }
            }
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("   1. Display all sites");
            Console.WriteLine("   2. Search by site ");
            Console.WriteLine("   3. Add");
            Console.WriteLine("   4. Update");
            Console.WriteLine("   5. Delete");
            Console.WriteLine("   6. Exit program");
            var option = Console.ReadKey().KeyChar;
            switch (option)
            {
                case '1':
                    DisplayAll();
                    break;
                case '2':
                    Search();
                    break;
                case '3':
                    Add();
                    break;
                case '4':
                    //UPDATE
                    break;
                case '5':
                    //DELETE
                    break;
                case '6':
                    //Exit
                default:
                    break;
            }
        }

        public void DisplayAll()
        {
            Console.Clear();
            Console.WriteLine("     SITE          PASSWORD   ");
            Console.WriteLine("     ____          ________   ");
            Console.WriteLine();
            foreach(KeyValuePair<string, string> kvp in PasswordManager)
            {
                Console.WriteLine("{0,10} {1,15}", kvp.Key, kvp.Value);
                Console.WriteLine();
            }

            ReturnToMenu();

        }

        public void ReturnToMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to return to the menu? (Y/N)");
            var choise = Console.ReadLine();

            switch (choise)
            {
                case "Y":
                case "y":
                    Menu();
                    break;
                case "N":
                case "n":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine($"Error: {choise} is not a valid response.");
                    ReturnToMenu();
                    break;
            }
        }

        public void Search()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the site: ");
            var site = Console.ReadLine();
            Console.Clear();
            string pw;
            if (PasswordManager.TryGetValue(site, out pw))
            {
                Console.WriteLine($"Site: {site}");
                Console.WriteLine($"Password: {pw}");
            }
            else
            {
                Console.WriteLine($"No match found for {site}");
            }

            ReturnToMenu();
        }

        public void Add()
        {
            Console.Clear();
            Console.WriteLine("Enter the site name");
            var site = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"What is password for {site}?");
            var pw = Console.ReadLine();
            PasswordManager.Add(site, pw);

            Menu();
        }

        public void Update()
        {

            Menu();
        }

        public void Delete()
        {



            Menu();
        }

        public void ExitProgram()
        {
            Console.Clear();
            Console.WriteLine("Goodbye");
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("C:/Users/jlundy/Documents/PM/MasswordPanager", "EPW.txt")))
            {
                foreach (var item in PasswordManager)
                {
                    var line = item.Key + ',' + item.Value;
                    outputFile.WriteLine(line);
                }
                    
            }
            File.Encrypt("C:/Users/jlundy/Documents/PM/MasswordPanager/EPW.txt");

        }
    }
}
