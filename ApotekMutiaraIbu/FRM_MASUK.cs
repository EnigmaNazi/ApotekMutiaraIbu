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

namespace ApotekMutiaraIbu
{
    public partial class FRM_MASUK : Form
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-6PC0I83;Database=ApotekMutiaraIbu;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        SqlDataReader rd;

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_masuk", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void DisplayDataSupplier()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_supplier", con);
            adapt.Fill(dt);
            dg_supplier.DataSource = dt;
            con.Close();
        }

        private void DisplayDataBarang()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_barang", con);
            adapt.Fill(dt);
            dg_barang.DataSource = dt;
            con.Close();
        }

        private void otomatis()
        {
            long hitung;
            string urut;

            con.Open();
            cmd = new SqlCommand("select no_masuk from tbl_masuk_detail where no_masuk in(select max(no_masuk) from tbl_masuk_detail) order by no_masuk desc", con);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["no_masuk"].ToString().Length - 4, 4)) + 1;

                string joinstr = "0000" + hitung;



                urut = "EBM" + joinstr.Substring(joinstr.Length - 4, 4);

            }
            else
            {
                urut = "EBM0001";
            }
            rd.Close();
            txt_no_masuk.Text = urut;
            con.Close();
        }

        private void cleartext()
        {
            txt_kode_supplier.Text = "";
            txt_nama_supplier.Text = "";
            txt_no_telp.Text = "";
            txt_alamat.Text = "";
            txt_kode_barang.Text = "";
            txt_nama_barang.Text = "";
            txt_harga.Text = "0";
            txt_stock.Text = "0";
            txt_jumlah_masuk.Text = "";
            txt_keterangan.Text = "";
            txt_no_masuk.Enabled = false;
            txt_kode_supplier.Enabled = false;
            txt_kode_barang.Enabled = false;
            txt_stock.Enabled = false;
            txt_harga.Enabled = false;
        }

        public FRM_MASUK()
        {
            InitializeComponent();
            DisplayData();
            DisplayDataSupplier();
            DisplayDataBarang();
            cleartext();
        }
        
        private void btn_new_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Enabled = false;
            txt_no_masuk.Enabled = false;
            cleartext();
            otomatis();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                int stock, jumlah_masuk, stock_akhir;
                stock = int.Parse(txt_stock.Text);
                jumlah_masuk = int.Parse(txt_jumlah_masuk.Text);
                stock_akhir = stock + jumlah_masuk;

                try
                {
                    if (txt_no_masuk.Text != "" && txt_kode_barang.Text != "" && txt_kode_supplier.Text != "" && txt_jumlah_masuk.Text != "")
                    {
                        cmd = new SqlCommand("update tbl_barang set stock=@stock_akhir where kode_barang=@kode_barang", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@stock_akhir", stock_akhir);
                        cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("gagal simpan");
                    }
                    if (txt_no_masuk.Text != "" && txt_kode_barang.Text != "" && txt_kode_supplier.Text != "" && txt_jumlah_masuk.Text != "")
                    {
                        cmd = new SqlCommand("insert into tbl_masuk(no_masuk,tgl_masuk,kode_supplier,kode_barang,jumlah_masuk,keterangan) values(@no_masuk,@tgl_masuk,@kode_supplier,@kode_barang,@jumlah_masuk,@keterangan)", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@no_masuk", txt_no_masuk.Text);
                        cmd.Parameters.Add("@tgl_masuk", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
                        cmd.Parameters.AddWithValue("@kode_supplier", txt_kode_supplier.Text);
                        cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                        cmd.Parameters.AddWithValue("@jumlah_masuk", txt_jumlah_masuk.Text);
                        cmd.Parameters.AddWithValue("@keterangan", txt_keterangan.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data berhasil disimpan");
                        DisplayData();
                        cleartext();
                    }
                    else
                    {
                        MessageBox.Show("gagal simpan");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Kode barang tidak boleh sama, pilih barang yang lain atau hapus dahulu barang yang akan diubah!");
                }
             }
             catch (Exception)
             {
                 MessageBox.Show("Kolom masih kosong!");
             }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int stock, jumlah_masuk, stock_akhir;
            stock = int.Parse(txt_stock.Text);
            jumlah_masuk = int.Parse(txt_jumlah_masuk.Text);
            stock_akhir = stock - jumlah_masuk;

            if (txt_no_masuk.Text != "" && txt_kode_barang.Text != "" && txt_kode_supplier.Text != "" && txt_jumlah_masuk.Text != "")
            {
                cmd = new SqlCommand("update tbl_barang set stock=@stock_akhir where kode_barang=@kode_barang", con);
                con.Open();
                cmd.Parameters.AddWithValue("@stock_akhir", stock_akhir);
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("gagal simpan");
            }

            if (txt_nama_barang.Text != "")
            {
                cmd = new SqlCommand("delete tbl_masuk where kode_barang=@kode_barang", con);
                con.Open();
                cmd.Parameters.AddWithValue("@kode_barang", txt_kode_barang.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("berhasil hapus");
                DisplayData();
                cleartext();
            }
            else
            {
                MessageBox.Show("gagal hapus");
            }
        }

        private void pilih_supplier_Click(object sender, EventArgs e)
        {
            group_cari_supplier.Visible = true;
        }

        private void pilih_barang_Click(object sender, EventArgs e)
        {
            group_cari_barang.Visible = true;
        }

        private void dg_supplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_kode_supplier.Text = dg_supplier.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_nama_supplier.Text = dg_supplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_no_telp.Text = dg_supplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_alamat.Text = dg_supplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                DisplayDataSupplier();
                group_cari_supplier.Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Klik isi tabel!");
            }
        }

        private void dg_barang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_kode_barang.Text = dg_barang.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_nama_barang.Text = dg_barang.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_harga.Text = dg_barang.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_stock.Text = dg_barang.Rows[e.RowIndex].Cells[3].Value.ToString();
                DisplayDataBarang();
                group_cari_barang.Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Klik isi tabel kode barang atau nama barang!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_no_masuk.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                dg_supplier_CellContentClick(sender, e);
                dg_barang_CellContentClick(sender, e);
                txt_kode_supplier.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_kode_barang.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_jumlah_masuk.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_keterangan.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                DisplayData();
            }
            catch (Exception)
            {
                MessageBox.Show("Klik isi tabel!");
            }
        }

        private void button_batal_supplier_Click(object sender, EventArgs e)
        {
            group_cari_supplier.Visible = false;
        }

        private void button_batal_barang_Click(object sender, EventArgs e)
        {
            group_cari_barang.Visible = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("insert into tbl_masuk_detail select * from tbl_masuk where no_masuk=@no_masuk", con);
                con.Open();
                cmd.Parameters.AddWithValue("@no_masuk", txt_no_masuk.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Berhasil simpan");
                cmd = new SqlCommand("delete tbl_masuk", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayData();
                cleartext();
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal simpan!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tbl_supplier WHERE nama_supplier like '%" + textBox1.Text + "%'";
            cmd = new SqlCommand(query, con);
            adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dg_supplier.DataSource = dt;
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tbl_barang WHERE kode_barang like '%" + textBox14.Text + "%'";
            cmd = new SqlCommand(query, con);
            adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dg_barang.DataSource = dt;
        }
    }
}
