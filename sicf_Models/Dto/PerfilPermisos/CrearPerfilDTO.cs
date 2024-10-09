using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.PerfilPermisos
{
    public class CrearPerfilDTO
    {

        public string nombrePerfil { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty;

        public List<int> Actividades { get; set; } = new List<int>();

        public bool Estado {  get; set; }   
    }

    public class EditarPerfilDTO : CrearPerfilDTO
    { 
        public int idPerfil { get; set; }
    }
}
