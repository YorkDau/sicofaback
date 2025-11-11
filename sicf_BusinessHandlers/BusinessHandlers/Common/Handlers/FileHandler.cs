namespace sicf_BusinessHandlers.BusinessHandlers.Common.Handlers
{
    public class FileHandler
    {
        public async Task<Stream> Get(string uriResource)
        {
            MemoryStream? ms = new();

            using (FileStream? stream = File.Open(uriResource, FileMode.Open, FileAccess.Read))
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                stream.Position = 0;
                await stream.CopyToAsync(ms);
            }

            return ms;
        }

        public async Task Save(Stream file, string uriResource)
        {
            using (FileStream? stream = File.Create(uriResource))
            {
                await file.CopyToAsync(stream);
            }
        }

        public void Delete(string uriResource)
        {
            bool exist = File.Exists(uriResource);
            if (exist)
            {
                File.Delete(uriResource);
            }
        }
    }
}
