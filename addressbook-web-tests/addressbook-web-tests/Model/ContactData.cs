using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        private string firstname = "";
        private string middlename = "";
        private string lastname = "";
        private string nickname = "";
        private string title = "";
        private string company = "";
        private string primaryAddress = "";
        private string homePhone = "";
        private string mobilePhone = "";
        private string workPhone = "";
        private string fax = "";
        private string email_1 = "";
        private string email_2 = "";
        private string email_3 = "";
        private string bday = "";
        private string bmonth = "-";
        private string byear = "";
        private string aday = "";
        private string amonth = "-";
        private string ayear = "";
        private string secondaryAddress = "";
        private string secondaryPhone = "";
        private string notes = "";

        public string FirstName
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        public string MiddleName
        {
            get
            {
                return middlename;
            }
            set
            {
                middlename = value;
            }
        }

        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                nickname = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
            }
        }

        public string PrimaryAddress
        {
            get
            {
                return primaryAddress;
            }
            set
            {
                primaryAddress = value;
            }
        }

        public string HomePhone
        {
            get
            {
                return homePhone;
            }
            set
            {
                homePhone = value;
            }
        }

        public string MobilePhone
        {
            get
            {
                return mobilePhone;
            }
            set
            {
                mobilePhone = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                return workPhone;
            }
            set
            {
                workPhone = value;
            }
        }

        public string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
            }
        }

        public string Email1
        {
            get
            {
                return email_1;
            }
            set
            {
                email_1 = value;
            }
        }

        public string Email2
        {
            get
            {
                return email_2;
            }
            set
            {
                email_2 = value;
            }
        }

        public string Email3
        {
            get
            {
                return email_3;
            }
            set
            {
                email_3 = value;
            }
        }

        public string BDay
        {
            get
            {
                return bday;
            }
            set
            {
                bday = value;
            }
        }

        public string ADay
        {
            get
            {
                return aday;
            }
            set
            {
                aday = value;
            }
        }

        public string BMonth
        {
            get
            {
                return bmonth;
            }
            set
            {
                bmonth = value;
            }
        }

        public string AMonth
        {
            get
            {
                return amonth;
            }
            set
            {
                amonth = value;
            }
        }

        public string BYear
        {
            get
            {
                return byear;
            }
            set
            {
                byear = value;
            }
        }

        public string AYear
        {
            get
            {
                return ayear;
            }
            set
            {
                ayear = value;
            }
        }

        public string SecondaryAddress
        {
            get
            {
                return secondaryAddress;
            }
            set
            {
                secondaryAddress = value;
            }
        }

        public string SecondaryPhone
        {
            get
            {
                return secondaryPhone;
            }
            set
            {
                secondaryPhone = value;
            }
        }

        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }
    }
}
