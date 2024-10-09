using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.Ciudadano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.TestEntity
{
    public class CiudadanoRepository : BaseRepository<SicofaCiudadano> , ICiudadanoRepository
    {
        private readonly SICOFAContext context;
        public CiudadanoRepository(SICOFAContext context) : base(context){

            this.context = context;
        }

        public async Task<SicofaCiudadano> BuscarCiudadano(long id) {

            var SicofaCiudadano =await  context.SicofaCiudadano.Where(s => s.IdCiudadano == id).FirstAsync();

            return SicofaCiudadano;
        }

        public async Task<CiudadanoEFDTO> BuscarCiudadanoid(long id) {

            var CiudadanoEFDTO =await  (from ciudadano in context.SicofaCiudadano
                                  join tipodocumento in context.SicofaDominio on ciudadano.IdTipoDocumento equals tipodocumento.IdDominio
                                  where ciudadano.IdCiudadano == id
                                  select new CiudadanoEFDTO
                                  {
                                      IdCiudadano = ciudadano.IdCiudadano,
                                      NombreCiudadano = ciudadano.NombreCiudadano,
                                      PrimerApellido = ciudadano.PrimerApellido,
                                      SegundoApellido = ciudadano.SegundoApellido,
                                      tipoDocumento = tipodocumento.NombreDominio,
                                      celular = ciudadano.Celular,
                                      telefonoFijo = ciudadano.TelefonoFijo,
                                      edad = ciudadano.Edad,
                                      correoElectronico = ciudadano.CorreoElectronico,
                                      fechaNacimiento = ciudadano.FechaNacimiento,
                                      numeroDocumento = ciudadano.NumeroDocumento,
                                      registroCompleto = ciudadano.RequiereModificacion


                                  }


                                  ).FirstAsync();

            return CiudadanoEFDTO;


        }

        public async Task<InvolucradoDTO> ObtenerVictimaPrincipal(long id)
        {

            string documento = await context.SicofaCiudadano.Where(s => s.IdCiudadano == id).Select(s => s.NumeroDocumento).FirstOrDefaultAsync();

            var CiudadanoEFDTO = await (from involucrado in context.SicofaInvolucrado

                                        where involucrado.NumeroDocumento == documento
                                        select new InvolucradoDTO
                                        {
                                            IdInvolucrado = involucrado.IdInvolucrado,
                                            Localidad = involucrado.Localidad,
                                            NumeroDocumento = involucrado.NumeroDocumento,
                                            TipoDocumento = involucrado.TipoDocumento,
                                            Nombres = involucrado.Nombres,
                                            Apellidos = involucrado.Apellidos,
                                            FechaNacimiento = involucrado.FechaNacimiento,
                                            Edad = involucrado.Edad,
                                            idGenero = involucrado.IdGenero,
                                            Telefono = involucrado.Telefono,
                                            CorreoElectronico = involucrado.CorreoElectronico,
                                            Barrio = involucrado.Barrio,
                                            EsPrincipal = involucrado.EsPrincipal,
                                            EsVictima = involucrado.EsVictima,
                                            EstadoEmbarazo = involucrado.EstadoEmbarazo,
                                            AfiliadoSeguridadSocial = involucrado.AfiliadoSeguridadSocial,
                                            DireccionRecidencia= involucrado.DireccionRecidencia,
                                            IdTipoDiscpacidad = involucrado.IdTipoDiscpacidad,
                                            Eps = involucrado.Eps,
                                            Ips = involucrado.Ips

                                            



                                        }


                                  ).FirstAsync();
            return CiudadanoEFDTO;


        }

    }
}
