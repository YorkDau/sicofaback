using ErrorOr;
using sicf_Models.Dto.Conversor;

namespace sicf_BusinessHandlers.BusinessHandlers.Conversor
{
    public interface IConversorService
    {
        Task<ErrorOr<ConversorResponse>> Handler(ConversorRequest request);
    }
}
