using AutoMapper;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Compartido;
using sicf_DataBase.Repositories;
using sicf_DataBase.Repositories.EvaluacionPsicologica;
using sicf_DataBase.Repositories.Tarea;
using sicf_Models.Core;
using sicf_Models.Dto;
using sicf_Models.Dto.EvaluacionPsicologica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.EvaluacionPsicologica
{
    public class EvaluacionPiscologicaService : IEvaluacionPsicologicaService
    {

        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;
        private ICompartidoRepository compartido;
        private IEvaluacionPsicologicaRepository EvaluacionPsicologicaRepository;
        private ITareaRepository tareaRepository;


        public EvaluacionPiscologicaService(IUnitofWork unitofWork, IMapper mapper, 
            IEvaluacionPsicologicaRepository EvaluacionPsicologicaRepository, 
            ICompartidoRepository compartido, ITareaRepository tareaRepository) {

            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.compartido = compartido;
            this.EvaluacionPsicologicaRepository = EvaluacionPsicologicaRepository;
            this.tareaRepository = tareaRepository;
            
        }


        #region HU2

        public AccionanteDTO IdentificarAccionante(long idSolicitudProceso) {

            try
            {

                return EvaluacionPsicologicaRepository.IdentificarAccionante(idSolicitudProceso);
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }

        }

        public DatosInstitucionesDTO IdentificarDatosInstitucionales(long id)
        {
            try
            {

                return EvaluacionPsicologicaRepository.IdentificarDatosInstitucionales(id);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public InformacionVictimaDTO ObtenerInvolucrado(long id, bool esvictima, bool principal)
        {
            try
            {

                return EvaluacionPsicologicaRepository.ObtenerInvolucrado(id, esvictima, principal);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public void ActualizarInvolucrado(ActualizacionInvolucradoDTO data) {

            try
            {
                EvaluacionPsicologicaRepository.ActualizarInvolucrado(data);
                // Cerrar tarea y crear la nueva con identificacion de riesgo
                var involucrado = EvaluacionPsicologicaRepository.ConsultarInvolucrado(data.IdInvolucrado);

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }

            

        }
        #endregion HU2



        public async Task<List<QuestionarioRespuestaPreviaDTO>> ObtenerCuestionarioViolencia(int tipoViolencia, long idSolicitudServicio, long? idTarea) 
        {
            try
            {
                long tarea = await tareaRepository.UltimaTarea(idSolicitudServicio);
                var dom = await EvaluacionPsicologicaRepository.ObtenerCuestionarioViolencia(tipoViolencia, idSolicitudServicio, tarea);

                return dom;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }

        }

        public void  RegistrarCuestionario(RespuestaCuestionarioDTO data) {

            try
            {
                EvaluacionPsicologicaRepository.RegistrarCuestionario(data, data.idTarea);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EvaluacionRiegoDTO> EvaluacionRiesgosPorSolicitud(long idSolicitud) {


            try
            {
                var evaluacionT = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(idSolicitud, null);
               return  await EvaluacionPsicologicaRepository.EvaluacionRiesgosPorSolicitud(idSolicitud, evaluacionT.IdTarea);
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            
            }

        
        }

        public DescripcionHechosDTO ObtenerDescripcionHechosPorSolicitud(long id)
        {
            try {

                return EvaluacionPsicologicaRepository.ObtenerDescripcionHechosPorSolicitud(id);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }

        }

        public void ActualizarDescripcioHechosPorSolicitud(DescripcionHechosDTO data)
        {


            try
            {
                EvaluacionPsicologicaRepository.ActualizarDescripcioHechosPorSolicitud(data);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }

        }

        public long CrearEvaluacionPsicologica(long idSolicitudServicio, long idTarea)
        {
            try {

              return   EvaluacionPsicologicaRepository.CrearEvaluacionPsicologica(idSolicitudServicio,idTarea);

                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void RegistrarRecomendacion(long idSolicitudServicio, string descripcion) {
            try
            {

                var evaluacionT = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(idSolicitudServicio, null);
                EvaluacionPsicologicaRepository.RegistrarRecomendacion(idSolicitudServicio, descripcion, evaluacionT.IdTarea);

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }

        public List<EvaluacionOrientacionRespuesta> EvaluacionOrientacion(long idSolicitudServicio, string data) 
        {
            try
            {
                var evaluacionT = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(idSolicitudServicio, null);
                return EvaluacionPsicologicaRepository.EvaluacionOrientacion(idSolicitudServicio, data , evaluacionT.IdTarea);

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }

        }

        public async Task RegistrarEvaluacionOrientacion(RegistroEvaluacionEmocionalDTO data)
        {
            try
            {
                var evaluacionT = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(data.IdSolicitudServicio, null);
                EvaluacionPsicologicaRepository.RegistrarEvaluacionOrientacion(data, (long)evaluacionT.IdTarea);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task ActualizarEvaluacionPsicologica(RegistroEvaluacionEmocionalDTO data)
        {
            try
            {
                //var idTarea =await tareaRepository.UltimaTarea(data.IdSolicitudServicio);
                var evaluacionT = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(data.IdSolicitudServicio, null);
                await EvaluacionPsicologicaRepository.ActualizarEvaluacionPsicologica(data, (long)evaluacionT.IdTarea);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

         public async Task<ObtenerEvaluacionPsicologicaEmocionalDTO> ObtenerEvaluacionPsicologicaEmocional(long idSolicitudServicio, string tipoDominio )
            {
        try
            {
                var evaluacionT = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(idSolicitudServicio, null);
                ObtenerEvaluacionPsicologicaEmocionalDTO evaluacion =  EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologicaEmocional(idSolicitudServicio,tipoDominio,(long)evaluacionT.IdTarea);

                return evaluacion;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public VictimaPrincipalDTO ObtenerVictimaPrincipal(long idSolicitud)
        {
            try
            {
               return  EvaluacionPsicologicaRepository.ObtenerVictimaPrincipal(idSolicitud);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizaEvaluacionPsicologicaEntrevista(EvaluacionPsicologicaEntrevistaDTO data)
        {
            try
            {
                var evaluacionT = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(data.idSolicitudServicio, null);
                EvaluacionPsicologicaRepository.ActualizaEvaluacionPsicologicaEntrevista(data,(long)evaluacionT.IdTarea);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }

        public EvaluacionPsicologicaEntrevistaDTO ObtenerEvaluacionPsicologicaEntrevista(long idSolicitud) {
            try
            {
                var evaluacion = EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologica(idSolicitud, null);
                return EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologicaEntrevista(idSolicitud , (long)evaluacion.IdTarea);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }


        }

        public  InvolucradosReporte13DTO Reporte13(long idSolicitudServicio)
        {
            try 
            {
                var involucrados =  EvaluacionPsicologicaRepository.ReporteHU13(idSolicitudServicio);

                return involucrados;
            
            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }


        }



        //public List<EvaluacionPsicologicaReporte17DTO> ObtenerEvaluacionPsicologicaReporte17(long idSolicitud, string tipoDoc)
        //{
        //    try
        //    {
        //        return EvaluacionPsicologicaRepository.ObtenerEvaluacionPsicologicaReporte17(idSolicitud, tipoDoc);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);

        //    }


        //}



    }
}
