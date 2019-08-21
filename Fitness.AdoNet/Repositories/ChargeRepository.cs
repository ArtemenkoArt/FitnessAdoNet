using Fitness.DataObject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.AdoNet.Repositories
{
    public interface IChargeRepository
    {
        IEnumerable<ChargeExtendet> GetList();
        ChargeExtendet GetItem(int id);
        void Insert(ChargeExtendet chargeExtendet);
        void Update(ChargeExtendet chargeExtendet);
        void Delete(int idCharge);
    }

    public class ChargeRepository : IChargeRepository
    {
        private string _connectionString;

        public ChargeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private ChargeExtendet ConvertSqlRowToCharge(DataRow dr)
        {
            ChargeExtendet charge = new ChargeExtendet
            {
                Date = Convert.ToDateTime(dr["Date"]),
                GroupName = dr["GroupName"].ToString(),
                GroupId = Convert.ToInt32(dr["GroupId"]),
                Id = Convert.ToInt32(dr["Id"]),
                Name = dr["Name"].ToString(),
                Summ = Convert.ToDecimal(dr["Summ"])
            };
            return charge;
        }

        public IEnumerable<ChargeExtendet> GetList()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "select c.Id, cg.Name as GroupName, cg.Id as GroupId, c.Name, c.Summ, c.Date from Charges as c join ChargeGroup as cg on c.GroupId = cg.Id";

                var comm = new SqlCommand(sql, connection);

                DataTable dt = new DataTable();

                var adapter = new SqlDataAdapter(comm);

                adapter.Fill(dt);

                var result = new List<ChargeExtendet>();          

                foreach (DataRow dr in dt.Rows) //NOT VAR
                {
                    result.Add(ConvertSqlRowToCharge(dr));
                }
                return result;
            }
        }

        public ChargeExtendet GetItem(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "select c.Id, cg.Name as GroupName, cg.Id as GroupId, c.Name, c.Summ, c.Date from Charges c join ChargeGroup cg on c.GroupId = cg.Id where c.Id = " + id.ToString();

                var comm = new SqlCommand(sql, connection);

                DataTable dt = new DataTable();

                var adapter = new SqlDataAdapter(comm);

                adapter.Fill(dt);

                ChargeExtendet result = null;

                foreach (DataRow dr in dt.Rows) //NOT VAR
                {
                    result = ConvertSqlRowToCharge(dr);
                    break;
                }
                return result;
            }
        }

        public void Insert(ChargeExtendet chargeExtendet)
        {
            string sql = "insert into [dbo].[Charges] (Name, Summ, Date, GroupId) values(@PName, @PSumm, @PDate, @PGroupId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(sql, connection);
                comm.Parameters.AddWithValue("PName", chargeExtendet.Name.ToString());
                comm.Parameters.AddWithValue("PSumm", chargeExtendet.Summ.ToString());
                comm.Parameters.AddWithValue("PDate", chargeExtendet.Date.ToString());
                comm.Parameters.AddWithValue("PGroupId", chargeExtendet.GroupId.ToString());
                //comm.ExecuteScalar();
                int numrow = comm.ExecuteNonQuery();
            }
        }

        public void Update(ChargeExtendet chargeExtendet)
        {
            string sql = "update [dbo].[Charges] set Name = @PName, Summ = @PSumm, Date = @PDate, GroupId = @PGroupId where Id = @PId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(sql, connection);
                comm.Parameters.AddWithValue("PName", chargeExtendet.Name.ToString());
                comm.Parameters.AddWithValue("PSumm", chargeExtendet.Summ.ToString());
                comm.Parameters.AddWithValue("PDate", chargeExtendet.Date.ToString());
                comm.Parameters.AddWithValue("PGroupId", chargeExtendet.GroupId.ToString());
                comm.Parameters.AddWithValue("PId", chargeExtendet.Id.ToString());
                //comm.ExecuteScalar();
                int numrow = comm.ExecuteNonQuery();
            }
        }

        public void Delete(int idCharge)
        {
            string sql = "delete from [dbo].[Charges] where id = @PId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(sql, connection);
                comm.Parameters.AddWithValue("PId", idCharge.ToString());

                //comm.ExecuteScalar();
                int numrow = comm.ExecuteNonQuery();
            }
        }

       
    }
}
