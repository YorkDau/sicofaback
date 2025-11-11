namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class RegistroEvaluacionInfoMenoresDTO
    {
        public long IdSolicitudServicio { get; set; }
        
        public bool hayMenores { get; set; } = false;
        
        public string? valoracionPsicologica { get; set; } = string.Empty;

        public string? valoracionEntornoFamiliar { get; set; } = string.Empty;

    }
}
