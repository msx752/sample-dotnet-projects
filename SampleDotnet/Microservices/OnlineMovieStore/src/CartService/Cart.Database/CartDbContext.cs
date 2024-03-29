﻿using Cart.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Database
{
    public class CartDbContext : DbContext
    {
        public DbSet<CartEntity> Baskets { get; set; }
        public DbSet<CartItemEntity> BasketItems { get; set; }

        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
        }
    }
}