namespace SeleniumFramework.Generators.GeneratorData
{
    public class Address
    {
        #region Private Properties

        private readonly string _streetAddress;
        private readonly string _city;
        private readonly string _zipCode;
        private readonly string _state;

        #endregion

        #region Constructor

        public Address(string streetAddress, string city, string zipCode, string state)
        {
            _streetAddress = streetAddress;
            _city = city;
            _zipCode = zipCode;
            _state = state;
        }

        #endregion

        #region Public Properties

        public string StreetAddress => _streetAddress;

        public string City => _city;

        public string ZipCode => _zipCode;

        public string State => _state;

        #endregion

        #region Public Methods

        public string ToFullAddressString() => $"{_streetAddress} {_city}, {_state} {_zipCode}";

        #endregion

    }
}

