using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.PerfilPermisos
{

    public class ActividadesDTO {

        public int IdActividad {get; set;}

        public string NombreTarea { get; set; } = string.Empty;

    }
    public class PerfilActividadesDTO
    {

        public int IdPerfil {get; set;}
        public string nombrePerfil { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty;

        public List<Tuple<int, string>> actividades;

    }

    public class PerfilActividadEdicionDTO
    { 
        public int IdActividad { get; set; } = 0;

        public string nombreActividad { get; set; } = string.Empty;

        public bool activo { get; set; } = false;
    
    }

    public class PerfilEdicionDTO 
    { 
        public int IdPerfil { get; set; }

        public string nombrePerfil {get ; set; } = string.Empty;

        public string codigo { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;   

        public IEnumerable<PerfilActividadEdicionDTO> Actividades { get; set; } = new List<PerfilActividadEdicionDTO>();
    
    }
}
