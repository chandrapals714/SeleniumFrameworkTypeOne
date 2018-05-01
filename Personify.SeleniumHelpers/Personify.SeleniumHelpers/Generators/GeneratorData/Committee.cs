namespace SeleniumFramework.Generators.GeneratorData
{
    public class Committee
    {
        #region Private Properties

        private string _committeeName;
        private string _emailAddress;
        private string _phoneNumber;
        private Address _mailingAddress;

        #endregion

        #region Constructor

        public Committee()
        {

        }

        //kw
        public Committee(string committee, string email, string phone, Address address)
        {
            _committeeName = committee;
            _emailAddress = email;
            _phoneNumber = phone;
            _mailingAddress = address;
        }
        #endregion

        #region Public Properties

        //kw
        public string CommitteeName { get { return _committeeName; } set { _committeeName = value; } }
        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public Address MailingAddress { get { return _mailingAddress; } set { _mailingAddress = value; } }
        #endregion

    }
}