using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using Personify.Helpers;

namespace SeleniumExtensions
{
    public static class SeleniumExtensions
    {
        
        public static IWebElement FindElement(this IWebDriver driver, CustomBy.jQueryBy by)
        {   
            var element = (IWebElement)Executor.ExecuteJavascript("return $" + by.Selector + ".get(0)");// <IWebElement>("return $" + by.Selector + ".get(0)");

            if (element != null)
                return element;
            
                throw new NoSuchElementException("No element found with jQuery command: jQuery" + by.Selector);
        }
        
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, CustomBy.jQueryBy  by)
        {
            ReadOnlyCollection<IWebElement> collection = (ReadOnlyCollection<IWebElement>)Executor.ExecuteJavascript("return jQuery" + by.Selector + ".get()"); ///driver.ExecuteJavaScript<ReadOnlyCollection<IWebElement>>("return jQuery" + by.Selector + ".get()");
           
            if (collection == null)
                collection = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            
            return collection;
        }
    }
}
