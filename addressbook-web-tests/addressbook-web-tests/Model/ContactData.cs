using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : BaseDataObj, IEquatable<ContactData>, IComparable<ContactData>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string PrimaryAddress { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string BDay { get; set; }
        public string ADay { get; set; }
        public string BMonth { get; set; }
        public string AMonth { get; set; }
        public string BYear { get; set; }
        public string AYear { get; set; }
        public string SecondaryAddress { get; set; }
        public string SecondaryPhone { get; set; }
        public string Notes { get; set; }

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
                    return (GetPhoneFormatted(HomePhone) + GetPhoneFormatted(MobilePhone) + GetPhoneFormatted(WorkPhone)).Trim();
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
            this.Init();
        }


        public ContactData(string name)
        {
            this.Init();
            this.FirstName = name;
        }


        private void Init()
        {
            this.FirstName = "";
            this.MiddleName = "";
            this.LastName = "";
            this.Nickname = "";
            this.Title = "";
            this.Company = "";
            this.PrimaryAddress = "";
            this.HomePhone = "";
            this.MobilePhone = "";
            this.WorkPhone = "";
            this.Fax = "";
            this.Email1 = "";
            this.Email2 = "";
            this.Email3 = "";
            this.BDay = "";
            this.BMonth = "-";
            this.BYear = "";
            this.ADay = "";
            this.AMonth = "-";
            this.AYear = "";
            this.SecondaryAddress = "";
            this.SecondaryPhone = "";
            this.Notes = "";
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

            bool ids_are_equal = (Id is null || other.Id is null || Id == other.Id);

            return FirstName == other.FirstName 
                && LastName == other.LastName
                && PrimaryAddress == other.PrimaryAddress
                && ids_are_equal;
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
            return Id.GetHashCode() + FirstName.GetHashCode() + LastName.GetHashCode();
        }


        public override string ToString()
        {
            return String.Format("id = {0}; firstName = {1}; lastName = {2}", Id, FirstName, LastName);
        }


        private string GetPhoneFormatted(string phone)
        /** Format phone number as if it's being displayed on contacts table view**/
        {
            if (phone == null || phone == "")
            {
                return "";
            }

            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
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
