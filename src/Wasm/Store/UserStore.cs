using Gbs.Shared.Identity;

namespace Gbs.Wasm.Store;

public class UserStore : BaseStore<UserDto, string, RegisterDto, RegisterDto>, IUserStore
{
    public UserStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) :
        base(http, dateTime, uiService) { }

    public override string BaseUrl { get; } = "api/users";
    
    public override UserDto? GetByIdQuery(string id) => Data.FirstOrDefault(u => u.Id == id);

    public override async Task Add(RegisterDto item)
    {
        IsLoading = true;
        var result = await Http.PostAsJsonAsync(BaseUrl, item)
            .EnsureSuccess<string>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            IsLoading = false;
        }
    }
    
    public async Task UpdateChurch(string id, UserUpdateChurchDto request)
    {
        var result = await Http
            .PutAsJsonAsync($"api/users/{id}/church", request)
            .EnsureSuccess<UserDto>();
        
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            return;
        }

        await ForceFetch();
    }

    public async Task UpdateRoles(string id, UserUpdateRoleDto request)
    {
        var result = await Http.PutAsJsonAsync($"api/users/{id}/roles", request)
            .EnsureSuccess<UserDto>();
        
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            return;
        }

        await ForceFetch();
    }

    public async Task UpdateActiveState(string id, UserUpdateActiveStateDto request)
    {
        var result = await Http.PutAsJsonAsync($"api/users/{id}/active", request)
            .EnsureSuccess<UserDto>();
        
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            return;
        }

        await ForceFetch();
    }
}