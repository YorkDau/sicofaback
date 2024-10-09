using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using sicf_DataBase.BDConnection;
using sicf_DataBase.Compartido;
using sicf_DataBase.Data;
using sicf_Models.Dto.ReporteSolicitud;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System.Data;
using System.Data.SqlClient;
using static sicf_Models.Constants.Constants;


namespace sicf_DataBase.Repositories.ReporteSolicitud
{
    public class ReporteSolicitudRepository : BdConnection, IReporteSolicitudRepository
    {
        public ResponseListaPaginada responseListaPaginada { get; set; }

        private IConfiguration? configuration { get; set; }

        public ReporteSolicitudRepository(IConfiguration configuration) : base(configuration)
        {
            responseListaPaginada = new ResponseListaPaginada();
        }

        /// <summary>
        /// ObtenerReporteSolicitudes: consulta la información base para reportes de solicitudes
        /// </summary>
        /// <param name="prm_solicitud"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public ResponseListaPaginada ObtenerReporteSolicitudes(RequestReporteSolicitudDTO prm_solicitud)
        {
            try
            {
                List<InformacionSolicitudDTO> solicitudes = new List<InformacionSolicitudDTO>();
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_REPORTES_SOLICITUDES_BASE";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        EstablecerParametrosReporteSolicitudes(_command, prm_solicitud);

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();
                        using SqlDataReader reader = _command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount >= 1)
                                {
                                    InformacionSolicitudDTO solicitud = new InformacionSolicitudDTO();

                                    solicitud.fechaRegistro = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_registro"]);
                                    solicitud.codigoSolicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_solicitud"]);
                                    solicitud.comisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["comisaria"]);
                                    solicitud.direccioncomisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccion_comisaria"]);
                                    solicitud.nombreCompletoFuncionario = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_completo_funcionario"]);
                                    solicitud.codigoTipoDocumentoFuncionario = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_tipo_documento_funcionario"]);
                                    solicitud.tipoDocumentoFuncionario = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento_funcionario"]);
                                    solicitud.numeroDocumentoFuncionario = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento_funcionario"]);
                                    solicitud.cargoFuncionario = ConvertFDBVal.ConvertFromDBVal<string>(reader["cargo_funcionario"]);
                                    solicitud.correoElectronicoFuncionario = ConvertFDBVal.ConvertFromDBVal<string>(reader["correo_electronico_funcionario"]);
                                    solicitud.contactoTelefonoFijoFuncionario = ConvertFDBVal.ConvertFromDBVal<long>(reader["contacto_telefono_fijo_funcionario"]);
                                    solicitud.contactoCelularFuncionario = ConvertFDBVal.ConvertFromDBVal<long>(reader["contacto_celular_funcionario"]);
                                    solicitud.nombreCompletoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_completo_involucrado"]);
                                    solicitud.fechaNacimientoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["fecha_nacimiento_involucrado"]);
                                    solicitud.edadInvolucrado = ConvertFDBVal.ConvertFromDBVal<int>(reader["edad_involucrado"]);
                                    solicitud.codigoTipoDocumentoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_tipo_documento_involucrado"]);
                                    solicitud.tipoDocumentoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento_involucrado"]);
                                    solicitud.numeroDocumentoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento_involucrado"]);
                                    solicitud.fechaExpedicionDocInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["fecha_expedicion_doc_involucrado"]);
                                    solicitud.lugarExpedicionDocInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["lugar_expedicion_doc_involucrado"]);
                                    solicitud.codigoPaisInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_pais_involucrado"]);
                                    solicitud.paisInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["pais_involucrado"]);
                                    solicitud.codigoDepartamentoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_departamento_involucrado"]);
                                    solicitud.departamentoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["departamento_involucrado"]);
                                    solicitud.codigoCidudadInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_ciudad_involucrado"]);
                                    solicitud.ciudadMunicipioInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["ciudad_municipio_involucrado"]);
                                    solicitud.correoElectronicoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["correo_electronico_involucrado"]);
                                    solicitud.contactoFijoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["contacto_fijo_involucrado"]);
                                    solicitud.contactoConfianzaInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["contacto_confianza_involucrado"]);
                                    solicitud.direccionUbicacionInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccion_ubicacion_involucrado"]);
                                    solicitud.sexoGeneroInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["sexo_genero_involucrado"]);
                                    solicitud.identidadGeneroInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["identidad_genero_involucrado"]);
                                    solicitud.orientacionSexualInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["orientacion_sexual_involucrado"]);
                                    solicitud.nivelAcademicoInvolucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["nivel_academico_involucrado"]);
                                    solicitud.vicitmaEsPoblacionProteccionEspecial = ConvertFDBVal.ConvertFromDBVal<string>(reader["vicitma_es_poblacion_proteccion_especial"]);
                                    solicitud.victimaPoneHechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["victima_pone_hechos"]);
                                    solicitud.rol = ConvertFDBVal.ConvertFromDBVal<string>(reader["rol"]);
                                    #region Datos del denunciante
                                    //solicitud.codigoTipoDocumentoDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_tipo_documento_denunciante"]);
                                    //solicitud.tipoDocumentoDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento_denunciante"]);
                                    //solicitud.numeroDocumentoDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento_denunciante"]);
                                    //solicitud.fechaExpedicionDocDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["fecha_expedicion_doc_denunciante"]);
                                    //solicitud.lugarExpedicionDocDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["lugar_expedicion_doc_victma"]);
                                    //solicitud.codigoPaisDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_pais_denunciante"]);
                                    //solicitud.paisDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["pais_denunciante"]);
                                    //solicitud.codigoDepartamentoDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_departamento_denunciante"]);
                                    //solicitud.departamentoDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["departamento_denunciante"]);
                                    //solicitud.codigoCiudadDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo_cidudad_denunciante"]);
                                    //solicitud.ciudadMunicipioDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["ciudad_municipio_denunciante"]);
                                    //solicitud.correoElectronicoDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["correo_electronico_denunciante"]);
                                    //solicitud.contactoFijoDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["contacto_fijo_denunciante"]);
                                    //solicitud.direccionUbicacionDenunciante = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccion_ubicacion_denunciante"]);
                                    #endregion
                                    solicitud.tipoViolencia = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_violencia"]);
                                    solicitud.descripcionDeHechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion_de_hechos"]);
                                    solicitud.fechaHechoViolento = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_hecho_violento"]);
                                    solicitud.horaHechoViolento = ConvertFDBVal.ConvertFromDBVal<TimeSpan>(reader["hora_hecho_violento"]);
                                    solicitud.DescripcionLugareHechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["Descripcion_lugar_de_hechos"]);

                                    solicitudes.Add(solicitud);

                                }
                            }
                        }
                        _connectionDb.Close();
                    }
                }

                if (solicitudes.Any())
                {
                    responseListaPaginada = new ResponseListaPaginada();
                    responseListaPaginada.DatosPaginados = solicitudes;
                    responseListaPaginada.TotalRegistros = solicitudes.Count;
                }


                return responseListaPaginada;
            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message, ex.HResult);
            }
        }

        /// <summary>
        /// Establece la lista de parámetros y sus repesctivos valores pare el método ObtenerReporteSolicitudes
        /// </summary>
        /// <param name="cmdParametros"></param>
        /// <param name="prm_solicitud"></param>
        private void EstablecerParametrosReporteSolicitudes(SqlCommand cmdParametros, RequestReporteSolicitudDTO prm_solicitud)
        {
            try
            {
                if (!String.IsNullOrEmpty(prm_solicitud.codigoTipoDocumento))
                    cmdParametros.Parameters.AddWithValue("@codigoTipoDocumento", BdValidation.ToDBNull(prm_solicitud.codigoTipoDocumento));
                if (!String.IsNullOrEmpty(prm_solicitud.numeroDocumento))
                    cmdParametros.Parameters.AddWithValue("@numeroDocumento", BdValidation.ToDBNull(prm_solicitud.numeroDocumento));
                if (!String.IsNullOrEmpty(prm_solicitud.codigoSolicitud))
                    cmdParametros.Parameters.AddWithValue("@codigoSolicitud", BdValidation.ToDBNull(prm_solicitud.codigoSolicitud));
                if (!String.IsNullOrEmpty(prm_solicitud.codigoTipoViolencia))
                    cmdParametros.Parameters.AddWithValue("@codigoTipoViolencia", BdValidation.ToDBNull(prm_solicitud.codigoTipoViolencia));
                if (!Equals(prm_solicitud.fechaSolicitudDesde, null))
                    cmdParametros.Parameters.AddWithValue("@fechaSolicitudDesde", BdValidation.ToDBNull(prm_solicitud.fechaSolicitudDesde));
                if (!Equals(prm_solicitud.fechaSolicitudHasta, null))
                    cmdParametros.Parameters.AddWithValue("@fechaSolicitudHasta", BdValidation.ToDBNull(prm_solicitud.fechaSolicitudHasta));
                
                    cmdParametros.Parameters.AddWithValue("@idComisaria",null);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Message.ErrorRequest);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Message.ErrorRequest,ex.HResult);
            }

        }
    }
}



