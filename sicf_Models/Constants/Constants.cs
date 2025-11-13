using Microsoft.Extensions.Primitives;
using sicf_Models.Dto.Comisaria;

namespace sicf_Models.Constants
{
    public static class Constants
    {

        public const string FormatoFecha = "dd/MM/yyyy HH:mm:ss";
        public const string FormatoFechaCorta = "dd/MM/yyyy";
        public const string FormatoFechaCorta2 = "{0:dd/MM/yyyy}";
        public const string FormatoSoloHora = "HH:mm:ss";

        public struct Message
        {

            #region CitaController
            public const string Error_GetCiudadesMunicipios_depID_Requerido = "Departamento ID es requerido!";
            public const string duplicadoFecha = "Fecha duplicada";
            #endregion


            #region Generic Message

            public const string ErrorInterno = "Error Interno";
            public const string Ok = "Se ejecuta petición correctamente";
            public const string ErrorRequest = "Error en el envío de parámetros";
            public const string DescFallo = "Ha fallado la solicitud";
            public const string Sindatos = "No existen registros asociados con el indentificador";
            public const string ErrorGenerico = "En estos momentos presentamos inconvenientes en la comunicación del sistema, intenta más tarde o espera 5 minutos para volver a intentarlo";
            public const string PermisoDenegado = "Permiso Denegado para ejecutar la actividad";
            public const string registroExito = "registro exitoso";
            public const string Vacio = "VACIO";
            public const string ErrorFechas = "Inconsistencia en las fechas";
            #endregion

            #region Validaciones
            public const string NoEsPDFArchivo = "Error: El archivo no esta en el formato PDF, por favor cargar un archivo con ext .pdf !!";
            public const string PdfFileNoexiste = "Error: El archivo pdf no existe en el Blog Storage !!";
            public const string NombresApellidosRequeridos = "Para poder buscar por nombre y apellidos es necesariocompleto!!!";
            public const string FechaNOValida = "Error, fecha formato no valido para el sistema, formato de fecha valido dd/MM/yyyy HH:mm:ss!!";
            public const string RequeridoRequestRecomdSegRedApoyoEntExt = "Error: almenos una opción de Seguridad o Redes de Apoyo debe ser seleccionada.";
            public const string RequeridoPdfFileName = "Error: El nombre del archivo PDF es requerido";
            public const string FechaInicialMayorFinal = "Error: Fecha inicial es mayor que la Fecha final";
            public const string FechaMenorAActual = "Error: fecha inicial o final no puede ser menor que la fecha actual";
            public const string HoraInicialMayorIgualFinal = "Error: Hora inicial es mayor o igual que la Hora final";
            public const string FechaFutura = "Error: Deben ser fechas y horas futuras";
            public const string HorarioNoDisponible = "Error: No existe disponibilidad a esa hora";
            public const string SolicitudNoexiste = "Error: La solicitud no existe";
            public const string CodigoComisariaExiste = "Error: Ya existe una comisaría con el código ingresado.";
            public const string NombreComisariaExiste = "Error: Ya existe una comisaría con el nombre ingresado.";
            public const string CorreoComisarioExiste = "Error: Ya existe un usuario con el correo ingresado.";
            public const string IdentificacionComisarioExiste = "Error: Ya existe un usuario con el numero de identificación ingresado.";
            #endregion

            #region Comisarias

            public const string ministerioCreado = "Se ha creado el ministerio y el super administrador correctamente";
            public const string ministerioFallo = "Ocurrió un error al crear el ministerio";
            public const string comisariasNoCreadas = "No se crearon algunas comisarias de la petición.";

            #endregion Comisarias
        }

        public struct PerfilCodigo
        {
            public const string comisario = "COM";
            public const string Comisario = "Comisario";
            public const string ComisariaNoidentificada = "Comisaria no identificada";
        }


        public struct TareaEstados
        {
            public const string TipoDominio = "Estado_Tarea";
            public const string PENDIENTE = "PENDIENTE";
            public const string TERMINADO = "TERMINADO";
            public const string EJECUCION = "EJECUCION";
            public const string AJUSTAR = "AJUSTAR";


        }

