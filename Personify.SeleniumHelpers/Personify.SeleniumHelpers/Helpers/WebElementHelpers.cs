using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using SeleniumExtensions;

namespace Personify.Helpers
{
    public static class WebElementHelpers
    {

        /// <summary>
        /// Sets value of Kendo Masked Textbox with up down toggles.
        /// </summary>
        /// <param name="ele">IWebelement Kendo masked text box.</param>
        /// <param name="value">value to set</param>
        public static void SetValueMaskedNumberEdit(this IWebElement ele, string value)
        {
            string setAmountJs =
                $"$(\"input[name='{ele.GetName()}']\").kendoMaskedTextBox({{value: \"{value}\"}});";

            Executor.ExecuteJavascript(setAmountJs);
            Executor.ExecuteJavascript(@"$(arguments[0]).show();", ele);
            ele.SendKeys(Keys.Tab);
        }

        /// <summary>
        /// Sets value of Kendo Masked Textbox with up down toggles.
        /// </summary>
        /// <param name="by">By locator of Kendo masked text box.</param>
        /// <param name="value">value to set.</param>
        public static void SetValueMaskedNumberEdit(this By by, string value)
        {
            WaitHelpers.WaitForElementExist(by).SetValueMaskedNumberEdit(value);
        }

        /// <summary>
        /// Gets the value of the masked currency edit
        /// </summary>
        /// <param name="ele">IWebelement masked currency edit</param>
        /// <returns>value of the element</returns>
        public static string GetValueMaskedNumberEdit(this IWebElement ele)
        { 
            try
            {
                string getAmountJs =
                $"return $(\"input[name='{ele.GetName()}']\").data(\"kendoMaskedTextBox\").value();";
                return (string)Executor.ExecuteJavascript(getAmountJs);
            }
            catch (Exception)
            {
                return ele.GetAttribute("value");
            }
        }

        /// <summary>
        /// Gets the value of the masked currency edit
        /// </summary>
        /// <param name="by">By locator for masked currency edit</param>
        /// <returns>value of the element</returns>
        public static string GetValueMaskedNumberEdit(this By by)
        {
            return WaitHelpers.WaitForElementExist(by).GetValueMaskedNumberEdit();
        }

