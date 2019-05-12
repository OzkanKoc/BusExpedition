using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework.Collections
{
    public class DriverCollection
    {
        private Driver[] _drivers = new Driver[0];
        public int Count
        {
            get
            {
                return _drivers.Length;
            }
        }

        public Driver this[int index]
        {
            get
            {
                return _drivers[index];
            }
        }
        public void Add(Driver driver)
        {
            Array.Resize(ref _drivers, _drivers.Length + 1);
            _drivers[_drivers.Length - 1] = driver;
        }
        public void Remove(Driver driver)
        {
            for (int i = 0; i < IndexOf(driver) - 1; i++)
            {
                _drivers[i] = _drivers[i + 1];
            }
            Array.Resize(ref _drivers, _drivers.Length - 1);
        }
        public int IndexOf(Driver driver)
        {
            return Array.IndexOf(_drivers, driver);
        }
        public bool Contains(Driver driver)
        {
            for (int i = 0; i < _drivers.Length; i++)
            {
                if (driver == _drivers[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
