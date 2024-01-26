using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NadinSoft.Infra.Data
{
    public class NadinSoftContext : IdentityDbContext
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
