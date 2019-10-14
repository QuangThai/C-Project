using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG12019.DAL.Entity
{
    public class QuaTrinhHocTap
    {
        public string MaQuaTrinhHocTap { get; set; }
        public int TuNam { get; set; }
        public int DenNam { get; set; }
        public string ThoiGianHoc
        {
            get
            {
                return string.Format("{0} -> {1}", TuNam, DenNam);
            }
        }
        public string HocTai { get; set; }
        public string MaSinhVien { get; set; }
        public virtual SinhVien SinhVien { get; set; }

        /// <summary>
        /// Lấy danh sách quá trình học tập của 1 SINH VIÊN từ MockData     
        /// </summary>
        /// <param name="maSinhVien">Mã sinh viên của sinh viên cần lấy</param>
        /// <returns>Danh sách quá trình học tập</returns>
        public static List<QuaTrinhHocTap> GetList(string maSinhVien)
        {
            List<QuaTrinhHocTap> listQuaTrinhHocTap = new List<QuaTrinhHocTap>();
            for(int i = 0; i < 5; i++)
            {
                QuaTrinhHocTap quaTrinhHocTap = new QuaTrinhHocTap
                {
                    MaQuaTrinhHocTap = i.ToString(),
                    TuNam = 1990 + i,
                    DenNam = 1990 + i + 1,
                    HocTai = "Phan Bội Châu",
                    MaSinhVien = maSinhVien
                };
                listQuaTrinhHocTap.Add(quaTrinhHocTap);
            }
            return listQuaTrinhHocTap;
        }

        /// <summary>
        /// Lấy danh sách quá trình học tập của sinh viên từ File
        /// </summary>
        /// <param name="pathData">Đường dẫn file chứa dữ liệu</param>
        /// <param name="maSinhVien">Mã sinh viên cần lấy dữ liệu</param>
        /// <returns>Danh sách quá trình học tập của sinh viên có mã là maSinhVien</returns>
        public static List<QuaTrinhHocTap> GetListFromFile(string pathData, string maSinhVien)
        {
            var arrayLines = File.ReadAllLines(pathData);
            List<QuaTrinhHocTap> ketQua = new List<QuaTrinhHocTap>();
            foreach(var line in arrayLines)
            {
                var lsValue = line.Split(new char[] { '#' });
                var quaTrinhHocTap = new QuaTrinhHocTap
                {
                    MaSinhVien = lsValue[0],
                    MaQuaTrinhHocTap = lsValue[1],
                    TuNam = Convert.ToInt32(lsValue[2]),
                    DenNam = Int32.Parse(lsValue[3]),
                    HocTai = lsValue[4]
                };
                if (quaTrinhHocTap.MaSinhVien == maSinhVien)
                    ketQua.Add(quaTrinhHocTap);
            }
            return ketQua;
        }
    }
}
