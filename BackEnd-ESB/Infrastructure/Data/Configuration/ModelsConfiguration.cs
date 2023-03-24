using ESB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESB.Infrastructure.Data.Configuration
{
    public class ModelsConfiguration
    {

        public static void Configuration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bomberos>(entity =>
            {
                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Nacimiento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RegistroIncendios>(entity =>
            {
                entity.ToTable("Registro_Incendios");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.IdBomberoCargo).HasColumnName("Id_BomberoCargo");

                entity.HasOne(d => d.IdBomberoCargoNavigation)
                    .WithMany(p => p.RegistroIncendios)
                    .HasForeignKey(d => d.IdBomberoCargo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registro_Incendios_Bomberos");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.NombreRoles)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<UsuarioRoles>(entity =>
            {
                entity.Property(e => e.IdRoles).HasColumnName("Id_Roles");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsuarioRoles)
                    .HasForeignKey(d => d.IdRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsuarioRoles)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRoles_Usuarios");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ClaveSeguridad)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UltimoAcceso).HasColumnType("datetime");
            });


        }

    }
}
