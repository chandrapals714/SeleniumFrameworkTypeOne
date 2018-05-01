using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace Personify.Helpers
{
    internal enum WaitType
    {
        Exists,
        Visible,
        Clickable
    }

    public static class WaitHelpers
    {
        public static int RecurseCount;
        public static TimeSpan DefaultWait { get; set; }
        #region Public Methods
        
        public static IWebElement WaitForClickable(By by)
        {
            var x = WebDriverFactory.LastDriver.FindElement(by);
            return WaitForClickable(WebDriverFactory.LastDriver.FindElement(by));
        }

        public static IWebElement WaitForClickable(IWebElement element)
        {
            WebDriverHelpers.SetImplicitWait(TimeSpan.Zero); //ToDo: remove once all elements use new func

            var wait = new WebDriverWait(WebDriverFactory.LastDriver, DefaultWait);
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            IWebElement ele = wait.Until<IWebElement>(
                ExpectedConditions.ElementToBeClickable(element));
            ExplicitWait(100);
            WebDriverHelpers.SetImplicitWait(DefaultWait);//ToDo: remove once all elements use new func

            return ele;
        }
        
        public static IWebElement WaitForElementVisible(By locator)
        {
            WebDriverHelpers.SetImplicitWait(TimeSpan.Zero);//ToDo: remove once all elements use new func

            WebDriverWait waitVisible = new WebDriverWait(WebDriverFactory.LastDriver, DefaultWait);
            waitVisible.PollingInterval = TimeSpan.FromMilliseconds(500);
            waitVisible.IgnoreExceptionTypes(typeof(NoSuchElementException));
            waitVisible.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            
            IWebElement element = waitVisible.Until<IWebElement>(ExpectedConditions.ElementIsVisible(locator));

            WebDriverHelpers.SetImplicitWait(DefaultWait);//ToDo: remove once all elements use new func

            return element;
        }
        
        public static IWebElement WaitForElementExist(By locator)
        {
            WebDriverHelpers.SetImplicitWait(TimeSpan.Zero);//ToDo: remove once all elements use new func

            WebDriverWait waitExist = new WebDriverWait(WebDriverFactory.LastDriver, DefaultWait);
            waitExist.PollingInterval = TimeSpan.FromMilliseconds(500);
            waitExist.IgnoreExceptionTypes(typeof(NoSuchElementException));
            waitExist.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            IWebElement element = waitExist.Until<IWebElement>(ExpectedConditions.ElementExists(locator));

            WebDriverHelpers.SetImplicitWait(DefaultWait);//ToDo: remove once all elements use new func

            return element;
        }
        
        public static void WaitForLoadingOverlays(IWebDriver driver,int timeout = 200)
        {
            //TODO: Hack for network lag, must be removed. - <Pramod and 06/03/2017>
            ExplicitWait();

            WebDriverHelpers.SetImplicitWait(TimeSpan.FromSeconds(0));
            var wait = new WebDriverWait(driver, DefaultWait);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".loading-spinner")));
            ExplicitWait(100);
            RecurseCount++;
            try
            {
                if (driver.FindElements(By.CssSelector(".loading-spinner")).Count(spnr => spnr.Displayed == true) > 1 && RecurseCount < timeout)
                {
                    WaitForLoadingOverlays(driver);
                }
            }
            catch (Exception)
            {
                
            }
            
            RecurseCount = 0;
            WebDriverHelpers.SetImplicitWait(DefaultWait);
            WaitForAjaxLoad();//ToDo: remove - hack for net lag.
        }

        public static void WaitForAjaxLoad()
        {
            var wait = new WebDriverWait(WebDriverFactory.LastDriver, DefaultWait);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            wait.Until(p => Executor.ExecuteJavascript("if (window.jQuery && window.app) {return jQuery.active == 0 && document.readyState == 'complete' && !(window.app.loading && app.loading.length != 0)} else {return false}"));
            ExplicitWait(100);
            wait.Until(p => Executor.ExecuteJavascript("if (window.jQuery && window.app) {return jQuery.active == 0 && document.readyState == 'complete' && !(window.app.loading && app.loading.length != 0)} else {return false}"));
        }

        public static void WaitForPresent(this IWebElement element , IWebDriver driver) //TODO: Could this be rolled into WaitForElementVisible? - MO
        {
            int timeoutCounter = 0;
            
            WebDriverHelpers.SetImplicitWait(TimeSpan.Zero);
            while (timeoutCounter < 100)
            {
                try
                {                    
                    var x = element.Displayed;
                    break;
                }
                catch (Exception)
                {
                    ExplicitWait(100);
                    timeoutCounter++;
                }
            }
            WebDriverHelpers.SetImplicitWait(DefaultWait);
       }

        //Below method is explicit function/method of wait, it is required to handle slowness of application in some cases.
        public static void ExplicitWait(int timeout = 500)
        {
              Thread.Sleep(timeout);
        }

       

        #endregion
    }
}