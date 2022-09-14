using System.Net;

namespace gbs.Client.Services.Api;

public class BaseApiService
{
    protected async Task<ServiceResponse<T>> EnsureSuccess<T>(HttpResponseMessage? response)
    {
        if (response == null)
        {
            return ServerNoResponse<T>();
        }

        try
        {
            if (response.IsSuccessStatusCode == false)
            {
                return await HandleHttpError<T>(response);
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
            if (result == null || result.Data == null)
            {
                return ServerNoResponse<T>();
            }

            return result;
        }
        catch (Exception e)
        {
            return HandleException<T>(e);
        }
    }

    private ServiceResponse<T> ServerNoResponse<T>()
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = "No response from server"
        };
    }

    private ServiceResponse<T> HandleException<T>(Exception e)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = "Something went wrong"
        };
    }

    private async Task<ServiceResponse<T>> HandleHttpError<T>(HttpResponseMessage response)
    {
        var result = new ServiceResponse<T> {Success = false};

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            result.Message = "Internal server error";
            return result;
        }
        
        var responseData = await response.Content.ReadFromJsonAsync<ServiceResponse<T>>();
        result.Message = responseData?.Message ?? "Something went wrong";
        return result;
    }
}