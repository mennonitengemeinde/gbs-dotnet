namespace Gbs.Domain.Common.Const;

public static class Roles
{
    public const string Admin = "Admin";
    public const string SuperAdmin = "Super Admin";
    public const string Teacher = "Teacher";
    public const string ChurchLeader = "Church Leader";
    public const string ChurchTeacher = "Church Teacher";
    public const string Sound = "Sound";
    public const string Student = "Student";
    
    public static readonly string[] AllRoles = { Admin, SuperAdmin, Teacher, ChurchLeader, ChurchTeacher, Sound, Student };
}