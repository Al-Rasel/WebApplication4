
using Dapper;
using SampleDatabaseConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication4.Models.AuthData
{
    public class Person
    {
        public static bool IsLiveConnection = true;
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }

        public async Task<IEnumerable<Person>> GetAllSubCategories(int categoryId)
        {
            var parameter = new DynamicParameters();
            parameter.Add(name: "@CategoryId", value: categoryId, dbType: DbType.Int32,
                direction: ParameterDirection.Input);

            using (var connection = new DatabaseConnection().GetConnection(isLiveConnection: IsLiveConnection))
            {
                return await connection.QueryAsync<Person>(
                    sql: @"[Deal].[USP_LoadSubCategory]",
                    commandType: CommandType.StoredProcedure,
                    param: parameter);
            }
        }

    }
}