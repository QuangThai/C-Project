using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG12019.DAL.Entity
{
    public class SinhVien
    {
        public string MaSinhVien { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public SEX GioiTinh { get; set; }
        public string QueQuan { get; set; }
        public virtual ICollection<QuaTrinhHocTap> ListQuaTrinhHocTap { get; set; }

        /// <summary>
        /// Lấy sinh viên từ Mock Data
        /// </summary>
        /// <param name="maSinhVien"></param>
        /// <returns></returns>
        public static SinhVien Get(string maSinhVien)
        {
            SinhVien sinhVien = new SinhVien
            {
                MaSinhVien = maSinhVien,
                Ho = "Nguyễn",
                Ten = "Zũng",
                NgaySinh = DateTime.Now.Date,
                QueQuan = "TT Huế",
                GioiTinh = SEX.Male,
            };
            sinhVien.ListQuaTrinhHocTap = QuaTrinhHocTap.GetList(maSinhVien);
            return sinhVien;
        }

        /// <summary>
        /// Lấy sinh viên từ File datastudent.txt
        /// </summary>
        /// <param name="maSinhVien"></param>
        /// <returns></returns>
        public static SinhVien GetFromFile(string pathDataFile, string maSinhVien)
        {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            var arrayLines = File.ReadAllLines(pathDataFile);
            foreach (var line in arrayLines)
            {
                var lsValue = line.Split(new char[] { '#' });
                var sinhVien = new SinhVien
                {
                    MaSinhVien = lsValue[0],
                    Ho = lsValue[1],
                    Ten = lsValue[2],
                    GioiTinh = lsValue[3] ==
                        "Male" ? SEX.Male : (lsValue[3] == "Female" ? SEX.Female : SEX.Other),
                    NgaySinh = DateTime.ParseExact(lsValue[4], "yyyy-MM-dd", cultureInfo),
                    QueQuan = lsValue[5]
                };
                if (sinhVien.MaSinhVien == maSinhVien)
                    return sinhVien;
            }
            return null;
        }
    }
    public enum SEX
    {
        Female, Male, Other
    }
}
