using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_white
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public string Name { get; set; }


        public GroupData()
        {

        }

        public int CompareTo(GroupData other)
        {
            return this.Name.CompareTo(other.Name);
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

            return Name == other.Name;
         }
    }
}
