using System.Web;
using OpenQA.Selenium;

namespace Personify.Helpers
{
    public static class Executor
    {
        public static object ExecuteJavascript(string script, IWebElement ele)
        {
             return ((IJavaScriptExecutor)WebDriverFactory.LastDriver).ExecuteScript(script, ele);
        }

        public static object ExecuteJavascript(string script)
        {
            return ((IJavaScriptExecutor)WebDriverFactory.LastDriver).ExecuteScript(script);
        }

        public static object ExecuteJavascript(string script, object obj)
        {
            return ((IJavaScriptExecutor)WebDriverFactory.LastDriver).ExecuteScript(script,obj);
        }
    }
}