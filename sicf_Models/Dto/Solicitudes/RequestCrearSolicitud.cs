namespace sicf_Models.Dto.Solicitudes
{
    public class RequestCrearSolicitud
    {
        public long idCiudadano { get; set; }
        public long idComisaria { get; set; }
        public string fechaSolicitud { get; set; }
        public string horaSolicitud { get; set; }
        public string fechaHechoViolento { get; set; }
        public string descripcionHechos { get; set; }
        public bool esVictima { get; set; }

        public string? adjunto { get; set; } = string.Empty;
        public string? archivoTraslado { get; set; } = string.Empty;
        public string? id_tipo_entidad { get; set; }
        public bool conviveConAgresor { get; set; }
        public int relacionParentescoAgresor{ get; set; }
        public bool esCompetenciaComisaria { get; set; }
        public string noCompetenciaDescripcion { get; set; }
        public int idtipoTramite { get; set; }
        public int idContextofamiliar { get; set; }
        public bool esNecesarioRemitir { get; set; }
        public long? idComisariaRemision{ get; set; }
        public long idEntidadExterna{ get; set; }
        public string justificacionRemision { get; set; }
        public int idUsuarioSistema { get; set; }
        public string tipoSolicitud { get; set; }
        public string SexoAfectado { get; set; }


        public RequestCrearSolicitud() {

            fechaSolicitud = string.Empty;
            horaSolicitud = string.Empty;
            fechaHechoViolento = string.Empty;
            descripcionHechos = string.Empty;
            noCompetenciaDescripcion = string.Empty;
            esVictima = false;
            conviveConAgresor = false;
            esCompetenciaComisaria = false;
            esNecesarioRemitir = false;
            justificacionRemision = string.Empty;
            tipoSolicitud = string.Empty;
            SexoAfectado = string.Empty;
        }
    }

    public class RequestActualizarSolicitud : RequestCrearSolicitud
    {
        public long idSolicitud { get; set; }
    }
}
