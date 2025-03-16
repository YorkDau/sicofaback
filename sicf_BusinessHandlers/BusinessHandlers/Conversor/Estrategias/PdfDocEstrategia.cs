using System.Diagnostics;

namespace sicf_BusinessHandlers.BusinessHandlers.Conversor.Estrategias
{
    internal class PdfDocEstrategia : IConversionEstrategia
    {
        private string GetArguments(string originFilePath, string convertedDirectory)
        {
            return $"--invisible --infilter=\"writer_pdf_import\" --convert-to doc:\"MS Word 97\" --outdir {convertedDirectory} {originFilePath}";
        }

        public async Task<Process> Handler(string originFilePath)
        {
            await Task.Delay(0);

            var original = Path.ChangeExtension(originFilePath, ".pdf");
            var convertedDirectory = Path.GetDirectoryName(originFilePath);
            var arguments = GetArguments(original, convertedDirectory);
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\LibreOffice\program\soffice.com",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = convertedDirectory
                }
            };
        }
    }
}
