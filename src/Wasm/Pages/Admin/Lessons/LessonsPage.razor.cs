using Gbs.Shared.Lessons;

namespace Gbs.Wasm.Pages.Admin.Lessons;

public partial class LessonsPage : ComponentBase, IDisposable
{
    [Inject] public ILessonStore LessonStore { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        await LessonStore.ForceFetch();
        LessonStore.OnChange += StateHasChanged;
    }
    
    private async Task MoveLessonUp(LessonDto lesson)
    {
        await LessonStore.UpdateOrder(lesson.Id, lesson.Order - 1);
    }
    
    private async Task MoveLessonDown(LessonDto lesson)
    {
        await LessonStore.UpdateOrder(lesson.Id, lesson.Order + 1);
    }

    public void Dispose()
    {
        LessonStore.OnChange -= StateHasChanged;
    }
}