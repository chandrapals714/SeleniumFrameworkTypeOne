using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Automation;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using Button = TestStack.White.UIItems.Button;
using TextBox = TestStack.White.UIItems.TextBox;

namespace Personify.Helpers
{
    public static class FileUploader 
    {
        public static void Upload(string filePath,string browserType)
        {
            throw new NotImplementedException("File upload being re-architected");//todo: address after control is re-architectec -dg 06/02/2017

            if (browserType == "Grid" || browserType == "Mobile")
            {
                RemoteWebDriverUpload(filePath);
            }

            //get and attach to browser process
            var browser = Application.Attach(Process.GetProcessById(WebDriverFactory.CurrentBrowserPID));
            
            //get file upload window
            var fileUploadWindow = browser.GetWindows().Where(w => w.Name == "File Upload" || w.Name == "Open").ToArray();
            var x = browser.GetWindows();
            //enter filepath
            var searchCriteria = SearchCriteria
                .ByControlType(ControlType.Edit)
                .AndAutomationId("1148");
            var fileNameCombo = (TextBox)fileUploadWindow[0].Get(searchCriteria);
            fileNameCombo.Text = filePath;
            WaitHelpers.ExplicitWait(250);
            //click open button
            var openSearch = SearchCriteria
                .ByControlType(ControlType.Button)
                .AndAutomationId("1");
            var openButton = (Button)fileUploadWindow[0].Get(openSearch);
            openButton.Click();
            WaitHelpers.ExplicitWait(1000);
            var wait = new WebDriverWait(WebDriverFactory.LastDriver, TimeSpan.FromSeconds(5));
            wait.PollingInterval = TimeSpan.FromSeconds(1);
            wait.Until(
                ExpectedConditions.TextToBePresentInElement(WebDriverFactory.LastDriver.FindElement(By.CssSelector(".k-upload-status")), "Done"));
        }

        private static void RemoteWebDriverUpload(string filePath)
        {
            throw new NotImplementedException("File upload not yet implemented for RemoteWebDriver");
        }


    }
}