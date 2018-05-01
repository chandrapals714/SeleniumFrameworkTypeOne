using System;
using System.IO;
using System.Text;
using OpenQA.Selenium;

namespace SeleniumFramework.Generators
{
    public class NameGenerator : GeneratorBase
    {

#region Private Properties
        private readonly string[] _firstNameSyllables = {"ja","be","mor","sid","mon","da","fre","al","her","sten"};
        private readonly string[] _lastNameSyllables = {"ste", "ve", "ens", "burg", "ess", "no", "green", "am", "ed", "oli",};
        private readonly string[] _lastNameSyllablesForCommitteee = { "cste", "cve", "cens", "cburg", "tess", "eno", "sgreen", "iam", "yed", "zoli"};

        #endregion

        #region Public Methods

        public string GetName()
        {
            return LastGenerated = $"{GetFirstName()} {GetLastName()}";
        }

        //kw
        public string GetCompany()
        {
            return LastGenerated = $"{GetCompanyName()}";
        }

        public string GetCommmittee()
        {
            return LastGenerated = $"{GetCommmitteeName()}";
        }

        #endregion

        #region Private Methods
        private string GetFirstName()
        {
            return MakeFirstLetterUpper(GetSyllables(_firstNameSyllables));
        }

        private string GetLastName()
        {
            return MakeFirstLetterUpper(GetSyllables(_lastNameSyllables));
        }
        //kw
        private string GetCompanyName()
        {
            return MakeFirstLetterUpper(GetSyllables(_lastNameSyllables));
        }

        private string GetCommmitteeName()
        {
            return MakeFirstLetterUpper(GetSyllables(_lastNameSyllablesForCommitteee));
        }


        #endregion

    }
}