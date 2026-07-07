using System.Linq.Expressions;
using ConstructionExpenseManager.Api.Data;
using ConstructionExpenseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionExpenseManager.Api.Repositories;

public class GstVendorEntryRepository : Repository<GstVendorEntry>, IGstVendorEntryRepository
{
    public GstVendorEntryRepository(AppDbContext db) : base(db) { }

    public async Task<List<GstVendorEntry>> GetForWorkAsync(GstWorkType workType, int workId) =>
        await DbSet.Include(g => g.Repayments)
            .Where(g => g.WorkType == workType && g.WorkId == workId)
            .OrderByDescending(g => g.SentDate)
            .ToListAsync();

    public async Task<GstVendorEntry?> GetByIdWithRepaymentsAsync(int id) =>
        await DbSet.Include(g => g.Repayments).FirstOrDefaultAsync(g => g.Id == id);

    public async Task<List<GstVendorEntry>> FindWithRepaymentsAsync(Expression<Func<GstVendorEntry, bool>> predicate) =>
        await DbSet.Include(g => g.Repayments).Where(predicate).ToListAsync();
}
