﻿using Gbs.Shared.Common.Enums;
using Gbs.Shared.Grades;

namespace Gbs.Shared.Students;

public class StudentResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? PostalCode { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public EnrollmentState EnrollmentStatus { get; set; }
    public string? Testimony { get; set; }
    public bool AgreedToGbsConcept { get; set; } = false;
    public int ChurchId { get; set; }
    public string ChurchName { get; set; } = string.Empty;
    public int GenerationId { get; set; }
    public string GenerationName { get; set; } = string.Empty;

    public string? UserId { get; set; }


    public IEnumerable<GradeResponse> Grades { get; set; } = new List<GradeResponse>();
}