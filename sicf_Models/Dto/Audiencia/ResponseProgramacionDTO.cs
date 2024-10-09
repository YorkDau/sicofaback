namespace sicf_Models.Dto.Audiencia
{
    public class ResponseProgramacionDTO
    {
        public long? idProgramacion { get; set; }
        public long? idSolicitud { get; set; }
        public long? idTarea { get; set; }
        public long? idTarea_uso { get; set; }
        public string? etiqueta { get; set; }
        public string? razon { get; set; }
        public DateTime? fecha { get; set; }
        public string? estado { get; set; }
    }
}
