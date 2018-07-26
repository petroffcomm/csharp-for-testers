using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class BaseDataObj
    {
        protected int comparisonWithCheckForNULL(Object obj1, Object obj2)
        {
            String param1 = (String)obj1;
            String param2 = (String)obj2;

            int result = 0;
            if ((obj1 == null) && (obj2 == null))
            {
                result = 0;
            }
            else if ((obj1 == null) && (obj2 != null) && (param2 != ""))
            {
                result = 1;
            }
            else if ((obj2 == null) && (obj1 != null) && (param1 != ""))
            {
                result = -1;
            }
            else if ((obj1 != null) && (obj2 != null))
            {
                result = param1.CompareTo(param2);
            }
            return result;
        }
    }
}
