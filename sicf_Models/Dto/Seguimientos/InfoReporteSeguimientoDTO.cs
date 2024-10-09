namespace sicf_Models.Dto.Seguimientos
{
    public class InfoReporteSeguimientoDTO
    {
        public string? nombreVictima { get; set; }
        public string? tipoDocuVictima { get; set; }
        public string? numeroDocumentoVictima { get; set; }
        public string? lugarExpedicionVictima { get; set; }
        public string? numeroTelVictima { get; set; }
        public string? correoVictima { get; set; }
        public string? direccionVictima { get; set; }
        public string? nombreComisario { get; set; }
        public string? ciudadRemision { get; set; }
        public string? estado { get; set; }
        public int? edadVictima { get; set; }

    }

    public class RemisionesAsociada
    {
        public string? nombreInvolucrado { get; set; }
        public string? nombreUsuario { get; set; }
        public string? nombreRemision { get; set; }
        public string? fecha { get; set; }
        public long idAnexo { get; set; }
        public string? estado { get; set; }

    }

    public class RemisionDisponiblesDTO
    {
        public int idRemision { get; set; }
        public string? nombre { get; set; }
    }
}
