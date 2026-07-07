using ConstructionExpenseManager.Api.Models;

namespace ConstructionExpenseManager.Api.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByMobileNumberAsync(string mobileNumber);
}
