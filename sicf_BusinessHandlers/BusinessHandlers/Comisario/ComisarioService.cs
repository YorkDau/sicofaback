using AutoMapper;
using sicf_BusinessHandlers.BusinessHandlers.Archivos;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.AbogadoRepository;
using sicf_DataBase.Repositories.Comisario;
using sicf_Models.Constants;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Archivos;
using sicf_Models.Dto.Comisario;
using sicf_Models.Dto.Solicitudes;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.Comisario
{
    public class ComisarioService : IComisarioService
    {
        private readonly IComisarioRepository comisarioRepository;
        private readonly ITareaHandler tareaHandler;
        private readonly IArchivoService archivoService;

        private readonly IMapper mapper;

        public ComisarioService(IComisarioRepository comisarioRepository, IMapper mapper, ITareaHandler tareaHandler, IArchivoService archivoService)
        {
            this.comisarioRepository = comisarioRepository;
            this.mapper = mapper;
            this.tareaHandler = tareaHandler;
            this.archivoService = archivoService;
        }


        public async Task RegistrarTomaDecision(RequestTomaDecisionDTO data)
        {
            try
            {
                if (data.concilacionPrevia)
                {
                    await GuardarArchivoAsync(data.actaConciliacionAnterior, data.idSolicitudServicio, "Acta_Conciliacion_Anterior", data.idEntidadTraslado);
                    await GuardarArchivoAsync(data.autoCierre, data.idSolicitudServicio, "Auto_Cierre"); //
                } 


                await comisarioRepository.RegistrarTomaDecision(data);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        
        }
        public async Task RegistrarTomaDecisionInformacion(RequestTomaDecisionInformacionDTO data)
        {
            try
            {
                if (data.cierre)
                {
                    await GuardarArchivoAsync(data.autoCierre, data.idSolicitudServicio, "Auto_Cierre");
                }
                await comisarioRepository.RegistrarTomaDecisionInformacion(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private async Task GuardarArchivoAsync(string? archivoBase64, long idSolicitud, string tipoDocumento, long? idEntidadTraslado = null)
        {
            if (archivoBase64 is { Length: > 0 })
            {
                var archivo = new CargaArchivoDTO
                {
                    idSolicitudServicio = idSolicitud,
                    entrada = archivoBase64,
                    tipoDocumento = tipoDocumento,
                };
                if (idEntidadTraslado != null)
                {
                    archivo.idEntidadTraslado = idEntidadTraslado;
                }

                await archivoService.Carga(archivo);
            }
        }


    }
}
