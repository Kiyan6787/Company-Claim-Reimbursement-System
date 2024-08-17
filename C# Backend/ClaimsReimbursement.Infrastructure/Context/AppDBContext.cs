using ClaimsReimbursement.Infrastructure.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ClaimsReimbursement.Infrastructure.Context;

public class AppDBContext : IdentityDbContext<AppUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Seeding the data for the currencies, reimbursement types, and banks.

        builder.Entity<Currencies>().HasData(
        new Currencies { CurrencyId = 1, Code = "USD" },
        new Currencies { CurrencyId = 2, Code = "EUR" },
        new Currencies { CurrencyId = 3, Code = "INR" },
        new Currencies { CurrencyId = 4, Code = "ZAR" }
        );

        builder.Entity<ReimbursementTypes>().HasData(
            new ReimbursementTypes { ReimbursementTypeId = 1, Type = "Travel" },
            new ReimbursementTypes { ReimbursementTypeId = 2, Type = "Medical" },
            new ReimbursementTypes { ReimbursementTypeId = 3, Type = "Food" },
            new ReimbursementTypes { ReimbursementTypeId = 4, Type = "Entertainment" },
            new ReimbursementTypes { ReimbursementTypeId = 5, Type = "Miscellaneous" }
        );

        builder.Entity<Bank>().HasData(
            new Bank { BankId = 1, BankName = "Last International Bank" },
            new Bank { BankId = 2, BankName = "ASBA Bank" },
            new Bank { BankId = 3, BankName = "Discovery Bank" },
            new Bank { BankId = 4, BankName = "LLB" },
            new Bank { BankId = 5, BankName = "Gold Bank" }
            );

        //Defining the relationships between the entities.

        builder.Entity<Reimbursement>()
            .HasOne(r => r.Currency)
            .WithMany(c => c.Reimbursements)
            .HasForeignKey(r => r.CurrencyId);

        builder.Entity<Reimbursement>()
            .HasOne(r => r.ReimbursementType)
            .WithMany(c => c.Reimbursements)
            .HasForeignKey(r => r.ReimbursementTypeId);

        builder.Entity<Bank>()
            .HasMany(r => r.AppUsers)
            .WithOne(c => c.Bank)
            .HasForeignKey(r => r.BankId);

    }

    //Defining the DB sets.
    public DbSet<Reimbursement> Reimbursements { get; set; } = default!;
    public DbSet<ReimbursementTypes> ReimbursementTypes { get; set; } = default!;
    public DbSet<Currencies> Currencies { get; set; } = default!;
    public DbSet<Bank> Bank { get; set; } = default!;

}
