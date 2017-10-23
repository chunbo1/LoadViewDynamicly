using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadViewDynamicly
{
    public class SwitchViewMessage
    {
        public string ViewName { get; set; }
    }

    public class ProductSelectionChangedMessage
    {
        public string MsgValue { get; set; }
    }

    public class ProductClearedMessage
    {
        public string MsgValue { get; set; }
    }
    
}//NS
