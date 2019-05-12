using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework.Collections
{
    public class TicketCollection
    {
        private Ticket[] _tickets = new Ticket[0];
        public int Count
        {
            get
            {
                return _tickets.Length;
            }
        }

        public Ticket this[int index]
        {
            get
            {
                return _tickets[index];
            }
        }
        public void Add(Ticket ticket)
        {
            Array.Resize(ref _tickets, _tickets.Length + 1);
            _tickets[_tickets.Length - 1] = ticket;
        }
        public void Remove(Ticket ticket)
        {
            for (int i = 0; i < IndexOf(ticket) - 1; i++)
            {
                _tickets[i] = _tickets[i + 1];
            }
            Array.Resize(ref _tickets, _tickets.Length - 1);
        }
        public int IndexOf(Ticket ticket)
        {
            return Array.IndexOf(_tickets, ticket);
        }
        public bool Contains(Ticket ticket)
        {
            for (int i = 0; i < _tickets.Length; i++)
            {
                if (ticket == _tickets[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