        public struct SolicitudServicioEstados
        {
            public const string abierto = "ABIERTO";
            public const string cerrado = "CERRADO";
        }

        public struct SolicitudServicioSubEstados
        {
            public const string proceso = "EN PROCESO";
            public const string apelado = "APELADO";
            public const string sinDenuncia = "SIN DENUNCIA";
            public const string citado = "CITADO";
            public const string seguimiento = "SEGUIMIENTO";
            public const string levantada = "LEVANTADA";
            public const string icbf = "COMPETENCIA ICBF";
            public const string adulto_mayor = "COMPETENCIA PSI";
        }

        public struct FlujoEstados
        {
            public const string INICIO = "INICIO";
            public const string INTERMEDIO = "INTERMEDIO";

        }

        public struct Plantillas
        {
            public const string ejecucion = "EJECUCION";
            public const string terminado = "TERMINADO";
        }

        public struct CodigoProceso
        {
            public const string GeneracionCaso = "GCASO";
            public const string Seguimiento = "SEG";
            public const string LevantamientoMedidas = "LEVMED";
            public const string RestablecimientoDerechos = "PARD";

        }

        public struct CodigoPefil
        {
            public const string Psicologo = "PSI";
            public const string Comisario = "COM";
            public const string TrabajadorSocial = "TSO";
            public const string Abogado = "ABO";

            public const string AdministradorDeSistema = "ADS";
            public const string Auxiliar = "AUX";

        }

        public struct Path
        {
            public const string PscologiaResumen = "/psicologia/resumen/";
            public const string AbogadoResumen = "/abogado/resumen/";
            public const string ComisarioResumen = "/comisario/resumen/";



        }

        public struct Apelacion
        {
            public const string estadoRegistro = "EJECUCION";
            public const string estadoCierre = "TERMINADO";
        }

        public struct Evaluacion
        {
            public const string motivo = "Motivo";

            public const string antecedente = "Antecedentes importantes y situación actual";

            public const string metodologia = "metodología";

            public const string relato = "Relato de los hechos";

            public const string conclusiones = "Conclusion y Recomendaciones";

            public const string recomendacion = "La recomendacion ha sido creada";

            public const string redapoyo = "Red_apoyo";

            public const string percepcion = "Persistencia";

            public const string seguridad = "Seguridad";

            public const string redApoyoExterno = "Red_apoyo_externo";
             
            public const string informacionMenores = "informacion_menores";

        }





        public struct Riesgo
        {

            public const string riesgoBajo = "Riesgo Bajo";

            public const string riesgoMedio = "Riesgo Medio";

            public const string riesgoAlto = "Riesgo Alto";

            public const string riesgoNoCalculado = "Riesgo no calculado";

            public const int bajo = 20;

            public const int medio = 40;

            public const int totalPreguntas = 52;



        }


        public struct ErrorRespuestaEvaluacionRiesgo
        {

            public const string errorRespuestas = "Error al crear la respuestas";

            public const string solicitudNoexiste = "La solicitud con el identificador ingresado no existe";

            public const string totalidadRespuestas = "Se deben diligenciar el totalidad de preguntas para dar la evaluacion de riesgo";

            public const string cantidadHijos = "No se puede hacer el registro debido a que la infomación de hijos es inconsistente";

            public const string cantidadPreguntas = "Se deben contestar todas las preguntas de evaluacion de riego";

            public const string errorEvaluacionPsicologica = "No existe ningun registro asociado a la solicitud de servicio";





        }



        public struct Tarea
        {
            public struct Mensajes
            {
                public const string errorConfiguracionflujo = "No existe un flujo de tareas configurado, comuníquese con el administrador del sistema";
                public const string errorConfiguracionperfil = "No existe un perfil de actividad configurado, comuníquese con el administrador del sistema";
                public const string errorConsultaTarea = "No existe la tarea consultada.";
                public const string errorValidarFlujo = "No se pudo validar el flujo.";
                public const string errorCerrarActuacion = "No se pudo cerrar la actuacion.";

            }

            public struct StoredProcedure
            {
                public const string ValidarFlujoTarea = "PR_SICOFA_VALIDAR_FLUJO_TAREA";
                public const string CerrarActuacion = "PR_SICOFA_CERRAR_ACTUACION";
            }

