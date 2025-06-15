using System.ComponentModel.DataAnnotations;

namespace QLCB.Models
{
    public class NhanVien
    {
        [Key]
        [StringLength(5)]
        public String MaNhanVien { get; set; }
        [Required]
        [StringLength(100)]
        public String HoTen { get; set; }
        [Required]
        public float Luong { get; set; }
    }
}
