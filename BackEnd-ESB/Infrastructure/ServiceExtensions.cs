using ESB.Application.Interfaces.Repositories;
using ESB.Application.Interfaces.Services;
using ESB.Infrastructure.Data;
using ESB.Infrastructure.Repositories;
using ESB.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace ESB.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PrincipalContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("default"),
                b => b.MigrationsAssembly(typeof(PrincipalContext).Assembly.FullName)));

            services.AddScoped<DbContext, PrincipalContext>();

            services.AddScoped<IDbConnection>(db =>
              new SqlConnection(configuration.GetConnectionString("default")));


            #region Repositories
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(EFRepository<>));
            #endregion

            #region Services  
   
            services.AddScoped<IRoleService, RolesService>();
    
            services.AddScoped<IUsuariosService, UsuarioService>();
       
            services.AddScoped<IUsuarioRolesService, UsuarioRolesService>();
            services.AddScoped<IBomberosService , BomberosService>();
            services.AddScoped<IRegistroIncendiosService , RegistroIncendiosService>(); 
        


            #endregion
        }
    }
}
 