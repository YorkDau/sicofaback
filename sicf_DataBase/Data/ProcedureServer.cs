using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace sicf_DataBase.Data
{
    public static class ProcedureServer
    {

        public static IQueryable<TEntity> ExecuteStoreProdecure<TEntity>(this DbSet<TEntity> source, string sql, params SqlParameter[] parameters) where TEntity : class
        {
            return source.FromSqlRaw(sql, parameters);
        }

    }
}

