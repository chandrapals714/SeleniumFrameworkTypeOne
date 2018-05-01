namespace SeleniumFramework.Generators.GeneratorData
{
    public class Company
    {
        #region Private Properties

        private string _companyName;
        private string _emailAddress;
        private string _phoneNumber;
        private Address _mailingAddress;

        #endregion

        #region Constructor

        public Company()
        {

        }

        //kw
        public Company(string company, string email, string phone, Address address)
        {
            _companyName = company;
            _emailAddress = email;
            _phoneNumber = phone;
            _mailingAddress = address;
        }
        #endregion

        #region Public Properties

        //kw
        public string CompanyName { get { return _companyName; } set { _companyName = value; } }
        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public Address MailingAddress { get { return _mailingAddress; } set { _mailingAddress = value; } }
        #endregion

    }
}