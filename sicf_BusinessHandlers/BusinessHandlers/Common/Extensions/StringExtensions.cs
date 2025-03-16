namespace sicf_BusinessHandlers.BusinessHandlers.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToDataUri(this string value, string mimeType)
        {
            return $"data:{mimeType};base64,{value}";
        }
    }
}
