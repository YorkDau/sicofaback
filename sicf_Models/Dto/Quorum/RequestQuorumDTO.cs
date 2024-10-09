namespace sicf_Models.Dto.Quorum
{
    public class RequestQuorumDTO
    {
        public RequestQuorumDTO(
                                long IdSolicitudServicio,
                                long IdInvolucrado,
                                long IdAnexo,
                                int IdEstado,
                                long IdTarea,
                                string Entrada,
                                string TipoDocumento,
                                long IdQuorum)
        {
            this.IdSolicitudServicio = IdSolicitudServicio;
            this.IdInvolucrado = IdInvolucrado;
            this.IdAnexo = IdAnexo;
            this.IdEstado = IdEstado;
            this.IdTarea = IdTarea;
            this.Entrada = Entrada;
            this.TipoDocumento = TipoDocumento;
            this.IdUsuario = IdUsuario;
            this.IdQuorum = IdQuorum;
        }

            public long IdSolicitudServicio { get; set; }
            public long IdInvolucrado { get; set; }
            public long? IdAnexo { get; set; }
            public int IdEstado { get; set; }
            public long IdTarea { get; set; }
            public string Entrada { get; set; }
            public string TipoDocumento { get; set; }
            public int IdUsuario { get; set; }
            public long IdQuorum { get; set; }
    }
}
