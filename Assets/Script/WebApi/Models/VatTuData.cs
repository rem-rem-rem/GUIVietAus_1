using System.Collections.Generic;

public class VatTuData
{
    public int id;
    public string tenVatTu;
    public string ngayNhap;
    public int soLuong;
    public float thanhTien;
    public string ghiChu;
}

public class vatuList
{
    public List<VatTuData> data;

    public int TotalItems { get; set; } // Tổng số bản ghi
    public int ItemsPerPage { get; set; } // Số bản ghi mỗi trang
    public int CurrentPage { get; set; } // Trang hiện tại
}

//public class ResultData
//{
//    public vatuList data;

//}
