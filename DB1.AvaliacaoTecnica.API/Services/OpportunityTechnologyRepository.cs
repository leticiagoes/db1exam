using System.Data;
using System.Data.OleDb;
using DB1.AvaliacaoTecnica.API.Models;

namespace DB1.AvaliacaoTecnica.API.Services
{
    public class OpportunityTechnologyRepository
    {
        private string TableName = "OpportunityTechnology";
        OleDbConnection connection;
        OleDbDataAdapter adapter;
        DataSet ds;
        OleDbCommand cmd;

        public OpportunityTechnologyRepository()
        { 
            connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\DB1_Candidates.mdb");
        }

        public DataTable GetAll()
        {
            string query = "SELECT Id, Weight, IdOpportunity, IdTechnology FROM " + TableName;
            return ExecuteSelect(query);
        }

        public DataTable GetAllWithDescription()
        {
            string query = @"SELECT OT.Id, OT.Weight, OT.IdOpportunity, OT.IdTechnology, 
                                O.Description AS DescriptionOpportunity,
                                T.Description AS DescriptionTechnology
                                FROM (" + TableName + @" AS OT
                                INNER JOIN Opportunity AS O ON O.Id = OT.IdOpportunity)
                                INNER JOIN Technology T ON T.Id = OT.IdTechnology
                                ORDER BY T.Description ASC, O.Description ASC";
            return ExecuteSelect(query);
        }

        public DataTable GetById(long Id)
        {
            string query = "SELECT Id, Weight, IdOpportunity, IdTechnology FROM " + TableName + " WHERE Id = " + Id;
            return ExecuteSelect(query);
        }

        public DataTable GetExist(OpportunityTechnology entity)
        {
            string query = "SELECT Id, Weight, IdOpportunity, IdTechnology FROM " + TableName + " WHERE IdOpportunity = " + entity.IdOpportunity + " AND IdTechnology = " + entity.IdTechnology;
            if (entity.Id > 0)
                query = "SELECT Id, Weight, IdOpportunity, IdTechnology FROM " + TableName + " WHERE Id <> " + entity.Id + " AND IdOpportunity = " + entity.IdOpportunity + " AND IdTechnology = " + entity.IdTechnology;
            return ExecuteSelect(query);
        }

        public void Insert(OpportunityTechnology entity)
        {
            string query = "INSERT INTO " + TableName + " (Weight, IdOpportunity, IdTechnology) VALUES (" + entity.Weight + ", " + entity.IdOpportunity + ", " + entity.IdTechnology + ")";
            ExecuteCommand(query);
        }

        public void Update(OpportunityTechnology entity)
        {
            string query = "UPDATE " + TableName + " SET Weight = " + entity.Weight + ", IdOpportunity = " + entity.IdOpportunity + ", IdTechnology = " + entity.IdTechnology + " WHERE Id = " + entity.Id;
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

        public Validate ValidateInsert(OpportunityTechnology entity)
        {
            Validate valid = new Validate();

            if (entity.IdOpportunity > 0 && entity.IdTechnology > 0)
            {
                DataTable dt = GetExist(entity);
                valid.IsValid = !(dt.Rows.Count > 0);
                valid.Message = !valid.IsValid ? "Já existe um item para a mesma vaga e mesma tecnologia." : "Item não encontrado";
            }

            return valid;
        }
    }
}