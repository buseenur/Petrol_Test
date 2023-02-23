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

namespace Test_Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection("Data Source=DESKTOP-70RL3KF\\SQLEXPRESS;Initial Catalog=TestBenzin;Integrated Security=True");

        void listele1()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from tblhareket", baglanti);
            SqlDataAdapter da=new SqlDataAdapter(komut);
            DataTable dt=new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
       
        void listele()
        {
            //kurşunsuz95
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select*from TBLBENZIN where petroltur='kurşunsuz95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblKursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                LblKursnszLt.Text = dr[4] + "LT".ToString();
            }
            baglanti.Close();
            //maxdiesel
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select*from tblbenzın where petroltur='maxdiesel'", baglanti);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblMaxDiesel.Text = dr1[3].ToString();
                progressBar4.Value = int.Parse(dr1[4].ToString());
                LblMaxDieselLt.Text = dr1[4] + "LT".ToString();
            }
            baglanti.Close();
            //prodiesel
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select*from tblbenzın where petroltur='prodiesel'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblProDiesel.Text = dr2[3].ToString();
                progressBar3.Value = int.Parse(dr2[4].ToString());
                LblProDieselLt.Text = dr2[4] + "LT".ToString();
            }
            baglanti.Close();
            //gaz
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select*from tblbenzın where petroltur='gaz'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblGaz.Text = dr3[3].ToString();
                progressBar2.Value = int.Parse(dr3[4].ToString());
                LblGazLt.Text = dr3[4] + "LT".ToString();
            }
            baglanti.Close();
            //kasa
            baglanti.Open();
            SqlCommand kmtkasa = new SqlCommand("select*from tblkasa", baglanti);
            SqlDataReader drkasa=kmtkasa.ExecuteReader();
            while (drkasa.Read())
            {
                LblKasa.Text = drkasa[0].ToString();
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            listele1();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95=Convert.ToDouble(LblKursunsuz95.Text);
            litre=Convert.ToDouble(numericUpDown1.Value);
            tutar=kursunsuz95*litre;
            TxtKursunsuzFyt.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double maxdiesel, litre, tutar;
            maxdiesel=Convert.ToDouble(LblMaxDiesel.Text);
            litre=Convert.ToDouble(numericUpDown2.Value);
            tutar=maxdiesel*litre;
            TxtMaxDieselFyt.Text=tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double prodiesel, litre, tutar;
            prodiesel = Convert.ToDouble(LblProDiesel.Text);
            litre=Convert.ToDouble(numericUpDown3.Value);
            tutar= prodiesel*litre;
            TxtProDieselFyt.Text=(tutar.ToString());
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double gaz,litre,tutar;
            gaz=Convert.ToDouble(LblGaz.Text);
            litre=Convert.ToDouble(numericUpDown4.Value);
            tutar=gaz*litre;
            TxtGazFyt.Text=(tutar.ToString());
        }

        private void BtnDepoDoldur_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value!=0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (plaka,benzınturu,lıtre,fıyat,aractur) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
                komut.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                komut.Parameters.AddWithValue("@p2","Kurşunsuz95");
                komut.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtKursunsuzFyt.Text));
                komut.Parameters.AddWithValue("@p5",TxtAracTur.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
               

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set mıktar=mıktar+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1",decimal.Parse(TxtKursunsuzFyt.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();
                

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzın set stok=stok-@p1 where petroltur='kurşunsuz95'",baglanti);
                komut3.Parameters.AddWithValue("@p1",numericUpDown1.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close() ;
                MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            if (numericUpDown2.Value!=0)
            {
                baglanti.Open();
                SqlCommand komutmx = new SqlCommand("insert into tblhareket (plaka,benzınturu,lıtre,fıyat,aractur) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
                komutmx.Parameters.AddWithValue("@p1",TxtPlaka.Text);
                komutmx.Parameters.AddWithValue("@p2", "MaxDisel");
                komutmx.Parameters.AddWithValue("@p3",numericUpDown2.Value);
                komutmx.Parameters.AddWithValue("@p4",decimal.Parse(TxtMaxDieselFyt.Text));
                komutmx.Parameters.AddWithValue("@p5", TxtAracTur.Text);
                komutmx.ExecuteNonQuery();
                baglanti.Close() ;

                baglanti.Open();
                SqlCommand komutmx1 = new SqlCommand("update tblkasa set mıktar=mıktar+@p1", baglanti);
                komutmx1.Parameters.AddWithValue("@p1",decimal.Parse(TxtMaxDieselFyt.Text));
                komutmx1.ExecuteNonQuery() ;
                baglanti.Close() ;

                baglanti.Open() ;
                SqlCommand komutmx2 = new SqlCommand("update tblbenzın set stok=stok-@p1 where petroltur='maxdiesel'", baglanti);
                komutmx2.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                komutmx2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

                if (numericUpDown3.Value!=0)
                {
                    baglanti.Open();
                    SqlCommand komutpr = new SqlCommand("insert into tblhareket (plaka,benzınturu,lıtre,fıyat,aractur) values(@p1,@p2,@p3,@p4,@p5)", baglanti);
                    komutpr.Parameters.AddWithValue("@p1",TxtPlaka.Text);
                    komutpr.Parameters.AddWithValue("@p2", "ProDiesel");
                    komutpr.Parameters.AddWithValue("@p3",numericUpDown3.Value);
                    komutpr.Parameters.AddWithValue("@p4", decimal.Parse(TxtProDieselFyt.Text));
                    komutpr.Parameters.AddWithValue("@p5", TxtAracTur.Text);
                    komutpr.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    SqlCommand komutpr1 = new SqlCommand("update tblkasa set mıktar=mıktar+@p1", baglanti);
                    komutpr1.Parameters.AddWithValue("@p1", decimal.Parse(TxtProDieselFyt.Text));
                    komutpr1.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    SqlCommand komutpr2 = new SqlCommand("update tblbenzın set stok=stok-@p1 where petroltur='prodiesel'", baglanti);
                    komutpr2.Parameters.AddWithValue("@p1",numericUpDown3.Value);
                    komutpr2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                if (numericUpDown4.Value != 0)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into tblhareket (plaka,benzınturu,lıtre,fıyat,aractur) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
                    komut.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                    komut.Parameters.AddWithValue("@p2", "Gaz");
                    komut.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                    komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtGazFyt.Text));
                    komut.Parameters.AddWithValue("@p5", TxtAracTur.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();


                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("update tblkasa set mıktar=mıktar+@p1", baglanti);
                    komut2.Parameters.AddWithValue("@p1", decimal.Parse(TxtGazFyt.Text));
                    komut2.ExecuteNonQuery();
                    baglanti.Close();


                    baglanti.Open();
                    SqlCommand komut3 = new SqlCommand("update tblbenzın set stok=stok-@p1 where petroltur='gaz'", baglanti);
                    komut3.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listele1();
        }
    }
}
