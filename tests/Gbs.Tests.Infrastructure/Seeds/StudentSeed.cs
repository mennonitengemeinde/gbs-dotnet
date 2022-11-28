namespace Gbs.Tests.Infrastructure.Seeds;

public static class StudentSeed
{
    public static List<Student> GetStudents()
    {
        return new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "Student 1",
                DateOfBirth = new DateTime(2000, 1, 1),
                Address = "Address 1",
                City = "City 1",
                State = "State 1",
                Country = "Country 1",
                PostalCode = "PostalCode 1",
                MaritalStatus = MaritalStatus.Married,
                Email = "student1@email.com",
                Phone = "1233211231",
                ChurchId = 1
            },
            new Student
            {
                Id = 2,
                Name = "Student 2",
                DateOfBirth = new DateTime(2000, 1, 1),
                Address = "Address 2",
                City = "City 2",
                State = "State 2",
                Country = "Country 2",
                PostalCode = "PostalCode 2",
                MaritalStatus = MaritalStatus.Single,
                Email = "student2@email.com",
                Phone = "1233211232",
                ChurchId = 1
            },
            new Student
            {
                Id = 3,
                Name = "Student 3",
                DateOfBirth = new DateTime(2000, 1, 1),
                Address = "Address 3",
                City = "City 3",
                State = "State 3",
                Country = "Country 3",
                PostalCode = "PostalCode 3",
                MaritalStatus = MaritalStatus.Married,
                Email = "student3@email.com",
                Phone = "1233211233",
                ChurchId = 2
            }
        };
    }
}