            public struct etiqueta
            {
                public const string estadoActivo = "ABIERTA";
                public const string estadoCerrado = "CERRADO";
                public const string AutorizacionMedidas = "AUTMED";


            }

        }


        public struct FormatoHora
        {
            public const string ZonaHorariaColombiaLinux = "America/Bogota";

            public const string ZonaHorariaColombiaWindows = "SA Pacific Standard Time";

            public const string hora = "hh:mm tt";

            public const string espacioFormato = " ";

            public const string formaTo24 = "HH:mm";

        }

        public struct EvaluacionPsicologicaEmocional
        {
            public const string Seguridad = "Seguridad";

            public const string RedApoyoExterno = "Red_apoyo_externo";

            public const string creado = "Evaluacion Psicologica Actualizada";

            public const string evaluacionPsicologicaError = "No existe una evaluacion psicologica asociada ha esta tarea"; 


        }

        public struct Cuestionario
        {

            public const int circunstanciaAgrevantes = 7;

            public const int persepcionVictima = 8;


        }



        public struct Medidas
        {
            public const int CodMedidaProteccion = 1;
            public const int CodMedidaProteccionEntidad = 2;
            public const int CodMedidaAtencion = 3;
            public const int CodMedidaEstabilizacion = 4;
            public const int CodMedidaPard = 7;
            public const string descMedidaProteccion = "Medidas de Protección";
            public const string descMedidaProteccionEntidad = "Medidas de Protección Entidad";
            public const string descMedidaAtencion = "Medidas de Atención";
            public const string descMedidaEstabilizacion = "Medidas de Estabilización";

            public const int medidaProteccionPlantilla = 5;
            public const int medidaAtencionPlantilla = 10;
            public const  int medidaEstablizacionPlantilla = 13;

            public const int medidaProvisionalesPard = 495;

          



            public struct Estados
            {

                public const string provisional = "PROVISIONAL";
                public const string seguimiento = "SEGUIMIENTO";
                public const string revocada = "REVOCADA";
                public const string noAplica = "NO APLICA";

            }

            public struct EstadosPruebasPard
            {
                public const string porEjecutar = "POR EJECUTAR";
                public const string desistir = "DESISTIR";
            }

            public struct Seguimiento
            {
                public struct Mensajes
                {
                    public const string NoHaySeguimientos = "No se encontraron Seguimientos.";
                    public const string NoHayProgramaciones = "No se encontraron Programaciones.";
                    public const string noHayplantilla = "No se encontro plantilla en ejecuccion";

                }

                public struct estados
                {
                    public const string Abierto = "ABIERTO";
                    public const string parcialmenteCumplida = "PARCIALMENTE CUMPLIDA";
                    public const string sinVerificar = "SIN VERIFICAR";
                    public const string cumple = "CUMPLE";
                    public const string noCumple = "NO CUMPLE";
                    public const string prorroga = "PRORROGA";
                }

                public struct etiquetas
                {
                    public const string actividadEjecutarSeguimiento = "EJEC-SEG";
                    public const string actividadProgramarSeguimiento = "LEGSEG";
                    public const string actividadProgramarSeguimientoPard = "AUDSPARD";
                    public const string actividadEjecutarSeguimientoPard = "PARD-SEG";
                }
            }


        }

        public struct Auto
        {

            public const string SeccionPadre = "1";
            public const string sinMedidaPruebas = "0";
            public const string estadoActivoSeccion = "0";
            public const string descAgresor = "Accionado";
            public const string descVictima = "Accionante";
            public const string CodigoActRealizarFallo = "FALLOCVF";
            public const string CodigoDocAuto = "AUTOCVF";
            public const string CodigoDocFallo1 = "FALLOCVF1";
            public const string CodigoDocFallo2 = "FALLOCVF2";
            public const string CodigoDocFallo3 = "FALLOCVF3";
            public const string CodigoDocFallo4 = "FALLOCVF4";
            public const string CodigoEstadoTerminado = "TERMINADO";
            public const string Observacion = "MEDIDA AÑADIDA POR ";
            public const string MensajeErrorSinProgramacion = "La solicitud de servicio no contiene una programación de audiencia.";
            public const string Espacio = "_________";

        }

