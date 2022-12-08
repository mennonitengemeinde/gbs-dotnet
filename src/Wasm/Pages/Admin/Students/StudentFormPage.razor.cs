using Gbs.Shared.Common.Extensions;
using Gbs.Shared.Students;

namespace Gbs.Wasm.Pages.Admin.Students;

public partial class StudentFormPage
{
    [Inject] public IStudentStore StudentStore { get; set; } = null!;
    [Inject] public IGenerationStore GenerationStore { get; set; } = null!;
    [Inject] public IChurchStore ChurchStore { get; set; } = null!;
    [Inject] public IAuthService AuthService { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter] private CascadingUiState UiState { get; set; } = null!;
    [Parameter] public int? Id { get; set; }

    private CreateStudentRequest _model = new();
    private bool _isProcessing;
    private bool _isAdmin;
    private bool _isEdit;

    protected override async Task OnInitializedAsync()
    {
        var tasks = new[]
        {
            GenerationStore.Fetch(),
            ChurchStore.Fetch(),
        };
        _isAdmin = await AuthService.UserIsAdmin();

        await Task.WhenAll(tasks);

        if (Id.HasValue)
        {
            _isEdit = true;
            var student = await StudentStore.GetById(Id.Value);
            if (student == null)
            {
                NavigationManager.NavigateTo("/admin/students");
                return;
            }

            _model = new CreateStudentRequest
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                MaritalStatus = student.MaritalStatus,
                Address = student.Address,
                City = student.City,
                Province = student.State,
                Country = student.Country,
                PostalCode = student.PostalCode,
                Email = student.Email,
                Phone = student.Phone,
                ChurchId = student.ChurchId
            };
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        UiState.IsPageLoading = StudentStore.IsLoading;
    }

    private async Task HandleValidSubmit()
    {
        _isProcessing = true;
        _model.FirstName = _model.FirstName.CapitalizeFirstLetterOfEachWord();
        _model.LastName = _model.LastName.CapitalizeFirstLetterOfEachWord();
        if (_isEdit && Id.HasValue)
        {
            await StudentStore.Update(Id.Value, _model);
        }
        else
        {
            await StudentStore.Add(_model);
        }

        _isProcessing = false;

        if (!StudentStore.HasError)
        {
            NavigationManager.NavigateTo("admin/students");
        }
        else
        {
            StudentStore.ClearErrors();
        }
    }
}