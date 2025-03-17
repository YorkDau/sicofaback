using ErrorOr;
using sicf_BusinessHandlers.BusinessHandlers.Common.Extensions;
using sicf_BusinessHandlers.BusinessHandlers.Common.Handlers;
using sicf_BusinessHandlers.BusinessHandlers.Conversor.Fabricas;
using sicf_Models.Dto.Conversor;
using System.Diagnostics;

namespace sicf_BusinessHandlers.BusinessHandlers.Conversor.Fachadas
{
    public class ConversorProcesoFachada
    {
        private readonly FileHandler _file;

        private readonly IDictionary<string, string> mimeTypes = new Dictionary<string, string>()
        {
            { "doc", "application/msword" }
        };

        public ConversorProcesoFachada(FileHandler file)
        {
            _file = file;
        }

        /// <summary>
        /// This method need LibreOffice installed in the server.
        /// For more information, visit <a href="https://www.libreoffice.org/get-help/install-howto/">LibreOffice Installation Guide</a>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ErrorOr<ConversorResponse>> Handler(ConversorRequest request)
        {
            var estrategia = await new ConversorEstrategiaFabrica().ObtenerEstrategia(request.From, request.To);
            if (estrategia.IsError)
            {
                return Error.Failure(description: estrategia.FirstError.Description);
            }

            var tempFileName = Path.ChangeExtension(Path.GetTempFileName(), request.From);

            byte[] bytes = Convert.FromBase64String(request.Base64);
            using (MemoryStream ms = new(bytes))
            {
                await _file.Save(ms, tempFileName);
            }

            var handler = estrategia.Value.Compile();
            Process process = await handler(tempFileName);

            process.Start();

            Stream converted = null!;
            var retryTimes = 3;
            var retryMilisecondsDelay = 10000;
            do
            {
                await Task.Delay(retryMilisecondsDelay);
                try
                {
                    converted = await _file.Get(Path.ChangeExtension(tempFileName, request.To));
                }
                catch { }
                retryTimes--;
            } while (converted is null && retryTimes > 0);

            if (converted is null)
            {
                return Error.Failure(description: $"El archivo no se ha posido convertir o ha superado el tiempo fuera configurado, ruta: {Path.GetTempFileName()}");
            }

            var msConverted = new MemoryStream();
            converted.Position = 0;
            await converted.CopyToAsync(msConverted);
            msConverted.Position = 0;

            var convertedFileBase64 = Convert.ToBase64String(msConverted.ToArray());
            var convertedFileDateUri = convertedFileBase64.ToDataUri(mimeTypes[request.To]);
            return new ConversorResponse(convertedFileDateUri);
        }
    }
}
