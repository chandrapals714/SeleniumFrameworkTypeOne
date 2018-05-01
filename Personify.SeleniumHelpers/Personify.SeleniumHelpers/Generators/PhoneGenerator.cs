using System;
using System.Runtime.InteropServices;

namespace SeleniumFramework.Generators
{
    public enum PhoneNumberType
    {
        Domestic,
        Foreign
    };
    
    public class PhoneGenerator : GeneratorBase
    {
        public string GetPhoneNumber(PhoneNumberType phoneNumberType)
        {
             LastGenerated = phoneNumberType == PhoneNumberType.Domestic ? 
                $"{GetRandNumString(3)}-{GetRandNumString(3)}-{GetRandNumString(4)}-{GetRandNumString(4)}" : //011 55 11 2345678
                $"011-{GetRandNumString(2)}-{GetRandNumString(2)}-{GetRandNumString(7)}";
            string phoneGen = LastGenerated;

            return phoneGen;
        }
    }
}