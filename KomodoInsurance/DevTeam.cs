using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance
{
    public class DevTeam
    {
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public List<Developer> TeamMembers { get; set; }

        public DevTeam()
        {
                
        }

        public DevTeam(string teamName)
        {
            TeamName = teamName;
        }
    }
}
