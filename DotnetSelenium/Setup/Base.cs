
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using System;
using System.Xml.Serialization;
namespace DotnetSelenium.Setup
{
   
    public class Base
    {
        protected  IWebDriver driver;
        protected  WebDriverWait wait;
        
        [SetUp]
        public void Setup() {
           driver =new ChromeDriver();
           driver.Manage().Window.Maximize();
           driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);
           wait = new WebDriverWait(driver,TimeSpan.FromSeconds(30));      
        }
        [TearDown]
        public void TearDown() { 
             driver.Dispose();
        }

    }
}