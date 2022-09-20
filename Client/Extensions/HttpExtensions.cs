using System.Net;

namespace gbs.Client.Extensions;

public static class HttpExtensions
{
    public static async Task<ServiceResponse<T>> EnsureSuccess<T>(this Task<HttpResponseMessage> responseTask)
    {
        HttpResponseMessage? response = null;
        try
        {
            response = await responseTask;
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
        catch (Exception)
        {
            if (response == null)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = "No response from server"
                };
            }
            Console.WriteLine("HandleError: " + response.StatusCode);
            return await HandleHttpError<T>(response);
        }
    }

    // public static async Task HandleErrors<T>(this Task<HttpResponseMessage> task)
    // {
    //     try
    //     {
    //         await task;
    //     }
    //     catch (HttpRequestException ex)
    //     {
    //         return CreateResponse
    //     }
    // }

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

        Console.WriteLine("HandleHttpError: " + response.StatusCode);

        var responseData = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
        result.Message = responseData?.Message ?? "Something went wrong";
        return result;
    }
}