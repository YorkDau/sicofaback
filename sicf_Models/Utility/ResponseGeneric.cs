using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Utility
{
    public class ResponseGeneric
    {
        public object? Data { set; get; }
        public bool Success { set; get; }
        public string Message { set; get; }
        public string Status { set; get; }

        public ResponseGeneric() {

            Message = "";
            Status = "";
        }
    }
}
