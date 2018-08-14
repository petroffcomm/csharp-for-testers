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


        public static List<GroupData> GetAllRecordsFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
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
    }
}
