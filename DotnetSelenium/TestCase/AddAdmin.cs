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
     public  class AddAdmin
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By Admin = By.XPath("//ul/li/a/span[text()='Admin']");
        private static readonly By AddButton = By.XPath("//button[normalize-space()='Add']");
        private static readonly By UserRoleDropdown = By.XPath("//label[text()='User Role']/following::div[contains(@class, 'oxd-select-text')][1]");
        private static readonly By AdminOption = By.XPath("//div[@role='option']/span[text()='Admin']");
        private static readonly By EmployeeNameInput = By.XPath("//input[@placeholder='Type for hints...']");
        private static readonly By Dropdown = By.XPath("//div[@class='oxd-select-text-input' and text()='-- Select --']");
        private static readonly By EnabledOption = By.XPath("//div[@role='option']//span[text()='Enabled']");
        private static readonly By UsernameInput = By.XPath("//label[text()='Username']/following::input[1]");
        private static readonly By PasswordInput = By.XPath("//label[text()='Password']/following::input[@type='password'][1]");
        private static readonly By ConfirmPasswordInput = By.XPath("//label[text()='Confirm Password']/following::input[@type='password'][1]");
        private static readonly By SaveButton = By.XPath("//button[normalize-space()='Save']");
        public AddAdmin(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        
        public AddAdmin adminadd()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(Admin)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(AddButton)).Click();
            return this;
        }
        public AddAdmin detailsenroll() 
        {
            
            wait.Until(ExpectedConditions.ElementToBeClickable(UserRoleDropdown)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminOption)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(EmployeeNameInput)).SendKeys("Tony Stark");
            wait.Until(ExpectedConditions.ElementToBeClickable(Dropdown)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(EnabledOption)).Click();
            return this;
        }

        public AddAdmin enterpass()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(UsernameInput)).SendKeys("Tony#Greatness");
            wait.Until(ExpectedConditions.ElementIsVisible(PasswordInput)).SendKeys("Tony@greatness45forever");
            wait.Until(ExpectedConditions.ElementIsVisible(ConfirmPasswordInput)).SendKeys("Tony@greatness45forever");
            wait.Until(ExpectedConditions.ElementToBeClickable(SaveButton)).Click();
            return this;
        }
    }
}
