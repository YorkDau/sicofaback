namespace sicfServicesApi.Utility
{
    public interface IValidarAcceso
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCiudadano"></param>
        /// <param name="codPefil"></param>
        /// <param name="codActividad"></param>
        /// <param name="componente"></param>
        /// <returns></returns>
        public bool GetPermiso(long userID, string codPefil, string codActividad, string componente);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="codPefil"></param>
        /// <returns></returns>
        public bool IsUserPerfil(long userID, string codPefil);
    }
}
