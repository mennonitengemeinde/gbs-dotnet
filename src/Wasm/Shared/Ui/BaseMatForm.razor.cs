namespace Gbs.Wasm.Shared.Ui;

public abstract class BaseMatForm : ComponentBase
{
    protected bool IsProcessing;
    protected string[]? ValidationErrors;
    [Inject] protected IUiService UiService { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public int? Id { get; set; }

    protected bool HasError(ServiceError? err)
    {
        if (err != null)
        {
            ValidationErrors = err.Errors;
            IsProcessing = false;
            return true;
        }

        UiService.ShowSuccessAlert(Id.HasValue ? "Updated successfully" : "Created successfully");
        IsProcessing = false;
        return false;
    }
}