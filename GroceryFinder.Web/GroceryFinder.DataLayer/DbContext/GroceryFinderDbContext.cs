﻿using GroceryFinder.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.DbContext;

public class GroceryFinderDbContext : IdentityDbContext<AppUser, UserRole, Guid>
{
    public GroceryFinderDbContext(DbContextOptions<GroceryFinderDbContext> options) : base(options)
    {

    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<GroceryStore> GroceryStores { get; set; }
    public DbSet<ProductGroceryStore> ProductGroceryStores { get; set; }
    public DbSet<UserAllergy> UserAllergy { get; set; }
    public DbSet<PriceUpdateSubscription> PriceUpdateSubscription { get; set; }
    public DbSet<EmailQueueItem> EmailQueueItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

