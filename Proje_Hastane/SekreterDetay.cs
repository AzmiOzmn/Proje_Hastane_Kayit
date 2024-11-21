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

namespace Proje_Hastane
{
    public partial class SekreterDetay : Form
    {
        public SekreterDetay()
        {
            InitializeComponent();
        }
        public string tcno;
        sqlBaglantisi bgl = new sqlBaglantisi(); 
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tcno;

            //Ad Soyad
            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTC=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader d1 = komut1.ExecuteReader();
            while (d1.Read())
            {
                LblAdSoyad.Text = d1[0].ToString();
            }
            bgl.baglanti().Close();

            // Branşları DataGride Aktarma

            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // Doktorları Dt ye aktarma 

            DataTable dk1 = new DataTable();
            SqlDataAdapter dl1 = new SqlDataAdapter("Select (DoktorAd +' '+ DoktorSoyad) as 'Doktorlar',DoktorBrans From Tbl_Doktorlar", bgl.baglanti());
            dl1.Fill(dk1);
            dataGridView2.DataSource = dk1;

            // Branşı Comboxa Aktarma

            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutsave = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komutsave.Parameters.AddWithValue("@p1", MskTarih.Text);
            komutsave.Parameters.AddWithValue("@p2", MskSaat.Text);
            komutsave.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komutsave.Parameters.AddWithValue("@p4", CmbDoktor.Text);
            komutsave.ExecuteNonQuery();
            bgl.baglanti().Close(); 
            MessageBox.Show("Randevu Oluştururuldu","Bilgilendirme",MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnRandevuListesi_Click(object sender, EventArgs e)
        {
           FrmRandevuListesi frs = new FrmRandevuListesi();
            frs.Show();
            this.Hide();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();

        }

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",RchDuyuru.Text);
            komut.ExecuteNonQuery(); 
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");

        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            DoktorPaneli fr = new DoktorPaneli();
            fr.Show();
        }

        private void BtnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBransPaneli fr = new FrmBransPaneli();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
          
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }
    }
}
