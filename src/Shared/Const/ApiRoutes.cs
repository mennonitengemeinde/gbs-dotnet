﻿namespace Gbs.Shared.Const;

public static class SubjectRoutes
{
    public const string GetAll = "api/subjects";
    public static string Get(int id) => $"api/subjects/{id}";
    public const string Create = "api/subjects";
}