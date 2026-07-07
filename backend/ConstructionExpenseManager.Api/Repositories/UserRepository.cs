using ConstructionExpenseManager.Api.Data;
using ConstructionExpenseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionExpenseManager.Api.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext db) : base(db) { }

    public async Task<User?> GetByMobileNumberAsync(string mobileNumber) =>
        await DbSet.FirstOrDefaultAsync(u => u.MobileNumber == mobileNumber);
}
