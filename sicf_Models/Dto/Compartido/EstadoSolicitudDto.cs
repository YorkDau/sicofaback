
namespace sicf_Models.Dto.Compartido
{
    public class EstadoSolicitudDto
    {
        public int id_estado_solicitud { get; set; }
        public string estado_solicitud { get; set; }


        public EstadoSolicitudDto()
        {
            id_estado_solicitud = default;
            estado_solicitud = string.Empty;
        }

    }
}