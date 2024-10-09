using sicf_Models.Utility;
using System.Runtime.Serialization;

namespace sicfExceptions.Exceptions
{
    public class ControledException : Exception
    {
        private readonly static Dictionary<int, string> _excepcionArgumentos = new Dictionary<int, string>();
        readonly public ResponseGeneric RespuestaApi = new ResponseGeneric
        {
            Data = null,
            Message = "Error Desconocido",
            Status = "500",
            Success = false
        };

        public ControledException()
        {
        }

        /// <summary>
        /// ControledException : Entidad para el manejo de excepciones controladas
        /// </summary>
        /// <param name="mensaje">mensaje en string.</param>
        public ControledException(string mensaje)
        : base(mensaje)
        {
            RespuestaApi.Message = mensaje;
        }
        /// <summary>
        /// ControledException : Entidad para el manejo de excepciones controladas
        /// </summary>
        /// <param name="code">Codigo de Excepcion Int.</param>
        public ControledException(int code)
        {
            RespuestaApi.Status = code.ToString();
        }

        /// <summary>
        /// ControledException : Entidad para el manejo de excepciones controladas
        /// </summary>
        /// <param name="innerException">Objecto Exception.</param>
        public ControledException(Exception innerException)
        {
            RespuestaApi.Message = innerException.Message;
        }

        /// <summary>
        /// ControledException : Entidad para el manejo de excepciones controladas
        /// </summary>
        /// <param name="mensaje">Objecto Exception.</param>
        /// <param name="argumentos">Objecto Exception.</param>
        public ControledException(string mensaje, params string[] argumentos)
        : base(string.Format(mensaje, argumentos))
        {

            RespuestaApi = new ResponseGeneric
            {
                Data = null,
                Message = "",
                Status = "500",
                Success = false
            };
        }

        /// <summary>
        /// ControledException : Entidad para el manejo de excepciones controladas
        /// </summary>
        /// <param name="code">Codigo Exception Int.</param>
        /// <param name="argumentos">String[] argumentos</param>
        public ControledException(int code, params string[] argumentos)
        {
            _excepcionArgumentos.Add(code, argumentos[0]);
            RespuestaApi = new ResponseGeneric
            {
                Data = null,
                Message = argumentos[0],
                Status = code.ToString(),
                Success = false
            };
        }

        public ControledException(string mensaje, int code)
        : base(mensaje)
        {
            RespuestaApi.Status = code.ToString();
            RespuestaApi.Message = mensaje;
        }

        protected ControledException(SerializationInfo info, StreamingContext context) : base(info, context) { }


        public string GetArguments(int Code)
        {
            string argument = "";

            if (_excepcionArgumentos != null && _excepcionArgumentos.Count != 0)
            {
                argument = _excepcionArgumentos[Code];
                _excepcionArgumentos.Clear();
            }

            return argument;
        }
    }
}
