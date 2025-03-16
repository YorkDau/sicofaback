using System.Diagnostics;

namespace sicf_BusinessHandlers.BusinessHandlers.Conversor.Estrategias
{
    internal interface IConversionEstrategia
    {
        Task<Process> Handler(string originFilePath);
    }
}
