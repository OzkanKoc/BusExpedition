using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework.Collections
{
    public class HostCollection
    {
        private Host[] _hosts = new Host[0];
        public int Count
        {
            get
            {
                return _hosts.Length;
            }
        }

        public Host this[int index]
        {
            get
            {
                return _hosts[index];
            }
        }
        public void Add(Host host)
        {
            Array.Resize(ref _hosts, _hosts.Length + 1);
            _hosts[_hosts.Length - 1] = host;
        }
        public void Remove(Host host)
        {
            for (int i = 0; i < IndexOf(host) - 1; i++)
            {
                _hosts[i] = _hosts[i + 1];
            }
            Array.Resize(ref _hosts, _hosts.Length - 1);
        }
        public int IndexOf(Host host)
        {
            return Array.IndexOf(_hosts, host);
        }
        public bool Contains(Host host)
        {
            for (int i = 0; i < _hosts.Length; i++)
            {
                if (host == _hosts[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
