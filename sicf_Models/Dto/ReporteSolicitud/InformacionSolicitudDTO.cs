namespace sicf_Models.Dto.ReporteSolicitud
{
    [Serializable]
    public class InformacionSolicitudDTO
    {
        public DateTime? fechaRegistro { get; set; }
        public string? codigoSolicitud { get; set; }
        public string? comisaria { get; set; }

        public string? direccioncomisaria { get; set; }
        public string? nombreCompletoFuncionario { get; set; }
        public string? codigoTipoDocumentoFuncionario { get; set; }
        public string? tipoDocumentoFuncionario { get; set; }
        public string? numeroDocumentoFuncionario { get; set; }
        public string? cargoFuncionario { get; set; }
        public string? correoElectronicoFuncionario { get; set; }
        public long contactoTelefonoFijoFuncionario { get; set; }
        public long contactoCelularFuncionario { get; set; }
                      
        public string? nombreCompletoInvolucrado { get; set; }
        public string? fechaNacimientoInvolucrado { get; set; }
        public int? edadInvolucrado { get; set; }
        public string? codigoTipoDocumentoInvolucrado { get; set; }
        public string? tipoDocumentoInvolucrado { get; set; }
        public string? numeroDocumentoInvolucrado { get; set; }
        public string? fechaExpedicionDocInvolucrado { get; set; }
        //public string? lugarExpedicionDocInvolucrado { get; set; }
        public string? codigoPaisInvolucrado { get; set; }
        public string? paisInvolucrado { get; set; }
        public string? codigoDepartamentoInvolucrado { get; set; }
        public string? departamentoInvolucrado { get; set; }
        public string? codigoCidudadInvolucrado { get; set; }
        public string? ciudadMunicipioInvolucrado { get; set; }
        public string? correoElectronicoInvolucrado { get; set; }
        public string? contactoFijoInvolucrado { get; set; }
        public string? contactoConfianzaInvolucrado { get; set; }
        public string? direccionUbicacionInvolucrado { get; set; }
        public string? sexoGeneroInvolucrado { get; set; }
        public string? identidadGeneroInvolucrado { get; set; }
        public string? orientacionSexualInvolucrado { get; set; }
        public string? nivelAcademicoInvolucrado { get; set; }
        public string? vicitmaEsPoblacionProteccionEspecial { get; set; }
        public string? victimaPoneHechos { get; set; }
        public string? rol { get; set; }

        #region Datos del denunciante
        //public string? codigoTipoDocumentoDenunciante { get; set; }
        //public string? tipoDocumentoDenunciante { get; set; }
        //public string? numeroDocumentoDenunciante { get; set; }
        //public string? fechaExpedicionDocDenunciante { get; set; }
        //public string? lugarExpedicionDocDenunciante { get; set; }
        //public string? codigoPaisDenunciante { get; set; }
        //public string? paisDenunciante { get; set; }
        //public string? codigoDepartamentoDenunciante { get; set; }
        //public string? departamentoDenunciante { get; set; }
        //public string? codigoCiudadDenunciante { get; set; }
        //public string? ciudadMunicipioDenunciante { get; set; }
        //public string? correoElectronicoDenunciante { get; set; }
        //public string? contactoFijoDenunciante { get; set; }
        //public string? direccionUbicacionDenunciante { get; set; }
        #endregion

        public string? tipoViolencia { get; set; }
        public string? descripcionDeHechos { get; set; }
        public DateTime fechaHechoViolento { get; set; }
        public TimeSpan horaHechoViolento { get; set; }
        public string? DescripcionLugareHechos { get; set; }
    }


    [Serializable]
    public class ReporteSolicitudDTO
    {
        public DateTime? fecha_ingreso { get; set; }
        public string? comisaria { get; set; }
        public string? historia { get; set; }
        public string? barrio { get; set; }
        public string? direccion_ubicacion_involucrado { get; set; }
        public string? nombre_completo_involucrado { get; set; }
        public string? tipo_documento_involucrado { get; set; }
        public string? numero_documento_involucrado { get; set; }
        public int? edad_involucrado { get; set; }
        public string? tipo_violencia { get; set; }
        public string? descripcion_lugar_de_hechos { get; set; }
        public TimeSpan? hora_hecho_violento { get; set; }
        public string? sexo_genero_involucrado { get; set; }
        public string? identidad_genero_involucrado { get; set; }
        public string? orientacion_sexual_involucrado { get; set; }
        public string? etnia_involucrado { get; set; }
        public string? pais_involucrado { get; set; }
        public string? victima_conflicto_armado_involucrado { get; set; }
        public string? vicitma_es_poblacion_proteccion_especial { get; set; }
        public string? discapacidad_involucrado { get; set; }
        public string? nivel_academico_involucrado { get; set; }
        public string? ocupacion_involucrado { get; set; }
        public int? estrato_involucrado { get; set; }
        public int? hijos_involucrado { get; set; }
        public string? estado_embarazo_involucrado { get; set; }
        public string? afiliado_seguridad_social_involucrado { get; set; }
        public string? contexto_familiar_involucrado { get; set; }
        public string? convive_con_agresor { get; set; }
        public string? nombre_completo_agresor { get; set; }
        public string? tipo_documento_agresor { get; set; }
        public string? numero_documento_agresor { get; set; }
        public int? edad_agresor { get; set; }
        public string? sexo_genero_agresor { get; set; }
        public string? identidad_genero_agresor { get; set; }
        public string? etnia_agresor { get; set; }
        public string? ocupacion_agresor { get; set; }
        public string? nivel_academico_agresor { get; set; }
        public int? hijos_agresor { get; set; }
        public string? pertenece_grupo_armado_agresor { get; set; }
        public string? medida_proteccion_otorgada_ley_575_2000 { get; set; }
        public DateTime? fecha_audiencia { get; set; }
        public string? estado { get; set; }
        public DateTime? fecha_estado_proceso { get; set; }
        public string? observaciones { get; set; }
    }


    // AÑADE ESTE NUEVO DTO AQUÍ
    [Serializable]
    public class ReporteSolicitudPARDDTO
    {
        public long id_solicitud_servicio { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public string estado_solicitud { get; set; }
        public string codigo_solicitud { get; set; }
        public long? id_comisaria { get; set; } // CAMBIO: de int a long?
        public string nombre_comisaria { get; set; }
        public string descripcion_de_hechos { get; set; }
        public string tipo_solicitud { get; set; }
        public string subestado_solicitud { get; set; }

        // Datos de la víctima principal
        public string NombreVictima { get; set; }
        public string DocumentoVictima { get; set; }
        public string TipoDocumentoVictima { get; set; }
        public string GeneroVictima { get; set; }
        public string OrientacionSexualVictima { get; set; }
        public int? EdadVictima { get; set; } // Lo mantengo como int? a menos que se confirme que es BIGINT
        public string TelefonoVictima { get; set; }
        public string CorreoVictima { get; set; }
        public string DireccionVictima { get; set; }
        public string nombre_contacto_confianza { get; set; }
        public string telefono_contacto_confianza { get; set; }

        // Datos de la solicitud complementaria si es PARD
        public string es_PARD { get; set; } // 'SI' o 'NO'
        public string tipo_presolicitud { get; set; }
        public string observacion_Legal { get; set; }
        public string denuncia_verificada { get; set; } // 'SI' o 'NO'
        public string observacion_verificacion { get; set; }
        public string continua_denuncia { get; set; } // 'SI' o 'NO'
        public string competencia_icbf { get; set; } // 'SI' o 'NO'
        public string observaciones_competencia_icbf { get; set; }
        public string numero_documento_denunciante { get; set; }
        public string nombres_denunciante { get; set; }
        public string telefono_denunciante { get; set; }
        public string correo_denunciante { get; set; }

        // Datos de plantilla
        public long? id_plantilla { get; set; } // CAMBIO: de int? a long?
        public string estado_plantilla { get; set; }
        public string observacion_plantilla { get; set; }
        public string aprobado { get; set; } // 'SI' o 'NO'
        public string apelacion { get; set; } // 'SI' o 'NO'
        public string afecta_medidas { get; set; } // 'SI' o 'NO'

        // Estado involucrado en plantilla-sección
        public string estado_involucrado { get; set; }

        // Datos del anexo principal
        public string nombre_documento_anexo { get; set; }
        public DateTime? fecha_creacion_anexo { get; set; } // Puede ser NULL
        public string es_anexo_victima { get; set; } // 'SI' o 'NO'

        // Datos del usuario que subió el anexo
        public string nombre_usuario_anexo { get; set; }
        public string correo_usuario_anexo { get; set; }
        public string cargo_usuario { get; set; }
        public long celular_usuario { get; set; }
        public string perfil_usuario { get; set; }

        // Datos de tarea
        public string estado_tarea { get; set; }
        public DateTime? fecha_creacion_tarea { get; set; } // Puede ser NULL
        public DateTime? fecha_terminacion_tarea { get; set; } // Puede ser NULL
    }
}
