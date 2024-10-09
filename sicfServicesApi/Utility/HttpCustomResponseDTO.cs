namespace sicfServicesApi.Utility
{
    public class HttpCustomResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
