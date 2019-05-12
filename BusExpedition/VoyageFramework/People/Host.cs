using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Host : Person
    {
        public Host(string firstName, string lastName, DateTime dateOfBirth) :
            base(firstName, lastName)
        {
            DateOfBirth = dateOfBirth;
            if (Age < 18)
            {
                throw new ArgumentException(nameof(dateOfBirth));
            }
        }
    }
}
