namespace sicf_Models.Dto.Compartido
{
    public class InvolucradoDTO
    {
        public long IdSolicitudServicio { get; set; }
        public long IdInvolucrado { get; set; }
        public string? NumeroDocumento { get; set; }
        public int? TipoDocumento { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public int? Edad { get; set; }
        public int? EdadEn { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public bool? EsVictima { get; set; }
        public bool? EsPrincipal { get; set; }
        public bool? EsRepresentante { get; set; }
        //public long? IdLugarExpedicion { get; set; }
        public int? PaisExp { get; set; }
        public int? DepartamentoExp { get; set; }
        public int? MunicipioExp { get; set; }
        public string? Eps { get; set; }
        public InvolucradoComplementariaDTO? InfoAdicional { get; set; }
    }
}
