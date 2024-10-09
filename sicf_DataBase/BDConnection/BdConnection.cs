using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.BDConnection
{
    public class BdConnection
    {
        protected SqlConnectionStringBuilder builder { set; get; }

        private IConfiguration configuration { get; set; }

        internal SqlCommand _command;

        internal SqlConnection _connectionDb;

        public BdConnection(IConfiguration iconfig) {

            _command = new SqlCommand();
            _connectionDb = new SqlConnection();
            configuration = iconfig;
            builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
    }

    static public class ConvertFDBVal {

        public static T? ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }
    }

    public static class BdValidation
    {
        /// <summary>
        /// valida un parametro de entrada para validar si es null o no 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static  object ToDBNull(object? value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
    }
}
