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
            ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

            try
            {
                string query;
                if (prm_solicitud.pard_generar)
                {
                    query = "PR_SICOFA_REPORTES_SOLICITUDES_PARD";
                }
                else
                {
                    query = "PR_SICOFA_REPORTES_SOLICITUDES_BASE";
                }

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;

                        // AUMENTA EL COMMANDTIMEOUT AQUÍ
                       // _command.CommandTimeout = 180; // Establece el timeout a 180 segundos (3 minutos)
                                                       // Puedes probar con 60, 90, o más si es necesario.

                        if (prm_solicitud.pard_generar)
                        {
                            _command.Parameters.AddWithValue("@id_comisaria", (object)prm_solicitud.id_comisaria ?? DBNull.Value);
                        }
                        else
                        {
                            EstablecerParametrosReporteSolicitudes(_command, prm_solicitud);
                        }

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();
                        using SqlDataReader reader = _command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            if (prm_solicitud.pard_generar)
                            {
                                List<ReporteSolicitudPARDDTO> solicitudesPARD = new List<ReporteSolicitudPARDDTO>();

                                while (reader.Read())
                                {
                                    ReporteSolicitudPARDDTO solicitud = new ReporteSolicitudPARDDTO
                                    {
                                        id_solicitud_servicio = reader["id_solicitud_servicio"] != DBNull.Value ? Convert.ToInt64(reader["id_solicitud_servicio"]) : 0,
                                        fecha_solicitud = reader["fecha_solicitud"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_solicitud"]) : DateTime.MinValue,
                                        estado_solicitud = reader["estado_solicitud"]?.ToString(),
                                        codigo_solicitud = reader["codigo_solicitud"]?.ToString(),
                                        id_comisaria = reader["id_comisaria"] != DBNull.Value ? (long?)Convert.ToInt64(reader["id_comisaria"]) : null,
                                        nombre_comisaria = reader["nombre_comisaria"]?.ToString(),
                                        descripcion_de_hechos = reader["descripcion_de_hechos"]?.ToString(),
                                        tipo_solicitud = reader["tipo_solicitud"]?.ToString(),
                                        subestado_solicitud = reader["subestado_solicitud"]?.ToString(),

                                        NombreVictima = reader["NombreVictima"]?.ToString(),
                                        DocumentoVictima = reader["DocumentoVictima"]?.ToString(),
                                        TipoDocumentoVictima = reader["TipoDocumentoVictima"]?.ToString(),
                                        GeneroVictima = reader["GeneroVictima"]?.ToString(),
                                        OrientacionSexualVictima = reader["OrientacionSexualVictima"]?.ToString(),
                                        EdadVictima = reader["EdadVictima"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EdadVictima"]) : null,
                                        TelefonoVictima = reader["TelefonoVictima"]?.ToString(),
                                        CorreoVictima = reader["CorreoVictima"]?.ToString(),
                                        DireccionVictima = reader["DireccionVictima"]?.ToString(),
                                        nombre_contacto_confianza = reader["nombre_contacto_confianza"]?.ToString(),
                                        telefono_contacto_confianza = reader["telefono_contacto_confianza"]?.ToString(),

                                        es_PARD = reader["es_PARD"]?.ToString() == "1" ? "SI" : "NO",
                                        tipo_presolicitud = reader["tipo_presolicitud"]?.ToString(),
                                        observacion_Legal = reader["observacion_Legal"]?.ToString(),
                                        denuncia_verificada = reader["denuncia_verificada"]?.ToString() == "1" ? "SI" : "NO",
                                        observacion_verificacion = reader["observacion_verificacion"]?.ToString(),
                                        continua_denuncia = reader["continua_denuncia"]?.ToString() == "1" ? "SI" : "NO",
                                        competencia_icbf = reader["competencia_icbf"]?.ToString() == "1" ? "SI" : "NO",
                                        observaciones_competencia_icbf = reader["observaciones_competencia_icbf"]?.ToString(),
                                        numero_documento_denunciante = reader["numero_documento_denunciante"]?.ToString(),
                                        nombres_denunciante = reader["nombres_denunciante"]?.ToString(),
                                        telefono_denunciante = reader["telefono_denunciante"]?.ToString(),
                                        correo_denunciante = reader["correo_denunciante"]?.ToString(),

                                        id_plantilla = long.TryParse(reader["id_plantilla"]?.ToString(), out long idPlantilla) ? idPlantilla : (long?)null,
                                        estado_plantilla = reader["estado_plantilla"]?.ToString(),
                                        observacion_plantilla = reader["observacion_plantilla"]?.ToString(),
                                        aprobado = reader["aprobado"]?.ToString() == "1" ? "SI" : "NO",
                                        apelacion = reader["apelacion"]?.ToString() == "1" ? "SI" : "NO",
                                        afecta_medidas = reader["afecta_medidas"]?.ToString() == "1" ? "SI" : "NO",

                                        estado_involucrado = reader["estado_involucrado"]?.ToString(),

                                        nombre_documento_anexo = reader["nombre_documento_anexo"]?.ToString(),
                                        fecha_creacion_anexo = DateTime.TryParse(reader["fecha_creacion_anexo"]?.ToString(), out DateTime fechaAnexo) ? fechaAnexo : (DateTime?)null,
                                        es_anexo_victima = reader["es_anexo_victima"]?.ToString() == "1" ? "SI" : "NO",

                                        nombre_usuario_anexo = reader["nombre_usuario_anexo"]?.ToString(),
                                        correo_usuario_anexo = reader["correo_usuario_anexo"]?.ToString(),
                                        cargo_usuario = reader["cargo_usuario"]?.ToString(),
                                        celular_usuario = long.TryParse(reader["celular_usuario"]?.ToString(), out long celular) ? celular : 0,
                                        perfil_usuario = reader["perfil_usuario"]?.ToString(),

                                        estado_tarea = reader["estado_tarea"]?.ToString(),
                                        fecha_creacion_tarea = DateTime.TryParse(reader["fecha_creacion_tarea"]?.ToString(), out DateTime fechaCreacionTarea) ? fechaCreacionTarea : (DateTime?)null,
                                        fecha_terminacion_tarea = DateTime.TryParse(reader["fecha_terminacion_tarea"]?.ToString(), out DateTime fechaTerminacionTarea) ? fechaTerminacionTarea : (DateTime?)null,
                                    };

                                    solicitudesPARD.Add(solicitud);
                                }

                                responseListaPaginada.DatosPaginados = solicitudesPARD;
                                responseListaPaginada.TotalRegistros = solicitudesPARD.Count;
                            }
                            else
                            {
                                // Tu lógica existente para ReporteSolicitudDTO
                                List<ReporteSolicitudDTO> solicitudes = new List<ReporteSolicitudDTO>();
                                while (reader.Read())
                                {
                                    if (reader.FieldCount >= 1)
                                    {
                                        ReporteSolicitudDTO solicitud = new ReporteSolicitudDTO
                                        {
                                            fecha_ingreso = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_ingreso"]),
                                            comisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["comisaria"]),
                                            historia = ConvertFDBVal.ConvertFromDBVal<string>(reader["historia"]),
                                            barrio = ConvertFDBVal.ConvertFromDBVal<string>(reader["barrio"]),
                                            direccion_ubicacion_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["direccion_ubicacion_involucrado"]),
                                            nombre_completo_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_completo_involucrado"]),
                                            tipo_documento_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento_involucrado"]),
                                            numero_documento_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento_involucrado"]),
                                            edad_involucrado = ConvertFDBVal.ConvertFromDBVal<int>(reader["edad_involucrado"]),
                                            tipo_violencia = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_violencia"]),
                                            descripcion_lugar_de_hechos = ConvertFDBVal.ConvertFromDBVal<string>(reader["descripcion_lugar_de_hechos"]),
                                            hora_hecho_violento = ConvertFDBVal.ConvertFromDBVal<TimeSpan>(reader["hora_hecho_violento"]),
                                            sexo_genero_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["sexo_genero_involucrado"]),
                                            identidad_genero_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["identidad_genero_involucrado"]),
                                            orientacion_sexual_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["orientacion_sexual_involucrado"]),
                                            etnia_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["etnia_involucrado"]),
                                            pais_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["pais_involucrado"]),
                                            victima_conflicto_armado_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["victima_conflicto_armado_involucrado"]),
                                            vicitma_es_poblacion_proteccion_especial = ConvertFDBVal.ConvertFromDBVal<string>(reader["vicitma_es_poblacion_proteccion_especial"]),
                                            discapacidad_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["discapacidad_involucrado"]),
                                            nivel_academico_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["nivel_academico_involucrado"]),
                                            ocupacion_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["ocupacion_involucrado"]),
                                            estrato_involucrado = ConvertFDBVal.ConvertFromDBVal<int>(reader["estrato_involucrado"]),
                                            hijos_involucrado = ConvertFDBVal.ConvertFromDBVal<int>(reader["hijos_involucrado"]),
                                            estado_embarazo_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado_embarazo_involucrado"]),
                                            afiliado_seguridad_social_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["afiliado_seguridad_social_involucrado"]),
                                            contexto_familiar_involucrado = ConvertFDBVal.ConvertFromDBVal<string>(reader["contexto_familiar_involucrado"]),
                                            convive_con_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["convive_con_agresor"]),
                                            nombre_completo_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_completo_agresor"]),
                                            tipo_documento_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo_documento_agresor"]),
                                            numero_documento_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["numero_documento_agresor"]),
                                            edad_agresor = ConvertFDBVal.ConvertFromDBVal<int>(reader["edad_agresor"]),
                                            sexo_genero_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["sexo_genero_agresor"]),
                                            identidad_genero_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["identidad_genero_agresor"]),
                                            etnia_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["etnia_agresor"]),
                                            ocupacion_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["ocupacion_agresor"]),
                                            nivel_academico_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["nivel_academico_agresor"]),
                                            hijos_agresor = ConvertFDBVal.ConvertFromDBVal<int>(reader["hijos_agresor"]),
                                            pertenece_grupo_armado_agresor = ConvertFDBVal.ConvertFromDBVal<string>(reader["pertenece_grupo_armado_agresor"]),
                                            medida_proteccion_otorgada_ley_575_2000 = ConvertFDBVal.ConvertFromDBVal<string>(reader["medida_proteccion_otorgada_ley_575_2000"]),
                                            fecha_audiencia = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_audiencia"]),
                                            estado = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado"]),
                                            fecha_estado_proceso = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_estado_proceso"]),
                                            observaciones = ConvertFDBVal.ConvertFromDBVal<string>(reader["observaciones"])
                                        };
                                        solicitudes.Add(solicitud);
                                    }
                                }
                                responseListaPaginada.DatosPaginados = solicitudes;
                                responseListaPaginada.TotalRegistros = solicitudes.Count;
                            }
                        }
                        _connectionDb.Close();
                    }
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



