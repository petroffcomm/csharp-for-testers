using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : BaseDataObj, IEquatable<ContactData>, IComparable<ContactData>
    {
        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "address")]
        public string PrimaryAddress { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email1 { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "homepage")]
        public string HomePage { get; set; }

        //[Column(Name = "")]
        public string BDay { get; set; }

        //[Column(Name = "")]
        public string ADay { get; set; }

        [Column(Name = "bmonth")]
        public string BMonth { get; set; }

        [Column(Name = "amonth")]
        public string AMonth { get; set; }

        [Column(Name = "byear")]
        public string BYear { get; set; }

        [Column(Name = "ayear")]
        public string AYear { get; set; }

        [Column(Name = "address2")]
        public string SecondaryAddress { get; set; }

        [Column(Name = "phone2")]
        public string SecondaryPhone { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

        // don't serialize/deserialize this field to/from XML and JSON
        [XmlIgnore]
        [JsonIgnore]
        [Column(Name = "deprecated")]
        public MySql.Data.Types.MySqlDateTime DeprecationDate { get; set; }

        private string allPhones;
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (GetPhoneFormatted(HomePhone) + GetPhoneFormatted(MobilePhone)
                            + GetPhoneFormatted(WorkPhone) + GetPhoneFormatted(SecondaryPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }


        private string allEmails;
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (GetEmailFormatted(Email1) + GetEmailFormatted(Email2) + GetEmailFormatted(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }


        public ContactData()
        {
            Init();
        }


        public ContactData(string name)
        {
            this.FirstName = name;
            Init();
        }


        protected void Init()
        {
            // Initiate this pars to avoid NPEs in GetHashCode()
            this.LastName = "";
            this.PrimaryAddress = "";
        }


        public static List<ContactData> GetActiveRecordsFromDB()
        {
            List<ContactData> contacts = new List<ContactData>();

            using (AddressBookDB db = new AddressBookDB())
            {
                /** c.Deprecated.Year == 0 - picks out only active records
                    which weren't removed from UI **/

                contacts = (from c in db.Contacts
                            where c.DeprecationDate.Year == 0
                            select c
                            ).ToList();
            }

            // Process records to get them in proper format to compare with records from
            // UI representation level
            List<ContactData> contacts2 = new List<ContactData>();
            foreach (ContactData c in contacts)
            {
                contacts2.Add(Utils.PrepareEntityForContactsView(c));
            }

            return contacts2;
        }


        public static List<ContactData> GetAllRecordsFromDB()
        {
            List<ContactData> contacts = new List<ContactData>();

            using (AddressBookDB db = new AddressBookDB())
            {
                /** getting all records - including deleted **/

                contacts = (from c in db.Contacts select c
                            ).ToList();
            }

            return contacts;
        }


        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            bool idsAreEqual = (Id is null || other.Id is null || Id == other.Id);

            return strFieldsAreEq(FirstName, other.FirstName)
                && strFieldsAreEq(LastName, other.LastName)
                && strFieldsAreEq(PrimaryAddress, other.PrimaryAddress)
                && idsAreEqual;
        }


        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            /**int result = FirstName.CompareTo(other.FirstName);
            if (result != 0)
                return result;

            result = LastName.CompareTo(other.LastName);
            if (result != 0)
                return result;

            result = Id.CompareTo(other.Id);
            if (result != 0)
                return result;**/

            int result = comparisonWithCheckForNULL(FirstName, other.FirstName);
            if (result != 0)
                return result;

            result = comparisonWithCheckForNULL(LastName, other.LastName);
            if (result != 0)
                return result;

            result = comparisonWithCheckForNULL(PrimaryAddress, other.PrimaryAddress);
            if (result != 0)
                return result;

            result = comparisonWithCheckForNULL(Id, other.Id);
            if (result != 0)
                return result;

            return 0;
        }


        public override int GetHashCode()
        {
            return FirstName.GetHashCode()  + LastName.GetHashCode();
        }


        public override string ToString()
        {
            return String.Format("id = {0}; firstName = {1}; lastName = {2}; primaryAddress = {3}", Id, FirstName, LastName, PrimaryAddress);
        }


        private string GetPhoneFormatted(string phone)
        /** Format phone number as if it's being displayed on contacts table view **/
        {
            if (phone == null || phone == "")
            {
                return "";
            }

            // Replace space-char, hyphen-char and brackets
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }


        private string GetEmailFormatted(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }

            return email + "\r\n";
        }
    }
}
