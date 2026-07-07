using System.Linq.Expressions;
using ConstructionExpenseManager.Api.Models;

namespace ConstructionExpenseManager.Api.Repositories;

public interface IGstVendorEntryRepository : IRepository<GstVendorEntry>
{
    Task<List<GstVendorEntry>> GetForWorkAsync(GstWorkType workType, int workId);
    Task<GstVendorEntry?> GetByIdWithRepaymentsAsync(int id);
    Task<List<GstVendorEntry>> FindWithRepaymentsAsync(Expression<Func<GstVendorEntry, bool>> predicate);
}
