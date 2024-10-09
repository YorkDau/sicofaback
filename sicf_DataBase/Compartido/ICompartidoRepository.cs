using sicf_Models.Dto.Compartido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Compartido
{
    public interface ICompartidoRepository
    {

        //Eliminado public List<TipoDocumentoDto> ObtenerTipoDocuemntos();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PaisDto> ObtenerPais();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PaisDto> ObtenerPais(int TipoId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DepartamentoDto> ObtenerDepartamento(int idPais);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depID"></param>
        /// <returns></returns>
        public List<CiudadMunicipioDto> ObtenerCiudadesMunicipios(long idDep);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LocalidadDto> ObtenerLocalidades(long ciudMunID);//ObtenerSexos()
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<SexoGeneroOrientacionSexual> ObtenerSexoGeneroOrientacionSexual(string tipo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tipo_Dominio"></param>
        /// <returns></returns>
        public List<DominioDto> ObtenerDominio(string Tipo_Dominio);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EstadoSolicitudDto> ObtenerEstadoSolicitud();

        //TODO: lo comentado se debe eliminar cuando  TODO en TareaController se verifique que funciono OK: Joel VIlA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ///Task<List<ResponseCasosPendienteAtencion>> ObtenerCasosPendienteAtencionAsync(RequestCasosPendienteDeAtencion request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idtarea"></param>
        /// <returns></returns>
        public long ProvisionalTarea(long idtarea);

        //TODO: lo comentado se debe eliminar cuando  TODO en TareaController se verifique que funciono OK: Joel VIlA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tareaID"></param>
        /// <returns></returns>
        ///public int? ConsutarRiesgo(long tareaID);
        ///
        public Task<bool> GuardarInvolucrado(InvolucradoDTO involucrado);

        public Task<List<InvolucradoInfoListaDTO>> ListarInvolucradosComplementariaInfo(long IdSolicitudServicio);

        public Task<bool> ActualizarInvolucradoComplementaria(InvolucradoDTO involucrado);

        public Task<FuncionarioDTO> ObtenerDatosFuncionarioPorTarea(long idtarea);
    }
}
