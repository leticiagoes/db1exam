using System.Data;
using System.Data.OleDb;
using DB1.AvaliacaoTecnica.API.Models;

namespace DB1.AvaliacaoTecnica.API.Services
{
    public class CandidateRepository
    {
        private string TableName = "Candidate";
        OleDbConnection connection;
        OleDbDataAdapter adapter;
        DataSet ds;
        OleDbCommand cmd;

        public CandidateRepository()
        {
            connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\DB1_Candidates.mdb");
        }

        public DataTable GetAll()
        {
            string query = "SELECT Id, Name, IdOpportunity FROM " + TableName;
            return ExecuteSelect(query);
        }

        public DataTable GetById(long Id)
        {
            string query = "SELECT Id, Name, IdOpportunity FROM " + TableName + " WHERE Id = " + Id;
            return ExecuteSelect(query);
        }

        public DataTable GetByName(string Name)
        {
            string query = "SELECT Id, Name, IdOpportunity FROM " + TableName + " WHERE Name = '" + Name + "'";
            return ExecuteSelect(query);
        }

        public DataTable GetByNameAndId(Candidate entity)
        {
            string query = "SELECT Id, Name, IdOpportunity FROM " + TableName + " WHERE Name = '" + entity.Name + "' AND Id <> " + entity.Id;
            return ExecuteSelect(query);
        }

        public DataTable GetScoreByCandidate()
        {
            string query = @"SELECT C.Name, SUM(OT.Weight) AS Score
                                FROM(Candidate AS C
                                INNER JOIN CandidateTechnology AS CT ON CT.IdCandidate = C.Id)
                                INNER JOIN OpportunityTechnology AS OT ON OT.IdTechnology = CT.IdTechnology AND OT.IdOpportunity = C.IdOpportunity
                                GROUP BY C.Name
                                ORDER BY C.Name ASC";
            return ExecuteSelect(query);
        }

        public void Insert(Candidate entity)
        {
            string query = "INSERT INTO " + TableName + " (Name, IdOpportunity) VALUES ('" + entity.Name + "', " + entity.IdOpportunity + ")";
            ExecuteCommand(query);
        }

        public void Update(Candidate entity)
        {
            string query = "UPDATE " + TableName + " SET Name = '" + entity.Name + "', IdOpportunity = " + entity.IdOpportunity + " WHERE Id = " + entity.Id;
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

        public Validate ValidateDelete(long Id)
        {
            Validate valid = new Validate();

            if (Id > 0)
            {
                DataTable dt = GetById(Id);
                valid.IsValid = dt.Rows.Count > 0;
                valid.Message = dt.Rows.Count > 0 ? "Esse item já existe." : "Item não encontrado.";
            }

            return valid;
        }

        public Validate ValidateInsert(Candidate entity)
        {
            Validate valid = new Validate();

            if (!string.IsNullOrEmpty(entity.Name))
            {
                DataTable dt = GetByName(entity.Name);
                valid.IsValid = !(dt.Rows.Count > 0);
                valid.Message = !valid.IsValid ? "Já existe um item com mesma descrição." : "Item não encontrado";
            }

            return valid;
        }

        public Validate ValidateUpdate(Candidate entity)
        {
            Validate valid = new Validate();

            if (entity.Id > 0)
            {
                DataTable dt = GetById(entity.Id);
                valid.IsValid = dt.Rows.Count > 0;

                if (valid.IsValid && !string.IsNullOrEmpty(entity.Name))
                {
                    DataTable dt2 = GetByNameAndId(entity);
                    valid.IsValid = !(dt2.Rows.Count > 0);
                    valid.Message = !valid.IsValid ? "Já existe um item com mesma descrição." : "Item não encontrado";
                }
                else
                    valid.Message = dt.Rows.Count > 0 ? "Esse item já existe." : "Item não encontrado.";
            }

            return valid;
        }
    }
}