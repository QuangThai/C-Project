using AppG12019.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppG12019
{
    public partial class frmThongTinhSinhVien : Form
    {
        string pathAvatarFolder;
        string pathAvatarImage;
        string pathDataStudent;
        string pathDataLearningHistory;
        SinhVien sinhVien;
        public frmThongTinhSinhVien(string maSinhVien)
        {
            InitializeComponent();
            picAvatar.AllowDrop = true;
            pathAvatarFolder = Application.StartupPath + @"\avatar";
            pathAvatarImage = pathAvatarFolder + @"\avatar.png";
            pathDataStudent = Application.StartupPath + @"\Data\student.data";
            pathDataLearningHistory = Application.StartupPath + @"\Data\learninghistory.data";
            if (File.Exists(pathAvatarImage))
            {
                showImageAvatar(pathAvatarImage);
            }

            dgvQuaTrinhHocTap.AutoGenerateColumns = false;
            sinhVien = SinhVien.GetFromFile(pathDataStudent, maSinhVien);
            
            if (sinhVien == null)
            {
                throw new Exception("Sinh viên có mã: " + maSinhVien + " không tồn tại");
            }
            else
            {
                sinhVien.ListQuaTrinhHocTap = QuaTrinhHocTap.GetListFromFile(pathDataLearningHistory, maSinhVien);
                txtMaSinhVien.Text = sinhVien.MaSinhVien;
                txtHo.Text = sinhVien.Ho;
                txtTen.Text = sinhVien.Ten;
                dtpNgaySinh.Value = sinhVien.NgaySinh;
                chkNam.Checked = sinhVien.GioiTinh == SEX.Male;
                txtQueQuan.Text = sinhVien.QueQuan;

                bdsQuaTrinhHocTap.DataSource = sinhVien.ListQuaTrinhHocTap;
                dgvQuaTrinhHocTap.DataSource = bdsQuaTrinhHocTap;
                lblTongSoMuc.Text = string.Format("{0}", sinhVien.ListQuaTrinhHocTap.Count);
            }

        }
        void showImageAvatar(string path, bool saveOption = false)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            picAvatar.Image = Image.FromStream(fileStream);
            if (saveOption)
            {
                picAvatar.Image.Save(pathAvatarImage);
            }
            fileStream.Close();
        }

        private void LnkChooseAvatar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            #region Chọn ảnh và lưu vào thư mục Avatar
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh đại diện";
            openFileDialog.Filter = "Bạn phải chọn file ảnh (*.png, *.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;
                showImageAvatar(fileName, true);
            }
            #endregion
        }



        private void PicAvatar_DragDrop(object sender, DragEventArgs e)
        {
            try
            {

                var lsFile = (string[])e.Data.GetData(DataFormats.FileDrop);

                showImageAvatar(lsFile.FirstOrDefault(), true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng chọn file ảnh");
            }
        }

        private void PicAvatar_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void MniXoaAnhDaiDien_Click(object sender, EventArgs e)
        {
            picAvatar.Image = Properties.Resources.avatar;

            if (File.Exists(pathAvatarImage))
                File.Delete(pathAvatarImage);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn xóa?",
                "Thông báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
                ) == System.Windows.Forms.DialogResult.OK) ;
            {
                var quaTrinhHocTap = bdsQuaTrinhHocTap.Current as QuaTrinhHocTap;
                //QuaTrinhHocTap.Remove(pathDataLearningHistory, quaTrinhHocTap.MaQuaTrinhHocTap);
                // viết code xóa dữ liệu
                MessageBox.Show("Đã xóa dữ liệu có mã là : " + quaTrinhHocTap.MaQuaTrinhHocTap,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            var f = new frmQuaTrinhHocTapChiTiet(null);
            f.ShowDialog();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            var quaTrinhHocTap = bdsQuaTrinhHocTap.Current as QuaTrinhHocTap;
            if(quaTrinhHocTap !=null)
            {
                var f = new frmQuaTrinhHocTapChiTiet(quaTrinhHocTap);
                f.ShowDialog();
            }
        }
    }
}
