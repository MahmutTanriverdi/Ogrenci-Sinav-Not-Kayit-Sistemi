using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OgrenciNotKayitSistemi
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string numara;
        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            string durum;
            LblNumara.Text = numara;
            SqlCommand komut = new SqlCommand("Select * from TBLDERS where OGRNUMARA=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2] + " " + dr[3];
                LblSinav1.Text = dr[4].ToString();
                LblSinav2.Text = dr[5].ToString();
                LblSinav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                durum = dr[8].ToString();
                if(durum == "True")
                {
                    LblDurum.Text = "Geçti";
                }
                else
                {
                    LblDurum.Text = "Kaldı";
                }
            }
            bgl.baglanti().Close();

        }
    }
}
