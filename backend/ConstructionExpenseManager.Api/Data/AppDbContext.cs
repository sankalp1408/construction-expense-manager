using ConstructionExpenseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionExpenseManager.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<TenderWork> TenderWorks => Set<TenderWork>();
    public DbSet<TenderRaBill> TenderRaBills => Set<TenderRaBill>();
    public DbSet<GstVendorEntry> GstVendorEntries => Set<GstVendorEntry>();
    public DbSet<GstVendorRepayment> GstVendorRepayments => Set<GstVendorRepayment>();

    public DbSet<CommissionWork> CommissionWorks => Set<CommissionWork>();

    public DbSet<PrivateWork> PrivateWorks => Set<PrivateWork>();
    public DbSet<PrivateWorkMilestone> PrivateWorkMilestones => Set<PrivateWorkMilestone>();
    public DbSet<PrivateWorkCategory> PrivateWorkCategories => Set<PrivateWorkCategory>();
    public DbSet<PrivateWorkCategoryPayment> PrivateWorkCategoryPayments => Set<PrivateWorkCategoryPayment>();
    public DbSet<PrivateWorkMaterial> PrivateWorkMaterials => Set<PrivateWorkMaterial>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ----- Users -----
        modelBuilder.Entity<User>(e =>
        {
            e.HasIndex(u => u.MobileNumber).IsUnique();
            e.Property(u => u.MobileNumber).HasMaxLength(15).IsRequired();
            e.Property(u => u.Name).HasMaxLength(120).IsRequired();
            e.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
        });

        // ----- Tender Works -----
        modelBuilder.Entity<TenderWork>(e =>
        {
            e.Property(t => t.TenderName).HasMaxLength(200).IsRequired();
            e.Property(t => t.NameOfWork).HasMaxLength(300).IsRequired();
            e.Property(t => t.CorporatorName).HasMaxLength(150);

            foreach (var money in new[]
            {
                nameof(TenderWork.TenderAmount), nameof(TenderWork.TenderFee), nameof(TenderWork.TenderEMD),
                nameof(TenderWork.TenderFilingAmount), nameof(TenderWork.GstTotal), nameof(TenderWork.BilledGst),
                nameof(TenderWork.ExtraGstBill), nameof(TenderWork.WorkExpenditure),
                nameof(TenderWork.GstBillCommission)
            })
            {
                e.Property(money).HasColumnType("decimal(14,2)");
            }

            foreach (var pct in new[]
            {
                nameof(TenderWork.SecurityDepositPercent), nameof(TenderWork.OfficeProtocolPercent),
                nameof(TenderWork.CorporatorProtocolPercent)
            })
            {
                e.Property(pct).HasColumnType("decimal(5,2)");
            }

            e.HasMany(t => t.RaBills)
                .WithOne(r => r.TenderWork)
                .HasForeignKey(r => r.TenderWorkId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TenderRaBill>(e =>
        {
            e.Property(r => r.RaBillNumber).HasMaxLength(50).IsRequired();
            e.Property(r => r.BillDate).HasColumnType("date");
            e.Property(r => r.BilledAmount).HasColumnType("decimal(14,2)");
            e.Property(r => r.CorporatorCommissionPercent).HasColumnType("decimal(5,2)");
            e.Property(r => r.OfficerCommissionPercent).HasColumnType("decimal(5,2)");
            e.Property(r => r.Remarks).HasMaxLength(500);
        });

        // ----- Shared GST vendor sub-ledger -----
        modelBuilder.Entity<GstVendorEntry>(e =>
        {
            e.Property(g => g.WorkType).HasConversion<string>().HasMaxLength(20);
            e.Property(g => g.VendorName).HasMaxLength(150).IsRequired();
            e.Property(g => g.SentDate).HasColumnType("date");
            e.Property(g => g.GstBillAmount).HasColumnType("decimal(14,2)");
            e.Property(g => g.CommissionPercent).HasColumnType("decimal(5,2)");
            e.HasIndex(g => new { g.WorkType, g.WorkId });

            e.HasMany(g => g.Repayments)
                .WithOne(r => r.GstVendorEntry)
                .HasForeignKey(r => r.GstVendorEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GstVendorRepayment>(e =>
        {
            e.Property(r => r.ReceivedDate).HasColumnType("date");
            e.Property(r => r.AmountReceived).HasColumnType("decimal(14,2)");
            e.Property(r => r.Mode).HasConversion<string>().HasMaxLength(20);
        });

        // ----- Commission Works -----
        modelBuilder.Entity<CommissionWork>(e =>
        {
            e.Property(c => c.WorkName).HasMaxLength(300).IsRequired();
            e.Property(c => c.PartyName).HasMaxLength(150).IsRequired();
            e.Property(c => c.TenderWorkAmount).HasColumnType("decimal(14,2)");
            e.Property(c => c.CommissionPercent).HasColumnType("decimal(5,2)");
            e.Property(c => c.GstAmount).HasColumnType("decimal(14,2)");
            e.Property(c => c.BilledGst).HasColumnType("decimal(14,2)");
            e.Property(c => c.ExtraGstBill).HasColumnType("decimal(14,2)");
            e.Property(c => c.GstBillCommission).HasColumnType("decimal(14,2)");
        });

        // ----- Private Works -----
        modelBuilder.Entity<PrivateWork>(e =>
        {
            e.Property(p => p.ClientName).HasMaxLength(150).IsRequired();
            e.Property(p => p.WorkDescription).HasMaxLength(500);
            e.Property(p => p.AreaSqft).HasColumnType("decimal(14,2)");
            e.Property(p => p.RatePerSqft).HasColumnType("decimal(14,2)");

            e.HasMany(p => p.Milestones)
                .WithOne(m => m.PrivateWork)
                .HasForeignKey(m => m.PrivateWorkId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p => p.Categories)
                .WithOne(c => c.PrivateWork)
                .HasForeignKey(c => c.PrivateWorkId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p => p.Materials)
                .WithOne(m => m.PrivateWork)
                .HasForeignKey(m => m.PrivateWorkId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PrivateWorkMilestone>(e =>
        {
            e.Property(m => m.StageName).HasMaxLength(150).IsRequired();
            e.Property(m => m.PercentOfTotal).HasColumnType("decimal(5,2)");
            e.Property(m => m.PaidAmount).HasColumnType("decimal(14,2)");
            e.Property(m => m.PaidDate).HasColumnType("date");
            e.Property(m => m.Status).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<PrivateWorkCategory>(e =>
        {
            e.Property(c => c.CategoryName).HasMaxLength(100).IsRequired();
            e.Property(c => c.WorkerName).HasMaxLength(150);
            e.Property(c => c.RateBasis).HasMaxLength(100);
            e.Property(c => c.AgreedTotalAmount).HasColumnType("decimal(14,2)");

            e.HasMany(c => c.Payments)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PrivateWorkCategoryPayment>(e =>
        {
            e.Property(p => p.PaymentDate).HasColumnType("date");
            e.Property(p => p.Amount).HasColumnType("decimal(14,2)");
            e.Property(p => p.Remarks).HasMaxLength(300);
        });

        modelBuilder.Entity<PrivateWorkMaterial>(e =>
        {
            e.Property(m => m.MaterialName).HasMaxLength(150).IsRequired();
            e.Property(m => m.VendorName).HasMaxLength(150);
            e.Property(m => m.PaymentDate).HasColumnType("date");
            e.Property(m => m.Amount).HasColumnType("decimal(14,2)");
        });
    }
}
