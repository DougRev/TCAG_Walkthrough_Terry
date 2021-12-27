using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAG_Repositories.Employee_Repository;
using TCAG_Repositories.Entities;
using TCAG_Repositories.Entities.Enums;

namespace ToCatchAGremlin.UI.UIs
{
    public class Employee_DictionaryUI
    {
        private readonly EmployeeRepsoitory _eRepo = new EmployeeRepsoitory();
        
        public void Run()
        {
            SeedContent();
            RunApplication();
        }

        

        private void RunApplication()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Welcome to Catch A Gremlin Employee Database\n" +
               "1. Add Employee To Databse\n" +
               "2. View All Employees\n" +
               "3. View An Employee\n" +
               "4. Update Employee Data\n" +
               "5. Delete Employee\n" +
               "10. Close Application\n");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        AddEmployeeToDatabase();
                        break;
                    case "2":
                        ViewAllEmployees();
                        break;
                    case "3":
                        ViewAnEmployee();
                        break;
                    case "4":
                        UpdateEmployeeData();
                        break;
                    case "5":
                        DeleteEmployee();
                        break;
                    case "10":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
                Console.Clear();
            }
        }

        private void AddEmployeeToDatabase()
        {
            Console.Clear();
            Employee employee = new Employee();
            Console.WriteLine("Please input employee First Name");
            employee.FirstName = Console.ReadLine();
            Console.WriteLine("Please input employee Last Name");
            employee.LastName = Console.ReadLine();
            bool success = _eRepo.AddEmployeeToDatabase(employee);
            if (success)
            {
                Console.WriteLine($"{employee.FullName} has been added to the datatbase.");
            }
            else
            {
                Console.WriteLine("FAILED");
            }
            Console.ReadKey();
        }

        private void ViewAllEmployees()
        {
            Console.Clear();
            Dictionary<int, Employee> employeesInDatabase = _eRepo.GetEmployees();
            foreach (var employee in employeesInDatabase)
            {
                ViewEmployeeDetails(employee.Value);
            }
            Console.ReadKey();
        }
        private void ViewEmployeeDetails(Employee employee)
        {
            Console.WriteLine($"{employee.FullName}");
            Console.WriteLine("Has access to:");
           if(employee.EmployeeAccess.Count > 0)
            {
                foreach (var door in employee.EmployeeAccess)
                {
                    Console.WriteLine(door);
                }
            }
            else
            {
                Console.WriteLine("Employee doesnt have access to any doors.");

            }
            Console.WriteLine("Has caputred:");
           if (employee.CapturedGremlins.Count > 0)
            {
                foreach (var gremlin in employee.CapturedGremlins)
                {
                    Console.WriteLine($"{gremlin.Name}: {gremlin.GremlinType}");
                }
            }
            else
            {
                Console.WriteLine("Employee hasnt captured any gremlins.");
            }
            Console.WriteLine($"employee total earnings: {employee.TotalEarnings}");
            Console.WriteLine("------------------------------------------\n");
        }
        private void ViewAnEmployee()
        {
            Console.Clear();
            Console.WriteLine("Please enter existing employee ID:");
            int userInput = int.Parse(Console.ReadLine());
            Employee employee = _eRepo.GetEmployeeByKey(userInput);
            if(employee != null)
            {
                ViewEmployeeDetails(employee);
            }
            else
            {
                Console.WriteLine($"Employee with ID: {userInput} does not exist.");
            }
            Console.ReadKey();
        }

        private void UpdateEmployeeData()
        {
            Console.Clear();

            Console.WriteLine("Please enter existing employee ID:");
            int userInput = int.Parse(Console.ReadLine());

            Console.WriteLine("Please make a selection:\n" +
                "1. Add Employee Door Access\n" +
                "2. Remove EMployee Door Access\n" +
                "3. Enroll Gremlin Capture\n");
            var userSelection = int.Parse(Console.ReadLine());
            switch (userSelection)
            {
                case 1:
                    AddEmployeeDoorAccess(userInput);
                    break;
                    case 2:
                    RemoveEmployeeDoorAccess(userInput);
                    break;
                case 3:
                    EnrollGremlinCapture(userInput);
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            Console.ReadKey();
        }

        private void AddEmployeeDoorAccess(int userInput)
        {
            bool hasAddedAllDoors = false;
            while (!hasAddedAllDoors)
            {
                Console.Clear();
                Console.WriteLine("Please enter the door name.");
                var userResponse = Console.ReadLine();
                if (userResponse != null)
                {
                    _eRepo.GiveEmployeeAccessToRoom(userInput, userResponse);
                    Console.WriteLine("Do you want to add another room y/n?");
                    userResponse = Console.ReadLine();
                    if (userResponse == "Y".ToLower())
                    {
                        continue; 
                    }
                    else
                    {
                        hasAddedAllDoors = true;
                    }
                }
            }
            Console.ReadKey();
        }

        private void RemoveEmployeeDoorAccess(int userInput)
        {
            bool hasAddedAllDoors = false;
            while (!hasAddedAllDoors)
            {
                Console.Clear();
                Console.WriteLine("Please enter the door name.");
                var userResponse = Console.ReadLine();
                if (userResponse != null)
                {
                    _eRepo.RemoveEmployeeRoomAccess(userInput, userResponse);
                    Console.WriteLine("Do you want to remove another room y/n?");
                    userResponse = Console.ReadLine();
                    if (userResponse == "Y".ToLower())
                    {
                        continue;
                    }
                    else
                    {
                        hasAddedAllDoors = true;
                    }
                }
            }
                
        }

        private void EnrollGremlinCapture(int userInput)
        {
            Console.Clear();
            var employee = _eRepo.GetEmployeeByKey(userInput);
            if (employee != null)
            {
                Gremlin gremlin = new Gremlin();
                Console.WriteLine("Please enter a gremlin Name.");
                gremlin.Name = Console.ReadLine();

                Console.WriteLine("Please select a gremlin type\n" +
                    "1. King\n" +
                    "2. Enforcer\n" +
                    "3. Pawn\n");
                var chosenValue = int.Parse(Console.ReadLine());
                gremlin.GremlinType = (GremlinType)chosenValue;

                bool success = _eRepo.ApprehendedGremlin(gremlin, userInput);
                if (success)
                {
                    Console.WriteLine($"{employee.FullName} has apprehended {gremlin.Name} its worth {gremlin.GremlinValue}!");
                }
                else
                {
                    Console.WriteLine("FAILED!");
                }
            }
            Console.ReadKey();
        }

        private void DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("Please enter existing employee ID:");
            int userInput = int.Parse(Console.ReadLine());
            bool success = _eRepo.RemoveEmployee(userInput);
            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine($"Employee with ID: {userInput} does not exist.");
            }
            Console.ReadKey();
        }

        private void SeedContent()
        {
            Employee employeeA = new Employee("Donny","Brasco",
                new List<string> 
                { "A1",
                  "A2"
                },
                new List<Gremlin> 
                { 
                  new Gremlin 
                  {
                    Name="Stripe",
                    GremlinType=GremlinType.King
                  }, new Gremlin
                  {
                    Name="Stars",
                    GremlinType=GremlinType.Pawn
                  },
                });

            _eRepo.AddEmployeeToDatabase(employeeA);
        }
    }
}
