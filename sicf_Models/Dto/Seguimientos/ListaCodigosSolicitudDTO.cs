namespace sicf_Models.Dto.Seguimientos
{
    public class ListaCodigosSolicitudDTO
    {
        public ListaCodigosSolicitudDTO(
                                long IdSolicitud,
                                string CodigoSolicitud,
                                string NombreCompleto)
        {
            this.IdSolicitud = IdSolicitud;
            this.CodigoSolicitud = CodigoSolicitud;
            this.NombreCompleto = NombreCompleto;
        }

        public long IdSolicitud { get; set; }
        public string CodigoSolicitud { get; set; }
        public string NombreCompleto { get; set; }
    }
}