        public struct ReportesRemision
        {
            public const int cantidadIvolucrados = 2;
            public const string victima = "No registra victima";
            public const string agresor = "No registra Agresor";

            public const string medicinalegal = "Oficio_Remisorio_Medicina_legal";
            public const string secretariaMujer = "Remision_secretaria_de_Bienestar_u_otro_organo";
            public const string remisionProcesoPsicologia = "Remision_Proceso_Psicologia_Externa";
            public const string remisionApoyoPolicivo = "Remision_Apoyo_Policivo_Victima_Mujer";
            public const string remisionProcesoExterno = "Remision_Proceso_Psicologia_Externa";
            public const string recepcionFiscalia = "Recepcion_Denuncia_Fiscalia";
            public const string remisionVisitaDomiciliario = "Remision_Visita_domiciliaria";
            public const string solicitudRegimenSalud = "Solicitud_afiliacion_Regimen_de_salud";
            public const string solicitudProtocoloRiesgo = "Solicitud_Protocolo_de_Riesgo";
            public const string solicitudHistoria = "Solicitud_Historia_Clinica";

            public const string faltaCantidadInvolucrados = "No se registran ambos involucrados";

            public const string RemisionFormatoPolicia = "Remision_Formato_a_policia";
            public const string RemisionSistemaSalud = "Remision_Formato_Sistema_de_Salud";
            public const string RemisionFormatoPersoneria = "Remision_Formato_Personeria";

            public const string RemisionTratamientoTerapeutico = "Remision_Tratamiento_Terapeutico";

            public const string SolicitudEvaluacionRiesgoRemisionesNNA = "Solicitud_Evaluacion_del_Riesgo_Remisiones_NNA";

            public const string estadoActivo = "ACTIVO";



        }

        public struct CargaDocumento
        {


            public const string noSolicitud = "No existe una solicitud con el id";

            public const string noTipoDocumento = "No existe el tipo de documento asociado";

            public const string extensionPDf = ".pdf";

            public const string noDocumento = "No esta asociado el documento";

            public const string blobStorage = "sicofadev";

            public const string documentoCargado = "Se ha cargado el documento";
        }

        public struct Formato
        {
            public const string noExisteFormato = "No existe un formato con este nombre o codigo.";
        }

        public struct programacion
        {
            public const string estadoDisponible = "DISPONIBLE";
            public const string estadoNoDisponible = "NODISPONIBLE";
            public const string etiquetaAudiencia = "PROG-AUD";
            public const string etiquetaSeguimiento = "PROG-SEG";
            public const string errorConfiguracionEtiqueta = "No se encuentra una etiqueta configurada para el siguiente flujo.";
        }


        public struct CargaRemisiones
        {


            public const string noInvolucrados = "No existen involucrados asociados ha está solicitud de servicio";
        }

        public struct Notificacion
        {

            public const string codigoNoficiacion = "NOTI";
            public const string tipoEstado = "Estado_Notificacion";
            public const string codigoEstadoInicial = "EN01";
            public const string enviada = "EN03";
            public const string noEnvidad = "No enviado";
            public const string recibido = "EN02";

            public const string incidenteIncumplimiento = "incidente_incumplimiento";
            public const string notificacionMedioProteccion = "notificacion_medida_de_proteccion";
            public const string constanciaMedidaProteccion = "constancia medida proteccion";
            public const string notificacionporaviso = "notificacion_por_aviso_de_medida_de_proteccion";
            public const string notificacionpersional = "notificacion_personal_de_la_medida_proteccion";
            public const string notificacionError = "No existe una notificación para actualizar";
            public const string involucradosNotificar = "No existen involucrados a notificar";
            public const string notificacionNoExiste = "No existe una notificacion";

        }

        public struct EliminacionAnexo
        {
            public const string remision = "REMI";

            public const string notiticacion = "NOTI";

            public const string seguimiento = "SEG";


            public const string identificacion = "IDERIESGO";

