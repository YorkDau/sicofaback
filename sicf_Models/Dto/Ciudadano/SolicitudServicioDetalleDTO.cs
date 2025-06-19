namespace sicf_Models.Dto.Ciudadano
{
    public class SolicitudServicioDetalleDTO
    {
        public string codigo_solicitud { get; set; }
        public string nombre_ciudaddano { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public DateTime hora_solicitud { get; set; }
        public DateTime fecha_hecho_violento { get; set; }
        public string descripcion_de_hechos { get; set; }
        public bool es_victima { get; set; }
        public string? id_tipo_entidad { get; set; }
        public int numero_victimas { get; set; }
    }
}
