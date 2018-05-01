using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Personify.Helpers
{
    public static class AssertHelpers
    {
        public static bool Matches(this string st1, string st2)
        {
            return st1 == st2;
        }

        public static bool AssertWebElementText(this IWebElement element, string expected, out string text)
        {
            element.DrawHighlight(WebDriverFactory.LastDriver);
            text = element.Text;
            return text.Matches(expected);
        }
    }
}
