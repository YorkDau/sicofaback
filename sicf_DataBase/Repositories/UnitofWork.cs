using Microsoft.EntityFrameworkCore.Infrastructure;
using sicf_DataBase.Data;
using sicf_DataBase.Repositories.EvaluacionPsicologica;
using sicf_DataBase.Repositories.TestEntity;
using sicf_DataBase.Repositories.Usuario;
using sicf_DataBase.Repositories.Tarea;

namespace sicf_DataBase.Repositories
{
    public class UnitofWork : IUnitofWork
    {

        private readonly SICOFAContext context;
       

        public UnitofWork(SICOFAContext context) 
        {
            this.context = context;
          
        }


        public ICiudadanoRepository CiudadanoRepository => new CiudadanoRepository(context);

        public ISolicitudEFRepository SolicitudEFRepository => new SolicitudEFRespository(context);

        public IUsuarioRepository UsuarioRepository => new UsuarioRepository(context);



        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
}

        public DatabaseFacade Database
        {
            get { return context.Database; }
        }

    }
}
