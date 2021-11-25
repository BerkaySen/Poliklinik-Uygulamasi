using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace PoliklinikProgrami
{

    class sinif
    {

        
       /* public string hast(string sor)
        {

        /*    bag.Open();
            ds1.Clear();
            kayit = new OleDbDataAdapter("select * from HastaBilgi", bag);
            kayit.Fill(ds1, "HastaBilgi");
           // dataGridView1.DataSource = ds1.Tables["HastaBilgi"];
            bag.Close();
            return ds1.Tables["HastaBilgi"].ToString();*/
        //}
        public void tüm_listele()
        {
            OleDbConnection bag = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hastahane.mdb");
            OleDbCommand kmt = new OleDbCommand();

            OleDbDataAdapter kayit = new OleDbDataAdapter();
            DataTable tablo = new DataTable();
            DataSet ds1 = new DataSet();
            
        }

    }
}
