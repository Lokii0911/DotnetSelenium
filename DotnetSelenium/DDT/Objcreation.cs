using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DotnetSelenium.DDT
{
    public class Objcreation
    {
        public static IEnumerable<object[]> LoginTestData
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestDatafile.xlsx");
                Console.WriteLine($"Looking for Excel at: {path}");
                Console.WriteLine(File.Exists(path) ? "File found " : "File missing ");
                return ExcelReader.ReadExcel(path, "Login");
            }
        }
        public static IEnumerable<object[]> AddEmployeeTestData
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestDatafile.xlsx");
                Console.WriteLine($"Looking for Excel at: {path}");
                Console.WriteLine(File.Exists(path) ? "File found " : "File missing ");
                return ExcelReader.ReadExcel(path, "AddEmp");
            }
        }

        public static IEnumerable<object[]> BuzzPost
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestDatafile.xlsx");
                Console.WriteLine($"Looking for Excel at: {path}");
                Console.WriteLine(File.Exists(path) ? "File found " : "File missing ");
                return ExcelReader.ReadExcel(path,"Buzzpost");

            }
        }

        public static IEnumerable<object[]> Adminadd
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestDatafile.xlsx");
                Console.WriteLine($"Looking for Excel at: {path}");
                Console.WriteLine(File.Exists(path) ? "File found " : "File missing ");
                return ExcelReader.ReadExcel(path, "AddAdmin");
            }
        }

        public static IEnumerable<object[]> Recruitment
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestDatafile.xlsx");
                Console.WriteLine($"Looking for Excel at: {path}");
                Console.WriteLine(File.Exists(path) ? "File found " : "File missing ");
                return ExcelReader.ReadExcel(path, "Recruitment");

            }
        }
    }
}
