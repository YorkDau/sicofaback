using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_DataBase.Repositories.SolicitudesRepository;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Ciudadano;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace sicf_DataBase.Repositories.TestEntity
{
    public class SolicitudEFRespository : BaseRepository<SicofaCiudadano> , ISolicitudEFRepository
    {

        private readonly SICOFAContext context;
        public SolicitudEFRespository(SICOFAContext context) : base(context)
        {

            this.context = context;
        }



        public  List<SolicitudServicioEFDTO> SolicitudesServicioPorCiudadano(long id) {


            var solicitudes =  context.SicofaSolicitudServicio.Where(s => s.IdCiudadano == id).ToList();

            List<SolicitudServicioEFDTO> salida = new List<SolicitudServicioEFDTO>();

            foreach (var x in solicitudes) 
            {

                SolicitudServicioEFDTO solicitud = new SolicitudServicioEFDTO();


                string estado =  (from solicitudServicioestado in context.SicofaSolicitudServicioEstadoSolicitud
                                 join estadoSolicitud in context.SicofaEstadoSolicitud on solicitudServicioestado.IdEstadoSolicitud equals estadoSolicitud.IdEstadoSolicitud
                                 where solicitudServicioestado.IdSolicitudServicio == x.IdSolicitudServicio

                                 select estadoSolicitud.EstadoSolicitud
                                 ).First();

                solicitud.IdSolicitudServicio = x.IdSolicitudServicio;
                solicitud.FechaSolicitud = x.FechaSolicitud;
                solicitud.CodigoSolicitud = x.CodigoSolicitud;
                solicitud.estadoSolicitud = estado;
                solicitud.DescripcionDeHechos = x.DescripcionDeHechos;
                solicitud.HoraSolicitud = x.HoraSolicitud;

                salida.Add(solicitud);

            }

            return salida;

        }

        public async Task<bool> CerrarSolicitud(long idSolicitudServicio)
        {
            try
            {
                var _solicitud = await context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();

                if (_solicitud != null)
                {
                    _solicitud.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                    _solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.levantada;
                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new ControledException(Constants.Message.SolicitudNoexiste);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
