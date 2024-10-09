using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sicf_Models.Core;
using sicf_Models.Dto.Apelacion;

namespace sicf_DataBase.Repositories.Apelacion
{
    public interface IApelacionRepository
    {
        public Task<SicofaApelacion> ConsultarApelacion(long idTarea);
        public bool ActualizarTareaApelacion(long idTarea, string estado, DateTime? fechaTerminacion);
        public bool CrearTareaNulidad(long idTarea, int idFlujo, long idSolicitudServicio);
        public Task<bool> CerrarApelacion(long idTarea);
        public Task<SicofaApelacion> ObtenerApelacion(ApelacionObtencionDTO apelacion);
        public Task<bool> ActualizarApelacion(ApelacionDTO apelacion);
        public bool ActualizarMedidas(ApelacionDTO apelacion);
        public bool AplicarEstadoMedidas(long idSolicitudServicio);
        public List<ApelacionMedidasDTO> ConsultarMedidasApelacion(long idTarea);
        public List<ApelacionTareasDTO> ConsultarTareasApelacion(long idSolicitudServicio);
        public int ConsultarMedidasSeguimiento(long idSolicitudServicio);
        public bool ActualizarSolicitudServicio(long idSolicitudServicio, string estado, string subestado);
    }
}
