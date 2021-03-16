using System;
namespace GZIDAL002.Helpers
{
    public static class Utils
    {
        public static int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;

            // For leap years we need this
            if (birthDate > now.AddYears(-age))
                age--;

            return age;
        }
    }
}
