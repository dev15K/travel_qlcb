-- Tạo database
CREATE DATABASE QLCB;
GO

USE QLCB;
GO

-- Tạo bảng MayBay
CREATE TABLE MayBay (
    MaMayBay NVARCHAR(10) PRIMARY KEY,
    Loai NVARCHAR(50) NOT NULL,
    TamBay INT NOT NULL
);

-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    MaNhanVien NVARCHAR(10) PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL,
    Luong DECIMAL(18,2) NOT NULL
);

-- Tạo bảng SanBay
CREATE TABLE SanBay (
    MaSanBay NVARCHAR(10) PRIMARY KEY,
    TenSanBay NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200) NOT NULL
);

-- Tạo bảng ChuyenBay
CREATE TABLE ChuyenBay (
    MaChuyenBay NVARCHAR(10) PRIMARY KEY,
    MaMayBay NVARCHAR(10) NOT NULL,
    MaSanBayDi NVARCHAR(10) NOT NULL,
    MaSanBayDen NVARCHAR(10) NOT NULL,
    NgayGioBay DATETIME NOT NULL,
    ThoiGianBay INT NOT NULL,
    FOREIGN KEY (MaMayBay) REFERENCES MayBay(MaMayBay),
    FOREIGN KEY (MaSanBayDi) REFERENCES SanBay(MaSanBay),
    FOREIGN KEY (MaSanBayDen) REFERENCES SanBay(MaSanBay)
);

-- Tạo bảng VeBay
CREATE TABLE VeBay (
    MaVe NVARCHAR(10) PRIMARY KEY,
    MaChuyenBay NVARCHAR(10) NOT NULL,
    LoaiVe NVARCHAR(20) NOT NULL,
    GiaVe DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (MaChuyenBay) REFERENCES ChuyenBay(MaChuyenBay)
);

-- Tạo bảng ChungNhan
CREATE TABLE ChungNhan (
    MaNhanVien NVARCHAR(10) NOT NULL,
    MaMayBay NVARCHAR(10) NOT NULL,
    PRIMARY KEY (MaNhanVien, MaMayBay),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaMayBay) REFERENCES MayBay(MaMayBay)
);

-- Tạo bảng PhanCong
CREATE TABLE PhanCong (
    MaNhanVien NVARCHAR(10) NOT NULL,
    MaChuyenBay NVARCHAR(10) NOT NULL,
    PRIMARY KEY (MaNhanVien, MaChuyenBay),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaChuyenBay) REFERENCES ChuyenBay(MaChuyenBay)
); 