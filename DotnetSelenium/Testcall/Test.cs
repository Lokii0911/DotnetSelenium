using DotnetSelenium.Setup;
using DotnetSelenium.TestCase;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSelenium.Testcall
{
    public class Test:Base
    {
        

        [Test]
        public void LoginCheck()
        {
            Login log = new Login(driver,wait);
            log
                .DoLogin()
                .LoginVerify();
                
        }

        [Test]  
        public void LoginFailCheck()
        {
            Login log = new Login(driver, wait);
            log
                .DoNotLogin()
                .DonotLoginVerify();

        }

        [Test]
        public void Punch()
        {
            Login log = new Login(driver, wait);
            PunchCheck punch = new PunchCheck(driver, wait);
            log
                .DoLogin()
                .LoginVerify();
            punch.punchcheck();

        }

        [Test]
        public void AdminCheck()
        {
            AddAdmin adminadd=new AddAdmin(driver,wait);
            Login log = new Login(driver, wait);
            log
                .DoLogin();
            adminadd
                    .adminadd()
                    .detailsenroll()
                    .enterpass();
        }

        [Test]
        public void Emp() {
            Login log = new Login(driver, wait);
            AddEmployee add=new AddEmployee(driver, wait);
            log
                .DoLogin()
                .LoginVerify();
                

            add
                .Emp()
                .AddButton()
                .logincredetials();


        }
        [Test]
        public void Buzzpost()
        {
            Login log = new Login(driver, wait);
            Addbuzz buzz=new Addbuzz(driver, wait);
            log.DoLogin();
            buzz
                .Buzzclick()
                .BuzzVerify();
        }

    }

}
