using Newtonsoft.Json;

namespace sicfExceptions.Exceptions
{
    public class ExceptionManager
    {
       
        private static readonly Diccionario _mensajesClase = new Diccionario();

        Dictionary<int, object> _mensajesJson;
        private ExceptionManager(string idioma)
        {
            _mensajesJson = new Dictionary<int, object>();
            CargarMensajes(idioma);
        }
        public static ExceptionManager Inicio(string idioma)
        {
            return new ExceptionManager(idioma);
        }
        public Exception Procesar(Exception excepcion)
        {
            return ProcesarExcepcion(excepcion);
        }
        private Exception ProcesarExcepcion(Exception excepcion)
        {
            var excepcionControlada = new ControledException();
            if (excepcion.GetType() == typeof(ControledException))
                excepcionControlada = (ControledException)excepcion;

            excepcionControlada.RespuestaApi.Message = ObtenerMensajes(excepcionControlada);
            excepcionControlada.RespuestaApi.Data = excepcionControlada.RespuestaApi.Message;

            //if (ValidarPersistenciaMongoDB())
            //{
            //    _conexionMongoDB.InsertaLogs(excepcion, excepcionControlada);
            //}
            //if (ValidarPersistenciaArchivo())
            //{
            //    _conexionArchivo.InsertaLogs(excepcion, excepcionControlada);
            //}
            return excepcionControlada;
        }
       
        private void CargarMensajes(string idioma)
        {
            idioma = ValidarIdioma(idioma);

            try
            {
                //var results = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(File.ReadAllText(ConfigManager._rutaConfigMensajes)).FirstOrDefault()[idioma].ToString();
                //var res = JsonConvert.DeserializeObject<Dictionary<string, object>>(results).ToList();

                //foreach (var appMensaje in res)
                //{
                //    _mensajesJson.Add(Convert.ToInt32(appMensaje.Key), appMensaje.Value.ToString());
                //}
            }
            catch
            {

                _mensajesJson = _mensajesClase.diccionarioES();

                //No carga diccionario desde archivo config
                //Se carga desde clase :
                //switch (idioma)
                //{
                //    case Language.Spanish:
                //        _mensajesJson = _mensajesClase.diccionarioES();
                //        break;
                //    case Language.English:
                //        _mensajesJson = _mensajesClase.diccionarioEN();
                //        break;
                //}
            }

        }

        private string ObtenerMensajes(ControledException excepcionControlada)
        {
            var appMessage = excepcionControlada.RespuestaApi.Message;
            var argumentos = excepcionControlada.GetArguments(Convert.ToInt32(excepcionControlada.RespuestaApi.Status));
            try
            {
                //if (excepcionControlada.RespuestaApi != null && _mensajesJson.ContainsKey(Convert.ToInt32(excepcionControlada.RespuestaApi.Status)))
                //{
                //    appMessage = _mensajesJson[Convert.ToInt32(excepcionControlada.RespuestaApi.Status)].ToString();
                //    if (argumentos != "")
                //    {
                //        appMessage = string.Format(appMessage, argumentos);
                //    }
                //}
            }
            catch
            {
                return appMessage;
            }
            return appMessage;
        }
        private static string ObtenerIdiomaPorDefecto()
        {
            try
            {
                //var configuracion = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(File.ReadAllText(ConfigManager._rutaConfiguracion)).FirstOrDefault()["LanguageConfiguration"].ToString();
                //return JsonConvert.DeserializeObject<Dictionary<string, object>>(configuracion).ToList()[0].Value.ToString();

                return "";
            }
            catch
            {
                return "es";//Language.Spanish;
            }
        }

        private string ValidarIdioma(string idioma)
        {

            //if (idioma == Language.English ||
            //   idioma == Language.Spanish)
            //{
            //    return idioma;
            //}

            return "es";//_idiomaPorDefecto;
        }

    }
}
