using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.PerfilPermisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.PerfilPermisos
{
    public class PerfilPermisosRepository : IPerfilPermisosRepository
    {

        private readonly SICOFAContext context;

        public PerfilPermisosRepository(SICOFAContext context)
        {
            this.context = context;
        }



        public async Task CrearPerfil(CrearPerfilDTO data , int idComisaria)
        {
            try
            {
               var idPerfilCreado = await CrearPerfilSimple(data, idComisaria);

                await AgregarActividadPorPerfil(idPerfilCreado , data.Actividades);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task EditarPerfil(EditarPerfilDTO data)
        {
            try
            {
                var perfil = await context.SicofaPerfil.Where(s => s.IdPerfil == data.idPerfil).FirstAsync();

                perfil.NombrePerfil = data.nombrePerfil;
                perfil.Codigo = data.Codigo;
                perfil.Estado = data.Estado;

               await  context.SaveChangesAsync();

                await EditarActividadesPorPerfil(data.idPerfil , data.Actividades);

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        // informacion basica del perfil
        public async Task<PerfilEdicionDTO> ObtenerPerfilId(long id) {

            try
            {
                var perfilSalida = await context.SicofaPerfil.Where(se => se.IdPerfil == id)
                    .Select(sel => new PerfilEdicionDTO { IdPerfil = sel.IdPerfil, nombrePerfil = sel.NombrePerfil, codigo = sel.Codigo,Estado =(bool)sel.Estado }).FirstAsync();

                return perfilSalida;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        // metodo complementaario a informacion de actividades asociadas al perfil y si estan activas,
        public async Task<IEnumerable<PerfilActividadEdicionDTO>> ActividadesPorPerfil(int idPerfil)
        {
            try
            {
                var perfil = context.SicofaPerfil.Include(se => se.SicofaPerfilActividad).Where(s => s.IdPerfil == idPerfil).First();

                var salida = await (from perfilActividad in context.SicofaPerfilActividad
                                 join actividad in context.SicofaActividad on perfilActividad.IdActividad equals actividad.IdActividad
                                 where perfilActividad.IdPerfil == idPerfil
                                 select new PerfilActividadEdicionDTO
                                 {
                                     nombreActividad = actividad.NombreActividad,
                                     IdActividad = actividad.IdActividad,
                                     activo = (bool)actividad.Estado
                                 }).ToListAsync();

  /*              var cheto = dom.Select(seu => seu.IdActividad).ToArray();
                var active = (from actividad in context.SicofaActividad
                              where !cheto.Contains(actividad.IdActividad)
                              select new PerfilActividadEdicionDTO { nombreActividad = actividad.NombreActividad, IdActividad = actividad.IdActividad, activo = false }).ToList();

                var salida = dom.Concat(active);
  */

                return salida;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }


        public async Task<List<ActividadesDTO>> ObtenerListaActividades()
        {
            try {

                return await context.SicofaActividad.Select(s => new ActividadesDTO { IdActividad = s.IdActividad, NombreTarea = s.NombreActividad }).ToListAsync();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        #region MetodosInternos
        private async Task<int> CrearPerfilSimple(CrearPerfilDTO data, int Idcomisaria) 
        {

            try
            {
                SicofaPerfil perfil = new SicofaPerfil();
                perfil.NombrePerfil = data.nombrePerfil;
                perfil.Codigo = data.Codigo;
                perfil.Estado = true;

                context.SicofaPerfil.Add(perfil);

                await context.SaveChangesAsync();

                return perfil.IdPerfil;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        private async Task AgregarActividadPorPerfil(int idperfil, List<int> actividades)
        {
            try
            {
                List<SicofaPerfilActividad> inner = new List<SicofaPerfilActividad>();

                foreach (var actividad in actividades)
                {

                    SicofaPerfilActividad act = new SicofaPerfilActividad();
                    act.IdPerfil = idperfil;
                    act.IdActividad = actividad;
                    inner.Add(act);
                }

                context.SicofaPerfilActividad.AddRange(inner);


               await  context.SaveChangesAsync();
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }

        private async Task<List<Tuple<int, string>>> ActividadPorPerfil(int idPerfil)
        {
            try
            {

                var salida = await (from perfilActividad in context.SicofaPerfilActividad
                              join actividad in context.SicofaActividad on perfilActividad.IdActividad equals actividad.IdActividad
                              where perfilActividad.IdPerfil == idPerfil
                              select Tuple.Create(actividad.IdActividad, actividad.NombreActividad)
                              ).ToListAsync();

                return salida;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }


        private async Task EditarActividadesPorPerfil(int idPerfil, List<int> actividades)
        {
            try
            {
                var perfil = await context.SicofaPerfil.Include( u => u.SicofaPerfilActividad).Where(s => s.IdPerfil == idPerfil).FirstAsync();

                context.SicofaPerfilActividad.RemoveRange(perfil.SicofaPerfilActividad);

                List<SicofaPerfilActividad> inner = new List<SicofaPerfilActividad>();

                foreach (var actividad in actividades)
                {

                    SicofaPerfilActividad act = new SicofaPerfilActividad();
                    act.IdPerfil = idPerfil;
                    act.IdActividad = actividad;
                    inner.Add(act);
                }

                context.SicofaPerfilActividad.AddRange(inner);
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        
        }

        #endregion MetodosInternos




    }
}
