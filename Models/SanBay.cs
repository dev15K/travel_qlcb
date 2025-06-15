using System.ComponentModel.DataAnnotations;

namespace QLCB.Models
{
	public class SanBay
	{
		[Key]
		[StringLength(5)]
		public string MaSanBay { get; set; }

		[Required]
		[StringLength(100)]
		public string TenSanBay { get; set; }

		[Required]
		[StringLength(50)]
		public string ThanhPho { get; set; }

		[Required]
		[StringLength(50)]
		public string QuocGia { get; set; }

		// Navigation properties
		public ICollection<ChuyenBay>? ChuyenBaysDi { get; set; }
		public ICollection<ChuyenBay>? ChuyenBaysDen { get; set; }
	}
}
