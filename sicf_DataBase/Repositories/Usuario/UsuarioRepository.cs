using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.EvaluacionPsicologica;
using sicf_Models.Dto.PerfilUsuario;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Dto.Token;
using sicf_Models.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Usuario
{
    public class UsuarioRepository:IUsuarioRepository
    {
        private readonly SICOFAContext _context;
       
        public UsuarioRepository(SICOFAContext context)
        {

            this._context = context;
        }

        public bool GetPermiso(long userID, string codPefil, string codActividad, string componente)
        {
            try
            {
                var respuesta = (from user in _context.SicofaUsuarioSistema
                                 join usperfil in _context.SicofaUsuarioSistemaPerfil on user.IdUsuarioSistema equals usperfil.IdUsuarioSistema
                                 join perf in _context.SicofaPerfil on usperfil.IdPerfil equals perf.IdPerfil
                                 join perfact in _context.SicofaPerfilActividad on perf.IdPerfil equals perfact.IdPerfil
                                 join act in _context.SicofaActividad on perfact.IdActividad equals act.IdActividad
                                 where user.IdUsuarioSistema == userID & perf.Codigo == codPefil & (act.Etiqueta == codActividad & act.Componente == componente)
                                 select user.IdUsuarioSistema
                             ).ToList();

                if (respuesta.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsUserPerfil(long userID, string codPefil) {
            try
            {
                var respuesta = (from user in _context.SicofaUsuarioSistema
                                 join usperfil in _context.SicofaUsuarioSistemaPerfil on user.IdUsuarioSistema equals usperfil.IdUsuarioSistema
                                 join perf in _context.SicofaPerfil on usperfil.IdPerfil equals perf.IdPerfil
                                 where user.IdUsuarioSistema == userID & perf.Codigo == codPefil
                                 select user.IdUsuarioSistema
                               ).ToList();

                if (respuesta.Count > 0)
                    return true;

               return false; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public async Task<bool> VerificarCredenciales(string email, string password) 
        {
            try
            {
                string pass = CreateMD5(password);
                return  await _context.SicofaUsuarioSistema.AnyAsync(s => s.CorreoElectronico.ToLower() == email & s.EncriptPassw == pass & s.Activo == true);

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            
            
            }
        
        }

        public async Task<List<string>> PerfilesUsuario(string email)
        {
            try
            {
                var salida = await (from usu in _context.SicofaUsuarioSistema
                              join usuper in _context.SicofaUsuarioSistemaPerfil on usu.IdUsuarioSistema equals usuper.IdUsuarioSistema
                              join per in _context.SicofaPerfil on usuper.IdPerfil equals per.IdPerfil
                              where usu.CorreoElectronico == email
                              select per.Codigo).ToListAsync();

                return salida;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<List<ComisariaPerfilDTO>> PerfilesUsuarioComisaria(int idUsuario)
        {
            return await (from usuper in _context.SicofaUsuarioSistemaPerfil
                          join per in _context.SicofaPerfil on usuper.IdPerfil equals per.IdPerfil
                          join usuario_comisaria in _context.SicofaUsuarioComisaria on usuper.IdUsuarioSistema equals usuario_comisaria.IdUsuario
                          join comisaria in _context.SicofaComisaria on usuario_comisaria.IdComisaria equals comisaria.IdComisaria
                          where usuper.IdUsuarioSistema == idUsuario
                          select new ComisariaPerfilDTO
                          {
                              idComisaria = comisaria.IdComisaria,
                              perfil = per.Codigo,
                              nombrePerfil = per.NombrePerfil
                          }).ToListAsync();

        }

        public async Task<Tuple<int, int,bool?>> InformacionUsuario(string email)
        {
            return await (from usuario in _context.SicofaUsuarioSistema
                          join comisaria in _context.SicofaUsuarioComisaria on usuario.IdUsuarioSistema equals comisaria.IdUsuario
                          where usuario.CorreoElectronico == email
                          select Tuple.Create(usuario.IdUsuarioSistema, comisaria.IdComisaria, usuario.cambioPass)).FirstOrDefaultAsync()!;
        }


        public async Task<Tuple<int,string,string>> CrearUsuario(CrearUsuarioDTO data)
        {
            try {


              var previo  =  await _context.SicofaUsuarioSistema.Where(s => s.CorreoElectronico == data.correoElectronico).FirstOrDefaultAsync();

                if (previo != null) {

                    throw new Exception(UsuarioMensaje.usuarioRegistrado);
                }


                SicofaUsuarioSistema usuario = new SicofaUsuarioSistema();

                usuario.NumeroDocumento = data.numeroDocumento;
                usuario.IdTipoDocumento = data.tipoDocumento;
                usuario.Nombres = data.nombres;
                usuario.Apellidos = data.apellidos;
                usuario.CorreoElectronico = data.correoElectronico;
                usuario.Cargo = "as";
                usuario.Activo = true;
                usuario.cambioPass = true;

                string claveAsignada = GetUniqueKey();
                usuario.EncriptPassw = CreateMD5(claveAsignada);
              

                bool fijo = Int64.TryParse(data.telefonoFijo, out long number);
                bool celular = Int64.TryParse(data.celular, out long number2);

                usuario.TelefonoFijo = number;
                usuario.Celular = number2;
                usuario.FechaCreacion = DateTime.Now;

                _context.SicofaUsuarioSistema.Add(usuario);

                await _context.SaveChangesAsync();

                var salida = Tuple.Create(usuario.IdUsuarioSistema , usuario.EncriptPassw ,claveAsignada);
                return salida;
                
            }
            catch (Exception ex) 
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarUsuario(UsuarioDTO data)
        {
            try
            {

                var validarEmailUsuaruio = await (from usuE in _context.SicofaUsuarioSistema
                                                  where usuE.CorreoElectronico == data.correoElectronico 
                                                  & usuE.IdUsuarioSistema != data.IdUsuarioSistema                                                
                                                  select usuE.CorreoElectronico).ToListAsync();

                var usuario = await (from usu in _context.SicofaUsuarioSistema
                                     join usucomi in _context.SicofaUsuarioComisaria on usu.IdUsuarioSistema equals usucomi.IdUsuario
                                     where usu.IdUsuarioSistema == data.IdUsuarioSistema && usucomi.IdComisaria == data.idComisaria
                                     select usu).FirstOrDefaultAsync();

                if (validarEmailUsuaruio.Count.Equals(0))
                {

                    if (usuario != null)
                    {
                        usuario.NumeroDocumento = data.numeroDocumento == "" ? usuario.NumeroDocumento : data.numeroDocumento;
                        usuario.IdTipoDocumento = data.tipoDocumento == 0 ? usuario.IdTipoDocumento : data.tipoDocumento;
                        usuario.Nombres = data.nombres == "" ? usuario.Nombres : data.nombres;
                        usuario.Apellidos = data.apellidos == "" ? usuario.Apellidos : data.apellidos;
                        usuario.CorreoElectronico = data.correoElectronico == "" ? usuario.CorreoElectronico : data.correoElectronico;
                        usuario.Cargo = data.Cargo == "" ? usuario.Cargo : data.Cargo;
                        usuario.TelefonoFijo = data.telefonoFijo == 0 ? usuario.TelefonoFijo : data.telefonoFijo;
                        usuario.Celular = data.celular == 0 ? usuario.Celular : data.celular;
                        usuario.Activo = data.Activo;
                        if (data.perfiles.Count > 0)
                        {
                            await ActualizarPerfiles(data.IdUsuarioSistema, data.perfiles, data.idComisaria);
                        }
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        throw new Exception("Usuario no pertenece a esta comisaria!");
                    }
                }else
                {
                    throw new Exception("El correo electonico ya existe");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ActualizarPerfiles(int idUsuario, List<int> perfiles, int idiComisaria)
        {
            try
            {
                if (perfiles.Count <= 0)
                {
                    throw new Exception(UsuarioMensaje.noPerfil);
                }

                var perfilesEliminar = await _context.SicofaUsuarioSistemaPerfil.Where(x => x.IdUsuarioSistema == idUsuario).ToListAsync();

                if (perfilesEliminar != null)
                {
                    _context.SicofaUsuarioSistemaPerfil.RemoveRange(perfilesEliminar);
                    await _context.SaveChangesAsync();
                }

                List <SicofaUsuarioSistemaPerfil> asignacion = new List<SicofaUsuarioSistemaPerfil>();

                foreach (int perfil in perfiles)
                {
                    SicofaUsuarioSistemaPerfil inner = new SicofaUsuarioSistemaPerfil();
                    inner.IdUsuarioSistema = idUsuario;
                    inner.IdPerfil = perfil;
                    //inner.IdComisaria = idiComisaria;
                    asignacion.Add(inner);
                }
                await _context.SicofaUsuarioSistemaPerfil.AddRangeAsync(asignacion);
                await _context.SaveChangesAsync();
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

                var usuario = await (from usu in _context.SicofaUsuarioSistema
                                     join usucomi in _context.SicofaUsuarioComisaria on usu.IdUsuarioSistema equals usucomi.IdUsuario
                                     where usu.IdUsuarioSistema == userID
                                     select new UsuarioDTO
                                     {
                                         IdUsuarioSistema = usu.IdUsuarioSistema,
                                         tipoDocumento = usu.IdTipoDocumento,
                                         numeroDocumento = usu.NumeroDocumento,
                                         nombres = usu.Nombres,
                                         apellidos = usu.Apellidos,
                                         correoElectronico = usu.CorreoElectronico,
                                         telefonoFijo = usu.TelefonoFijo,
                                         celular = usu.Celular,
                                         Cargo = usu.Cargo,
                                         Activo = usu.Activo,
                                         idComisaria = usucomi.IdComisaria
                                     }
                                    ).FirstOrDefaultAsync();




                usuario.perfiles = listaPerfiles(Convert.ToInt32(userID));                

                return usuario;
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

                var usuario = await (from usu in _context.SicofaUsuarioSistema
                                     join usucomi in _context.SicofaUsuarioComisaria on usu.IdUsuarioSistema equals usucomi.IdUsuario
                                     where usu.CorreoElectronico == email
                                     select new UsuarioDTO
                                     {
                                         IdUsuarioSistema = usu.IdUsuarioSistema,
                                         tipoDocumento = usu.IdTipoDocumento,
                                         numeroDocumento = usu.NumeroDocumento,
                                         nombres = usu.Nombres,
                                         apellidos = usu.Apellidos,
                                         correoElectronico = usu.CorreoElectronico,
                                         telefonoFijo = usu.TelefonoFijo,
                                         celular = usu.Celular,
                                         Cargo = usu.Cargo,
                                         Activo = usu.Activo,
                                         idComisaria = usucomi.IdComisaria
                                     }
                                    ).FirstOrDefaultAsync();




                usuario.perfiles = listaPerfiles(Convert.ToInt32(usuario.IdUsuarioSistema));

                return usuario;
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

                FormattableString strQuery = $"EXEC PR_SICOFA_OBTENER_LISTA_USUARIOS @id_comisaria={idComisaria}";

                var resul = await _context.UsuarioDTOs.FromSqlInterpolated(strQuery).ToListAsync();

                var agrupado=resul.Select(s => new UsuarioPerfilesDTO
                {
                    IdUsuarioSistema = s.IdUsuarioSistema,
                    tipoDocumento = s.tipoDocumento,
                    numeroDocumento = s.numeroDocumento,
                    nombres = s.nombres,
                    apellidos = s.apellidos
                ,
                    correoElectronico = s.correoElectronico,
                    telefonoFijo = s.telefonoFijo,
                    celular = s.celular,
                    Cargo = s.Cargo,
                    Activo = s.Activo,
                    idComisaria = s.idComisaria
                }).ToList();


                foreach (var usu in agrupado)
                {
                    usu.perfiles = listaPerfilesPorComisaria(usu.IdUsuarioSistema, idComisaria);
                }

                return agrupado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<int> listaPerfiles(int IdUsuarioSistema)
        {
            try
            {
                return  _context.SicofaUsuarioSistemaPerfil.Where(x => x.IdUsuarioSistema == IdUsuarioSistema).Select(y => y.IdPerfil).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PerfilDTO> listaPerfilesPorComisaria(int IdUsuarioSistema, int idComisaria)
        {
            try
            {
                return (from usuSisPer in _context.SicofaUsuarioSistemaPerfil
                        join perfil in _context.SicofaPerfil on usuSisPer.IdPerfil equals perfil.IdPerfil
                        where usuSisPer.IdUsuarioSistema == IdUsuarioSistema 
                        select new PerfilDTO
                        {
                            idPerfil = perfil.IdPerfil,
                            nombrePerfil = perfil.NombrePerfil
                        }
                        ).ToList();                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task UsuarioAsignacionComisaria(int idUsuario, int Comisiaria) 
        {
            try
            {
                SicofaUsuarioComisaria sicofaUsuarioComisaria = new SicofaUsuarioComisaria();

                sicofaUsuarioComisaria.IdComisaria = Comisiaria;
                sicofaUsuarioComisaria.IdUsuario = idUsuario;

                _context.SicofaUsuarioComisaria.Add(sicofaUsuarioComisaria);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task AsignarPerfiles(int idUsuario, List<int> perfiles, long idComisaria)
        {
            try
            {
                if (perfiles.Count <= 0) {

                    throw new Exception(UsuarioMensaje.noPerfil);
                }
                List<SicofaUsuarioSistemaPerfil> asignacion = new List<SicofaUsuarioSistemaPerfil>();

                foreach (int perfil in perfiles) {

                    SicofaUsuarioSistemaPerfil inner = new SicofaUsuarioSistemaPerfil();

                    inner.IdUsuarioSistema = idUsuario;
                    inner.IdPerfil = perfil;
                    ///inner.IdComisaria = idComisaria;

                    asignacion.Add(inner);
                }

                await _context.SicofaUsuarioSistemaPerfil.AddRangeAsync(asignacion);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task AgregarhistorialContrasena(int usuario , string pass)
        {
            try
            {
                var previa = await _context.SicofaHistorialContrasena.Where(s => s.IdUsuario == usuario).FirstOrDefaultAsync();

                if (previa == null)
                {

                    SicofaHistorialContrasena contra = new SicofaHistorialContrasena();
                    contra.IdUsuario = usuario;
                    contra.EncriptPass = pass;
                    contra.FechaInicio = DateTime.Now;

                    _context.SicofaHistorialContrasena.Add(contra);

                    await _context.SaveChangesAsync();

                }
                else {

                    previa.FechaFin = DateTime.Now;
                    SicofaHistorialContrasena contra = new SicofaHistorialContrasena();
                    contra.IdUsuario = usuario;
                    contra.EncriptPass = pass;
                    contra.FechaInicio = DateTime.Now;

                    _context.SicofaHistorialContrasena.Add(contra);

                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }


        }

        public async Task<int> ComisariaUsuario(string email)
        {
            try
            {
                //
                var response = await (from usu in _context.SicofaUsuarioSistema
                                      join comi in _context.SicofaUsuarioComisaria on usu.IdUsuarioSistema equals comi.IdUsuario
                                      where usu.CorreoElectronico == email

                                      select comi.IdComisaria
                               ).FirstOrDefaultAsync();

                return response;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<string> AsignacionClaveTemporal(string email)
        {
            try
            {

                var usuario =await _context.SicofaUsuarioSistema.Where(s => s.CorreoElectronico == email).FirstOrDefaultAsync();

                if (usuario == null) {
                    throw new Exception(LoginMensaje.noUsuario);
                }

                var guid = Guid.NewGuid().ToString();

                string Temporalpass = GetUniqueKey();

                var passhash=CreateMD5(Temporalpass);

                usuario.EncriptPassw = passhash;

                usuario.cambioPass = true;
             

                await _context.SaveChangesAsync();

                return Temporalpass;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        // valida si tiene la bandera activa para cambio de clave
        public async Task<int> ValidarDisponiblidadCambio(string email) 
        {
            try
            {
                var solicitud = await _context.SicofaUsuarioSistema.Where(s => s.CorreoElectronico == email & s.cambioPass == true).FirstOrDefaultAsync();

                if (solicitud != null)
                {

                    return solicitud.IdUsuarioSistema;

                }
                else {

                    throw new Exception(UsuarioMensaje.noCambioclave);

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message); 

            } 
        }

        public async Task CambioClave(int id_usuario, string nuevopass)
        {
            try
            {
                var solicitud = await _context.SicofaUsuarioSistema.Where(s => s.IdUsuarioSistema == id_usuario).FirstAsync();

                solicitud.cambioPass = false;
                solicitud.EncriptPassw = CreateMD5(nuevopass);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        }



        public async Task GuardarHistorial(int id_usuario_sistema, string pass)
        {
            try
            {
                var validatacion = await verificarDuplicidadClave(id_usuario_sistema, pass);

                if (validatacion)
                {

                    throw new Exception(LoginMensaje.contrasenaUsada);
                }

                var historialBorrado = await _context.SicofaHistorialContrasena.Where(s => s.IdUsuario == id_usuario_sistema).ToListAsync();

                    SicofaHistorialContrasena inner = new SicofaHistorialContrasena();

                if (historialBorrado.Count() == 5)
                {
                    _context.SicofaHistorialContrasena.Remove(historialBorrado.First());
                }

                inner.EncriptPass = CreateMD5(pass);
                inner.IdUsuario = id_usuario_sistema;
                inner.FechaInicio = DateTime.Now;

               _context.SicofaHistorialContrasena.Add(inner);
                await _context.SaveChangesAsync();



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

      

        public async Task<bool> verificarDuplicidadClave(int id_usuario_sistema, string pass)
        {
            try
            {
               var com = await _context.SicofaHistorialContrasena.Where(s => s.IdUsuario == id_usuario_sistema).ToListAsync();
                var dom =com.Any( s => s.EncriptPass == CreateMD5(pass));

                return dom;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        private  string CreateMD5(string input)
        {
           // stable dev
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); 
            }
        }

        public static string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }


    }
}
