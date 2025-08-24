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
using DotnetSelenium.Setup;

namespace DotnetSelenium.TestCase
{
    public class Login: Basefunc
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By username=By.Name("username");    
        private static readonly By password=By.Name("password");
        private static readonly By button = By.CssSelector("button[type='submit']");
        private static readonly By dashboardHeader = By.XPath("//h6[text()='Dashboard']");

        public Login(IWebDriver driver,WebDriverWait wait):base(driver, wait)
        {
            this.driver = driver;
            this.wait=wait; 
        }

        public Login DoLogin(string usernameValue, string passwordValue)
        {
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
            driver.FindElement(username).Clear();
            driver.FindElement(username).SendKeys(usernameValue ?? "");
            driver.FindElement(password).Clear();
            driver.FindElement(password).SendKeys(passwordValue ?? "");
            SafeClick(button);
            return this; 
        }

        public bool IsLoginSuccessful()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(dashboardHeader));
                return driver.FindElement(dashboardHeader).Displayed;
            }
            catch
            {
                return false; 
            }
        }



    }
}
