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
                "6. Remove Developer From Team\n" +
                "7. Remove Developer from Database\n" +
                "8. Exit Application");
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
                        RemoveDevFromTeam();
                        break;
                    case "7":
                        RemoveDevFromDatabase();
                        break;
                    case "8":
                        isRunning = false;
                        Console.WriteLine("Thank you for using Komodo's Team Manager! Have a great day!");
                        break;
                    default:
                        break;

                }
            }
        }

        private void RemoveDevFromDatabase()
        {
            bool isRemovingDev = true;
            Console.Clear();
            Console.WriteLine("Enter the User ID of the developer you wish to remove! Press 0 to see list of Developers!");
            int userInput = int.Parse(Console.ReadLine());
            while (isRemovingDev)
            {
                if (userInput == 0)
                {
                    ViewAllDevelopers();
                }
                else
                {
                    Developer dev = _devRepo.GetDevById(userInput);
                    DisplayDeveloperInfo(dev);
                    Console.WriteLine("Is this the developer you wish to remove?\n" +
                        "1. Yes\n" +
                        "2. No");
                    string yesOrNo = Console.ReadLine();
                    if (yesOrNo == "1")
                    {
                        if (dev.TeamId != 0)
                        {
                            DevTeam team = _devTeamRepo.GetDevTeamById(dev.TeamId);
                            team.TeamMembers.Remove(dev);
                            _devRepo.UpdateDevTeamId(dev, 0);
                        }
                        _devRepo.DeleteDev(dev);
                        Console.WriteLine($"Thank you for removing {dev.FirstName} {dev.LastName}!");
                        Console.ReadKey();
                        isRemovingDev = false;
                    }
                }
            }
        }

        private void RemoveDevFromTeam()
        {
            bool isRemovingDev = true;
            while (isRemovingDev)
            {
                Console.Clear();
                Console.WriteLine("Enter the User ID of the developer you wish to remove from the team! Press 0 to see list of Developers!");
                int userInput = int.Parse(Console.ReadLine());

                if (userInput == 0)
                {
                    ViewAllDevelopers();
                }
                else
                {
                    // get the developer from the repo using the Id
                    Developer dev = _devRepo.GetDevById(userInput);
                    // show the user the developer info to verify correct user
                    DisplayDeveloperInfo(dev);
                    Console.WriteLine("Is this the developer you wish to remove from the team?\n" +
                        "1. Yes\n" +
                        "2. No");
                    string yesOrNo = Console.ReadLine();
                    if (yesOrNo == "1")
                    {
                        DevTeam team = _devTeamRepo.GetDevTeamById(dev.TeamId);
                        team.TeamMembers.Remove(dev);
                        // setting developer's team id to 0 (no team assigned)
                        _devRepo.UpdateDevTeamId(dev, 0);
                        Console.WriteLine($"Thank you for removing {dev.FirstName} {dev.LastName} from the team!");
                        Console.ReadKey();
                        isRemovingDev = false;
                    }
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
            bool isAddingDev = true;
            while (isAddingDev)
            {
                Console.Clear();
                Console.WriteLine("Enter the User ID of the developer you wish to add to the team! Press 0 to see list of Developers!");
                int userInput = int.Parse(Console.ReadLine());

                if (userInput == 0)
                {
                    ViewAllDevelopers();
                }
                else
                {

                    Developer dev = _devRepo.GetDevById(userInput);
                    DisplayDeveloperInfo(dev);
                    Console.WriteLine("Is this the developer you wish to add to the team?\n" +
                        "1. Yes\n" +
                        "2. No");
                    string yesOrNo = Console.ReadLine();
                    if (yesOrNo == "1")
                    {
                        ViewAllTeams();
                        Console.WriteLine("Enter the Team ID of the team you wish to add the developer to!");
                        int teamInput = int.Parse(Console.ReadLine());
                        DevTeam team = _devTeamRepo.GetDevTeamById(teamInput);
                        team.TeamMembers.Add(dev);
                        _devRepo.UpdateDevTeamId(dev, team.TeamId);
                        Console.WriteLine($"Thank you for adding {dev.FirstName} {dev.LastName} to the team!");
                        Console.ReadKey();
                        isAddingDev = false;
                    }
                }
            }
        }

        private void AddDeveloperToTeam(DevTeam team)
        {
            Console.Clear();
            Console.WriteLine("Enter the User ID of the developer you wish to add to the team! Press 0 to see list of Developers!");
            int userInput = int.Parse(Console.ReadLine());

            if (userInput == 0)
            {
                ViewAllDevelopers();
            }
            else
            {
                Developer dev = _devRepo.GetDevById(userInput);
                DisplayDeveloperInfo(dev);
                Console.WriteLine("Is this the developer you wish to add to the team?\n" +
                    "1. Yes\n" +
                    "2. No");
                string yesOrNo = Console.ReadLine();
                if (yesOrNo == "1")
                {
                    team.TeamMembers.Add(dev);
                    _devRepo.UpdateDevTeamId(dev, team.TeamId);
                    Console.WriteLine($"Thank you for adding {dev.FirstName} {dev.LastName} to the team!");
                    Console.ReadKey();
                }
            }
        }

        private void CreateTeam()
        {
            Console.WriteLine("Please input the name of your new team!");
            string userInputTeamName = Console.ReadLine();

            DevTeam newTeam = new DevTeam(userInputTeamName);

            bool isSuccessfull = _devTeamRepo.CreateTeam(newTeam);

            bool isAddingDevs = true;

            while (isAddingDevs)
            {
                AddDeveloperToTeam(newTeam);

                Console.WriteLine("Do you need to add more Developers to the team?\n" +
                    "1. Yes\n" +
                    "2. No");
                string userInputYesNo = Console.ReadLine();

                if (userInputYesNo == "1")
                {
                    continue;
                }
                else
                {
                    isAddingDevs = false;
                }
            }

            if (isSuccessfull)
            {
                Console.WriteLine($"{newTeam.TeamName} has been added to our Database.\n");
                DisplayTeamInfo(newTeam);
            }
            else
            {
                Console.WriteLine($"{userInputTeamName} could not be added to the database. Sorry!");
            }
            Console.WriteLine("Press Enter to go back to Main Menu!");
            Console.ReadLine();

        }

        private void AddDeveloper()
        {
            Console.Clear();

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
            var team = "";
            if (dev.TeamId == 0)
            {
                team = "No Team Assigned";
            }
            else
            {
                team = $"{dev.TeamId}";
            }

            Console.WriteLine($"UserID: {dev.Id}\n" +
                $"First Name:           {dev.FirstName}\n" +
                $"Last Name:            {dev.LastName}\n" +
                $"Pluralsight Access:   {dev.PluralsightAccess}\n" +
                $"Team ID:              {team}");
            Console.WriteLine("*************************************");
        }

        private void DisplayTeamInfo(DevTeam team)
        {
            Console.WriteLine($"Team ID: {team.TeamId}\n" +
                $"Team Name:             {team.TeamName}\n" +
                $"Team Members:          ");
            foreach (var dev in team.TeamMembers)
            {
                DisplayDeveloperInfo(dev);
            }
            Console.WriteLine("****************************");
        }
    }
}
