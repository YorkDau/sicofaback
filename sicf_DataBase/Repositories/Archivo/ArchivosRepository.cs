using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.Archivos;
using System.Data;
using System.Data.SqlClient;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Archivo
{
    public class ArchivosRepository : IArchivosRepository
    {

        private readonly SICOFAContext context;

        public ArchivosRepository(SICOFAContext context)
        {
            this.context = context;
        }

        public async Task<bool> getSolicitudServicio(long idSolicitudServicio) 
        {
            try
            {

               return await context.SicofaSolicitudServicio.AnyAsync(s => s.IdSolicitudServicio == idSolicitudServicio);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }
        }

        public async Task<string> ObtenerCodigoSolicitud(long idSolicitudServicio) 
        {
            try
            {

                var sol = await context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstAsync();

                return sol.CodigoSolicitud;

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }


        }

        public async Task<Tuple<bool,bool,int>> ObtenerTipoDocumentoAnexo(string documento)
        {
            try {

               var tipodocumento =await context.SicofaDocumento.Where(s => s.NombreDocumento == documento).FirstOrDefaultAsync();

                Tuple<bool, bool, int> salida = Tuple.Create(false, false , 0);
                if (tipodocumento !=null)
                {
                    salida = Tuple.Create(true,(bool)tipodocumento.Multiple!,tipodocumento.IdDocumento!);
                }

                return salida;

            } catch (Exception ex)
            {

                throw new Exception(ex.Message);
                    
          }
        
        
        }

        public async Task<long> RegistarArchivoSolicitud(int idDocumento, long idSolicitudServicio, string nombreDocumento, int idUsuario, long idTarea) {

            try
            {
                SicofaSolicitudServicioAnexo entrada = new SicofaSolicitudServicioAnexo();
                entrada.IdSolicitudServicio = idSolicitudServicio;
                entrada.IdDocumento = idDocumento;
                entrada.IdTarea = idTarea;
                entrada.Victima = true;
                entrada.NombreDocumento = nombreDocumento;
                entrada.IdUsuario = idUsuario;
                entrada.FechaCreacion = DateTime.Now;

                await context.SicofaSolicitudServicioAnexo.AddAsync(entrada);

                 await context.SaveChangesAsync();

                return entrada.IdSolicitudAnexo;
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }


        }

        
        public async  Task<List<SicofaSolicitudServicioAnexo>> ConsultarRegistroArchivo(long idSolicitudServicio, int idDocumento, long idTarea)
        {
            try
            {
                var archivoEntrada =await context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudServicio == idSolicitudServicio & s.IdDocumento == idDocumento & (s.IdTarea== idTarea)).ToListAsync();
                if (archivoEntrada == null) {

                    throw new Exception(CargaDocumento.noDocumento);
                }

                return archivoEntrada;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }

        }

        public async Task<string> ComisariaAsociada(long idSolicitudServicio)
        {
            try
            {
                string? nombre = await (
                    from soli in context.SicofaSolicitudServicio
                    join comi in context.SicofaComisaria on soli.IdComisaria equals comi.IdComisaria
                    where soli.IdSolicitudServicio == idSolicitudServicio
                    select comi.Nombre
                ).FirstOrDefaultAsync();

                return nombre ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la comisaría asociada: {ex.Message}", ex);
            }
        }

        public async Task<bool> ActualizarComisariaTrasladoRemision(long idComisariaTraslado, long idSolitudServicio, int idUsuario)
        {
            try
            {
                var solicitud = await context.SicofaSolicitudServicio
                    .FirstOrDefaultAsync(s => s.IdSolicitudServicio == idSolitudServicio);

                if (solicitud == null)
                    return false;

                long idComisariaOrigen = solicitud.IdComisaria;

                using (var connection = new SqlConnection(context.Database.GetConnectionString()))
                using (var command = new SqlCommand("PR_SICOFA_CREAR_REMISION_SOLICITUD_SERVICIO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_solicitud_servicio", idSolitudServicio);
                    command.Parameters.AddWithValue("@id_comisaria_origen", idComisariaOrigen);
                    command.Parameters.AddWithValue("@tipo_remision", 1);
                    command.Parameters.AddWithValue("@id_comisaria_destino", idComisariaTraslado);
                    command.Parameters.AddWithValue("@id_entidad_externa", DBNull.Value);
                    command.Parameters.AddWithValue("@justificacion", "COMPETENCIA A PREVENCION");
                    command.Parameters.AddWithValue("@usuario", idUsuario);

                    connection.Open();
                    var result = await command.ExecuteScalarAsync();
                    connection.Close();

                    return result != null && Convert.ToInt64(result) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar la remisión: {ex.Message}", ex);
            }
        }



        public async Task<Tuple<bool,bool>> ValidarActualizacion(long idSolicitud, string tipoDocumento, long idTarea)
        {
          
            var salida = await (from soli in context.SicofaSolicitudServicioAnexo
                          join docu in context.SicofaDocumento on soli.IdDocumento equals docu.IdDocumento
                          where soli.IdSolicitudServicio == idSolicitud & docu.NombreDocumento == tipoDocumento
                          & soli.IdTarea == idTarea
                          select Tuple.Create(soli.NombreDocumento, docu.Multiple)
                          ).FirstOrDefaultAsync();

            if (salida != null) {

                if ((bool)salida.Item2!)
                {
                    return Tuple.Create(true, (bool)salida.Item2);
                }
                else {

                    return Tuple.Create(true, false);

                }
                
                
            }

            return Tuple.Create(false,false); 


        }

        public async Task<int> ContadorArchivosMultiples(long idSolicitud, string tipoDocumento) 
        {

            var salida = await (from soli in context.SicofaSolicitudServicioAnexo
                                join docu in context.SicofaDocumento on soli.IdDocumento equals docu.IdDocumento
                                where soli.IdSolicitudServicio == idSolicitud & docu.NombreDocumento == tipoDocumento
                                select Tuple.Create(soli.NombreDocumento, docu.Multiple)
                          ).ToArrayAsync();

            return salida.Count();
        }
        //public Task<SicofaFormatos> ObtenerFormato(string nombreFormato, string codigo);

        public async Task<SicofaSolicitudServicioAnexo> ObtenerArchivoPorId(long idSolicitudAnexo)
        {
            try
            {

               var documento = await context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudAnexo == idSolicitudAnexo).FirstOrDefaultAsync();

                if (documento == null) {

                    throw new Exception(CargaDocumento.noDocumento);
                }

                return documento;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<string> ObtenerTipoAnexo(long idSolicitiudServicio, long  idAnexo)
        {
            try
            {
                context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudServicio == idSolicitiudServicio & s.IdSolicitudAnexo == idAnexo).FirstOrDefault();

                string tipoAnexo = await (from anexo in context.SicofaSolicitudServicioAnexo
                                 join docu in context.SicofaDocumento on anexo.IdDocumento equals docu.IdDocumento
                                 
                                 where anexo.IdSolicitudServicio == idSolicitiudServicio & anexo.IdSolicitudAnexo == idAnexo
                                 select docu.Codigo
                                 ).
                                 FirstAsync();

                return tipoAnexo;



            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            
            }
        }

        public async Task EliminarDocumentoServicio(long idDocumentoServicio)
        {
            try
            {
                var registro=await context.SicofaDocumentoServicioSolicitud.Where(s => s.IdDocServ == idDocumentoServicio).FirstOrDefaultAsync();

                context.SicofaDocumentoServicioSolicitud.Remove(registro);

              await  context.SaveChangesAsync();

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarDocumentoAnexo(long idSolicitudAnexo)
        {
            try
            {
                var registro = await context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudAnexo == idSolicitudAnexo).FirstOrDefaultAsync();

                if (registro != null) { 
                
                context.SicofaSolicitudServicioAnexo.Remove(registro);
                    await context.SaveChangesAsync();
                }



            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        public async Task<long> ObtenerRegistroEliminancion( long idAnexo)
        {
            try
            {
               
                var eliminacion = await context.SicofaDocumentoServicioSolicitud.Where(s => s.IdAnexo == idAnexo).FirstOrDefaultAsync();



                return eliminacion.IdDocServ;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }



        }


        public async Task<SicofaFormatos> ObtenerFormato(string nombreFormato, string codigo)
        {
            try
            {
                var listaFormatos = await context.SicofaFormatos.Where(s => (s.NombreDocumento == nombreFormato) || 
                                                                        (s.Codigo == codigo)).FirstOrDefaultAsync();

                if (listaFormatos == null)
                {
                    throw new Exception(Formato.noExisteFormato);
                }
                return listaFormatos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SicofaFormatos>> ListaFormatos()
        {
            try
            {
                return await context.SicofaFormatos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarAnexoInvolucradoAdicional(CargaActaVerificacionDerechosDTO data)
        {
            try
            {
                SicofaInvolucradoComplementaria complemen = await context.SicofaInvolucradoComplementaria.Where(s => s.IdInvolucrado == data.idInvolucrado).FirstOrDefaultAsync();
                if (complemen == null)
                {
                    complemen = new SicofaInvolucradoComplementaria();
                    complemen.IdInvolucrado = data.idInvolucrado;
                    complemen.IdAnexo = data.idAnexoServicio;

                    context.SicofaInvolucradoComplementaria.Add(complemen);
                }
                else
                    complemen.IdAnexo = data.idAnexoServicio;
                
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task GuardarNotificacion(CargaNotificacionPARD data , long idAnexo)
        {
            try
            {
                var documento =await context.SicofaDocumentoServicioSolicitud.Where(s => s.IdDocServ == data.idDocumento).FirstOrDefaultAsync();

                if (documento != null)
                {

                    documento.IdAnexo = idAnexo;
                    documento.IdInvolucrado = data.idInvolucrado;

                    await context.SaveChangesAsync();
                }
                else {

                    throw new Exception("documento no encontrado");
                }


            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }


        public async Task ActualizarNotificacionSolicitudAnexo(CargaNotificacionPARD data, long idAnexo)
        {
            try
            {
                var Anexo = await context.SicofaSolicitudServicioAnexo.Where(s => s.IdSolicitudAnexo == idAnexo).FirstOrDefaultAsync();

                if (Anexo != null)
                {

                    Anexo.idInvolucrado = data.idInvolucrado;

                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Anexo no encontrado");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

    }
}
