using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiForLogging
{
    public partial class apiforloggingdbContext : DbContext
    {
        private string Localhost = "localhost";
        private string Port = "5432";
        private string Database = "apiforloggingdb";
        private string Username;
        private string Password;

        public apiforloggingdbContext(string localhost, string port, string database, string username, string password)
        {
            Localhost = localhost;
            Port = port;
            Database = database;
            Username = username;
            Password = password;
        }

        public apiforloggingdbContext()
        {
        }

        public apiforloggingdbContext(DbContextOptions<apiforloggingdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Record> Records { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql($"Host={Localhost};Port={Port};Database={Database};Username={Username};Password={Password}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("records");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Loglevel)
                    .HasColumnType("character varying")
                    .HasColumnName("loglevel");

                entity.Property(e => e.Source)
                    .HasColumnType("character varying")
                    .HasColumnName("source");

                entity.Property(e => e.Text)
                    .HasColumnType("character varying")
                    .HasColumnName("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
