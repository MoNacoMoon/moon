using Microsoft.EntityFrameworkCore;

namespace moon.Models
{
    public class UserAuthDbContext : DbContext
    {
        public DbSet<СекретныйВопрос> СекретныйВопрос { get; set; } = null!;
        public DbSet<Пользователь> Пользователь { get; set; } = null!;

        public UserAuthDbContext()
        {
        }

        public UserAuthDbContext(DbContextOptions<UserAuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=user_auth_db;Username=postgres;Password=Moon_GGWW52");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<СекретныйВопрос>(entity =>
            {
                entity.ToTable("СекретныйВопрос");
                entity.HasKey(e => e.КодСекретногоВопроса);
                entity.Property(e => e.КодСекретногоВопроса).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Пользователь>(entity =>
            {
                entity.ToTable("Пользователь");
                entity.HasKey(e => e.КодПользователя);
                entity.Property(e => e.КодПользователя).ValueGeneratedOnAdd();

                entity.HasOne(d => d.СекретныйВопрос)
                    .WithMany(p => p.Пользователи)
                    .HasForeignKey(d => d.КодСекретногоВопроса);
            });
        }
    }
}
