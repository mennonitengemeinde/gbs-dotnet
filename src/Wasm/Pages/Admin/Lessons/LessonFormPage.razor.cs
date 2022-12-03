using Gbs.Shared.Lessons;

namespace Gbs.Wasm.Pages.Admin.Lessons;

public partial class LessonFormPage : ComponentBase, IDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IGenerationStore GenerationStore { get; set; } = null!;
    [Inject] public ILessonStore LessonStore { get; set; } = null!;
    [Inject] public ISubjectStore SubjectStore { get; set; } = null!;
    [Inject] public ITeacherStore TeacherStore { get; set; } = null!;
    [Inject] public IUiService UiService { get; set; } = null!;

    [CascadingParameter] CascadingUiState UiState { get; set; } = null!;
    [Parameter] public int? Id { get; set; }

    LessonCreateDto _model = new();
    bool _isEdit;

    protected override async Task OnInitializedAsync()
    {
        await Task.WhenAll(SubjectStore.Fetch(), GenerationStore.Fetch(), TeacherStore.Fetch());

        if (Id.HasValue)
        {
            _isEdit = true;
            var lesson = await LessonStore.GetById(Id.Value);
            if (lesson != null)
            {
                _model = new LessonCreateDto
                {
                    Name = lesson.Name,
                    VideoUrl = lesson.VideoUrl,
                    IsVisible = lesson.IsVisible,
                    GenerationId = lesson.GenerationId,
                    SubjectId = lesson.SubjectId,
                    TeacherId = lesson.TeacherId
                };
            }
        }

        SubjectStore.OnChange += StateHasChanged;
        GenerationStore.OnChange += StateHasChanged;
        LessonStore.OnChange += StateHasChanged;
        TeacherStore.OnChange += StateHasChanged;
    }

    async Task HandleValidSubmit()
    {
        UiState.IsPageLoading = true;
        if (_isEdit)
        {
            await LessonStore.Update(Id!.Value, _model);
        }
        else
        {
            await LessonStore.Add(_model);
        }

        if (!LessonStore.HasError)
        {
            UiState.IsPageLoading = false;
            UiService.ShowSuccessAlert("Lesson added successfully");
            NavigationManager.NavigateTo("admin/lessons");
        }

        UiState.IsPageLoading = false;
    }

    public void Dispose()
    {
        SubjectStore.OnChange -= StateHasChanged;
        GenerationStore.OnChange -= StateHasChanged;
        LessonStore.OnChange -= StateHasChanged;
        TeacherStore.OnChange -= StateHasChanged;
    }
}