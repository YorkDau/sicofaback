using AutoMapper;
using sicf_BusinessHandlers.BusinessHandlers.Compartido;
using sicf_DataBase.Repositories;
using sicf_DataBase.Repositories.Usuario;
using sicf_Models.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Usuario
{
    public class UsuarioHandler: IUsuarioHandler
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private ISendgridNotificaciones _IsendgridNotificaciones;

        public UsuarioHandler(IUnitofWork unitofWork, IMapper mapper, ISendgridNotificaciones IsendgridNotificaciones) {

            _unitofWork = unitofWork;
            _mapper = mapper;
            _IsendgridNotificaciones = IsendgridNotificaciones;
        }

        public bool IsUserPerfil(long userID, string codPefil) {

            try
            {
                return   _unitofWork.UsuarioRepository.IsUserPerfil(userID, codPefil);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        }

        public async Task CrearUsuario(CrearUsuarioDTO data)
        {
            try

            {
                var dataUsu = await _unitofWork.UsuarioRepository.CrearUsuario(data);

                await _unitofWork.UsuarioRepository.UsuarioAsignacionComisaria(dataUsu.Item1,data.Idcomisaria);

                await _unitofWork.UsuarioRepository.AsignarPerfiles(dataUsu.Item1 , data.perfiles, data.Idcomisaria);

                //await _unitofWork.UsuarioRepository.AgregarhistorialContrasena(dataUsu.Item1, dataUsu.Item2);

                _IsendgridNotificaciones.EnviarContrasena(data.correoElectronico , dataUsu.Item3);


            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        
        public async Task<bool> ActualizarUsuario(UsuarioDTO data)
        {
            try
            {
                return await _unitofWork.UsuarioRepository.ActualizarUsuario(data);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDTO> ConsultarUsuario(long userID)
        {
            try
            {
                return await _unitofWork.UsuarioRepository.ConsultarUsuario(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDTO> ConsultarUsuarioPorCorreo(string email)
        {
            try
            {
                return await _unitofWork.UsuarioRepository.ConsultarUsuarioPorCorreo(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UsuarioPerfilesDTO>> ListarUsuarios(int idComisaria)
        {
            try
            {

                return await _unitofWork.UsuarioRepository.ListarUsuarios(idComisaria);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<int> ComisariaUsuario(string email)
        {
            try
            {
                var response= await _unitofWork.UsuarioRepository.ComisariaUsuario(email);
                return response;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

       
    }
}
