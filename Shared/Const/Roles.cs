namespace gbs.Shared.Const;

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
    
    // public const string Admin = "Admin";
    // public const string SuperAdmin = "Super Admin";
    // public const string Teacher = "Teacher";
    // public const string ChurchLeader = "Church Leader";
    // public const string ChurchTeacher = "Church Teacher";
    // public const string Sound = "Sound";
    // public const string User = "User";

    // public const string AllButUser = $"{Admin}, {SuperAdmin}, {Teacher}, {ChurchLeader}, {ChurchTeacher}, {Sound}";
    // public const string AdminAndTeachers = $"{Admin}, {SuperAdmin}, {Teacher}, {ChurchLeader}, {ChurchTeacher}";
    // public const string Admins = $"{Admin}, {SuperAdmin}";
    // public const string AdminAndSound = $"{Admin}, {SuperAdmin}, {Sound}";
    //
    // public static readonly string[] AllRoles = { Admin, SuperAdmin, Teacher, ChurchLeader, ChurchTeacher, Sound, User };
}