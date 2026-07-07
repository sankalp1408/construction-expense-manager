using ConstructionExpenseManager.Api.Models;

namespace ConstructionExpenseManager.Api.Repositories;

public interface IPrivateWorkRepository : IRepository<PrivateWork>
{
    Task<PrivateWork?> GetWithDetailsAsync(int id);
    Task<List<PrivateWork>> GetAllWithDetailsAsync();
}
