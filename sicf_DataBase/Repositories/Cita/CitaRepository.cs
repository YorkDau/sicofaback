using sicf_Models.Dto.Cita;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using sicf_DataBase.BDConnection;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using sicf_DataBase.Data;
using sicf_Models.Core;
using Microsoft.EntityFrameworkCore;
using static sicf_Models.Constants.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using sicf_Models.Dto.Ciudadano;

namespace sicf_DataBase.Repositories.Cita
{
    public class CitaRepository : BdConnection, ICitaRepository
    {

        private readonly SICOFAContext _context;

        public List<ResponseComisariaDto> listaComisariaDto;

        //public  List<ResponseDisponibiliadCitasDto> ListaDisponibiliadCitasDto;

        private IConfiguration? configuration { get; set; }

        public CitaRepository(SICOFAContext context, IConfiguration configuration) : base(configuration)
        {
            _context = context;
            try
            {


                listaComisariaDto = new List<ResponseComisariaDto>();

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
        public ResponseListaPaginada ObtenerDepertamentos()
        {
            try
            {
                List<ResponseDepartamentoDto> listaDepartamentos = new List<ResponseDepartamentoDto>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_DEPARTAMENTOS";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Creando  la conexión  a la BD ( _connectionDb.Open() )", listarVehiculosDtoParam);

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {

                                ResponseDepartamentoDto responseDepartamentoDto = new ResponseDepartamentoDto();

                                responseDepartamentoDto.depID = ConvertFDBVal.ConvertFromDBVal<long>(reader["Id Dep"]);
                                responseDepartamentoDto.nombDep = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre Departamento"]);


                                listaDepartamentos.Add(responseDepartamentoDto);

                            }

                        _connectionDb.Close();

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Cerrando  la conexión  a la BD ( _connectionDb.Close() )", listarVehiculosDtoParam);
                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "La ejecución del servicio de  consultarVehiculos fue realizada con éxito.", listarVehiculosDtoParam);

                    }
                }

                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

                responseListaPaginada.DatosPaginados = listaDepartamentos;
                responseListaPaginada.TotalRegistros = listaDepartamentos.Count;

                return responseListaPaginada;
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

