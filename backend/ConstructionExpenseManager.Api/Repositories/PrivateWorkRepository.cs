using ConstructionExpenseManager.Api.Data;
using ConstructionExpenseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionExpenseManager.Api.Repositories;

public class PrivateWorkRepository : Repository<PrivateWork>, IPrivateWorkRepository
{
    public PrivateWorkRepository(AppDbContext db) : base(db) { }

    private IQueryable<PrivateWork> WithDetails() =>
        DbSet
            .Include(p => p.Milestones)
            .Include(p => p.Categories).ThenInclude(c => c.Payments)
            .Include(p => p.Materials)
            .Include(p => p.DepartmentalLabours).ThenInclude(d => d.Rows);

    public async Task<PrivateWork?> GetWithDetailsAsync(int id) =>
        await WithDetails().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<PrivateWork>> GetAllWithDetailsAsync() =>
        await WithDetails().OrderByDescending(p => p.Id).ToListAsync();
}
