using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApotekMutiaraIbu
{
    public partial class FRM_MENU_UTAMA : Form
    {
        public FRM_MENU_UTAMA()
        {
            InitializeComponent();
        }

        private void dataBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_BARANG FBARANG = new FRM_BARANG();
            FBARANG.MdiParent = this;
            FBARANG.Show();
        }

        private void dataSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_SUPPLIER FPSUPPLIER = new FRM_SUPPLIER();
            FPSUPPLIER.MdiParent = this;
            FPSUPPLIER.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FRM_LOGIN FLOGIN = new FRM_LOGIN();
            FLOGIN.Show();
        }

        private void penerimaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_MASUK FTERIMA = new FRM_MASUK();
            FTERIMA.MdiParent = this;
            FTERIMA.Show();
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
