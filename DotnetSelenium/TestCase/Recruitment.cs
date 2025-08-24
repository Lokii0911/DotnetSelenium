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
    public class Recruitment:Basefunc
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private static readonly By RecruitmentMenu = By.XPath("//ul/li/a/span[text()='Recruitment']");
        private static readonly By AddButton = By.XPath("//button[contains(@class,'oxd-button--secondary') and contains(normalize-space(.), 'Add')]");
        private static readonly By firstname = By.XPath("//input[@name='firstName' and contains(@class, 'orangehrm-firstname') and @placeholder='First Name']");
        private static readonly By lastname = By.XPath("//input[@name='lastName' and contains(@class, 'orangehrm-lastname') and @placeholder='Last Name']");
        private static readonly By Email = By.XPath("//label[text()='Email']/ancestor::div[contains(@class,'oxd-input-group')]//input[@placeholder='Type here']");
        private static readonly By ContactNumber = By.XPath("//label[text()='Contact Number']/ancestor::div[contains(@class,'oxd-input-group')]//input[@placeholder='Type here']\r\n");
        private static readonly By SaveButton = By.XPath("//button[@type='submit' and contains(@class,'oxd-button--secondary') and contains(normalize-space(.), 'Save')]");
        private static readonly By VerifytoastMessage = By.XPath("//div[contains(@class,'oxd-toast-content')]");

        public Recruitment(IWebDriver driver, WebDriverWait wait):base(driver,wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public Recruitment Reg(string FirstName,string LastName,string mailid,string phoneno)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(RecruitmentMenu)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(AddButton)).Click();
            IWebElement Firstname = wait.Until(ExpectedConditions.ElementToBeClickable(firstname));
            Firstname.Click();
            Firstname.SendKeys(FirstName);
            IWebElement Lastname = wait.Until(ExpectedConditions.ElementToBeClickable(lastname));
            Lastname.Click();
            Lastname.SendKeys(LastName);
            wait.Until(ExpectedConditions.ElementToBeClickable(Email)).SendKeys(mailid);
            wait.Until(ExpectedConditions.ElementToBeClickable(ContactNumber)).SendKeys(phoneno);
            var saveBtn = wait.Until(ExpectedConditions.ElementExists(SaveButton));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", saveBtn);
            wait.Until(ExpectedConditions.ElementToBeClickable(SaveButton)).Click();
            return this;
        }

        public Recruitment VerifyRecruitmentSave(string expected)
        {
            VerifySave(expected, VerifytoastMessage);   
            return this;
        }


    }
}
