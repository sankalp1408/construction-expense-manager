using ConstructionExpenseManager.Api.Data;
using ConstructionExpenseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionExpenseManager.Api.Repositories;

public class TenderWorkRepository : Repository<TenderWork>, ITenderWorkRepository
{
    public TenderWorkRepository(AppDbContext db) : base(db) { }

    public async Task<TenderWork?> GetWithRaBillsAsync(int id) =>
        await DbSet.Include(t => t.RaBills).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<List<TenderWork>> GetAllWithRaBillsAsync() =>
        await DbSet.Include(t => t.RaBills).OrderByDescending(t => t.Id).ToListAsync();
}
