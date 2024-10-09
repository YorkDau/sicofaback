using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class correoNotifacionDTO
    {
        public string senderName { get; set; }
        public string subject { get; set; }
        public string mailTo { get; set; }
        public string htmlContent { get; set; }
        public correoNotifacionDTO()
        {
            this.senderName = "";
            this.subject = "";
            this.mailTo = "";
            this.htmlContent = "";
        }
    }
}
