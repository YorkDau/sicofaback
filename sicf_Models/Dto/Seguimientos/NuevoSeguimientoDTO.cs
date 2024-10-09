namespace sicf_Models.Dto.Seguimientos
{
    public class NuevoSeguimientoDTO
    {
        public NuevoSeguimientoDTO(
                                string TipoDocumento,
                                string NumeroDocumento,
                                string Perfil,
                                string CodigoSolicitud,
                                string CodUsuario)
        {
            this.TipoDocumento = TipoDocumento;
            this.NumeroDocumento = NumeroDocumento;
            this.Perfil = Perfil;
            this.CodigoSolicitud = CodigoSolicitud;
            this.CodUsuario = CodUsuario;
        }

        public string TipoDocumento { get; set; }
        public string  NumeroDocumento { get; set; }
        public string Perfil { get; set; }
        public string CodigoSolicitud { get; set; }
        public string CodUsuario { get; set; }
    }
}
