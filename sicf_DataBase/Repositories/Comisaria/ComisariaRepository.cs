using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Comisaria;
using sicf_Models.Dto.EvaluacionPsicologica;
using sicf_Models.Dto.Token;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;


namespace sicf_DataBase.Repositories.Comisaria
{
    public class ComisariaRepository : IComisariaRepository
    {
        private readonly SICOFAContext context;
        private readonly IConfiguration Configuration;

        public ComisariaRepository(SICOFAContext context, IConfiguration configuration)
        {
            this.context = context;
            this.Configuration = configuration;
        }

        public long IniciarComisaria(CreacionComisariaDTO data)
        {
            try
            {
                var idComisaria = CrearComisaria(data);
                var idUsuario = CrearUsuarioComisario(data.comisario);
                AsignacionComisaria((int)idComisaria, (int)idUsuario, PerfilesConstantes.codComisario);

                return idComisaria;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        #region metodosPublicos

        private long CrearUsuarioComisario(ComisarioDTO data)
        {
            try
            {
                SicofaUsuarioSistema usuario = new SicofaUsuarioSistema();
                usuario.Nombres = data.nombres;
                usuario.Celular = (long)Convert.ToDouble(data.celular);
                usuario.Apellidos = data.apellido;
                usuario.IdTipoDocumento = data.IdDocumento;
                usuario.TelefonoFijo = data.telefonoFijo == String.Empty ? 0 : (long)Convert.ToDouble(data.celular);
                usuario.Cargo = "data";
                var pass = data.nombres + data.apellido + data.numeroDocumento;
                usuario.EncriptPassw = CreateMD5(pass);
                usuario.CorreoElectronico = data.correoElectronico;
                usuario.NumeroDocumento = data.numeroDocumento;
                usuario.Activo = true;
                usuario.cambioPass = true;
                context.SicofaUsuarioSistema.Add(usuario);

                context.SaveChanges();
                EntregarContrasena(usuario.CorreoElectronico, pass);


                return usuario.IdUsuarioSistema;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        private async Task EntregarContrasena(string correo, string passs)
        {
            var email = new MimeMessage();
            try
            {
                email.From.Add(MailboxAddress.Parse(Configuration.GetSection("Email:UserName").Value));
                email.To.Add(MailboxAddress.Parse(correo));
                email.Subject = "Nueva contraseña Sicofa";
                email.Body = new TextPart(TextFormat.Html) {Text = @"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <h2 style='color: #0066cc;'>Nueva contraseña Sicofa</h2>
                            <p>Estimado usuario,</p>
                            <p>Tu nueva contraseña para acceder a Sicofa es: <strong>" + passs + @"</strong></p>
                            <p>Por razones de seguridad, te recomendamos cambiar esta contraseña después de tu próximo inicio de sesión.</p>
                            <p>Si no has solicitado este cambio, por favor contacta con nuestro equipo de soporte inmediatamente.</p>
                            <p>Saludos cordiales,<br>El equipo de Sicofa</p>
                        </body>
                    </html>" 
                };

                var smtp = new MailKit.Net.Smtp.SmtpClient();

                var host = Configuration.GetSection("Email:Host").Value;
                var port = Convert.ToInt32(Configuration.GetSection("Email:Port").Value);
                var user = Configuration.GetSection("Email:UserName").Value;
                var pass = Configuration.GetSection("Email:PassWord").Value;

                smtp.Connect(host, port, SecureSocketOptions.StartTls);

                smtp.Authenticate(user, pass);
                smtp.Send(email);
                smtp.Disconnect(true);



            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private bool AsignacionComisaria(int idComisaria, int idUsuario, string perfil)
        {
            try
            {
                SicofaUsuarioComisaria usuarioComisaria = new SicofaUsuarioComisaria();
                SicofaUsuarioSistemaPerfil sicofaUsuarioSistemaPerfil = new SicofaUsuarioSistemaPerfil();

                usuarioComisaria.IdComisaria = idComisaria;
                usuarioComisaria.IdUsuario = idUsuario;

                context.SicofaUsuarioComisaria.Add(usuarioComisaria);

                sicofaUsuarioSistemaPerfil.IdPerfil = context.SicofaPerfil.Where(p => p.Codigo == perfil).Select(s => s.IdPerfil).Single();
                sicofaUsuarioSistemaPerfil.IdUsuarioSistema = idUsuario;
                //sicofaUsuarioSistemaPerfil.IdComisaria = idComisaria;
                sicofaUsuarioSistemaPerfil.Estado = PerfilesConstantes.EstadoActivo;

                context.SicofaUsuarioSistemaPerfil.Add(sicofaUsuarioSistemaPerfil);

                context.SaveChanges();

                return true;
            }
            catch (Exception ex) {
                // Registrar la excepción y su excepción interna
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw; // Vuelve a lanzar la excepción si es necesario
            }
        }

        public async Task<InformacionComisariaDTO> InformacionComisaria(int idComisaria)
        {
            try 
            {
                var comisaria = await context.SicofaComisaria.Where(s => s.IdComisaria == idComisaria).Select(se =>
                new InformacionComisariaDTO { correo = se.CorreoElectronico, nombreComisaria = se.Nombre, 
                    direccion = se.Direccion, telefono = se.Telefono }).FirstAsync();

                return comisaria;

            }
            catch (Exception)
            {
                throw new Exception(ComisariaMensaje.noComisaria);
            }
        }

        public async Task ActualizarComisaria(InformacionComisariaDTO data, int idComisaria)
        {
            try
            {
                SicofaComisaria comisaria = await context.SicofaComisaria.Where(s => s.IdComisaria == (long)idComisaria).FirstAsync();

                comisaria.Nombre = data.nombreComisaria;
                comisaria.IdCiudadMunicipio = data.idCiudadMunicipio == null ? comisaria.IdCiudadMunicipio : (long)data.idCiudadMunicipio;
                comisaria.Direccion = data.direccion;
                comisaria.CorreoElectronico = data.correo;
                comisaria.Telefono = data.telefono;
                comisaria.Modalidad = data.modalidad;
                comisaria.Naturaleza = data.naturaleza;

                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public long ActualizarComisaria(ComisariaDTO data)
        {
            try
            {
                var comisaria = context.SicofaComisaria.Where(c => c.IdComisaria == data.idComisaria).FirstOrDefault();

                if (comisaria != null)
                {
                    comisaria.CorreoElectronico = data.correo;
                    comisaria.Direccion = data.direccion;
                    comisaria.IdCiudadMunicipio = data.idCiudadMunicipio;
                    comisaria.Nombre = data.nombreComisaria;
                    comisaria.Telefono = data.telefono;

                    context.SaveChanges();

                    return comisaria.IdComisaria;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ComisariaDTO> ConsultarComisaria(RequestComisariaDTO data)
        {
            var responseComisaria = (from comisaria in context.SicofaComisaria
                                     join ciuMun in context.SicofaCiudadMunicipio on comisaria.IdCiudadMunicipio equals ciuMun.IdCiudadMunicipio
                                     join departamento in context.SicofaDepartamento on ciuMun.IdDepartamento equals departamento.IdDepartamento
                                     where (ciuMun.IdDepartamento == data.idDepartamento || data.idDepartamento == 0) &
                                        (ciuMun.IdCiudadMunicipio == data.idCiudadMunicio || data.idCiudadMunicio == 0) &
                                        (comisaria.Nombre.Contains(data.nombreComisaria) || data.nombreComisaria.Equals(String.Empty))
                                     select new ComisariaDTO
                                     {
                                         idComisaria = comisaria.IdComisaria,
                                         idCiudadMunicipio = ciuMun.IdCiudadMunicipio,
                                         idDepartamento = ciuMun.IdDepartamento,
                                         codigoComisaria = comisaria.CodigoComisaria,
                                         nombreComisaria = comisaria.Nombre,
                                         direccion = comisaria.Direccion,
                                         telefono = comisaria.Telefono,
                                         correo = comisaria.CorreoElectronico,
                                         modalidad = comisaria.Modalidad,
                                         naturaleza = comisaria.Naturaleza
                                     }).ToList();

            return responseComisaria;
        }
        public long? ValidarCodigoComisaria(string codigoComisaria)
        {
            return context.SicofaComisaria.Where(c => c.CodigoComisaria == codigoComisaria).Select(c => c.IdComisaria).FirstOrDefault();
        }
        public long? ValidarnombreComisaria(string nombreComisaria)
        {
            return context.SicofaComisaria.Where(c => c.Nombre.Replace(" ", "") == nombreComisaria.Replace(" ", "")).Select(c => c.IdComisaria).FirstOrDefault();
        }
        public long? ValidarCorreoComisario(string correoElectronico)
        {
            return context.SicofaUsuarioSistema.Where(c => c.CorreoElectronico == correoElectronico).Select(c => c.IdUsuarioSistema).FirstOrDefault();
        }
        public long? ValidarIdentificacionComisario(string numeroDocumento)
        {
            return context.SicofaUsuarioSistema.Where(c => c.NumeroDocumento == numeroDocumento).Select(c => c.IdUsuarioSistema).FirstOrDefault();
        }

        public ComisarioDTO? ConsultarComisario(long idComisaria)
        {
            return (from usuario in context.SicofaUsuarioSistema
                    join usuarioperfil in context.SicofaUsuarioSistemaPerfil on usuario.IdUsuarioSistema equals usuarioperfil.IdUsuarioSistema
                    join perfil in context.SicofaPerfil on usuarioperfil.IdPerfil equals perfil.IdPerfil
                    join usuarioComisaria in context.SicofaUsuarioComisaria on usuario.IdUsuarioSistema equals usuarioComisaria.IdUsuario
                    where (perfil.Codigo == Constants.CodigoPefil.Comisario & usuarioComisaria.IdComisaria == idComisaria)
                    select new ComisarioDTO
                    {
                        IdDocumento = usuario.IdTipoDocumento,
                        nombres = usuario.Nombres,
                        apellido = usuario.Apellidos,
                        numeroDocumento = usuario.NumeroDocumento,
                        correoElectronico = usuario.CorreoElectronico,
                        telefonoFijo = usuario.TelefonoFijo.ToString(),
                        celular = usuario.Celular.ToString()
                    }).FirstOrDefault();                                  
        }
        public List<UsuarioComisariaDTO>? ConsutalUsuarioComisaria(long idComisaria)
        {
            return (from usuario in context.SicofaUsuarioSistema
                    join usuarioComisaria in context.SicofaUsuarioComisaria on usuario.IdUsuarioSistema equals usuarioComisaria.IdUsuario
                    join dominio in context.SicofaDominio on usuario.IdTipoDocumento equals dominio.IdDominio
                    where (usuarioComisaria.IdComisaria == idComisaria)
                    select new UsuarioComisariaDTO
                    {
                        IdUsuarioSistema = usuario.IdUsuarioSistema,
                        nombres = usuario.Nombres,
                        apellidos = usuario.Apellidos,
                        numeroDocumento = usuario.NumeroDocumento,
                        telefonoFijo = usuario.TelefonoFijo,
                        celular = usuario.Celular,
                        tipoDocumento = dominio.NombreDominio,
                        correoElectronico = usuario.CorreoElectronico,
                        Activo = usuario.Activo,
                        codigotipoDocumento = dominio.Codigo
                    }).ToList();
        }
        public List<ComisariaUsuario> ConsultaComisariasUsuario(int idUsuario)
        {
            return (from usuarioComisaria in context.SicofaUsuarioComisaria
                    join comisaria in context.SicofaComisaria on usuarioComisaria.IdComisaria equals comisaria.IdComisaria
                    where usuarioComisaria.IdUsuario == idUsuario
                    select new ComisariaUsuario
                    {
                        idComisaria = comisaria.IdComisaria,
                        nombreComisaria = comisaria.Nombre
                    }).ToList();        
        }
        public Task<ControledResponseDTO> CrearMinisterio(CreacionComisariaDTO ministerio)
        {
            try
            {
                ControledResponseDTO response = new ControledResponseDTO();

                int existeADM = (from u in context.SicofaUsuarioSistema
                                 join up in context.SicofaUsuarioSistemaPerfil on u.IdUsuarioSistema equals up.IdUsuarioSistema
                                 join p in context.SicofaPerfil on up.IdPerfil equals p.IdPerfil
                                 where p.Codigo == PerfilesConstantes.codAdministrador
                                 select u).Count();

                if (existeADM == 0)
                {
                    int existeCom = (from c in context.SicofaComisaria
                                     select c).Count();

                    if (existeCom > 0)
                    {
                        response.state = false;
                        response.message = "Ya hay datos de comisarias creadas en este despliegue.";
                    }
                    else
                    {
                        var idComisaria = CrearComisaria(ministerio);
                        var idUsuario = CrearUsuarioComisario(ministerio.comisario);
                        
                        AsignacionComisaria((int)idComisaria, (int)idUsuario, PerfilesConstantes.codAdministrador);

                        response.state = true;
                        response.message = "Se ha creado al ministerio y al usuario administrador de forma correcta.";
                    }
                }
                else
                {
                    response.state = false;
                    response.message = "Ya existe un usuario con perfil Administrador en el sistema.";
                }

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<List<InformacionComisariaDTO>> CargarComisarias(List<MComisariaDTO> comisarias)
        {
            try
            {
                List<InformacionComisariaDTO> comisariasNoCreadas = new List<InformacionComisariaDTO>();

                int cantidadComisarias = context.SicofaComisaria.Count();

                if (cantidadComisarias > 0)
                {
                    throw new ControledException("Ya existen comisarias creadas en el despliegue, por ende, se recomienda validar con el área de TI.");
                }
                else
                {
                    CreacionComisariaDTO nuevaComisaria;
                    InformacionComisariaDTO comisariaExistente;
                    
                    foreach (var item in comisarias)
                    {
                        SicofaComisaria Comisaria = context.SicofaComisaria
                            .Where(c => c.CodigoComisaria == item.codigoComisaria || c.Nombre == item.nombreComisaria).First();

                        if (Comisaria == null)
                        {
                            nuevaComisaria = new CreacionComisariaDTO();

                            nuevaComisaria.idCiudadMunicipio = (from m in context.SicofaCiudadMunicipio where m.Codigo == item.codigoMunicipio select (int)m.IdCiudadMunicipio).First();
                            nuevaComisaria.codigoComisaria = item.codigoComisaria;
                            nuevaComisaria.nombreComisaria = item.nombreComisaria;
                            nuevaComisaria.direccion = item.direccion;
                            nuevaComisaria.telefono = item.telefono;
                            nuevaComisaria.correo = item.correo;
                            nuevaComisaria.modalidad = item.modalidad;
                            nuevaComisaria.naturaleza = item.naturaleza;

                            nuevaComisaria.comisario.IdDocumento = (from td in context.SicofaDominio where td.Codigo == item.codigoDocumento && td.TipoDominio == "Tipo_identificacion" select td.IdDominio).First();
                            nuevaComisaria.comisario.nombres = item.nombres;
                            nuevaComisaria.comisario.apellido = item.apellido;
                            nuevaComisaria.comisario.numeroDocumento = item.numeroDocumento;
                            nuevaComisaria.comisario.correoElectronico = item.correoElectronico;
                            nuevaComisaria.comisario.telefonoFijo = item.telefonoFijo;
                            nuevaComisaria.comisario.celular = item.celular;

                            var idComisaria = CrearComisaria(nuevaComisaria);
                            var idUsuario = CrearUsuarioComisario(nuevaComisaria.comisario);

                            AsignacionComisaria((int)idComisaria, (int)idUsuario, PerfilesConstantes.codComisario);
                        }
                        else
                        {
                            comisariaExistente = new InformacionComisariaDTO();

                            comisariaExistente.idComisaria = (int)Comisaria.IdComisaria;
                            comisariaExistente.idCiudadMunicipio = Comisaria.IdCiudadMunicipio;
                            comisariaExistente.nombreComisaria = Comisaria.Nombre;
                            comisariaExistente.telefono = Comisaria.Telefono;
                            comisariaExistente.direccion = Comisaria.Direccion;
                            comisariaExistente.correo = Comisaria.CorreoElectronico;
                            comisariaExistente.modalidad = Comisaria.Modalidad;
                            comisariaExistente.naturaleza = Comisaria.Naturaleza;

                            comisariasNoCreadas.Add(comisariaExistente);
                        }
                    }
                }

                return Task.FromResult(comisariasNoCreadas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Tuple<string, string> ObtenerNombreComisariayComisario(long id)
        {
            try {

                SicofaSolicitudServicio solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == id).First();
                if (solicitud != null)
                {
                    Tuple<string,string> salida= (from usuarioComisaria in context.SicofaUsuarioComisaria
                            join comisaria in context.SicofaComisaria on usuarioComisaria.IdComisaria equals comisaria.IdComisaria
                            join usuarioSistema in context.SicofaUsuarioSistema on usuarioComisaria.IdUsuario equals usuarioSistema.IdUsuarioSistema
                            join usuarioPerfil in context.SicofaUsuarioSistemaPerfil on usuarioComisaria.IdUsuario equals usuarioPerfil.IdUsuarioSistema
                            join perfil in context.SicofaPerfil on usuarioPerfil.IdPerfil equals perfil.IdPerfil
                            where usuarioComisaria.IdComisaria == solicitud.IdComisaria & usuarioPerfil.Estado == true & perfil.NombrePerfil == PerfilCodigo.Comisario
                            select Tuple.Create(comisaria.Nombre, $"{usuarioSistema.Nombres} {usuarioSistema.Apellidos}")

                       ).First();

                    return salida;
                }
                else {

                    throw new Exception(PerfilCodigo.ComisariaNoidentificada);
                }
            
            }catch(Exception ex){

                throw new Exception(ex.Message);
                    
                    }
        }
        #endregion metodosPublicos

        #region metodosPrivados
        private long CrearComisaria(CreacionComisariaDTO data)
        {
            try
            {
                SicofaComisaria comisaria = new SicofaComisaria();

                comisaria.Nombre = data.nombreComisaria;
                comisaria.Direccion = data.direccion;
                comisaria.Telefono = data.telefono;
                comisaria.CorreoElectronico = data.correo;
                comisaria.IdCiudadMunicipio = data.idCiudadMunicipio;
                comisaria.CodigoComisaria = data.codigoComisaria;
                comisaria.CitaOnline = true;
                comisaria.Modalidad = data.modalidad;
                comisaria.Naturaleza = data.naturaleza;

                context.SicofaComisaria.Add(comisaria);

                context.SaveChanges();

                return comisaria.IdComisaria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
        #endregion metodosPrivados
    }
}
