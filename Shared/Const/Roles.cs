﻿namespace gbs.Shared.Const;

public static class Roles
{
    public const string Admin = "Admin";
    public const string SuperAdmin = "SuperAdmin";
    public const string Teacher = "Teacher";
    public const string Sound = "Sound";
    public const string User = "User";
    
    public static string[] AllRoles = new string[] { Admin, SuperAdmin, Teacher, Sound, User };
}