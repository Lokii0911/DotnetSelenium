using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetSelenium.Setup;

namespace DotnetSelenium.TestCase
{
    public class PunchCheck: Basefunc
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By Time=By.XPath("//span[text()='Time']");
        private static readonly By Attendance = By.XPath("//span[text()='Attendance ']/ancestor::li");
        private static readonly By PunchInout = By.XPath("//a[contains(., 'Punch In/Out')]");
        private static readonly By punchInButton = By.XPath("//button[normalize-space()='In']");
        private static readonly By punchOutButton = By.XPath("//button[normalize-space()='Out']");
        private static readonly By punchNote = By.XPath("//textarea[@placeholder='Type here' and contains(@class,'oxd-textarea')]");
        
        public PunchCheck(IWebDriver driver,WebDriverWait wait):base(driver,wait)
        {
            this.driver = driver;
            this.wait = wait;   
        }
        public void NavigatePage()
        {
            SafeClick(Time); 
            SafeClick(Attendance);
            SafeClick(PunchInout);        
        }
     
        public void punchcheck()
        {
            NavigatePage();
            if (IsElementVisible(punchInButton))
            {
                Console.WriteLine("Punch In button found");
                wait.Until(ExpectedConditions.ElementToBeClickable(punchNote)).SendKeys("Punching in via automation");
                wait.Until(ExpectedConditions.ElementToBeClickable(punchInButton)).Click();
                bool punchedIn = wait.Until(driver => IsElementVisible(punchOutButton));
                Assert.IsTrue(punchedIn, "Punch In failed — Punch Out button not visible after punching in.");
                Console.WriteLine("Punch In successful ");
            }
            else if (IsElementVisible(punchOutButton))
            {
                Console.WriteLine("Punch Out button found ");
                wait.Until(ExpectedConditions.ElementToBeClickable(punchNote)).SendKeys("Punching out via automation");
                wait.Until(ExpectedConditions.ElementToBeClickable(punchOutButton)).Click();
                bool punchedOut = wait.Until(driver => IsElementVisible(punchInButton));
                Assert.IsTrue(punchedOut, "Punch Out failed — Punch In button not visible after punching out.");
                Console.WriteLine("Punch Out successful 🚀");
            }
            else
            {
                Assert.Fail("Neither Punch In nor Punch Out button is available.");
            }
        }
    

        /*public bool IsElementVisible(By locator)
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
        }*/


    }
}
