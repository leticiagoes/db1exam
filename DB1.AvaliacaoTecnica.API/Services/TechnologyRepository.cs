using System.Data;
using System.Data.OleDb;
using DB1.AvaliacaoTecnica.API.Models;

namespace DB1.AvaliacaoTecnica.API.Services
{
    public class TechnologyRepository
    {
        private string TableName = "Technology";
        OleDbConnection connection;
        OleDbDataAdapter adapter;
        DataSet ds;
        OleDbCommand cmd;

        public TechnologyRepository()
        {
            connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\DB1_Candidates.mdb");
        }

        public DataTable GetAll()
        {
            string query = "SELECT Id, Description FROM " + TableName;
            return ExecuteSelect(query);
        }

        public DataTable GetById(long Id)
        {
            string query = "SELECT Id, Description FROM " + TableName + " WHERE Id = " + Id;
            return ExecuteSelect(query);
        }

        public DataTable GetByDescription(string Description)
        {
            string query = "SELECT Id, Description FROM " + TableName + " WHERE Description = '" + Description + "'";
            return ExecuteSelect(query);
        }

        public DataTable GetByDescriptionAndId(Technology entity)
        {
            string query = "SELECT Id, Description FROM " + TableName + " WHERE Description = '" + entity.Description + "' AND Id <> " + entity.Id;
            return ExecuteSelect(query);
        }

        public void Insert(Technology entity)
        {
            string query = "INSERT INTO " + TableName + " (Description) VALUES ('" + entity.Description + "')";
            ExecuteCommand(query);
        }

        public void Update(Technology entity)
        {
            string query = "UPDATE " + TableName + " SET Description = '" + entity.Description + "' WHERE Id = " + entity.Id;
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

        public Validate ValidateInsert(Technology entity)
        {
            Validate valid = new Validate();

            if (!string.IsNullOrEmpty(entity.Description))
            {
                DataTable dt = GetByDescription(entity.Description);
                valid.IsValid = !(dt.Rows.Count > 0);
                valid.Message = !valid.IsValid ? "Já existe um item com mesma descrição." : "Item não encontrado";
            }

            return valid;
        }

        public Validate ValidateUpdate(Technology entity)
        {
            Validate valid = new Validate();

            if (entity.Id > 0)
            {
                DataTable dt = GetById(entity.Id);
                valid.IsValid = dt.Rows.Count > 0;
                
                if (valid.IsValid && !string.IsNullOrEmpty(entity.Description))
                {
                    DataTable dt2 = GetByDescriptionAndId(entity);
                    valid.IsValid = !(dt2.Rows.Count > 0);
                    valid.Message = !valid.IsValid ? "Já existe um item com mesma descrição." : "Item não encontrado";
                }else
                    valid.Message = dt.Rows.Count > 0 ? "Esse item já existe." : "Item não encontrado.";
            }

            return valid;
        }
    }
}