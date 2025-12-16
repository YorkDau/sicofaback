using Humanizer;
using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Comisario;
using sicf_Models.Dto.Presolicitud;
using sicf_Models.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Comisario
{
    public class ComisarioRepository : IComisarioRepository
    {

        private readonly SICOFAContext context;

        public ComisarioRepository(SICOFAContext context) 
        {
            this.context = context;
        }


        public async Task RegistrarTomaDecision(RequestTomaDecisionDTO data)
        {
            SicofaSolicitudServicio solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstOrDefault();
            SicofaSolicitudServicioComplementaria complementaria = context.SicofaSolicitudServicioComplementaria.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstOrDefault();
            if (solicitud == null)
            {
                throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
            }
            solicitud.Conciliacion = data.concilacionPrevia;
            solicitud.CumpleConciliacion = data.cumpleConcilacionPrevia;
            if (data.concilacionPrevia)
            {
                solicitud.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.no_competencia;
            }
            if (complementaria == null)
            {
                complementaria = new SicofaSolicitudServicioComplementaria();
                complementaria.IdSolicitudServicio = data.idSolicitudServicio;
                complementaria.IdEntidadTraslado = data.idEntidadTraslado;
                complementaria.ObservacionesComisario = data.observaciones;
                context.SicofaSolicitudServicioComplementaria.Add(complementaria);
            } 
            else
            {
                complementaria.IdEntidadTraslado = data.idEntidadTraslado;
                complementaria.ObservacionesComisario = data.observaciones;

            }
            await context.SaveChangesAsync();
        }

        public async Task RegistrarTomaDecisionInformacion(RequestTomaDecisionInformacionDTO data)
        {
            SicofaSolicitudServicio solicitud = context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstOrDefault();
            SicofaSolicitudServicioComplementaria complementaria = context.SicofaSolicitudServicioComplementaria.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstOrDefault();
            if (solicitud == null)
            {
                throw new Exception(ErrorRespuestaEvaluacionRiesgo.errorEvaluacionPsicologica);
            }
            solicitud.EsNecesarioRemitir = data.esNecesarioRemitir;
            if (data.cierre)
            {
                solicitud.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.sin_vulneracion;
            }
            if (complementaria == null)
            {
                complementaria = new SicofaSolicitudServicioComplementaria();
                complementaria.IdSolicitudServicio = data.idSolicitudServicio;
                complementaria.ObservacionesComisario = data.observaciones;
                context.SicofaSolicitudServicioComplementaria.Add(complementaria);
            }
            else
            {
                complementaria.ObservacionesComisario = data.observaciones;
            }
            await context.SaveChangesAsync();
        }

    }

}





