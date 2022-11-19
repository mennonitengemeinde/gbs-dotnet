﻿namespace Gbs.Wasm.Store;

public class LessonStore : BaseStore<LessonDto, LessonCreateDto, LessonCreateDto>, ILessonStore
{
    public LessonStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/lessons";

    public override LessonDto? GetByIdQuery(int id) => Value.FirstOrDefault(l => l.Id == id);
}