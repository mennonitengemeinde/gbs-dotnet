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

            return await ReadFromJson<T>(response);

            // var result = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
            // if (result == null || result.Data == null)
            // {
            //     return new ServiceResponse<T>
            //     {
            //         Success = false,
            //         Message = "No response from server",
            //         StatusCode = 404
            //     };
            // }
            //
            // return result;
        }
        catch (Exception)
        {
            if (response == null)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = "No response from server",
                    StatusCode = 404
                };
            }
            return await HandleHttpError<T>(response);
        }
    }

    private static async Task<ServiceResponse<T>> ReadFromJson<T>(HttpResponseMessage response)
    {
        try
        {
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
            if (result == null || result.Data == null)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = "No response from server",
                    StatusCode = 404
                };
            }

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static async Task<ServiceResponse<T>> HandleHttpError<T>(HttpResponseMessage response)
    {
        var result = new ServiceResponse<T> {Success = false};

        switch (response.StatusCode)
        {
            case HttpStatusCode.InternalServerError:
                result.Message = "Internal server error";
                result.StatusCode = 500;
                return result;
            case HttpStatusCode.Unauthorized:
                result.Message = "Unauthorized";
                result.StatusCode = 401;
                return result;
        }

        var responseData = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
        result.Message = responseData?.Message ?? "Something went wrong";
        return result;
    }
}