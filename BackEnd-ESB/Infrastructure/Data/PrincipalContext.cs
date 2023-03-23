using ESB.Domain.Entities;
using ESB.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ESB.Infrastructure.Data
{
    public partial class PrincipalContext : DbContext
    {
        public PrincipalContext()
        {
        }

        public PrincipalContext(string CnString): base(GetOptions(CnString))
        {
        }

        public static DbContextOptions GetOptions(string CnString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), CnString).Options;
        }

        public PrincipalContext(DbContextOptions<PrincipalContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Bomberos> Bomberos { get; set; }
        public virtual DbSet<RegistroIncendios> RegistroIncendios { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UsuarioRoles> UsuarioRoles { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelsConfiguration.Configuration(modelBuilder); 
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}