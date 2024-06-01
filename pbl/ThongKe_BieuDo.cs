using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ValueObject;

namespace pbl
{
    public partial class ThongKe_BieuDo : Form
    {
        PBL3Entities1 db = new PBL3Entities1 ();
        public ThongKe_BieuDo()
        {
            InitializeComponent();
        }

        private void BieuDo_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(2024,1,1);
            rad_dt_thang.Checked = true;
            btn_ok.PerformClick();
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Thong_Ke();
        }
        public void Set_Font(Chart chart)
        {
            chart.ChartAreas[0].AxisX.TitleFont = new Font("Segoe UI Semibold", 10); 
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Segoe UI Semibold", 10); 
            chart.Legends[0].Font = new Font("Segoe UI Semibold", 10); 
        }
        public void Thong_Ke()
        {
            if (rad_dt_nv.Checked)
            {
                DoanhThu_NhanVien();
            }
            else if (rad_sp_sl.Checked)
            {
                SoLuongDaBan_SanPham();
            }
            else if(rad_dt_thang.Checked)
            {
                DoanhThu_Thang();
            }
            else if(rad_kh_tt.Checked)
            {
                KhachHang_ThanThiet();
            }
        }
        public void DoanhThu_NhanVien()
        {
            chart2.Visible = true;
            chart3.Visible = false;
            chart1.Visible = false;
            chart2.BeginInit();
            chart2.Series.Clear();
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            using (PBL3Entities1 pbl = new PBL3Entities1())
            {
                var doanhThuTheoNhanVien = pbl.HoaDons.Where(x => x.NgayTaoHoaDon >= start && x.NgayTaoHoaDon < end)
                                          .GroupBy(p => p.IDNhanVien)
                                          .Select(group => new
                                          {
                                              IDNhanVien = group.Key,
                                              TongDoanhThu = group.Sum(hd => hd.TongTien)
                                          })
                                          .Join(pbl.NhanViens,
                                                hd => hd.IDNhanVien,
                                                nv => nv.IDNhanVien,
                                                (hd, nv) => new
                                                {
                                                    TenNhanVien = nv.TenNhanVien,
                                                    TongDoanhThu = hd.TongDoanhThu
                                                })
                                          .ToList();
                var series = new Series("Doanh Thu");
                series.ChartType = SeriesChartType.Pie;
                foreach (var item in doanhThuTheoNhanVien)
                {
                    series.Points.AddXY(item.TenNhanVien, item.TongDoanhThu);
                }
                chart2.Series.Add(series);
                chart2.ChartAreas[0].AxisX.Title = "Tên Nhân Viên";
                chart2.ChartAreas[0].AxisY.Title = "Tổng Doanh Thu";
            }
            Set_Font(chart2);
            chart2.EndInit();
        } 
        public void SoLuongDaBan_SanPham()
        {
            chart2.Visible = false;
            chart3.Visible = true;
            chart1.Visible = false;
            chart3.BeginInit();
            chart3.Series.Clear();
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            using (PBL3Entities1 context = new PBL3Entities1())
            {
                var top5SanPhamBanChayNhat = context.ChiTietHoaDons
                    .Join(context.HoaDons,
                        cthd => cthd.IDHoaDon,
                        hd => hd.IDHoaDon,
                        (cthd, hd) => new { cthd.IDChiTiet, cthd.SoLuong, hd.NgayTaoHoaDon })
                     .Where(x => x.NgayTaoHoaDon >= start && x.NgayTaoHoaDon < end)
                    .Join(context.ChiTietSanPhams,
                          cthd => cthd.IDChiTiet,
                          ctsp => ctsp.IDChiTiet,
                          (cthd, ctsp) => new { ctsp.IDSanPham, cthd.SoLuong })
                    .GroupBy(x => x.IDSanPham)
                    .Select(g => new
                    {
                        IDSanPham = g.Key,
                        SoLuongBan = g.Sum(x => x.SoLuong)
                    })
                    .OrderByDescending(x => x.SoLuongBan)
                    .Take(5)
                    .Join(context.SanPhams,
                          x => x.IDSanPham,
                          sp => sp.IDSanPham,
                          (x, sp) => new
                          {
                              sp.Ten,
                              x.SoLuongBan
                          })
                    .ToList();
                var series = new Series("Số Lượng");
                series.ChartType = SeriesChartType.Column;
                foreach (var item in top5SanPhamBanChayNhat)
                {
                    series.Points.AddXY(item.Ten, item.SoLuongBan);
                }
                chart3.Series.Add(series);
                chart3.ChartAreas[0].AxisX.Title = "Tên Sản Phẩm";
                chart3.ChartAreas[0].AxisY.Title = "Số lượng đã bán";
            }
            Set_Font(chart3);
            chart3.EndInit();
        }
        public void DoanhThu_Thang()
        {
            chart2.Visible = false;
            chart3.Visible = false;
            chart1.Visible = true;
            chart1.BeginInit();
            chart1.Series.Clear();
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            using (PBL3Entities1 context = new PBL3Entities1())
            {
                var doanhThuTheoThang = context.ChiTietHoaDons
                    .Join(context.HoaDons,
                          cthd => cthd.IDHoaDon,
                          hd => hd.IDHoaDon,
                          (cthd, hd) => new { cthd.IDChiTiet, hd.TongTien, hd.NgayTaoHoaDon })
                    .Where(x => x.NgayTaoHoaDon >= start && x.NgayTaoHoaDon < end)
                    .GroupBy(x => new { x.NgayTaoHoaDon.Year, x.NgayTaoHoaDon.Month })
                    .Select(g => new
                    {
                        Thang = g.Key.Month,
                        Nam = g.Key.Year,
                        TongDoanhThu = g.Sum(x => x.TongTien)
                    })
                    .OrderBy(x => x.Nam)
                    .ThenBy(x => x.Thang)
                    .ToList();
                var series = new Series("Doanh Thu");
                series.ChartType = SeriesChartType.Line;

                foreach (var item in doanhThuTheoThang)
                {
                    series.Points.AddXY(item.Thang, item.TongDoanhThu);
                }
                chart1.Series.Add(series);
                chart1.ChartAreas[0].AxisX.Title = "Tháng";
                chart1.ChartAreas[0].AxisY.Title = "Tổng Doanh Thu";
            }
            Set_Font(chart1);
            chart1.EndInit();
        }
        public void KhachHang_ThanThiet()
        {
            chart2.Visible = false ;
            chart3.Visible = false;
            chart1.Visible = true;
            chart1.BeginInit();
            chart1.Series.Clear();
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            using (PBL3Entities1 db = new PBL3Entities1())
            {
                var khachhangThanthiet = db.KhachHangs.Join(db.HoaDons,
                          kh => kh.IDKhachHang,
                          hd => hd.IDKhachHang,
                           (kh, hd) => new { kh.Ten, kh.IDKhachHang, hd.TongTien, hd.NgayTaoHoaDon })
                        .Where(x => x.NgayTaoHoaDon >= start && x.NgayTaoHoaDon < end)
                        .GroupBy(x => new { x.IDKhachHang, x.Ten })
                        .Select(g => new
                         {
                                 IDKhachHang = g.Key.IDKhachHang,
                                 TenKhachHang = g.Key.Ten,
                                 TongDoanhThu = g.Sum(x => x.TongTien)
                         }).OrderByDescending(x => x.TongDoanhThu) 
                        .Take(5).ToList();

                var series = new Series("Doanh Thu");
                series.ChartType = SeriesChartType.Column;
                foreach (var item in khachhangThanthiet)
                {
                    series.Points.AddXY(item.TenKhachHang, item.TongDoanhThu);
                }
                chart1.Series.Add(series);
                chart1.ChartAreas[0].AxisX.Title = "Khách Hàng";
                chart1.ChartAreas[0].AxisY.Title = "Tổng Doanh Thu";
            }
            Set_Font(chart1);
            chart1.EndInit();
        }
    }
}