        /// <summary>
        /// Change to tab/subtab specified
        /// </summary>
        /// <param name="tab">name of top level tab</param>
        /// <param name="dropDownItem">name of submenu item</param>
        public static void ChangeTab(string tab, string dropDownItem = null)
        {
            //locate the tab link and click it
            WaitHelpers.WaitForElementVisible(By.CssSelector(".nav.top-tab-bar"));
            WaitHelpers.WaitForClickable(By.CssSelector(".nav.top-tab-bar"));
            IWebElement tabStrip = WebDriverFactory.LastDriver.FindElement(By.CssSelector(".nav.top-tab-bar"));
            var xpathLocator = $"//a[descendant::div[contains(@id,\"DetailView_{tab}\")]] | //a[descendant::div[contains(@id,'DetailView_') and contains(.,\"{tab}\")]]";
            WaitHelpers.WaitForElementVisible(By.XPath(xpathLocator));
            IWebElement tabToClick = WaitHelpers.WaitForClickable(tabStrip.FindElement(By.XPath(xpathLocator)));
            WaitHelpers.ExplicitWait();
            tabToClick.Click();
            
            //if drop-down, click the drop-down item
            if (!string.IsNullOrEmpty(dropDownItem))
            {
                //IWebElement element = WebDriverFactory.LastDriver.ExecuteJavaScript<IWebElement>("return $" + "(\"a:has(div:contains(\'Demographics\'))\").filter(\":visible\")" + ".get(0)");
                //element.DrawHighlight();

                //var els = tabToClick.FindElements(By.XPath($"//a[descendant::div[contains(.,\"{dropDownItem}\")]]"));
                //foreach (var e in els)
                //{
                //    if (e.Displayed) { e.Click(); }
                //}
                //IWebElement dropDownToClick = WaitHelpers.WaitForClickable(tabToClick.FindElement(By.XPath($"//a[descendant::div[contains(.,\"{dropDownItem}\")]]")));
                //dropDownToClick.Click();

                WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"a:has(div:contains(\'{dropDownItem}\'))\").filter(\":visible\")")).Click();
            }
            WaitHelpers.WaitForAjaxLoad();
        }
        
        /// <summary>
        /// Set text in a standard input element.
        /// </summary>
        /// <param name="ele">IWebElement input</param>
        /// <param name="text">Text to set.</param>
        public static void SetTextStandardTextBox(this IWebElement ele, string text)
        {
            ele.Click();
            ele.Clear();
            ele.SendKeys(text);
        }
        
        /// <summary>
        /// Sets text in a standard input element.
        /// </summary>
        /// <param name="by">By locator of the textbox element.</param>
        /// <param name="text">Text to enter into the textbox.</param>
        public static void SetTextStandardTextBox(this By by, string text)
        {
            WaitHelpers.WaitForClickable(by).SetTextStandardTextBox(text);
        }
        
        /// <summary>
        /// Removes item from the multi-select.
        /// </summary>
        /// <param name="ele">Kendo multi-select.</param>
        /// <param name="text">text of the item to remove.</param>
        public static void RemoveTextMultiSelect(this IWebElement ele, string text)
        {
            //generate the locator of the list element.
            var listLocator =
                By.XPath(
                    $"//ul[contains(@id, '{ele.FindElement(By.XPath(".//parent::*")).FindElement(By.TagName("input")).GetAttribute("aria-owns").Split(' ')[1]}')]");
            //check to see if text is available in list.
            if (!string.IsNullOrEmpty(text) || ele.FindElement(listLocator).FindElements(By.TagName("li"))
                .Any(p => p.GetAttribute("innerHTML").ToLower().Equals(text.ToLower())))
            {
                //remove text
                ele.FindElements(By.TagName("li"))
                    .Where(p => p.FindElement(By.TagName("span")).GetAttribute("innerHTML").ToLower().Equals(text.ToLower())).ToArray()[0].FindElement(By.CssSelector(".k-icon.k-delete")).Click();
                WaitHelpers.ExplicitWait(100);
            }
            else
            {
                var listItems = ele.FindElements(By.TagName("li"));
                foreach (var item in listItems)
                {
                    item.FindElement(By.CssSelector(".k-icon.k-delete")).Click();
                    WaitHelpers.ExplicitWait(100);
                }
            }
        }

        /// <summary>
        /// Removes each of the items in the array from the multi-select.
        /// </summary>
        /// <param name="ele">Kendo multiselect.</param>
        /// <param name="text">text of the items to remove.</param>
        public static void RemoveTextMultiSelect(this IWebElement ele, string[] text)
        {
            foreach (var t in text)
            {
                ele.RemoveTextMultiSelect(t);
            }
        }

        /// <summary>
        /// Removes each of the items in the array from the multi-select.
        /// </summary>
        /// <param name="ele">By locator for Kendo multiselect.</param>
        /// <param name="text">text of the items to remove.</param>
        public static void RemoveTextMultiSelect(this By ele, string[] text)
        {
            WebDriverFactory.LastDriver.FindElement(ele).RemoveTextMultiSelect(text);
        }

        /// <summary>
        /// Removes each of the items in the array from the multi-select.
        /// </summary>
        /// <param name="ele">By Locator for Kendo multiselect.</param>
        /// <param name="text">text of the items to remove.</param>
        public static void RemoveTextMultiSelect(this By ele, string text)
        {
            WebDriverFactory.LastDriver.FindElement(ele).RemoveTextMultiSelect(text);
        }

        /// <summary>
        /// Selects the item in the multi-select by text.
        /// </summary>
        /// <param name="ele">Kendo multi-select element.</param>
        /// <param name="text">Text of item to select.</param>
        public static void SetTextMultiSelect(this IWebElement ele, string text)
        {

            //generate the locator of the list element.
            var listLocator =
                By.XPath(
                    $"//ul[contains(@id, '{ele.FindElement(By.XPath(".//parent::*")).FindElement(By.TagName("input")).GetAttribute("aria-owns").Split(' ')[1]}')]");

            //check to see if text is available in list.
            if (!string.IsNullOrEmpty(text) || WebDriverFactory.LastDriver.FindElement(listLocator).FindElements(By.TagName("li"))
                .Any(p => p.GetAttribute("innerHTML").ToLower().Equals(text.ToLower())))
            {
                //click to display listbox
                ele.FindElement(By.XPath(".//parent::*")).Click();

                //wait for list to be visible
                var wait = new WebDriverWait(WebDriverFactory.LastDriver, WaitHelpers.DefaultWait);
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                wait.Until(ExpectedConditions.ElementIsVisible(listLocator));
                WaitHelpers.ExplicitWait();
                //select text
                WebDriverFactory.LastDriver.FindElement(listLocator).FindElements(By.TagName("li"))
                    .Where(p => p.GetAttribute("innerHTML").ToLower().Equals(text.ToLower())).ToArray()[0].Click();

                //wait until listbox is invisible
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(listLocator));
            }
            else
            {
                ele.FindElement(By.XPath(".//parent::*")).FindElement(By.TagName("input")).ForceTextToListInput(text);
            }

        }

        /// <summary>
        /// Selects all the items in the multi-select by text.
        /// </summary>
        /// <param name="ele">Kendo multi-select element.</param>
        /// <param name="text">Text of items to select.</param>
        public static void SetTextMultiSelect(this IWebElement ele, string[] text)
        {
            foreach (var t in text)
            {
                ele.SetTextMultiSelect(t);
            }
        }

        /// <summary>
        /// Selects all the items in the multi-select by text.
        /// </summary>
        /// <param name="ele">By locator of Kendo multi-select element.</param>
        /// <param name="text">Text of items to select.</param>
        public static void SetTextMultiSelect(this By ele, string[] text)
        {
            WebDriverFactory.LastDriver.FindElement(ele).SetTextMultiSelect(text);
        }

        /// <summary>
        /// Selects the item in the multi-select by text.
        /// </summary>
        /// <param name="ele">By locator of Kendo multi-select element.</param>
        /// <param name="text">Text of item to select.</param>
        public static void SetTextMultiSelect(this By ele, string text)
        {
            WebDriverFactory.LastDriver.FindElement(ele).SetTextMultiSelect(text);

        }

        /// <summary>
        /// Selects provided text from Kendo ComboDropdown
        /// </summary>
        /// <param name="ele">IWebElement Kendo ComboDropdown</param>
        /// <param name="text">Text to select</param>
        public static void SetTextComboDropdown(this IWebElement ele, string text)
        {
            ele.ForceTextToListInput(text);

            //if (!string.IsNullOrEmpty(text))
            //{
            //    ele.ForceTextToListInput(text);
            //    return;
            //}

            //ele.Clear();

            ////get element name
            //var eleName = ele.GetName();

            ////get kendoComboBox's ID
            ////var inputId =     
            ////    ele.FindElement(By.XPath($"(//*[@name='{eleName}']//parent::*//parent::*//input)[2]")).GetId();
            //var inputIdjQ = WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName.Split('_')[0]}']\")")).GetId();

            ////generate the locator element of listbox linked to ele.
            ////var listLocator = By.XPath($"//ul[contains(@id, '{ele.GetAttribute("aria-owns")}')]");
            //var listLoc = CustomBy.jQuery($"(\"#{ele.GetAttribute("aria-owns")}\")");
            //var listLi = WebDriverFactory.LastDriver.FindElement(listLoc).FindElements(By.TagName("li"));

            //if (!string.IsNullOrEmpty(text) || listLi.Any(p => p.GetAttribute("innerHTML").ToLower().Equals(text.ToLower())))//check to see if text is available in list.
            //{
            //    try
            //    {
            //        //listLocator = By.XPath($"//ul[contains(@id, '{ele.GetAttribute("aria-owns")}')]");

            //        //for debugging
            //        //var xxxx = WebDriverFactory.LastDriver.FindElement(listLoc).FindElements(By.TagName("li"));
            //        //var list = new List<string> { };

            //        //foreach (var x in xxxx)
            //        //{
            //        //    list.Add(x.GetAttribute("innerHTML"));
            //        //}
            //        //

            //        var script =
            //        $"$('#{inputIdjQ}').getKendoComboBox().select(" +
            //        $"{WebDriverFactory.LastDriver.FindElement(listLoc).FindElements(By.TagName("li")).Select((item, index) => new { Item = item, Index = index }).First(i => i.Item.GetAttribute("innerHTML").ToLower().Equals(text.ToLower())).Index});" +
            //        $"$('#{inputIdjQ}').getKendoComboBox().trigger('change');";

            //        Executor.ExecuteJavascript(script);

            //        //var xxxx = WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName}']\")"))
            //        //    .GetCurrentValueofComboDropdown();


            //        if (!WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName}']\")")).GetCurrentValueofComboDropdown().ToLower().Equals(text.ToLower()))
            //        {
            //            WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName}']\")")).ForceTextToListInput(text);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName}']\")")).DrawHighlight();
            //        WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName}']\")")).ForceTextToListInput(text);
            //    }
            //}
            //else//if text is not in list, manually enter it
            //{
            //    WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName}']\")")).DrawHighlight();
            //    WebDriverFactory.LastDriver.FindElement(CustomBy.jQuery($"(\"[name='{eleName}']\")")).ForceTextToListInput(text);
            //}

        }

        /// <summary>
        /// Selects provided text from Kendo ComboDropdown
        /// </summary>
        /// <param name="by">By locator for Kendo ComboDropdown</param>
        /// <param name="text">Text to select</param>
        public static void SetTextComboDropdown(this By by, string text)
        {
            SetTextComboDropdown(WebDriverFactory.LastDriver.FindElement(by), text);
        }
        
        /// <summary>
        /// Force text into Kendo DropDownCombo input.
        /// </summary>
        /// <param name="ele">IWebElement input</param>
        /// <param name="text">text to input</param>
        private static void ForceTextToListInput(this IWebElement ele, string text)//ToDo: remove sleeps once Netlag is fixed - dg 03/14/17
        {
            WaitHelpers.ExplicitWait(200);
            WaitHelpers.WaitForAjaxLoad();
            WaitHelpers.ExplicitWait(100);
            ele.Click();
            WaitHelpers.ExplicitWait(100);
            ele.Clear();
            WaitHelpers.ExplicitWait(100);
            ele.SendKeys(text);
            WaitHelpers.ExplicitWait(100);
            ele.SendKeys(Keys.Tab);
            WaitHelpers.ExplicitWait(100);
        }

        /// <summary>
        /// Retrieves all values from a Kendo ComboDropDown
        /// </summary>
        /// <param name="by">By locator for Kendo ComboDropDown</param>
        /// <returns>Dictionary with values from the Kendo ComboDropDown element.</returns>
        public static Dictionary<string, string> GetAllValueFromComboDropdown(this By by)
        {
            return WaitHelpers.WaitForClickable(by).GetAllValueFromComboDropdown();
        }

        /// <summary>
        /// Gets current value of Kendo ComboDropdown
        /// </summary>
        /// <param name="ele">IWebElement Kendo ComboDropDown</param>
        /// <returns>Current selected value of the combo</returns>
        public static string GetCurrentValueofComboDropdown(this IWebElement ele)
        {
            CheckDom();
            ele.DrawHighlight();
            
            var x = WebDriverFactory.LastDriver
                .FindElement(CustomBy.jQuery(
                    $"(\"#{ele.GetAttribute("aria-owns").Replace("_listbox", "_option_selected")}\")"));

            //By.Id($"{ele.GetAttribute("aria-owns").Replace("_listbox", "_option_selected")}");
            return WebDriverFactory.LastDriver
                .FindElement(CustomBy.jQuery($"(\"#{ele.GetAttribute("aria-owns").Replace("_listbox", "_option_selected")}\")")).GetClass().Contains("k-state-selected") ?  WebDriverFactory.LastDriver
                .FindElement(CustomBy.jQuery($"(\"#{ele.GetAttribute("aria-owns").Replace("_listbox", "_option_selected")}\")")).GetAttribute("innerHTML") : string.Empty;
        }

        private static void CheckDom()
        {
            var wait = new WebDriverWait(WebDriverFactory.LastDriver,TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(".//*")));
        }
        /// <summary>
        /// Gets current value of Kendo ComboDropdown
        /// </summary>
        /// <param name="by">By locator Kendo ComboDropDown</param>
        /// <returns>Current selected value of the combo</returns>
        public static string GetCurrentValueofComboDropdown(this By by)
        {
            return WaitHelpers.WaitForClickable(by).GetCurrentValueofComboDropdown();
        }

        /// <summary>
        /// Retrieves all values from a Kendo ComboDropDown
        /// </summary>
        /// <param name="ele">IWebElement Kendo ComboDropDown</param>
        /// <returns>Dictionary with values from the Kendo ComboDropDown element.</returns>
        public static Dictionary<string,string> GetAllValueFromComboDropdown(this IWebElement ele)
        {
            WaitHelpers.ExplicitWait(200);
            ele.DrawHighlight();
            WaitHelpers.ExplicitWait(200);
            return WebDriverFactory.LastDriver.FindElements(
                By.XPath($"//ul[contains(@id,'{ele.GetAttribute("aria-owns")}')]/li")).Select(p => p.GetAttribute("innerHTML").ToUpper().Trim()).Distinct().ToDictionary(p => p);
        }

        /// <summary>
        /// Takes a screenshot of the IWebElement
        /// </summary>
        /// <param name="element">IWebElement to take Screenshot of</param>
        /// <returns>Bitmap</returns>
        public static Bitmap TakeScreenShot(this IWebElement element)
        {
            var fullScreenShot = ((ITakesScreenshot) WebDriverFactory.LastDriver).GetScreenshot().AsByteArray;
            var ssAsBmp = new Bitmap(new MemoryStream(fullScreenShot));
            var croppingRect = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width,
                element.Size.Height);
            var retBmp = ssAsBmp.Clone(croppingRect, ssAsBmp.PixelFormat);
            
            //cleanup
            ssAsBmp.Dispose();

            return retBmp;
        }

        /// <summary>
        /// Scrolls IWebElement into view.
        /// </summary>
        /// <param name="ele">IWebElement to scroll into view.</param>
        public static void ScrollIntoView(this IWebElement ele)
        {
            Executor.ExecuteJavascript("arguments[0].scrollIntoView()", ele);
            WaitHelpers.ExplicitWait(250);
        }

        /// <summary>
        /// Gets Id attribute of the given element.
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>Id attribute of the element</returns>
        public static string GetId(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("id");
            }
            catch (Exception)
            {
                return  null;
            }
        }

        /// <summary>
        /// Gets Name attribute of the given element.
        /// </summary>
        /// <param name="element">IWebElement.</param>
        /// <returns>Name attribute of the element.</returns>
        public static string GetName(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("name");
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Gets the text value of the row index supplied.
        /// </summary>
        /// <param name="table">IWebElement table.</param>
        /// <param name="rowIndex">Index of the row to get text from.</param>
        /// <returns>Text from row index provided.</returns>
        public static string GetRowDataFromTable(this IWebElement table, int rowIndex)
        {
            return table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"))[rowIndex].Text.ToString();
        }

        /// <summary>
        /// Gets the td element element at the row and column provided
        /// </summary>
        /// <param name="table">IWebelement table</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <returns>td element at the column/row index.</returns>
        public static IWebElement GetCellFromTable(this IWebElement table, int rowIndex, int columnIndex)
        {
            return table.FindElements(By.XPath("./tbody/tr"))[rowIndex].FindElements(By.XPath("./td"))[columnIndex];
            
            //return null;
            //return table.FindElements(By.TagName("tr"))[rowIndex].FindElements(By.TagName("td"))[columnIndex];
        }

        /// <summary>
        /// Gets the div element at the row and column provided
        /// </summary>
        public static IWebElement GetCellFromOrderTable(this IWebElement table, int rowIndex, int columnIndex, int divIndex)
        {
            return table.FindElements(By.XPath("./tbody/tr"))[rowIndex].FindElements(By.XPath("./td"))[columnIndex].FindElements(By.XPath("./div/div"))[divIndex];
        }

        /// <summary>
        /// Gets the row index of the row containing the text supplied
        /// </summary>
        /// <param name="table">IWebElement table.</param>
        /// <param name="cellText">Text to search.</param>
        /// <returns>Row index that contains supplied text.</returns>
        public static int GetRowWithCellText(this IWebElement table, string cellText)
        {
            bool flag = false;
            int counter = 0;
            var rowCount = table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Count;
            for (int i = 0; i < rowCount; i++)
            {
                string str = table.GetRowDataFromTable(i).Replace(" ","");
                
                if (str.Contains(cellText.Replace(" ", "")))
                {
                    counter = i;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                var x = "";
            }
            return (flag) ? counter : -1;
        }

        /// <summary>
        /// Gets the count of the rows in the table.
        /// </summary>
        /// <param name="table">IWebElement table</param>
        /// <returns>Row count of the table.</returns>
        public static int Rowcount(this IWebElement table)
        {
            WebDriverHelpers.SetImplicitWait(TimeSpan.FromSeconds(3));
            var y = table.FindElements(By.CssSelector("tbody>tr")).Count;
            //var x = table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Count;
            WebDriverHelpers.SetImplicitWait(WaitHelpers.DefaultWait);

            return y;
        }
        
        /// <summary>
        /// Gets the Class attribute of the element
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>Class attribute of the element.</returns>
        public static string GetClass(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("Class");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Quick check to see if the element is present. 
        /// </summary>
        /// <param name="element">IWebElement to check</param>
        /// <param name="driver">IWebDriver</param>
        /// <returns>If element is present on the loaded page.</returns>
        public static bool IsElementPresent(this IWebElement element, IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                var test = element.Displayed;
                driver.Manage().Timeouts().ImplicitWait = (WaitHelpers.DefaultWait);
                return test;
            }
            catch (Exception)
            {
                driver.Manage().Timeouts().ImplicitWait = (WaitHelpers.DefaultWait);
                return false;
            }
        }
        
        /// <summary>
        /// Sets value of Kendo switch.
        /// </summary>
        /// <param name="ele">IWebElement Kendo switch</param>
        /// <param name="value">Walve to set switch to.</param>
        public static void SetSwitchValue(this IWebElement ele, bool value)
        {
            if (value != ele.GetSwitchValue())
            {
                try
                {
                    ele.FindElement(By.XPath($".//parent::*"))
                        .FindElement(By.ClassName($"km-switch-label-on")).Click();
                }
                catch (Exception e)
                {
                    ele.FindElement(By.XPath($".//parent::*"))
                        .FindElement(By.ClassName($"km-switch-label-off")).Click();
                }

            }

            //var id = ele.GetId();
            
            //if (!string.IsNullOrEmpty(id) && value != ele.GetSwitchValue())
            //{
            //    Executor.ExecuteJavascript($"$('#{id}').data('kendoMobileSwitch').toggle()");
            //    Executor.ExecuteJavascript($"$('#{id}').data('kendoMobileSwitch').trigger('change')");

            //}
            //else if (string.IsNullOrEmpty(id))
            //{
            //    throw new Exception("Element has no Id, does not appear to be Kendo Switch.");
            //}
        }

        /// <summary>
        /// Sets value of Kendo sswitch.
        /// </summary>
        /// <param name="by">By locator for Kendo switch</param>
        /// <param name="value">Value to set switch to.</param>
        public static void SetSwitchValue(this By by, bool value)
        {
            WaitHelpers.WaitForElementExist(by).SetSwitchValue(value);
        }

        /// <summary>
        /// Gets value of Kendo switch
        /// </summary>
        /// <param name="ele">IWebElement Kendo switch</param>
        /// <returns>Value of Kendo switch.</returns>
        public static bool GetSwitchValue(this IWebElement ele)
        {
            return ele.Selected;
        }

        /// <summary>
        /// Gets value of Kendo switch
        /// </summary>
        /// <param name="by">By locator for Kendo switch</param>
        /// <returns>Value of Kendo switch.</returns>
        public static bool GetSwitchValue(this By by)
        {
            return WaitHelpers.WaitForElementExist(by).GetSwitchValue();
        }


        /// <summary>
        /// Sets value of checkbox element.
        /// </summary>
        /// <param name="ele">IWebElement CheckBox</param>
        /// <param name="value">True/False</param>
        public static void SetCheckBoxValue(this IWebElement ele, bool value)//ToDo: need to refactor to work with all Checkbox elements.
        {
            if(ele.GetCheckBoxValue() != value)
            {
                Executor.ExecuteJavascript("arguments[0].click();", ele);
            }
        }

        /// <summary>
        /// Gets the value of the CheckBox element.
        /// </summary>
        /// <param name="ele">IWebElement CheckBox</param>
        /// <returns>Value of the CheckBox element.</returns>
        public static bool GetCheckBoxValue(this IWebElement ele)
        {
            try
            {
                return Convert.ToBoolean(ele.GetAttribute("checked"));
            }
            catch (Exception)
            {
                throw new Exception("Element is not a CheckBox element.");
            }
        }

        /// <summary>
        /// Clicks the element located using the By.
        /// </summary>
        /// <param name="by">By locator of element to click </param>
        public static void Click(this By by)
        {
            WaitHelpers.WaitForClickable(by).Click();
        }

        /// <summary>
        /// Draws a red boarder highlight around the element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driver"></param>
        public static void DrawHighlight(this IWebElement element, IWebDriver driver = null)
        {
            Executor.ExecuteJavascript(@"$(arguments[0]).css({ ""border-width"" : ""3px"", ""border-style"" : ""solid"", ""border-color"" : ""red"" });", element);
        }

        public static void RemoveHighlight(this IWebElement element, IWebDriver driver = null)
        {
            Executor.ExecuteJavascript(@"$(arguments[0]).css({ ""border-width"" : ""3px"", ""border-style"" : """", """" : """" });", element);
        }

        /// <summary>
        /// Perform selected operation on a row of a table.
        /// </summary>
        /// <param name="table">IWebElement table</param>
        /// <param name="driver">IWebDriver</param>
        /// <param name="rowNo">Index of row to perform operation.</param>
        /// <param name="operation">Operation to perform</param>
        public static void SelectOperationsForRowsBasedOnValue(this IWebElement table, IWebDriver driver, int rowNo, string operation)
        {
            WaitHelpers.ExplicitWait(200);
            Assert.IsTrue(table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Count > 0, "Any row with the desired Cell text/date is not found");

            var flag = true;
            table.WaitForPresent(driver);
            WaitHelpers.ExplicitWait(200);
            var row = table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"))[rowNo];
            switch (operation)
            {
                case "Edit":
                    row.FindElement(By.XPath("(//i[text()='mode_edit'])[" + (rowNo + 1) + "]")).DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(row.FindElement(By.XPath("(//i[text()='mode_edit'])[" + (rowNo + 1) + "]"))).Click();
                    flag = false;
                    break;

                case "Go To Details":
                    row.FindElement(By.XPath("(//i[text()='keyboard_arrow_right'])[" + (rowNo + 2) + "]")).DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(row.FindElement(By.XPath("(//i[text()='keyboard_arrow_right'])[" + (rowNo + 2) + "]"))).Click();
                    flag = false;
                    break;

                case "Expand_More":
                    //WaitHelpers.WaitForClickable(table.FindElement(By.XPath("(//i[text()='expand_more'])[" + (rowNo + 3) + "]"))).DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(table.FindElement(By.XPath($"(//i[text()='expand_more' and contains(@class, 'actionButton')])[{rowNo + 1}]"))).DrawHighlight(driver);
                    table.FindElement(By.XPath($"(//i[text()='expand_more' and contains(@class, 'actionButton')])[{rowNo + 1}]")).Click();
                    flag = false;
                    break;

                case "Expand_Less":
                    //table.FindElement(By.XPath("(//i[text()='expand_more'])[" + (rowNo + 1) + "]")).DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(table.FindElement(By.XPath($"(//i[contains(text(), 'expand_less') and contains(@class, 'actionButton')])[{rowNo + 1}]"))).DrawHighlight(driver);
                    table.FindElement(By.XPath($"(//i[contains(text(),'expand_less') and contains(@class, 'actionButton')])[{rowNo + 1}]")).Click();
                    flag = false;
                    break;
                
                case "Transfer Credit":
                    var cell = row.FindElement(By.CssSelector(".material-icons.dropdown-toggle"));
                    cell.DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(cell).Click();
                    WaitHelpers.ExplicitWait(250);
                    WaitHelpers.WaitForClickable(table.FindElement(By.XPath($"(//a[text()='Transfer Credit'])[{rowNo + 1}]"))).DrawHighlight(driver);
                    table.FindElement(By.XPath($"(//a[text()='Transfer Credit'])[{rowNo + 1}]")).Click();
                    flag = false;
                    break;

                case "Add New Order":
                    cell = row.FindElement(By.CssSelector(".material-icons.dropdown-toggle"));
                    cell.DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(cell).Click();
                    WaitHelpers.ExplicitWait(250);
                    if (cell.FindElements(By.XPath(".//parent::*//ul//li//a")).Any(e => e.Text.ToLower().Contains("Add New Order".ToLower())))
                    {
                        WaitHelpers.WaitForClickable(cell.FindElements(By.XPath(".//parent::*//ul//li//a")).Where(e => e.Text.ToLower().Contains("Add New Order".ToLower())).ToArray()[0]).Click();
                        flag = false;
                    }
                    break;


                default:
                    cell = row.FindElement(By.CssSelector(".material-icons.dropdown-toggle"));
                    cell.DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(cell).Click();
                    WaitHelpers.ExplicitWait(250);
                    if (cell.FindElements(By.XPath(".//parent::*//ul//li//a")).Any(e => e.Text.ToLower().Equals(operation.ToLower())))
                    {
                        WaitHelpers.WaitForClickable(cell.FindElements(By.XPath(".//parent::*//ul//li//a")).Where(e => e.Text.ToLower().Equals(operation.ToLower())).ToArray()[0]).Click();
                        flag = false;
                    }
                    break;
            }            
            Assert.IsFalse(flag, "The desried operation is not found for the particular row: " + rowNo + "Expected Operation: " + operation);
        }


        /// <summary>
        /// Perform selected operation on a row of a table For Order Module
        /// </summary>
        /// <param name="table">IWebElement table</param>
        /// <param name="driver">IWebDriver</param>
        /// <param name="rowNo">Index of row to perform operation.</param>
        /// <param name="operation">Operation to perform</param>
        public static void SelectOperationsForRowsBasedOnValueForOrderModule(this IWebElement table, IWebDriver driver, int rowNo, string operation)
        {
            WaitHelpers.ExplicitWait(200);
            Assert.IsTrue(table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Count > 0, "Any row with the desired Cell text/date is not found");

            var flag = true;
            table.WaitForPresent(driver);
            WaitHelpers.ExplicitWait(200);
            var row = table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"))[rowNo];
            switch (operation)
            {
                default:
                    var cell = row.FindElement(By.XPath(".//div[contains(@id,'ActionsMenu')]/button/i"));
                    cell.DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(cell).Click();
                    WaitHelpers.ExplicitWait(250);
                    if (cell.FindElements(By.XPath(".//parent::*//parent::*//ul//li//a//span")).Any(e => e.Text.ToLower().Equals(operation.ToLower())))
                    {
                        WaitHelpers.WaitForClickable(cell.FindElements(By.XPath(".//parent::*//parent::*//ul//li//a//span")).Where(e => e.Text.ToLower().Equals(operation.ToLower())).ToArray()[0]).Click();
                        flag = false;
                    }
                    break;                                
            }
            Assert.IsFalse(flag, "The desried operation is not found for the particular row: " + rowNo + "Expected Operation: " + operation);
        }



        //Created specifically for Checkboxes in Order module
        public static void ClickCheckboxes(this IWebElement checkboxElement,IWebDriver driver)//ToDo: Need to refactor all checkboxes.
        {
                try
                {
                    Executor.ExecuteJavascript("arguments[0].click();", checkboxElement);
                    //checkboxElement.Click();
                }
                catch
                {
                    Executor.ExecuteJavascript("window.scrollTo(0, document.body.scrollHeight)");
                    Executor.ExecuteJavascript("arguments[0].click();", checkboxElement);
                    //checkboxElement.Click();
                }
        }
        
        public static void SelectOperationsForRowsBasedOnValueForBatch(this IWebElement table, IWebDriver driver,
           int rowNo, string operation)
        {
            Assert.IsTrue(table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Count > 0,
                "Any row with the desired Cell text/date is not found");

            var flag = true;
            table.WaitForPresent(driver);
            switch (operation)
            {
                case "DeleteReceiptTypeOfBatch":
                    table.FindElement(By.XPath("(//i[text()='clear'])[" + (rowNo + 1) + "]")).DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(
                        table.FindElement(By.XPath("(//i[text()='clear'])[" + (rowNo + 1) + "]"))).Click();
                    flag = false;
                    break;

                case "EditReceiptTypeOfBatch":
                    table.FindElement(By.XPath("(//i[text()='mode_edit'])[" + (rowNo + 1) + "]")).DrawHighlight(driver);
                    WaitHelpers.WaitForClickable(
                        table.FindElement(By.XPath("(//i[text()='mode_edit'])[" + (rowNo + 1) + "]"))).Click();
                    flag = false;
                    break;

                default:                    
                    break;
            }

            Assert.IsFalse(flag,
                "The desried operation is not found for the particular row: " + rowNo + "Expected Operation: " +
                operation);

        }
        
    }//end of class
}//end of namespace