using sicf_DataBase.Repositories.Comisaria;
using sicf_Models.Constants;
using sicf_Models.Dto.Comisaria;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Comisaria
{
    public class ComisariaService : IComisariaService
    {
        private readonly IComisariaRepository _comisariaRepository;

        public ComisariaService (IComisariaRepository comisariaRepository)
        {
            _comisariaRepository = comisariaRepository;
        }

        public string IniciarComisaria(CreacionComisariaDTO data)
        {
            try
            {
                var validacion = ValildarComisaria(data);

                if (validacion.Length.Equals(0))
                {
                    _comisariaRepository.IniciarComisaria(data);
                    return Constants.ComisariaMensaje.comisariaCreada;
                }

                throw new ControledException(validacion);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InformacionComisariaDTO> InformacionComisaria(int idComisaria)
        {
            try
            {
                var response =await _comisariaRepository.InformacionComisaria(idComisaria);
                return response;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task ActualizarComisaria(InformacionComisariaDTO data, int comisaria)
        {
            try
            {
                await _comisariaRepository.ActualizarComisaria(data, comisaria);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ComisariaDTO> ConsultarComisaria(RequestComisariaDTO data)
        {
            try
            {
                return _comisariaRepository.ConsultarComisaria(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ValildarComisaria(CreacionComisariaDTO data)
        {
            long? id;

            id = _comisariaRepository.ValidarCodigoComisaria(data.codigoComisaria);
            if (!id!.Value.Equals(0))
                return Constants.Message.CodigoComisariaExiste;

            id = _comisariaRepository.ValidarnombreComisaria(data.nombreComisaria);
            if (!id!.Value.Equals(0))
                return Constants.Message.NombreComisariaExiste;

            id = _comisariaRepository.ValidarCorreoComisario(data.comisario.correoElectronico);
            if (!id!.Value.Equals(0))
                return Constants.Message.CorreoComisarioExiste;

            id = _comisariaRepository.ValidarCorreoComisario(data.comisario.correoElectronico);
            if (!id!.Value.Equals(0))
                return Constants.Message.CorreoComisarioExiste;

            id = _comisariaRepository.ValidarIdentificacionComisario(data.comisario.numeroDocumento.ToString());
            if (!id!.Value.Equals(0))
                return Constants.Message.IdentificacionComisarioExiste;

            return String.Empty;         
        }

        public ComisarioDTO? ConsultarComisario(long idComisaria)
        {
            try
            {
                return _comisariaRepository.ConsultarComisario(idComisaria);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UsuarioComisariaDTO>? ConsutalUsuarioComisaria(long idComisaria)
        {
            try
            {
                return _comisariaRepository.ConsutalUsuarioComisaria(idComisaria);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ControledResponseDTO> CrearMinisterio(CreacionComisariaDTO ministerio)
        {
            try
            {
                return await _comisariaRepository.CrearMinisterio(ministerio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<InformacionComisariaDTO>> CargarComisarias(List<MComisariaDTO> comisarias)
        {
            try
            {
                return await _comisariaRepository.CargarComisarias(comisarias);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Tuple<string, string> ObtenerNombreComisariayComisario(long id)
        {
            try
            {
                return _comisariaRepository.ObtenerNombreComisariayComisario(id);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);

            }
        }
    }
}
