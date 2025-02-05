using sicf_DataBase.Repositories.Cita;
using sicf_Models.Core;
using sicf_Models.Dto.Cita;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Cita
{
    public class CitaHandler: ICitaHandler
    {
        private readonly ICitaRepository _citaRepository;

		public CitaHandler(ICitaRepository citaRepository) {

			_citaRepository = citaRepository;

		}
        public ResponseListaPaginada GetDepartamentos() {

			//// se realiza insercion en bitacora
			try
			{
				return _citaRepository.ObtenerDepertamentos();
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

		public ResponseListaPaginada GetCiudadesMunicipios(int depID)
		{

			//// se realiza insercion en bitacora
			try
			{
				return _citaRepository.ObtenerCiudadesMunicipios(depID);
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

		public ResponseListaPaginada GetComisarias(int ciudmunID)
		{
			// se realiza insercion en bitacora
			try
			{
				return _citaRepository.ObtenerComisarias(ciudmunID);
			}
			catch (ControledException ex)
			{
				throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
			}
			catch (Exception ex)
			{
				throw new ControledException(ex.HResult);
			}
		}

		public ResponseListaPaginada ReservarObtenerDisponibilidadCita(long idCita) {

			//// se realiza insercion en bitacora
			try
			{
				return _citaRepository.ReservarObtenerDisponibilidadCita(idCita);
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

        public ResponseListaPaginada ObtenerCitaComisaria(int id_comisaria)
        {

            //// se realiza insercion en bitacora
            try
            {
                return _citaRepository.ObtenerCitaComisaria(id_comisaria);
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


        public ResponseListaPaginada CrearCita(RequestCitaDto requestCitaDto)
		{
			
			//// se realiza insercion en bitacora
			try
			{
				return _citaRepository.CrearCita(requestCitaDto);
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

		public ResponseListaPaginada AtenderCita(long idCiudadano)
		{

			//// se realiza insercion en bitacora
			try
			{
				return _citaRepository.AtenderCita(idCiudadano);
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

		#region Validaciones.
		public ResponseListaPaginada ValidarCita(RequestCitaDto requestCitaDto)
		{
			try
			{
				ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();	

				List<string> errors = new List<string>();

				if (requestCitaDto.idComisaria == 0)
					errors.Add("IdComisaria, campo obligatorio.");

				if (requestCitaDto.tipoAtencionList.Count == 0)
					errors.Add("Almenos un tipo de vilencia debe ser diligenciado, campo obligatorio.");

				if (string.IsNullOrWhiteSpace(requestCitaDto.nombCiudadano))
					errors.Add("Nombre ciudadano, campo obligatorio.");

				if (string.IsNullOrWhiteSpace(requestCitaDto.primerApellido))
					errors.Add("Primer apellido, campo obligatorio.");

				if (string.IsNullOrWhiteSpace(requestCitaDto.direccResidencia))
					errors.Add("Dirección de Residencia, campo obligatorio.");

				if (requestCitaDto.tipoDocumento == 0)
					errors.Add("Tipo Documento, campo obligatorio.");

				if (string.IsNullOrWhiteSpace(requestCitaDto.numeroDocumento))
					errors.Add("Número Documento, campo obligatorio.");

				if (string.IsNullOrWhiteSpace(requestCitaDto.celular)   && string.IsNullOrWhiteSpace(requestCitaDto.correoElectronico) && string.IsNullOrWhiteSpace(requestCitaDto.telf))
					errors.Add("Al menos uno de los siguientes datos: Correo electrónico, teléfono o celular, debe ser diligenciado!!!.");

				responseListaPaginada.DatosPaginados = errors;
				responseListaPaginada.TotalRegistros = errors.Count;

				return responseListaPaginada;
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
		#endregion


		#region agregarcita

		public async Task<ControledResponseDTO> GuardarCita(CrearCita data , int comisaria)
        {
			try
			{
				return await _citaRepository.GuardarCita(data,comisaria);
			}
			catch (Exception ex) 
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<CitaDisponibleDTO>> ConsultarCita(int comisaria) {

			return await _citaRepository.ConsultarCita(comisaria);


		}

		public async Task ActualizarEstadoCita(long idCita, bool activo)
        {
			try {
				await _citaRepository.ActualizarEstadoCita(idCita , activo);
			}
			catch (Exception ex) {

				throw new Exception(ex.Message);
			}
		
		}

		#endregion agregarcita


	}
}
