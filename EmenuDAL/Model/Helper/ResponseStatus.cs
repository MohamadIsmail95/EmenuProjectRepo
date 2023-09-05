using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Helper
{
    public class ResponseStatus
    {
        public enum ApiReturnCode
        {
            success = 0,
            fail = 1,
        }
    }
}
