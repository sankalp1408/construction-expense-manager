using ConstructionExpenseManager.Api.Models;

namespace ConstructionExpenseManager.Api.Repositories;

public interface ITenderWorkRepository : IRepository<TenderWork>
{
    Task<TenderWork?> GetWithRaBillsAsync(int id);
    Task<List<TenderWork>> GetAllWithRaBillsAsync();
}
