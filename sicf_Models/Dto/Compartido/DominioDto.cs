
namespace sicf_Models.Dto.Compartido
{
    public class DominioDto
    {
        public int id_Dominio { get; set; }
        public string Tipo_Dominio { get; set; }
        public string codigo { get; set; }
        public string Nombre_Dominio { get; set; }
        public string Tipo_Lista { get; set; }

        public DominioDto()
        {
            id_Dominio = default;
            Tipo_Dominio = string.Empty;
            codigo = string.Empty;
            Nombre_Dominio = string.Empty;
            Tipo_Lista = string.Empty;
        }


    }
}
