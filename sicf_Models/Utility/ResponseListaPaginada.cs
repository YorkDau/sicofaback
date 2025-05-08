using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Utility
{
    public class ResponseListaPaginada
    {
        public object? DatosPaginados { set; get; }

        public int TotalRegistros { set; get; }
        public string Message { set; get; }

    }
}
