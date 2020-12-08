using Api.Domain.Entities;
using Api.Data.Map;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Data.Context
{
    public class Contexto : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
            modelBuilder.Entity<UserEntity>().HasData(
            new UserEntity
            {
                id = Guid.NewGuid(),
                nome = "Administrador",
                email = "maxwbs@gmail.com",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            }
        );
            base.OnModelCreating(modelBuilder);
        }
    }
}
