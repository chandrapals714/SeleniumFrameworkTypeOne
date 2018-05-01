using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personify.Helpers;
using SeleniumFramework.Generators.GeneratorData;

namespace SeleniumFramework.Generators
{
    public enum UserType
    {
        Employee,
        Individual,
        Company,
        Base
    };

    public class UserGenerator : GeneratorBase
    {
#region Private Properties

        private User _lastGenerated;
        private readonly AddressGenerator _addressGen;
        private readonly EmailGenerator _emailGen;
        private readonly NameGenerator _nameGen;
        private readonly PhoneGenerator _phoneGen;
        
        #endregion

        #region Constructor

        public UserGenerator()
        {
            _addressGen = new AddressGenerator();
            _emailGen = new EmailGenerator();
            _nameGen = new NameGenerator();
            _phoneGen = new PhoneGenerator();
        }

#endregion

        #region Public Properties

        public new User LastGenerated => _lastGenerated;          

        #endregion

        #region Public Methods

        public User GetUser()
        {
            var name = _nameGen.GetName().Split(' ');
            return _lastGenerated = new User(name[0], name[1], _emailGen.GetEmailAddress(EmailType.Random),
            _phoneGen.GetPhoneNumber(PhoneNumberType.Domestic), "PopUpStuff", "PopUpStuff", _addressGen.GetAddress());
        }

        public IndividualConstituent GetIndividualConstituent(EmailType emailType, PhoneNumberType phoneNumberType, string parentConstId = "",bool confidential = false,
            bool includeInPrintDirectory = true,bool includeInMobileDirectory = true)
        {
            var name = _nameGen.GetName().Split(' ');
            var constituent = new IndividualConstituent(
                name[0],name[1], _addressGen.GetAddress(),_emailGen.GetEmailAddress(emailType),_phoneGen.GetPhoneNumber(phoneNumberType),LoremIpsumGenerator.GetWords(1),
                LoremIpsumGenerator.GetWords(1),LoremIpsumGenerator.GetWords(1),"Home",GetPrefix(),GetSuffix(),GetCredentials(), parentConstId,"",LoremIpsumGenerator.GetWords(2),LoremIpsumGenerator.GetWords(1),
                "", GetCountry(), confidential, includeInPrintDirectory,LoremIpsumGenerator.GetWords(3), includeInMobileDirectory);
            return constituent;
        }

        public CompanyConstituent GetCompanyConstituent()
        {
            var name = _nameGen.GetCompany();
            var constituent = new CompanyConstituent(name, _addressGen.GetAddress());
            return constituent;
        }

        public CommitteeConstituent GetCommitteeConstituent()
        {
            var name = _nameGen.GetCommmittee();
            var constituent = new CommitteeConstituent(name, _addressGen.GetAddress());
            return constituent;
        }

        #endregion

        #region Private Methods

        private bool RandomBool()
        {
            return Rng.Next(0, 1) == 0;
        }

        private string GetCountry()
        {
            string[] _countries = {"United States"};
            return _countries[0];
        }
        private string GetCredentials()//finish
        {
            //string[] _credentials = {"CAP","CSP", "MD", "Trainer", "PHD" };
            string query = "SELECT DESCR from APP_CODE WHERE TYPE LIKE 'CREDENTIALS'";
            var ds = SqlHelper.GetData(query);
            
            return ds.Tables[0].Rows[Rng.Next(0, ds.Tables[0].Rows.Count)]["DESCR"].ToString();
        }
        private string GetPrefix()
        {
            string[] _prefixes = { "Dr", "Miss", "Mr", "Mrs", "Ms" };
            return _prefixes[Rng.Next(0, _prefixes.Length - 1)];
            //string _prefixes = PersonifyHelper.GetActiveAppCodesForDropdown("name_prefix", "CUS").FirstOrDefault().Value;
            //return _prefixes;
        }
        private string GetSuffix()
        {
            string[] _suffixes = { "III", "Jr", "Sr" };
            return _suffixes[Rng.Next(0, _suffixes.Length - 1)];

        }
        #endregion

    }
}
