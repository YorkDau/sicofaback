using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Notificacion;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Notificaciones
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly SICOFAContext context;


        public NotificacionRepository(SICOFAContext context) 
        {

            this.context = context;
            
        }


        public async Task<List<NotificacionDTO>> ObtenerTipoNotificacion() {

            try
            {

          return  await context.SicofaDocumento.Where(s => s.Codigo == Notificacion.codigoNoficiacion)
                    .Select( se => new NotificacionDTO { idNotificacion = se.IdDocumento , nombreNotificacion = se.NombreDocumento } ).ToListAsync();

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }

             
        }

        public async Task<IEnumerable<NotificacionSolicitud>> NotificacionesAsociadas(long idSolicitudServicio, string tipoNotificacion,long idTarea)
        {
            try
            {

                List<NotificacionSolicitud> ListadoNotificados = new List<NotificacionSolicitud>();
                var involucrados = await context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(se => se.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();

               // if (involucrados != null)
                //{
                    foreach (var involucrado in involucrados.IdInvolucrado)
                    {

                        NotificacionSolicitud entrada = new NotificacionSolicitud();

                        var notificacion = await EncontrarNotificacion(involucrado.IdInvolucrado, tipoNotificacion, idTarea);
                        entrada.idInvolucrado = involucrado.IdInvolucrado;
                        entrada.nombres = $"{involucrado.PrimerNombre} {involucrado.SegundoNombre} {involucrado.PrimerApellido} {involucrado.SegundoApellido}";
                        entrada.idAnexo = notificacion != null ? notificacion.Item1 : null;
                        entrada.estado = notificacion != null ? notificacion.Item2 : Notificacion.noEnvidad;

                        ListadoNotificados.Add(entrada);

                    }


                    return ListadoNotificados;

                //}
                //else {

                //    throw new Exception(Notificacion.involucradosNotificar);
                //}

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private async Task<Tuple<long?,string>> EncontrarNotificacion(long idInvolucrado, string tipoNotificacion,long idTarea)
        {
            try
            {
                var notificacion = await (from noti in context.SicofaDocumentoServicioSolicitud
                                        join docu in context.SicofaDocumento on noti.IdDocumento equals docu.IdDocumento
                                        join domi in context.SicofaDominio on noti.IdEstado equals domi.IdDominio
                                    where noti.IdInvolucrado == idInvolucrado & docu.NombreDocumento == tipoNotificacion
                                    && noti.IdTarea == idTarea
                                        select Tuple.Create(noti.IdAnexo,domi.NombreDominio)).FirstOrDefaultAsync();

                return notificacion!;
             

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<List<RemisionesAsociada>> NotificacionAsociadaPorSolicitud(long idSolicitudServicio,long idTarea)
        {
            try
            {
                List<RemisionesAsociada> salida = await (from docu in context.SicofaDocumentoServicioSolicitud
                                                         join documen in context.SicofaDocumento on docu.IdDocumento equals documen.IdDocumento
                                                         join solianexo in context.SicofaSolicitudServicioAnexo on docu.IdDocumento equals solianexo.IdDocumento
                                                         join invo in context.SicofaInvolucrado on docu.IdInvolucrado equals invo.IdInvolucrado
                                                         join usu in context.SicofaUsuarioSistema on solianexo.IdUsuario equals usu.IdUsuarioSistema
                                                         join domi in context.SicofaDominio on docu.IdEstado equals domi.IdDominio
                                                         where docu.IdSolicitudServicio == idSolicitudServicio && documen.Codigo == Notificacion.codigoNoficiacion
                                                         && docu.IdTarea == idTarea

                                                         select
                                                         new RemisionesAsociada
                                                         {
                                                             nombreInvolucrado = $"{invo.PrimerNombre}  {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                                             nombreRemision = documen.NombreDocumento,
                                                             idAnexo = solianexo.IdSolicitudAnexo,
                                                             nombreUsuario = $"{usu.Nombres} {usu.Apellidos}",
                                                             fecha = solianexo.FechaCreacion.ToString(),
                                                             estado = domi.NombreDominio
                                                             
                                                         }).ToListAsync();

                return salida;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<ReporteNotificacionDTO> IncidenteDeIncumplimiento(long idSolicitudServicio , long idInvolucrado) 
        {
            try
            {
                ReporteNotificacionDTO salida = new ReporteNotificacionDTO();

                var informacionComisaria = await InformacinComisaria(idSolicitudServicio);

                var solicitud=context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(se => se.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault();
                var involucrados = solicitud.IdInvolucrado;
                var victima = involucrados.Where(s => s.EsVictima == true & s.EsPrincipal == true).FirstOrDefault();
                var agresor = involucrados.Where(s => s.IdInvolucrado == idInvolucrado).FirstOrDefault();

                salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
                salida.nombreAgresor = $"{agresor.PrimerNombre} {agresor.SegundoNombre} {agresor.PrimerApellido} {agresor.SegundoApellido}";

                salida.lugarExpedicionVictima = await LugarExpedicion(victima.IdInvolucrado);
                salida.lugarExpedicionAgresor = await LugarExpedicion(agresor.IdInvolucrado);

                return salida;

            }
            catch (Exception ex)
            { 
            
                throw new Exception(ex.Message);
            
            }
        }


        public async Task<ReporteNotificacionDTO> MedidaDeProteccion(long idSolicitudServicio, long idInvolucrado)
        {
            try
            {
                ReporteNotificacionDTO salida = new ReporteNotificacionDTO();

                var informacionComisaria = await InformacinComisaria(idSolicitudServicio);

                var solicitud = context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(se => se.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault();

                if (solicitud != null)
                {
                    var involucrados = solicitud.IdInvolucrado;
                    var victima = involucrados.Where(s => s.IdInvolucrado == idInvolucrado).First();
                    var Codigo = context.SicofaDominio.Where(y => y.IdDominio == victima.TipoDocumento).First().Codigo;

                    salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
                    salida.tipoDocVictima = Codigo;
                    salida.numeroDocVictima = $"{victima.NumeroDocumento} ";

                    salida.lugarExpedicionVictima = await LugarExpedicion(victima.IdInvolucrado);

                    salida.ciudadNotificacion = informacionComisaria.Item5;

                    return salida;
                }
                else {

                    throw new Exception(Message.SolicitudNoexiste);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public async Task<ReporteNotificacionDTO> ConstanciaProteccion(long idSolicitudServicio, long idInvolucrado)
        {
            try
            {
                ReporteNotificacionDTO salida = new ReporteNotificacionDTO();

                var informacionComisaria = await InformacinComisaria(idSolicitudServicio);

                var solicitud = context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(se => se.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault();

                if (solicitud != null)
                {
                    var involucrados = solicitud.IdInvolucrado;
                    var victima = involucrados.Where(s => s.IdInvolucrado == idInvolucrado).First();
                    var Codigo = context.SicofaDominio.Where(y => y.IdDominio == victima.TipoDocumento).First().Codigo;
                    salida.tipoDocVictima = Codigo ;
                    salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";

                    salida.lugarExpedicionVictima = await LugarExpedicion(victima.IdInvolucrado);

                    return salida;
                }
                else { 
                
                     throw new Exception(Message.SolicitudNoexiste);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }


        private async Task<string> LugarExpedicion(long idInvolucrado){
            try
            {
                var salida = string.Empty;
                 salida = await (from involu in context.SicofaInvolucrado
                              join ciudad in context.SicofaCiudadMunicipio on involu.IdLugarExpedicion equals ciudad.IdCiudadMunicipio
                           where involu.IdInvolucrado == idInvolucrado
                           select ciudad.Nombre
                              ).FirstOrDefaultAsync();

                return salida;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);

            }

        }


        private async Task<Tuple<string, string, string, string, string>> InformacinComisaria(long idSolicitudServicio)
        {
            try
            {
                var solicitud = await (from soli in context.SicofaSolicitudServicio
                                       join comisaria in context.SicofaComisaria on soli.IdComisaria equals comisaria.IdComisaria
                                       join municipio in context.SicofaCiudadMunicipio on comisaria.IdCiudadMunicipio equals municipio.IdCiudadMunicipio
                                       where soli.IdSolicitudServicio == idSolicitudServicio
                                       select Tuple.Create(comisaria.Direccion, comisaria.Telefono, comisaria.CorreoElectronico, comisaria.Nombre, municipio.Nombre)).FirstOrDefaultAsync();

                return solicitud;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }


        public async Task<bool> NotificacionPrevia(long idinvolucrado, string tipoRemision)
        {
            try
            {
                var tipodcumento = await context.SicofaDocumento.Where(s => s.NombreDocumento == tipoRemision).FirstOrDefaultAsync();
                if (tipodcumento == null) {
                    throw new Exception(ErrorNotificacion.noNotificacion);
                }
                return await context.SicofaDocumentoServicioSolicitud.AnyAsync(s => s.IdInvolucrado == idinvolucrado & s.IdDocumento == tipodcumento!.IdDocumento);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ActualizarNotificacion(long idinvolucrado, string tipoRemision, long idSolicitudServicio , long idanexo , long idTarea)
        {
            try
            {
                var tipodcumento = await context.SicofaDocumento.Where(s => s.NombreDocumento == tipoRemision).FirstOrDefaultAsync();
                int estado = await context.SicofaDominio.Where(s => s.Codigo == Notificacion.recibido).Select(s => s.IdDominio).FirstOrDefaultAsync();

                var  notificacion = await context.SicofaDocumentoServicioSolicitud.Where(s => s.IdSolicitudServicio == idSolicitudServicio & s.IdInvolucrado == idinvolucrado & s.IdDocumento == tipodcumento.IdDocumento & s.IdTarea == idTarea).FirstOrDefaultAsync();

                if (notificacion != null)
                {
                    notificacion.IdEstado = estado;
                    notificacion.IdAnexo = idanexo;
                }
                else {

                    throw new Exception(Notificacion.notificacionError);
                }


                await context.SaveChangesAsync();

            }
            catch (Exception ex) { 
            
                throw new Exception(ex.Message);
            }

        }

        public async Task<long> RegistrarNotificacion(long idInvolucrado, int idRemision, long idSolicitudServicio, long? idAnexo,long? idTarea)
        {
            int estado = await context.SicofaDominio.Where(s => s.Codigo == Notificacion.enviada).Select(s => s.IdDominio).FirstOrDefaultAsync();
            SicofaDocumentoServicioSolicitud remision1 = new SicofaDocumentoServicioSolicitud();

            remision1.IdDocumento = idRemision;
            remision1.IdSolicitudServicio = idSolicitudServicio;
            remision1.Personalizada = false;
            remision1.IdInvolucrado = idInvolucrado;
            remision1.IdEstado = estado;
            remision1.IdAnexo = idAnexo;
            remision1.IdTarea = idTarea;

            context.SicofaDocumentoServicioSolicitud.Add(remision1);

            await context.SaveChangesAsync();

            return remision1.IdDocServ;


        }
    }
}
