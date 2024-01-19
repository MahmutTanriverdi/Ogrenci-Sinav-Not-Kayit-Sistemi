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
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLDERS", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            listele();
            SqlCommand komut = new SqlCommand("Select Count(*) from TBLDERS where DURUM = 1", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblGecenSayisi.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
            dr.Close();

            SqlCommand komut1 = new SqlCommand("Select Count(*) from TBLDERS where DURUM = 0", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKalanSayisi.Text = dr1[0].ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into TBLDERS (OGRNUMARA, OGRAD, OGRSOYAD ) " +
                "values (@p1, @p2, @p3)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgrAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtOgrSoyad.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MskNumara.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtOgrAd.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtOgrSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(TxtSinav1.Text);
            s2 = Convert.ToDouble(TxtSinav2.Text);
            s3 = Convert.ToDouble(TxtSinav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text = ortalama.ToString("0.##");

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            SqlCommand komut = new SqlCommand("Update TBLDERS set OGRS1 = @p1, OGRS2 = @p2," +
                " OGRS3 = @p3, ORTALAMA = @p4, DURUM = @p5 where OGRNUMARA = @p6", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtSinav1.Text);
            komut.Parameters.AddWithValue("@p2", TxtSinav2.Text);
            komut.Parameters.AddWithValue("@p3", TxtSinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", MskNumara.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Notları Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
    }
}
