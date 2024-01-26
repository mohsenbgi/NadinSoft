using Microsoft.EntityFrameworkCore;

namespace NadinSoft.Infra.Data
{
    public class NadinSoftContext : DbContext
    {
        public NadinSoftContext(DbContextOptions<NadinSoftContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NadinSoftContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
