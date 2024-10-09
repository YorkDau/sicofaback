using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Remision
{
    public class RequestRecomdSegRedApoyoEntExtDto
    {
        public long SolicitudID { get; set; }
        public bool SegCheckbox1 { get; set; }
        public bool SegCheckbox2 { get; set; }
        public bool SegCheckbox3 { get; set; }
        public bool SegCheckbox4 { get; set; }
        public bool SegCheckbox5 { get; set; }
        public bool SegCheckbox6 { get; set; }
        public bool SegCheckbox7 { get; set; }
        public bool SegCheckbox8 { get; set; }
        public bool SegCheckbox9 { get; set; }
        public bool RedApoyCheckbox1 { get; set; }
        public bool RedApoyCheckbox2 { get; set; }
        public bool ConfirmoFirma { get; set; }

        public RequestRecomdSegRedApoyoEntExtDto() {

            SegCheckbox1 = false;
            SegCheckbox2 = false;
            SegCheckbox3 = false;
            SegCheckbox4 = false;
            SegCheckbox5 = false;
            SegCheckbox6 = false;
            SegCheckbox7 = false;
            SegCheckbox8 = false;
            SegCheckbox9 = false;

            RedApoyCheckbox1 = false;
            RedApoyCheckbox2 = false;

            ConfirmoFirma = false;
        }
    }
}
