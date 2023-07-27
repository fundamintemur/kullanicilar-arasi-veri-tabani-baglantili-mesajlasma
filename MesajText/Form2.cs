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

namespace MesajText
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        SqlConnection bgl = new SqlConnection(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");
        public string numara;
        void GelenKutusu()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select*from TBLMESAJLAR where ALICI=" + numara, bgl);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void GidenKutusu()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select*from TBLMESAJLAR where GONDEREN=" + numara, bgl);
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            GelenKutusu();
            GidenKutusu();
            //AD VE SOYADI ÇEKME
            bgl.Open();
            SqlCommand komut = new SqlCommand("select AD,SOYAD from TBLKISILER where NUMARA=@p1", bgl);
            komut.Parameters.AddWithValue("@p1", LblNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] +" "+ dr[1];
            }
            bgl.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komutEkle = new SqlCommand("insert into TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) values(@p1,@p2,@p3,@p4)", bgl);
            komutEkle.Parameters.AddWithValue("@p1", numara);
            komutEkle.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komutEkle.Parameters.AddWithValue("@p3", textBox1.Text);
            komutEkle.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komutEkle.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Mesaj Sisteme Eklendi");
            GidenKutusu();

        }
    }
}
