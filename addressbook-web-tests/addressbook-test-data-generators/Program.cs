using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string objectsType = args[0];
            int recsQty = Convert.ToInt32(args[1]);
            string fileName = args[2];

            string dataFormat = args[3];

            if (objectsType == "groups")
            {
                ProduceGroups(dataFormat, recsQty, fileName);
            } else
              if (objectsType == "contacts")
            {
                ProduceContacts(dataFormat, recsQty, fileName);
            }
        }


        /** Groups methods **/


        static void ProduceGroups(string dataFormat, int recsQty, string fileName)
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < recsQty; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(100)
                });
            }

            if (dataFormat != "excel")
            {
                StreamWriter writer = new StreamWriter(fileName);
                switch (dataFormat)
                {
                    case "csv":
                        WriteGroupsToCsvFile(groups, writer);
                        break;
                    case "xml":
                        WriteGroupsToXmlFile(groups, writer);
                        break;
                    case "json":
                        WriteGroupsToJsonFile(groups, writer);
                        break;
                    default:
                        Console.WriteLine("Unrecognized file format.");
                        break;
                }
                writer.Close();
            }
            else
            {
                WriteGroupsToExcelFile(groups, fileName);
            }
            
        }


        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("{0},{1},{2}",
                    group.Name, group.Header, group.Footer));
            }
        }


        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }


        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }


        static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook wBook = excelApp.Workbooks.Add();
            Excel.Worksheet wSheet = wBook.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                wSheet.Cells[row, 1] = group.Name;
                wSheet.Cells[row, 2] = group.Header;
                wSheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullPath = Directory.GetCurrentDirectory() + "\\" + fileName;
            File.Delete(fullPath);
            wBook.SaveAs(fullPath);

            wBook.Close();
            excelApp.Visible = false;
            excelApp.Quit();
        }


        /** Contacts methods **/


        static void ProduceContacts(string dataFormat, int recsQty, string fileName)
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < recsQty; i++)
            {
                contacts.Add(new ContactData() {
                    FirstName = TestBase.GetRandomAllowedStringFor(TestBase.GENERAL, 20),
                    LastName = TestBase.GetRandomAllowedStringFor(TestBase.GENERAL, 20),
                    Email1 = TestBase.GetRandomAllowedStringFor(TestBase.EMAIL, 30),
                    Email2 = TestBase.GetRandomAllowedStringFor(TestBase.EMAIL, 30),
                    Email3 = TestBase.GetRandomAllowedStringFor(TestBase.EMAIL, 30),
                    HomePhone = TestBase.GetRandomAllowedStringFor(TestBase.PHONE, 9),
                    MobilePhone = TestBase.GetRandomAllowedStringFor(TestBase.PHONE, 9),
                    WorkPhone = TestBase.GetRandomAllowedStringFor(TestBase.PHONE, 9)
                });
            }

            if (dataFormat != "excel")
            {
                StreamWriter writer = new StreamWriter(fileName);
                switch (dataFormat)
                {
                    case "csv":
                        WriteContactsToCsvFile(contacts, writer);
                        break;
                    case "xml":
                        WriteContactsToXmlFile(contacts, writer);
                        break;
                    case "json":
                        WriteContactsToJsonFile(contacts, writer);
                        break;
                    default:
                        Console.WriteLine("Unrecognized file format.");
                        break;
                }
                writer.Close();
            }
            else
            {
                WriteContactsToExcelFile(contacts, fileName);
            }
        }


        private static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    contact.FirstName, contact.LastName, contact.Email1, contact.Email2, contact.Email3,
                    contact.HomePhone, contact.MobilePhone, contact.WorkPhone));
            }
        }


        private static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }


        private static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }


        private static void WriteContactsToExcelFile(List<ContactData> contacts, string fileName)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook wBook = excelApp.Workbooks.Add();
            Excel.Worksheet wSheet = wBook.ActiveSheet;

            /** Set formatting to "text" for all cells **/
            wSheet.Cells.NumberFormat = "@";

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                wSheet.Columns.AutoFit();
                wSheet.Cells[row, 1] = contact.FirstName;
                wSheet.Cells[row, 2] = contact.LastName;
                wSheet.Cells[row, 3] = contact.Email1;
                wSheet.Cells[row, 4] = contact.Email2;
                wSheet.Cells[row, 5] = contact.Email3;
                wSheet.Cells[row, 6] = contact.HomePhone;
                wSheet.Cells[row, 7] = contact.MobilePhone;
                wSheet.Cells[row, 8] = contact.WorkPhone;
                row++;
            }

            string fullPath = Directory.GetCurrentDirectory() + "\\" + fileName;
            File.Delete(fullPath);
            wBook.SaveAs(fullPath);

            wBook.Close();
            excelApp.Visible = false;
            excelApp.Quit();
        }
    }
}
