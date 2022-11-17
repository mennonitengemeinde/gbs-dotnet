namespace Gbs.Wasm.Store;

public class GenerationStore : BaseStore<GenerationDto, GenerationCreateDto>
{
    public GenerationStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime, uiService) { }
    
    public override string BaseUrl { get; } = "api/generations";
    public override event Action? OnChange;

    public override List<GenerationDto> Value
    {
        get => Values;
        protected set
        {
            Values = value;
            OnChange?.Invoke();
            LastUpdated = DateTimeService.UtcNow;
        }
    }

    public override GenerationDto? GetByIdQuery(int id) => Value.FirstOrDefault(x => x.Id == id);
}