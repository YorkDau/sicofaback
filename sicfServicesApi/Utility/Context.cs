using System.IdentityModel.Tokens.Jwt;

namespace sicfServicesApi.Utility
{
    public static class Context
    {
        public static DatosUsuarioToken GetToken(this HttpContext context)
        {
            try
            {


                var token = context.Request.Headers["Authorization"].ToString().Split("Bearer ");

                string token1 = token[1].ToString();

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(token1);

                var response = ObtenerDataToken(jwtSecurityToken);
                return response;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public static string GetTokenRaw(this HttpContext context)
        {

            var token = context.Request.Headers["Authorization"].ToString().Split("Bearer ");

            string token1 = token[1].ToString();

           
            return token1;
        }

        public static DatosUsuarioToken ObtenerDataToken(JwtSecurityToken jwtToken)
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
