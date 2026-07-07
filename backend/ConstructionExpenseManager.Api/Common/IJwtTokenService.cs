using ConstructionExpenseManager.Api.Models;

namespace ConstructionExpenseManager.Api.Common;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
