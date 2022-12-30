namespace Gbs.Tests.Application.UnitTests.Common;

public interface IQueryTests
{
    Task GetAll_ReturnsAllRecords();
    Task GetAll_ReturnsEmptyList_WhenNoRecords();
    Task GetById_ReturnsRecord();
    Task GetById_ReturnsNull_WhenNoRecord();
}