using Gbs.Application.Entities;
using Gbs.Shared.Common.Enums;

namespace Gbs.Tests.Infrastructure.Seeds;

public static class LessonSeed
{
    public static List<Lesson> GetLessons()
    {
        return new List<Lesson>
        {
            new()
            {
                Id = 1,
                Name = "Lesson 1",
                Order = 1,
                GenerationId = 1,
                SubjectId = 1,
                TeacherId = 1,
                IsVisible = Visibility.Visible,
                VideoUrl = "https://www.youtube.com/embed/1"
            },
            new()
            {
                Id = 2,
                Name = "Lesson 2",
                Order = 2,
                GenerationId = 1,
                SubjectId = 1,
                TeacherId = 1,
                IsVisible = Visibility.Visible,
                VideoUrl = "https://www.youtube.com/embed/2"
            },
            new()
            {
                Id = 3,
                Name = "Lesson 3",
                Order = 3,
                GenerationId = 1,
                SubjectId = 1,
                TeacherId = 1,
                IsVisible = Visibility.Visible,
                VideoUrl = "https://www.youtube.com/embed/3"
            },
            new()
            {
                Id = 4,
                Name = "Lesson 4",
                Order = 4,
                GenerationId = 1,
                SubjectId = 1,
                TeacherId = 1,
                IsVisible = Visibility.Private,
                VideoUrl = "https://www.youtube.com/embed/4"
            },
            new()
            {
                Id = 5,
                Name = "Lesson 5",
                Order = 5,
                GenerationId = 1,
                SubjectId = 1,
                TeacherId = 1,
                IsVisible = Visibility.Hidden,
                VideoUrl = "https://www.youtube.com/embed/5"
            }
        };
    }
}