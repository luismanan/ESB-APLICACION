using Dapper;
using ESB.Application.DTOs;
using ESB.Application.Interfaces.Repositories;
using ESB.Application.Wrappers;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ESB.Infrastructure.Repositories
{
    public class DirectQueryOrSPRepository : DapperBaseRepository, IDirectQueryOrSPRepository
    {

        public DirectQueryOrSPRepository(IDbConnection connection) : base(connection)
        {

        }


        public async Task<Response<SpResult>> SpTestResult(string nombre, string apellido)
        {

            var sSql = "dbo.GetInfoTestDapper";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Nombre", nombre, DbType.String, ParameterDirection.Input);
            parameters.Add("@Apellido", apellido, DbType.String, ParameterDirection.Input);

            var dbResult = await base._connection.QueryAsync<SpResult>(sSql, commandType: CommandType.StoredProcedure, param: parameters);

            return new Response<SpResult>(dbResult.LastOrDefault());

        }




    }
}
