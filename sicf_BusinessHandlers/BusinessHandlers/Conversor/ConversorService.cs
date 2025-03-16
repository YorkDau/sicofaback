using ErrorOr;
using sicf_BusinessHandlers.BusinessHandlers.Conversor.Fachadas;
using sicf_Models.Dto.Conversor;

namespace sicf_BusinessHandlers.BusinessHandlers.Conversor
{
    public class ConversorService : IConversorService
    {
        private readonly ConversorProcesoFachada _fachada;

        public ConversorService(ConversorProcesoFachada fachada)
        {
            _fachada = fachada;
        }

        public async Task<ErrorOr<ConversorResponse>> Handler(ConversorRequest request)
        {
            return await _fachada.Handler(request);
        }
    }
}
