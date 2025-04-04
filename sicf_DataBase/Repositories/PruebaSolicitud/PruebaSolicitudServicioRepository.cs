using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.PruebaSolicitud;
using sicf_Models.Dto.PruebasPard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.PruebaSolicitud
{
    public class PruebaSolicitudServicioRepository : IPruebaSolicitudServicioRepository
    {
        private readonly SICOFAContext context;

        public PruebaSolicitudServicioRepository(SICOFAContext context)
        {
            this.context = context;
        }


        private async Task<List<PruebaAsociadaDTO>> ListaPruebaAsociadas(long idSolicitud, string tipoPrueba)
        {
            try
            {
                var Lista = await (
                    from pruebas in context.SicofaSolicitudPrueba
                    join anexo in context.SicofaSolicitudServicioAnexo on pruebas.IdAnexo equals anexo.IdSolicitudAnexo
                    join usua in context.SicofaUsuarioSistema on anexo.IdUsuario equals usua.IdUsuarioSistema
                    join involucrado in context.SicofaInvolucrado on pruebas.IdInvolucrado equals involucrado.IdInvolucrado into inv
                    from i in inv.DefaultIfEmpty() // LEFT JOIN para involucrado
                    join dominio in context.SicofaDominio on pruebas.IdPruebaPericial equals dominio.IdDominio into dominioJoin
                    from d in dominioJoin.DefaultIfEmpty() // LEFT JOIN para dominio
                    where pruebas.IdSolicitudServicio == idSolicitud && pruebas.TipoPrueba == tipoPrueba
                    select new PruebaAsociadaDTO
                    {
                        // Datos básicos de la prueba
                        idPrueba = pruebas.IdSolicitudPrueba,
                        tipoPrueba = pruebas.TipoPrueba,
                        nombrePrueba = pruebas.NombreArchivo,
                        idAnexo = anexo.IdSolicitudAnexo,
                        fecha = anexo.FechaCreacion,

                        // Información del usuario del sistema
                        idUsuario = usua.IdUsuarioSistema,
                        nombreUsuario = $"{usua.Nombres} {usua.Apellidos}",

                        // Información del involucrado (si existe)
                        nombreInvolucrado = i != null ?
                            $"{(i.PrimerNombre ?? "")} {(i.SegundoNombre ?? "")} {(i.PrimerApellido ?? "")} {(i.SegundoApellido ?? "")}".Trim()
                            : (d != null ? d.NombreDominio : "Sin prueba pericial"),
                        documentoInvolucrado = i != null ? i.NumeroDocumento : null,
                        tipoDocumentoInvolucrado = i != null ? i.TipoDocumento.ToString() : null,

                        // Información de la prueba pericial (mejorada)
                        idPruebaPericial = pruebas.IdPruebaPericial ?? 1,
                        nombrePruebaPericial = pruebas.IdPruebaPericial == null ? "Sin tipo definido" :
                                             (d != null ? d.NombreDominio : "Tipo no encontrado")
                    }).ToListAsync();

                return Lista;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<PruebaAsociadaDTO>> PruebaAsociadas(long idSolitiudServicio, long idTarea)
        {
            try
            {
              var periciales = await  ListaPruebaAsociadas(idSolitiudServicio, cPruebaSolicitud.periciales);


                var accioanteAccionado = await ListaPruebaAsociadas(idSolitiudServicio, cPruebaSolicitud.pruebaAccionanteAccionado);


                var salida = periciales.Concat(accioanteAccionado)
                       .OrderByDescending(x => x.fecha)
                       .ToList();

                return salida;


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public async Task RegistrarPruebaSolicitud(long idsolicitudServicio, long idTarea, string tipoPrueba, long idAnexo, long? idinvolucrado, string nombre, int IdPruebaPericial)
        {
            try
            {

                SicofaSolicitudPrueba entrada = new SicofaSolicitudPrueba()
                { IdSolicitudServicio = idsolicitudServicio, IdTarea = idTarea, TipoPrueba = tipoPrueba, IdAnexo = idAnexo, IdInvolucrado = idinvolucrado, NombreArchivo = nombre,IdPruebaPericial = IdPruebaPericial  };

                context.SicofaSolicitudPrueba.Add(entrada);

                await context.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public async Task EliminarPruebaSolicitud(long idPrueba)
        {
            try
            {
                var prueba = await context.SicofaSolicitudPrueba.Where(s => s.IdSolicitudPrueba == idPrueba).FirstOrDefaultAsync();

                if (prueba != null)
                {

                    context.SicofaSolicitudPrueba.Remove(prueba);

                    await context.SaveChangesAsync();
                }
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public async Task<List<PruebaAsociadaJuezDTO>> PruebaAsociadasJuez(long idSolitiudServicio, long idTarea)
        {

            try
            {

                var pruebasJuez = await (from pruebas in context.SicofaSolicitudPrueba
                                         join anexo in context.SicofaSolicitudServicioAnexo on pruebas.IdAnexo equals anexo.IdSolicitudAnexo
                                         where pruebas.IdSolicitudServicio == idSolitiudServicio &
                                         pruebas.IdTarea == idTarea & pruebas.TipoPrueba == cPruebaSolicitud.pruebaJuez
                                         select new PruebaAsociadaJuezDTO
                                         {

                                             nombrePrueba = pruebas.NombreArchivo,
                                             idAnexo = anexo.IdSolicitudAnexo,
                                             idPrueba = pruebas.IdSolicitudPrueba,

                                             fechaCreacion = (DateTime)anexo.FechaCreacion!,
                                             fechaModificacion = (DateTime)anexo.FechaCreacion!

                                         }
                                                          ).ToListAsync();

                return pruebasJuez;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task EditarPruebaJuez(long idPrueba, long idAnexo)
        {
            var prueba = await context.SicofaSolicitudPrueba.Where(s => s.IdSolicitudPrueba == idPrueba).FirstOrDefaultAsync();

            if (prueba == null)
            {
                throw new Exception("no existe la prueba");
            }

            var anexo = await context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudAnexo == idAnexo).FirstAsync();

            anexo.FechaActualizacion = DateTime.Now;
            await context.SaveChangesAsync();
        }











    }
}
