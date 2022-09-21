namespace gbs.Shared.Const;

public static class Roles
{
    public const string Admin = "Admin";
    public const string SuperAdmin = "SuperAdmin";
    public const string Teacher = "Teacher";
    public const string ChurchLeader = "ChurchLeader";
    public const string ChurchTeacher = "ChurchTeacher";
    public const string Sound = "Sound";
    public const string User = "User";

    public static string[] AllRoles = new string[]
        { Admin, SuperAdmin, Teacher, ChurchLeader, ChurchTeacher, Sound, User };
}