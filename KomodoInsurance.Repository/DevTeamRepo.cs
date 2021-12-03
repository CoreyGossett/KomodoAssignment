using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KomodoInsurance.Repository
{
    public class DevTeamRepo
    {
        private readonly List<DevTeam> _devteams = new List<DevTeam>();

        private int _count = 0;

        public bool CreateTeam(DevTeam team)
        {
            if (team is null)
            {
                return false;
            }
            else
            {
                _count++;
                team.Id = _count;
                _devteams.Add(team);
                return true;
            }
        }

        public List<DevTeam> GetDevTeams()
        {
            return _devteams;
        }

        public DevTeam GetDevTeamById(int id)
        {
            foreach (DevTeam team in _devteams)
            {
                if (id == team.Id)
                {
                    return team;
                }
            }
            return null;
        }

        public bool UpdateTeam(int id, DevTeam newTeamData)
        {
            DevTeam oldTeamData = GetDevTeamById(id);
            if (oldTeamData != null)
            {
                oldTeamData.Id = newTeamData.Id;
                oldTeamData.TeamName = newTeamData.TeamName;
                oldTeamData.TeamMembers = newTeamData.TeamMembers;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTeam(int id)
        {
            DevTeam teamToBeDeleted = GetDevTeamById(id);
            if (teamToBeDeleted == null)
            {
                return false;
            }
            else
            {
                _devteams.Remove(teamToBeDeleted);
                return true;
            }
        }

    }
}
