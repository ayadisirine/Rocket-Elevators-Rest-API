using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;
using Rocket_Elevators_Rest_API.Models;


namespace Rocket_Elevators_Rest_API.Controllers
{
    public class ElevatorsQuery
    {

        public ElevatorsQuery(AppDb db)
        {
            Db = db;
        }
        
        public async Task<List<Elevators>> IdleElevatorsAsync(string status)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Status` FROM `elevators` WHERE `Status` = @status;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@status",
                DbType = DbType.String,
                Value = status,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }
        public async Task<Elevators> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `status` FROM `elevators` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        private async Task<List<Elevators>> ReadAllAsync(DbDataReader reader)
        {
            var elevators = new List<Elevators>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var e = new Elevators(Db)
                    {
                        Id = reader.GetInt32(0),
                        Status = reader.GetString(1),
                    };
                    elevators.Add(e);
                }
            }
            return elevators;
        }
        public AppDb Db { get; }
    }
}