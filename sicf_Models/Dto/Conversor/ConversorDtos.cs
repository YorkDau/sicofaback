namespace sicf_Models.Dto.Conversor
{
    public record ConversorRequest(string From, string To, string Base64);
    public record ConversorResponse(string DataUri);
}
