using GroceryFinder.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.DbContext;

public class GroceryFinderDbContext : IdentityDbContext<AppUser, UserRole, Guid>
{
    public GroceryFinderDbContext(DbContextOptions<GroceryFinderDbContext> options) : base(options)
    {

    }

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

