﻿namespace Gbs.Wasm.Services.Api;

public abstract class BaseApiCrud<T, TCreate, TUpdate, TId> : BaseApiService<T>, IApiCrud<TCreate, TUpdate, TId>
{
    protected readonly HttpClient Http;

    protected BaseApiCrud(IDateTimeService dateTimeService, IUiService uiService, HttpClient http) : base(
        dateTimeService, uiService)
    {
        Http = http;
    }

    public abstract string BaseUrl { get; }
    public int CacheTime => 5;

    public async Task Fetch(ComponentBase sender)
    {
        if (DateTimeService.UtcNow - LastUpdated > TimeSpan.FromMinutes(CacheTime) || Data.Count == 0)
            await ForceFetch(sender);
    }

    public async Task ForceFetch(ComponentBase sender)
    {
        SetLoading(sender, true);
        var result = await Http.GetAsync(BaseUrl)
            .EnsureSuccess<List<T>>();
        
        if (result.Success == false || result.Data == null)
        {
            await SetState(sender, new(), false,
                new ServiceError(result.Message, result.Errors, result.StatusCode));
            return;
        }

        await SetState(sender, result.Data);
    }

    public async Task Create(ComponentBase sender, TCreate request)
    {
        SetLoading(sender, true);
        var result = await Http.PostAsJsonAsync(BaseUrl, request)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await HandleError(sender, result);
            return;
        }

        SetLoading(sender, false);
        // await ForceFetch(sender);
    }

    public async Task Update(ComponentBase sender, TId id, TUpdate request)
    {
        SetLoading(sender, true);
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}", request)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await HandleError(sender, result);
            return;
        }
        SetLoading(sender, false);

        // await ForceFetch(sender);
    }

    public async Task Delete(ComponentBase sender, TId id)
    {
        SetLoading(sender, true);
        var result = await Http.DeleteAsync($"{BaseUrl}/{id}")
            .EnsureSuccess<bool>();
        if (!result.Success)
        {
            await HandleError(sender, result);
            return;
        }
        SetLoading(sender, false);

        // await ForceFetch(sender);
    }
    
    protected async Task HandleError(ComponentBase sender, Result result)
    {
        await SetError(sender, new ServiceError(result.Message, result.Errors, result.StatusCode), false);
        SetLoading(sender, false);
    }
}