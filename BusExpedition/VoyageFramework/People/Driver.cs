using System;

namespace VoyageFramework
{
    public class Driver : Person
    {
        public Driver(string firstName, string lastName, LicenseType licenseType, DateTime dateOfBirt) :
            base(firstName, lastName)
        {
            LicenseType = licenseType;
            DateOfBirth = dateOfBirt;
            if (Age < 25)
            {
                throw new ArgumentException();
            }
        }
        public LicenseType LicenseType { get; set; }
    }
}