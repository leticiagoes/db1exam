using System.Data;
using System.Data.OleDb;
using DB1.AvaliacaoTecnica.API.Models;

namespace DB1.AvaliacaoTecnica.API.Services
{
    public class CandidateTechnologyRepository
    {
        private string TableName = "CandidateTechnology";
        OleDbConnection connection;
        OleDbDataAdapter adapter;
        DataSet ds;
        OleDbCommand cmd;

        public CandidateTechnologyRepository()
        {
            connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\DB1_Candidates.mdb");
        }

        public DataTable GetAll()
        {
            string query = "SELECT Id, IdCandidate, IdTechnology FROM " + TableName;
            return ExecuteSelect(query);
        }

        public DataTable GetById(long Id)
        {
            string query = "SELECT Id, IdCandidate, IdTechnology FROM " + TableName + " WHERE Id = " + Id;
            return ExecuteSelect(query);
        }

        public void Insert(CandidateTechnology entity)
        {
            string query = "INSERT INTO " + TableName + " (IdCandidate, IdTechnology) VALUES (" + entity.IdCandidate + ", " + entity.IdTechnology + ")";
            ExecuteCommand(query);
        }

        public void Update(CandidateTechnology entity)
        {
            string query = "UPDATE " + TableName + " SET IdCandidate = " + entity.IdCandidate + ", IdTechnology = " + entity.IdTechnology + " WHERE Id = " + entity.Id;
            ExecuteCommand(query);
        }

        public void Delete(long Id)
        {
            string query = "DELETE FROM " + TableName + " WHERE Id = " + Id;
            ExecuteCommand(query);
        }

        private DataTable ExecuteSelect(string query)
        {
            DataTable dt = new DataTable();
            adapter = new OleDbDataAdapter(query, connection);
            ds = new DataSet();

            adapter.Fill(ds, TableName);
            if (ds.Tables[TableName].Rows.Count > 0)
            {
                dt = ds.Tables[TableName];
            }

            return dt;
        }

        private void ExecuteCommand(string query)
        {
            cmd = new OleDbCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}