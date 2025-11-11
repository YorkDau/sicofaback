using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Presolicitud
{
    public class PresolicitudOUT
    {
        public long id_solicitud { get; set; }
        public long? idCiudadano { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public string descripcion_hechos { get; set; }
        public string? tipo_documento_denunciante { get; set; }
        public string? numero_documento_denunciante { get; set; }
        public string? nombres_denunciante { get; set; }
        public string? correo_denunciante { get; set; }
        public string? telefono_denunciante { get; set; }
        public int? tipo_entidad_denunciante { get; set; }
        public string? num_documento_victima { get; set; }
        public string? tipo_documento_victima { get; set; }
        public string? primer_nombre_victima { get; set; }
        public string? segundo_nombre_victima { get; set; }
        public string? primer_apellido_victima { get; set; }
        public string? segundo_apellido_victima { get; set; }
        public string? correo_electronico_victima { get; set; }
        public string? telefono_victima { get; set; }
        public string? direccion_victima { get; set; }
        public string? datos_adicionales_victima { get; set; }
        public DateTime? fecha_nacimiento_victima { get; set; }
        public string tipoPresolicitud { get; set; }
        public string? archivoAdjunto { get; set; }
        public long? id_anexo { get; set; }
        public long? id_comisaria { get; set; }
        public long? idSolicitudRelacionado { get; set; }
        public List<SolicitudServicioDTO>? solicitudesCiudadano { get; set; }
        
                
        public PresolicitudOUT()
        {
            tipo_documento_denunciante = string.Empty;
            tipo_documento_victima = string.Empty;
            descripcion_hechos = string.Empty;
            numero_documento_denunciante = string.Empty;
            nombres_denunciante = string.Empty;
            correo_denunciante = string.Empty;
            telefono_denunciante = string.Empty;
            num_documento_victima = string.Empty;
            primer_nombre_victima = string.Empty;
            segundo_nombre_victima = string.Empty;
            primer_apellido_victima = string.Empty;
            segundo_apellido_victima = string.Empty;
            correo_electronico_victima = string.Empty;
            telefono_victima = string.Empty;
            direccion_victima = string.Empty;
            datos_adicionales_victima = string.Empty;
            tipoPresolicitud = string.Empty;
            archivoAdjunto = string.Empty;
        }
    }
}
