using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAG_Repositories.Entities;
using TCAG_Repositories.Entities.Enums;
using TCAG_Repositories.Gremlin_Repositories;

namespace ToCatchAGremlin.UI.UIs
{
    public class Gremlin_List_ProgramUI
    {
        private readonly Gremlin_List_Repo _gRepo = new Gremlin_List_Repo();

        public void Run()
        {
            Seed();
            RunApplication();
        }

      

        private void RunApplication()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Welcome To The Gremlin Repository.\n" +
                    "1. Add A Gremlin to the Database.\n" +
                    "2. View All Gremlins\n" +
                    "3. View A Single Gremlin.\n" +
                    "4. Update an existing gremlin.\n" +
                    "5. Delete an existing gremlin.\n" +
                    "50. Close Application\n");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        AddGremlinToDatabase();
                        break;
                        case "2":
                        ViewAllGremlins();
                        break;
                    case "3":
                        ViewAnExistingGremlin();
                        break;
                    case "4":
                        UpdateAnExistingGremlin();
                        break ;
                        case "5":
                        DeleteAnExistingGremlin();
                        break;
                    case "50":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("invalid selection");
                        break;
                }
            Console.Clear();
            }
        }

        private void AddGremlinToDatabase()
        {
            Console.Clear();
            Gremlin gremlin = new Gremlin();
            Console.WriteLine("Please enter a gremlin Name.");
            gremlin.Name = Console.ReadLine();

            Console.WriteLine("Please select a gremlin type\n" +
                "1. King\n" +
                "2. Enforcer\n" +
                "3. Pawn\n");
            var chosenValue = int.Parse(Console.ReadLine());
            gremlin.GremlinType = (GremlinType)chosenValue;

            bool success = _gRepo.AddGremlinToDatabase(gremlin);
            if (success)
            {
                Console.WriteLine($"{gremlin.Name} has been added to the database.");
            }
            else
            {
                Console.WriteLine("Gremlin failed to be added to database.");
            }
            Console.ReadKey();
        }

        private void ViewAllGremlins()
        {
            Console.Clear();
            List<Gremlin> gremlinsInDatabase = _gRepo.GetGremlins();
            foreach (var gremlin in gremlinsInDatabase)
            {
                DisplayGremlinData(gremlin);
            }
            Console.ReadKey();
        }
        private void DisplayGremlinData(Gremlin gremlin)
        {
            Console.WriteLine($"GremlinID: {gremlin.ID}\n" +
                $"GremlinName: {gremlin.Name}\n" +
                $"GremlinType: {gremlin.GremlinType}\n" +
                $"GremlinValue: {gremlin.GremlinValue}\n" +
                $"-----------------------------------------\n");
        }
        private void ViewAnExistingGremlin()
        {
            Console.Clear();
            Console.WriteLine("Please input the ID of an existing gremlin");
            int gremlinID = int.Parse(Console.ReadLine());
            Gremlin gremlin = _gRepo.GetGremlin(gremlinID);
            if (gremlin != null)
            {
                DisplayGremlinData(gremlin);
            }
            else
            {
                Console.WriteLine($"The gremlin with ID: {gremlinID} does not exist");
            }
            Console.ReadKey();
        }

        private void UpdateAnExistingGremlin()
        {
            Console.Clear();
            Console.WriteLine("Please input an existing gremlin ID for Update.");
            int gremlinID = int.Parse(Console.ReadLine());

            Gremlin gremlin = new Gremlin();
            Console.WriteLine("Please enter a gremlin Name.");
            gremlin.Name = Console.ReadLine();

            Console.WriteLine("Please select a gremlin type\n" +
                "1. King\n" +
                "2. Enforcer\n" +
                "3. Pawn\n");
            var chosenValue = int.Parse(Console.ReadLine());
            gremlin.GremlinType = (GremlinType)chosenValue;

            bool success = _gRepo.UpdateGremlinData(gremlinID, gremlin);
            if(success)
            {
                Console.WriteLine("SUCCESS");

            }
            else
            {
                Console.WriteLine("Gremlin failed to be updated.");
            }

            Console.ReadKey();
        }

        private void DeleteAnExistingGremlin()
        {
            Console.Clear();
            Console.WriteLine("Please input the ID of an existing gremlin to delete");
            int gremlinID = int.Parse(Console.ReadLine());
            bool success = _gRepo.DeleteGremlin(gremlinID);
            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine($"The gremlin with ID: {gremlinID} does not exist");
            }
            Console.ReadKey();
        }
        private void Seed()
        {
            Gremlin gremlinA = new Gremlin("Bob",GremlinType.Pawn);
            Gremlin gremlinB = new Gremlin("Sam",GremlinType.King);
            Gremlin gremlinC = new Gremlin("Jon",GremlinType.Enforcer);
            _gRepo.AddGremlinToDatabase(gremlinA);
            _gRepo.AddGremlinToDatabase(gremlinB);
            _gRepo.AddGremlinToDatabase(gremlinC);
        }

    }
}
