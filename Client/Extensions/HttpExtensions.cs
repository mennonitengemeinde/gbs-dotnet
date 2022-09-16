using System.Net;

namespace gbs.Client.Extensions;

public static class HttpExtensions
{
    public static async Task<ServiceResponse<T>> EnsureSuccess<T>(this Task<HttpResponseMessage> responseTask)
    {
        HttpResponseMessage response = await responseTask;
        try
        {
            if (response.IsSuccessStatusCode == false)
            {
                return await HandleHttpError<T>(response);
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
            if (result == null || result.Data == null)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = "No response from server"
                };
            }

            return result;
        }
        catch (Exception e)
        {
            return await HandleHttpError<T>(response);
        }
    }

    private static async Task<ServiceResponse<T>> HandleHttpError<T>(HttpResponseMessage response)
    {
        var result = new ServiceResponse<T> {Success = false};

        switch (response.StatusCode)
        {
            case HttpStatusCode.InternalServerError:
                result.Message = "Internal server error";
                return result;
            case HttpStatusCode.Unauthorized:
                result.Message = "Unauthorized";
                return result;
        }

        var responseData = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
        result.Message = responseData?.Message ?? "Something went wrong";
        return result;
    }
}