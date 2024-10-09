namespace sicf_Models.Dto.Quorum
{
    public class RequestActualizarQuorumDTO
    {
        public RequestActualizarQuorumDTO(
                                            long IdQuorum,
                                            int IdEstado,
                                            long IdAnexo)
        {
            this.IdQuorum = IdQuorum;
            this.IdEstado = IdEstado;
            this.IdAnexo = IdAnexo;
        }

            public long IdQuorum { get; set; }
            public int IdEstado { get; set; }
        public long IdAnexo { get; set; }

    }
}
