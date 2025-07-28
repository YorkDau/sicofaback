using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sicf_DataBase.BDConnection;
using sicf_DataBase.Data;
using sicf_DataBase.Repositories;
using sicf_DataBase.Repositories.SolicitudesRepository;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicfExceptions.Exceptions;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Compartido
{
    public class CompartidoRepository: BdConnection, ICompartidoRepository
    {
        private readonly SICOFAContext context;

        public CompartidoRepository(SICOFAContext context, IConfiguration configuration) : base(configuration)
        {
            this.context = context;
            
        }
       
        // Metodo eliminado por cambio a otro servicio  public List<TipoDocumentoDto> ObtenerTipoDocuemntos()

        public List<PaisDto> ObtenerPais()
        {

            try
            {
                List<PaisDto> paisList = new List<PaisDto>();
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                    {
                        String query = "PR_SICOFA_OBTENER_PAISES";

                        using (_command = new SqlCommand(query, _connectionDb))
                        {
                            _command.CommandType = CommandType.StoredProcedure;
                            _connectionDb.Open();

                            using (SqlDataReader reader = _command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    PaisDto paisDto = new PaisDto();
                                    paisDto.paisID = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_pais"]);
                                    paisDto.nombrePais = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_pais"]);

                                    paisList.Add(paisDto);
                                }
                            }
                        }

                        _connectionDb.Close();
                    }
                }

                return paisList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        /// <summary>
        /// Rafael Marquez Consultar pais por tipo Id para mostrar u ocultar Colombia en caso que no corresponda el tipo de identificación con tipoId distinto a CC
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public List<PaisDto> ObtenerPais(int id_tipo_documento)
        {

            try
            {
                List<PaisDto> paisList = new List<PaisDto>();
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                    {
                        String query = "PR_SICOFA_OBTENER_PAISES";

                        using (_command = new SqlCommand(query, _connectionDb))
                        {
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.Parameters.AddWithValue("@id_tipo_documento", BdValidation.ToDBNull(id_tipo_documento));
                            _connectionDb.Open();

                            using (SqlDataReader reader = _command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    PaisDto paisDto = new PaisDto();
                                    paisDto.paisID = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_pais"]);
                                    paisDto.nombrePais = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_pais"]);

                                    paisList.Add(paisDto);
                                }
                            }
                        }

                        _connectionDb.Close();
                    }
                }

                return paisList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<DepartamentoDto> ObtenerDepartamento(int idPais)
        {

            try
            {
                List<DepartamentoDto> departamentoList = new List<DepartamentoDto>();
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                    {
                        String query = "PR_SICOFA_OBTENER_DEPARTAMENTO_POR_PAIS";
                        using (_command = new SqlCommand(query, _connectionDb))
                        {
                            
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.Parameters.AddWithValue("@idPais", BdValidation.ToDBNull(idPais));
                            _connectionDb.Open();

                            using (SqlDataReader reader = _command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    DepartamentoDto departamentoDto = new DepartamentoDto();
                                    departamentoDto.departamentoID = ConvertFDBVal.ConvertFromDBVal<long>(reader["id_departamento"]);
                                    departamentoDto.departamentoNombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre"]);

                                    departamentoList.Add(departamentoDto);
                                }
                            }
                        }
                        _connectionDb.Close();
                    }
                }

                return departamentoList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<CiudadMunicipioDto> ObtenerCiudadesMunicipios(long idDep)
        {
            try
            {
                List<CiudadMunicipioDto> ciudmunicipioList = new List<CiudadMunicipioDto>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_CIUDAD_MUNICIPIO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@depID", BdValidation.ToDBNull(idDep));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using (SqlDataReader reader = _command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CiudadMunicipioDto ciudadMunicipio = new CiudadMunicipioDto();

                                ciudadMunicipio.idDep = ConvertFDBVal.ConvertFromDBVal<long>(reader["Id Dep"]);
                                ciudadMunicipio.ciudmunID = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID CiudadMunicipio"]);
                                ciudadMunicipio.nombCiudMun = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre CiudadM"]);
                                ciudadMunicipio.codigo = ConvertFDBVal.ConvertFromDBVal<string>(reader["codigo"]);

                                ciudmunicipioList.Add(ciudadMunicipio);
                            }
                        }

                        _connectionDb.Close();

                    }
                }

                return ciudmunicipioList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<LocalidadDto> ObtenerLocalidades(long ciudMunID)
        {
            try
            {
                List<LocalidadDto> localidadList = new List<LocalidadDto>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_LOCALIDADES";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@ciudMunID", BdValidation.ToDBNull(ciudMunID));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using (SqlDataReader reader = _command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LocalidadDto localidadDto = new LocalidadDto();

                                localidadDto.localidadID = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_localidad"]);
                                localidadDto.localidadNombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre_localidad"]);
                               

                                localidadList.Add(localidadDto);
                            }
                        }

                        _connectionDb.Close();

                    }
                }

                return localidadList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public List<SexoGeneroOrientacionSexual> ObtenerSexoGeneroOrientacionSexual(string tipo)
        {
            try
            {
                List<SexoGeneroOrientacionSexual> sexoList = new List<SexoGeneroOrientacionSexual>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_SEXOS_GENEROS_ORIENTACIONSEXUALES";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@tipo", BdValidation.ToDBNull(tipo));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using (SqlDataReader reader = _command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SexoGeneroOrientacionSexual sexogeneroorientSexualDto = new SexoGeneroOrientacionSexual();

                                sexogeneroorientSexualDto.id = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_sex_gen_orient"]);
                                sexogeneroorientSexualDto.nombre = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre"]);
                                sexogeneroorientSexualDto.tipo = ConvertFDBVal.ConvertFromDBVal<string>(reader["tipo"]);


                                sexoList.Add(sexogeneroorientSexualDto);
                            }
                        }

                        _connectionDb.Close();

                    }
                }

                return sexoList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }


        /// <summary>
        /// Obtiene tabla de dominio
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public List<DominioDto> ObtenerDominio(string Tipo_Dominio)
        {
            try
            {
                List<DominioDto> DominioList = (from dom in context.SicofaDominio
                                                where dom.TipoDominio == Tipo_Dominio && dom.Activo == true
                                                select new DominioDto
                                                { 
                                                    id_Dominio = dom.IdDominio,
                                                    Tipo_Dominio = dom.TipoDominio,
                                                    codigo = dom.Codigo,
                                                    Nombre_Dominio = dom.NombreDominio,
                                                    Tipo_Lista = dom.TipoLista
                                                }).ToList();
                
                if(DominioList.Count > 0)
                    return DominioList;
                else
                    throw new ControledException("El dominio " + Tipo_Dominio + " no devolvió ningún valor.");
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        /// <summary>
        /// Obtiene tabla de dominio
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public List<EstadoSolicitudDto> ObtenerEstadoSolicitud()
        {
            try
            {
                List<EstadoSolicitudDto> EstadoSolicitudList = new List<EstadoSolicitudDto>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_ESTADOSOLICITUD";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using (SqlDataReader reader = _command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EstadoSolicitudDto EstadoSolicitud = new EstadoSolicitudDto();

                                EstadoSolicitud.id_estado_solicitud = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_estado_solicitud"]);
                                EstadoSolicitud.estado_solicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["estado_solicitud"]);

                                EstadoSolicitudList.Add(EstadoSolicitud);
                            }
                        }

                        _connectionDb.Close();
                    }
                }

                return EstadoSolicitudList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        // provisional Flujo tarea
        public long ProvisionalTarea(long idtarea) {

            SicofaTarea previa=context.SicofaTarea.Where(s => s.IdTarea == idtarea).First();

            previa.Estado = "TERMINADO";
            previa.FechaTerminacion = DateTime.Now;

            SicofaTarea newTarea = new SicofaTarea();

            newTarea.IdTareaAnt = previa.IdTarea;
            newTarea.Estado = "EJECUCION";
            newTarea.IdFlujo = 2;
            newTarea.FechaCreacion = DateTime.Now;
            newTarea.IdUsuarioSistema = previa.IdUsuarioSistema;

            context.SicofaTarea.Add(newTarea);

            context.SaveChanges();

            return newTarea.IdTarea;
        
        }

        public async Task<List<InvolucradoInfoListaDTO>> ListarInvolucradosComplementariaInfo(long idSolicitudServicio)
        {
            try
            {
                FormattableString strQuery = $"EXEC PR_SICOFA_INVOLUCRADO_ADICIONALES @pi_SolicitudServicio={idSolicitudServicio}";
                return  await context.SicofaInvolucradosAdicionales.FromSqlInterpolated(strQuery).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<bool> GuardarInvolucrado(InvolucradoDTO involucrado)
        {

            try
            {
                
                var solicitud = context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(se => se.IdSolicitudServicio == involucrado.IdSolicitudServicio).FirstOrDefault();
                var involucrados = solicitud.IdInvolucrado;
                if (solicitud.IdInvolucrado.Count(s => s.TipoDocumento == involucrado.TipoDocumento &&
                                                       s.NumeroDocumento == involucrado.NumeroDocumento) > 0)
                {
                    throw new Exception("Ya existe un involucrado con el mismo Tipo de identificación y número de identificación asociado a esta solicitud de servicio.");
                }

                SicofaInvolucrado invo = new SicofaInvolucrado();
                invo.NumeroDocumento = involucrado.NumeroDocumento;
                invo.TipoDocumento = Convert.ToInt16(involucrado.TipoDocumento);
                invo.Nombres = $"{involucrado.PrimerNombre} {involucrado.SegundoNombre}";
                invo.PrimerNombre = involucrado.PrimerNombre;
                invo.SegundoNombre = involucrado.SegundoNombre;
                invo.Apellidos = $"{involucrado.PrimerApellido} {involucrado.SegundoApellido}"; 
                invo.PrimerApellido = involucrado.PrimerApellido;
                invo.SegundoApellido = involucrado.SegundoApellido;
                invo.Edad = involucrado.Edad;
                invo.Telefono = involucrado.Telefono;
                invo.CorreoElectronico = involucrado.CorreoElectronico;
                invo.EsRepresentante = involucrado.EsRepresentante ?? false;

                if (involucrado.EsVictima == true)
                {
                    invo.EsVictima = involucrado.EsVictima.Value;
                    invo.EsPrincipal = false;
                }
                else {

                    if (involucrados.Any(s => s.EsPrincipal == true & s.EsVictima == false))
                    {

                        invo.EsPrincipal = false;
                        invo.EsVictima = false;
                    }
                    else {
                        invo.EsPrincipal = true;
                        invo.EsVictima = false;

                    }
                
                }



                // invo.EsPrincipal = involucrado.EsPrincipal.Value;
                invo.Eps = involucrado.Eps;

                //invo.IdLugarExpedicion = involucrado.IdLugarExpedicion == 0 ? invo.IdLugarExpedicion : involucrado.IdLugarExpedicion;
                invo.IdPaisExpedicion = involucrado.PaisExp == 0 ? invo.IdPaisExpedicion : involucrado.PaisExp;
                invo.IdDepartamentoExpedicion = involucrado.DepartamentoExp == 0 ? invo.IdDepartamentoExpedicion : involucrado.DepartamentoExp;
                invo.IdMunicipioExpedicion = involucrado.MunicipioExp == 0 ? invo.IdMunicipioExpedicion : involucrado.MunicipioExp;

                context.SicofaInvolucrado.Add(invo);
                await context.SaveChangesAsync();

                if (invo.IdInvolucrado == 0)
                {
                    throw new ControledException("No se guardo el involucrado");
                }

                involucrado.InfoAdicional.IdInvolucrado = invo.IdInvolucrado;
                await GuardarInvolucradoComplementaria(involucrado.InfoAdicional);
                await GuardarInvolucradoSolicitud(involucrado.IdSolicitudServicio, invo.IdInvolucrado);

                return true;
            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GuardarInvolucradoComplementaria(InvolucradoComplementariaDTO involucrado)
        {

            try
            {
                SicofaInvolucradoComplementaria deta = new SicofaInvolucradoComplementaria();
                deta.IdInvolucrado = involucrado.IdInvolucrado;
                deta.RegistroExpedidoEn = involucrado.RegistroExpedidoEn;
                deta.NombreEntidadExpedicion = involucrado.NombreEntidadExpedicion;
                deta.DatosAdicionales = involucrado.DatosAdicionales;
                deta.NombreResponsableCustodia = involucrado.NombreResponsableCustodia;
                deta.ParentescoResponsableCustodia = involucrado.ParentescoResponsableCustodia;
                deta.NombreResponsableCuidado = involucrado.NombreResponsableCuidado;
                deta.ParentescoResponsableCuidado = involucrado.ParentescoResponsableCuidado;
                deta.VinculacionSistemaSalud = involucrado.VinculacionSistemaSalud;
                deta.Regimen = involucrado.Regimen;
                deta.BeneficiarioDeNombre = involucrado.BeneficiarioDeNombre;
                deta.FisicaAdecuada = involucrado.FisicaAdecuada;
                deta.NutricionalAdecuada = involucrado.NutricionalAdecuada;
                deta.PsicologicaAdecuada = involucrado.PsicologaAdecuada;
                deta.VacunacionCompleta = involucrado.VacunacionCompleta;
                deta.MatriculadoEnElColegio = involucrado.MatriculadoEnElColegio;
                deta.GradoCursa = involucrado.GradoCursa;
                deta.JornadaEstudio = involucrado.JornadaEstudio;
                deta.TipoVivienda = involucrado.TipoVivienda;
                deta.OtroTipoVivienda = involucrado.OtroTipoVivienda;
                deta.OtrotipoViviendaCual = involucrado.OtroTipoViviendaCual;
                deta.NumeroHabitacionesVivienda = involucrado.NumeroHabitacionesVivienda == string.Empty ? 0 : Convert.ToInt16(involucrado.NumeroHabitacionesVivienda);
                deta.DistribucionHabitaciones = involucrado.DistribuciuonHabitaciones;
                deta.ViviendaConBaños = involucrado.ViviendaConBaños;
                deta.ViviendaconCocina = involucrado.ViviendaconCocina;
                deta.ViviendaConLuz = involucrado.ViviendaConLuz;
                deta.ViviendaConAgua = involucrado.ViviendaConAgua;
                deta.ViciendaConGas = involucrado.ViciendaConGas;
                deta.OtrosServicios = involucrado.OtrosServicios;
                deta.Estratificacion = involucrado.Estratificacion;
                deta.AsisteExtracurriculares = involucrado.AsisteExtracurriculares;
                deta.ActividadesExtracurriculares = involucrado.ActividadesExtracurriculares;
                deta.FamiliaExtensa = involucrado.FamiliaExtensa;
                deta.OtraInformacionFamiliaExtensa = involucrado.OtraInformacionFamiliaExtensa;
                deta.NoInformacion = involucrado.NoInformacion;

                deta.ObservacionesSalud = involucrado.ObservacionesSalud;
                deta.ObservacionesPsicologia = involucrado.ObservacionesPsicologia;
                deta.ObservacionesTrabajoSocial = involucrado.ObservacionesTrabajoSocial;

                context.SicofaInvolucradoComplementaria.Add(deta);
                await context.SaveChangesAsync();
                return true;
            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       

        public async Task<bool> GuardarInvolucradoSolicitud(long idSolicitudServicio, long idInvolucrado)
        {

            try
            {
                string? mensaje = "";
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_CREAR_SERVICIO_INVOLUCRADO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@id_solicitud_servicio", idSolicitudServicio);
                        _command.Parameters.AddWithValue("@id_involucrado", idInvolucrado);

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        _command.ExecuteNonQuery();
                        _connectionDb.Close();

                    }

                }
                return true;
            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarInvolucradoComplementaria(InvolucradoDTO involucrado)
        {

            try
            {
                SicofaInvolucrado invo = await context.SicofaInvolucrado.Where(x => x.IdInvolucrado == involucrado.IdInvolucrado).FirstOrDefaultAsync();


                // ;
                invo.NumeroDocumento = involucrado.NumeroDocumento == "" ? invo.NumeroDocumento : involucrado.NumeroDocumento;
                invo.TipoDocumento = involucrado.TipoDocumento == 0 ? invo.TipoDocumento : Convert.ToInt16(involucrado.TipoDocumento);
                invo.Nombres = $"{involucrado.PrimerNombre} {involucrado.SegundoNombre}";
                invo.PrimerNombre = involucrado.PrimerNombre;
                invo.SegundoNombre = involucrado.SegundoNombre;
                if ($"{involucrado.PrimerNombre} {involucrado.SegundoNombre}" != "")
                {
                    invo.Nombres = $"{involucrado.PrimerNombre} {involucrado.SegundoNombre}";
                }
                else 
                {
                    invo.Nombres = invo.Nombres;
                }
                invo.PrimerApellido = involucrado.PrimerApellido;
                invo.SegundoApellido = involucrado.SegundoApellido;
                if ($"{involucrado.PrimerApellido} {involucrado.SegundoApellido}" != "")
                {
                    invo.Apellidos = $"{involucrado.PrimerApellido} {involucrado.SegundoApellido}";
                }
                else
                {
                    invo.Apellidos = invo.Apellidos;
                }
                invo.Edad = involucrado.Edad == 0 ? invo.Edad : involucrado.Edad;
                invo.Telefono = involucrado.Telefono == "" ? invo.Telefono : involucrado.Telefono;
                invo.CorreoElectronico = involucrado.CorreoElectronico == "" ? invo.CorreoElectronico : involucrado.CorreoElectronico;
                invo.EsVictima = involucrado.EsVictima is null ? invo.EsVictima : involucrado.EsVictima.Value;
                invo.EsPrincipal = involucrado.EsPrincipal == null ? invo.EsPrincipal : involucrado.EsPrincipal;
                invo.EsRepresentante = involucrado.EsRepresentante == null ? invo.EsRepresentante : involucrado.EsRepresentante;
                invo.Eps = involucrado.Eps == "" ? invo.Eps : involucrado.Eps;

                //invo.IdLugarExpedicion = involucrado.IdLugarExpedicion == 0 ? invo.IdLugarExpedicion : involucrado.IdLugarExpedicion;
                invo.IdPaisExpedicion = involucrado.PaisExp == 0 ? invo.IdPaisExpedicion : involucrado.PaisExp;
                invo.IdDepartamentoExpedicion = involucrado.DepartamentoExp == 0 ? invo.IdDepartamentoExpedicion : involucrado.DepartamentoExp;
                invo.IdMunicipioExpedicion = involucrado.MunicipioExp == 0 ? invo.IdMunicipioExpedicion : involucrado.MunicipioExp;

                await context.SaveChangesAsync();

                await ActualizarInvolucradoComplementaria(involucrado.InfoAdicional);

                return true;
            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarInvolucradoComplementaria(InvolucradoComplementariaDTO involucrado)
        {

            try
            {
                SicofaInvolucradoComplementaria deta = await context.SicofaInvolucradoComplementaria.Where(x => x.IdInvolucrado == involucrado.IdInvolucrado).FirstOrDefaultAsync();

                if (deta == null)
                {
                    deta = new SicofaInvolucradoComplementaria();
                    deta.IdInvolucrado = involucrado.IdInvolucrado;
                    deta.RegistroExpedidoEn = involucrado.RegistroExpedidoEn;
                    deta.NombreEntidadExpedicion = involucrado.NombreEntidadExpedicion;
                    deta.DatosAdicionales = involucrado.DatosAdicionales;
                    deta.NombreResponsableCustodia = involucrado.NombreResponsableCustodia;
                    deta.ParentescoResponsableCustodia = involucrado.ParentescoResponsableCustodia;
                    deta.NombreResponsableCuidado = involucrado.NombreResponsableCuidado;
                    deta.ParentescoResponsableCuidado = involucrado.ParentescoResponsableCuidado;
                    deta.VinculacionSistemaSalud = involucrado.VinculacionSistemaSalud;
                    deta.Regimen = involucrado.Regimen;
                    deta.BeneficiarioDeNombre = involucrado.BeneficiarioDeNombre;
                    deta.FisicaAdecuada = involucrado.FisicaAdecuada;
                    deta.NutricionalAdecuada = involucrado.NutricionalAdecuada;
                    deta.PsicologicaAdecuada = involucrado.PsicologaAdecuada;
                    deta.VacunacionCompleta = involucrado.VacunacionCompleta;

                    bool escolarizado = involucrado.Escolarizado ?? false;
                    deta.Escolarizado = escolarizado;
                    deta.MatriculadoEnElColegio = escolarizado ? involucrado.MatriculadoEnElColegio : "N/A";
                    deta.GradoCursa = escolarizado ? involucrado.GradoCursa : "N/A";
                    deta.JornadaEstudio = escolarizado ? involucrado.JornadaEstudio : "N/A";

                    deta.TipoVivienda = involucrado.TipoVivienda;
                    deta.OtroTipoVivienda = involucrado.OtroTipoVivienda;
                    deta.NumeroHabitacionesVivienda = involucrado.NumeroHabitacionesVivienda == string.Empty ? 0 : Convert.ToInt16(involucrado.NumeroHabitacionesVivienda);
                    deta.DistribucionHabitaciones = involucrado.DistribuciuonHabitaciones;
                    deta.ViviendaConBaños = involucrado.ViviendaConBaños;
                    deta.ViviendaconCocina = involucrado.ViviendaconCocina;
                    deta.ViviendaConLuz = involucrado.ViviendaConLuz;
                    deta.ViviendaConAgua = involucrado.ViviendaConAgua;
                    deta.ViciendaConGas = involucrado.ViciendaConGas;
                    deta.OtrosServicios = involucrado.OtrosServicios;
                    deta.Estratificacion = involucrado.Estratificacion;
                    deta.AsisteExtracurriculares = involucrado.AsisteExtracurriculares;
                    deta.ActividadesExtracurriculares = involucrado.ActividadesExtracurriculares;
                    deta.FamiliaExtensa = involucrado.FamiliaExtensa;
                    deta.OtraInformacionFamiliaExtensa = involucrado.OtraInformacionFamiliaExtensa;
                    deta.OtrotipoViviendaCual = involucrado.OtroTipoViviendaCual;
                    deta.NoInformacion = involucrado.NoInformacion;
                    deta.ObservacionesSalud = involucrado.ObservacionesSalud;
                    deta.ObservacionesPsicologia = involucrado.ObservacionesPsicologia;
                    deta.ObservacionesTrabajoSocial = involucrado.ObservacionesTrabajoSocial;

                    context.SicofaInvolucradoComplementaria.Add(deta);

                    await context.SaveChangesAsync();
                    return true;

                }
                else {


                  
                    deta.RegistroExpedidoEn = involucrado.RegistroExpedidoEn;
                    deta.NombreEntidadExpedicion = involucrado.NombreEntidadExpedicion;
                    deta.DatosAdicionales = involucrado.DatosAdicionales;
                    deta.NombreResponsableCustodia = involucrado.NombreResponsableCustodia;
                    deta.ParentescoResponsableCustodia = involucrado.ParentescoResponsableCustodia;
                    deta.NombreResponsableCuidado = involucrado.NombreResponsableCuidado;
                    deta.ParentescoResponsableCuidado = involucrado.ParentescoResponsableCuidado;
                    deta.VinculacionSistemaSalud = involucrado.VinculacionSistemaSalud;
                    deta.Regimen = involucrado.Regimen;
                    deta.BeneficiarioDeNombre = involucrado.BeneficiarioDeNombre;
                    deta.FisicaAdecuada = involucrado.FisicaAdecuada;
                    deta.NutricionalAdecuada = involucrado.NutricionalAdecuada;
                    deta.PsicologicaAdecuada = involucrado.PsicologaAdecuada;
                    deta.VacunacionCompleta = involucrado.VacunacionCompleta;

                    bool escolarizado = involucrado.Escolarizado ?? false;
                    deta.Escolarizado = escolarizado;
                    deta.MatriculadoEnElColegio = escolarizado ? involucrado.MatriculadoEnElColegio : "N/A";
                    deta.GradoCursa = escolarizado ? involucrado.GradoCursa : "N/A";
                    deta.JornadaEstudio = escolarizado ? involucrado.JornadaEstudio : "N/A";

                    deta.TipoVivienda = involucrado.TipoVivienda;
                    deta.OtroTipoVivienda = involucrado.OtroTipoVivienda;
                    deta.NumeroHabitacionesVivienda = involucrado.NumeroHabitacionesVivienda == string.Empty ? 0 : Convert.ToInt16(involucrado.NumeroHabitacionesVivienda);
                    deta.DistribucionHabitaciones = involucrado.DistribuciuonHabitaciones;
                    deta.ViviendaConBaños = involucrado.ViviendaConBaños;
                    deta.ViviendaconCocina = involucrado.ViviendaconCocina;
                    deta.ViviendaConLuz = involucrado.ViviendaConLuz;
                    deta.ViviendaConAgua = involucrado.ViviendaConAgua;
                    deta.ViciendaConGas = involucrado.ViciendaConGas;
                    deta.OtrosServicios = involucrado.OtrosServicios;
                    deta.Estratificacion = involucrado.Estratificacion;
                    deta.AsisteExtracurriculares = involucrado.AsisteExtracurriculares;
                    deta.ActividadesExtracurriculares = involucrado.ActividadesExtracurriculares;
                    deta.FamiliaExtensa = involucrado.FamiliaExtensa;
                    deta.OtraInformacionFamiliaExtensa = involucrado.OtraInformacionFamiliaExtensa;
                    deta.OtrotipoViviendaCual = involucrado.OtroTipoViviendaCual;

                    deta.ObservacionesSalud = involucrado.ObservacionesSalud;
                    deta.ObservacionesPsicologia = involucrado.ObservacionesPsicologia;
                    deta.ObservacionesTrabajoSocial = involucrado.ObservacionesTrabajoSocial;


                    await context.SaveChangesAsync();
                    return true;

                }
            }

            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<FuncionarioDTO> ObtenerDatosFuncionarioPorTarea(long idtarea)
        {
            try
            {
               // var evaluacion = context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idtarea & s.).FirstOrDefault();

                var evaluacion = await (from eva in context.SicofaEvaluacionPsicologica
                                   join tar in context.SicofaTarea on eva.IdTarea equals tar.IdTarea
                                   where eva.IdSolicitudServicio == idtarea & tar.Estado == TareaEstados
                                        .EJECUCION select eva).FirstOrDefaultAsync();

               if (evaluacion == null) 
                {
                    throw new Exception(EvaluacionPsicologicaEmocional.evaluacionPsicologicaError);
                }

                FuncionarioDTO salida = (from tarea in context.SicofaTarea
                                         join usuario in context.SicofaUsuarioSistema on tarea.IdUsuarioSistema equals usuario.IdUsuarioSistema
                                         join perfil in context.SicofaPerfil on tarea.IdPerfil equals perfil.IdPerfil
                                         where tarea.IdTarea == evaluacion.IdTarea
                                         select new FuncionarioDTO { nombre = usuario.Nombres,
                                             apellido = usuario.Apellidos,
                                             perfil = perfil.NombrePerfil,
                                             email = usuario.CorreoElectronico
                                         }
                                          ).First();

                                    
                var datosComisaria = await (
                                        from usuario in context.SicofaUsuarioSistema
                                        join comisaria in context.SicofaUsuarioComisaria on usuario.IdUsuarioSistema equals comisaria.IdUsuario
                                        join comi in context.SicofaComisaria on comisaria.IdComisaria equals comi.IdComisaria
                                        where usuario.CorreoElectronico == salida.email
                                        select Tuple.Create(
                                            comi.Nombre,
                                            comi.Direccion,
                                            usuario.NumeroTarjetaProfesional
                                        )
                                    ).FirstAsync();

                                    salida.direccionComisaria = datosComisaria.Item2;
                                    salida.nombreComisaria = datosComisaria.Item1;
                                    salida.numeroTarjetaProfesional = string.IsNullOrWhiteSpace(datosComisaria.Item3)
                                        ? "No tiene Tarjeta profesional"
                                        : datosComisaria.Item3;



                return salida;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);

            }
        } 
    }
}
