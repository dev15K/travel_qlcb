using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace QLCB.Models
{
	public class MayBay
	{
		[Key]
		[StringLength(5)]
		public string MaMayBay { get; set; }

		[Required]
		[StringLength(50)]
		public string LoaiMayBay { get; set; }

		public int TamBay { get; set; }

		// Navigation property
		public ICollection<ChuyenBay>? ChuyenBays { get; set; }
	}
}
