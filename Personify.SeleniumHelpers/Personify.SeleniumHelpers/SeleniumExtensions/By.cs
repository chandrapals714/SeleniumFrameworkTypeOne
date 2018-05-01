using OpenQA.Selenium;

namespace SeleniumExtensions
{
    public class CustomBy : By
    {
        public static jQueryBy jQuery(string selector)
        {
            return new jQueryBy(selector);
        }
        
        public class jQueryBy
        {
            public string Selector
            {
                get;
                set;
            }

            public jQueryBy(string selector)
            {
                this.Selector = selector;
            }
            
        }
    }
}
