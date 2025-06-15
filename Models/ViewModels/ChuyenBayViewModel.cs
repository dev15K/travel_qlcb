namespace QLCB.Models.ViewModels;

public class ChuyenBayViewModel
{
    public string MaChuyenBay { get; set; }
    public string TenChuyenBay { get; set; }
    public DateTime ThoiGianKhoiHanh { get; set; }
    public DateTime ThoiGianDen { get; set; }
    public int SoGhe { get; set; }
    public string TenSanBayDi { get; set; }
    public string TenSanBayDen { get; set; }
    
    public List<VeBayViewModel> VeBays { get; set; } = new();
}

public class VeBayViewModel
{
    public string MaVeBay { get; set; }
    public DateTime NgayDatVe { get; set; }
    public decimal GiaVe { get; set; }
    public string MaHanhKhach { get; set; }
}