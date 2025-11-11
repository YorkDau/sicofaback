namespace sicf_Models.Dto.Compartido
{
    public class InvolucradoComplementariaDTO
    {
        public long IdInvolucrado { get; set; }
        public string? RegistroExpedidoEn { get; set; }
        public string? NombreEntidadExpedicion { get; set; }
        public string? DatosAdicionales { get; set; }
        public string? NombreResponsableCustodia { get; set; }
        public string? ParentescoResponsableCustodia { get; set; }
        public string? NombreResponsableCuidado { get; set; }
        public string? ParentescoResponsableCuidado { get; set; }
        public bool? VinculacionSistemaSalud { get; set; }
        public string? Regimen { get; set; }
        public string? BeneficiarioDeNombre { get; set; }
        public bool? FisicaAdecuada { get; set; }
        public bool? NutricionalAdecuada { get; set; }
        public bool? PsicologaAdecuada { get; set; }
        public bool? VacunacionCompleta { get; set; }
        
        public string? Adjunto { get; set; }
        public string? Consetimiento { get; set; }

        public bool? Escolarizado { get; set; }
        public string? MatriculadoEnElColegio { get; set; }
        public string? GradoCursa { get; set; }
        public string? JornadaEstudio { get; set; }
        public string? TipoVivienda { get; set; }
        public string? OtroTipoVivienda { get; set; }

        public string? OtroTipoViviendaCual { get; set; }
        public string? NumeroHabitacionesVivienda { get; set; }
        public string? DistribuciuonHabitaciones { get; set; }
        public bool? ViviendaConBaños { get; set; }
        public bool? ViviendaconCocina { get; set; }
        public bool? ViviendaConLuz { get; set; }
        public bool? ViviendaConAgua { get; set; }
        public bool? ViciendaConGas { get; set; }
        public bool? OtrosServicios { get; set; }
        public string? Estratificacion { get; set; }
        public bool? AsisteExtracurriculares { get; set; }
        public string? ActividadesExtracurriculares { get; set; }
        public bool? FamiliaExtensa { get; set; }
        public string? OtraInformacionFamiliaExtensa { get; set; }
        public long? IdAnexoSolicitud { get; set; }
        public string? Entrada { get; set; }
        public bool? NoInformacion { get; set; }

        public string? ObservacionesSalud { get; set; }
        public string? ObservacionesPsicologia { get; set; }
        public string? ObservacionesTrabajoSocial { get; set; }
    }
}
