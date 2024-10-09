using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Quorum;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Quorum
{
    public class QuorumServicioRepository : IQuorumServicioRepository
    {
        private readonly SICOFAContext context;

        public QuorumServicioRepository(SICOFAContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<QuorumDTO>> ListaInvolucradosQuorum(long idSolitiudServicio, long idTarea)
        {
            try
            {
                var involucrados = await context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolitiudServicio).FirstOrDefaultAsync();

                List<QuorumDTO> quorumList = new List<QuorumDTO>();
                foreach (var Involucrado in involucrados.IdInvolucrado)
                {
                    QuorumDTO quorum = new QuorumDTO();
                    quorum.idInvolucrado = Involucrado.IdInvolucrado;
                    quorum.idSolicitudServicio = idSolitiudServicio;
                    quorum.nombreInvolucrado = $"{Involucrado.PrimerNombre} {Involucrado.SegundoNombre} {Involucrado.PrimerApellido} {Involucrado.SegundoApellido}";
                    var infoQuorum = await conusltarQuorum(Involucrado.IdInvolucrado, idTarea);
                    quorum.idQuorum = infoQuorum is null ? -1 : infoQuorum.Item1;
                    quorum.idAnexo = infoQuorum is null ? -1 : infoQuorum.Item2;
                    quorum.estadoQuorum = estadoQuorum(infoQuorum is null ? -1 : infoQuorum.Item3);
                    quorum.EsVictima = Involucrado.EsVictima;
                    quorum.EsPricipal = Involucrado.EsPrincipal;
                    quorum.IdEstado = infoQuorum is null ? -1 : infoQuorum.Item3;

                    quorumList.Add(quorum);
                }
                return quorumList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private string estadoQuorum(int? idEstado)
        {
            string nombreEstado = "No Asiste";

            switch (idEstado)
            {
                case Constants.EstadosQuorum.asiste:
                    nombreEstado = "Asiste";
                    break;
                case Constants.EstadosQuorum.noAsiste:
                    nombreEstado = "No Asiste";
                    break;
                case Constants.EstadosQuorum.excusaConJustaCausa:
                    nombreEstado = "Excusa con justa causa";
                    break;
                case Constants.EstadosQuorum.excusaSinJustaCausa:
                    nombreEstado = "Excusa sin justa causa";
                    break;
            }


            return nombreEstado;
        }

        private async Task<Tuple<long, long?, int?>> conusltarQuorum(long idInvolucrado, long idTarea)
        {
            try
            {
                var quorum = await context.SicofaQuorum.Where(x => x.IdInvolucrado == idInvolucrado &&
                                                         x.IdTarea == idTarea).Select(s => Tuple.Create(s.IdQuorum, s.IdAnexo, s.IdEstado)).FirstOrDefaultAsync();

                return quorum;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task ActualizarQuorum(RequestActualizarQuorumDTO data)
        {
            try
            {
                var quorum = await context.SicofaQuorum.Where(s => s.IdQuorum == data.IdQuorum).FirstOrDefaultAsync();

                quorum.IdAnexo = data.IdAnexo == 0 ? quorum.IdAnexo : data.IdAnexo;
                quorum.IdEstado = data.IdEstado;
                
                await context.SaveChangesAsync();

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }

        public async Task<bool> GuardarQuorum(RequestQuorumDTO quorum)
        {

            try
            {
                var datosE = await context.SicofaInvolucrado.Where(s => s.IdInvolucrado == quorum.IdInvolucrado).Select(d => Tuple.Create(d.EsPrincipal,d.EsVictima)).FirstAsync();
                var idProgramacion = await context.SicofaProgramacion.Where(f => f.IdSolicitud == quorum.IdSolicitudServicio).OrderByDescending(s => s.FechaModifica)
                                    .Select(g => g.IdProgramacion).FirstOrDefaultAsync();

                if (idProgramacion == 0 || idProgramacion == null)
                {
                    throw new ControledException(Constants.Auto.MensajeErrorSinProgramacion);
                }

                SicofaQuorum NuevoQuorum  = new SicofaQuorum();
                NuevoQuorum.IdProgramacion = idProgramacion;
                NuevoQuorum.IdSolicitudServicio = quorum.IdSolicitudServicio;
                NuevoQuorum.IdInvolucrado = quorum.IdInvolucrado;
                NuevoQuorum.IdAnexo = quorum.IdAnexo;
                NuevoQuorum.IdEstado = quorum.IdEstado;
                NuevoQuorum.IdTarea = quorum.IdTarea;
                NuevoQuorum.EsPricipal = datosE.Item1;
                NuevoQuorum.EsVictima = datosE.Item2; 

                context.SicofaQuorum.Add(NuevoQuorum);
                await context.SaveChangesAsync();

                return true;
            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
