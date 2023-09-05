using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Filter
{
    public class Filter
    {
        public string key { get; set; }
        public List<string> values { get; set; }

    }

    public class FilterObjectList
    {
      
        public string key { get; set; }
        public List<object> value { get; set; }

        public FilterObjectList()
        { }
        public FilterObjectList(string key, List<object> value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
