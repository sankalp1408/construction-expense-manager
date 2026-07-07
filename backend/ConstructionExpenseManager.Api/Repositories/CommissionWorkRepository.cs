using ConstructionExpenseManager.Api.Data;
using ConstructionExpenseManager.Api.Models;

namespace ConstructionExpenseManager.Api.Repositories;

public class CommissionWorkRepository : Repository<CommissionWork>, ICommissionWorkRepository
{
    public CommissionWorkRepository(AppDbContext db) : base(db) { }
}
