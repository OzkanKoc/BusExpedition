using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
        public string IdentityNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Today.Year - DateOfBirth.Year;

                return DateTime.Today.DayOfYear > DateOfBirth.DayOfYear ? age-- : age;
            }
        }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
