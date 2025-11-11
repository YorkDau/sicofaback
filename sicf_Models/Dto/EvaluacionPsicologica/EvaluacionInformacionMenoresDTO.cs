namespace sicf_Models.Dto.EvaluacionPsicologica;

public record EvaluacionInformacionMenoresDTO(
   bool? hayMenores, 
   string? valoracionPsicologica, 
   string? valoracionEntornoFamiliar
);