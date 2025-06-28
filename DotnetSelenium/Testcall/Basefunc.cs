using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSelenium.Testcall
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
    }
}
