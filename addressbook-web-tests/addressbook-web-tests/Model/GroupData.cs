using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : BaseDataObj, IEquatable<GroupData>, IComparable<GroupData>
    {
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "group_name")]
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; }

        [Column(Name = "group_footer")]
        public string Footer { get; set; }


        // Empty constructor by XML Serialization library
        public GroupData()
        {
        }


        public GroupData(string name)
        {
            this.Name = name;
            this.Header = "";
            this.Footer = "";
        }


        public GroupData(string name, string header, string footer)
        {
            this.Name = name;
            this.Header = header;
            this.Footer = footer;
        }


        public bool Equals(GroupData other)
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

            return ids_are_equal && Name == other.Name;
        }


        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            /**int result = comparisonWithCheckForNULL(Id, other.Id);
            if (result != 0)
                return result;**/

            int result = comparisonWithCheckForNULL(Name, other.Name);
            if (result != 0)
                return result;

            result = comparisonWithCheckForNULL(Id, other.Id);
            if (result != 0)
                return result;

            return 0;
        }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }


        public override string ToString()
        {
            return String.Format("Id = {0}; name = {1}; header = {2}; footer = {3}", Id, Name, Header, Footer);
        }


        public static List<GroupData> GetAllRecordsFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }


        public List<ContactData> GetContacts()
        {
            List<ContactData> contacts = new List<ContactData>();

            using (AddressBookDB db = new AddressBookDB())
            {
                contacts = (from c in db.Contacts
                            from r in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)
                            select c).ToList();
                // add Distinct() before ToList() when duplicate records appear
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


        public static GroupData GetNonEmptyGroup()
        {
            List<GroupData> groups = new List<GroupData>();

            using (AddressBookDB db = new AddressBookDB())
            {
                groups = (from g in db.Groups select g).ToList();

                if (groups.Count() == 0)
                    // Nothing to process further, so we return 'null' right here
                    return null;
            }

            foreach (GroupData group in groups)
            {
                if (group.GetContacts().Count() > 0)
                    return group;
            }

            // if no proper object were found return 'null'
            return null;
        }
    }
}
