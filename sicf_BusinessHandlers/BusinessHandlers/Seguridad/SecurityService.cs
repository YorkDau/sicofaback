using Azure;
using Humanizer.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sicf_BusinessHandlers.BusinessHandlers.Compartido;
using sicf_DataBase.Repositories.Comisaria;
using sicf_DataBase.Repositories.PerfilUsuario;
using sicf_DataBase.Repositories.Usuario;
using sicf_Models.Dto;
using sicf_Models.Dto.PerfilUsuario;
using sicf_Models.Dto.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.Seguridad
{
    public class SecurityService : ISecurityService
    {


        private readonly Authentication _Authenticacion;
        private readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;
        private IUsuarioRepository usuariorepository;
        private IPerfilUsuarioRepository perfilUsuarioRepository;
        private ISendgridNotificaciones sendgridNotificaciones;
        private IComisariaRepository comisariaRepository;
        public SecurityService(IOptions<Authentication> Authenticacion, IConfiguration configuration,  
            IUsuarioRepository usuariorepository, IPerfilUsuarioRepository perfilUsuarioRepository, ISendgridNotificaciones sendgridNotificaciones, IComisariaRepository comisariaRepository)
        {
            this._Authenticacion = Authenticacion.Value;
            this.Configuration = configuration;
            this.usuariorepository = usuariorepository;
            this.perfilUsuarioRepository = perfilUsuarioRepository;
            this.sendgridNotificaciones = sendgridNotificaciones;
            this.comisariaRepository = comisariaRepository;
        }
        public string pruebaCoso() {
          
            return _Authenticacion.SecretKey;
        }

        private string GenerateToken(string name, string user)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Authenticacion.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var key = Encoding.ASCII.GetBytes(_Authenticacion.SecretKey);

            //Claims
            var claims = new[]
            {
                new Claim("Name", name),
                new Claim("User", user)

            };

            //Payload
            var payload = new JwtPayload
            (
                _Authenticacion.Issuer,
                _Authenticacion.Audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_Authenticacion.MinutesToken))

            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<TokenPerfilesDTO> EntregarToken(string email, string password)
        {
            try
            {
               var verificacion = await  usuariorepository.VerificarCredenciales(email, password);

                if (!verificacion)
                {
                    throw new Exception(UsuarioMensaje.usuarioNoidentificado);
                }

                var usuarioInfo = await usuariorepository.InformacionUsuario(email);

                var comisarias = comisariaRepository.ConsultaComisariasUsuario(usuarioInfo.Item1);


                var perfilesComisaria = await usuariorepository.PerfilesUsuarioComisaria(usuarioInfo.Item1);


                    TokenPerfilesDTO tokenSalida = new TokenPerfilesDTO();

                    tokenSalida.perfiles = perfilesComisaria;
                    tokenSalida.comisarias = comisarias;
                    tokenSalida.userID = usuarioInfo.Item1;
                    tokenSalida.token = GenerateToken(email, email);
                    tokenSalida.reset = usuarioInfo.Item3;


                return tokenSalida;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }


        public Tuple<bool, object> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Authenticacion.SecretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return new Tuple<bool, object>(true, ObtenerDataToken(jwtToken));
            }
            catch
            {
                return new Tuple<bool, object>(false, null);
            }
        }

        public string RefreshToken(string usuario)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Authenticacion.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim("Name", usuario),
                new Claim("User", usuario)

            };

            //Payload
            var payload = new JwtPayload
            (
                _Authenticacion.Issuer,
                _Authenticacion.Audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_Authenticacion.MinutesToken))
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        private DatosUsuarioToken ObtenerDataToken(JwtSecurityToken jwtToken)
        {
            string userName = jwtToken.Claims.First(x => x.Type == "User").Value;
            DateTimeOffset dateTimeOffsetExpiracion = DateTimeOffset.FromUnixTimeSeconds(int.Parse(jwtToken.Claims.First(x => x.Type == "exp").Value)).LocalDateTime;
            DateTimeOffset dateTimeOffsetCreado = DateTimeOffset.FromUnixTimeSeconds(int.Parse(jwtToken.Claims.First(x => x.Type == "nbf").Value)).LocalDateTime;

            var dataToken = new DatosUsuarioToken
            {
                usuario = userName,
                fecha_creacion = dateTimeOffsetCreado.DateTime,
                fecha_expiracion = dateTimeOffsetExpiracion.DateTime,

            };
            return dataToken;
        }

        public async Task<List<PerfilDTO>> ListaPerfiles()
        {
            //stable dev
            try
            {
             return await   perfilUsuarioRepository.ListaPerfiles();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }


        public async Task<bool> ValidarExistenciaCorreo(string email)
        {
            try
            {
                var pass = await usuariorepository.AsignacionClaveTemporal(email);

                 await sendgridNotificaciones.EnviarCambioContrasena(email,pass);

                return  true;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }


        }

        public async Task CambioClave(string email, string pass)
        {
            try
            {
                // valida si tiene solicitud de dsponibilidad de cambio

                // si no tiene lanza exception
                var usuario =await usuariorepository.ValidarDisponiblidadCambio(email);

                // valida si la clave que ingresa esta disponible y guarda el registro , si no lanza exception 
                await usuariorepository.GuardarHistorial(usuario, pass);

                await usuariorepository.CambioClave(usuario,pass);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> verificarDuplicidadClave(int id_usuario_sistema, string pass)
        {
            try
            {
               var Response =await usuariorepository.verificarDuplicidadClave(id_usuario_sistema , pass);

                return Response;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }




    }

    public class DatosUsuarioToken
    {
        #region Propiedadees
        public string usuario { get; set; }
        public DateTime fecha_creacion { get; set; }

        public DateTime fecha_expiracion { get; set; }
        #endregion Propiedades
    }
}
