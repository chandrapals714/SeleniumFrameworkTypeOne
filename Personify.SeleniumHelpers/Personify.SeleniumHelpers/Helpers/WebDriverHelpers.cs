using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Personify.Helpers
{
    public static class WebDriverHelpers
    {
        public static void ValidatePageLoad(this IWebDriver driver)
        {
            if (!driver.Title.Contains("Personify"))
            {
                Assert.Fail($"Page failed to load succesfully. {driver.Url}");
            }
        }

        public static void SetImplicitWait(TimeSpan ts)
        {
            try
            {
                WebDriverFactory.LastDriver.Manage().Timeouts().ImplicitWait = ts;
            }
            catch (Exception e)
            {
                //TimeSpan wait = WebDriverFactory.LastDriver.Manage().Timeouts().ImplicitWait;
                //wait = ts;
            }
            
        }

        public static bool SwitchToPopUp(this IWebDriver driver, string windowTitle)
        {
            var curWindow = driver.WindowHandles;
            return driver.SwitchTo().Window(curWindow[0]).Title.Equals(windowTitle);
        }

        /// <summary>
        /// Accepts or Cancels a Java Alert Pop up.
        /// </summary>
        /// <param name="dismiss">dismiss or accept alert.</param>
        /// <returns>The alerts text.</returns>
        public static string HandleJavaAlert(bool dismiss = false)
        {
            WaitHelpers.ExplicitWait();
            try
            {
                var javaAlert = WebDriverFactory.LastDriver.SwitchTo().Alert();
                var alertText = javaAlert.Text;
                WaitHelpers.ExplicitWait();
                if (dismiss) { javaAlert.Dismiss(); }
                else { javaAlert.Accept(); }
                //Driver.SwitchTo().Window("");
                WaitHelpers.ExplicitWait();
                return alertText;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
