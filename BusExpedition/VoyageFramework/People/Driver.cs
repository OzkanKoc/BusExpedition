using System;

namespace VoyageFramework
{
    public class Driver : Person
    {
        public Driver(string firstName, string lastName, LicenseType licenseType, DateTime dateOfBirth) :
            base(firstName, lastName)
        {
            LicenseType = licenseType;
            DateOfBirth = dateOfBirth;
            if (Age < 25)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (licenseType == LicenseType.None)
            {
                throw new ArgumentOutOfRangeException(nameof(licenseType));
            }
        }
        public LicenseType LicenseType { get; set; }
    }
}