using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumFramework.Generators.GeneratorData;

namespace SeleniumFramework.Generators
{
    public enum CompanyType
    {
        Company,
        Association,
        Committee
    };
    //kw
    public class CompanyGenerator : GeneratorBase
    {
        #region Private Properties

        private Company _lastGenerated;
        private readonly AddressGenerator _addressGen;
        private readonly EmailGenerator _emailGen;
        private readonly NameGenerator _nameGen;
        private readonly PhoneGenerator _phoneGen;

        #endregion

        #region Constructor

        public CompanyGenerator()
        {
            _addressGen = new AddressGenerator();
            _emailGen = new EmailGenerator();
            _nameGen = new NameGenerator();
            _phoneGen = new PhoneGenerator();
        }

        #endregion

        #region Public Properties

        public new Company LastGenerated => _lastGenerated;

        #endregion

        #region Public Methods
        public Company GetCompany()
        {
            var name = _nameGen.GetCompany().Split(' ');
            return _lastGenerated = new Company(name[0], _emailGen.GetEmailAddress(EmailType.Random),
            _phoneGen.GetPhoneNumber(PhoneNumberType.Domestic), _addressGen.GetAddress());
        }
        public CompanyConstituent GetCompanyConstituent(string parentConstId = "", bool confidential = false,
            bool includeInPrintDirectory = true, bool includeInMobileDirectory = true)
        {
            var name = _nameGen.GetCompany().Split(' ');
            var company = new CompanyConstituent(name[0], _addressGen.GetAddress(), GetPrefix(), LoremIpsumGenerator.GetWords(1), "Work",
            parentConstId, "", confidential, includeInPrintDirectory, includeInMobileDirectory);
            return company;
        }

        #endregion

        #region Private Methods

        private bool RandomBool()
        {
            return Rng.Next(0, 1) == 0;
        }

        private string GetCountry()
        {
            string[] _countries = { "United States" };
            return _countries[0];
        }
        private string GetPrefix()
        {
            string[] _prefixes = { "Dr", "Miss", "Mr", "Mrs", "Ms" };
            return _prefixes[Rng.Next(0, _prefixes.Length - 1)];
        }

        #endregion

    }
}
