using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using sicf_DataBase.BDConnection;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using sicfExceptions.Exceptions;
using System.Collections;
using sicf_Models.Dto.Ciudadano;
using sicf_DataBase.Compartido;
using System.Numerics;
using static sicf_Models.Constants.Constants;
using sicf_DataBase.Data;
using sicf_Models.Core;
using Microsoft.EntityFrameworkCore;
using sicf_Models.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace sicf_DataBase.Repositories.SolicitudesRepository
{
    public class SolicitudesRepository : BdConnection, ISolicitudesRepository
    {
        public ResponseListaPaginada responseListaPaginada { get; set; }
        private readonly ICompartidoRepository _compartidoRepository;
        private readonly SICOFAContext _context;

        private IConfiguration? configuration { get; set; }

        public SolicitudesRepository(IConfiguration configuration, ICompartidoRepository compartidoRepository, SICOFAContext context) : base(configuration)
        {
            responseListaPaginada = new ResponseListaPaginada();
            _compartidoRepository = compartidoRepository;
            _context = context;
        }

        #region Joel Vila Bringuez
        /// <summary>
        /// Se sugiere cambiar nombre a ObtenerCitas
        /// </summary>
        /// <param name="requestSolicitudDto"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public ResponseListaPaginada ObtenerSolicitudes(RequestSolicitudDto requestSolicitudDto)
        {
            try
            {
                List<ResponseSolicitudesDto> solicitudesList;

                string? mensaje = "";
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_CITAS";
                    using (_command = new SqlCommand(query))
                    {

                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@nombre_ciudadano", BdValidation.ToDBNull((string.IsNullOrEmpty(requestSolicitudDto.nombCiudadano)) ? null : requestSolicitudDto.nombCiudadano));
                        _command.Parameters.AddWithValue("@primer_apellido", BdValidation.ToDBNull((string.IsNullOrEmpty(requestSolicitudDto.primerApellido)) ? null : requestSolicitudDto.primerApellido));
                        _command.Parameters.AddWithValue("@segundo_apellido", BdValidation.ToDBNull((string.IsNullOrEmpty(requestSolicitudDto.segundoApellido)) ? null : requestSolicitudDto.segundoApellido));
                        _command.Parameters.AddWithValue("@numero_documento", BdValidation.ToDBNull((string.IsNullOrEmpty(requestSolicitudDto.numeroDocumento)) ? null : requestSolicitudDto.numeroDocumento));
                        _command.Parameters.AddWithValue("@codigoCita", BdValidation.ToDBNull((string.IsNullOrEmpty(requestSolicitudDto.codigoCita)) ? null : requestSolicitudDto.codigoCita));
                        _command.Parameters.AddWithValue("@fecha", BdValidation.ToDBNull((string.IsNullOrEmpty(requestSolicitudDto.fecha)) ? null : DateTime.ParseExact(requestSolicitudDto.fecha, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        _command.Parameters.AddWithValue("@estadoCita", BdValidation.ToDBNull((requestSolicitudDto.estadoCita == default) ? null : requestSolicitudDto.estadoCita));
                        _command.Parameters.AddWithValue("@idComisaria", BdValidation.ToDBNull((requestSolicitudDto.idComisaria == default) ? null : requestSolicitudDto.idComisaria));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        solicitudesList = new List<ResponseSolicitudesDto>();


                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1)
                                {
                                    ResponseSolicitudesDto responseSolicitudesDto = new ResponseSolicitudesDto();

                                    responseSolicitudesDto.idCita = ConvertFDBVal.ConvertFromDBVal<long>(reader["idCita"]);
                                    responseSolicitudesDto.idCiudadano = ConvertFDBVal.ConvertFromDBVal<long>(reader["idCiudadano"]);//pendiente idCiudadano en PR
                                    responseSolicitudesDto.nombres = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombres"]);
                                    responseSolicitudesDto.apellidos = ConvertFDBVal.ConvertFromDBVal<string>(reader["apellidos"]);
                                    responseSolicitudesDto.numeroDocumento = ConvertFDBVal.ConvertFromDBVal<string>(reader["numeroDocumento"]);
                                    responseSolicitudesDto.horaCita = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["horaCita"]).ToString("hh:mm tt");
                                    responseSolicitudesDto.fechaCita = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fechaCita"]).ToShortDateString();
                                    responseSolicitudesDto.origenCita = ConvertFDBVal.ConvertFromDBVal<string>(reader["origenCita"]); //Origen cita 
                                    responseSolicitudesDto.estado = ConvertFDBVal.ConvertFromDBVal<string>(reader["Estado"]);

                                    solicitudesList.Add(responseSolicitudesDto);

                                }
                                else
                                {
                                    mensaje = ConvertFDBVal.ConvertFromDBVal<string>(reader["Mensaje"]);
                                }
                            }

                        _connectionDb.Close();
                    }
                }

                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

                if (mensaje != "")
                    responseListaPaginada.DatosPaginados = mensaje;
                else
                {

                    if (solicitudesList != null)
                    {
                        responseListaPaginada.DatosPaginados = solicitudesList;

                        responseListaPaginada.TotalRegistros = solicitudesList.Count;
                    }
                }

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

        public ResponseListaPaginada ObtenerCiudadanos(RequestCiudadano requestCiudadano)
        {
            try
            {
                List<ResponseCiudadanoDto> ciudadanosList;

                string? mensaje = "";
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_CIUDADANOS";
                    using (_command = new SqlCommand(query))
                    {

                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@nombre_ciudadano", BdValidation.ToDBNull((!string.IsNullOrEmpty(requestCiudadano.nombre_ciudadano)) ? requestCiudadano.nombre_ciudadano.Trim() : requestCiudadano.nombre_ciudadano));
                        _command.Parameters.AddWithValue("@apellido_ciudadano", BdValidation.ToDBNull((!string.IsNullOrEmpty(requestCiudadano.apellido_ciudadano)) ? requestCiudadano.apellido_ciudadano.Trim() : requestCiudadano.apellido_ciudadano));
                        _command.Parameters.AddWithValue("@numero_documento", BdValidation.ToDBNull(requestCiudadano.numero_documento));

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();


                        ciudadanosList = new List<ResponseCiudadanoDto>();


                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1)
                                {

                                    ResponseCiudadanoDto responseCiudadano = new ResponseCiudadanoDto();

                                    responseCiudadano.idCiudadano = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Ciudadano"]);
                                    responseCiudadano.nombres = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombres"]);
                                    responseCiudadano.apellidos = ConvertFDBVal.ConvertFromDBVal<string>(reader["Apellidos"]);
                                    responseCiudadano.tipo_documento = ConvertFDBVal.ConvertFromDBVal<string>(reader["Tipo Documento"]);
                                    responseCiudadano.numero_documento = ConvertFDBVal.ConvertFromDBVal<string>(reader["Numero Documento"]);
                                    responseCiudadano.numero_solicitudes = ConvertFDBVal.ConvertFromDBVal<int>(reader["Numero Solicitudes"]);
                                    responseCiudadano.fecha_ult_solicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["Ultima Solicitud"]);


                                    ciudadanosList.Add(responseCiudadano);

                                }
                                else
                                {
                                    mensaje = ConvertFDBVal.ConvertFromDBVal<string>(reader["Mensaje"]);
                                }
                            }

                        _connectionDb.Close();
                    }
                }

                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

                if (mensaje != "")
                    responseListaPaginada.DatosPaginados = mensaje;
                else
                {
                    if (ciudadanosList != null)
                    {
                        responseListaPaginada.DatosPaginados = ciudadanosList;
                        responseListaPaginada.TotalRegistros = ciudadanosList.Count;
                    }
                }

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

        public ResponseListaPaginada RegistrarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano)
        {
            try
            {
                ResponseCrearCiudadano responseCrearCiudadano = new ResponseCrearCiudadano();
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_REGISTRAR_CIUDADANO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;

                        _command.Parameters.AddWithValue("@nombreCiudadano", BdValidation.ToDBNull((!string.IsNullOrEmpty(requestRegistrarCiudadano.segundoNombre)) ? requestRegistrarCiudadano.primerNombre.Trim() + " " + requestRegistrarCiudadano.segundoNombre.Trim() : requestRegistrarCiudadano.primerNombre.Trim()));
                        _command.Parameters.AddWithValue("@primerApellidoCiudadano", BdValidation.ToDBNull((!string.IsNullOrEmpty(requestRegistrarCiudadano.primerApellido)) ? requestRegistrarCiudadano.primerApellido.Trim() : requestRegistrarCiudadano.primerApellido));
                        _command.Parameters.AddWithValue("@segundoApellidoCiudadano", BdValidation.ToDBNull((!string.IsNullOrEmpty(requestRegistrarCiudadano.segundoApellido)) ? requestRegistrarCiudadano.segundoApellido.Trim() : requestRegistrarCiudadano.segundoApellido));
                        _command.Parameters.AddWithValue("@idTipoDocumento", BdValidation.ToDBNull(requestRegistrarCiudadano.idTipoDocumento));
                        _command.Parameters.AddWithValue("@numeroDocumento", BdValidation.ToDBNull(requestRegistrarCiudadano.numeroDocumento));
                        _command.Parameters.AddWithValue("@fechaExpedicion", BdValidation.ToDBNull((string.IsNullOrEmpty(requestRegistrarCiudadano.fechaExpedicion)) ? null : DateTime.ParseExact(requestRegistrarCiudadano.fechaExpedicion, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        //_command.Parameters.AddWithValue("@idlugarExpedicion", BdValidation.ToDBNull(requestRegistrarCiudadano.idlugarExpedicion));
                        _command.Parameters.AddWithValue("@fechaNacimiento", BdValidation.ToDBNull((string.IsNullOrEmpty(requestRegistrarCiudadano.fechaNacimiento)) ? null : DateTime.ParseExact(requestRegistrarCiudadano.fechaNacimiento, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture))); ;
                        _command.Parameters.AddWithValue("@edad", BdValidation.ToDBNull(requestRegistrarCiudadano.edad));
                        /*
                         *  @idPaisExpedicion int,
                            @idDepartamentoExpedicion bigint,
                            @idMunicipioExpedicion bigint,
                         * */
                        _command.Parameters.AddWithValue("@idPaisExpedicion", BdValidation.ToDBNull((requestRegistrarCiudadano.idPaisExpedicion == 0) ? null : requestRegistrarCiudadano.idPaisExpedicion));
                        _command.Parameters.AddWithValue("@idDepartamentoExpedicion", BdValidation.ToDBNull((requestRegistrarCiudadano.idDepartamentoExpedicion == 0) ? null : requestRegistrarCiudadano.idDepartamentoExpedicion));
                        _command.Parameters.AddWithValue("@idMunicipioExpedicion", BdValidation.ToDBNull((requestRegistrarCiudadano.idMunicipioExpedicion == 0) ? null : requestRegistrarCiudadano.idMunicipioExpedicion));
                        _command.Parameters.AddWithValue("@idPaisNacimiento", BdValidation.ToDBNull((requestRegistrarCiudadano.idPaisNacimiento == 0) ? null : requestRegistrarCiudadano.idPaisNacimiento));
                        _command.Parameters.AddWithValue("@idDepartamentoNacimiento", BdValidation.ToDBNull((requestRegistrarCiudadano.idDepartamentoNacimiento == 0) ? null : requestRegistrarCiudadano.idDepartamentoNacimiento));
                        _command.Parameters.AddWithValue("@idMunicipioNacimiento", BdValidation.ToDBNull((requestRegistrarCiudadano.idMunicipioNacimiento == 0) ? null : requestRegistrarCiudadano.idMunicipioNacimiento));
                        _command.Parameters.AddWithValue("@idSexo", BdValidation.ToDBNull((requestRegistrarCiudadano.idSexo == 0) ? null : requestRegistrarCiudadano.idSexo));
                        _command.Parameters.AddWithValue("@idIdentidadGenero", BdValidation.ToDBNull((requestRegistrarCiudadano.idIdentidadGenero == 0) ? null : requestRegistrarCiudadano.idIdentidadGenero));
                        _command.Parameters.AddWithValue("@idOrientacionSexual", BdValidation.ToDBNull((requestRegistrarCiudadano.idOrientacionSexual == 0) ? null : requestRegistrarCiudadano.idOrientacionSexual));
                        _command.Parameters.AddWithValue("@idNivelAcademico", BdValidation.ToDBNull((requestRegistrarCiudadano.idNivelAcademico == 0) ? null : requestRegistrarCiudadano.idNivelAcademico));
                        _command.Parameters.AddWithValue("@idEstadoAcademico", BdValidation.ToDBNull((requestRegistrarCiudadano.idEstadoAcademico == 0) ? null : requestRegistrarCiudadano.idEstadoAcademico)); //agregado
                        _command.Parameters.AddWithValue("@direccionResidencia", BdValidation.ToDBNull(requestRegistrarCiudadano.direccionResidencia));

                        _command.Parameters.AddWithValue("@idLocalidad", BdValidation.ToDBNull((requestRegistrarCiudadano.idLocalidad == 0) ? null : requestRegistrarCiudadano.idLocalidad));

                        _command.Parameters.AddWithValue("@barrio", BdValidation.ToDBNull(requestRegistrarCiudadano.barrio));
                        _command.Parameters.AddWithValue("@telefono", BdValidation.ToDBNull(requestRegistrarCiudadano.telefono));
                        _command.Parameters.AddWithValue("@celular", BdValidation.ToDBNull(requestRegistrarCiudadano.celular));
                        _command.Parameters.AddWithValue("@correoElectronico", BdValidation.ToDBNull(requestRegistrarCiudadano.correoElectronico));
                        _command.Parameters.AddWithValue("@idDiscapasidad", BdValidation.ToDBNull((requestRegistrarCiudadano.idDiscapasidad == 0) ? null : requestRegistrarCiudadano.idDiscapasidad));
                        _command.Parameters.AddWithValue("@estadoEmbarazo", BdValidation.ToDBNull(requestRegistrarCiudadano.estadoEmbarazo.estadoEmbarazo));
                        _command.Parameters.AddWithValue("@mesesEmbarazo", BdValidation.ToDBNull(requestRegistrarCiudadano.estadoEmbarazo.mesesEmbarazo));
                        _command.Parameters.AddWithValue("@estaAfiliado", BdValidation.ToDBNull(requestRegistrarCiudadano.afiliadoSeguridadSocial.estaAfiliado));
                        _command.Parameters.AddWithValue("@eps", BdValidation.ToDBNull(requestRegistrarCiudadano.afiliadoSeguridadSocial.eps));
                        _command.Parameters.AddWithValue("@ips", BdValidation.ToDBNull(requestRegistrarCiudadano.afiliadoSeguridadSocial.ips));

                        _command.Parameters.AddWithValue("@poblacionLgtbi", BdValidation.ToDBNull(requestRegistrarCiudadano.poblacionLgtbi));
                        _command.Parameters.AddWithValue("@adultoMayor", BdValidation.ToDBNull(requestRegistrarCiudadano.adultoMayor));
                        _command.Parameters.AddWithValue("@mujerEmbarazada", BdValidation.ToDBNull(requestRegistrarCiudadano.mujerEmbarazada));
                        _command.Parameters.AddWithValue("@ninoNinaAdolocente", BdValidation.ToDBNull(requestRegistrarCiudadano.ninoNinaAdolocente));
                        _command.Parameters.AddWithValue("@migrante", BdValidation.ToDBNull(requestRegistrarCiudadano.migrante));
                        _command.Parameters.AddWithValue("@victimaConflictoArmado", BdValidation.ToDBNull(requestRegistrarCiudadano.victimaConflictoArmado));
                        _command.Parameters.AddWithValue("@personasLideresDefensorasDH", BdValidation.ToDBNull(requestRegistrarCiudadano.personasLideresDefensorasDH));
                        _command.Parameters.AddWithValue("@personasHabitalidadCalle", BdValidation.ToDBNull(requestRegistrarCiudadano.personasHabitalidadCalle));
                        _command.Parameters.AddWithValue("@puebloIndigena", BdValidation.ToDBNull(requestRegistrarCiudadano.puebloIndigena));

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                responseCrearCiudadano.idCiudadano = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Ciudadano"]);
                                responseCrearCiudadano.nombres = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombres"]);
                                responseCrearCiudadano.apellidos = ConvertFDBVal.ConvertFromDBVal<string>(reader["Apellidos"]);
                            }

                        _connectionDb.Close();
                    }
                }

                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();
                responseListaPaginada.DatosPaginados = responseCrearCiudadano;

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
            try
            {
                ResponseCrearCiudadano responseCrearCiudadano = new ResponseCrearCiudadano();
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_EDITAR_CIUDADANO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idCiudadano", BdValidation.ToDBNull(requestRegistrarCiudadano.idCiudadano));
                        _command.Parameters.AddWithValue("@nombreCiudadano", BdValidation.ToDBNull(requestRegistrarCiudadano.primerNombre) + " " + BdValidation.ToDBNull(requestRegistrarCiudadano.segundoNombre));
                        _command.Parameters.AddWithValue("@primerApellidoCiudadano", BdValidation.ToDBNull(requestRegistrarCiudadano.primerApellido));
                        _command.Parameters.AddWithValue("@segundoApellidoCiudadano", BdValidation.ToDBNull(requestRegistrarCiudadano.segundoApellido));
                        _command.Parameters.AddWithValue("@idTipoDocumento", BdValidation.ToDBNull(requestRegistrarCiudadano.idTipoDocumento));
                        _command.Parameters.AddWithValue("@numeroDocumento", BdValidation.ToDBNull(requestRegistrarCiudadano.numeroDocumento));
                        _command.Parameters.AddWithValue("@fechaExpedicion", BdValidation.ToDBNull((string.IsNullOrEmpty(requestRegistrarCiudadano.fechaExpedicion)) ? null : DateTime.ParseExact(requestRegistrarCiudadano.fechaExpedicion, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        //_command.Parameters.AddWithValue("@idlugarExpedicion", BdValidation.ToDBNull(requestRegistrarCiudadano.idlugarExpedicion));
                        _command.Parameters.AddWithValue("@fechaNacimiento", BdValidation.ToDBNull((string.IsNullOrEmpty(requestRegistrarCiudadano.fechaNacimiento)) ? null : DateTime.ParseExact(requestRegistrarCiudadano.fechaNacimiento, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture))); ;
                        _command.Parameters.AddWithValue("@edad", BdValidation.ToDBNull(requestRegistrarCiudadano.edad));
                        /*
                         *  @idPaisExpedicion int,
                            @idDepartamentoExpedicion bigint,
                            @idMunicipioExpedicion bigint,
                         * */
                        _command.Parameters.AddWithValue("@idPaisExpedicion", BdValidation.ToDBNull((requestRegistrarCiudadano.idPaisExpedicion == 0) ? null : requestRegistrarCiudadano.idPaisExpedicion));
                        _command.Parameters.AddWithValue("@idDepartamentoExpedicion", BdValidation.ToDBNull((requestRegistrarCiudadano.idDepartamentoExpedicion == 0) ? null : requestRegistrarCiudadano.idDepartamentoExpedicion));
                        _command.Parameters.AddWithValue("@idMunicipioExpedicion", BdValidation.ToDBNull((requestRegistrarCiudadano.idMunicipioExpedicion == 0) ? null : requestRegistrarCiudadano.idMunicipioExpedicion));
                        _command.Parameters.AddWithValue("@idPaisNacimiento", BdValidation.ToDBNull((requestRegistrarCiudadano.idPaisNacimiento == 0) ? null : requestRegistrarCiudadano.idPaisNacimiento));
                        _command.Parameters.AddWithValue("@idDepartamentoNacimiento", BdValidation.ToDBNull((requestRegistrarCiudadano.idDepartamentoNacimiento == 0) ? null : requestRegistrarCiudadano.idDepartamentoNacimiento));
                        _command.Parameters.AddWithValue("@idMunicipioNacimiento", BdValidation.ToDBNull((requestRegistrarCiudadano.idMunicipioNacimiento == 0) ? null : requestRegistrarCiudadano.idMunicipioNacimiento));
                        _command.Parameters.AddWithValue("@idSexo", BdValidation.ToDBNull((requestRegistrarCiudadano.idSexo == 0) ? null : requestRegistrarCiudadano.idSexo));
                        _command.Parameters.AddWithValue("@idIdentidadGenero", BdValidation.ToDBNull((requestRegistrarCiudadano.idIdentidadGenero == 0) ? null : requestRegistrarCiudadano.idIdentidadGenero));
                        _command.Parameters.AddWithValue("@idOrientacionSexual", BdValidation.ToDBNull((requestRegistrarCiudadano.idOrientacionSexual == 0) ? null : requestRegistrarCiudadano.idOrientacionSexual));
                        _command.Parameters.AddWithValue("@idNivelAcademico", BdValidation.ToDBNull((requestRegistrarCiudadano.idNivelAcademico == 0) ? null : requestRegistrarCiudadano.idNivelAcademico));
                        _command.Parameters.AddWithValue("@idEstadoAcademico", BdValidation.ToDBNull((requestRegistrarCiudadano.idEstadoAcademico == 0) ? null : requestRegistrarCiudadano.idEstadoAcademico)); //agregado
                        _command.Parameters.AddWithValue("@direccionResidencia", BdValidation.ToDBNull(requestRegistrarCiudadano.direccionResidencia));

                        _command.Parameters.AddWithValue("@idLocalidad", BdValidation.ToDBNull((requestRegistrarCiudadano.idLocalidad == 0) ? null : requestRegistrarCiudadano.idLocalidad));

                        _command.Parameters.AddWithValue("@barrio", BdValidation.ToDBNull(requestRegistrarCiudadano.barrio));
                        _command.Parameters.AddWithValue("@telefono", BdValidation.ToDBNull(requestRegistrarCiudadano.telefono));
                        _command.Parameters.AddWithValue("@celular", BdValidation.ToDBNull(requestRegistrarCiudadano.celular));
                        _command.Parameters.AddWithValue("@correoElectronico", BdValidation.ToDBNull(requestRegistrarCiudadano.correoElectronico));
                        _command.Parameters.AddWithValue("@idDiscapasidad", BdValidation.ToDBNull((requestRegistrarCiudadano.idDiscapasidad == 0) ? null : requestRegistrarCiudadano.idDiscapasidad));
                        _command.Parameters.AddWithValue("@estadoEmbarazo", BdValidation.ToDBNull(requestRegistrarCiudadano.estadoEmbarazo.estadoEmbarazo));
                        _command.Parameters.AddWithValue("@mesesEmbarazo", BdValidation.ToDBNull(requestRegistrarCiudadano.estadoEmbarazo.mesesEmbarazo));
                        _command.Parameters.AddWithValue("@estaAfiliado", BdValidation.ToDBNull(requestRegistrarCiudadano.afiliadoSeguridadSocial.estaAfiliado));
                        _command.Parameters.AddWithValue("@eps", BdValidation.ToDBNull(requestRegistrarCiudadano.afiliadoSeguridadSocial.eps));
                        _command.Parameters.AddWithValue("@ips", BdValidation.ToDBNull(requestRegistrarCiudadano.afiliadoSeguridadSocial.ips));

                        _command.Parameters.AddWithValue("@poblacionLgtbi", BdValidation.ToDBNull(requestRegistrarCiudadano.poblacionLgtbi));
                        _command.Parameters.AddWithValue("@adultoMayor", BdValidation.ToDBNull(requestRegistrarCiudadano.adultoMayor));
                        _command.Parameters.AddWithValue("@mujerEmbarazada", BdValidation.ToDBNull(requestRegistrarCiudadano.mujerEmbarazada));
                        _command.Parameters.AddWithValue("@ninoNinaAdolocente", BdValidation.ToDBNull(requestRegistrarCiudadano.ninoNinaAdolocente));
                        _command.Parameters.AddWithValue("@migrante", BdValidation.ToDBNull(requestRegistrarCiudadano.migrante));
                        _command.Parameters.AddWithValue("@victimaConflictoArmado", BdValidation.ToDBNull(requestRegistrarCiudadano.victimaConflictoArmado));
                        _command.Parameters.AddWithValue("@personasLideresDefensorasDH", BdValidation.ToDBNull(requestRegistrarCiudadano.personasLideresDefensorasDH));
                        _command.Parameters.AddWithValue("@personasHabitalidadCalle", BdValidation.ToDBNull(requestRegistrarCiudadano.personasHabitalidadCalle));
                        _command.Parameters.AddWithValue("@puebloIndigena", BdValidation.ToDBNull(requestRegistrarCiudadano.puebloIndigena));

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                responseCrearCiudadano.idCiudadano = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Ciudadano"]);
                                responseCrearCiudadano.nombres = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombres"]);
                                responseCrearCiudadano.apellidos = ConvertFDBVal.ConvertFromDBVal<string>(reader["Apellidos"]);
                            }

                        _connectionDb.Close();
                    }
                }

                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();
                responseListaPaginada.DatosPaginados = responseCrearCiudadano;

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

        public bool ConsultarNumeroDocumentoCiudadano(string numeroDocuemnto, int idtipoDocumento)
        {
            try
            {
                int count = 0;
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_CONSULTAR_NUMERO_DOCUMENTO_CIUDADANO";
                    using (_command = new SqlCommand(query))
                    {

                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@numDoc", BdValidation.ToDBNull(numeroDocuemnto));
                        _command.Parameters.AddWithValue("@idtipoDoc", BdValidation.ToDBNull(idtipoDocumento));

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                                count = ConvertFDBVal.ConvertFromDBVal<int>(reader["Count"]);

                        _connectionDb.Close();

                        if (count > 0)
                            return true;
                        else
                            return false;
                    }
                }
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

        public string ObtenerNumeroSolicitud(long idComisaria)
        {
            try
            {
                string codSolicitud = "";
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_CODIGO_SOLICITUD";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@tipoConsecutivo", BdValidation.ToDBNull("SOLICITUDES"));
                        _command.Parameters.AddWithValue("@idComisaria", BdValidation.ToDBNull(idComisaria));


                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                                codSolicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["Codigo Solicitud"])!;

                        _connectionDb.Close();
                    }
                }

                return codSolicitud;
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

        public long CrearSolicitudCiudadano(RequestCrearSolicitud requestCrearSolicitud, string codSolicitud)
        {
            try
            {
                long idSolicitud = 0;
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_REGISTRAR_SOLICITUD_CIUDADANO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idCiudadano", BdValidation.ToDBNull(requestCrearSolicitud.idCiudadano));
                        _command.Parameters.AddWithValue("@idComisaria", BdValidation.ToDBNull(requestCrearSolicitud.idComisaria));

                        _command.Parameters.AddWithValue("@codigoSolicitud", BdValidation.ToDBNull(codSolicitud));
                        _command.Parameters.AddWithValue("@fechaSolicitud", BdValidation.ToDBNull(DateTime.ParseExact(requestCrearSolicitud.fechaSolicitud, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        _command.Parameters.AddWithValue("@horaSolicitud", BdValidation.ToDBNull(DateTime.ParseExact(requestCrearSolicitud.horaSolicitud, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        _command.Parameters.AddWithValue("@descripcionHechos", BdValidation.ToDBNull(requestCrearSolicitud.descripcionHechos));
                        _command.Parameters.AddWithValue("@esVictima", BdValidation.ToDBNull(requestCrearSolicitud.esVictima));


                        _command.Parameters.AddWithValue("@fechaHechoViolento", BdValidation.ToDBNull(DateTime.ParseExact(requestCrearSolicitud.fechaHechoViolento, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        _command.Parameters.AddWithValue("@conviveConAgresor", BdValidation.ToDBNull(requestCrearSolicitud.conviveConAgresor));
                        _command.Parameters.AddWithValue("@idtipoTramite", BdValidation.ToDBNull((requestCrearSolicitud.idtipoTramite == 0) ? null : requestCrearSolicitud.idtipoTramite));
                        _command.Parameters.AddWithValue("@idContextofamiliar", BdValidation.ToDBNull((requestCrearSolicitud.idContextofamiliar == 0) ? null : requestCrearSolicitud.idContextofamiliar));
                        _command.Parameters.AddWithValue("@noCompetenciaDescripcion", BdValidation.ToDBNull(requestCrearSolicitud.noCompetenciaDescripcion));

                        _command.Parameters.AddWithValue("@esCompetenciaComisaria", BdValidation.ToDBNull(requestCrearSolicitud.esCompetenciaComisaria));
                        _command.Parameters.AddWithValue("@esNecesarioRemitir", BdValidation.ToDBNull(requestCrearSolicitud.esNecesarioRemitir));
                        _command.Parameters.AddWithValue("@relacionParentescoAgresor", BdValidation.ToDBNull(requestCrearSolicitud.relacionParentescoAgresor));

                        _command.Parameters.AddWithValue("@justificacionRemision", BdValidation.ToDBNull(requestCrearSolicitud.justificacionRemision));
                        _command.Parameters.AddWithValue("@idUsuarioSistema", BdValidation.ToDBNull((requestCrearSolicitud.idUsuarioSistema == 0) ? null : requestCrearSolicitud.idUsuarioSistema));

                        _command.Connection = _connectionDb;

                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                                idSolicitud = ConvertFDBVal.ConvertFromDBVal<long>(reader["IdSolicitud"])!;

                        _connectionDb.Close();
                    }
                }

                return idSolicitud;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModificarSolicitud"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public long ActualizarSolicitudCiudadano(RequestActualizarSolicitud requestModificarSolicitud)
        {
            try
            {
                long idSolicitud = 0;
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_MODIFICAR_SOLICITUD_CIUDADANO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idSolicitud", BdValidation.ToDBNull(requestModificarSolicitud.idSolicitud));
                        _command.Parameters.AddWithValue("@idCiudadano", BdValidation.ToDBNull(requestModificarSolicitud.idCiudadano));
                        _command.Parameters.AddWithValue("@idComisaria", BdValidation.ToDBNull(requestModificarSolicitud.idComisaria));

                        _command.Parameters.AddWithValue("@fechaSolicitud", BdValidation.ToDBNull(DateTime.ParseExact(requestModificarSolicitud.fechaSolicitud, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        _command.Parameters.AddWithValue("@horaSolicitud", BdValidation.ToDBNull(DateTime.ParseExact(requestModificarSolicitud.horaSolicitud, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        _command.Parameters.AddWithValue("@descripcionHechos", BdValidation.ToDBNull(requestModificarSolicitud.descripcionHechos));
                        _command.Parameters.AddWithValue("@esVictima", BdValidation.ToDBNull(requestModificarSolicitud.esVictima));


                        _command.Parameters.AddWithValue("@fechaHechoViolento", BdValidation.ToDBNull(DateTime.ParseExact(requestModificarSolicitud.fechaHechoViolento, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));
                        _command.Parameters.AddWithValue("@conviveConAgresor", BdValidation.ToDBNull(requestModificarSolicitud.conviveConAgresor));
                        _command.Parameters.AddWithValue("@idtipoTramite", BdValidation.ToDBNull((requestModificarSolicitud.idtipoTramite == 0) ? null : requestModificarSolicitud.idtipoTramite));
                        _command.Parameters.AddWithValue("@idContextofamiliar", BdValidation.ToDBNull((requestModificarSolicitud.idContextofamiliar == 0) ? null : requestModificarSolicitud.idContextofamiliar));
                        _command.Parameters.AddWithValue("@noCompetenciaDescripcion", BdValidation.ToDBNull(requestModificarSolicitud.noCompetenciaDescripcion));

                        _command.Parameters.AddWithValue("@esCompetenciaComisaria", BdValidation.ToDBNull(requestModificarSolicitud.esCompetenciaComisaria));
                        _command.Parameters.AddWithValue("@esNecesarioRemitir", BdValidation.ToDBNull(requestModificarSolicitud.esNecesarioRemitir));
                        _command.Parameters.AddWithValue("@relacionParentescoAgresor", BdValidation.ToDBNull(requestModificarSolicitud.relacionParentescoAgresor));

                        _command.Parameters.AddWithValue("@justificacionRemision", BdValidation.ToDBNull(requestModificarSolicitud.justificacionRemision));
                        _command.Parameters.AddWithValue("@idUsuarioSistema", BdValidation.ToDBNull((requestModificarSolicitud.idUsuarioSistema == 0) ? null : requestModificarSolicitud.idUsuarioSistema));

                        _command.Connection = _connectionDb;

                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                                idSolicitud = ConvertFDBVal.ConvertFromDBVal<long>(reader["IdSolicitud"])!;

                        _connectionDb.Close();
                    }
                }

                return idSolicitud;
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
            try
            {
                ResponseEditarCiudadano responseEditar = new ResponseEditarCiudadano();
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_CARGAR_DATOS_CIUDADANO_EDITAR";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idCiudadano", BdValidation.ToDBNull(idCiudadano));

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();


                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                responseEditar.primerNombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombres"]);
                                responseEditar.primerApellido = ConvertFDBVal.ConvertFromDBVal<string>(reader["primerApellido"]);
                                responseEditar.segundoApellido = ConvertFDBVal.ConvertFromDBVal<string>(reader["segundoApellido"]);
                                responseEditar.idTipoDocumento = ConvertFDBVal.ConvertFromDBVal<int>(reader["idTipoDocumento"]);
                                responseEditar.numeroDocumento = ConvertFDBVal.ConvertFromDBVal<string>(reader["numeroDocumento"]);
                                responseEditar.fechaExpedicion = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fechaExpedicion"]).ToShortDateString();
                                //responseEditar.idlugarExpedicion = ConvertFDBVal.ConvertFromDBVal<long>(reader["idlugarExpedicion"]);
                                responseEditar.fechaNacimiento = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fechaNacimiento"]).ToShortDateString();
                                responseEditar.edad = ConvertFDBVal.ConvertFromDBVal<int>(reader["edad"]);
                                responseEditar.idPaisNacimiento = ConvertFDBVal.ConvertFromDBVal<int>(reader["idPaisNacimiento"]);
                                responseEditar.idDepartamentoNacimiento = ConvertFDBVal.ConvertFromDBVal<long>(reader["idDepartamentoNacimiento"]);
                                responseEditar.idMunicipioNacimiento = ConvertFDBVal.ConvertFromDBVal<long>(reader["idMunicipioNacimiento"]);
                                responseEditar.idSexo = ConvertFDBVal.ConvertFromDBVal<int>(reader["idSexo"]);
                                responseEditar.idIdentidadGenero = ConvertFDBVal.ConvertFromDBVal<int>(reader["idIdentidadGenero"]);
                                responseEditar.idOrientacionSexual = ConvertFDBVal.ConvertFromDBVal<int>(reader["idOrientacionSexual"]);
                                responseEditar.idNivelAcademico = ConvertFDBVal.ConvertFromDBVal<int>(reader["idNivelAcademico"]);
                                responseEditar.idNivelAcademico = ConvertFDBVal.ConvertFromDBVal<int>(reader["idEstadoAcademico"]); //agregado
                                responseEditar.direccionResidencia = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccionResidencia"]);
                                responseEditar.barrio = ConvertFDBVal.ConvertFromDBVal<string>(reader["barrio"]);
                                responseEditar.telefono = ConvertFDBVal.ConvertFromDBVal<string>(reader["telefono"]);
                                responseEditar.celular = ConvertFDBVal.ConvertFromDBVal<string>(reader["celular"]);
                                responseEditar.correoElectronico = ConvertFDBVal.ConvertFromDBVal<string>(reader["correoElectronico"]);
                                responseEditar.idDiscapasidad = ConvertFDBVal.ConvertFromDBVal<int>(reader["idDiscapasidad"]);


                                responseEditar.estadoEmbarazo.estadoEmbarazo = ConvertFDBVal.ConvertFromDBVal<string>(reader["estadoEmbarazo"]);
                                responseEditar.estadoEmbarazo.mesesEmbarazo = ConvertFDBVal.ConvertFromDBVal<int>(reader["mesesEmbarazo"]);
                                responseEditar.afiliadoSeguridadSocial.estaAfiliado = ConvertFDBVal.ConvertFromDBVal<string>(reader["estaAfiliado"]);
                                responseEditar.afiliadoSeguridadSocial.eps = ConvertFDBVal.ConvertFromDBVal<string>(reader["eps"]);
                                responseEditar.afiliadoSeguridadSocial.ips = ConvertFDBVal.ConvertFromDBVal<string>(reader["ips"]);
                                responseEditar.adultoMayor = ConvertFDBVal.ConvertFromDBVal<bool>(reader["poblacionLgtbi"]);
                                responseEditar.mujerEmbarazada = ConvertFDBVal.ConvertFromDBVal<bool>(reader["adultoMayor"]);
                                responseEditar.poblacionLgtbi = ConvertFDBVal.ConvertFromDBVal<bool>(reader["mujerEmbarazada"]);
                                responseEditar.ninoNinaAdolocente = ConvertFDBVal.ConvertFromDBVal<bool>(reader["ninoNinaAdolocente"]);
                                responseEditar.migrante = ConvertFDBVal.ConvertFromDBVal<bool>(reader["migrante"]);
                                responseEditar.victimaConflictoArmado = ConvertFDBVal.ConvertFromDBVal<bool>(reader["victimaConflictoArmado"]);
                                responseEditar.personasLideresDefensorasDH = ConvertFDBVal.ConvertFromDBVal<bool>(reader["personasLideresDefensorasDH"]);
                                responseEditar.personasHabitalidadCalle = ConvertFDBVal.ConvertFromDBVal<bool>(reader["personasHabitalidadCalle"]);
                                responseEditar.puebloIndigena = ConvertFDBVal.ConvertFromDBVal<string>(reader["puebloIndigena"]);

                            }

                        _connectionDb.Close();

                    }
                }

                return responseEditar;
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

        #endregion

        #region miguel moreno
        public Tuple<bool?, int> RegistroInvolucrado(RequestDatosInvolucrado data)
        {
            Tuple<bool?, int> salida;
            try
            {
                string? mensaje = "";
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_CREAR_INVOLUCRADO_V2";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;

                        _command.Parameters.AddWithValue("@nombre_ciudadano", BdValidation.ToDBNull(data.nombre_ciudadano));
                        _command.Parameters.AddWithValue("@primer_nombre", BdValidation.ToDBNull(data.primer_nombre));
                        _command.Parameters.AddWithValue("@segundo_nombre", BdValidation.ToDBNull(data.segundo_nombre));
                        _command.Parameters.AddWithValue("@primer_apellido", BdValidation.ToDBNull(data.primer_apellido));
                        _command.Parameters.AddWithValue("@segundo_apellido", BdValidation.ToDBNull(data.segundo_apellido));
                        _command.Parameters.AddWithValue("@apellido_ciudadano", BdValidation.ToDBNull(data.apellido_ciudadano));
                        _command.Parameters.AddWithValue("@tipo_documento", BdValidation.ToDBNull(data.id_tipo_documento));
                        _command.Parameters.AddWithValue("@numero_documento", BdValidation.ToDBNull(data.numero_documento));
                        _command.Parameters.AddWithValue("@fecha_nacimiento", BdValidation.ToDBNull(data.fecha_nacimiento));
                        _command.Parameters.AddWithValue("@genero", BdValidation.ToDBNull(data.genero));
                        _command.Parameters.AddWithValue("@edad", BdValidation.ToDBNull(data.edad));
                        _command.Parameters.AddWithValue("@telefono", BdValidation.ToDBNull(data.telefono));
                        _command.Parameters.AddWithValue("@correo_electronico", BdValidation.ToDBNull(data.correo_electronico));
                        _command.Parameters.AddWithValue("@localidad", BdValidation.ToDBNull(data.localidad));
                        _command.Parameters.AddWithValue("@barrio", BdValidation.ToDBNull(data.barrio));
                        _command.Parameters.AddWithValue("@direccion", BdValidation.ToDBNull(data.direccion));
                        _command.Parameters.AddWithValue("@es_victima", data.tipoInvolucrado);
                        _command.Parameters.AddWithValue("@principal", data.principal ? true : false);

                        // cambio añadido para idenficiar agresor y victima principal
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        int idInsercion = Convert.ToInt32(_command.ExecuteScalar().ToString());
                        salida = Tuple.Create(data.tipoInvolucrado, idInsercion);

                        _connectionDb.Close();
                    }
                }

                return salida;
            }
            catch (Exception e)
            {
                throw new ControledException(e.HResult);
            }
        }


        public void RegistroServicioInvolucrado(long idSolicitudServicio, long idInvolucrado)
        {
            try
            {
                string? mensaje = "";
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_CREAR_SERVICIO_INVOLUCRADO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@id_solicitud_servicio", idSolicitudServicio);
                        _command.Parameters.AddWithValue("@id_involucrado", idInvolucrado);

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        _command.ExecuteNonQuery();
                        _connectionDb.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public ResponseListaPaginada ObtenerCiudadano(int id)
        {
            CiudadanoSimpleDTO ciudadano = new CiudadanoSimpleDTO();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_OBTENER_CIUDADANO";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id_ciudadano", BdValidation.ToDBNull(id));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();

                    using SqlDataReader reader = _command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                ciudadano.id = ConvertFDBVal.ConvertFromDBVal<Int64>(reader["id"]);
                                ciudadano.nombre_ciudadano = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_ciudadano"]);
                                ciudadano.primer_apellido = ConvertFDBVal.ConvertFromDBVal<string>(reader["primer_apellido"]);
                                ciudadano.segundo_apellido = ConvertFDBVal.ConvertFromDBVal<string>(reader["segundo_apellido"]);
                                ciudadano.tipo_documento = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento"]);
                                ciudadano.celular = ConvertFDBVal.ConvertFromDBVal<string>(reader["celular"]);
                                ciudadano.telefono_fijo = ConvertFDBVal.ConvertFromDBVal<string>(reader["telefono_fijo"]);
                                ciudadano.edad = reader["edad"].ToString() == string.Empty ? null : ConvertFDBVal.ConvertFromDBVal<int>(reader["edad"]);
                                ciudadano.fecha_nacimiento = reader["fecha_nacimiento"].ToString() == string.Empty ? null : ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_nacimiento"]);
                                ciudadano.correo_electronico = ConvertFDBVal.ConvertFromDBVal<string>(reader["correo_electronico"]);
                                ciudadano.numero_documento = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento"]);
                                ciudadano.registro_completo = ConvertFDBVal.ConvertFromDBVal<bool>(reader["requiereModificacion"]);

                                ciudadano.poblacion_lgtbi = reader["poblacion_lgtbi"].ToString() == string.Empty ? false : ConvertFDBVal.ConvertFromDBVal<bool>(reader["poblacion_lgtbi"]);
                                ciudadano.adulto_mayor = reader["adulto_mayor"].ToString() == string.Empty ? false : ConvertFDBVal.ConvertFromDBVal<bool>(reader["adulto_mayor"]);
                                ciudadano.mujer_embarazada = reader["mujer_embarazada"].ToString() == string.Empty ? false : ConvertFDBVal.ConvertFromDBVal<bool>(reader["mujer_embarazada"]);
                                ciudadano.nino_nina_adolecente = reader["nino_nina_adolecente"].ToString() == string.Empty ? false : ConvertFDBVal.ConvertFromDBVal<bool>(reader["nino_nina_adolecente"]);
                                ciudadano.victima_conflicto_armado = reader["victima_conflicto_armado"].ToString() == string.Empty ? false : ConvertFDBVal.ConvertFromDBVal<bool>(reader["victima_conflicto_armado"]);
                                ciudadano.persona_lider_defensor_DH = reader["persona_lider_defensor_DH"].ToString() == string.Empty ? false : ConvertFDBVal.ConvertFromDBVal<bool>(reader["persona_lider_defensor_DH"]);
                                ciudadano.pueblo_indigena = reader["pueblo_indigena"].ToString() == String.Empty ? "No" : ConvertFDBVal.ConvertFromDBVal<string>(reader["pueblo_indigena"]);
                                ciudadano.migrante = reader["migrante"].ToString() == string.Empty ? false : ConvertFDBVal.ConvertFromDBVal<bool>(reader["migrante"]);
                            }

                        }

                        _connectionDb.Close();
                        ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();
                        responseListaPaginada.DatosPaginados = ciudadano;
                        return responseListaPaginada;
                    }
                    throw new Exception("no existen datos");
                }
            }
        }

        /// <summary>
        ///  el metodo regresa los procesos activos que tiene el ciudadano.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int id, int idComisaria)
        {
            List<SolicitudServicioDTO> solicitudes = new List<SolicitudServicioDTO>();

            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_CONSULTA_PROCESO_POR_CIUDADANO";
                using (_command = new SqlCommand(query, _connectionDb))
                {
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@id_ciudadano", BdValidation.ToDBNull(id));
                    _command.Parameters.AddWithValue("@id_comisaria", BdValidation.ToDBNull(idComisaria));

                    _connectionDb.Open();

                    using (SqlDataReader reader = _command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1)
                                {
                                    SolicitudServicioDTO solicitud = new SolicitudServicioDTO
                                    {
                                        id_solicitud_servicio = ConvertFDBVal.ConvertFromDBVal<Int64>(reader["id_solicitud_servicio"]),
                                        fecha_solicitud = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_solicitud"]),
                                        hora_solicitud = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["hora_solicitud"]),
                                        descripcion_de_hechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion_de_hechos"]),
                                        estado_de_la_solicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado_solicitud"]),
                                        codigo_solicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_solicitud"]),
                                        proceso = ConvertFDBVal.ConvertFromDBVal<string>(reader["proceso"]),
                                        perfil = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_perfil"]),
                                        retormar_solicitud = ConvertFDBVal.ConvertFromDBVal<bool>(reader["retomar_solicitud"])
                                    };
                                    solicitudes.Add(solicitud);
                                }
                            }
                        }
                        return new ResponseListaPaginada
                        {
                            DatosPaginados = solicitudes,
                            TotalRegistros = solicitudes.Count
                        };
                    }
                }
            }
        }
        public ResponseListaPaginada ObtenerSolicitudServiciosCiudadano(int idComisaria)
        {
            string? mensaje = "";
            List<SolicitudServicioDTO> solicitudes = new List<SolicitudServicioDTO>();

            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_CONSULTAR_SOLICTUDES_POR_COMISARIA";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@id_comisaria", BdValidation.ToDBNull(idComisaria));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();

                    using SqlDataReader reader = _command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                SolicitudServicioDTO solicitud = new SolicitudServicioDTO();
                             
                                solicitud.estado_de_la_solicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado_solicitud"]);
                                solicitud.codigo_solicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_solicitud"]);
                                solicitudes.Add(solicitud);
                            }
                        }

                        _connectionDb.Close();
                        ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();
                        responseListaPaginada.DatosPaginados = solicitudes;
                        responseListaPaginada.TotalRegistros = solicitudes.Count;
                        return responseListaPaginada;
                    }
                    throw new Exception("no existen datos");
                }
            }

     
        }
        

        public ResponseListaPaginada ConsultarSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud)
        {
            List<ConsultaGeneralSolicitudDTO> resultados = new List<ConsultaGeneralSolicitudDTO>();

            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                using (_command = new SqlCommand("PR_SICOFA_CONSULTA_GENERAL_SOLICITUDES", _connectionDb))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@numeroDocumento",
                        string.IsNullOrEmpty(solicitud.NumeroDocumento) ? DBNull.Value : (object)solicitud.NumeroDocumento);

                    _command.Parameters.AddWithValue("@codigoSolicitud",
                        string.IsNullOrEmpty(solicitud.CodigoSolicitud) ? DBNull.Value : (object)solicitud.CodigoSolicitud);

                    _command.Parameters.AddWithValue("@idComisaria",
                        BdValidation.ToDBNull(solicitud.IdComisaria));

                    _connectionDb.Open();

                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ConsultaGeneralSolicitudDTO item = new ConsultaGeneralSolicitudDTO
                            {
                                IdSolicitudServicio = Convert.ToInt32(reader["id_solicitud_servicio"]),
                                FechaSolicitud = Convert.ToDateTime(reader["fecha_solicitud"]),
                                IdComisaria = Convert.ToInt32(reader["id_comisaria"]),
                                EstadoSolicitud = Convert.ToString(reader["estado_solicitud"]),
                                CodigoSolicitud = Convert.ToString(reader["codigo_solicitud"]),
                                NumeroDocumento = Convert.ToString(reader["numero_documento"]),
                                NombreCiudadano = Convert.ToString(reader["nombre_ciudadano"]),
                                PrimerApellido = Convert.ToString(reader["primer_apellido"]),
                                SegundoApellido = Convert.ToString(reader["segundo_apellido"]),
                                CodigoDocumento = Convert.ToString(reader["CodigoDocumento"]),
                                NombreDocumento = Convert.ToString(reader["NombreDocumento"]),
                                Proceso = Convert.ToString(reader["Proceso"])
                            };

                            resultados.Add(item);
                        }
                    }

                    _connectionDb.Close();

                    ResponseListaPaginada response = new ResponseListaPaginada
                    {
                        DatosPaginados = resultados,
                        TotalRegistros = resultados.Count,
                        Message = resultados.Count > 0 ? "Datos obtenidos correctamente" : "No se encontraron registros"
                    };

                    return response;
                }
            }
        }
        public ResponseListaPaginada ConsultarPreSolicitudesGeneralesPorFiltros(ConsultaGeneralSolicitudRequestDTO solicitud)
        {
            List<ConsultaGeneralPreSolicitudDTO> resultados = new();

            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            using (_command = new SqlCommand("PR_SICOFA_CONSULTA_GENERAL_PRE_SOLICITUDES", _connectionDb))
            {
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.AddWithValue("@fechaSolicitud",
                    solicitud.FechaSolicitud.HasValue
                        ? solicitud.FechaSolicitud.Value
                        : DBNull.Value);


                _command.Parameters.AddWithValue("@codigoSolicitud",
                    string.IsNullOrEmpty(solicitud.CodigoSolicitud)
                        ? DBNull.Value
                        : (object)solicitud.CodigoSolicitud);

                _command.Parameters.AddWithValue("@idComisaria",
                    BdValidation.ToDBNull(solicitud.IdComisaria));

                _connectionDb.Open();

                using SqlDataReader reader = _command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new ConsultaGeneralPreSolicitudDTO
                        {
                            IdSolicitudServicio = Convert.ToInt32(reader["id_solicitud_servicio"]),
                            FechaSolicitud = Convert.ToDateTime(reader["fecha_solicitud"]),
                            IdComisaria = Convert.ToInt32(reader["id_comisaria"]),
                            EstadoSolicitud = Convert.ToString(reader["estado_solicitud"]),
                            CodigoSolicitud = Convert.ToString(reader["codigo_solicitud"]),
                            EsCompetenciaComisaria = Convert.ToInt32(reader["esCompetenciaComisaria"]), 
                            EsVictima = Convert.ToInt32(reader["es_victima"]),              
                            DescripcionHechos = Convert.ToString(reader["descripcion_de_hechos"]), 
                        };

                        resultados.Add(item);
                    }
                }

                _connectionDb.Close();

                return new ResponseListaPaginada
                {
                    DatosPaginados = resultados,
                    TotalRegistros = resultados.Count,
                    Message = resultados.Count > 0 ? "Datos obtenidos correctamente" : "No se encontraron registros"
                };
            }
        }

        public SolicitudServicioDetalleDTO ObtenerSolicitudServiciosCiudadanoDetalle(int id)
        {
            string? mensaje = "";
            SolicitudServicioDetalleDTO solicitud = new SolicitudServicioDetalleDTO();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_CONSULTA_PROCESO_DETALLE_POR_ID";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id", BdValidation.ToDBNull(id));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                solicitud.codigo_solicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_solicitud"]);
                                solicitud.nombre_ciudaddano = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_ciudadano"]);
                                solicitud.fecha_solicitud = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_solicitud"]);
                                solicitud.hora_solicitud = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["hora_solicitud"]);
                                solicitud.descripcion_de_hechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion_de_hechos"]);
                                solicitud.es_victima = ConvertFDBVal.ConvertFromDBVal<bool>(reader["es_victima"]);
                                solicitud.fecha_hecho_violento = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_hecho_violento"]);
                            }
                        }
                        _connectionDb.Close();
                        return solicitud;
                    }
                }
            }
            throw new Exception("no existen datos");
        }


        public SolicitudServicioDatosDTO ObtenerDatosSolicitud(int idSolicitud)
        {

            return (from solicitud in _context.SicofaSolicitudServicio
                    join ciudadano in _context.SicofaCiudadano on solicitud.IdCiudadano equals ciudadano.IdCiudadano
                    where solicitud.IdSolicitudServicio == idSolicitud
                    select new SolicitudServicioDatosDTO
                    {
                        codigo_solicitud = solicitud.CodigoSolicitud,
                        id_ciudadano = ciudadano.IdCiudadano.ToString(),
                        nombre_ciudaddano = ciudadano.NombreCiudadano,
                        fecha_solicitud = solicitud.FechaSolicitud.ToString(Constants.FormatoFechaCorta),
                        hora_solicitud = solicitud.HoraSolicitud.ToString(Constants.FormatoSoloHora),
                        fecha_hecho_violento = Convert.ToDateTime(solicitud.FechaHechoViolento).ToString(Constants.FormatoFechaCorta),
                        descripcion_de_hechos = solicitud.DescripcionDeHechos,
                        es_victima = solicitud.EsVictima,
                        relacionParentescoAgresor = solicitud.IdRelacionParentescoAgresor.ToString(),
                        conviveConAgresor = solicitud.ConviveConAgresor,
                        esCompetenciaComisaria = solicitud.EsCompetenciaComisaria,
                        esNecesarioRemitir = solicitud.EsNecesarioRemitir,
                        idtipoTramite = solicitud.IdTipoTramite.ToString(),
                        idContextofamiliar = solicitud.IdContextoFamiliar.ToString(),
                        noCompetenciaDescripcion = solicitud.NoCompetenciaDescrip
                    }).FirstOrDefault()!;

        }


        public List<CantidadInvolucradosDTO> cantidadInvolucradosPorServicio(int id)
        {
            List<CantidadInvolucradosDTO> listaInvolucrados = new List<CantidadInvolucradosDTO>();
            string? mensaje = "";
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_CANTIDAD_INVOLUCRADOS";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id", BdValidation.ToDBNull(id));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                CantidadInvolucradosDTO contadorInvolucrados = new CantidadInvolucradosDTO();
                                contadorInvolucrados.cantidad = ConvertFDBVal.ConvertFromDBVal<int>(reader["cantidad"]);
                                contadorInvolucrados.tipo_victima = ConvertFDBVal.ConvertFromDBVal<bool>(reader["tipo_victima"]);
                                listaInvolucrados.Add(contadorInvolucrados);
                            }
                            else
                                mensaje = ConvertFDBVal.ConvertFromDBVal<string>(reader["respuesta"]);
                        }
                    }
                }
                _connectionDb.Close();
            }

            return listaInvolucrados;
        }


        public List<ComisariaDTO> ComisariasPorMunicipio(int id)
        {
            List<ComisariaDTO> comisarias = new List<ComisariaDTO>();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_OBTENER_COMISARIA_POR_MUNICIPIO_ID";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id_ciudad_municipio", BdValidation.ToDBNull(id));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                ComisariaDTO comisaria = new ComisariaDTO();
                                comisaria.id_comisaria = ConvertFDBVal.ConvertFromDBVal<Int64>(reader["id_comisaria"]);
                                comisaria.codigo_comisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_comisaria"]);
                                comisaria.nombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre"]);
                                comisarias.Add(comisaria);
                            }
                        }
                        _connectionDb.Close();
                        return comisarias;
                    }

                    throw new Exception("no existen datos");
                }
            }
        }

        public List<ComisariaDTO> ObtenerComisariasTraslado(int idComisariaActual)
        {
            return (from comisaria in _context.SicofaComisaria
                    where comisaria.IdComisaria != idComisariaActual
                    select new ComisariaDTO {
                    id_comisaria = comisaria.IdComisaria,
                        codigo_comisaria = comisaria.CodigoComisaria,
                        nombre = comisaria.Nombre

                    }).ToList();              
        }



        public void RegistroRemisionSolicitud(RequestRemisionSolicitud data)
        {
            try
            {
                int idInsercion = 0;
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_CREAR_REMISION_SOLICITUD_SERVICIO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@id_solicitud_servicio", data.id_solicitud_servicio);
                        _command.Parameters.AddWithValue("@id_comisaria_origen", data.id_comisaria_origen);
                        _command.Parameters.AddWithValue("@tipo_remision", data.tipo_remision);
                        _command.Parameters.AddWithValue("@id_comisaria_destino", data.id_comisaria_destino != null ? data.id_comisaria_destino : DBNull.Value);
                        _command.Parameters.AddWithValue("@id_entidad_externa", data.id_entidad_externa != 0 ? data.id_entidad_externa : DBNull.Value);
                        _command.Parameters.AddWithValue("@justificacion", data.justificacion);
                        _command.Parameters.AddWithValue("@usuario", data.idUsuarioSistema);
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        idInsercion = Convert.ToInt32(_command.ExecuteScalar().ToString());
                        _connectionDb.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public int ConsultaRemisionExistentePorSolicitud(Int64 id)
        {
            int mensaje = 0;
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_CONSULTA_REMISION_EXISTENTE_POR_SOLICITUD_ID";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id_solicitud_servicio", BdValidation.ToDBNull(id));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount >= 1)
                                mensaje = ConvertFDBVal.ConvertFromDBVal<int>(reader["mensaje"]);
                        }
                    }
                }
                _connectionDb.Close();
            }

            return mensaje;
        }


        public List<EntidadExterna> ObtenerEntidadExterna()
        {
            List<EntidadExterna> entidades = new List<EntidadExterna>();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_OBTENER_ENTIDADES_EXTERNAS";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount >= 1)
                            {
                                EntidadExterna entidad = new EntidadExterna();
                                entidad.id_entidad_externa = ConvertFDBVal.ConvertFromDBVal<Int64>(reader["id_entidad_externa"]);
                                entidad.codigo_entidad_extera = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_entidad_externa"]);
                                entidad.direccion = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccion"]);
                                entidad.nombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre"]);
                                entidad.telefono = ConvertFDBVal.ConvertFromDBVal<string>(reader["telefono"]);
                                entidades.Add(entidad);
                            }
                        }

                        _connectionDb.Close();
                        return entidades;
                    }

                    throw new Exception("no existen datos");
                }
            }
        }

        public List<SexoGeneroOrientacionDTO> SexoOrientacionGenero(string tipo)
        {
            List<SexoGeneroOrientacionDTO> listado = new List<SexoGeneroOrientacionDTO>();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_OBTENER_SEXOS_GENEROS_ORIENTACIONSEXUALES";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@tipo", BdValidation.ToDBNull(tipo));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                SexoGeneroOrientacionDTO genero = new SexoGeneroOrientacionDTO();
                                genero.id_sex_gen_orient = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_sex_gen_orient"]);
                                genero.nombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre"]);

                                listado.Add(genero);
                            }
                        }
                    }

                    _connectionDb.Close();
                    return listado;
                }
                return listado;
            }
        }

        public List<TipoDiscapacidadDTO> ObtenerTipoDiscapacidad()
        {
            List<TipoDiscapacidadDTO> listado = new List<TipoDiscapacidadDTO>();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_OBTENER_TIPO_DISCAPACIDAD";
                using (_command = new SqlCommand(query))
                {

                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                TipoDiscapacidadDTO discapacidad = new TipoDiscapacidadDTO();
                                discapacidad.id_tipo_discapacidad = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_tipo_discapacidad"]);
                                discapacidad.descripcion = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion"]);

                                listado.Add(discapacidad);
                            }
                        }
                    }

                    _connectionDb.Close();
                    return listado;
                }

                return listado;
            }
        }

        public List<NivelAcademicoDTO> ObtenerNivelAcademico()
        {
            List<NivelAcademicoDTO> listado = new List<NivelAcademicoDTO>();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_OBTENER_NIVEL_ACADEMICO";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                NivelAcademicoDTO nivelAcademico = new NivelAcademicoDTO();
                                nivelAcademico.id_nivel_academico = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_nivel_acedemico"]);
                                nivelAcademico.nivel_academico = ConvertFDBVal.ConvertFromDBVal<string>(reader["nivel_academico"]);

                                listado.Add(nivelAcademico);
                            }
                        }
                    }

                    _connectionDb.Close();
                    return listado;
                }
                return listado;
            }
        }

        public List<TipoRelacionDTO> ObtenerTIpoRelacion()
        {
            List<TipoRelacionDTO> listado = new List<TipoRelacionDTO>();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                string query = "PR_SICOFA_OBTENER_TIPO_RELACION";
                using (_command = new SqlCommand(query))
                {

                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                TipoRelacionDTO tiporelacion = new TipoRelacionDTO();
                                tiporelacion.id_tipo_relacion = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_tipo_relacion"]);
                                tiporelacion.descripcion = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion"]);

                                listado.Add(tiporelacion);
                            }
                        }
                    }

                    _connectionDb.Close();
                    return listado;
                }
                return listado;
            }
        }

        public List<PreguntasTipoViolenciaDTO> ObtenerQuestionarioViolencia(int id_tipo_violencia)
        {
            List<PreguntasTipoViolenciaDTO> listaPreguntas = new List<PreguntasTipoViolenciaDTO>();
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {

                string query = "PR_SICOFA_OBTENER_QUESTIONARIO_TIPO_VIOLENCIA";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id_tipo_violencia", BdValidation.ToDBNull(id_tipo_violencia));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                PreguntasTipoViolenciaDTO pregunta = new PreguntasTipoViolenciaDTO();
                                pregunta.id_questionario = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_questionario"]);
                                pregunta.descripcion = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion"]);
                                pregunta.es_cerrada = ConvertFDBVal.ConvertFromDBVal<bool>(reader["es_cerrada"]);
                                pregunta.puntuacion = ConvertFDBVal.ConvertFromDBVal<int>(reader["puntuacion"]);
                                listaPreguntas.Add(pregunta);
                            }
                        }
                    }

                    _connectionDb.Close();
                    return listaPreguntas;
                }
                return listaPreguntas;
            }
        }

        public int ContadorPreguntasPorTipoViolencia(int id_tipo_violencia)
        {
            int mensaje = 0;
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {

                string query = "PR_SICOFA_CONTADOR_PREGUNTAS";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id_tipo_violencia", BdValidation.ToDBNull(id_tipo_violencia));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount >= 1)
                                mensaje = ConvertFDBVal.ConvertFromDBVal<int>(reader["cantidadPreguntas"]);
                        }
                    }
                }
                _connectionDb.Close();
            }

            return mensaje;
        }


        public void RegistroRespuestasQuestionario(Int64 id_solicitud_servicio, RespuestaPorPregunta data)
        {
            try
            {
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_REGISTRO_QUESTIONARIO_RESPUESTAS";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@id_solicitud_servicio", id_solicitud_servicio);
                        _command.Parameters.AddWithValue("@id_questionario", data.id_questionario);
                        _command.Parameters.AddWithValue("@mes", data.mes);
                        _command.Parameters.AddWithValue("@puntuacion", data.puntaje);

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        _command.ExecuteNonQuery();
                        _connectionDb.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }


        public void RegistroInvolucradoPrincipalAproceso(Int64 idCiudadano, Int64 idSolicitudServicio)
        {
            try
            {
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_ACTUALIZAR_VICTIMA_PRINCIPAL_ID_V2";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idCiudadano", idCiudadano);
                        _command.Parameters.AddWithValue("@solicitudProceso", idSolicitudServicio);

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        _command.ExecuteNonQuery();
                        _connectionDb.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public RequestDatosInvolucradoPrincipal1 ConsultaInvolucradoPrincipal(int id_ciudadano)
        {
            RequestDatosInvolucradoPrincipal1 involucrado = new RequestDatosInvolucradoPrincipal1();

            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {
                // cambio sp db
                string query = "PR_SICOFA_OBTENERVICTIMAPRINCIPAL_V2";
                using (_command = new SqlCommand(query))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id_ciudadano", BdValidation.ToDBNull(id_ciudadano));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    //TODO: Eliminar Nombres y Apellidos
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 1)
                            {
                                involucrado.id = ConvertFDBVal.ConvertFromDBVal<Int64>(reader["id_involucrado"]);
                                involucrado.primer_nombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["primer_nombre"]); ;
                                involucrado.segundo_nombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["segundo_nombre"]); ;
                                involucrado.primer_apellido = ConvertFDBVal.ConvertFromDBVal<string>(reader["primer_apellido"]); ;
                                involucrado.segundo_apellido = ConvertFDBVal.ConvertFromDBVal<string>(reader["segundo_apellido"]); ;
                                involucrado.nombre_ciudadano = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombres"]);
                                involucrado.apellido_ciudadano = ConvertFDBVal.ConvertFromDBVal<string>(reader["apellidos"]);
                                involucrado.numero_documento = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento"]);
                                involucrado.fecha_nacimiento = reader["fecha_nacimiento"].ToString() == string.Empty ? null : ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_nacimiento"]);
                                involucrado.genero = ConvertFDBVal.ConvertFromDBVal<string>(reader["genero"]);
                                involucrado.id_tipo_documento = ConvertFDBVal.ConvertFromDBVal<int>(reader["tipo_documento"]);
                                involucrado.edad = ConvertFDBVal.ConvertFromDBVal<int>(reader["edad"]);
                                involucrado.telefono = ConvertFDBVal.ConvertFromDBVal<string>(reader["telefono"]);
                                involucrado.correo_electronico = ConvertFDBVal.ConvertFromDBVal<string>(reader["correo_electronico"]);
                                involucrado.localidad = ConvertFDBVal.ConvertFromDBVal<string>(reader["localidad"]);
                                involucrado.barrio = ConvertFDBVal.ConvertFromDBVal<string>(reader["barrio"]);
                                involucrado.direccion = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccion_recidencia"]);
                                involucrado.tipoInvolucrado = ConvertFDBVal.ConvertFromDBVal<bool>(reader["es_victima"]);
                                involucrado.principal = ConvertFDBVal.ConvertFromDBVal<bool>(reader["es_principal"]);
                                involucrado.id_tipo_discapacidad = 1;
                                involucrado.estado_embarazo = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado_embarazo"]);
                                involucrado.afiliado_seguridad_social = ConvertFDBVal.ConvertFromDBVal<string>(reader["afiliado_seguridad_social"]);
                                involucrado.eps = ConvertFDBVal.ConvertFromDBVal<string>(reader["eps"]);
                                involucrado.ips = ConvertFDBVal.ConvertFromDBVal<string>(reader["ips"]);
                            }
                        }
                    }
                }

                _connectionDb.Close();
                return involucrado;
            }
            return involucrado;
        }

        public int ValidarSolicitudPorCiudadano(long id_solicitud_servicio, long @id_ciudadano)
        {
            int mensaje = 0;
            using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
            {

                string query = "PR_SICOFA_VALIDAR_REGISTRO_INVOLUCRADO";
                using (_command = new SqlCommand(query))
                {

                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.AddWithValue("@id_solitud_servicio", BdValidation.ToDBNull(id_solicitud_servicio));
                    _command.Parameters.AddWithValue("@id_ciudadano", BdValidation.ToDBNull(id_ciudadano));

                    _command.Connection = _connectionDb;
                    _connectionDb.Open();
                    using SqlDataReader reader = _command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount >= 1)
                                mensaje = ConvertFDBVal.ConvertFromDBVal<int>(reader["cantidad"]);
                        }
                    }
                }
                _connectionDb.Close();
            }

            return mensaje;
        }

        public Task<SolicitudGeneralDTO> ConsultaGeneralSolicitud(long idSolicitudServicio)
        {
            try
            {
                SolicitudGeneralDTO result = new SolicitudGeneralDTO();
                SicofaSolicitudServicio solicitud = _context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado)
                    .Where(s => s.IdSolicitudServicio == idSolicitudServicio).SingleOrDefault()!;

                if (solicitud != null)
                {
                    result.idSolicitudServicio = solicitud.IdSolicitudServicio;
                    result.fechaSolicitud = solicitud.FechaSolicitud;
                    result.relatoHechos = solicitud.DescripcionDeHechos;
                    result.estadoSolicitud = solicitud.EstadoSolicitud;
                    result.subestadoSolicitud = solicitud.SubestadoSolicitud;

                    if (solicitud.IdInvolucrado.Count > 0)
                    {
                        List<SolicitudGeneralInvolucradoDTO> involucrados = new List<SolicitudGeneralInvolucradoDTO>();
                        SolicitudGeneralInvolucradoDTO involucrado;
                        foreach (var inv in solicitud.IdInvolucrado)
                        { 
                            involucrado = new SolicitudGeneralInvolucradoDTO();
                            involucrado.idInvolucrado = inv.IdInvolucrado;
                            involucrado.tipoDocumento = (int)inv.TipoDocumento!;
                            involucrado.numeroDocumento = inv.NumeroDocumento;
                            involucrado.nombres = inv.PrimerNombre + (inv.SegundoNombre.Length > 0 ?" " + inv.SegundoNombre : "") + " " + inv.PrimerApellido + (inv.SegundoApellido.Length > 0 ? " " + inv.SegundoApellido : "");
                            involucrado.tipoInvolucrado = inv.EsVictima ? "Accionante" : "Accionado";

                            involucrados.Add(involucrado);
                        }
                        result.involucrados = involucrados;
                    }

                    result.tareas = (from t in _context.SicofaTarea
                                     join f in _context.SicofaFlujoV2 on t.IdFlujo equals f.IdFlujo
                                     join a in _context.SicofaActividad on f.IdActividadMain equals a.IdActividad
                                     join p in _context.SicofaProceso on f.IdProceso equals p.IdProceso
                                     join u in _context.SicofaUsuarioSistema on t.IdUsuarioSistema equals u.IdUsuarioSistema
                                     where t.IdSolicitudServicio == idSolicitudServicio
                                     orderby t.IdTarea
                                     select new SolicitudGeneralTareasDTO
                                     { 
                                         idTarea = t.IdTarea,
                                         nombreTarea = a.NombreActividad,
                                         nombreProceso = p.NombreProceso,
                                         usuario = u.Nombres+ " " + u.Apellidos,
                                         fechaCreacion = (DateTime)t.FechaCreacion!,
                                         fechaTerminacion = t.FechaTerminacion,
                                         estadoTarea = t.Estado
                                     }).ToList();

                    result.anexos = (from a in _context.SicofaSolicitudServicioAnexo
                                     join d in _context.SicofaDocumento on a.IdDocumento equals d.IdDocumento
                                     where a.IdSolicitudServicio == idSolicitudServicio
                                     orderby a.IdSolicitudAnexo
                                     select new SolicitudGeneralAnexoDTO
                                     { 
                                         idAnexo = a.IdSolicitudAnexo,
                                         nombreDocumento = d.NombreDocumento,
                                         nombreArchivo = a.NombreDocumento,
                                         fechaCreacion = (DateTime)a.FechaCreacion
                                     }).ToList();
                }

                return Task.FromResult(result);
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

                List<RemisionSolicitudServicioComisariaAnteriorDTO> remisionSolicitudes = new List<RemisionSolicitudServicioComisariaAnteriorDTO>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_REMISION_SOLICITUD_SERVICIO_ANTERIOR";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;

                        _command.Parameters.AddWithValue("@id_solicitud_servicio", BdValidation.ToDBNull(idSolicitudServicio));

                        _command.Connection = _connectionDb;

                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1)
                                {
                                    RemisionSolicitudServicioComisariaAnteriorDTO remisionSolicitud = new RemisionSolicitudServicioComisariaAnteriorDTO();
                                    remisionSolicitud.id_comisaria_origen = ConvertFDBVal.ConvertFromDBVal<Int64>(reader["id_comisaria_origen"]);
                                    remisionSolicitud.nombre_comisaria_origen = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_comisaria_origen"]);
                                    remisionSolicitud.justificacion = ConvertFDBVal.ConvertFromDBVal<string>(reader["justificacion"]);
                                    remisionSolicitud.remitente = ConvertFDBVal.ConvertFromDBVal<string>(reader["remitente"]);
                                    remisionSolicitud.descripcion_de_hechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion_de_hechos"]);
                                    remisionSolicitudes.Add(remisionSolicitud);
                                }
                            }
                            _connectionDb.Close();

                           
                        }
                    }
                }
                return remisionSolicitudes;
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

        List<ConsultaGeneralSolicitudDTO> ISolicitudesRepository.ConsultaSolicitudGeneral(ConsultaGeneralSolicitudDTO solicitud)
        {
            throw new NotImplementedException();
        }

        #endregion Diana Ariza
    }
}
#endregion




