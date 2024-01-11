using System.Net;

public static class HttpStatusCodeExtensions
{
    public static bool IsSuccessStatusCode(this HttpStatusCode httpStatusCode)
    {
        var statusCode = (int)httpStatusCode;
        return statusCode >= 200 && statusCode <= 299;
    }
}