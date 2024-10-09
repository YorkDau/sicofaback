using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Seguridad;
using sicf_Models.Dto.Token;
using static sicf_Models.Constants.Constants;
using System.Net;
using CoreApiResponse;
using sicfServicesApi.Utility;
using sicf_Models.Dto.PerfilUsuario;
using SendGrid;
using sicf_Models.Dto.Usuario;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {

        private ISecurityService securityService;
        private IUsuarioHandler usuarioHandler;


        string caster = string.Empty;


        //private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(ISecurityService securityService, IUsuarioHandler usuarioHandler)
        {
            this.securityService = securityService;
            this.usuarioHandler = usuarioHandler;


        }

        [HttpPost("Ingreso")]

        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            //CrearUsuario();
            CrearUsuarioDTO data = new CrearUsuarioDTO();

            var perfiles = new List<int>();
            perfiles.Add(8);
            data.nombres = "admin";
            data.apellidos = "Perez";
            data.correoElectronico = "adminsuper@adminsuper.com";
            data.telefonoFijo = "2353534";
            data.celular = "3014101887";
            data.numeroDocumento = "10516747843";
            data.tipoDocumento = 1;
            data.perfiles = perfiles;
            data.Idcomisaria = 1;




            try
            {
                //await this.usuarioHandler.CrearUsuario(data);

                var response = await securityService.EntregarToken(login.email, login.password);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex) {


                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);
            }
        }
        

        [Authorize]
        [HttpPost("Cambioclave")]

        public async Task<IActionResult> CambioClave(CambioClaveDTO data)
        {
            try {
                var quest = Context.GetToken(HttpContext);

                await securityService.CambioClave(quest.usuario,data.password);

                return CustomResult(Message.Ok, UsuarioMensaje.cambioContrasena , HttpStatusCode.OK); 
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [Authorize]
        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var tu = Context.GetTokenRaw(HttpContext);
              var shu = securityService.ValidateToken(tu);

                if (shu.Item1)
                {
                   var fact = Context.GetToken(HttpContext);

                   var nuevoToken= securityService.RefreshToken(fact.usuario);

                    return CustomResult(Message.Ok, nuevoToken, HttpStatusCode.OK);
                }
                else {

                    return CustomResult(Message.ErrorInterno, UsuarioMensaje.noRefresh, HttpStatusCode.BadRequest);
                }
             
            }
            catch (Exception ex) {

                return Ok("vali");
            
            }
        
        }

        [HttpGet("ListaPerfiles")]

        public async Task<IActionResult> ListaPerfiles()
        {
            try
            {
                //
                var response = await securityService.ListaPerfiles();

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }


        [HttpPost("ResetPassword")]

        public async Task<IActionResult> Resetpass([FromBody] ResetPasswordDTO data)
        {
            try
            {
                    var response =await securityService.ValidarExistenciaCorreo(data.email);

                return CustomResult(Message.Ok, UsuarioMensaje.cambioContrasena, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);

            }
        
        }

        [HttpGet("CheckValidation/{id_usuario_sistema}/{pass}")]

        public async Task<IActionResult> verificarDuplicidadClave(int id_usuario_sistema, string pass) 
        {
            try
            {
                var response = await securityService.verificarDuplicidadClave(id_usuario_sistema , pass);

                return Ok(response);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);

            }
        
        }

      




    }
}
