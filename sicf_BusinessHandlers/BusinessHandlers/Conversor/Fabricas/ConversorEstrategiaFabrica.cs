using ErrorOr;
using sicf_BusinessHandlers.BusinessHandlers.Conversor.Estrategias;
using System.Diagnostics;
using System.Linq.Expressions;

namespace sicf_BusinessHandlers.BusinessHandlers.Conversor.Fabricas
{
    internal class ConversorEstrategiaFabrica
    {
        private readonly IDictionary<string, IConversionEstrategia> estrategias = new Dictionary<string, IConversionEstrategia>()
        {
            { "PDF_DOC", new PdfDocEstrategia() }
        };

        public async Task<ErrorOr<Expression<Func<string, Task<Process>>>>> ObtenerEstrategia(string form, string to)
        {
            try
            {
                await Task.Delay(0);

                var key = $"{form}_{to}".ToUpper();
                IConversionEstrategia conversor = estrategias[key];

                Expression<Func<string, Task<Process>>> handlerExpression = (filePath) => conversor.Handler(filePath);
                return handlerExpression;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
