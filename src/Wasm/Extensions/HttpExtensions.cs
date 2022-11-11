using System.Net;

namespace Gbs.Client.Wasm.Extensions;

public static class HttpExtensions
{
    public static async Task<Result<T>> EnsureSuccess<T>(this Task<HttpResponseMessage> responseTask)
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
        }
        catch (Exception)
        {
            if (response == null)
            {
                return Result.NotFound<T>("No response from server");
            }

            return await HandleHttpError<T>(response);
        }
    }

    private static async Task<Result<T>> ReadFromJson<T>(HttpResponseMessage response)
    {
        try
        {
            var result = await response.Content.ReadFromJsonAsync<Result<T>>();
            if (result == null || result.Data == null)
            {
                return Result.NotFound<T>("No response from server");
            }

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static async Task<Result<T>> HandleHttpError<T>(HttpResponseMessage response)
    {
        // var result = new Result<T> {Success = false};

        switch (response.StatusCode)
        {
            case HttpStatusCode.InternalServerError:
                return Result.InternalError<T>();
            case HttpStatusCode.Forbidden:
                return Result.Forbidden<T>();
            case HttpStatusCode.Unauthorized:
                return Result.Unauthorized<T>();
        }

        var responseData = await response.Content.ReadFromJsonAsync<Result<T>>();
        return responseData ?? Result.NotFound<T>("Something went wrong");
    }
}