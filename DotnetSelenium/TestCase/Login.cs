using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace DotnetSelenium.TestCase
{
    public class Login
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By username=By.Name("username");    
        private static readonly By password=By.Name("password");
        private static readonly By button = By.CssSelector("button[type='submit']");

        public Login(IWebDriver driver,WebDriverWait wait)
        {
            this.driver = driver;
            this.wait=wait; 
        }

        public Login DoLogin()
        {
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
            driver.FindElement(username).SendKeys("Admin");
            driver.FindElement(password).SendKeys("admin123");
            wait.Until(ExpectedConditions.ElementToBeClickable(button));
            driver.FindElement(button).Click();
            return this;
            
        }

        public Login LoginVerify()
        {

            By dashboardHeader = By.XPath("//h6[text()='Dashboard']");
            wait.Until(ExpectedConditions.ElementIsVisible(dashboardHeader));
            string actualText = driver.FindElement(dashboardHeader).Text;
            Assert.That(actualText, Is.EqualTo("Dashboard"), "Login failed or unexpected page title");
            return this;
        }
        
            
        
    }
}