            public const string incumedida = "INCUMEDIDA";



        }

        public struct cPruebaSolicitud
        {

            public const string periciales = "pruebas_periciales";

            public const string pruebaAccionanteAccionado = "pruebas_accionante_accionado";

            public const string pruebaJuez = "prueba_juez";
        }

        public struct EstadosQuorum
        {

            public const int noAsiste = 0;

            public const int asiste = 1;

            public const int excusaConJustaCausa = 2;

            public const int excusaSinJustaCausa = 3;

        }

        public struct QuorumEstados
        {
            public const string disponible = "DISPONIBLE";
            public const string noDisponible = "NO DISPONIBLE";
        }

        public struct MDecisionJuez
        {

            public const string decisionJuez = "Se ha registrado la decision del juez";
        }


        public struct ErrorNotificacion
        {

            public const string noNotificacion = "El tipo documento no existe";
        }

        public struct ReportesSeguimientos
        {

            public const string FormatoSeguimientoMedidasProteccion = "Formato_Seguimiento_Medidas_Proteccion";
            public const string InstrumentoVerificacionEfectividadMedidaDeProteccion = "Instrumento_Verificacion_De_La_Efectividad_De_La_Medida_De_Proteccion";
            public const string InstrumentoSeguimientoMedidasDeAtencion = "Instrumento_Para_El_Seguimiento_A_Las_Medidas_De_Atencion";
            public const string ConstanciaSeguimientoContactoTelefonico = "Constancia_De_Seguimiento_Contacto_Telefonico";
            public const string InformeSeguimientoEntrevistaInterventiva = "Informe_De_Seguimiento_Entrevista_Interventiva";
            public const string AutoOrdenandoVisitaDomiciliaria = "Auto_Ordenando_Visita_Domiciliaria";
        }

        public struct PerfilesConstantes
        {

            public const int Comisario = 2;
            public const string codComisario = "COM";
            public const string codAdministrador = "ADM";

            public const string CodigoAdministrador = "ADM";

            public const bool EstadoActivo = true;
        }

        public struct DominioMensajes
        {

            public const string errorPrevio = "Dominio registrado previamente";

            public const string creado = "Dominio Creado";

            public const string editado = "Dominio editado";


        }

        public struct UsuarioMensaje
        {

            public const string usuarioNoidentificado = "El usuario no se encuentra registrado";

            public const string noPerfil = "Por lo menos se requiere un perfil";

            public const string noRefresh = "No se puede crear un nuevo token, intente ingresando nuevamente";

            public const string usuarioRegistrado = "Ya existe un usuario registrado con ese correo electronico";

            public const string noCambioclave = "No se puede realizar el cambio de contraseña";

            public const string cambioContrasena = "Se a realizado el cambio de contraseña con exito";
        }

        public struct ComisariaMensaje
        {

            public const string noComisaria = "No se encuentra una comisaria con esté id";

            public const string comisariaCreada = "Comisaria Creada";

            public const string comisariaActualiza = "Comisaria Actualizada";
        }

        public struct CitaMensaje
        {

            public const string tipogendamiento = "Agendada Web";


        }

        public struct InfoComisario
        {

            public const string creacionComisario = "el comisario ha sido creado";
        }

        public struct LoginMensaje
        {
            public const string noUsuario = "No existe un usuario con ese correo electronico";

            public const string contrasenaUsada = "La contraseña ha sido usada previamente , Intente con otra nuevamente";
        }


        public struct Politicas
        {
            public const string politicaComisario = "COM";
        }

        public struct Pard
        {
            public struct Mensajes
            {
                public const string errorActualizarPruebas = "No se pudo actualizar las pruebas";
                public const string errorAnexoPruebasPard = "No se pudo crear o actualizar el Anexo";
                public const string errorActualizarMedidas = "No se pudo actualizar las medidas";
            }

            public struct tiposDocumentoAnexo
            {
                public const string AnexoPruebasPard = "Anexo_Pruebas_Pard";
            }
         }

        public struct VictimaReporte {

            public const int seccionReporteVictima = 231;
            public static readonly string[] interPolacionVictima = { "<victima>" , "<cedulaVictima>" };
        }

    }
}