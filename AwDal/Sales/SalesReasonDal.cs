using AwDal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Production;
using System.Data.SqlClient;
using System.Data;
using Common.Runtime;
using Entities.Sales;

namespace AwDal.Production
{
    public class SalesReasonDal : AwBaseDal
    {
        public SalesReasonDal(string connectionString) : base(connectionString)
        {
        }

        public List<SalesReason> GetAll()
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = "Sales.uspGetSales",
                CommandType = CommandType.StoredProcedure
            };

            DataTable dt = new();
            SqlDataAdapter adapter = new(command);
            adapter.Fill(dt);

            var query = dt.AsEnumerable().Select(x => new SalesReason
            {
                SalesReasonID = Convert.ToInt32(x["SalesReasonID"]),
                Name = Convert.ToString(x["Name"]),
                ReasonType = Convert.ToString(x["ReasonType"]),
                ModifiedDate = Convert.ToDateTime(x["ModifiedDate"])
            });

            return query.ToList();
        }

        public async Task<SalesReason> GetById(int categoryId)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = "Sales.uspGetSales",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@SalesID", categoryId);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            SalesReason sales = new();
            if (reader.Read())
            {
                sales.SalesReasonID = reader.GetInt32("SalesReasonID");
                sales.Name = reader.GetString("Name");
                sales.ReasonType = reader.GetString("ReasonType");
                sales.ModifiedDate = reader.GetDateTime("ModifiedDate");
            }
            return sales;
        }
        public async Task<int> Create(string salesName)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlParameter parameter = new()
            {
                ParameterName = "@Name",
                Value = salesName
            };
            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = "Sales.uspCreateSales",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(parameter);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<ExecutionResult> Save(string salesName)
        {
            ExecutionResult result = new();
            try
            {
                SqlConnection conn = GetConnection();
                conn.Open();
                SqlParameter parameter = new()
                {
                    ParameterName = "@Name",
                    Value = salesName
                };
                SqlCommand command = new()
                {
                    Connection = conn,
                    CommandText = "Sales.uspSaveSales",
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(parameter);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    result.Outcome = reader.GetBoolean("Outcome");
                    result.Id = reader.GetInt32("Id");
                    result.Message = reader.GetString("Message");
                }
                else
                {
                    result = new ExecutionResult { Outcome = false, Message = "No se obtuvo respuesta desde la base de datos." };
                }
            }
            catch (Exception ex)
            {

                result = new ExecutionResult { Outcome = false, Message = ex.Message };
            }
            return result;
        }
    }
}
