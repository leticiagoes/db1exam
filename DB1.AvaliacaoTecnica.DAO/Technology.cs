using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.AvaliacaoTecnica.DAO
{
    public class Technology
    {
        OleDbConnection connection;
        OleDbDataAdapter adapter;
        DataSet ds;
        OleDbCommand cmd;

        public Technology(string path)
        {
            connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @"\DB1_Candidates.mdb");
        }

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            adapter = new OleDbDataAdapter("SELECT Id, Description FROM Technology", connection);
            ds = new DataSet();

            adapter.Fill(ds, "Technology");
            if (ds.Tables["Technology"].Rows.Count > 0)
            {
                dt = ds.Tables["Technology"];
            }

            return dt;
        }
    }
}
