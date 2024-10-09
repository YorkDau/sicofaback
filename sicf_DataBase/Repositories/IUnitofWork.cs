using Microsoft.EntityFrameworkCore.Infrastructure;
using sicf_DataBase.Repositories.EvaluacionPsicologica;
using sicf_DataBase.Repositories.TestEntity;
using sicf_DataBase.Repositories.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories
{
    public interface IUnitofWork
    {

        void SaveChanges();
        Task<int> SaveChangesAsync();

        DatabaseFacade Database { get; }

        ICiudadanoRepository CiudadanoRepository { get; }

        ISolicitudEFRepository SolicitudEFRepository { get;}

        IUsuarioRepository  UsuarioRepository { get; }
    }
}
