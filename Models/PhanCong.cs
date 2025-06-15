using System.ComponentModel.DataAnnotations;
namespace QLCB.Models
{
	public class PhanCong
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string MaChuyenBay { get; set; }

		[Required]
		public string MaNhanVien { get; set; }

		[Required]
		public DateTime NgayPhanCong { get; set; }
	}
}
