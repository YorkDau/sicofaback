using sicf_Models.Dto.Archivos;

namespace sicf_Models.Dto.PruebasPard
{
    public class PruebasPardDTO
    {
        public long IdSolicitudServicio { get; set; }
        public string? nombreMedida { get; set; }
        public int TipoMedida { get; set; }
        public int IdMedida { get; set; }
        public string? Estado { get; set; }
        public long? IdAnexoPard { get; set; }
        public string? nombreArchivo { get; set; }
    }

    public class PruebasPardAnexoDTO
    {
        public int IdMedida { get; set; }
        public long idAnexoServicio { get; set; }
        public CargaArchivoDTO archivoDTO { get; set; }
    }

    public class PruebasDecretoPardDTO
    {
        public long IdSolicitudServicio { get; set; }
        public string? nombreMedida { get; set; }
        public int IdMedida { get; set; }
    }

    public class PruebasDecretoAgregarDTO
    {
        public long idSolicitudServicio { get; set; }
        public long idTarea { get; set; }
        public int idMedida { get; set; }
        public int tipoMedida { get; set; }
        public string tipoDecreto { get; set; }

        public PruebasDecretoAgregarDTO()
        {
            this.tipoDecreto = String.Empty;
        }
    }

    public class PruebasDecretoConsultarDTO
    {
        public long idSolicitudServicio { get; set; }
        public long? idMedida { get; set; }
        public long? idSolicitudServicioAnexo { get; set; }
        public long? idAnexo { get; set; }
        public string nombrePrueba { get; set; }
        public string nombreArchivo { get; set; }   


        public PruebasDecretoConsultarDTO()
        {
            this.nombrePrueba = string.Empty;
            this.nombreArchivo = string.Empty;
        }
    }
}
