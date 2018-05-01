using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using SeleniumFramework.Generators.GeneratorData;

namespace SeleniumFramework.Generators 
{

    public class AddressGenerator : GeneratorBase
    {

#region Private Properties

        private readonly string[] _streetNames = {"Laurel", "Bayview", "Lamar", "First", "River", "43rd", "Duvall", "Gallows", "Aardvark", "Cherry", "Union", "Lombard", "Madison", "Park" };
        private readonly string[] _streetSuffixes = {"St.", "Ave.", "Blvd.", "Way", "Loop" };
        private readonly string[] _cities = {"Austin", "San Francisco", "Vienna", "Miami", "New York", "Whoville", "Yakima", "Eureka", "Bangladesh"};
        private readonly string[] _states = {"CA", "TX", "WA", "NY", "DE", "GA", "FL", "HI", "OK", "PA", "VA", "TN", "WV", "WI", "NJ"};
        private readonly string[] _cityZipState = {"Austin,Texas,78751","San Francisco,California,94105","Miami,Florida,33114","Vienna,Virginia,22181", "New York,New York,10001", "Yakima,Washington,98901","Scranton,Pennsylvania,18501"};
        private Address _lastGenerated;

        #endregion

#region Public Properties

        public new Address LastGenerated => _lastGenerated;

        #endregion

#region Public Methods

        public Address GetAddress()
        {
            var address = _cityZipState[Rng.Next(0, _cityZipState.Length)].Split(',');
            return _lastGenerated = new Address(GetStreet(), address[0], address[2], address[1]);
        } 
        
#endregion

#region Private Methods

        private string GetStreet()
        {
            return $"{GetRandNumString(Rng.Next(1,5))} {GetStreetName()} {GetStreetSuffix()}";
        }

        private string GetStreetName()
        {
            return _streetNames[Rng.Next(0, _streetNames.Length)];
        }

        private string GetStreetSuffix()
        {
            return _streetSuffixes[Rng.Next(0, _streetSuffixes.Length)];
        }

        private string GetCity()
        {
            return _cities[Rng.Next(0,_cities.Length)];
        }

        private string GetState()
        {
            return _states[Rng.Next(0, _states.Length)];
        }

        private string GetZipCode()
        {
            var zip = GetRandNumString(5);
            while (zip.IndexOf('0') == 0)
            {
                zip = GetRandNumString(5);
            }
           return zip;
        }
        
#endregion
    }
}