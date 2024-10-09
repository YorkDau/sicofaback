using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class RequestAsignarTarea
    {
        /*TODO: No se puede usar Id por seguridad
         userID: Usar el Token de JWT.
        user id en el Front colocarlo en el header*/
        public int userID { get; set; }
        public long tareaID { get; set; }
        public string? perfilCod { get; set; }
        public string? valorEtiqueta { get; set; }
    }

    public class RequestAsignarTareaJuez : RequestAsignarTarea 
    {
        public long idSolicitudServicio { get; set; }
    }
}
