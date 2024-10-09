using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Dominio
{
    public class DominioRepository : IDominioRepository
    {

        private readonly SICOFAContext context;

        public DominioRepository(SICOFAContext context)
        {
            this.context = context;
        }


        public List<DominioAgrupadoDTO> ListaDominio()
        {
            try
            {
                var response =  context.SicofaDominio.GroupBy(p => p.TipoDominio,
                (key, g) => new DominioAgrupadoDTO { nombreDominio = g.First().TipoDominio }
                ).ToList();
                return response;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }


        public async  Task<List<DominioAsociadoDTO>> DominioPorGrupo(string data)
        {
            try
            {
                var response = await context.SicofaDominio.Where(s => s.TipoDominio == data).Select( se => new DominioAsociadoDTO { id = se.IdDominio , codigo = se.Codigo, nombre = se.NombreDominio} ).ToListAsync();

                return response;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task AgregarDominio(EntradaDominioDTO data)
        {
            try
            {
                var check= await context.SicofaDominio.AnyAsync(s => s.Codigo == data.codigo);

                if (check)
                    throw new Exception(DominioMensajes.errorPrevio);

                SicofaDominio dominio = new SicofaDominio();

                dominio.Codigo = data.codigo;
                dominio.NombreDominio = data.nombreDominio;
                dominio.TipoDominio = data.TipoDominio;
                dominio.Activo = true;
                dominio.TipoLista = data.tipoLista;

                context.SicofaDominio.Add(dominio);

                await context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditarDominio(DominioActualizarDTO data)
        {
            try
            {
                /*var check = await context.SicofaDominio.Where(s => s.IdDominio != data.id & s.Codigo == data.codigo).FirstOrDefaultAsync();

                if (check != null)
                    throw new Exception(DominioMensajes.errorPrevio);*/

                var actualizar = await context.SicofaDominio.Where(s => s.IdDominio == data.id).FirstAsync();
                
                //actualizar.TipoLista = data.tipoLista;
                actualizar.NombreDominio = data.nombreDominio;
                actualizar.Activo = data.activo;
                //actualizar.Codigo = data.codigo;
                await context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DominioActualizarDTO> DominioDetalles(int id)
        {
            try
            {
                var salida = await context.SicofaDominio.Where(s => s.IdDominio == id).Select( se => new DominioActualizarDTO { id = se.IdDominio
                
                ,
                nombreDominio = se.NombreDominio , activo = (bool)se.Activo , codigo = se.Codigo , tipoLista = se.TipoLista}).FirstAsync();

                return salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }



    }
}
