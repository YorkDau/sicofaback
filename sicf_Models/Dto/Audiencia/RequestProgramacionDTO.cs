using sicf_Models.Dto.Tarea;

namespace sicf_Models.Dto.Audiencia
{
    public class RequestProgramacionDTO
    {
        public RequestProgramacionDTO(long idSolicitud, long idTarea, string etiqueta, string razon, string fechaI, string fechaF, string estado, int usuarioModifica, bool? generaEtiqueta, string? nombreEtiqueta, bool? valorEtiqueta)
        {
            this.idSolicitud = idSolicitud;
            this.idTarea = idTarea;
            this.etiqueta = etiqueta;
            this.razon = razon;
            this.fechaInicial = fechaI;
            this.fechaFinal = fechaF;
            this.estado = estado;
            this.usuarioModifica = usuarioModifica;

        }

        public long idSolicitud { get; set; }
        public long idTarea { get; set; }
        public long? idTarea_uso { get; set; }
        public string etiqueta { get; set; }
        public string razon { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
        public string estado { get; set; }
        public int usuarioModifica { get; set; } 

    }
}
