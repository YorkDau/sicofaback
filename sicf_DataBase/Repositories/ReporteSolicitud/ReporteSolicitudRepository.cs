using Microsoft.Extensions.Configuration;
using sicf_DataBase.BDConnection;
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
                List<ReporteSolicitudDTO> solicitudes = new List<ReporteSolicitudDTO>();
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
                                    ReporteSolicitudDTO solicitud = new ReporteSolicitudDTO();

                                    solicitud.fecha_ingreso = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_ingreso"]);
                                    solicitud.comisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["comisaria"]);
                                    solicitud.historia = ConvertFDBVal.ConvertFromDBVal<string>(reader["historia"]);
                                    solicitud.barrio = ConvertFDBVal.ConvertFromDBVal<string>(reader["barrio"]);
                                    solicitud.direccion_ubicacion_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccion_ubicacion_involucrado"]);
                                    solicitud.nombre_completo_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_completo_involucrado"]);
                                    solicitud.tipo_documento_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento_involucrado"]);
                                    solicitud.numero_documento_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento_involucrado"]);
                                    solicitud.edad_involucrado = ConvertFDBVal.ConvertFromDBVal<int>(reader["edad_involucrado"]);
                                    solicitud.tipo_violencia = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_violencia"]);
                                    solicitud.descripcion_lugar_de_hechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion_lugar_de_hechos"]);
                                    solicitud.hora_hecho_violento = ConvertFDBVal.ConvertFromDBVal<TimeSpan>(reader["hora_hecho_violento"]);
                                    solicitud.sexo_genero_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["sexo_genero_involucrado"]);
                                    solicitud.identidad_genero_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["identidad_genero_involucrado"]);
                                    solicitud.orientacion_sexual_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["orientacion_sexual_involucrado"]);
                                    solicitud.etnia_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["etnia_involucrado"]);
                                    solicitud.pais_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["pais_involucrado"]);
                                    solicitud.victima_conflicto_armado_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["victima_conflicto_armado_involucrado"]);
                                    solicitud.vicitma_es_poblacion_proteccion_especial = ConvertFDBVal.ConvertFromDBVal<string>(reader["vicitma_es_poblacion_proteccion_especial"]);
                                    solicitud.discapacidad_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["discapacidad_involucrado"]);
                                    solicitud.nivel_academico_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["nivel_academico_involucrado"]);
                                    solicitud.ocupacion_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["ocupacion_involucrado"]);
                                    solicitud.estrato_involucrado = ConvertFDBVal.ConvertFromDBVal<int>(reader["estrato_involucrado"]);
                                    solicitud.hijos_involucrado = ConvertFDBVal.ConvertFromDBVal<int>(reader["hijos_involucrado"]);
                                    solicitud.estado_embarazo_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado_embarazo_involucrado"]);
                                    solicitud.afiliado_seguridad_social_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["afiliado_seguridad_social_involucrado"]);
                                    solicitud.contexto_familiar_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["contexto_familiar_involucrado"]);
                                    solicitud.convive_con_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["convive_con_agresor"]);
                                    solicitud.nombre_completo_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_completo_agresor"]);
                                    solicitud.tipo_documento_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento_agresor"]);
                                    solicitud.numero_documento_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento_agresor"]);
                                    solicitud.edad_agresor = ConvertFDBVal.ConvertFromDBVal<int>(reader["edad_agresor"]);
                                    solicitud.sexo_genero_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["sexo_genero_agresor"]);
                                    solicitud.identidad_genero_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["identidad_genero_agresor"]);
                                    solicitud.etnia_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["etnia_agresor"]);
                                    solicitud.ocupacion_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["ocupacion_agresor"]);
                                    solicitud.nivel_academico_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["nivel_academico_agresor"]);
                                    solicitud.hijos_agresor = ConvertFDBVal.ConvertFromDBVal<int>(reader["hijos_agresor"]);
                                    solicitud.pertenece_grupo_armado_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["pertenece_grupo_armado_agresor"]);
                                    solicitud.medida_proteccion_otorgada_ley_575_2000 = ConvertFDBVal.ConvertFromDBVal<string>(reader["medida_proteccion_otorgada_ley_575_2000"]);
                                    solicitud.fecha_audiencia = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_audiencia"]);
                                    solicitud.estado = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado"]);
                                    solicitud.fecha_estado_proceso = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_estado_proceso"]);
                                    solicitud.observaciones = ConvertFDBVal.ConvertFromDBVal<string>(reader["observaciones"]);

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
                if (!String.IsNullOrEmpty(prm_solicitud.NombreCompletoVictima))
                    cmdParametros.Parameters.AddWithValue("@NombreCompletoVictima", BdValidation.ToDBNull(prm_solicitud.NombreCompletoVictima));
                if (!String.IsNullOrEmpty(prm_solicitud.NombreCompletoVictimario))
                    cmdParametros.Parameters.AddWithValue("@NombreCompletoVictimario", BdValidation.ToDBNull(prm_solicitud.NombreCompletoVictimario));
                if (!String.IsNullOrEmpty(prm_solicitud.SexoVictima))
                    cmdParametros.Parameters.AddWithValue("@SexoVictima", BdValidation.ToDBNull(prm_solicitud.SexoVictima));
                if (!String.IsNullOrEmpty(prm_solicitud.IdentidadGeneroVictima))
                    cmdParametros.Parameters.AddWithValue("@IdentidadGeneroVictima", BdValidation.ToDBNull(prm_solicitud.IdentidadGeneroVictima));
                if (!Equals(prm_solicitud.FechaHechoViolento, null))
                    cmdParametros.Parameters.AddWithValue("@FechaHechoViolento", BdValidation.ToDBNull(prm_solicitud.FechaHechoViolento));
                if (!Equals(prm_solicitud.HoraHechoViolento, null))
                    cmdParametros.Parameters.AddWithValue("@HoraHechoViolento", BdValidation.ToDBNull(prm_solicitud.HoraHechoViolento));

                cmdParametros.Parameters.AddWithValue("@idComisaria", null);
            }
            catch (ControledException ex)
            {
                throw new ControledException(Message.ErrorRequest);
            }
            catch (Exception ex)
            {
                throw new ControledException(Message.ErrorRequest, ex.HResult);
            }
        }
    }
}



