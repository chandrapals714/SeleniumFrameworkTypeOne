namespace SeleniumFramework.Generators.GeneratorData
{
    public class User
    {
        #region Private Properties

        private string _firstName;
        private string _lastName;
        private string _emailAddress;
        private string _phoneNumber;
        private string _userName;
        private string _password;
        private Address _mailingAddress;

        #endregion

        #region Constructor

        public User()
        {
            
        }
        
        public User(string first,string last,string email,string phone,string userName,string password,Address address)
        {
            _firstName = first;
            _lastName = last;
            _emailAddress = email;
            _phoneNumber = phone;
            _userName = userName;
            _password = password;
            _mailingAddress = address;
        }
        #endregion

        #region Public Properties

        public string FullName => $"{_firstName} {_lastName}";
        public string FirstName { get {  return _firstName; }set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public Address MailingAddress { get { return _mailingAddress; } set { _mailingAddress = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string Password { get { return _password; } set { _password = value; } }

        #endregion

    }
}