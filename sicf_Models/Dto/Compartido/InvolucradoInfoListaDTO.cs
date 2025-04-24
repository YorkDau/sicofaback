using System.ComponentModel.DataAnnotations;

namespace sicf_Models.Dto.Compartido
{
    public class InvolucradoInfoListaDTO
    {
        [Key]
        public long? ID { get; set; }
        public long? IdSolicitudServicio { get; set; }
        public long? IdInvolucrado { get; set; }
        public string? NumeroDocumento { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string? TipoDocumento { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public int? Edad { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public bool?   EsVictima { get; set; }
        public bool?   EsPrincipal { get; set; }
        //public long?   IdLugarExpedicion { get; set; }
        public int? PaisExp { get; set; }
        public long? DepartamentoExp { get; set; }
        public long? MunicipioExp { get; set; }
        public string? Eps { get; set; }
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
        public bool? PsicologicaAdecuada { get; set; }
        public bool? VacunacionCompleta { get; set; }
        public string? MatriculadoEnElColegio { get; set; }
        public string? GradoCursa { get; set; }
        public string? JornadaEstudio { get; set; }
        public string? TipoVivienda { get; set; }
        public string? OtroTipoVivienda { get; set; }
        public int? NumeroHabitacionesVivienda { get; set; }
        public string? DistribuciuonHabitaciones { get; set; }
        public bool? ViviendaConBaños { get; set; }
        public bool? ViviendaConCocina { get; set; }
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
        public string? NombreDenunciante { get; set; }
        public string? NumeroDocumentoDen { get; set; }
        public string? TiposViolencia { get; set; }
        public string? NombreDocumento { get; set; }

        public string? OtroTipoViviendaCual { get; set; } 
        public string? Agresor { get; set; }

        public string? ObservacionesSalud { get; set; }
        public string? ObservacionesPsicologia { get; set; }
        public string? ObservacionesTrabajoSocial { get; set; }
    }
}
