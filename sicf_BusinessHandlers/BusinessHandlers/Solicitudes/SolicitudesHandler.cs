using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sicf_BusinessHandlers.BusinessHandlers.Archivos;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.Apelacion;
using sicf_DataBase.Repositories.SolicitudesRepository;
using sicf_Models.Constants;
using sicf_Models.Dto.Apelacion;
using sicf_Models.Dto.Archivos;
using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;

namespace sicf_BusinessHandlers.BusinessHandlers.Solicitudes
{
    public class SolicitudesHandler:ISolicitudesHandler
    {
        private readonly ISolicitudesRepository _solicitudesRepository;
		private readonly ITareaHandler _tareaHandler;
		private readonly IArchivoService _archivoService;

		public SolicitudesHandler(ISolicitudesRepository solicitudesRepository, ITareaHandler tareaHandler, IArchivoService archivoService)
        {
            _solicitudesRepository = solicitudesRepository;
			_tareaHandler = tareaHandler;
			_archivoService = archivoService;
        }

		#region Joel Vila Bringuez

		public ResponseListaPaginada GetSolicitudes(RequestSolicitudDto requestSolicitudDto)
		{
			//// se realiza insercion en bitacora

			try
			{
				return _solicitudesRepository.ObtenerSolicitudes(requestSolicitudDto);
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

		public ResponseListaPaginada GetCiudadanos(RequestCiudadano requestCiudadano)
		{
			//// se realiza insercion en bitacora

			try
			{
				return _solicitudesRepository.ObtenerCiudadanos(requestCiudadano);
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


		public ResponseListaPaginada RegistrarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano)
		{
			//// se realiza insercion en bitacora

			try
			{
				ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

				responseListaPaginada  = _solicitudesRepository.RegistrarCiudadano(requestRegistrarCiudadano);

				return responseListaPaginada;
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

		public ResponseListaPaginada EditarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano)
		{
			//// se realiza insercion en bitacora

			try
			{
				ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

				responseListaPaginada = _solicitudesRepository.EditarCiudadano(requestRegistrarCiudadano);

				return responseListaPaginada;
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

		public bool ConsultarNumeroDocumentoCiudadano(string numeroDocumento, int idtipoDocumento)
		{
			//// se realiza insercion en bitacora

			try
			{
				return _solicitudesRepository.ConsultarNumeroDocumentoCiudadano(numeroDocumento, idtipoDocumento);
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

		public string GetNumeroSolicitud(long idComisaria)
		{
			//// se realiza insercion en bitacora
			try
			{
				return _solicitudesRepository.ObtenerNumeroSolicitud(idComisaria);
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

		public async Task<long> CrearSolicitudCiudadano(RequestCrearSolicitud requestCrearSolicitud)
		{
			//// se realiza insercion en bitacora
			try
			{
				string codSolicitud = _solicitudesRepository.ObtenerNumeroSolicitud(requestCrearSolicitud.idComisaria);
				
				long idSolicitud = _solicitudesRepository.CrearSolicitudCiudadano(requestCrearSolicitud, codSolicitud);

				if (requestCrearSolicitud.adjunto is { Length: > 0 })
				{
					var archivo = new CargaArchivoDTO
					{
						idSolicitudServicio = idSolicitud,
						entrada = requestCrearSolicitud.adjunto,
						tipoDocumento = "Archivo_Tipo_Entidad",
					};
					await _archivoService.Carga(archivo);
				}

				if (!requestCrearSolicitud.esCompetenciaComisaria || (requestCrearSolicitud.esCompetenciaComisaria && requestCrearSolicitud.esNecesarioRemitir)) {

					RequestRemisionSolicitud data = new RequestRemisionSolicitud();
					data.id_solicitud_servicio = idSolicitud;
					data.justificacion = requestCrearSolicitud.justificacionRemision;
					data.id_comisaria_origen = requestCrearSolicitud.idComisaria;
					data.id_comisaria_destino = requestCrearSolicitud.idComisariaRemision;
					data.id_entidad_externa = requestCrearSolicitud.idEntidadExterna;
					data.idUsuarioSistema = requestCrearSolicitud.idUsuarioSistema;

					if (requestCrearSolicitud.idComisariaRemision != 0)
						data.tipo_remision = true;

					if (requestCrearSolicitud.idEntidadExterna != 0)
						data.tipo_remision = false;

					_solicitudesRepository.RegistroRemisionSolicitud(data);

				}

				_solicitudesRepository.RegistroInvolucradoPrincipalAproceso(requestCrearSolicitud.idCiudadano, idSolicitud);

				return idSolicitud;

				//_solicitudesRepository.RegistroInvolucrado(); integracion con miguel
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

		public async Task<long> ActualizarSolicitudCiudadano(RequestActualizarSolicitud requestActualizarSolicitud)
		{
			//// se realiza insercion en bitacora
			try
			{

				long idSolicitud = _solicitudesRepository.ActualizarSolicitudCiudadano(requestActualizarSolicitud);

				if (requestActualizarSolicitud.adjunto is { Length: > 0 })
				{
					var archivo = new CargaArchivoDTO
					{
						idSolicitudServicio = idSolicitud,
						entrada = requestActualizarSolicitud.adjunto,
						tipoDocumento = "Archivo_Tipo_Entidad",
					};
					await _archivoService.Carga(archivo);
				}

				if (!requestActualizarSolicitud.esCompetenciaComisaria || (requestActualizarSolicitud.esCompetenciaComisaria && requestActualizarSolicitud.esNecesarioRemitir))
				{

					RequestRemisionSolicitud data = new RequestRemisionSolicitud();
					data.id_solicitud_servicio = idSolicitud;
					data.justificacion = requestActualizarSolicitud.justificacionRemision;
					data.id_comisaria_origen = requestActualizarSolicitud.idComisaria;
					data.id_comisaria_destino = requestActualizarSolicitud.idComisariaRemision;
					data.id_entidad_externa = requestActualizarSolicitud.idEntidadExterna;
					data.idUsuarioSistema = requestActualizarSolicitud.idUsuarioSistema;

					if (requestActualizarSolicitud.idComisariaRemision != 0)
						data.tipo_remision = true;

					if (requestActualizarSolicitud.idEntidadExterna != 0)
						data.tipo_remision = false;

					_solicitudesRepository.RegistroRemisionSolicitud(data);

				}

				return idSolicitud;

				//_solicitudesRepository.RegistroInvolucrado(); integracion con miguel
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

		public ResponseEditarCiudadano CargarDatosCiudadanoEditar(long idCiudadano)
		{
			//// se realiza insercion en bitacora

			try
			{
				ResponseEditarCiudadano responseEditarCiudadano = new ResponseEditarCiudadano();


				responseEditarCiudadano = _solicitudesRepository.CargarDatosCiudadanoEditar(idCiudadano);


				string nombres = responseEditarCiudadano.primerNombre;

				nombres = nombres.TrimStart(' ');

				string primernombre = nombres;
				string segundonombre = "";

				if (nombres.Contains(" "))
				{
					primernombre = nombres.Split(' ')[0];
					segundonombre = nombres.Substring(nombres.IndexOf(' '));
					segundonombre = segundonombre.TrimStart(' ');
				}

				responseEditarCiudadano.primerNombre = primernombre;
				responseEditarCiudadano.segundoNombre = segundonombre;

				responseEditarCiudadano.direccionResidencia.Trim();

				return responseEditarCiudadano;

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

		#region Validaciones
		public ResponseListaPaginada ValidarSolicitudes(RequestSolicitudDto requestSolicitudDto)
		{
			try
			{
				ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

				List<string> errors = new List<string>();

				    //Al menos un lemento tiene valor
				if (!string.IsNullOrWhiteSpace(requestSolicitudDto.nombCiudadano) || !string.IsNullOrWhiteSpace(requestSolicitudDto.primerApellido)
					|| !string.IsNullOrWhiteSpace(requestSolicitudDto.numeroDocumento)
					|| !string.IsNullOrWhiteSpace(requestSolicitudDto.codigoCita) || !string.IsNullOrWhiteSpace(requestSolicitudDto.fecha)) 
				{
					     //Si un atributo del nombre no es nullo o vacio
					if (!string.IsNullOrWhiteSpace(requestSolicitudDto.nombCiudadano) || !string.IsNullOrWhiteSpace(requestSolicitudDto.primerApellido))
					{
						//Si un atributo del nombre no es nullo o vacio y luego no se tiene el nombre completo
						if (!string.IsNullOrWhiteSpace(requestSolicitudDto.nombCiudadano) && !string.IsNullOrWhiteSpace(requestSolicitudDto.primerApellido))
						{
							try
							{
								if(!string.IsNullOrEmpty(requestSolicitudDto.fecha))
								DateTime.ParseExact(requestSolicitudDto.fecha, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

							}
							catch (FormatException ex)
							{
								//loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Error en conversión de fecha, formato no valido, fecha inicial debe ser una fecha valida: " + ex.Message, reporteCobroDto);
								//throw new ControledException(ExceptionCode.ErrorcantidadRegDateFormatException);
								throw ex;
							}
						}
						else
						  errors.Add("Para poder buscar por nombre y apellidos es necesario diligenciar el nombre completo!!!");
					}

				}

				responseListaPaginada.DatosPaginados = errors;
				responseListaPaginada.TotalRegistros = errors.Count; //Si errors.Count == 0 la validacion paso con exito (nota: tambien aplica para cuando todos los atributos del request sean nullos o vacio, ya que por regla de negocio trae las solicitudes del dia actual.)

				return responseListaPaginada;
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

		public ResponseListaPaginada ValidarCiudadano(RequestCiudadano requestCiudadano)
		{
			try
			{
				ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

				List<string> errors = new List<string>();

				if (string.IsNullOrWhiteSpace(requestCiudadano.nombre_ciudadano) && string.IsNullOrWhiteSpace(requestCiudadano.apellido_ciudadano) && string.IsNullOrWhiteSpace(requestCiudadano.numero_documento))
					errors.Add("Al menos Nombre y Apellido del Ciudadano o número de documento del ciudadano debe ser diligenciado");


				if (!string.IsNullOrWhiteSpace(requestCiudadano.nombre_ciudadano) && string.IsNullOrWhiteSpace(requestCiudadano.apellido_ciudadano))
					errors.Add("Por favor ingrese el apellido del ciudadano, una vez que se introduce el nombre, se debe introducir el apellido");

				if (string.IsNullOrWhiteSpace(requestCiudadano.nombre_ciudadano) && !string.IsNullOrWhiteSpace(requestCiudadano.apellido_ciudadano))
					errors.Add("Por favor ingrese el nombre del ciudadano, una vez que se introduce el apellido, se debe introducir  el nombre");

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

        public ResponseListaPaginada ValidarRegistrarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano)
        {
            try
            {
                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

                List<string> errors = new List<string>();

				if (string.IsNullOrWhiteSpace(requestRegistrarCiudadano.primerNombre))
                    errors.Add("Primer nombre debe ser diligenciado, campo obligatorio");


				if (string.IsNullOrWhiteSpace(requestRegistrarCiudadano.primerApellido))
                    errors.Add("Primer apellido debe ser diligenciado, campo obligatorio");


                if (requestRegistrarCiudadano.idTipoDocumento == 0)
                    errors.Add("Tipo de documento debe ser diligenciado, campo obligatorio");

                if (string.IsNullOrWhiteSpace(requestRegistrarCiudadano.numeroDocumento))
                    errors.Add("Numero de documento debe ser diligenciado, campo obligatorio");

                if (requestRegistrarCiudadano.edad == 0)
                    errors.Add("Edad debe ser diligenciado, campo obligatorio");

                //if (requestRegistrarCiudadano.idNivelAcademico == 0)
                  //  errors.Add("Nivel académico debe ser diligenciado, campo obligatorio");

				try
				{
					if (!string.IsNullOrEmpty(requestRegistrarCiudadano.fechaExpedicion))
						DateTime.ParseExact(requestRegistrarCiudadano.fechaExpedicion, Constants.FormatoFecha, CultureInfo.InvariantCulture);

					if (!string.IsNullOrEmpty(requestRegistrarCiudadano.fechaNacimiento))
						DateTime.ParseExact(requestRegistrarCiudadano.fechaNacimiento, Constants.FormatoFecha, CultureInfo.InvariantCulture);

				}
				catch (FormatException ex)
				{
					//loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Error en conversión de fecha, formato no valido, fecha inicial debe ser una fecha valida: " + ex.Message, reporteCobroDto);
					//throw new ControledException(ExceptionCode.ErrorcantidadRegDateFormatException);

					throw new Exception("Error la fecha no tiene un formato valido");
				}

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


        #region miguel moreno 


        public ResponseListaPaginada ObtenerCiudadano(int id)
		{

			return _solicitudesRepository.ObtenerCiudadano(id);
		}

		public async Task<ResponseListaPaginada> RegistroInvolucrado(long id,List<RequestDatosInvolucrado> data)
		{
			try
			{

				ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

				foreach (var involucrado in data)
				{

					var tuple = _solicitudesRepository.RegistroInvolucrado(involucrado);

					_solicitudesRepository.RegistroServicioInvolucrado(id, tuple.Item2);

				}

				/*Se genera la tarea*/
				long idtask = await this._tareaHandler.IniciarProceso(id, Constants.CodigoProceso.GeneracionCaso);

				responseListaPaginada.DatosPaginados = Constants.Message.registroExito;

				return responseListaPaginada;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int id, int idComisaria)
        {
			try {

				return _solicitudesRepository.ObtenerSolicitudServiciosCiudadano(id, idComisaria);
			}
            catch (Exception e)
            {

				throw new Exception(e.Message);
			}

        }
        

        public ResponseListaPaginada ConsultarSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud)
        {
            try
            {
                return _solicitudesRepository.ConsultarSolicitudesGeneralesPorFiltros(solicitud);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }

        }
        public ResponseListaPaginada ConsultarPreSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud)
        {
            try
            {
                return _solicitudesRepository.ConsultarPreSolicitudesGeneralesPorFiltros(solicitud);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }

        }

        public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int idComisaria)
        {
            try
            {

                return _solicitudesRepository.ObtenerSolicitudServiciosCiudadano(idComisaria);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public SolicitudServicioDetalleDTO ObtenerSolicitudServiciosCiudadanoDetalle(int id) 
		{

			try {
				SolicitudServicioDetalleDTO response = _solicitudesRepository.ObtenerSolicitudServiciosCiudadanoDetalle(id);

				List<CantidadInvolucradosDTO> cantidad = _solicitudesRepository.cantidadInvolucradosPorServicio(id);

				response.numero_victimas = cantidad.Any(c => c.tipo_victima == true) ? cantidad.Where(s => s.tipo_victima == true).First().cantidad : 0;

				return response;

			}
			catch (Exception e) {

				 throw new Exception(e.Message);
			}
		}

		public SolicitudServicioDatosDTO ObtenerDatosSolicitud(int idSolicitud)
		{

			try
			{
				return _solicitudesRepository.ObtenerDatosSolicitud(idSolicitud);

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
		}

		public List<ComisariaDTO> ComisariasPorMunicipio(int id)
        {
			try {
				List<ComisariaDTO> response = _solicitudesRepository.ComisariasPorMunicipio(id);

				return response;
			}
			catch (Exception e) {

				throw new Exception(e.Message);
			}
		
		}

		public List<ComisariaDTO> ObtenerComisariasTraslado(int idComisariaActual)
		{
			try
			{
				List<ComisariaDTO> response = _solicitudesRepository.ObtenerComisariasTraslado(idComisariaActual);

				return response;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}

		}

		public string RegistroRemisionSolicitud(RequestRemisionSolicitud data)
        {
			try {
				int validate =_solicitudesRepository.ConsultaRemisionExistentePorSolicitud(data.id_solicitud_servicio);

				if (validate == 0)
				{

					if (data.tipo_remision && data.id_entidad_externa == 0)
					{

						_solicitudesRepository.RegistroRemisionSolicitud(data);

					}
					else if (data.tipo_remision == false && data.id_comisaria_destino == 0)
					{

						_solicitudesRepository.RegistroRemisionSolicitud(data);
					}

				}
				else {

					throw new Exception("Ya se hizo un registro de remision de solicitud");
				}




				return "Creado";
			}
			catch (Exception e) 
			{

				throw new Exception(e.Message);
			}
		}

		public List<EntidadExterna> ObtenerEntidadExterna()
        {
			try {

				var response =_solicitudesRepository.ObtenerEntidadExterna();

				

				return response;

			}
			catch (Exception e) {


				throw new Exception(e.Message);
			}
		
		}


		public ResponseListaPaginada ObtenerQuestionarioViolencia(int id_tipo_violencia) 
		{
			var response=_solicitudesRepository.ObtenerQuestionarioViolencia(id_tipo_violencia);

			ResponseListaPaginada response1 = new ResponseListaPaginada();
			response1.DatosPaginados = response;
			response1.TotalRegistros = response.Count;

			return response1;
		}

		public ResponseListaPaginada SexoOrientacionGenero(string tipo) {

			var response = _solicitudesRepository.SexoOrientacionGenero(tipo);
			ResponseListaPaginada response1 = new ResponseListaPaginada();
			response1.DatosPaginados = response;
			response1.TotalRegistros = response.Count;

			return response1;
		}


		public List<TipoDiscapacidadDTO> ObtenerTipoDiscapacidad() {

			List<TipoDiscapacidadDTO> response = _solicitudesRepository.ObtenerTipoDiscapacidad();
			

			return response;
		}

		public ResponseListaPaginada ObtenerNivelAcademico() 
		{
			var response = _solicitudesRepository.ObtenerNivelAcademico();
			ResponseListaPaginada response1 = new ResponseListaPaginada();
			response1.DatosPaginados = response;
			response1.TotalRegistros = response.Count;

			return response1;

		}

		public ResponseListaPaginada ObtenerTIpoRelacion() {


			var response = _solicitudesRepository.ObtenerTIpoRelacion();
			ResponseListaPaginada response1 = new ResponseListaPaginada();
			response1.DatosPaginados = response;
			response1.TotalRegistros = response.Count;

			return response1;
		}


		public ResponseListaPaginada RegistrarRespuestaQuestionario(RespuestaQuestionarioDTO questinario) 
		{
			int contador = 0;
			try
			{
				 contador = _solicitudesRepository.ContadorPreguntasPorTipoViolencia(questinario.id_tipo_violencia);

				if (contador == questinario.listado_respuestas.Count)
				{

					foreach (var x in questinario.listado_respuestas) {

						_solicitudesRepository.RegistroRespuestasQuestionario(questinario.id_solicitud_servicio, x);
					}

					ResponseListaPaginada response1 = new ResponseListaPaginada();
					response1.DatosPaginados = "creados";
					response1.TotalRegistros = 1;

					return response1;

				}
				else
				{
					throw new Exception($"Cantidad insuficiente de respuetas , deben ser :{contador}");

				}
			}
			catch (Exception ex) {

				throw new Exception(ex.Message);

			}
		
		}

		public RequestDatosInvolucradoPrincipal1 ConsultaInvolucradoPrincipal(int id_ciudadano) 
		{

			try
			{

				var response = _solicitudesRepository.ConsultaInvolucradoPrincipal(id_ciudadano);

				return response;

			}
			catch (Exception ex) {

				throw new Exception(ex.Message);
			}



		}


		#endregion

		#endregion

		public string ObtenerNumeroSolicitud(long idComisaria)
		{
			try
			{
				var response = _solicitudesRepository.ObtenerNumeroSolicitud(idComisaria);
				return response;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<SolicitudGeneralDTO> ConsultaGeneralSolicitud(long idSolicitudServicio)
		{
			try
			{
				Task<SolicitudGeneralDTO> solicitud = _solicitudesRepository.ConsultaGeneralSolicitud(idSolicitudServicio);
				return solicitud;
			}
			catch (Exception ex)
			{
				throw new ControledException(ex.HResult);
			}
		}

        #region Diana Ariza

        public List<RemisionSolicitudServicioComisariaAnteriorDTO> ObtenerRemisionSolicitudServicio(int idSolicitudServicio)
        {
            try
            {
                List<RemisionSolicitudServicioComisariaAnteriorDTO> response = _solicitudesRepository.ObtenerRemisionSolicitudServicio(idSolicitudServicio);

                return response;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        #endregion Diana Ariza

    }
}
