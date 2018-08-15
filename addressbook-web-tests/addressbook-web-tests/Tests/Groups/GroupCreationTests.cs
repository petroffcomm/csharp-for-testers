using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        // Method to produce random test data sets
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }

            return groups;
        }


        // Method to produce random data sets from CSV text file
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                groups.Add(new GroupData(fields[0])
                {
                    Header = fields[1],
                    Footer = fields[2]
                });
            }

            return groups;
        }


        // Method to produce random data sets from XML file
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
        }


        // Method to produce random data sets from JSON file
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }


        // Method to produce random data sets from Excel file
        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook wBook = excelApp.Workbooks.Open(Directory.GetCurrentDirectory() + "\\" + "groups.xlsx");
            Excel.Worksheet wSheet = wBook.ActiveSheet;
            Excel.Range wSheetRange = wSheet.UsedRange;

            for (int i = 1; i <= wSheetRange.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = wSheetRange.Cells[i, 1].Value,
                    Header = wSheetRange.Cells[i, 2].Value,
                    Footer = wSheetRange.Cells[i, 3].Value
                });
            }

            wBook.Close();
            excelApp.Visible = false;
            excelApp.Quit();

            return groups;
        }


        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAllRecordsFromDB();

            app.Groups.Create(group);
            // First check if lists' sizes are equal
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.Count());

            List<GroupData> newGroups = GroupData.GetAllRecordsFromDB();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }


        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("d'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = GroupData.GetAllRecordsFromDB();

            app.Groups.Create(group);
            // First check if lists' sizes are equal
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.Count());

            List<GroupData> newGroups = GroupData.GetAllRecordsFromDB();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }


        [Test]
        public void TestDBConnectivity()
        {
            List<GroupData> groups = GroupData.GetAllRecordsFromDB();
            foreach (ContactData contact in groups[0].GetContacts())
            {
                Console.WriteLine(contact);
            }
        }

    }
}
