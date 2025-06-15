using System.ComponentModel.DataAnnotations;
namespace QLCB.Models
{
	public class ChungNhan
	{
		[Key]
		[StringLength(10)]
		public string MaChungNhan { get; set; }

		[Required]
		[StringLength(100)]
		public string TenChungNhan { get; set; }

		[Required]
		public DateTime NgayCap { get; set; }

		[Required]
		public DateTime NgayHetHan { get; set; }

		[Required]
		public string MaNhanVien { get; set; }
	}
}
