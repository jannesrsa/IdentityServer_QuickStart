using CustomUserManagerRepository.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomUserManagerRepository
{
    public class DataContext : DbContext
    {
        private string _connectionString;

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataContext(DbContextOptions<DataContext> options)
         : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        private DbSet<LoginResult> LoginResult { get; set; }

        public bool GetLoginAsync(string username, string password)
        {
            var pUName = new SqlParameter("@UName", SqlDbType.NVarChar) { Value = username };
            var pPassword = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password };
            return LoginResult.FromSqlRaw("[CustomUM].[GetLogin] @UName, @Password", pUName, pPassword).AsEnumerable<LoginResult>().FirstOrDefault<LoginResult>().PasswordMatch;
         }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder
                    .UseSqlServer(_connectionString, options =>
                    {
                        options.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
                    });
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CustomUM");
            DefineUser(modelBuilder);
            DefineLoginResult(modelBuilder);
        }

        private static void DefineUser(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasKey(a => a.UserID)
                .HasName("PK_CustomUM_User");
        }

        private static void DefineLoginResult(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<LoginResult>()
                .HasNoKey();
        }


    }
}
