using sicf_DataBase.Repositories.Filter;

namespace sicf_BusinessHandlers.BusinessHandlers.Filter
{
    public class FilterService : IFilterService
    {

        private IFilterRepository _filterRepository;

        public FilterService(IFilterRepository filterRepository) {

            _filterRepository = filterRepository;


        }

        public async Task<bool> ValidarPermiso(string email, string perfil)
        {
            try
            {
                var response =await _filterRepository.ValidarPermiso(email , perfil);

                return response;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        
        }
    }
}
