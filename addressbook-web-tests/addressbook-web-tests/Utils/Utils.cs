using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public static class Utils
    {
        public static GroupData PrepareEntityForGroupsView(GroupData g)
        {
            string pattern = "\\s+";
            Regex rgx = new Regex(pattern);

            return new GroupData()
            {
                Name = SetNullOrValueOf(rgx.Replace(g.Name, " ").Trim()),
                Header = g.Header,
                Footer = g.Footer,
                Id = g.Id
            };
        }


        public static ContactData PrepareEntityForContactsView(ContactData c)
        {
            string pattern = "\\s+";
            Regex rgx = new Regex(pattern);

            return new ContactData()
            {
                Id = c.Id,
                FirstName = SetNullOrValueOf(rgx.Replace(c.FirstName, " " ).Trim()),
                LastName = SetNullOrValueOf(rgx.Replace(c.LastName, " ").Trim()),
                HomePhone = SetNullOrValueOf(rgx.Replace(c.HomePhone, " ").Trim()),
                MobilePhone = SetNullOrValueOf(rgx.Replace(c.MobilePhone, " ").Trim()),
                WorkPhone = SetNullOrValueOf(rgx.Replace(c.WorkPhone, " ").Trim()),
                Fax = SetNullOrValueOf(rgx.Replace(c.Fax, " ").Trim()),
                SecondaryPhone = SetNullOrValueOf(rgx.Replace(c.SecondaryPhone, " ").Trim()),
                Email1 = SetNullOrValueOf(rgx.Replace(c.Email1, " ").Trim()),
                Email2 = SetNullOrValueOf(rgx.Replace(c.Email2, " ").Trim()),
                Email3 = SetNullOrValueOf(rgx.Replace(c.Email3, " ").Trim()),
                PrimaryAddress = c.PrimaryAddress.Trim()
            };
        }


        public static string PrepareDetailedViewForContact(ContactData contact)
        {
            string middleName = checkValueForEmptiness(contact.MiddleName);
            string HPage = checkValueForEmptiness(contact.HomePage);
            string SecondaryPhone = checkPhoneForEmptiness("P", contact.SecondaryPhone);

            if (middleName != "")
                middleName = " " + middleName + " ";
            else
                middleName = " ";


            if (HPage != "")
                HPage = "Homepage:\r\n" + HPage + "\r\n";


            if (SecondaryPhone != "")
                SecondaryPhone = "\r\n" + SecondaryPhone + "\r\n";


            string contactDetaiedView = (checkValueForEmptiness(contact.FirstName) + middleName +
                                        checkValueForEmptiness(contact.LastName)).Replace("\r\n", "") + "\r\n" +
                                        checkValueForEmptiness(contact.Nickname) +
                                        checkValueForEmptiness(contact.Title) +
                                        checkValueForEmptiness(contact.Company) +
                                        checkValueForEmptiness(contact.PrimaryAddress) + "\r\n" +
                                        checkPhoneForEmptiness("H", contact.HomePhone) +
                                        checkPhoneForEmptiness("M", contact.MobilePhone) +
                                        checkPhoneForEmptiness("W", contact.WorkPhone) +
                                        checkPhoneForEmptiness("F", contact.Fax) + "\r\n" +
                                        checkValueForEmptiness(contact.Email1) +
                                        checkValueForEmptiness(contact.Email2) +
                                        checkValueForEmptiness(contact.Email3) +
                                        HPage +
                                        "\r\n" + checkValueForEmptiness(contact.SecondaryAddress) + "\r\n" +
                                        SecondaryPhone + 
                                        checkValueForEmptiness(contact.Notes);



            return contactDetaiedView.Trim();
        }


        private static string checkValueForEmptiness(string val)
        {
            if (val == "" || val == null)
                return "";

            return val.Trim() + "\r\n";
        }


        private static string checkPhoneForEmptiness(string prefix, string phone)
        {
            if (phone == "" || phone == null)
                return "";

            return prefix + ": " + phone.Trim() + "\r\n";
        }


        private static string SetNullOrValueOf(string param)
        {
            if (param == "")
            {
                return null;
            }
            else
            {
                return param;
            }

        }
    }
}