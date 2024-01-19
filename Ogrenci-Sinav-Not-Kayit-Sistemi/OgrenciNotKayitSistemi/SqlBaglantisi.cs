using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OgrenciNotKayitSistemi
{
    internal class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("server=localhost; Initial Catalog=DbNotKayit;Integrated Security=SSPI");
            baglan.Open();
            return baglan;
        }
    }
}
