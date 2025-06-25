using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSelenium.TestCase
{
    public class PunchCheck
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By Time=By.XPath("//span[text()='Time']");
        private static readonly By Attendance = By.XPath("//span[text()='Attendance ']/ancestor::li");
        private static readonly By PunchInout = By.XPath("//a[contains(., 'Punch In/Out')]");
        private static readonly By punchInButton = By.XPath("//button[normalize-space()='In']");
        private static readonly By punchOutButton = By.XPath("//button[normalize-space()='Out']");

        public PunchCheck(IWebDriver driver,WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;   
        }
        public void NavigatePage()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(Time)).Click(); 
            wait.Until(ExpectedConditions.ElementToBeClickable(Attendance)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(PunchInout)).Click();        
        }
     
        public void punchcheck()
        {
            NavigatePage();
            if (IsElementVisible(punchInButton))
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(punchInButton)).Click();
                bool switched = wait.Until(driver => IsElementVisible(punchOutButton));
                Assert.IsTrue(switched, "Punch In failed — Out button not visible after clicking.");
            }
            else if (IsElementVisible(punchOutButton))
            {
                driver.Navigate().Refresh();
                wait.Until(ExpectedConditions.ElementToBeClickable(punchOutButton)).Click();
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(punchOutButton));
                bool switched = wait.Until(driver =>IsElementVisible(punchInButton));
                Assert.IsTrue(switched, "Punch Out failed — In button not visible after clicking.");
            }
            else
            {
                Assert.Fail("Neither Punch In nor Punch Out button is available.");
            }
        }
    

        public bool IsElementVisible(By locator)
        {
            try
            {
                var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return element.Displayed && element.Enabled;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


    }
}
