using AppG12019.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppG12019
{
    public partial class frmQuaTrinhHocTapChiTiet : Form
    {
        public frmQuaTrinhHocTapChiTiet(QuaTrinhHocTap  quaTrinhHocTap)
        {
            InitializeComponent();
            if(quaTrinhHocTap == null)
            {
                this.Text = "Thêm mới quá trình học tập";
            }
            else
            {
                this.Text = "Chỉnh sửa quá trình học tập";
                numTuNam.Value = quaTrinhHocTap.TuNam;
                numDenNam.Value = quaTrinhHocTap.DenNam;
                txtHocTai.Text = quaTrinhHocTap.HocTai;
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NumDenNam_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
