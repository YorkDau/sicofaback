using sicf_Models.Dto.Compartido;
using sicf_DataBase.Compartido;
using sicfExceptions.Exceptions;
using AutoMapper;
using System.Globalization;
using sicf_Models.Constants;
using sicf_Models.Dto.Archivos;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.Compartido
{
    public class CompartidoHandler: ICompartidoHandler
    {
        readonly private ICompartidoRepository _compartidoRepository;
        public CompartidoHandler(ICompartidoRepository compartidoRepository) {

            _compartidoRepository = compartidoRepository;


        }

        // Eliminado public List<TipoDocumentoDto> GetTipoDocumento()

        public List<PaisDto> GetPais()
        {

            try
            {
                return _compartidoRepository.ObtenerPais();
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<PaisDto> GetPais(int id_tipo_documento)
        {

            try
            {
                return _compartidoRepository.ObtenerPais(id_tipo_documento);
            }
            catch (ControledException ex)
            {
                //TODO: Auditoria de errores
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //TODO: Auditoria de errores
                throw new ControledException(ex.HResult);
            }
        }

        public List<DepartamentoDto> GetDepartamento(int idPais)
        {

            try
            {
                return _compartidoRepository.ObtenerDepartamento(idPais);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<CiudadMunicipioDto> GetCiudadesMunicipios(long depID)
        {
            try
            {
                return _compartidoRepository.ObtenerCiudadesMunicipios(depID);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<CiudadMunicipioDto> GetLugarExpedicion()
        {

            try
            {
                return _compartidoRepository.ObtenerCiudadesMunicipios(0);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<LocalidadDto> GetLocalidades(long ciudMunID)
        {
            try
            {
                return _compartidoRepository.ObtenerLocalidades(ciudMunID);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<SexoGeneroOrientacionSexual> GetSexoGeneroOrientacionSexual(string tipo)
        {
            try
            {
                return _compartidoRepository.ObtenerSexoGeneroOrientacionSexual(tipo);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        /// <summary>
        /// GetDominio
        /// </summary>Rafael Marquez Servicio queconsulta la tabla de Dominio
        /// <returns>string</returns>
        public List<DominioDto> GetDominio(string Tipo_Dominio)
        {
            try
            {
                return _compartidoRepository.ObtenerDominio(Tipo_Dominio);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        /// <summary>
        /// GetEstadoSolicitud
        /// </summary>Rafael Marquez Servicio que consulta la tabla EstadoSolicitud
        /// <returns>string</returns>
        public List<EstadoSolicitudDto> GetEstadoSolicitud()
        {
            try
            {
                return _compartidoRepository.ObtenerEstadoSolicitud();
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<bool> GuardarInvolucrado(InvolucradoDTO involucrado)
        {
            bool response = false;
            try
            {
                response = await this._compartidoRepository.GuardarInvolucrado(involucrado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        public async Task<List<InvolucradoInfoListaDTO>> ListarInvolucradosComplementariaInfo(long IdSolicitudServicio)
        {            
            try
            {
                var ListaInvolucrados = await this._compartidoRepository.ListarInvolucradosComplementariaInfo(IdSolicitudServicio);
                return ListaInvolucrados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarInvolucradoComplementaria(InvolucradoDTO involucrado)
        {
            bool response = false;
            try
            {
                response = await this._compartidoRepository.ActualizarInvolucradoComplementaria(involucrado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        public async Task<FuncionarioDTO> ObtenerDatosFuncionarioPorTarea(long idtarea)
        {
            try
            {
                var funcionario = await this._compartidoRepository.ObtenerDatosFuncionarioPorTarea(idtarea);
                return funcionario;
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        }

        //TODO: lo comentado se debe eliminar cuando  TODO en TareaController se verifique que funciono OK: Joel VIlA

        //public async Task<IEnumerable<ResponseCasosPendienteAtencion>> GetCasosPendienteDeAtencionAsync(RequestCasosPendienteDeAtencion request)
        //{
        //    try
        //    {
        //        IEnumerable<ResponseCasosPendienteAtencion> casosPendienteAtencionList = await _compartidoRepository.ObtenerCasosPendienteAtencionAsync(request); ;

        //        foreach (var item in casosPendienteAtencionList)
        //            item.riesgo = this._compartidoRepository.ConsutarRiesgo(item.idTarea);

        //        return casosPendienteAtencionList;
        //    }
        //    catch (ControledException ex)
        //    {
        //        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
        //        throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
        //    }
        //    catch (Exception ex)
        //    {
        //        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
        //        throw new ControledException(ex.HResult);
        //    }
        //}

        //public  List<string> ValidarCasosPendienteDeAtencion(RequestCasosPendienteDeAtencion request)
        //{
        //    try
        //    {
        //        List<string> errors = new List<string>();

        //        //if (!string.IsNullOrEmpty(request.nombres) || !string.IsNullOrEmpty(request.primerApellido) || !string.IsNullOrEmpty(request.segundoApellido))
        //        //    if (!(!string.IsNullOrEmpty(request.nombres) && !string.IsNullOrEmpty(request.primerApellido) && !string.IsNullOrEmpty(request.segundoApellido)))
        //        //        errors.Add(Constants.Message.NombresApellidosRequeridos);

        //        try
        //        {
        //            if (!string.IsNullOrEmpty(request.fecha))
        //                DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);

        //        }
        //        catch (FormatException)
        //        {
        //            //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Error en conversión de fecha, formato no valido, fecha inicial debe ser una fecha valida: " + ex.Message, reporteCobroDto);
        //            errors.Add(Constants.Message.FechaNOValida);
        //        }

        //        return errors;
        //    }
        //    catch (ControledException ex)
        //    {
        //        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
        //        throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
        //    }
        //    catch (Exception ex)
        //    {
        //        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
        //        throw new ControledException(ex.HResult);
        //    }
        //}
    }
}
