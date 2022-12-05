using Gbs.Shared.Churches;

namespace Gbs.Wasm.Store;

public class ChurchStore : BaseStore<ChurchDto, int, CreateChurchRequest, CreateChurchRequest>, IChurchStore
{
    public ChurchStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/churches";

    public override ChurchDto? GetByIdQuery(int id) => Data.FirstOrDefault(c => c.Id == id);
}