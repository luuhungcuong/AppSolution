namespace AppSolution.Infrastructure.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppSolutionDbContext : DbContext
    {
        public AppSolutionDbContext()
            : base("name=AppSolutionDbContext")
        {
        }

        public virtual DbSet<ActionLog> ActionLog { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<FunctionRoles> FunctionRoles { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<UserCode> UserCode { get; set; }
        public virtual DbSet<VendorCode> VendorCode { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionLog>()
                .Property(e => e.ActionID)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<FunctionRoles>()
                .Property(e => e.FunctionId)
                .IsUnicode(false);

            modelBuilder.Entity<UserCode>()
                .Property(e => e.TableID)
                .IsUnicode(false);

            modelBuilder.Entity<UserCode>()
                .Property(e => e.CodeID)
                .IsUnicode(false);

            modelBuilder.Entity<VendorCode>()
                .Property(e => e.TableID)
                .IsUnicode(false);

            modelBuilder.Entity<VendorCode>()
                .Property(e => e.CodeID)
                .IsUnicode(false);
        }
    }
}
