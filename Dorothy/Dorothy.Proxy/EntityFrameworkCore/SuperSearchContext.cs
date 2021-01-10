using Dorothy.Proxy.EntityFrameworkCore.Config;
using Dorothy.Proxy.Models;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Dorothy.Proxy.EntityFrameworkCore
{
    /// <summary>
    /// I make use of an Sqlite database saved to a file for the sake of the Development Test.
    /// In production it would ofcourse be connect to a MSSQL database or similar, hosted closeby the application.
    /// </summary>
    public class SuperSearchContext : DbContextWithTriggers
    {
        public delegate Task SearchesChanged(Task<List<SearchProxy>> searches, EventArgs args);
        public event SearchesChanged OnSearchesChanged;

        public delegate Task ResultsChanged(Task<List<ResultProxy>> results, EventArgs args);
        public event ResultsChanged OnResultsChanged;

        string DbPath { get; set; }

        public SuperSearchContext()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(dir, $"Search.db3");

            DbPath = path;

            SetupTriggers();
        }

        #region Triggers
        private void SetupTriggers() 
        {
            TriggersEnabled = true;

            Triggers<SearchProxy>.Deleted += SearchProxy_Deleted;
            Triggers<SearchProxy>.Updated += SearchProxy_Updated;
            Triggers<SearchProxy>.Inserted += SearchProxy_Inserted;

            Triggers<ResultProxy>.Deleted += ResultProxy_Deleted;
            Triggers<ResultProxy>.Updated += ResultProxy_Updated;
            Triggers<ResultProxy>.Inserted += ResultProxy_Inserted;
        }

        private void SearchProxy_Inserted(IInsertedEntry<SearchProxy, DbContext> obj)
        {
            Searches_Changed();
        }

        private void SearchProxy_Updated(IUpdatedEntry<SearchProxy, DbContext> obj)
        {
            Searches_Changed();
        }

        private void SearchProxy_Deleted(IDeletedEntry<SearchProxy, DbContext> obj)
        {
            Searches_Changed();
        }

        private void Searches_Changed() 
        {
            OnSearchesChanged?.Invoke(Searches.ToListAsync(), new EventArgs());
        }

        private void ResultProxy_Inserted(IInsertedEntry<ResultProxy, DbContext> obj)
        {
            Results_Changed();
        }

        private void ResultProxy_Updated(IUpdatedEntry<ResultProxy, DbContext> obj)
        {
            Results_Changed();
        }

        private void ResultProxy_Deleted(IDeletedEntry<ResultProxy, DbContext> obj)
        {
            Results_Changed();
        }

        private void Results_Changed() 
        {
            OnResultsChanged?.Invoke(Results.ToListAsync(), new EventArgs());
        }

        #endregion


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
