using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLCB.Models;

public class DatVe
{
    [Key]
    [StringLength(450)]
    public string MaVeBay { get; set; }

    [Required]
    public string MaChuyenBay { get; set; }

    [Required]
    public string MaHanhKhach { get; set; }

    [Required]
    public DateTime NgayDatVe { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal GiaVe { get; set; }

    // Các thuộc tính liên quan (không bắt buộc)
    public string? MaNhanVien { get; set; }
    public string? MaSanBayDi { get; set; }
    public string? MaSanBayDen { get; set; }
    public string? MaMayBay { get; set; }
    public string? MaChungNhan { get; set; }
    public string? MaPhanCong { get; set; }
}