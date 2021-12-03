using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KomodoInsurance.Repository;

namespace KomodoInsurance.UI
{
    class ProgramUI
    {
        private readonly DevTeamRepo _devTeamRepo = new DevTeamRepo();

        private readonly DeveloperRepo _devRepo = new DeveloperRepo();

        internal void Run()
        {
            RunApplication();
        }

        public void Menu()
        {
            Console.WriteLine("Welcome to Komodo Insurance Team Manager! What can we do for you today?\n" +
                "1. Add Developer\n" +
                "2. Create Team\n" +
                "3. Add Developer To Team\n" +
                "4. View Developers\n" +
                "5. View Teams\n" +
                "6. Exit Application");
        }

        private void RunApplication()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Menu();
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        AddDeveloper();
                        break;
                    case "2":
                        CreateTeam();
                        break;
                    case "3":
                        AddDeveloperToTeam();
                        break;
                    case "4":
                        ViewAllDevelopers();
                        break;
                    case "5":
                        ViewAllTeams();
                        break;
                    case "6":
                        isRunning = false;
                        Console.WriteLine("Thank you for using Komodo's Team Manager! Have a great day!");
                        break;
                    default:
                        break;

                }
            }
        }

        private void ViewAllTeams()
        {
            Console.Clear();
            List<DevTeam> listOfAllTeams = _devTeamRepo.GetDevTeams();

            foreach (var team in listOfAllTeams)
            {
                DisplayTeamInfo(team);
            }
            Console.ReadKey();
        }

        private void ViewAllDevelopers()
        {
            Console.Clear();
            List<Developer> listOfAllDevelopers = _devRepo.GetDevs();

            foreach (var dev in listOfAllDevelopers)
            {
                DisplayDeveloperInfo(dev);
            }
            Console.ReadKey();
        }

        private void AddDeveloperToTeam()
        {
            throw new NotImplementedException();
        }

        private void CreateTeam()
        {
            throw new NotImplementedException();
        }

        private void AddDeveloper()
        {
            Console.WriteLine("Please input the developers first name:");
            string userInputFirstName = Console.ReadLine();

            Console.WriteLine("Please input the developers last name:");
            string userInputLastName = Console.ReadLine();

            Console.WriteLine("Does the developer have access to Pluralsight?\n" +
                "1. Yes\n" +
                "2. No");

            string userInput = Console.ReadLine();

            Developer newDev = new Developer(userInputFirstName, userInputLastName);


            if (userInput == "1")
            {
                newDev.PluralsightAccess = true;
            }
            else
            {
                newDev.PluralsightAccess = false;
            }

            bool isSuccessful = _devRepo.AddDev(newDev);
            if (isSuccessful)
            {
                Console.WriteLine($"{newDev.FirstName} {newDev.LastName} has been added to our database. Their User Id is {newDev.Id}. Thank you!");
            }
            else
            {
                Console.WriteLine($"{userInputFirstName} {userInputLastName} couldn't be added to database. Sorry!");
            }
            Console.WriteLine("Press Enter to go back to Main Menu!");
            Console.ReadLine();
        }

        private void DisplayDeveloperInfo(Developer dev)
        {
            Console.WriteLine($"UserID: {dev.Id}\n" +
                $"First Name:           {dev.FirstName}\n" +
                $"Last Name:            {dev.LastName}\n" +
                $"Pluralsight Access:   {dev.PluralsightAccess}");
            Console.WriteLine("*************************************");
        }

        private void DisplayTeamInfo(DevTeam team)
        {
            Console.WriteLine($"Team ID: {team.Id}\n" +
                $"Team Name:             {team.TeamName}\n" +
                $"Team Members:          {team.TeamMembers}");
            Console.WriteLine("****************************");
        }
    }
}
