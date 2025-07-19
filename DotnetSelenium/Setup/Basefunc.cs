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

        public void SafestClick(By locator)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));

                // Scroll into view first
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);

                try
                {
                    element.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    // Fallback to JavaScript click if intercepted
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

    }
}
