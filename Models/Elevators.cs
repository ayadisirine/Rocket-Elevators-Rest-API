using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rocket_Elevators_Rest_API.Models
{
    public partial class Elevators
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public DateTime DateOfCommissioning { get; set; }
        public DateTime DateOfLastInspection { get; set; }
        public string CertificateOfInspection { get; set; }
        public string Information { get; set; }
        public string Notes { get; set; }
        public long? column_id { get; set; }
        public string ElevatorType { get; set; }
        public virtual Columns Column { get; set; }

       internal AppDb Db { get; set; }

        public Elevators()
       {
        }

        internal Elevators(AppDb db)
        {
            Db = db;
        }
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `elevators` SET `status` = @status WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }
 
        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
       {
           cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@status",
                DbType = DbType.String,
                Value = Status,
            });
        }
    }
}
