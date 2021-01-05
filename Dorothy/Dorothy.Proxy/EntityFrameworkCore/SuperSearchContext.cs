using Dorothy.Proxy.EntityFrameworkCore.Config;
using Dorothy.Proxy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dorothy.Proxy.EntityFrameworkCore
{
    /// <summary>
    /// I make use of an Sqlite database saved to a file for the sake of the Development Test.
    /// In production it would ofcourse be connect to a MSSQL database or similar, hosted closeby the application.
    /// </summary>
    public class SuperSearchContext : DbContext
    {
        string DbPath { get; set; }

        public SuperSearchContext(string sqliteFilePath)
        {
            DbPath = sqliteFilePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ResultProxyConfig());
            modelBuilder.ApplyConfiguration(new ResultStringProxyConfig());
            modelBuilder.ApplyConfiguration(new SearchProxyConfig());
        }

        public DbSet<SearchProxy> Searches { get; set; }
        public DbSet<ResultProxy> Results { get; set; }
        public DbSet<ResultStringProxy> ResultStrings { get; set; }
    }
}
