using Gbs.Shared.Generations;

namespace Gbs.Wasm.Store;

public class GenerationStore : BaseStore<GenerationResponse, int, CreateGenerationRequest, UpdateGenerationRequest>, IGenerationStore
{
    public GenerationStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/generations";

    public override GenerationResponse? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
}