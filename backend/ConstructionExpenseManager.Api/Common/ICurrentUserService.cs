namespace ConstructionExpenseManager.Api.Common;

public interface ICurrentUserService
{
    int UserId { get; }
    string Role { get; }
    bool IsSuperAdmin { get; }
}
