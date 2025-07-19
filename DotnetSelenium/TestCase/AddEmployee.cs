using DotnetSelenium.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSelenium.TestCase
{
    public class AddEmployee:Basefunc
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By PIM= By.XPath("//a[.//span[normalize-space()='PIM']]");
        private static readonly By Emplist= By.XPath("//a[contains(@class, 'nav-tab-item') and normalize-space()='Employee List']");
        private static readonly By Add= By.XPath("//button[contains(@class, 'oxd-button') and .//text()[normalize-space()='Add']]");
        private static readonly By FirstNameInput = By.Name("firstName");
        private static readonly By MiddleNameInput = By.Name("middleName");
        private static readonly By LastNameInput = By.Name("lastName");
        private static readonly By CheckboxInput = By.XPath("//span[contains(@class, 'oxd-switch-input')]");
        private static readonly By username = By.XPath("//div/input[@class='oxd-input oxd-input--active' and @autocomplete='off']");
        private static readonly By pass= By.XPath("//div/input[@class='oxd-input oxd-input--active' and @type='password' and @autocomplete='off']");
        private static readonly By passfocus = By.XPath("//div/input[@class='oxd-input oxd-input--focus' and @type='password' and @autocomplete='off']");
        private static readonly By Save= By.XPath("//button[@type='submit' and @class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']");

        public AddEmployee(IWebDriver driver, WebDriverWait wait):base(driver,wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        
        public  AddEmployee Emp()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(PIM)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(Emplist)).Click();
            return this;
        }

        
        public AddEmployee AddButton()
        {
            SafeClick(Add);
            wait.Until(ExpectedConditions.ElementToBeClickable(FirstNameInput)).SendKeys("Hari");
            wait.Until(ExpectedConditions.ElementToBeClickable(MiddleNameInput)).SendKeys("Chandra");
            wait.Until(ExpectedConditions.ElementToBeClickable(LastNameInput)).SendKeys("Gupta");
            return this;
        }
        public AddEmployee logincredetials()
        {
            SafeClick(CheckboxInput);
            wait.Until(ExpectedConditions.ElementToBeClickable(username)).SendKeys("Hari#07");
            wait.Until(ExpectedConditions.ElementToBeClickable(pass)).SendKeys("Hari#2004");
            wait.Until(ExpectedConditions.ElementToBeClickable(passfocus)).SendKeys("Hari#2004");
            SafeClick(Save);
            return this;

        }
    }
}
