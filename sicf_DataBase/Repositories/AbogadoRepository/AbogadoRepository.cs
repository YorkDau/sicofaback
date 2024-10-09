using Humanizer;
using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using sicf_Models.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.AbogadoRepository
{
    public class AbogadoRepository : BaseRepository<SicofaRespuestaQuestionarioTipoViolencia>, IAbogadoRepository
    {
        protected DbSet<TestProcedure> Simply;

        private readonly SICOFAContext context;

        public AbogadoRepository(SICOFAContext context) : base(context)
        {

            this.context = context;
            Simply = context.Set<TestProcedure>();
        }


        #region hu2

        public InvolucradosDTO ObtenerInvolucrados(long idSolicitudServicio)
        {
            try
            {
                InvolucradosDTO respuesta = new InvolucradosDTO();
                var solicitud = context.SicofaSolicitudServicio.Include(se =>
                se.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault();

                if (solicitud == null)
                {
                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
                }

                var victimaSolicitud = solicitud.IdInvolucrado.Where(s => s.EsVictima == true & s.EsPrincipal == true).First();
                var complementoVictima = context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == victimaSolicitud.IdInvolucrado).First();
                var informacionVictima = (from victi in context.SicofaInvolucrado
                                          join tipodoc in context.SicofaDominio on victi.TipoDocumento equals tipodoc.IdDominio
                                          join relacionPajera in context.SicofaDominio on victi.IdTipoRelacion equals relacionPajera.IdDominio
                                          join nivelAcademico in context.SicofaDominio on victi.IdNivelAcademico equals nivelAcademico.IdDominio
                                          where victi.IdInvolucrado == victimaSolicitud.IdInvolucrado
                                          select Tuple.Create(tipodoc.NombreDominio, relacionPajera.NombreDominio, nivelAcademico.NombreDominio)
                                          ).First();

                var estadoCivilVictima = context.SicofaDominio.Where(s => s.IdDominio == complementoVictima.RelacionPareja).First();

                //TODO: Eliminar nombres
                victimaDTO victima = new victimaDTO(victimaSolicitud.Nombres!, victimaSolicitud.PrimerNombre!, victimaSolicitud.SegundoNombre!, victimaSolicitud.PrimerApellido!,
                    victimaSolicitud.SegundoApellido!, victimaSolicitud.Apellidos!, informacionVictima.Item1, victimaSolicitud.NumeroDocumento!, victimaSolicitud.Barrio!, victimaSolicitud.Telefono!, victimaSolicitud.CorreoElectronico!, informacionVictima.Item2, informacionVictima.Item3, estadoCivilVictima.NombreDominio!, complementoVictima.Ocupacion!, null, victimaSolicitud.DireccionRecidencia!.TrimEnd(), (int)victimaSolicitud.Edad!);

                var agresorSolicitud = solicitud.IdInvolucrado.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();
                var complementoAgresor = context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == agresorSolicitud.IdInvolucrado).First();
                var informacionAgresor = (from victi in context.SicofaInvolucrado
                                          join tipodoc in context.SicofaDominio on victi.TipoDocumento equals tipodoc.IdDominio
                                          join nivelAcademico in context.SicofaDominio on victi.IdNivelAcademico equals nivelAcademico.IdDominio
                                          where victi.IdInvolucrado == victimaSolicitud.IdInvolucrado
                                          select Tuple.Create(tipodoc.NombreDominio, nivelAcademico.NombreDominio)
                                          ).First();

                var today = DateTime.Today;

                var edadAgresor = 0;

                if (agresorSolicitud.FechaNacimiento != null)
                {

                    var auxEdad = (DateTime)agresorSolicitud.FechaNacimiento;
                    edadAgresor = today.Year - auxEdad.Year;

                }

                //TODO: Eliminar nombres
                victimaDTO agresor = new victimaDTO(agresorSolicitud.Nombres, agresorSolicitud.PrimerNombre, agresorSolicitud.SegundoNombre,
                    agresorSolicitud.PrimerApellido, agresorSolicitud.SegundoApellido, agresorSolicitud.Apellidos, informacionAgresor.Item1,
                    agresorSolicitud.NumeroDocumento, agresorSolicitud.Barrio, agresorSolicitud.Telefono,
                    agresorSolicitud.CorreoElectronico, null, informacionAgresor.Item2, null, complementoAgresor.Ocupacion, null,
                    agresorSolicitud.DireccionRecidencia.TrimEnd(),
                    complementoAgresor.EdadAproximadaAgresor == null ? edadAgresor : (int)complementoAgresor.EdadAproximadaAgresor);


                respuesta.victima = victima;

                agresor.lugarExpedicionAgresor = LugarExpedicion(agresorSolicitud.IdInvolucrado).Result;

                respuesta.agresor = agresor;
                return respuesta;


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public async Task RegistrarMedidaProteccion(RequestMedidaProteccionDTO data)
        {
            SicofaSolicitudServicio solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstOrDefault();
            SicofaSolicitudServicioMedidaProtecion medidaPrevia = context.SicofaSolicitudServicioMedidaProtecion.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstOrDefault();

            if (solicitud == null)
            {
                throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
            }

            if (medidaPrevia == null)
            {

                SicofaSolicitudServicioMedidaProtecion medidas = new SicofaSolicitudServicioMedidaProtecion();
                medidas.IdSolicitudServicio = data.idSolicitudServicio;
                medidas.Pruebas = data.PruebasDocumento;
                medidas.NombreTestigo = data.nombreTestigo;
                medidas.CelularTestigo = data.celularTestigo;
                medidas.CorreoElectronicoTestigo = data.correoTestigo;
                medidas.DireccionTestigo = data.direccionTestigo;
                medidas.InformacionObservacion = data.textoFijoB;
                medidas.InformacionTexto = data.textoFijoA;
                context.SicofaSolicitudServicioMedidaProtecion.Add(medidas);
                await context.SaveChangesAsync();

                await RegistroViolenciaMedidaProteccion(medidas.IdMedida, data.tipoViolencia);

            }
            else
            {

                medidaPrevia.IdSolicitudServicio = data.idSolicitudServicio;
                medidaPrevia.Pruebas = data.PruebasDocumento;
                medidaPrevia.NombreTestigo = data.nombreTestigo;
                medidaPrevia.CelularTestigo = data.celularTestigo;
                medidaPrevia.CorreoElectronicoTestigo = data.correoTestigo;
                medidaPrevia.DireccionTestigo = data.direccionTestigo;
                medidaPrevia.InformacionObservacion = data.textoFijoB;
                medidaPrevia.InformacionTexto = data.textoFijoA;

                await context.SaveChangesAsync();
                await RegistroViolenciaMedidaProteccion(medidaPrevia.IdMedida, data.tipoViolencia);
            }

        }


        public RequestMedidaProteccionDTO ObtenerInformacionMedidasProteccion(long idSolicitudServicio)
        {
            try
            {
                var medida = context.SicofaSolicitudServicioMedidaProtecion.Where(s => s.IdSolicitudServicio
                == idSolicitudServicio).FirstOrDefault();

                if (medida == null)
                {

                    throw new Exception("No existe una medida de proteccion asociada");
                }

                var tiposviolencia = context.SicofaMedidaProteccionViolencia.Where(s => s.IdMedidaProtecion == medida.IdMedida).Select(me => me.IdTipoViolencia).ToList();


                RequestMedidaProteccionDTO salida = new RequestMedidaProteccionDTO();
                salida.tipoViolencia = tiposviolencia;
                salida.textoFijoA = medida.InformacionTexto;
                salida.textoFijoB = medida.InformacionObservacion;
                salida.idSolicitudServicio = idSolicitudServicio;
                salida.nombreTestigo = medida.NombreTestigo;
                salida.celularTestigo = medida.CelularTestigo;
                salida.correoTestigo = medida.CorreoElectronicoTestigo;
                salida.direccionTestigo = medida.DireccionTestigo;
                salida.PruebasDocumento = medida.Pruebas;

                return salida;

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        private async Task RegistroViolenciaMedidaProteccion(long idmedida, List<int?> tipoviolencia)
        {
            try
            {
                var previos = context.SicofaMedidaProteccionViolencia.Where(s => s.IdMedidaProtecion == idmedida).ToList();

                List<SicofaMedidaProteccionViolencia> entradas = new List<SicofaMedidaProteccionViolencia>();
                if (!previos.Count.Equals(0))
                {

                    context.SicofaMedidaProteccionViolencia.RemoveRange(previos);
                }



                foreach (var violencia in tipoviolencia)
                {
                    SicofaMedidaProteccionViolencia entrada = new SicofaMedidaProteccionViolencia();
                    entrada.IdMedidaProtecion = idmedida;
                    entrada.IdTipoViolencia = violencia;
                    entradas.Add(entrada);
                }

                context.SicofaMedidaProteccionViolencia.AddRange(entradas);
                context.SaveChanges();

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }


        }


        #endregion hu2



        #region hu15


        public List<TestProcedure> Testprocedure()
        {


            var sql = "TipoRemision";


            var result = Simply.ExecuteStoreProdecure(sql).ToListAsync();

            return result.Result;


        }

        public async Task<List<SicofaTipoRemision>> ObtenerTiposRemision()
        {
            try
            {
                var response = await context.SicofaTipoRemision.ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }




        }

        public async Task<SicofaSolicitudServicio> ObtenerSolicitudServicio(long idSolicitud)
        {

            var response = await context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitud).FirstOrDefaultAsync();

            if (response == null)
            {

                throw new Exception(ErrorRespuestaEvaluacionRiesgo.solicitudNoexiste);
            }

            return response; ;


        }

        public async Task<Tuple<long, long>> getVictimaAgresor(long idSolicitudServicio)
        {
            try
            {
                var solicitud = await context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).
                    Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();

                if (solicitud == null)
                {

                    throw new Exception(ErrorRespuestaEvaluacionRiesgo.solicitudNoexiste);
                }

                var victima = solicitud.IdInvolucrado.Where(s => s.EsVictima == true && s.EsPrincipal == true).FirstOrDefault();
                if (victima == null)
                {
                    throw new Exception(ReportesRemision.victima);
                }

                var agresor = solicitud.IdInvolucrado.Where(s => s.EsVictima == false && s.EsPrincipal == true).FirstOrDefault();

                if (agresor == null)
                {
                    throw new Exception(ReportesRemision.agresor);
                }

                Tuple<long, long> salida = new Tuple<long, long>(victima.IdInvolucrado, agresor.IdInvolucrado);


                return salida;

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }



        }


        public async Task<DocumentoRemisionDTO> OficioMedicinaLegal(long idVictima, long idSolicitudServicio)
        {

            var salida = await (from invo in context.SicofaInvolucrado
                                join municipio in context.SicofaCiudadMunicipio on invo.IdLugarExpedicion equals municipio.IdCiudadMunicipio
                                into invoMunicipio
                                from iMun in invoMunicipio.DefaultIfEmpty()
                                join dominio in context.SicofaDominio on invo.TipoDocumento equals dominio.IdDominio
                                where invo.IdInvolucrado == idVictima
                                select new DocumentoRemisionDTO
                                {
                                    nombreVictima = $"{invo.PrimerNombre} {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                    numeroDocumentoVictima = invo.NumeroDocumento,
                                    lugarExpedicionVictima = iMun.Nombre,
                                    tipoDocumentoVictima = dominio.Codigo,
                                    edadVictima = invo.Edad
                                }
                                          ).FirstAsync();

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);

            return salida;

        }

        public async Task<DocumentoRemisionDTO> SecretariaMujer(long idVictima, long idSolicitudServicio)
        {
            var salida = await (from invo in context.SicofaInvolucrado
                                join municipio in context.SicofaCiudadMunicipio on invo.IdLugarExpedicion equals municipio.IdCiudadMunicipio
                                into invoMunicipio
                                from iMun in invoMunicipio.DefaultIfEmpty()
                                join dominio in context.SicofaDominio on invo.TipoDocumento equals dominio.IdDominio
                                where invo.IdInvolucrado == idVictima
                                select new DocumentoRemisionDTO
                                {
                                    nombreVictima = $"{invo.PrimerNombre} {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                    numeroDocumentoVictima = invo.NumeroDocumento,
                                    lugarExpedicionVictima = iMun.Nombre,
                                    tipoDocumentoVictima = dominio.Codigo,
                                    direccionResidenciaVictima = invo.DireccionRecidencia.TrimEnd(),
                                    barrioVictima = invo.Barrio,
                                    telefonoVictima = invo.Telefono,
                                    correoElectronicoVictima = invo.CorreoElectronico

                                }
                                            ).FirstAsync();

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);


            return salida;
        }

        public async Task<DocumentoRemisionDTO> ProcesoPsicologiaExterna(long idVicitma, long IdAgresor, long idSolicitudServicio)
        {
            long[] involucrados = { idVicitma, IdAgresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {

                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.IdInvolucrado == idVicitma).First();
            var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();
            salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
            salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);



            return salida;

        }

        public async Task<DocumentoRemisionDTO> ApoyoPolicivoVictima(long idVictima, long Idagresor, long idSolicitudServicio)
        {
            long[] involucrados = { idVictima, Idagresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {

                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.IdInvolucrado == idVictima).First();
            var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();



            salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
            salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";
            salida.direccionResidenciaVictima = victima.DireccionRecidencia.TrimEnd();
            salida.barrioVictima = victima.Barrio;
            salida.numeroDocumentoVictima = victima.NumeroDocumento;
            salida.correoElectronicoAgresor = agresor.CorreoElectronico;
            salida.telefonoVictima = victima.Telefono;

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);
            salida.tipoDocumentoVictima = await TipoDocumento(victima.IdInvolucrado);
            salida.tipoDocumentoAgresor = await TipoDocumento(victima.IdInvolucrado);
            salida.numeroDocumentoAgresor = agresor.NumeroDocumento;
            salida.direcionResidenciaAgresor = agresor.DireccionRecidencia.TrimEnd();
            salida.barrioAgresor = agresor.Barrio;

            var informacionComisaria = await InformacionComisaria(idSolicitudServicio);
            salida.direccionComisaria = informacionComisaria.Item1;
            salida.telefonoComisaria = informacionComisaria.Item2;
            salida.correoComisaria = informacionComisaria.Item3;


            return salida;
        }


        public async Task<DocumentoRemisionDTO> DenunciaFiscalia(long idVictima, long Idagresor, long idSolicitudServicio)
        {

            long[] involucrados = { idVictima, Idagresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            var dum = (from involes in context.SicofaInvolucrado
                       where involucrados.Contains(involes.IdInvolucrado)
                       select new { involes.Nombres }).ToArray();

            List<ReporteDenunciaFiscaliaDTO> invo = await (from involu in context.SicofaInvolucrado
                                                           join tipodoc in context.SicofaDominio on involu.TipoDocumento equals tipodoc.IdDominio
                                                           join genero in context.SicofaDominio on involu.IdGenero equals genero.IdDominio into gj
                                                           from sub in gj.DefaultIfEmpty()
                                                           join municipio in context.SicofaCiudadMunicipio on involu.IdLugarExpedicion equals municipio.IdCiudadMunicipio
                                                            into invoMunicipio
                                                           from iMun in invoMunicipio.DefaultIfEmpty()
                                                           where involucrados.Contains(involu.IdInvolucrado)
                                                           select new ReporteDenunciaFiscaliaDTO
                                                           {
                                                               idInvolucrado = involu.IdInvolucrado,
                                                               nombres = $"{involu.PrimerNombre} {involu.SegundoNombre} {involu.PrimerApellido} {involu.SegundoApellido}",
                                                               numeroDocumento = involu.NumeroDocumento,
                                                               tipoDocumento = tipodoc.Codigo,
                                                               edad = involu.Edad,
                                                               genero = sub.NombreDominio,
                                                               direccionResidencia = involu.DireccionRecidencia.TrimEnd(),
                                                               barrio = involu.Barrio,
                                                               telefono = involu.Telefono,
                                                               correoCorreoElectronico = involu.CorreoElectronico,
                                                               esPrincipal = involu.EsPrincipal,
                                                               esVictima = involu.EsVictima,
                                                               lugarExpedicion = iMun.Nombre,
                                                               informacionFamiliar = $"nombres :{involu.NombreContactoConfianza}  telefono : {involu.TelefonoContactoConfianza} , direccion : {involu.DireccionContactoConfianza}",
                                                               fechaNacimiento = involu.FechaNacimiento
                                                           }

                                               ).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {

                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.idInvolucrado == idVictima).First();

            var agresor = invo.Where(s => s.esVictima == false & s.esPrincipal == true).First();
            var complementoAgresor = context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == Idagresor).First();



            salida.nombreVictima = victima.nombres;
            salida.tipoDocumentoVictima = victima.tipoDocumento;
            salida.numeroDocumentoVictima = victima.numeroDocumento;
            salida.lugarExpedicionVictima = victima.lugarExpedicion;
            salida.direccionResidenciaVictima = victima.direccionResidencia;
            salida.barrioVictima = victima.barrio;
            salida.telefonoVictima = victima.telefono;
            salida.correoElectronicoVictima = victima.correoCorreoElectronico;
            salida.lugarExpedicionVictima = victima.lugarExpedicion;
            salida.edadVictima = victima.edad;
            salida.generoVictima = victima.genero;
            salida.informacionFamiliarVictima = victima.informacionFamiliar;

            salida.nombreAgresor = agresor.nombres;
            salida.tipoDocumentoAgresor = agresor.tipoDocumento;
            salida.numeroDocumentoAgresor = agresor.numeroDocumento;
            salida.generoAgresor = agresor.genero;
            salida.direcionResidenciaAgresor = agresor.direccionResidencia;
            salida.barrioAgresor = agresor.barrio;
            salida.telefonoAgresor = agresor.telefono;
            salida.correoElectronicoAgresor = agresor.correoCorreoElectronico;

            int? edadAgresor = null;

            if (agresor.fechaNacimiento != null)
            {
                DateTime fechaNacimientoAgresor = (DateTime)agresor.fechaNacimiento;
                edadAgresor = DateTime.Today.Year - fechaNacimientoAgresor.Year;
            }

            salida.edadAgresor = complementoAgresor != null ? complementoAgresor.EdadAproximadaAgresor : edadAgresor;

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);


            return salida;


        }

        public async Task<DocumentoRemisionDTO> VisitaDomiciaria(long idVictima, long idAgresor, long idsolicitudServicio)
        {
            long[] involucrados = { idVictima, idAgresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {

                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.IdInvolucrado == idVictima).First();
            var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();

            salida.direccionResidenciaVictima = victima.DireccionRecidencia!.TrimEnd();
            salida.barrioVictima = victima.Barrio;
            salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";

            salida.direcionResidenciaAgresor = agresor.DireccionRecidencia!.TrimEnd();
            salida.barrioAgresor = agresor.Barrio;
            salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";

            salida.ciudadRemision = await ciudadRemision(idsolicitudServicio);


            return salida;
        }

        public async Task<DocumentoRemisionDTO> RegimenSalud(long idVictima, long idAgresor, long idSolicitudServicio)
        {
            long[] involucrados = { idVictima, idAgresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {
                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.IdInvolucrado == idVictima).First();
            var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();

            salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";

            salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);

            return salida;


        }

        public async Task<DocumentoRemisionDTO> ProtocoloRiesgo(long idVictima, long idAgresor, long idSolicitudServicio)
        {

            long[] involucrados = { idVictima, idAgresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {
                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.IdInvolucrado == idVictima).First();
            var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();

            salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
            salida.direccionResidenciaVictima = victima.DireccionRecidencia.TrimEnd();
            salida.barrioVictima = victima.Barrio;
            salida.localidadVictima = victima.Localidad;
            salida.telefonoVictima = victima.Telefono;

            salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";
            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);
            var informacionComisaria = await InformacionComisaria(idSolicitudServicio);
            salida.nombreComisaria = informacionComisaria.Item4;




            return salida;
        }

        public async Task<DocumentoRemisionDTO> HistoriaClinica(long idVictima, long idAgresor, long idSolicitudServicio)
        {
            long[] involucrados = { idVictima, idAgresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<ReporteDenunciaFiscaliaDTO> invo = await (from involu in context.SicofaInvolucrado
                                                           join tipodoc in context.SicofaDominio on involu.TipoDocumento equals tipodoc.IdDominio

                                                           where involucrados.Contains(involu.IdInvolucrado)
                                                           select new ReporteDenunciaFiscaliaDTO
                                                           {
                                                               idInvolucrado = involu.IdInvolucrado,
                                                               nombres = $"{involu.PrimerNombre} {involu.SegundoNombre} {involu.PrimerApellido} {involu.SegundoApellido}",
                                                               numeroDocumento = involu.NumeroDocumento,
                                                               tipoDocumento = tipodoc.Codigo,
                                                               barrio = involu.Barrio,
                                                               direccionResidencia = involu.DireccionRecidencia.TrimEnd(),
                                                               esPrincipal = involu.EsPrincipal,
                                                               esVictima = involu.EsVictima,
                                                               telefono = involu.Telefono
                                                           }

                                              ).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {

                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.idInvolucrado == idVictima).First();

            var agresor = invo.Where(s => s.esVictima == false & s.esPrincipal == true).First();

            salida.nombreVictima = victima.nombres;
            salida.direccionResidenciaVictima = victima.direccionResidencia;
            salida.barrioVictima = victima.barrio;
            salida.tipoDocumentoVictima = victima.tipoDocumento;
            salida.numeroDocumentoVictima = victima.numeroDocumento;
            salida.telefonoVictima = victima.telefono;
            salida.nombreAgresor = agresor.nombres;
            salida.tipoDocumentoAgresor = agresor.tipoDocumento;
            salida.numeroDocumentoAgresor = agresor.numeroDocumento;

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);

            return salida;

        }


        public async Task<DocumentoRemisionDTO> RemisionFormatoPolicia(long idVictima, long idAgresor)
        {

            long[] involucrados = { idVictima, idAgresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {
                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.IdInvolucrado == idVictima).First();
            var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();

            salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
            salida.barrioVictima = victima.Barrio;
            salida.direccionResidenciaVictima = victima.DireccionRecidencia.TrimEnd();

            salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";

            salida.tipoDocumentoVictima = await TipoDocumento(victima.IdInvolucrado);
            salida.tipoDocumentoAgresor = await TipoDocumento(victima.IdInvolucrado);

            return salida;
        }

        public async Task<DocumentoRemisionDTO> RemisionFormatoPersoneria(long idVictima, long IdAgresor, long idSolicitudServicio)
        {

            long[] involucrados = { idVictima, IdAgresor };
            DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

            List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

            if (invo.Count < ReportesRemision.cantidadIvolucrados)
            {
                throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
            }

            var victima = invo.Where(s => s.IdInvolucrado == idVictima).First();
            var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();

            salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";

            salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";

            salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);

            return salida;

        }

        public async Task<DocumentoRemisionDTO> RemisionTratamientoTerapeutico(long idVictima, long idAgresor, long idSolicitudServicio)
        {
            try
            {
                long[] involucrados = { idVictima, idAgresor };
                DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

                List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

                if (invo.Count < ReportesRemision.cantidadIvolucrados)
                {
                    throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
                }
                var victima = invo.Where(s => s.IdInvolucrado == idVictima).First();
                var agresor = invo.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();
                salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
                salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";

                salida.ciudadRemision = await ciudadRemision(idSolicitudServicio);

                return salida;
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }

        }

        public async Task<DocumentoRemisionDTO> SolicitudEvaluacionRiesgo(long idVictima, long idAgresor, long idSolicitudServicio)
        {
            try
            {
                long[] involucrados = { idVictima, idAgresor };
                DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

                List<SicofaInvolucrado> invo = await context.SicofaInvolucrado.Where(s => involucrados.Contains(s.IdInvolucrado)).ToListAsync();

                if (invo.Count < ReportesRemision.cantidadIvolucrados)
                {
                    throw new Exception(ReportesRemision.faltaCantidadInvolucrados);
                }
                var victima = invo.Where(s => s.EsVictima == true && s.IdInvolucrado == idVictima).FirstOrDefault();
                var agresor = invo.Where(s => s.EsVictima == false && s.IdInvolucrado == idAgresor).FirstOrDefault();
                salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
                salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";
                salida.telefonoVictima = victima.Telefono;
                salida.numeroDocumentoVictima = victima.NumeroDocumento;
                salida.numeroDocumentoAgresor = agresor.NumeroDocumento;
                salida.direccionResidenciaVictima = victima.DireccionRecidencia;

                salida.tipoDocumentoVictima = await TipoDocumento(victima.IdInvolucrado);

                salida.tipoDocumentoAgresor = await TipoDocumento(victima.IdInvolucrado);

                return salida;



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }





        private async Task<string> ciudadRemision(long idSolicitudServicio)
        {
            try
            {
                var ciudad = await (from soli in context.SicofaSolicitudServicio
                                    join comi in context.SicofaComisaria on soli.IdComisaria equals comi.IdComisaria
                                    join ciu in context.SicofaCiudadMunicipio on comi.IdCiudadMunicipio equals ciu.IdCiudadMunicipio
                                    where soli.IdSolicitudServicio == idSolicitudServicio
                                    select ciu.Nombre
                              ).FirstOrDefaultAsync();

                return ciudad;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        private async Task<string> TipoDocumento(long idinvolucrado)
        {
            try
            {
                var documento = string.Empty;

                var tipoDocumento = await (from involo in context.SicofaInvolucrado
                                           join dominio in context.SicofaDominio on involo.TipoDocumento equals dominio.IdDominio
                                           where involo.IdInvolucrado == idinvolucrado
                                           select dominio.Codigo
                                       ).FirstOrDefaultAsync();

                if (tipoDocumento != null)
                {

                    documento = tipoDocumento
;
                }

                return documento;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private async Task<Tuple<string, string, string, string>> InformacionComisaria(long idSolicitudServicio)
        {
            try
            {
                var solicitud = await (from soli in context.SicofaSolicitudServicio
                                       join comisaria in context.SicofaComisaria on soli.IdComisaria equals comisaria.IdComisaria
                                       where soli.IdSolicitudServicio == idSolicitudServicio
                                       select Tuple.Create(comisaria.Direccion, comisaria.Telefono, comisaria.CorreoElectronico, comisaria.Nombre)).FirstOrDefaultAsync();

                return solicitud;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        #endregion hu15




        #region CargaRemision

        public async Task<int> ObtenerRemision(string data)
        {
            try
            {
                var remision = await context.SicofaDocumento.Where(s => s.NombreDocumento == data).FirstOrDefaultAsync();

                if (remision == null)
                {

                    throw new Exception("remision no identificada");
                }

                return remision.IdDocumento;

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        public async Task<long> RegistrarSolicitudRemision(long idInvolucrado, int idRemision, long idSolicitudServicio, long? idAnexo)
        {
            int estado = await context.SicofaDominio.Where(s => s.Codigo == Notificacion.enviada).Select(s => s.IdDominio).FirstOrDefaultAsync();
            SicofaDocumentoServicioSolicitud remision1 = new SicofaDocumentoServicioSolicitud();

            remision1.IdDocumento = idRemision;
            remision1.IdSolicitudServicio = idSolicitudServicio;
            remision1.Personalizada = false;
            remision1.IdInvolucrado = idInvolucrado;
            remision1.IdEstado = estado;
            remision1.IdAnexo = idAnexo;

            context.SicofaDocumentoServicioSolicitud.Add(remision1);

            await context.SaveChangesAsync();

            return remision1.IdDocServ;


        }


        #endregion cargaRemision
        #region cambioshu15



        public async Task<List<InvolucradoSelectDTO>> ObtenerListaInvolucrado(long idSolicitudServicio)
        {
            try
            {
                var listado = await context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();

                if (listado.IdInvolucrado == null)
                {

                    throw new Exception(CargaRemisiones.noInvolucrados);
                }

                List<InvolucradoSelectDTO> salida = new List<InvolucradoSelectDTO>();

                foreach (var involucrado in listado.IdInvolucrado)
                {

                    InvolucradoSelectDTO entrada = new InvolucradoSelectDTO();
                    entrada.idInvolucrado = involucrado.IdInvolucrado;
                    entrada.nombres = $"{involucrado.PrimerNombre} {involucrado.SegundoNombre} {involucrado.PrimerApellido} {involucrado.SegundoApellido}";
                    entrada.documento = involucrado.NumeroDocumento;

                    if (involucrado.EsVictima != false)
                    {

                        salida.Add(entrada);
                    }
                }

                return salida;

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }


        }

        public async Task<List<RemisionDisponiblesDTO>> RemisionesDisponiblesPorInvolucrado(long idInvolucrado, string? estado)
        {
            try
            {
                RemisionDisponiblesDTO remision = new RemisionDisponiblesDTO();
                List<RemisionDisponiblesDTO> salida = new List<RemisionDisponiblesDTO>();

                var involucrado = await context.SicofaInvolucrado.Where(s => s.IdInvolucrado == idInvolucrado).FirstOrDefaultAsync();

                if (involucrado.EsVictima)
                {

                    salida = await context.SicofaDocumento.Where(s => (s.EsVictima == true || s.EsVictima == false) && s.Codigo == EliminacionAnexo.remision &&
                         s.Estado == estado)
                        .Select(s => new RemisionDisponiblesDTO { idRemision = s.IdDocumento, nombre = s.NombreDocumento }).ToListAsync();
                }
                else
                {

                    salida = await context.SicofaDocumento.Where(s => s.EsVictima == false && s.Codigo == EliminacionAnexo.remision &&
                         (s.Estado == estado))
                        .Select(s => new RemisionDisponiblesDTO { idRemision = s.IdDocumento, nombre = s.NombreDocumento }).ToListAsync();

                }

                return salida;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }



        public async Task<List<RemisionesAsociada>> RemisionesAsociadasPorSolicitud(long idSolicitud, long idTarea)
        {

         
            try
            {
               

                List<RemisionesAsociada> salida = await (from docu in context.SicofaDocumentoServicioSolicitud
                                                         join documen in context.SicofaDocumento on docu.IdDocumento equals documen.IdDocumento
                                                         join solianexo in context.SicofaSolicitudServicioAnexo on docu.IdAnexo equals solianexo.IdSolicitudAnexo
                                                         join invo in context.SicofaInvolucrado on docu.IdInvolucrado equals invo.IdInvolucrado
                                                         join usu in context.SicofaUsuarioSistema on solianexo.IdUsuario equals usu.IdUsuarioSistema
                                                         where docu.IdSolicitudServicio == idSolicitud 
                                                         && (solianexo.IdTarea == idTarea)
                                                         && documen.Codigo == EliminacionAnexo.remision

                                                         select
                                                   new RemisionesAsociada
                                                   {
                                                       nombreInvolucrado = $"{invo.PrimerNombre}  {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                                       nombreRemision = documen.NombreDocumento,
                                                       idAnexo = solianexo.IdSolicitudAnexo,
                                                       nombreUsuario = usu.Nombres,
                                                       fecha = Convert.ToString(solianexo.FechaCreacion)
                                                   }).ToListAsync();


                return salida;



            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }

        }

        public async Task ActualizarAnexoRemision(long idAnexo)
        {
            try
            {

                var anexo = context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudAnexo == idAnexo).FirstOrDefault();

                if (anexo == null)
                {

                    throw new Exception(ErrorNotificacion.noNotificacion);
                }

                anexo.FechaActualizacion = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }





        #endregion camboishu15


        public async Task<string> LugarExpedicion(long idInvolucrado)
        {
            try
            {

                var lugar = await (from invo in context.SicofaInvolucrado
                                   join ciudad in context.SicofaCiudadMunicipio on invo.IdLugarExpedicion equals ciudad.IdCiudadMunicipio
                                   where invo.IdInvolucrado == idInvolucrado

                                   select ciudad.Nombre).FirstOrDefaultAsync();

                return lugar;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

    }





}





