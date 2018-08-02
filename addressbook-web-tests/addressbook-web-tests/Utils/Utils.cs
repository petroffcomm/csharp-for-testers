namespace WebAddressbookTests
{
    public static class Utils
    {
        public static string PrepareDetailedViewForContact(ContactData contact)
        {
            string middleName = checkValueForEmptiness(contact.MiddleName);
            string HPage = checkValueForEmptiness(contact.HomePage);

            if (middleName != "")
                middleName = " " + middleName + " ";
            else
                middleName = " ";
             

            if (HPage != "")
                HPage = "Homepage:\r\n" + HPage + "\r\n";

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
                                        checkPhoneForEmptiness("P", contact.SecondaryPhone) + "\r\n" +
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
    }
}