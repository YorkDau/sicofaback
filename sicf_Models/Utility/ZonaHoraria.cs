using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace sicf_Models.Utility
{
    public class ZonaHoraria
    {
        public static DateTime ConvertirAHoraSistema(DateTime fecha)
        {
            var cstTimeZoneInfo = TZConvert.GetTimeZoneInfo(Constants.Constants.FormatoHora.ZonaHorariaColombiaWindows);
            return TimeZoneInfo.ConvertTimeFromUtc(fecha, cstTimeZoneInfo);           
        }

    }
}
