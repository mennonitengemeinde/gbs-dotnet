namespace Gbs.Wasm.Store;

public class GenerationStore : BaseStore<GenerationDto, GenerationCreateDto, GenerationCreateDto>, IGenerationStore
{
    public GenerationStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/generations";

    public override GenerationDto? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
}