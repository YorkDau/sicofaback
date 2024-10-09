using sicf_Models.Core;
using sicf_Models.Dto.Plantilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Plantilla
{
    public interface IPlantillaRepository
    {
        public Task<List<PlantillaSPDTO>> ObtenerSecciones(long idSolicitudServicio);
        public List<PlantillaInvolucradoDTO> ObtenerInvolucrados(long idSolicitudServicio);
        public List<PlantillaSeccionesDTO> AsignarInvolucrados(List<PlantillaSPDTO> secciones, List<PlantillaInvolucradoDTO> involucrados);
        public List<PlantillaResponseTree> ObtenerJerarquia(List<PlantillaSPDTO> pSeccion, long? pSeccionPadre);
        public Task<bool> ActualizarSecciones(PlantillaGuardarDTO secciones);
        public Task<long> ActualizarPlantilla(PlantillaRequestFirmaDTO firma);
        public Task<bool> AplicarMedidas(PlantillaRequestFirmaDTO firma);
        public int ConsultarMedidasSeguimiento(long idSolicitudServicio);
        public bool ActualizarSolicitudServicio(long idSolicitudServicio, string estado, string subestado);

        public Task<Tuple<bool, List<long>>> MedidasValidar(List<long?> listadoMedidas, long idSolicitudServicio);

        public  Task<Tuple<string, string>> InformacionVictimaReporte(long idSolcitud);

        public  Task<Tuple<bool, List<long>>> ListadoMedidas(long idSolicitud);
    }
}