        public ResponseListaPaginada ObtenerCiudadesMunicipios(long depID)
        {
            try
            {
                List<ResponseCiudadeMunicipioDto> listaCiudadeMunicipioDto = new List<ResponseCiudadeMunicipioDto>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_CIUDAD_MUNICIPIO";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@depID", BdValidation.ToDBNull(depID));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Creando  la conexión  a la BD ( _connectionDb.Open() )", listarVehiculosDtoParam);

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {

                                ResponseCiudadeMunicipioDto responseCiudadeMunicipioDto = new ResponseCiudadeMunicipioDto();

                                responseCiudadeMunicipioDto.idDep = ConvertFDBVal.ConvertFromDBVal<long>(reader["Id Dep"]);
                                responseCiudadeMunicipioDto.ciudmunID = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID CiudadMunicipio"]);
                                responseCiudadeMunicipioDto.nombCiudMun = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre CiudadM"]);


                                listaCiudadeMunicipioDto.Add(responseCiudadeMunicipioDto);

                            }

                        _connectionDb.Close();

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Cerrando  la conexión  a la BD ( _connectionDb.Close() )", listarVehiculosDtoParam);
                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "La ejecución del servicio de  consultarVehiculos fue realizada con éxito.", listarVehiculosDtoParam);

                    }
                }

                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

                responseListaPaginada.DatosPaginados = listaCiudadeMunicipioDto;
                responseListaPaginada.TotalRegistros = listaCiudadeMunicipioDto.Count;

                return responseListaPaginada;
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


        public ResponseListaPaginada ObtenerComisarias(int ciudmunID)
        {
            try
            {
                string mensaje = "";
                Tuple<string, List<ResponseComisariaDto>> tupleresponse;
                List<ResponseComisariaDto> responseComisariaDtosList = new List<ResponseComisariaDto>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_COMISARIAS";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@ciudMunicipioID", BdValidation.ToDBNull(ciudmunID));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                if (reader.FieldCount > 1)
                                {
                                    ResponseComisariaDto responseComisariaDto = new ResponseComisariaDto();

                                    responseComisariaDto.ciudadMunicipio = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre CiudadM"]);
                                    responseComisariaDto.comisariaID = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Comisaria"]);
                                    responseComisariaDto.nombComisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre Comisaria"]);
                                    responseComisariaDto.horarioSemanal = ConvertFDBVal.ConvertFromDBVal<string>(reader["Horario Semanal"]);
                                    responseComisariaDto.direccion = ConvertFDBVal.ConvertFromDBVal<string>(reader["Direccion"]);
                                    responseComisariaDto.telefono = ConvertFDBVal.ConvertFromDBVal<string>(reader["Telefono"]);
                                    responseComisariaDto.correo_electronico = ConvertFDBVal.ConvertFromDBVal<string>(reader["Correo Electronico"]);
                                    responseComisariaDto.cita_online = ConvertFDBVal.ConvertFromDBVal<bool>(reader["Cita Online"]);

                                    responseComisariaDto.disponibilidadCitasList = ObtenerDisponibilidadCitas(ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Comisaria"]));


                                    if (responseComisariaDto.disponibilidadCitasList.Count() == 0)
                                        responseComisariaDto.dispAgenda = false;
                                    else
                                        responseComisariaDto.dispAgenda = true;

                                    responseComisariaDtosList.Add(responseComisariaDto);
                                }
                                else
                                {
                                    mensaje = ConvertFDBVal.ConvertFromDBVal<string>(reader["mensaje_llamada_vida"])!;
                                }
                            }

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    ResponseComisariaDto responseComisariaDto = new ResponseComisariaDto();

                                    responseComisariaDto.ciudadMunicipio = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre CiudadM"]);
                                    responseComisariaDto.comisariaID = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Comisaria"]);
                                    responseComisariaDto.nombComisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre Comisaria"]);
                                    responseComisariaDto.horarioSemanal = ConvertFDBVal.ConvertFromDBVal<string>(reader["Horario Semanal"]);
                                    responseComisariaDto.direccion = ConvertFDBVal.ConvertFromDBVal<string>(reader["Direccion"]);
                                    responseComisariaDto.telefono = ConvertFDBVal.ConvertFromDBVal<string>(reader["Telefono"]);
                                    responseComisariaDto.correo_electronico = ConvertFDBVal.ConvertFromDBVal<string>(reader["Correo Electronico"]);
                                    responseComisariaDto.cita_online = ConvertFDBVal.ConvertFromDBVal<bool>(reader["Cita Online"]);

                                    responseComisariaDto.disponibilidadCitasList = ObtenerDisponibilidadCitas(ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Comisaria"]));

                                    if (responseComisariaDto.disponibilidadCitasList.Count() == 0)
                                        responseComisariaDto.dispAgenda = false;
                                    else
                                        responseComisariaDto.dispAgenda = true;

                                    responseComisariaDtosList.Add(responseComisariaDto);
                                }
                            }

                        }

                        _connectionDb.Close();
                    }
                }

                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

                tupleresponse = new Tuple<string, List<ResponseComisariaDto>>(mensaje, responseComisariaDtosList);

                responseListaPaginada.DatosPaginados = tupleresponse;
                responseListaPaginada.TotalRegistros = tupleresponse.Item2.Count;
                return responseListaPaginada;
            }
            catch (ControledException ex)
            {
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }


        public List<CitaDto> ObtenerListaCitasDisponiblesProximosTresDias(long idComisaria)
        {
            try
            {
                List<CitaDto> citaDtoList = new List<CitaDto>();

                List<SicofaCita> listaCitas = _context.SicofaCita.Where(sc => sc.IdComisaria == idComisaria && sc.Estado == 1  && sc.Activo == true && sc.FechaCita >= DateTime.Now && sc.FechaCita < DateTime.Now.AddDays(3)).ToList();
                foreach (SicofaCita sicofaCita in listaCitas)
                {
                    CitaDto citaDto = new CitaDto();
                    citaDto.idCita = sicofaCita.IdCita;
                    citaDto.idComisaria = sicofaCita.IdComisaria;
                    citaDto.fechaCita = sicofaCita.FechaCita.ToString();
                    citaDto.horaCita = sicofaCita.HoraCita.ToString();

                    citaDtoList.Add(citaDto);
                }
                return citaDtoList;
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


        public List<DisponibilidadCitaDto> ObtenerDisponibilidadCitas(long idComisaria)
        {
            try
            {
                var hoy = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);

                List<DisponibilidadCitaDto> disponibilidadCitaDtosList = new List<DisponibilidadCitaDto>();
                List<CitaDto> citaDtosList = new List<CitaDto>();

                citaDtosList = (from ct in _context.SicofaCita
                                where ct.IdComisaria == idComisaria & ct.FechaCita>hoy & ct.FechaCita <= hoy.AddDays(3)
                                & ct.Estado == 1 & ct.Activo == true
                                orderby ct.FechaCita, ct.HoraCita
                                select new CitaDto
                                {
                                    idCita = ct.IdCita,
                                    idComisaria = idComisaria,
                                    fechaCita = ct.FechaCita.ToString("MM/dd/yyyy"),
                                    horaCita = ct.HoraCita.ToString("hh:mm tt")
                                }).ToList();
                
                foreach (var item in citaDtosList.DistinctBy(x => x.fechaCita))
                {
                    DisponibilidadCitaDto disponibilidadCitaDto = new DisponibilidadCitaDto();

                    disponibilidadCitaDto.citaHorasList = ObtenerHorasFecha(item.fechaCita, citaDtosList);
                    disponibilidadCitaDto.fechaCita = item.fechaCita;
                    disponibilidadCitaDtosList.Add(disponibilidadCitaDto);
                }

                return disponibilidadCitaDtosList;
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

        public ResponseListaPaginada CrearCita(RequestCitaDto requestCitaDto)
        {
            try
            {
                ResponseCitaDto responseCitaDto = new ResponseCitaDto();
                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_CREAR_CITA";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idCita", BdValidation.ToDBNull(requestCitaDto.idCita));
                        _command.Parameters.AddWithValue("@numeroDocumento", BdValidation.ToDBNull(requestCitaDto.numeroDocumento));
                        _command.Parameters.AddWithValue("@idTipoDocumento", BdValidation.ToDBNull(requestCitaDto.tipoDocumento));
                        _command.Parameters.AddWithValue("@nombCiudadano", BdValidation.ToDBNull(requestCitaDto.nombCiudadano!.Trim()));
                        _command.Parameters.AddWithValue("@primerApellido", BdValidation.ToDBNull(requestCitaDto.primerApellido!.Trim()));
                        _command.Parameters.AddWithValue("@segundoApellido", BdValidation.ToDBNull(requestCitaDto.segundoApellido!.Trim()));

                        _command.Parameters.AddWithValue("@direccionRecidencia", BdValidation.ToDBNull(requestCitaDto.direccResidencia));

                        _command.Parameters.AddWithValue("@telefono", BdValidation.ToDBNull(requestCitaDto.telf));
                        _command.Parameters.AddWithValue("@celular", BdValidation.ToDBNull(requestCitaDto.celular));
                        _command.Parameters.AddWithValue("@correo_electronico", BdValidation.ToDBNull(requestCitaDto.correoElectronico));
                        _command.Parameters.AddWithValue("@idComisaria", BdValidation.ToDBNull(requestCitaDto.idComisaria));

                        string listTipoAtencion = "";

                        foreach (var item in requestCitaDto.tipoAtencionList)
                            listTipoAtencion = listTipoAtencion + item.ToString() + ",";

                        _command.Parameters.AddWithValue("@listTipoViolencia", BdValidation.ToDBNull(listTipoAtencion));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();


                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                responseCitaDto.nombComisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["nombre"]);
                                responseCitaDto.fechacita = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["fecha_cita"]).ToString("dd/MM/yyyy");
                                responseCitaDto.horacita = ConvertFDBVal.ConvertFromDBVal<DateTime>(reader["hora_cita"]).ToString("hh:mm tt");
                            }

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Creando  la conexión  a la BD ( _connectionDb.Open() )", listarVehiculosDtoParam);

                        _connectionDb.Close();

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Cerrando  la conexión  a la BD ( _connectionDb.Close() )", listarVehiculosDtoParam);
                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "La ejecución del servicio de  consultarVehiculos fue realizada con éxito.", listarVehiculosDtoParam);

                    }
                }

                responseListaPaginada.DatosPaginados = responseCitaDto;

                responseListaPaginada.TotalRegistros = 1;

                return responseListaPaginada;

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

        
         public ResponseListaPaginada ObtenerCitaComisaria(int id_comisaria)
        {
            try
            {
                string? mensaje;
                List<CitaDto> citas = new List<CitaDto>();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_CONSULTAR_CITAS_PUBLICAS";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@id_comisaria", BdValidation.ToDBNull(id_comisaria));

                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1)
                                {
                                    CitaDto cita = new CitaDto();

                                    cita.Estado = ConvertFDBVal.ConvertFromDBVal<int>(reader["estado"]);
                                    //cita.Activo = ConvertFDBVal.ConvertFromDBVal<int>(reader["activo"]);
                                    //cita.idCita = ConvertFDBVal.ConvertFromDBVal<int>(reader["id_cita"]);
                                    citas.Add(cita);
                                }
                            }

                            _connectionDb.Close();
                            ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();
                            responseListaPaginada.DatosPaginados = citas;
                            responseListaPaginada.TotalRegistros = citas.Count;
                            return responseListaPaginada;
                        }
                        throw new Exception("no existen datos");
                    }
                }
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

        public ResponseListaPaginada ReservarObtenerDisponibilidadCita(long idCita)
        {
            try
            {
                string? mensaje;
                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();
                ResponseComisariaDto responseComisariaDto = new ResponseComisariaDto();

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_RESERVAR_OBTENER_DISPONIBILIDAD_CITA";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idCita", BdValidation.ToDBNull(idCita));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Creando  la conexión  a la BD ( _connectionDb.Open() )", listarVehiculosDtoParam);

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {

                                if (reader.FieldCount > 1)
                                {

                                    responseComisariaDto.ciudadMunicipio = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre CiudadM"]);
                                    responseComisariaDto.comisariaID = ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Comisaria"]);
                                    responseComisariaDto.nombComisaria = ConvertFDBVal.ConvertFromDBVal<string>(reader["Nombre Comisaria"]);
                                    responseComisariaDto.horarioSemanal = ConvertFDBVal.ConvertFromDBVal<string>(reader["Horario Semanal"]);
                                    responseComisariaDto.direccion = ConvertFDBVal.ConvertFromDBVal<string>(reader["Direccion"]);
                                    responseComisariaDto.telefono = ConvertFDBVal.ConvertFromDBVal<string>(reader["Telefono"]);
                                    responseComisariaDto.correo_electronico = ConvertFDBVal.ConvertFromDBVal<string>(reader["Correo Electronico"]);
                                    responseComisariaDto.cita_online = ConvertFDBVal.ConvertFromDBVal<bool>(reader["Cita Online"]);

                                    responseComisariaDto.disponibilidadCitasList = ObtenerDisponibilidadCitas(ConvertFDBVal.ConvertFromDBVal<long>(reader["ID Comisaria"]));

                                    if (responseComisariaDto.disponibilidadCitasList.Count() == 0)
                                        responseComisariaDto.dispAgenda = false;
                                    else
                                        responseComisariaDto.dispAgenda = true;
                                }
                                else
                                {
                                    mensaje = ConvertFDBVal.ConvertFromDBVal<string>(reader["Mensaje"]);
                                    responseListaPaginada.DatosPaginados = mensaje;
                                    return responseListaPaginada;
                                }

                            }

                        _connectionDb.Close();

                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Cerrando  la conexión  a la BD ( _connectionDb.Close() )", listarVehiculosDtoParam);
                        //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "La ejecución del servicio de  consultarVehiculos fue realizada con éxito.", listarVehiculosDtoParam);

                    }
                }

                responseListaPaginada.DatosPaginados = responseComisariaDto;

                return responseListaPaginada;
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

        public List<CitaHora> ObtenerHorasFecha(string fecha, List<CitaDto> citaDtos)
        {
            try
            {
                List<CitaHora> listCitaHoras = new List<CitaHora>();

                foreach (var item in citaDtos)
                {
                    if (fecha == item.fechaCita)
                    {

                        CitaHora citaHora = new CitaHora();

                        citaHora.idCita = item.idCita;
                        citaHora.horaCita = item.horaCita;

                        listCitaHoras.Add(citaHora);

                    }
                }

                return listCitaHoras;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Cambia el estado de la Cita a Atendida
        /// </summary>
        /// <param name="CitaDto"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public ResponseListaPaginada AtenderCita(long idCiudadano)
        {
            try
            {
                ResponseListaPaginada responseListaPaginada = new ResponseListaPaginada();


                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_ATENDER_CITA";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@idCiudadano", BdValidation.ToDBNull(idCiudadano));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();


                        using SqlDataReader reader = _command.ExecuteReader();

                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                idCiudadano = ConvertFDBVal.ConvertFromDBVal<long>(reader["id_Cita"]);
                            }
                        _connectionDb.Close();
                    }
                }

                responseListaPaginada.DatosPaginados = idCiudadano;

                responseListaPaginada.TotalRegistros = 1;

                return responseListaPaginada;

            }
            catch (ControledException ex)
            {
                //TODO: Log Auditoria de Errores
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //TODO: Log Auditoria de Errores
                throw new ControledException(ex.HResult);
            }
        }

        public async Task CambioEstadoCita(long idCita, int estado)
        {
            try
            {
                var cita = await _context.SicofaCita.Where(s => s.IdCita == idCita).FirstAsync();

                cita.Estado = estado;

                await _context.SaveChangesAsync();


            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }

        public async  Task CrearCitaPresolicitud(RequestCitaDto requestCitaDto) 
        {
            try
            {

               long idciudadano = await AgregarCiudadano(requestCitaDto);

                await AsignarCitaCiudadano(requestCitaDto.idCita , idciudadano);

                await TipoViolenciaPorcita(requestCitaDto.idCita , requestCitaDto.tipoAtencionList);

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);    
            
            }
        
        
        }

        private async Task<long> AgregarCiudadano(RequestCitaDto requestCitaDto) {

            try
            {
               
               var ciudadanoPrevio= await _context.SicofaCiudadano.Where(s => s.NumeroDocumento == requestCitaDto.numeroDocumento).FirstOrDefaultAsync();

                if (ciudadanoPrevio != null) {

                    ciudadanoPrevio.RequiereModificacion = true;
                    await _context.SaveChangesAsync();

                    return ciudadanoPrevio.IdCiudadano;
                }

                SicofaCiudadano ciudadano = new SicofaCiudadano();

                ciudadano.NumeroDocumento = requestCitaDto.numeroDocumento;
                ciudadano.IdTipoDocumento = requestCitaDto.tipoDocumento;
                ciudadano.NombreCiudadano = requestCitaDto.nombCiudadano;
                ciudadano.PrimerApellido = requestCitaDto.primerApellido;
                ciudadano.SegundoApellido = requestCitaDto.segundoApellido;
                ciudadano.DireccionResidencia = requestCitaDto.direccResidencia;
                ciudadano.Celular = requestCitaDto.celular;
                ciudadano.TelefonoFijo = requestCitaDto.telf;
                ciudadano.CorreoElectronico = requestCitaDto.correoElectronico;
                ciudadano.RequiereModificacion = true;

                _context.SicofaCiudadano.Add(ciudadano);
                await _context.SaveChangesAsync();

                return ciudadano.IdCiudadano;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message); 
            
            }

        }
        private async Task AsignarCitaCiudadano(long idCita, long idCiudadano ) 
        
        {
            try
            {
                // actualiza la cita
                SicofaCita cita = await _context.SicofaCita.Where(s => s.IdCita == idCita).FirstAsync();
                cita.IdCiudadano = idCiudadano;
                cita.OrigenCita = "Presolicitud";
                cita.Estado = 3;
                cita.Activo = false;

                // crea el registro en cita tipo violencia

                await _context.SaveChangesAsync();

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        }

        private async Task TipoViolenciaPorcita(long idCita, List<int> data)
        {
            try
            {

                List<SicofaCitaTipoDeViolencia> citaViolencia = new List<SicofaCitaTipoDeViolencia>();

                foreach (var violencia in data) {

                    SicofaCitaTipoDeViolencia inner = new SicofaCitaTipoDeViolencia() {IdCita = idCita , IdTipoViolencia = violencia };
                    citaViolencia.Add(inner);
                }

                await _context.SicofaCitaTipoDeViolencia.AddRangeAsync(citaViolencia);

                await _context.SaveChangesAsync();


               
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task<ControledResponseDTO> GuardarCita(CrearCita data,int comisaria)
        {
            try
            {
                ControledResponseDTO response = new ControledResponseDTO();
                response.state = true;

                var previo = _context.SicofaCita.Where(s => s.IdComisaria == comisaria && s.FechaCita == data.fechaCita && data.horaCita == data.horaCita).FirstOrDefault();

                if (previo == null)
                {
                    SicofaCita cita = new SicofaCita();

                    cita.HoraCita = data.horaCita;
                    cita.FechaCita = data.fechaCita;
                    cita.Estado = 1;
                    cita.IdComisaria = comisaria;
                    cita.OrigenCita = CitaMensaje.tipogendamiento;
                    cita.Activo = true;

                    _context.SicofaCita.Add(cita);

                    await _context.SaveChangesAsync();
                    response.message = "Se crea la cita de forma correcta";
                }
                else
                {
                    response.state = false;
                    response.message = "Ya se encuentra una cita registrada con estas fechas para la comisaria.";
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ActualizarEstadoCita(long idCita, bool activo)
        {
            try
            {
              var cita =  _context.SicofaCita.Where(s => s.IdCita == idCita).First();

                cita.Activo = activo;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CitaDisponibleDTO>> ConsultarCita( int comisaria) 
        {
            var hoy = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow); ;
            
            var response = await _context.SicofaCita.Where(s => s.IdComisaria == comisaria & s.Estado == 1 && s.FechaCita >= hoy).
                Select(se => new CitaDisponibleDTO { idCita = se.IdCita , fechaCita = se.FechaCita , activo = se.Activo , HoraCita = se.HoraCita})
                .OrderBy(se => se.fechaCita).ThenBy(se => se.HoraCita).ToListAsync();

            return response;
        }
    }
}
