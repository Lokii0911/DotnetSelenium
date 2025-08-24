using NUnit.Framework;  
using DotnetSelenium.DDT;
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
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Test : Base
    {
        [Test, TestCaseSource(nameof(LoginTestData))]
        public void TC001_Logincheck(string testCase, string username, string password, string expected)
        {
            Login log = new Login(driver, wait);
            log.DoLogin(username, password);
            bool result = log.IsLoginSuccessful();

            if (expected == "Pass")
            {
                Assert.That(result, Is.True, $"Expected login to succeed but it failed for {username}/{password}");
            }
            else
            {
                Assert.That(result, Is.False, $"Expected login to fail but it succeeded for {username}/{password}");
            }
        }



        [Test, TestCaseSource(nameof(LoginTestData))]
        public void TC002_PunchVerification(string testCase, string username, string password, string expected)
        {
            Login log = new Login(driver, wait);
            PunchCheck punch = new PunchCheck(driver, wait);
            if (expected == "Pass")
            {
                log.DoLogin(username, password);
                punch.punchcheck();
            }
            

        }

        [Test, TestCaseSource(nameof(AddEmployeeTestData))]
        public void TC003_AddEmployeeVerfication(string tcid, string login_id, string password, string firstName, string middleName, string lastName, string empId, string username, string emppass, string conpass, string expected)
        {
            Login log = new Login(driver, wait);
            AddEmployee add = new AddEmployee(driver, wait);
            log
                .DoLogin(login_id, password);
            add
                .Emp()
                .AddButton(firstName, middleName, lastName, empId)
                .logincredetials(username, emppass, conpass)
                .VerifyEmployeeSave(expected);
        }

        [Test, TestCaseSource(nameof(BuzzPost))]
        public void TC004_BuzzpostAddVerificationt(string testid,string username,string password,string Post)
        {
            Login log = new Login(driver, wait);
            Addbuzz buzz = new Addbuzz(driver, wait);
            log.DoLogin(username,password);
            buzz
                .Buzzclick(Post)
                .BuzzVerify();
        }

        [Test, TestCaseSource(nameof(Adminadd))]
        public void TC005_AdminAddVerification(string test_id,string login_id,string password,string empname,string username,string pass,string confpass,string expected)
        {
            AddAdmin adminadd = new AddAdmin(driver, wait);
            Login log = new Login(driver, wait);
            log
                .DoLogin(login_id,password);
            adminadd
                    .adminadd()
                    .detailsenroll(empname)
                    .enterpass(username,pass,confpass)
                    .VerifyAdminSave(expected);
        }


        [Test, TestCaseSource(nameof(Recruitment))]
        public void TC006_RecruitmentVerification(string test_id,string login_id,string password,string firstname,string lastname,string mailid,string phoneno,string expected)
        {

            Login log = new Login(driver, wait);
            Recruitment rec = new Recruitment(driver, wait);
            log.DoLogin(login_id,password);
            rec
                .Reg(firstname,lastname,mailid,phoneno)
                .VerifyRecruitmentSave(expected);
        }

    }

}
