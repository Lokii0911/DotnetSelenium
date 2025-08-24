using DotnetSelenium.TestCase;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSelenium.Setup
{
    public class Basefunc
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Basefunc(IWebDriver driver,WebDriverWait wait) {
            this.driver = driver;
            this.wait = wait;
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
        public void SafeClick(By locator)
        {
            try
            {
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                element.Click();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Element not clickable : {locator}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during SafeClick: {e.Message}");
            }
        }

        public void Sendvalues(By locator,string value)
        {
            try
            {
               wait.Until(ExpectedConditions.ElementToBeClickable(locator)).SendKeys(value);
               
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Element not clickable : {locator}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during SafeClick: {e.Message}");
            }
        }

        public void SafestClick(By locator)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);

                try
                {
                    element.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Element not clickable : {locator}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during SafeClick: {e.Message}");
            }
        }

        public void VerifySave(string expected,By locator)
        {
            try
            {
                var toast = wait.Until(ExpectedConditions.ElementIsVisible(locator));

                bool success = toast.Text.Contains("Successfully Saved", StringComparison.OrdinalIgnoreCase);

                if (expected.Equals("Pass", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.That(success, Is.True, " Expected creation to succeed but no success toast appeared.");
                }
                else
                {
                    Assert.That(success, Is.False, "Expected failure, but success toast appeared.");
                }
            }
            catch (WebDriverTimeoutException)
            {
                if (expected.Equals("Pass", StringComparison.OrdinalIgnoreCase))
                    Assert.Fail(" Expected success but no toast appeared within timeout.");
                else
                    Assert.Pass("Expected failure, and no success toast appeared.");
            }
            
        }

    }
}
