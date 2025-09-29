using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto;
using sicf_Models.Dto.EvaluacionPsicologica;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.EvaluacionPsicologica
{
    public class EvaluacionPsicologicaRepository : IEvaluacionPsicologicaRepository
    {

        private readonly SICOFAContext context;
        //private readonly IMapper mapper;

        public EvaluacionPsicologicaRepository(SICOFAContext context) {

            this.context = context;
           
            
        }



        #region H2

        // accionate 
        public AccionanteDTO IdentificarAccionante(long idSolicitudProceso)
        {

            var solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitudProceso).FirstOrDefault()!;

            //var dem = context.SicofaSolicitudServicioEstadoSolicitud.Where(s => s.IdSolicitudServicio == idSolicitudProceso).OrderByDescending(s => s.IdSolicitudServicio).First();

           
            var involucrados = context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitudProceso).FirstOrDefault();

           AccionanteDTO salida = new AccionanteDTO();
            salida.codigoSolicitudServicio = solicitud.CodigoSolicitud;
            salida.estadoCaso = solicitud.EstadoSolicitud!; ;
            salida.nombreAccionante = involucrados.IdInvolucrado.Where(s => s.EsPrincipal == true && s.EsVictima == true).Select(s => $"{s.PrimerNombre} {s.SegundoNombre} {s.PrimerApellido} {s.SegundoApellido}").First();

;           foreach (var victima in involucrados.IdInvolucrado) {

                if (victima.EsVictima) { 
                
                salida.victimas.Add($"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}");
                }
;            }
            return salida;
        }

        public DatosInstitucionesDTO IdentificarDatosInstitucionales(long id) {

            SicofaSolicitudServicio solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == id).FirstOrDefault()!;

            var response = (from comisaria in context.SicofaComisaria
                       join municipio in context.SicofaCiudadMunicipio on comisaria.IdCiudadMunicipio equals municipio.IdCiudadMunicipio
                       join departamento in context.SicofaDepartamento on municipio.IdDepartamento equals departamento.IdDepartamento
                       where comisaria.IdComisaria == solicitud.IdComisaria
                       select Tuple.Create(comisaria.Nombre, municipio.Nombre, departamento.Nombre)
                       ).First();

            DatosInstitucionesDTO salida = new DatosInstitucionesDTO();
            salida.departamentoComisaria = response.Item3;
            salida.municipioComisaria = response.Item2;
            salida.comisaria = response.Item1;
            salida.fechaEntrevista = solicitud.FechaSolicitud;
            salida.numeroExpediente = solicitud.CodigoSolicitud;

            return salida;

        }

        public InformacionVictimaDTO ObtenerInvolucrado(long id, bool esvictima, bool principal) 
        {
            try
            {
                var solicitud = context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio == id).First();

                var involucrado = solicitud.IdInvolucrado.Where(s => s.EsPrincipal == principal && s.EsVictima == esvictima).First();

                InformacionVictimaDTO salida = new InformacionVictimaDTO();

                if ((bool)involucrado.EsPrincipal! & (bool)involucrado.EsVictima)
                {

                    SicofaCiudadano ciudadano = context.SicofaCiudadano.Where(c => c.NumeroDocumento == involucrado.NumeroDocumento).First();

                    var complementario = context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).FirstOrDefault();
                    salida.id = involucrado.IdInvolucrado;
                    salida.nombres = involucrado.Nombres;
                    salida.primerNombre = involucrado.PrimerNombre;
                    salida.segundoNombre = involucrado.SegundoNombre;
                    salida.primerApellido = involucrado.PrimerApellido;
                    salida.segundoApellido = involucrado.SegundoApellido;
                    salida.apellidos = involucrado.Apellidos;
                    salida.fechaNacimiento = involucrado.FechaNacimiento;
                    salida.fechaExpedicion = involucrado.FechaExpedicion;
                    salida.identidadGenero = involucrado.IdGenero;
                    salida.numeroDocumento = involucrado.NumeroDocumento;
                    salida.tipoDocumento = involucrado.TipoDocumento;
                    salida.sexo = involucrado.IdSexo;
                    salida.idGenero = involucrado.IdGenero;
                    salida.idEscolaridad = involucrado.IdNivelAcademico;
                    salida.idRegimen = involucrado.IdRegimen;
                    salida.ocupacion = complementario != null ? complementario.Ocupacion : null;
                    salida.numeroHijos = complementario != null ? complementario.NumeroHijos : null;
                    salida.agresorConflicto = complementario != null ? complementario.AgresorGrupoArmado : null;
                    salida.agresorconflictoDescripcion = complementario != null ? complementario.DescripcionGrupoArmado : null;
                    salida.descripcionRelacionAgresor = complementario != null ? complementario.DescripcionRelacionAgresor : null;
                    salida.idDiscapacidad = involucrado.IdTipoDiscpacidad;
                    salida.descripcionDiscapacidad = complementario != null ? complementario.DescripcionDiscapacidad : null;
                    salida.cultura = complementario != null ? complementario.IdCultura : null;
                    salida.embarazo = involucrado.EstadoEmbarazo;
                    salida.mesesEmbarazo = complementario != null ? complementario.MesesEmbarazo : ciudadano.MesesEmbarazo;
                    salida.victimaConflicto = involucrado.VictimaConflictoArmado;
                    salida.ips = involucrado.Ips;
                    salida.eps = involucrado.Eps;
                    salida.id = involucrado.IdInvolucrado;
                    salida.edadAproximadaAgresor = involucrado.Edad;
                    salida.DireccionRecidencia = involucrado.DireccionRecidencia;
                    salida.Telefono = involucrado.Telefono;

                    //salida.lugarExpedicion = involucrado.IdLugarExpedicion;
                    salida.paisExp = involucrado.IdPaisExpedicion;
                    salida.departamentoExp = involucrado.IdDepartamentoExpedicion;
                    salida.municipioExp = involucrado.IdMunicipioExpedicion;
                    
                    //salida.lugarNacimiento = involucrado.IdLugarNacimiento;
                    salida.paisNacimiento= involucrado.IdPaisNacimiento;
                    salida.departamentoNacimiento= involucrado.IdDepartamentoNacimiento;
                    salida.municipioNacimiento = involucrado.IdMunicipioNacimiento;

                    salida.relacionAgresor = involucrado.IdTipoRelacion;
                    salida.relacionPareja = complementario != null ? complementario.RelacionPareja : null;

                    var hijos = context.SicofaHijoinvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).ToList();

                    if (hijos.Count >= 1)
                    {
                        foreach (var hijo in hijos)
                        {

                            informacionHijo hijoSalida = new informacionHijo();
                            hijoSalida.nombres = hijo.Nombres;
                            hijoSalida.edad = hijo.Edad;
                            hijoSalida.edadEn = hijo.EdadEn;
                            hijoSalida.sexo = hijo.IdSexo;
                            hijoSalida.custodia = hijo.Custodia;
                            hijoSalida.relacionParental = (int) hijo.IdRelacionParental;

                            salida.hijos.Add(hijoSalida);
                        }
                    }
                }
                else
                {
                    // flujo para agresor
                    //TODO: Eliminar nombres y apellidos
                    var complementario = context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).FirstOrDefault();

                    salida.id = involucrado.IdInvolucrado;
                    salida.nombres = involucrado.Nombres;
                    salida.primerNombre = involucrado.PrimerNombre;
                    salida.segundoNombre = involucrado.SegundoNombre;
                    salida.primerApellido = involucrado.PrimerApellido;
                    salida.segundoApellido = involucrado.SegundoApellido;
                    salida.apellidos = involucrado.Apellidos;
                    salida.fechaNacimiento = involucrado.FechaNacimiento;
                    salida.fechaExpedicion = involucrado.FechaExpedicion;
                    salida.identidadGenero = involucrado.IdGenero;
                    salida.numeroDocumento = involucrado.NumeroDocumento;
                    salida.tipoDocumento = involucrado.TipoDocumento;
                    salida.sexo = involucrado.IdSexo;
                    salida.idGenero = involucrado.IdGenero;
                    salida.idEscolaridad = involucrado.IdNivelAcademico;
                    salida.cultura = complementario != null ? complementario.IdCultura : null;
                    salida.ocupacion = complementario != null ? complementario.Ocupacion : null;
                    salida.agresorconflictoDescripcion = complementario != null ? complementario.DescripcionRelacionAgresor : null;
                    salida.agresorConflicto = complementario != null ? complementario.AgresorGrupoArmado : null;
                    salida.relacionPareja = involucrado.IdTipoRelacion;
                    salida.edadAproximadaAgresor = complementario != null ? complementario.EdadAproximadaAgresor : involucrado.Edad;
                    var hijos = context.SicofaHijoinvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).ToList();
                    salida.numeroHijos = hijos.Count();

                    //salida.lugarExpedicion = involucrado.IdLugarExpedicion;
                    salida.paisExp = involucrado.IdPaisExpedicion;
                    salida.departamentoExp = involucrado.IdDepartamentoExpedicion;
                    salida.municipioExp = involucrado.IdMunicipioExpedicion;

                    if (hijos.Count >= 1)
                    {
                        foreach (var hijo in hijos)
                        {

                            informacionHijo hijoSalida = new informacionHijo();
                            hijoSalida.nombres = hijo.Nombres;
                            hijoSalida.edad = hijo.Edad;
                            hijoSalida.edadEn = hijo.EdadEn;
                            hijoSalida.sexo = hijo.IdSexo;
                            hijoSalida.custodia = hijo.Custodia;
                            hijoSalida.relacionParental = hijo.IdRelacionParental;

                            salida.hijos.Add(hijoSalida);
                        }
                    }

                    salida.agresorConflicto = salida.agresorConflicto = complementario != null ? complementario.AgresorGrupoArmado : null;
                    salida.agresorconflictoDescripcion = complementario != null ? complementario.DescripcionGrupoArmado : null;

                    context.SaveChanges();
                }

                return salida;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);            
            }
        }
            


        public SicofaInvolucrado ConsultarInvolucrado(long id) {
            return context.SicofaInvolucrado.Where(s => s.IdInvolucrado == id).First();
        }
        public void ActualizarInvolucrado(ActualizacionInvolucradoDTO data) {

            
            SicofaInvolucrado  involucrado= context.SicofaInvolucrado.Include(se => se.IdSolicitudServicio).Where(s => s.IdInvolucrado == data.IdInvolucrado).FirstOrDefault()!;

               //TODO: Agregar acá lo que aplica tanto para victima como agresor principales y secundarios
                involucrado.IdSexo = data.idSexo != null ? data.idSexo : involucrado.IdSexo;
                involucrado.FechaNacimiento = data.fechaNacimiento != null ? data.fechaNacimiento : involucrado.FechaNacimiento;


            if ((bool)involucrado.EsPrincipal! && (bool)involucrado.EsVictima)
            {
                //Victima Principal
                
                involucrado.IdTipoDiscpacidad = data.TipoDiscapcidad;
                involucrado.EstadoEmbarazo = data.embarazo;
                involucrado.Eps = data.eps;
                involucrado.Ips = data.ips;
                involucrado.VictimaConflictoArmado = data.victimaConflicto;
                involucrado.VictimaDesplazamiento = data.victimaDesplazamiento;
                involucrado.IdGenero = data.idIdentidadGenero;
                involucrado.IdTipoRelacion = data.RelacionAgresor;
                involucrado.IdNivelAcademico = data.Escolidad;
                involucrado.FechaExpedicion = data.fechaExpedicion;
                involucrado.IdRegimen = data.idRegimen;

                //involucrado.IdLugarExpedicion = data.lugarExpedicion;
                involucrado.IdPaisExpedicion = data.paisExp;
                involucrado.IdDepartamentoExpedicion = data.departamentoExp;
                involucrado.IdMunicipioExpedicion = data.municipioExp;
                
                //involucrado.IdLugarNacimiento = data.lugarNacimiento;
                involucrado.IdPaisNacimiento = data.paisNacimiento;
                involucrado.IdDepartamentoNacimiento = data.departamentoNacimiento;
                involucrado.IdMunicipioNacimiento = data.municipioNacimiento;

                var actualizacion = context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == data.IdInvolucrado).FirstOrDefault();

                // evitar duplicidad en registros de complementos
                if (actualizacion == null)
                {
                    SicofaComplementoInvolucrado ingreso = new SicofaComplementoInvolucrado();
                    ingreso.MesesEmbarazo = data.mesesEmbarazo;
                    ingreso.NumeroHijos = data.numeroHijos;
                    ingreso.IdEscolaridad = data.Escolidad;
                    ingreso.RelacionAgresor = data.RelacionAgresor;
                    ingreso.DescripcionRelacionAgresor = data.descripcionRelacionAgresor;
                    ingreso.DescripcionDiscapacidad = data.descripcionDiscapacidad;
                    ingreso.Ocupacion = data.ocupacion;
                    ingreso.IdCultura = data.Cultura;
                    ingreso.IdInvolucrado = data.IdInvolucrado;
                    ingreso.RelacionPareja = data.RelacionPareja;
                    context.SicofaComplementoInvolucrado.Add(ingreso);

                    context.SicofaHijoinvolucrado.RemoveRange(context.SicofaHijoinvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).ToList());


                    if (data.numeroHijos >= 1)
                    {
                        if (data.numeroHijos == data.informacionHijos.Count)
                        {
                            foreach (var hijo in data.informacionHijos)
                            {

                                SicofaHijoinvolucrado hijoSalida = new SicofaHijoinvolucrado();
                                hijoSalida.Nombres = hijo.nombres;
                                hijoSalida.Edad = (int)hijo.edad!;
                                hijoSalida.EdadEn = hijo.edadEn;
                                hijoSalida.IdSexo = (int)hijo.sexo!;
                                hijoSalida.Custodia = hijo.custodia;
                                hijoSalida.IdInvolucrado = involucrado.IdInvolucrado;
                                hijoSalida.IdSolicitudServicio = involucrado.IdSolicitudServicio.FirstOrDefault() != null ? involucrado.IdSolicitudServicio.First().IdSolicitudServicio : null;


                                context.SicofaHijoinvolucrado.Add(hijoSalida);
                            }
                        }
                        else
                        {

                            throw new Exception(ErrorRespuestaEvaluacionRiesgo.cantidadHijos);

                        }
                    }


                    context.SaveChanges();
                }
                else
                {
                    actualizacion.MesesEmbarazo = data.mesesEmbarazo;
                    actualizacion.NumeroHijos = data.numeroHijos;
                    actualizacion.IdEscolaridad = data.Escolidad;
                    actualizacion.RelacionAgresor = data.RelacionAgresor;
                    actualizacion.DescripcionRelacionAgresor = data.descripcionRelacionAgresor;
                    actualizacion.DescripcionDiscapacidad = data.descripcionDiscapacidad;
                    actualizacion.Ocupacion = data.ocupacion;
                    actualizacion.IdCultura = data.Cultura;
                    actualizacion.IdInvolucrado = data.IdInvolucrado;
                    actualizacion.RelacionPareja = data.RelacionPareja;


                    context.SicofaHijoinvolucrado.RemoveRange(context.SicofaHijoinvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).ToList());

                    if (data.numeroHijos >= 1)
                    {
                        if (data.informacionHijos.Count >= 1 & data.numeroHijos == data.informacionHijos.Count)
                        {
                            foreach (var datahijo in data.informacionHijos)
                            {
                                SicofaHijoinvolucrado hijo = new SicofaHijoinvolucrado();
                                hijo.IdInvolucrado = data.IdInvolucrado;
                                hijo.Nombres = datahijo.nombres;
                                hijo.Edad = (int)datahijo.edad;
                                hijo.EdadEn = datahijo.edadEn;
                                hijo.Custodia = datahijo.custodia;
                                hijo.IdSexo = datahijo.sexo;
                                hijo.IdRelacionParental = (int)datahijo.relacionParental;
                                hijo.IdSolicitudServicio = involucrado.IdSolicitudServicio.FirstOrDefault() != null ? involucrado.IdSolicitudServicio.First().IdSolicitudServicio : null;
                                context.SicofaHijoinvolucrado.Add(hijo);
                            }
                        }
                        else
                        {

                            throw new Exception(ErrorRespuestaEvaluacionRiesgo.cantidadHijos);
                        }
                    }
                }

                context.SaveChanges();

            }
            else 
            {
                //involucrado.Nombres 
                //TODO: Eliminar nombres y apellidos
                involucrado.IdNivelAcademico = data.Escolidad;
                involucrado.PrimerNombre = data.primerNombre;
                involucrado.SegundoNombre = data.segundoNombre;
                involucrado.PrimerApellido = data.primerApellido;
                involucrado.SegundoApellido = data.segundoApellido;
                involucrado.Nombres = data.nombres;
                involucrado.Apellidos = data.apellidos;
                involucrado.TipoDocumento = data.idtipoDocumento;
                involucrado.NumeroDocumento = data.numeroDocumento;              
                involucrado.IdGenero = data.idIdentidadGenero;
                involucrado.IdNivelAcademico = data.Escolidad;
                involucrado.FechaExpedicion = data.fechaExpedicion;

                //involucrado.IdLugarExpedicion = data.lugarExpedicion;
                involucrado.IdPaisExpedicion = data.paisExp;
                involucrado.IdDepartamentoExpedicion = data.departamentoExp;
                involucrado.IdMunicipioExpedicion = data.municipioExp;

                involucrado.Edad = data.edad != null ? data.edad : (data.edadAproximadaAgresor != null ? data.edadAproximadaAgresor : involucrado.Edad);

                //
                var actualizacion = context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == data.IdInvolucrado).FirstOrDefault();

                if (actualizacion == null)
                {

                    SicofaComplementoInvolucrado ingreso = new SicofaComplementoInvolucrado();
                    ingreso.Ocupacion = data.ocupacion;
                    ingreso.IdInvolucrado = data.IdInvolucrado;
                    ingreso.NumeroHijos = data.numeroHijos;
                    ingreso.AgresorGrupoArmado = data.agresorOrganizacionCriminal;
                    ingreso.DescripcionGrupoArmado = data.descripcionOrganizacionCriminal;
                    ingreso.IdCultura = data.Cultura;
                    ingreso.EdadAproximadaAgresor = data.edadAproximadaAgresor;
                    context.SicofaComplementoInvolucrado.Add(ingreso);

                    //involucrado.IdLugarExpedicion = data.lugarExpedicion;
                    involucrado.IdPaisExpedicion = data.paisExp;
                    involucrado.IdDepartamentoExpedicion = data.departamentoExp;
                    involucrado.IdMunicipioExpedicion = data.municipioExp;

                    var remover=context.SicofaHijoinvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).ToList();
                    context.SicofaHijoinvolucrado.RemoveRange(remover);

                    if (data.numeroHijos >= 1)
                    {
                        if (data.informacionHijos.Count >= 1 & data.numeroHijos == data.informacionHijos.Count)
                        {

                            foreach (var hijodata in data.informacionHijos)
                            {
                                SicofaHijoinvolucrado hijo = new SicofaHijoinvolucrado();
                                hijo.IdInvolucrado = data.IdInvolucrado;
                                hijo.Nombres = hijodata.nombres;
                                hijo.Edad = (int)hijodata.edad!;
                                hijo.EdadEn = hijodata.edadEn;
                                hijo.Custodia = hijodata.custodia;
                                hijo.IdSexo = hijodata.sexo;
                                hijo.IdRelacionParental = hijodata.relacionParental;
                                hijo.IdSolicitudServicio = involucrado.IdSolicitudServicio.FirstOrDefault() != null ? involucrado.IdSolicitudServicio.First().IdSolicitudServicio : null;
                                context.SicofaHijoinvolucrado.Add(hijo);
                            }
                        }
                        else
                        {
                            throw new Exception(ErrorRespuestaEvaluacionRiesgo.cantidadHijos);
                        }
                    }

                }
                else {

                    actualizacion.Ocupacion = data.ocupacion;
                    actualizacion.NumeroHijos = data.numeroHijos;
                    actualizacion.AgresorGrupoArmado = data.agresorOrganizacionCriminal;
                    actualizacion.DescripcionGrupoArmado = data.descripcionOrganizacionCriminal;
                    actualizacion.IdCultura = data.Cultura;
                    actualizacion.EdadAproximadaAgresor = data.edadAproximadaAgresor;

                    context.SicofaHijoinvolucrado.RemoveRange(context.SicofaHijoinvolucrado.Where(s => s.IdInvolucrado == involucrado.IdInvolucrado).ToList());

                    if (data.numeroHijos >= 1)
                    {

                        if (data.informacionHijos.Count >= 1 & data.numeroHijos == data.informacionHijos.Count)
                        {
                            foreach (var hijodata in data.informacionHijos)
                            {
                                SicofaHijoinvolucrado hijo = new SicofaHijoinvolucrado();
                                hijo.IdInvolucrado = data.IdInvolucrado;
                                hijo.Nombres = hijodata.nombres;
                                hijo.Edad = (int)hijodata.edad!;
                                hijo.EdadEn = hijodata.edadEn;
                                hijo.Custodia = hijodata.custodia;
                                hijo.IdSexo = hijodata.sexo;
                                hijo.IdRelacionParental = hijodata.relacionParental;
                                hijo.IdSolicitudServicio = involucrado.IdSolicitudServicio.FirstOrDefault() != null ? involucrado.IdSolicitudServicio.First().IdSolicitudServicio : null;
                                context.SicofaHijoinvolucrado.Add(hijo);
                            }
                        }
                        else
                        {
                            throw new Exception(ErrorRespuestaEvaluacionRiesgo.cantidadHijos);
                        }

                    }


                }

                context.SaveChanges();
            }

        
        }


        #endregion H2

        public async Task<List<QuestionarioRespuestaPreviaDTO>> ObtenerCuestionarioViolencia(int tipoViolencia, long  idSolicitudServicio,long idTarea) 
        {
             var evaluacion = ObtenerEvaluacionPsicologica(idSolicitudServicio,idTarea);

            var preguntas=context.SicofaQuestionarioTipoViolencia.Where(c => c.IdTipoViolencia == tipoViolencia).ToList();

            var respuestas = (from res in context.SicofaRespuestaQuestionarioTipoViolencia
                              join quesionario in context.SicofaQuestionarioTipoViolencia on res.IdQuestionario equals quesionario.IdQuestionario
                              where res.IdEvaluacionPsicologica == evaluacion.IdEvaluacion & quesionario.IdTipoViolencia == tipoViolencia
                              select Tuple.Create(res.IdQuestionario, res.Mes, res.Puntuacion, res.Nullable)
                              ).ToList();

            List<QuestionarioRespuestaPreviaDTO> salida = new List<QuestionarioRespuestaPreviaDTO>();
           
                foreach (var pregunta in preguntas)
                {
                    QuestionarioRespuestaPreviaDTO previo = new QuestionarioRespuestaPreviaDTO();
                    previo.IdQuestionario = pregunta.IdQuestionario;
                    previo.IdTipoViolencia = pregunta.IdTipoViolencia;
                    previo.Puntuacion = pregunta.Puntuacion;
                    previo.EsCerrada = pregunta.EsCerrada;
                    previo.Descripcion = pregunta.Descripcion;
                    previo.Nullable = pregunta.Nullable;

                    if (respuestas.Count() == preguntas.Count())
                    {
                        var respuesta = respuestas.Where(s => s.Item1 == pregunta.IdQuestionario).First();

                        int? puntuacionPrevia = respuesta.Item3;
                        if (respuesta.Item4 is not null)
                        {
                            puntuacionPrevia = null;
                        }
                        previo.PuntuacionPrevio = puntuacionPrevia;
                        previo.mesPrevio = respuesta.Item2;
                    }
                    salida.Add(previo);
                }

            return salida;
        
        }

        public void RegistrarCuestionario(RespuestaCuestionarioDTO data, long idTarea)
        {

            SicofaEvaluacionPsicologica evaluacion = ObtenerEvaluacionPsicologica(data.idSolicitudServicio , idTarea);
            if (evaluacion == null) throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorRespuestas);

           List<SicofaRespuestaQuestionarioTipoViolencia >insercion = new List<SicofaRespuestaQuestionarioTipoViolencia>();

            SicofaQuestionarioTipoViolencia violencia = context.SicofaQuestionarioTipoViolencia.Where(s => s.IdTipoViolencia == data.IdTipoViolencia).First();
            var respuestas = (from res in context.SicofaRespuestaQuestionarioTipoViolencia
                              join quesionario in context.SicofaQuestionarioTipoViolencia on res.IdQuestionario equals quesionario.IdQuestionario
                              where res.IdSolicitudServicio == data.idSolicitudServicio & quesionario.IdTipoViolencia == data.IdTipoViolencia
                              & res.IdEvaluacionPsicologica == evaluacion.IdEvaluacion
                              select Tuple.Create(res.IdQuestionario, res.Mes, res.Puntuacion)
                              ).ToList();

            if (respuestas.Count.Equals(0))
            {

                foreach (var respuesta in data.listadoRespuestas)
                {
                    SicofaRespuestaQuestionarioTipoViolencia entrada = new SicofaRespuestaQuestionarioTipoViolencia();
                    entrada.Puntuacion = 0;
                    entrada.Nullable = null;
                    entrada.IdQuestionario = respuesta.IdCuestionario;
                    entrada.IdSolicitudServicio = data.idSolicitudServicio;
                 
                    if (data.IdTipoViolencia == Cuestionario.circunstanciaAgrevantes || data.IdTipoViolencia == Cuestionario.persepcionVictima) 
                    {
                        entrada.Mes = null;
                    }
                    entrada.Mes = respuesta.mes;
                    entrada.IdEvaluacionPsicologica = evaluacion.IdEvaluacion;

                    if (respuesta is { puntuacion: true })
                    {
                        entrada.Puntuacion = (int)violencia.Puntuacion;
                    }

                    if (respuesta is { puntuacion: null })
                    {
                        entrada.Nullable = (int)violencia.Puntuacion;
                    }

                    insercion.Add(entrada);
                }

                context.SicofaRespuestaQuestionarioTipoViolencia.AddRange(insercion);

                context.SaveChanges();
            }
            else {

                foreach (var lista in data.listadoRespuestas)
                {

                    SicofaRespuestaQuestionarioTipoViolencia previa = 
                        context.SicofaRespuestaQuestionarioTipoViolencia.Where(s => s.IdQuestionario == lista.IdCuestionario & s.IdSolicitudServicio == data.idSolicitudServicio & s.IdEvaluacionPsicologica == evaluacion.IdEvaluacion).FirstOrDefault()!;
                   
                    if (previa is not null)
                    {
                        previa.Puntuacion = 0;
                        previa.Nullable = null;
                        previa.Mes = lista.mes;

                        if (lista is { puntuacion: true })
                        {
                            previa.Puntuacion = (int)violencia.Puntuacion!;
                        }

                        if (lista is { puntuacion: null })
                        {
                            previa.Nullable = (int)violencia.Puntuacion;
                        }

                        // TODO : correcion de bug donde por no se debe guardar mes en circunstancia agrevantes ni persecipcion victima, luego de probado se borra y se certifique el bug se elimina este comentario
                        if (data.IdTipoViolencia == Cuestionario.circunstanciaAgrevantes || data.IdTipoViolencia == Cuestionario.persepcionVictima)
                        {
                            previa.Mes = null;
                        }
                    }

                    context.SaveChanges();
                }
            }
        }

        public async Task<EvaluacionRiegoDTO> EvaluacionRiesgosPorSolicitud(long idSolicitud , long? Idtarea)
        {
            var solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitud).FirstOrDefault();

            EvaluacionRiegoDTO salida = new EvaluacionRiegoDTO();

            if (solicitud == null) {

                throw new Exception(ErrorRespuestaEvaluacionRiesgo.cantidadPreguntas);
            }

            var evalucion = ObtenerEvaluacionPsicologica(idSolicitud , Idtarea);

            var repuestas = context.SicofaRespuestaQuestionarioTipoViolencia.Where(s => s.IdEvaluacionPsicologica == evalucion.IdEvaluacion ).ToList();



            if (repuestas.Count.Equals(Riesgo.totalPreguntas))
            {

                var contador = repuestas.Sum(s => s.Puntuacion);



                if (contador <= Riesgo.bajo)
                {

                    salida.indicadorRiesgo = Riesgo.riesgoBajo;
                    salida.puntuacion = contador;

                }
                else if (contador > Riesgo.bajo & contador <= Riesgo.medio)
                {

                    salida.indicadorRiesgo = Riesgo.riesgoMedio;
                    salida.puntuacion = contador;
                }
                else if (contador > Riesgo.medio)
                {

                    salida.indicadorRiesgo = Riesgo.riesgoAlto;
                    salida.puntuacion = contador;

                }

                salida.descripcion = evalucion != null ? evalucion.Recomendaciones : null;  
                return salida;

            }
            else {

                throw new Exception(ErrorRespuestaEvaluacionRiesgo.totalidadRespuestas);
            }




        }



        public DescripcionHechosDTO ObtenerDescripcionHechosPorSolicitud(long id) 
        {
           
            var solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == id).FirstOrDefault();

            if (solicitud == null) {

                throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorRespuestas);
            }

            DescripcionHechosDTO salida = new DescripcionHechosDTO();

            var fecha = solicitud.FechaHechoViolento ?? DateTime.Now;
            salida.fecha = fecha.ToString("dd/MM/yyyy");
            var fechaparse = fecha;
         
            salida.hora = fechaparse.ToString("HH:mm");
            salida.descripcionHechos = solicitud.DescripcionDeHechos;
            salida.idSolicitudServicio = solicitud.IdSolicitudServicio;
            salida.lugarHechos = solicitud.LugarHechoViolento ?? "No asignado";


            return salida;

        }

        public void ActualizarDescripcioHechosPorSolicitud(DescripcionHechosDTO data)
        {
            try
            {
                var solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).First();

                if (solicitud == null)
                {
                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorRespuestas);
                }

                var fechaAjustada = DateTime.Parse(data.fecha+ FormatoHora.espacioFormato + data.hora);
                solicitud.FechaHechoViolento = fechaAjustada;
                solicitud.HoraSolicitud = fechaAjustada;
                solicitud.DescripcionDeHechos = data.descripcionHechos;
                solicitud.LugarHechoViolento = data.lugarHechos;

                context.SaveChanges();

            }
            catch (Exception ex) { 
            
                    throw new Exception(ex.Message);       
            
            }



        }


        // se debe crear apenas la tarea es aceptada por;
        public long CrearEvaluacionPsicologica(long idSolicitudServicio , long? idTarea) {

            try { 
        
                    var salida = new SicofaEvaluacionPsicologica();
                    salida.IdSolicitudServicio = idSolicitudServicio;

                    salida.IdTarea = idTarea;
                    context.SicofaEvaluacionPsicologica.Add(salida);
                    context.SaveChanges();

                    return (long)salida.IdTarea;
               

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }



        // Evaluacion PsicologicaOrientacion
        public async Task ActualizarEvaluacionPsicologica(RegistroEvaluacionEmocionalDTO data,long idTarea) 
        {
            SicofaEvaluacionPsicologica evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == data.IdSolicitudServicio & s.IdTarea == idTarea).FirstOrDefault();
            if (evaluacion == null)
            {

                throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
            }

            switch (data.tipoDominio) {

                case Evaluacion.motivo:

                    evaluacion.MotivoDescripcion = data.descripcionA;
                    break;

                case Evaluacion.antecedente:

                    evaluacion.AntecedenteDescripcion = data.descripcionA;
                    break;

                case Evaluacion.metodologia:

                    evaluacion.MetodologiaDescripcion= data.descripcionA;
                    break;


                case Evaluacion.relato:

                    evaluacion.RelatoHechosDescripcion = data.descripcionA;
                    break;


                case Evaluacion.redapoyo:

                    evaluacion.RedApoyoDescripcion = data.descripcionA;
                    evaluacion.TipoRedApoyoDescripcion = data.descripcionB;
                    RegistrarEvaluacionOrientacion(evaluacion.IdEvaluacion, data.respuestas);

                    break;

                case Evaluacion.percepcion:

                    evaluacion.PercepcionMujerDescripcion = data.descripcionA;
                    evaluacion.PersistenciaDescripcion = data.descripcionB;

                    RegistrarEvaluacionOrientacion(evaluacion.IdEvaluacion,data.respuestas);

                    break;

                case Evaluacion.conclusiones:
                    evaluacion.ConclusionesEntrevista = data.descripcionA;
                    evaluacion.RecomendacionesEntrevista = data.descripcionB;
                    break;



            }
        
                    context.SaveChanges();
        }

        public void RegistrarRecomendacion(long idSolicitudServicio, string descripcion , long? idTarea) 
        {
            SicofaEvaluacionPsicologica evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idSolicitudServicio & s.IdTarea == idTarea ).First();
            if (evaluacion == null)
            {

                throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
            }

            try
            {

                evaluacion.Recomendaciones = descripcion;

                context.SaveChanges();

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            
            }
        
        
        }

        public List<EvaluacionOrientacionRespuesta> EvaluacionOrientacion(long idSolicitudServicio , string data, long? idTarea) 
        {
            try
            {

                var listadoSalida = new List<EvaluacionOrientacionRespuesta>();

                var evaluacionPsicologica = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idSolicitudServicio & s.IdTarea == idTarea).FirstOrDefault();

                if (evaluacionPsicologica == null) {

                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
                }

                var respuestaPrevias = (from evaluacionLista in context.SicofaEvaluacionPsicologicaLista
                                        join evaluacion in context.SicofaEvaluacionPsicologica on evaluacionLista.IdEvaluacion equals evaluacion.IdEvaluacion
                                        join dominio in context.SicofaDominio on evaluacionLista.IdDominio equals dominio.IdDominio
                                        where evaluacionLista.IdEvaluacion == evaluacionPsicologica.IdEvaluacion & dominio.TipoDominio == data
                                        select evaluacionLista).ToList();

                var preguntasDominio = context.SicofaDominio.Where(s => s.TipoDominio == data).ToList();

                foreach (var dominio in preguntasDominio)
                {
                    var salida = new EvaluacionOrientacionRespuesta();
                    salida.idDominio = dominio.IdDominio;
                    salida.nombreDominio = dominio.NombreDominio;
                    salida.respuesta = false;
                    if (!respuestaPrevias.Count.Equals(0))
                    {
                        var cruce = respuestaPrevias.Where(s => s.IdDominio == dominio.IdDominio).FirstOrDefault();
                        salida.respuesta = cruce.Respuesta;


                    }
                    listadoSalida.Add(salida);

                }


                return listadoSalida;


            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }


        public void RegistrarEvaluacionOrientacion(RegistroEvaluacionEmocionalDTO data, long idTarea) 
        {
            try
            {
                List<SicofaEvaluacionPsicologicaLista> ingreso = new List<SicofaEvaluacionPsicologicaLista>();

                var evaluacionPsicologica = context.SicofaEvaluacionPsicologica.Where(s =>
                s.IdSolicitudServicio == data.IdSolicitudServicio && s.IdTarea == idTarea).FirstOrDefault();

                if (evaluacionPsicologica == null)
                {

                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
                }

                string[] dominio1= { Evaluacion.seguridad, Evaluacion.redApoyoExterno };

                var respuestaPrevias = (from evaluacionLista in context.SicofaEvaluacionPsicologicaLista
                                        join evaluacion in context.SicofaEvaluacionPsicologica on evaluacionLista.IdEvaluacion equals evaluacion.IdEvaluacion
                                        join dominio in context.SicofaDominio on evaluacionLista.IdDominio equals dominio.IdDominio
                                        where evaluacionLista.IdEvaluacion == evaluacionPsicologica.IdEvaluacion & 
                                        dominio1.Contains(dominio.TipoDominio)
                                        select evaluacionLista).ToList();





                if (respuestaPrevias.Count > 0)
                {
                    foreach (var respuesta in respuestaPrevias)
                    {
                        var cambio = data.respuestas.Where(s => s.idDominio == respuesta.IdDominio).First();
                        respuesta.Respuesta = cambio.respuesta;
                    }
                    context.SaveChanges();
                }
                else
                {

                    foreach (var respuesta in data.respuestas)
                    {

                        SicofaEvaluacionPsicologicaLista registro = new SicofaEvaluacionPsicologicaLista();
                        registro.IdEvaluacion = evaluacionPsicologica.IdEvaluacion;
                        registro.IdDominio = respuesta.idDominio;
                        registro.Respuesta = respuesta.respuesta;
                        //ingreso.Add(registro);
                        context.SicofaEvaluacionPsicologicaLista.Add(registro);
                    }
                    context.SaveChanges();
                }

                

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }

        }


        public ObtenerEvaluacionPsicologicaEmocionalDTO ObtenerEvaluacionPsicologicaEmocional(long idSolicituServicio,string tipoDominio,long idTarea) 
        {
            try
            {
                SicofaEvaluacionPsicologica evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idSolicituServicio & s.IdTarea==idTarea).FirstOrDefault()!;
                ObtenerEvaluacionPsicologicaEmocionalDTO salida = new ObtenerEvaluacionPsicologicaEmocionalDTO();
                if (evaluacion == null)
                {

                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
                }

                switch (tipoDominio)
                {

                    case Evaluacion.motivo:

                        salida.descripcionA = evaluacion.MotivoDescripcion;
                        break;

                    case Evaluacion.antecedente:

                        salida.descripcionA = evaluacion.AntecedenteDescripcion;
                        break;

                    case Evaluacion.metodologia:

                        salida.descripcionA = evaluacion.MetodologiaDescripcion;
                        break;


                    case Evaluacion.relato:

                        salida.descripcionA = evaluacion.RelatoHechosDescripcion;
                        break;


                    case Evaluacion.redapoyo:

                        salida.descripcionA = evaluacion.RedApoyoDescripcion;
                        salida.descripcionB = evaluacion.TipoRedApoyoDescripcion;

                        break;

                    case Evaluacion.percepcion:

                        salida.descripcionA = evaluacion.PercepcionMujerDescripcion;
                        salida.descripcionB = evaluacion.PersistenciaDescripcion;

                        break;

                    case Evaluacion.conclusiones:
                        salida.descripcionA = evaluacion.ConclusionesEntrevista;
                        salida.descripcionB = evaluacion.RecomendacionesEntrevista;
                        break;



                }

                return salida;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }


        }

        public VictimaPrincipalDTO ObtenerVictimaPrincipal(long idSolicitud)
        {
            try
            {
                var solicitud = context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitud).FirstOrDefault();
                SicofaInvolucrado involucradoVictima = solicitud.IdInvolucrado.Where(s => s.EsVictima == true & s.EsPrincipal == true).First();

                if (solicitud == null)
                {
                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
                }


                //TODO: Eliminar nombres y apellidos
                VictimaPrincipalDTO salida = (from involucrado in context.SicofaInvolucrado
                                              join dominiotipodocumento in context.SicofaDominio on involucrado.TipoDocumento equals dominiotipodocumento.IdDominio
                                              join dominioEscolaridad in context.SicofaDominio on involucrado.IdNivelAcademico equals dominioEscolaridad.IdDominio
                                              where involucrado.IdInvolucrado == involucradoVictima.IdInvolucrado
                                              select new VictimaPrincipalDTO
                                              {
                                                  idInvolucrado = involucrado.IdInvolucrado,
                                                  nombres = involucrado.Nombres,
                                                  primerNombre = involucrado.PrimerNombre,
                                                  segundoNombre = involucrado.SegundoNombre,
                                                  primerApellido = involucrado.PrimerApellido,
                                                  SegundoApellido = involucrado.SegundoApellido,
                                                  apellidos = involucrado.Apellidos,
                                                  tipoDocumento = dominiotipodocumento.NombreDominio,
                                                  numeroDocumento = involucrado.NumeroDocumento,
                                                  fechaNacimiento = involucrado.FechaNacimiento,
                                                  edad = involucrado.Edad,
                                                  eps = involucrado.Eps,
                                                  telefono = involucrado.Telefono,
                                                  barrio = involucrado.Barrio,
                                                  escolaridad = dominioEscolaridad.NombreDominio,
                                                  direccion = involucrado.DireccionRecidencia

                                              }).First();

                return salida; 

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }


        public void ActualizaEvaluacionPsicologicaEntrevista(EvaluacionPsicologicaEntrevistaDTO data, long idTarea)
        {

            try
            {
                var evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio & s.IdTarea == idTarea).FirstOrDefault();

                var involucrado = context.SicofaInvolucrado.Where(invo => invo.IdInvolucrado == data.idInvolucrado).FirstOrDefault();


                if (evaluacion == null)
                {
                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
                }

                evaluacion.FechaEntrevista = data.fechaEntrevista;
                evaluacion.FechaElaboracionInforme = data.fechaElaboracionInforme;

                involucrado.NombreContactoConfianza = data.nombreContacto;
                involucrado.DireccionContactoConfianza = data.direccionContacto;
                involucrado.TelefonoContactoConfianza = data.telefonoContacto;

                context.SaveChanges();




            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        public EvaluacionPsicologicaEntrevistaDTO ObtenerEvaluacionPsicologicaEntrevista(long idSolicitud, long idTarea)
        {
            try
            {
                var solicitud = context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitud).First();

                var involucrado=solicitud.IdInvolucrado.Where(s => s.EsVictima == true & s.EsPrincipal == true).First();

                var evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == solicitud.IdSolicitudServicio & s.IdTarea == idTarea).First();


                EvaluacionPsicologicaEntrevistaDTO salida = new EvaluacionPsicologicaEntrevistaDTO();


                if (solicitud == null)
                {
                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
                }

                //TODO: 20220827 USAR idSolicitud de los parametros
                salida.idInvolucrado = involucrado.IdInvolucrado;
                salida.idSolicitudServicio = solicitud.IdSolicitudServicio;
                salida.fechaEntrevista =   evaluacion.FechaEntrevista == null ? null : (DateTime)evaluacion.FechaEntrevista;
                salida.fechaElaboracionInforme = evaluacion.FechaElaboracionInforme == null ? null : (DateTime)evaluacion.FechaElaboracionInforme;
                salida.direccionContacto = involucrado.DireccionContactoConfianza;
                salida.telefonoContacto = involucrado.TelefonoContactoConfianza;
                salida.nombreContacto = involucrado.NombreContactoConfianza;

                return salida;



            }
            catch (Exception ex) {


                throw new Exception(ex.Message); 
            }
        
        }

        private void RegistrarEvaluacionOrientacion( long idEvaluacionPsicologica, List<RespuestaEvaluacionEmocionalDTO> data) 
        {

               int[] elementos=  data.Select(s => s.idDominio).ToArray();

             var identicarNucleo = context.SicofaEvaluacionPsicologicaLista.Where(s => elementos.Contains((int)s.IdDominio)).ToList();

            context.SicofaEvaluacionPsicologicaLista.RemoveRange(identicarNucleo);

            foreach (var respuesta in data) 
            {
                SicofaEvaluacionPsicologicaLista elemento = new SicofaEvaluacionPsicologicaLista();
                elemento.IdEvaluacion = idEvaluacionPsicologica;
                elemento.Respuesta = respuesta.respuesta;
                elemento.IdDominio = respuesta.idDominio;

                context.SicofaEvaluacionPsicologicaLista.Add(elemento);

            }
            context.SaveChanges();
        
        }

        public InvolucradosReporte13DTO ReporteHU13(long idSolicitudServicio)
        {
            try
            {
                InvolucradosReporte13DTO salida = new InvolucradosReporte13DTO();

                var solicitud =  context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).
                    Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault();

                var victima = solicitud.IdInvolucrado.Where(s => s.EsPrincipal == true & s.EsVictima == true).First();
                var agresor = solicitud.IdInvolucrado.Where(s => s.EsPrincipal == true & s.EsVictima == false).First();


                var tipodocumentoVictima = context.SicofaDominio.Where(s => s.IdDominio == victima.TipoDocumento).FirstOrDefault();
                var tipodocumentoAgresor = context.SicofaDominio.Where(s => s.IdDominio == agresor.TipoDocumento).FirstOrDefault();

                var ciudadano =  context.SicofaCiudadano.Where(c => c.NumeroDocumento == victima.NumeroDocumento).FirstOrDefault();
                //var LugarExpedicionVictima =  context.SicofaCiudadMunicipio.Where(s => s.IdCiudadMunicipio == ciudadano.IdLugarExpedicion).FirstOrDefault();

                salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
                salida.documentoVictima = victima.NumeroDocumento;

                //TODO :se debe hacer una revision en el registro el ciudadano ese dato esta intermitente.
                // luego de realizada la verificacion de ciudadano se elimina esto.
                //salida.lugarExpedicionVictima = LugarExpedicionVictima != null ? LugarExpedicionVictima.Nombre : "Bogota";
                salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";
                salida.documentoAgresor = agresor.NumeroDocumento;
                salida.tipoDocumentoVictima = tipodocumentoVictima != null ? tipodocumentoVictima.Codigo : String.Empty;
                salida.tipoDocumentoAgresor = tipodocumentoVictima != null ? tipodocumentoVictima.Codigo : String.Empty;


                //var evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault();

                var seguridad =  (from dominio in context.SicofaDominio
                                       where dominio.TipoDominio == Evaluacion.seguridad  
                                       select dominio.NombreDominio
                                 ).ToList();

                var redApoyoExterno = (from dominio in context.SicofaDominio
                                       where dominio.TipoDominio == Evaluacion.redApoyoExterno
                                       select dominio.NombreDominio
                                 ).ToList();



                

                salida.seguridad = seguridad;
                salida.redApoyoExterno = redApoyoExterno;


                return salida;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        //public async Task<Tuple<List<string>, List<string>>> ComplementoReporte13(long idSolicitudServicio) 
        //{
        //    try
        //    {
        //        var evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstAsync();

        //        var seguridad = await  (from evalista in context.SicofaEvaluacionPsicologicaLista
        //                         join dominio in context.SicofaDominio on evalista.IdDominio equals dominio.IdDominio
        //                         where dominio.TipoDominio == Evaluacion.seguridad & evalista.IdEvaluacion == evaluacion.Id & evalista.Respuesta == true
        //                         select dominio.NombreDominio
        //                         ).ToListAsync();

        //        var redApoyoExterno = await (from evalista in context.SicofaEvaluacionPsicologicaLista
        //                               join dominio in context.SicofaDominio on evalista.IdDominio equals dominio.IdDominio
        //                               where dominio.TipoDominio == Evaluacion.redApoyoExterno & evalista.IdEvaluacion == evaluacion.Id & evalista.Respuesta == true
        //                               select dominio.NombreDominio
        //                         ).ToListAsync();

        //        var response = Tuple.Create(seguridad, redApoyoExterno);

        //        return response;

        //    }
        //    catch (Exception e) {

        //        throw new Exception(e.Message);

        //    }

        //}

        //public List<EvaluacionPsicologicaReporte17DTO> ObtenerEvaluacionPsicologicaReporte17(long idSolicitud, string tipoDoc)
        //{
        //    try
        //    {
        //        var respuesta = (from evpl in this.context.SicofaEvaluacionPsicologicaLista
        //                join domi in this.context.SicofaDominio on evpl.IdDominio equals domi.IdDominio
        //                where evpl.IdEvaluacion == idSolicitud && domi.TipoDominio == tipoDoc
        //                select new EvaluacionPsicologicaReporte17DTO
        //                {
        //                    TipoDominio = domi.TipoDominio,
        //                    NombreDominio = domi.NombreDominio,
        //                    Respuesta = evpl.Respuesta
        //                }).ToList();

        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}

        #region metodoPrivadosTarea

        public SicofaEvaluacionPsicologica ObtenerEvaluacionPsicologica(long idSolicitud, long? IdTarea)
        {
            try
            {
                SicofaEvaluacionPsicologica evaluacion;
                if (IdTarea != null)
                {
                    //TODO: 20220827 Incluir tabla tarea para hallar la respuesta segun el id_instancia_proceso, quitar la solicitud de servicio.
                    evaluacion = context.SicofaEvaluacionPsicologica.Where(eva => eva.IdSolicitudServicio == idSolicitud & eva.IdTarea == IdTarea).FirstOrDefault();
                }
                else
                {
                    //20220827: Validar más adelante para que todos los reportes funcionen con tarea, ya que se añade esto para evitar impactar los controladores
                    evaluacion = context.SicofaEvaluacionPsicologica.Where(eva => eva.IdSolicitudServicio == idSolicitud).OrderByDescending(eva => eva.IdEvaluacion).First();
                }

                if (evaluacion == null)
                {
                    var id = CrearEvaluacionPsicologica(idSolicitud, IdTarea);
                    evaluacion =  context.SicofaEvaluacionPsicologica.Where(eva => eva.IdSolicitudServicio == idSolicitud & eva.IdTarea == IdTarea).FirstOrDefault();
                }

                return evaluacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
