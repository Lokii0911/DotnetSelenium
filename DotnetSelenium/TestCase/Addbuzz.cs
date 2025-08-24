using DotnetSelenium.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSelenium.TestCase
{
    public class Addbuzz : Basefunc
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By buzzButton = By.XPath("/html/body/div/div[1]/div[1]/aside/nav/div[2]/ul/li[12]/a");
        private static readonly By textArea = By.CssSelector("textarea.oxd-buzz-post-input");
        private static readonly By postButton = By.XPath("//button[normalize-space()='Post']");
        private static readonly By successToast = By.XPath("//div[@id='oxd-toaster_1']//p");

        public Addbuzz(IWebDriver driver,WebDriverWait wait):base(driver,wait)
       {
            this.driver = driver;
            this.wait = wait;
       }
       public Addbuzz Buzzclick(string post)
        {  
            SafeClick(buzzButton);
            driver.FindElement(textArea).SendKeys(post);
            SafestClick(postButton);
            return this;
        }
        public Addbuzz BuzzVerify()
        {
            if (IsElementVisible(successToast))
            {
                Console.WriteLine("Successful");
            }
            else
            {
                Assert.Fail("Success toast did not appear.");
            }
            return this;
        }
    }
}
