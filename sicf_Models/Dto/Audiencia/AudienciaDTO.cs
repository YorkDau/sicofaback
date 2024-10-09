namespace sicf_Models.Dto.Audiencia
{
    public class AudienciaDTO
    {
        public long IdProgramacion { get; set; }
        public long IdSolicitud { get; set; }
        public long IdTarea { get; set; }
        public long? IdTareaUso { get; set; }
        public string Etiqueta { get; set; } = null!;
        public string Razon { get; set; } = null!;
        public string FechaHoraInicial { get; set; }
        public string FechaHoraFinal { get; set; }
        public string Estado { get; set; } = null!;

    }
}
