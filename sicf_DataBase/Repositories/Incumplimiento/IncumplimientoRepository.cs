using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.Incumplimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Incumplimiento
{
    public class IncumplimientoRepository : IIncumplimientoRepository
    {

        private readonly SICOFAContext context ;

        public IncumplimientoRepository(SICOFAContext context)
        {
            this.context = context;
        }



        public async Task<ReporteIncumplimientoDTO> ReporteIncumplimiento(IncumplimientoReporteRequest data) 
        {
            try
            {
                ReporteIncumplimientoDTO reporte = new ReporteIncumplimientoDTO();

                reporte.ciudadSolicitud = await CiudadSolicitud(data.idSolicitudServicio);
                var comisaria = await InformacionComisaria(data.idSolicitudServicio);
                reporte.comisaria = new ComisariaDTO(comisaria.Item1, comisaria.Item2, comisaria.Item3, comisaria.Item4);
                reporte.victima = await Victima(data.idSolicitudServicio);
                reporte.agresor = await Agresor(data.idSolicitudServicio);
                reporte.victimaSecundario = await VictimaSecundaria(data.idSolicitudServicio);
                reporte.adicional = await InfoAdiconalIncumplimiento(data.idUsuario, data.idComisaria);

                return reporte;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RegistrarReporteIncumplimiento(IncumplimientosDTO data)
        {
            try
            {
                SicofaSolicitudServicioIncumplimiento creacion = new SicofaSolicitudServicioIncumplimiento();
                creacion.IdAnexo = data.idAnexo;
                creacion.IdTarea = data.idTarea;
                creacion.IdSolicitudServicio = data.idSolicitudServicio; 

                await context.SicofaSolicitudServicioIncumplimiento.AddAsync(creacion);

                await context.SaveChangesAsync();

                if (data.adicional != null)
                {
                    data.adicional.idIncumplimiento = creacion.IdIncumplimiento; ;
                    await RegistrarIncumplimientoDatosAdicionales(data.adicional);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task ActualizarReporteIncumplimiento(IncumplimientosDTO data)
        {
            try
            {
                var incumplimineto = await context.SicofaSolicitudServicioIncumplimiento.Where(x => x.IdIncumplimiento == data.idIncumplimiento).FirstOrDefaultAsync();
                incumplimineto.IdTarea = (data.idTarea == 0 ? incumplimineto.IdTarea : data.idTarea);
                incumplimineto.IdSolicitudServicio = (data.idSolicitudServicio == 0 ? incumplimineto.IdSolicitudServicio : data.idSolicitudServicio);
                incumplimineto.IdAnexo = (data.idAnexo == 0 ? incumplimineto.IdAnexo : data.idAnexo);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RegistrarIncumplimientoDatosAdicionales(IncumplimientoAdicionalDTO data)
        {
            try
            {
                SicofaIncumplimientoComplementaria creacion = new SicofaIncumplimientoComplementaria();
                creacion.IdIncumplimiento = data.idIncumplimiento;
                creacion.NombreFuncionario = data.nombreFuncionario;
                creacion.Cargo = data.cargo;
                creacion.NombreInstitucion = data.nombreInstitucion;
                creacion.DireccionInstitucion = data.direccionInstitucion;
                creacion.Email = data.correo;
                creacion.Telefono = data.telefono;

                await context.SicofaIncumplimientoComplementaria.AddAsync(creacion);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ValidarArchivoIncumplimiento(long idSolicitudServicio, long idTarea)
        {
            try
            {
               return await context.SicofaSolicitudServicioIncumplimiento.AnyAsync(s => s.IdSolicitudServicio == idSolicitudServicio && s.IdTarea == idTarea);
                
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<long> DocumentoIncumplimiento(long idSolicitudServicio, long idtarea)
        {
            try
            {
                var response= await context.SicofaSolicitudServicioIncumplimiento.Where(se => se.IdSolicitudServicio == idSolicitudServicio && se.IdTarea == idtarea).FirstAsync();

                return response.IdAnexo;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task EliminarIncumplimiento(long idAnexo)
        {
            try
            {
                var anexo =  await context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudAnexo == idAnexo).FirstAsync();

                var incumplimiento = await context.SicofaSolicitudServicioIncumplimiento.Where(de => de.IdAnexo == idAnexo).FirstAsync();

                context.SicofaSolicitudServicioIncumplimiento.Remove(incumplimiento);

                context.SicofaSolicitudServicioAnexo.Remove(anexo);

                await context.SaveChangesAsync();


            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        
        }

        public async Task<IncumplimientoAdicionalDTO> InfoAdiconalIncumplimiento(long idUsuario, long idComisaria)
        {
            try
            {
                IncumplimientoAdicionalDTO data = new IncumplimientoAdicionalDTO();
                var infoUsuario = await context.SicofaUsuarioSistema.Where(s => s.IdUsuarioSistema == idUsuario).FirstOrDefaultAsync();
                var infoComisaria = await context.SicofaComisaria.Where(s => s.IdComisaria == idComisaria).FirstOrDefaultAsync();

                data.nombreFuncionario = $"{infoUsuario.Nombres} {infoUsuario.Apellidos}";
                data.cargo = infoUsuario.Cargo;
                data.nombreInstitucion = infoComisaria.Nombre;
                data.direccionInstitucion = infoComisaria.Direccion;
                data.correo = infoComisaria.CorreoElectronico;
                data.telefono = infoComisaria.Telefono;

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region metodosReporte


        private async Task<InvolucradoIncumplimiento> Agresor(long idSolicitud) {

            var solicitud=await context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(s=> s.IdSolicitudServicio == idSolicitud).FirstAsync();

            var victimaSolicitud = solicitud.IdInvolucrado.Where(s => s.EsVictima == false & s.EsPrincipal == true).First();

            var complemento = await context.SicofaComplementoInvolucrado.Where(s => s.IdInvolucrado == idSolicitud).FirstOrDefaultAsync();

            InvolucradoIncumplimiento victima = new InvolucradoIncumplimiento();
            victima.nombreCompleto = $"{victimaSolicitud.PrimerNombre} {victimaSolicitud.SegundoNombre} {victimaSolicitud.PrimerApellido} {victimaSolicitud.SegundoApellido}";
            victima.direccionResidencia = victimaSolicitud.DireccionRecidencia;
            victima.numeroDocumento = victimaSolicitud.NumeroDocumento;
            victima.correoElectronico = victimaSolicitud.CorreoElectronico;
            victima.edad = (int)victima.edad == null ? (int)complemento.EdadAproximadaAgresor : 0;
            victima.telefono = victimaSolicitud.Telefono;
            victima.barrio = victimaSolicitud.Barrio;

            return victima;
        }

        private async Task<InvolucradoIncumplimiento> Victima(long idSolicitud)
        {


            var solicitud = await context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitud).FirstAsync();

            var victimaSolicitud = solicitud.IdInvolucrado.Where(s => s.EsVictima == true & s.EsPrincipal == true).First();

            InvolucradoIncumplimiento victima = new InvolucradoIncumplimiento();
            victima.nombreCompleto = $"{victimaSolicitud.PrimerNombre} {victimaSolicitud.SegundoNombre} {victimaSolicitud.PrimerApellido} {victimaSolicitud.SegundoApellido}";
            victima.direccionResidencia = victimaSolicitud.DireccionRecidencia;
            victima.numeroDocumento = victimaSolicitud.NumeroDocumento;
            victima.correoElectronico = victimaSolicitud.CorreoElectronico;
            victima.edad = victima.edad;
            victima.telefono = victimaSolicitud.Telefono;
            victima.barrio = victimaSolicitud.Barrio;
            victima.tipoRelacion = await TipoRelacion(victimaSolicitud.IdTipoRelacion != null ? (int)victimaSolicitud.IdTipoRelacion : 0 );

            return victima;
        }


        private async Task<List<InvolucradoIncumplimiento>> VictimaSecundaria(long idSolicitud) 
        {

            var solicitud = await context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitud).FirstAsync();


            var victimasSecundaria = solicitud.IdInvolucrado.Where(se => se.EsPrincipal == false & se.EsVictima == true).ToList();

            List<InvolucradoIncumplimiento> salida = new List<InvolucradoIncumplimiento>();

            foreach (var invo in victimasSecundaria) {


                InvolucradoIncumplimiento invocomple = new InvolucradoIncumplimiento();
                invocomple.nombreCompleto = $"{invo.PrimerNombre} {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}";
                invocomple.tipoDocumento = await TipoDocumento(invo.IdInvolucrado);
                invocomple.numeroDocumento = invo.NumeroDocumento;
                invocomple.edad = invo.Edad == null ? 0 : (int)invo.Edad;

                salida.Add(invocomple);
            }


            return salida;
        }
        // ciudad de la solicitud
        private async Task<string> CiudadSolicitud(long idSolicitudServicio)
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

        // informacion comisaria
        private async Task<Tuple<string, string, string, string>> InformacionComisaria(long idSolicitudServicio)
        {
            try
            {
                var solicitud = await (from soli in context.SicofaSolicitudServicio
                                       join comisaria in context.SicofaComisaria on soli.IdComisaria equals comisaria.IdComisaria
                                       where soli.IdSolicitudServicio == idSolicitudServicio
                                       select Tuple.Create(comisaria.Nombre,
                                                           comisaria.Direccion, 
                                                           comisaria.Telefono, 
                                                           comisaria.CorreoElectronico )).FirstOrDefaultAsync();

                return solicitud;
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

        private async Task<string> TipoRelacion(int idRelacion)
        {
            try
            {
                string tipoRelacion = string.Empty;

                var tiporelacion =await context.SicofaDominio.Where(s => s.IdDominio == idRelacion).FirstOrDefaultAsync();

                if (tipoRelacion != null) {

                    tipoRelacion = tiporelacion.NombreDominio;
                }

                return tipoRelacion;
            }
            catch (Exception e) {

                throw new Exception(e.Message);
            }
        }

        


        #endregion metodosReporte

    }
}
