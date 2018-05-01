using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;

namespace Personify.Helpers
{
    public enum BrowserType
    {
        IExplore,
        Chrome,
        FireFox,
        Edge,
        Mobile,
        Grid
    };
    
    public class WebDriverFactory
    {
        public static IWebDriver LastDriver;
        public static int CurrentBrowserPID;
        private static BrowserType currentBrowserType;
        
        public static IWebDriver GetDriver(BrowserType browserType,string perfectoUserName ="",string perfectoPassWord ="",string deviceId = "",string gridUrl ="", DesiredCapabilities desCap = null)
        {
            currentBrowserType = browserType;

            switch (browserType)
            {
                case BrowserType.IExplore:
                    var ieOpt = new InternetExplorerOptions
                    {
                        BrowserAttachTimeout = TimeSpan.FromSeconds(30),
                        EnableNativeEvents = true
                    };
                    ieOpt.PageLoadStrategy = PageLoadStrategy.Normal;
                    return LastDriver = new InternetExplorerDriver(@"C:\\AutomationFiles\\", ieOpt);
                    
                case BrowserType.Chrome:
                    var chromeOp = new ChromeOptions();

                    chromeOp.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOp.AddUserProfilePreference("profile.password_manager_enabled", false);
                    chromeOp.AddArguments("disable-infobars");
                    chromeOp.PageLoadStrategy = PageLoadStrategy.Normal;
                    //return LastDriver = new ChromeDriver(chromeOp);

                    return LastDriver = new ChromeDriver(@"C:\\AutomationFiles\\", chromeOp);

                case BrowserType.FireFox:
                    
                    var ffService = FirefoxDriverService.CreateDefaultService("C:\\AutomationFiles\\");
                    
                    var ffOptions = new FirefoxOptions();
                    ffOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    return
                        LastDriver =
                            new FirefoxDriver(ffService, ffOptions,TimeSpan.FromSeconds(60));

                case BrowserType.Edge:
                    return LastDriver = new EdgeDriver("C:\\AutomationFiles\\");

                case BrowserType.Mobile:
                    if (string.IsNullOrEmpty(perfectoUserName) || string.IsNullOrEmpty(perfectoPassWord) || string.IsNullOrEmpty(deviceId))
                    {
                        throw new Exception("Perfecto username, password or device list is null or empty.");
                    }

                    return LastDriver = SetUpPerfecto(perfectoUserName,perfectoPassWord,desCap);

                case BrowserType.Grid:
                    return LastDriver = new RemoteWebDriver(new Uri(gridUrl), desCap);

                default:
                    throw new Exception("WebDriverFactory.GetDriver() should not reach this code path.");
            }
            
        }

        public static void ForceRefresh()
        {
            LastDriver.ExecuteJavaScript("history.go(0)");
        }

        private static RemoteWebDriverExtended SetUpPerfecto(string userName,string passWord, DesiredCapabilities desCap = null)
        {
            var host = "mobilecloud.perfectomobile.com";

            if (desCap == null)
            {
                desCap = new DesiredCapabilities("mobileOS", string.Empty, new Platform(PlatformType.Any));
                desCap.SetCapability("platformName", "iOS");
                desCap.SetCapability("manufacturer", "Apple");
                desCap.SetCapability("resolution", "2048 x 1536");
                desCap.SetPerfectoLabExecutionId(host);
            }

            desCap.SetCapability("user", userName);
            desCap.SetCapability("password", passWord);

            var url = new Uri($"https://{host}/nexperience/perfectomobile/wd/hub");
            
            var driver = new RemoteWebDriverExtended(url, desCap, TimeSpan.FromMinutes(2));
            
            return driver;
        }

    }

   
}
