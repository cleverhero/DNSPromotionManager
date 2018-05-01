using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DNSPromotionManager.Models
{
    public enum TableItemEvent
    {
        Delete,
        Edit,
        Create,
    } 

    public class Table
    {
        public String Name;
        public String Caption;
        public List<Column> Columns;
        public DataTable Items;

        public Table(String name, String caption)
        {
            Name    = name;
            Caption = caption;
            Columns = new List<Column>();
            Items   = new DataTable();
        }

        public Table AddColumn(String name, String caption, bool isVisible = true)
        {
            Columns.Add( new SimpleColumn(name, caption, isVisible) );
            return this;
        }

        public Table AddColumn(String name, 
                               String caption, 
                               String ptable, 
                               String pcol, 
                               String ascol,
                               String[] visiblecols,
                               bool isVisible = true)
        {
            Link link = new Link(name, caption, ptable, pcol, ascol, visiblecols, isVisible);
            Columns.Add(link);
            return this;
        }

        public Table Load()
        {
            String connect = TableManager.getInstance().ConnectionString;

            QueryBuilder builder = new QueryBuilder(Name);
            foreach (Column col in Columns) col.Select(builder);
            String query = builder.Build();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                Items = ds.Tables[0];
            }

            return this;
        }

    }
}
