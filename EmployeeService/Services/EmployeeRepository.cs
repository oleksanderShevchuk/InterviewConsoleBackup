using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using EmployeeService.Model;

namespace EmployeeService.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<EmployeeDto> GetAll()
        {
            var list = new List<EmployeeDto>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ID, Name, ManagerID, Enable FROM Employee", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeDto
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ManagerID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Enable = reader.GetBoolean(3)
                            });
                        }
                    }
                }
            }

            return list;
        }

        public void UpdateEnable(int id, bool enable)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Employee SET Enable = @Enable WHERE ID = @ID", conn))
                {
                    cmd.Parameters.Add("@Enable", SqlDbType.Bit).Value = enable;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetQueryResult(string query) // "SELECT * FROM Employee"
        {
            var dt = new DataTable();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
