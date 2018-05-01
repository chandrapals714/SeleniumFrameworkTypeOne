using System;
using System.Drawing.Text;

namespace SeleniumFramework.Generators
{
    public enum EmailType
    {
        Random,
        Working
    };

    public class EmailGenerator : GeneratorBase
    {
        #region Private Properties 

        private readonly string[] _domains = {"google", "yahoo", "aol", "lycos"};
        private readonly string[] _words = {"mongo", "surfer", "press", "star", "fox", "super", "shark", "best","laser"};
        private readonly string[] _suffixes = {"net", "com", "org"};
        private const string WorkingEmail = "working@PopUpStuff.com";
        
        #endregion
        
        #region Public Methods

        public string GetEmailAddress(EmailType emailType)
        {
            return LastGenerated = emailType == EmailType.Working ? WorkingEmail : $"{GetUserName()}{GetRandNumString(3)}@{GetDomainName()}.{GetDomainSuffix()}";
        }

        #endregion

        #region Private Methods

        private string GetUserName()
        {
            return GetSyllables(_words);
        }

        private string GetDomainName()
        {
            return GetSyllables(_domains, true);
        }

        private string GetDomainSuffix()
        {
            return GetSyllables(_suffixes, true);
        }

        #endregion
    }
}