using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Generators
{
    public abstract class GeneratorBase
    {

#region Private Properties
        private string _lastGenerated;
        internal Random Rng;
#endregion

#region Constructor
        protected GeneratorBase()
        {
            Rng = new Random(DateTime.Now.Ticks.GetHashCode());
        }
#endregion

#region Public Properties

        public string LastGenerated
        {
            get { return _lastGenerated; }
            internal set { _lastGenerated = value; }
        }

        #endregion

#region Private Methods
        internal string GetSyllables(string[] syllableArray,bool singleSyllable = false)
        {
            var sb = new StringBuilder();
            int syllableCount = Rng.Next(2, 4);
            if (singleSyllable)
            {
                syllableCount = 1;
            }

            for (int i = 0; i < syllableCount; i++)
            {
                sb.Append(syllableArray[Rng.Next(0, syllableArray.Length -1)]);
            }
            return sb.ToString();
        }

        internal string MakeFirstLetterUpper(string lCaseString)
        {
            if (lCaseString == null)
                return null;

            return lCaseString.Length > 1 ? $"{char.ToUpper(lCaseString[0])}{lCaseString.Substring(1)}" : lCaseString.ToUpper();
        }

        internal string GetRandNumString(int count)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append(Rng.Next(0, 9));
            }

            return sb.ToString();
        }

        #endregion

    }
}
