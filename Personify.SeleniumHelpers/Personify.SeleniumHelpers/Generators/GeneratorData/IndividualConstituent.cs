using System.Runtime.InteropServices;
using System.Threading;


namespace SeleniumFramework.Generators.GeneratorData
{
    public class IndividualConstituent : User
    {

        private string _class = "";
        private string _middleName = "";
        private string _id = "";
        private string _prefix = "";
        private string _suffix = "";
        private string _credentials = "";
        private string _parentConstId = "";
        private string _parentConstName = "";
        private string _companyName = "";
        private string _jobTitle = "";
        private string _mailStop = "";
        private string _countryCode = "";
        private bool _confidential = false;
        private bool _includeInPrint = false;
        private bool _includeInMobile = false;
        private string _directoryPriority = "0";
        private bool _defaultBillTo = false;
        private bool _defaultShipTo = false;
        private bool _seasonalAddress = false;
        private string _dateBegins = "";
        private string _dateEnds = "";
        private bool _recurringSeasonalAddress = false;
        private string _addressType = "Home";
        private string _personalLine = "";

        public IndividualConstituent(string first, string last, Address address, string email = "", string phone = "", string userName = "", string password = "",string MiddleName = "",string AddressType = "Home",string Prefix = "", string Suffix = "", string Credentials = "",
            string ParentConstituentId = "", string ParentConstituentName = "", string CompanyName = "",
            string JobTitle = "test", string MailStop = "test", string CountryCode = "", bool Confidential = false, bool IncludeInPrintDirectory = true, string PersonalLine = "",
            bool IncludeInMobileDirectory = true, string DirectoryPriority = "0", bool DefaultBillTo = false,
            bool DefaultShipTo = false, bool SeasonalAddress = false, string DateBegins = "", string DateEnds = "", bool RecurringSeasonalAddress= false)
        {
            base.FirstName = first;
            base.LastName = last;
            base.EmailAddress = email;
            base.PhoneNumber = phone;
            base.UserName = userName;
            base.Password = password;
            base.MailingAddress = address;
            _class = "Individual";
            _middleName = MiddleName;
            _prefix = Prefix;
            _suffix = Suffix;
            _credentials = Credentials;
            _parentConstId = ParentConstituentId;
            _parentConstName = ParentConstituentName;
            _companyName = CompanyName;
            _jobTitle = JobTitle;
            _mailStop = MailStop;
            _personalLine = PersonalLine;
            _countryCode = CountryCode;
            _confidential = Confidential;
            _includeInPrint = IncludeInPrintDirectory;
            _includeInMobile = IncludeInMobileDirectory;
            _directoryPriority = DirectoryPriority;
            _defaultBillTo = DefaultBillTo;
            _defaultShipTo = DefaultShipTo;
            _seasonalAddress = SeasonalAddress;
            _dateBegins = DateBegins;
            _dateEnds = DateEnds;
            _recurringSeasonalAddress = RecurringSeasonalAddress;
        }

        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }
        public string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; }
        }
        public string Credentials
        {
            get { return _credentials; }
            set { _credentials = value; }
        }
        public string ParentConstituentId
        {
            get { return _parentConstId; }
            set { _parentConstId = value; }
        }
        public string ParentConstituentName
        {
            get { return _parentConstName; }
            set { _parentConstName = value; }
        }
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        public string JobTitle
        {
            get { return _jobTitle; }
            set { _jobTitle = value; }
        }
        public string MailStop
        {
            get { return _mailStop; }
            set { _mailStop = value; }
        }
        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }
        public bool Confidential
        {
            get { return _confidential; }
            set { _confidential = value; }
        }
        public bool IncludeInPrintDirectory
        {
            get { return _includeInPrint; }
            set { _includeInPrint = value; }
        }
        public bool IncludeInMobileDirectory
        {
            get { return _includeInMobile; }
            set { _includeInMobile = value; }
        }
        public string DirectoryPriority
        {
            get { return _directoryPriority; }
            set { _directoryPriority = value; }
        }
        public bool DefaultBillTo
        {
            get { return _defaultBillTo; }
            set { _defaultBillTo = value; }
        }
        public bool DefualtShipTo
        {
            get { return _defaultShipTo; }
            set { _defaultShipTo = value; }
        }
        public bool SeasonalAddress
        {
            get { return _seasonalAddress; }
            set { _seasonalAddress = value; }
        }
        public string DateBegins
        {
            get { return _dateBegins; }
            set { _dateBegins = value; }
        }
        public string DateEnds
        {
            get { return _dateEnds; }
            set { _dateEnds = value; }
        }
        public bool RecurringSeasonalAddress
        {
            get { return _recurringSeasonalAddress; }
            set { _recurringSeasonalAddress = value; }
        }

        public string AddressType
        {
            get { return _addressType; }
            set { _addressType = value; }
        }
        public string PersonalLine
        {
            get { return _personalLine; }
            set { _personalLine = value; }
        }
    }
}