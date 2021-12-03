using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance.Repository
{
    public class DeveloperRepo
    {
        public List<Developer> _developers;

        private int _count = 0;

        public DeveloperRepo()
        {
            _developers = new List<Developer>();
        }

        public bool AddDev(Developer dev)
        {
            if (dev is null)
            {
                return false;
            }
            _count++;
            dev.Id = _count;
            _developers.Add(dev);
            return true;
        }

        public List<Developer> GetDevs()
        {
            return _developers;
        }

        public Developer GetDevById(int id)
        {
            foreach (Developer dev in _developers)
            {
                if (id == dev.Id)
                {
                    return dev;
                }
            }
            return null;
        }

        public bool UpdateDev(int id, Developer newDevData)
        {
            Developer oldDevData = GetDevById(id);
            if (oldDevData != null)
            {
                oldDevData.Id = newDevData.Id;
                oldDevData.FirstName = newDevData.FirstName;
                oldDevData.LastName = newDevData.LastName;
                oldDevData.PluralsightAccess = newDevData.PluralsightAccess;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDev(int id)
        {
            Developer devToBeDeleted = GetDevById(id);
            if (devToBeDeleted == null)
            {
                return false;
            }
            else
            {
                _developers.Remove(devToBeDeleted);
                return true;
            }
        }
    }
}
