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
            MaHanhKhach = string.Empty;
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
        [Required(ErrorMessage = "Mã hành khách không được để trống.")]
        public string MaHanhKhach { get; set; }

        [Display(Name = "Ngày Đặt Vé")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày đặt vé không được để trống.")]
        public DateTime NgayDatVe { get; set; }

        [Display(Name = "Giá Vé")]
        [Required(ErrorMessage = "Giá vé không được để trống.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá vé phải lớn hơn hoặc bằng 0.")]
        public decimal GiaVe { get; set; }

        [Display(Name = "Mã Nhân Viên")]
        public string? MaNhanVien { get; set; } // Có thể null nếu không bắt buộc

        [Display(Name = "Mã Sân Bay Đi")]
        public string? MaSanBayDi { get; set; } // Có thể null nếu không bắt buộc

        [Display(Name = "Mã Sân Bay Đến")]
        public string? MaSanBayDen { get; set; } // Có thể null nếu không bắt buộc

        [Display(Name = "Mã Máy Bay")]
        public string? MaMayBay { get; set; } // Có thể null nếu không bắt buộc

        [Display(Name = "Mã Chứng Nhận")]
        public string? MaChungNhan { get; set; } // Có thể null nếu không bắt buộc

        [Display(Name = "Mã Phân Công")]
        public string? MaPhanCong { get; set; } // Có thể null nếu không bắt buộc

        // Thuộc tính không lưu trữ trong cơ sở dữ liệu
    }
}
