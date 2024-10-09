using sicf_Models.Dto.Compartido;


namespace sicf_BusinessHandlers.BusinessHandlers.Compartido
{
    public interface ICompartidoHandler
    {
        
        //Eliminado public List<TipoDocumentoDto> GetTipoDocumento();
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PaisDto> GetPais();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PaisDto> GetPais(int id_tipo_documento);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DepartamentoDto> GetDepartamento(int idPais);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depID"></param>
        /// <returns></returns>
        public List<CiudadMunicipioDto> GetCiudadesMunicipios(long depID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CiudadMunicipioDto> GetLugarExpedicion();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ciudMunID"></param>
        /// <returns></returns>
        public List<LocalidadDto> GetLocalidades(long ciudMunID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<SexoGeneroOrientacionSexual> GetSexoGeneroOrientacionSexual(string tipo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tipo_Dominio"></param>
        /// <returns></returns>
        public List<DominioDto> GetDominio(string Tipo_Dominio);

        /// <summary>
        /// Retornar tabla EstadoSolicitud
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<EstadoSolicitudDto> GetEstadoSolicitud();

        public Task<bool> GuardarInvolucrado(InvolucradoDTO involucrado);

        public Task<List<InvolucradoInfoListaDTO>> ListarInvolucradosComplementariaInfo(long IdSolicitudServicio);

        public Task<bool> ActualizarInvolucradoComplementaria(InvolucradoDTO involucrado);

        public Task<FuncionarioDTO> ObtenerDatosFuncionarioPorTarea(long idtarea);

        //TODO: lo comentado se debe eliminar cuando  TODO en TareaController se verifique que funciono OK: Joel VIlA

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<IEnumerable<ResponseCasosPendienteAtencion>> GetCasosPendienteDeAtencionAsync(RequestCasosPendienteDeAtencion request);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public List<string> ValidarCasosPendienteDeAtencion(RequestCasosPendienteDeAtencion request);

    }
}
