using Gbs.Shared.Identity;

namespace Gbs.Wasm.Store;

public class UserStore : BaseStore<UserResponse, string, RegisterRequest, RegisterRequest>, IUserStore
{
    public UserStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) :
        base(http, dateTime, uiService) { }

    public override string BaseUrl { get; } = "api/users";
    
    public override UserResponse? GetByIdQuery(string id) => Data.FirstOrDefault(u => u.Id == id);

    public override async Task Add(RegisterRequest item)
    {
        IsLoading = true;
        var result = await Http.PostAsJsonAsync(BaseUrl, item)
            .EnsureSuccess<string>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            SetErrors(result.Message, result.Errors);
            IsLoading = false;
        }
    }
    
    public async Task UpdateChurch(string id, UpdateUserChurchRequest request)
    {
        var result = await Http
            .PutAsJsonAsync($"api/users/{id}/church", request)
            .EnsureSuccess<UserResponse>();
        
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            SetErrors(result.Message, result.Errors);
            return;
        }

        await ForceFetch();
    }

    public async Task UpdateRoles(string id, UpdateUserRoleRequest request)
    {
        var result = await Http.PutAsJsonAsync($"api/users/{id}/roles", request)
            .EnsureSuccess<UserResponse>();
        
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            SetErrors(result.Message, result.Errors);
            return;
        }

        await ForceFetch();
    }

    public async Task UpdateActiveState(string id, UpdateUserActiveStateRequest request)
    {
        var result = await Http.PutAsJsonAsync($"api/users/{id}/active", request)
            .EnsureSuccess<UserResponse>();
        
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            SetErrors(result.Message, result.Errors);
            return;
        }

        await ForceFetch();
    }
}