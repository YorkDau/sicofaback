

namespace sicf_Models.Dto.ReporteSolicitud
{
    [Serializable]
    public class RequestReporteSolicitudDTO
    {
        public string? numeroDocumento { get; set; }
        public string? codigoTipoDocumento { get; set; }
        public string? codigoSolicitud { get; set; }
        public string? codigoTipoViolencia { get; set; }
        public DateTime? fechaSolicitudDesde { get; set; }
        public DateTime? fechaSolicitudHasta { get; set; }
        public long? id_comisaria { get; set; }

        /// nuevos parametros para la consulta del reporte de solictudes
      
        public string? NombreCompletoVictima { get; set; }
        public string? NombreCompletoVictimario { get; set; }
        public string? SexoVictima { get; set; }
        public string? IdentidadGeneroVictima { get; set; }    
        public DateTime? FechaHechoViolento { get; set; }
        public DateTime? HoraHechoViolento { get; set; }




    }
}
