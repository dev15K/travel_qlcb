using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLCB.Models
{
    public class VeBay
    {
        public VeBay()
        {
            MaVeBay = string.Empty;
            MaChuyenBay = string.Empty;
            MaHanhKhach = null;
            NgayDatVe = DateTime.Now;
            GiaVe = 0;
        }

        [Key]
        [Display(Name = "Mã Vé Bay")]
        [Required(ErrorMessage = "Mã vé bay không được để trống.")]
        public string MaVeBay { get; set; }

        [Display(Name = "Mã Chuyến Bay")]
        [Required(ErrorMessage = "Mã chuyến bay không được để trống.")]
        public string MaChuyenBay { get; set; }

        [Display(Name = "Mã Hành Khách")]
        public string? MaHanhKhach { get; set; }

        [Display(Name = "Ngày Đặt Vé")]
        [DataType(DataType.Date)]
        public DateTime NgayDatVe { get; set; }

        [Display(Name = "Giá Vé")]
        [Required(ErrorMessage = "Giá vé không được để trống.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá vé phải lớn hơn hoặc bằng 0.")]
        public decimal GiaVe { get; set; }

        [Display(Name = "Mã Nhân Viên")]
        public string? MaNhanVien { get; set; }

        [Display(Name = "Mã Sân Bay Đi")]
        public string? MaSanBayDi { get; set; }

        [Display(Name = "Mã Sân Bay Đến")]
        public string? MaSanBayDen { get; set; }

        [Display(Name = "Mã Máy Bay")]
        public string? MaMayBay { get; set; }

        [Display(Name = "Mã Chứng Nhận")]
        public string? MaChungNhan { get; set; }

        [Display(Name = "Mã Phân Công")]
        public string? MaPhanCong { get; set; }
    }
}