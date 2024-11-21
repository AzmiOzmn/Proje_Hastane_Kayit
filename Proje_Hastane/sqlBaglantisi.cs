using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    internal class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-65IOJF7;Initial Catalog=HastaneProje;Integrated Security=True;");
            baglan.Open();
            return baglan;

             
        }
    }
}
