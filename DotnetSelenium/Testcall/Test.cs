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
    public class Test : Base
    {

        [Test]
        public void TC001_Logincheck()
        {
            Login log = new Login(driver, wait);
            log
                .DoLogin()
                .LoginVerify();

        }

        [Test]
        public void TC002_Loginfailcheck()
        {
            Login log = new Login(driver, wait);
            log
                .DoNotLogin()
                .DonotLoginVerify();

        }

        [Test]
        public void TC003_PunchVerification()
        {
            Login log = new Login(driver, wait);
            PunchCheck punch = new PunchCheck(driver, wait);
            log
                .DoLogin()
                .LoginVerify();
            punch.punchcheck();

        }

        [Test]
        public void TC004_Adminverificationcheck()
        {
            AddAdmin adminadd = new AddAdmin(driver, wait);
            Login log = new Login(driver, wait);
            log
                .DoLogin();
            adminadd
                    .adminadd()
                    .detailsenroll()
                    .enterpass();
        }

        [Test]
        public void TC005_AddEmployeeVerfication()
        {
            Login log = new Login(driver, wait);
            AddEmployee add = new AddEmployee(driver, wait);
            log
                .DoLogin()
                .LoginVerify();


            add
                .Emp()
                .AddButton()
                .logincredetials();


        }
        [Test]
        public void TC006_BuzzpostAddVerificationt()
        {
            Login log = new Login(driver, wait);
            Addbuzz buzz = new Addbuzz(driver, wait);
            log.DoLogin();
            buzz
                .Buzzclick()
                .BuzzVerify();
        }

        [Test]
        public void TC007_RecruitmentVerification()
        {

            Login log = new Login(driver, wait);
            Recruitment rec = new Recruitment(driver, wait);
            log.DoLogin();
            rec
                .Reg();
        }

    }

}