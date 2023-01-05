using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class SellingHistory // історія продажів
    {
        internal Stack<SellingModel> History { get; private set; }

        public SellingHistory()
        {
            History = new Stack<SellingModel>();
        }
    }
}
