using sicf_Models.Core;

namespace sicf_Models.Dto.Seguimientos
{
    public class MedidaSeguimientoDTO
    {
        public long? idseguimientoMedidas { get; set; } 
        public long? idMedida { get; set; }
        public string? EstadoMedida { get; set; }
        public DateTime? prorroga { get; set; }
        public string? justificacionProrroga { get; set; }
        public string? nomMedida { get; set; }
        public string? textoMedida { get; set; }
        public int? tipoMedida { get; set; }
        public long? idAnexoProrroga { get; set; }
        public string? nombreAnexoProrroga { get; set; }

}

    public class MedidasSeguimientoDTO
    {

        List<MedidaSeguimientoDTO> medidas { get; set; }

        public MedidasSeguimientoDTO(List<MedidaSeguimientoDTO> medidas)
        {
            this.medidas = medidas;
        }
    }

    public class responseMedidasSeguimiento
    {
        public long idSeguimiento { get; set; }
        public long idSolicitudServicio { get; set; }
        public long? idProgramacion { get; set; }    
        public string? comentario { get; set; }
        public long? idTareaInstrumentos { get; set; }
        public int? usuarioModifica { get; set; }

        public List<MedidaSeguimientoDTO> medidasDeProteccion { get; set; }
        public List<MedidaSeguimientoDTO> medidasDeProteccionEntidad { get; set; }
        public List<MedidaSeguimientoDTO> medidasDeAtencion { get; set; }
        public List<MedidaSeguimientoDTO> medidasDeEstabilizacion { get; set; }

        public responseMedidasSeguimiento()
        {
            medidasDeProteccion = new List<MedidaSeguimientoDTO>();
            medidasDeProteccionEntidad = new List<MedidaSeguimientoDTO>();
            medidasDeAtencion = new List<MedidaSeguimientoDTO>();
            medidasDeEstabilizacion = new List<MedidaSeguimientoDTO>();
        }
    }

    public class responseMedidasSeguimientoPard
    {
        public long idSeguimiento { get; set; }
        public long idSolicitudServicio { get; set; }
        public long? idProgramacion { get; set; }
        public string? comentario { get; set; }
        public long? idTareaInstrumentos { get; set; }
        public int? usuarioModifica { get; set; }

        public List<MedidaSeguimientoDTO> medidas { get; set; }


        public responseMedidasSeguimientoPard()
        {
            medidas = new List<MedidaSeguimientoDTO>();

        }
    }

}
