using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Compartido
{
    public interface ISendgridNotificaciones
    {
        public Task<bool> enviarNotificacionCorreoElectronico();

        public  Task<bool> EnviarCambioContrasena(string email, string temporalPass);

        public  Task<bool> EnviarContrasena(string email, string pass);
    }
}
