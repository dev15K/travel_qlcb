using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QLCB.Models
{
    public class ChuyenBay : IValidatableObject
    {
        [Key]
        [Required(ErrorMessage = "Mã chuyến bay không được để trống")]
        public string MaChuyenBay { get; set; }

        [Required(ErrorMessage = "Tên chuyến bay không được để trống")]
        public string TenChuyenBay { get; set; }

        [Required(ErrorMessage = "Thời gian khởi hành không được để trống")]
        [Display(Name = "Thời gian khởi hành")]
        public DateTime ThoiGianKhoiHanh { get; set; }

        [Required(ErrorMessage = "Thời gian đến không được để trống")]
        [Display(Name = "Thời gian đến")]
        public DateTime ThoiGianDen { get; set; }

        [Required(ErrorMessage = "Số ghế không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số ghế không được âm")]
        public int SoGhe { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn máy bay")]
        public string MaMayBay { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sân bay đi")]
        public string MaSanBayDi { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sân bay đến")]
        public string MaSanBayDen { get; set; }

        // Navigation properties
        [ValidateNever]
        public MayBay MayBay { get; set; }

        [ValidateNever]
        [ForeignKey("MaSanBayDi")]
        public SanBay SanBayDi { get; set; }

        [ValidateNever]
        [ForeignKey("MaSanBayDen")]
        public SanBay SanBayDen { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ThoiGianDen < ThoiGianKhoiHanh)
            {
                yield return new ValidationResult(
                    "Thời gian đến không thể nhỏ hơn thời gian khởi hành.",
                    new[] { nameof(ThoiGianDen) });
            }
        }
    }
}
