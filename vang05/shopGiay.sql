use GIAY;

CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1),
	HoTen NVARCHAR(50) NOT NULL,
	TaiKhoan VARCHAR(15) UNIQUE,
	MatKhau VARCHAR(15) NOT NULL,
	Email VARCHAR(50) UNIQUE,
	DiaChi NVARCHAR(50),
	DienThoai VARCHAR(10),
	NgaySinh SMALLDATETIME,
	CONSTRAINT PK_Kh PRIMARY KEY(MaKH)
)
GO

CREATE TABLE ADMIN
(
	MaAdmin INT IDENTITY(1,1),
	HoTen NVARCHAR(50),
	DiaChi NVARCHAR(50),
	DienThoai VARCHAR(10),
	TenDN VARCHAR(15),
	MatKhau VARCHAR(15),
	NgaySinh VARCHAR(20),
	Email VARCHAR(50),
	Quyen Int Default 1,
	CONSTRAINT PK_AM PRIMARY KEY(MaAdmin)
)
GO
CREATE TABLE NHANVIEN(
	MaNV INT IDENTITY(1,1),
	TenNV NVARCHAR(50),
	DiaChi NVARCHAR(50),
	DienThoai VARCHAR(10),
	GioiTinh VARCHAR(15),
	MatKhau VARCHAR(15),
	NgaySinh VARCHAR(20),
	Email VARCHAR(50),
	Quyen Int Default 1,
	CONSTRAINT PK_NV PRIMARY KEY(MaNV)
)
CREATE TABLE NHANHIEU (
  MaNH INT, 
  TenNH varchar(255) NOT NULL
  CONSTRAINT PK_NH PRIMARY KEY(MaNH)
) 

CREATE TABLE GIAMGIA (
  MaGG INT NOT NULL,
  Ma varchar(255) NOT NULL,
  GIAMGIA INT NOT NULL
  CONSTRAINT PK_GG PRIMARY KEY(MaGG)
) 
CREATE TABLE DONDATHANG (
	  MaDH INT NOT NULL,
	  NgayDat SMALLDATETIME,
	  NgayGiao SMALLDATETIME,
	  MaKH INT,
	  SoDT int  NOT NULL,
	  Email varchar(255) NOT NULL,
	  DiaChi varchar(255) NOT NULL,
  TinhTrangGiaoHang varchar (10) NOT NULL
  CONSTRAINT PK_DDH PRIMARY KEY(MaDH)
) 
CREATE TABLE CHITIETDATHANG (
  MaGiay int  NOT NULL,
  MaDH int  NOT NULL,
  Ten varchar(255) NOT NULL,
  Size varchar(20) NOT NULL,
  SoLuong int  NOT NULL,
  HinhAnh varchar(255) NOT NULL,
  DonGia DECIMAL(9,2) CHECK(DonGia>=0),
  CONSTRAINT PK_CTDH PRIMARY KEY(MaDH,MaGiay)
) 
CREATE TABLE GIAY (
  MaGiay  INT IDENTITY(1,1),
  TenGiay varchar(255) NOT NULL,
  MaNH int NOT NULL,
  MaNSX INT NOT NULL,
  Size varchar (20) NOT NULL,
  GiaBan MONEY CHECK (GiaBan>=0),
  SoLuongBan INT CHECK(SoLuongBan>0),
  AnhGiay varchar(255) NOT NULL,
  MoTa NTEXT,
  CONSTRAINT PK_G PRIMARY KEY(MaGiay)
)
CREATE TABLE NHASANXUAT
(
MaNSX INT IDENTITY(1,1),
TenNXB NVARCHAR(100) NOT NULL,
DiaChi NVARCHAR(150),
Email NVARCHAR(50),
DienThoai NVARCHAR(15)
CONSTRAINT PK_NXB PRIMARY KEY(MaNSX)
)
ALTER TABLE GIAY ADD CONSTRAINT FK_S_CD FOREIGN KEY (MaNH) REFERENCES NHANHIEU (MaNH)
ALTER TABLE GIAY ADD CONSTRAINT FK_Sach_NXB FOREIGN KEY (MaNSX) REFERENCES NHASANXUAT(MaNSX)
ALTER TABLE DONDATHANG ADD CONSTRAINT FK_DDH_KH FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
ALTER TABLE CHITIETDATHANG ADD CONSTRAINT FK_CTDH_DDH FOREIGN KEY (MaDH) REFERENCES DONDATHANG(MaDH)
ALTER TABLE CHITIETDATHANG ADD CONSTRAINT FK_CTDH_S FOREIGN KEY (MaGIAY) REFERENCES GIAY(MaGIAY)

SET IDENTITY_INSERT [dbo].[KHACHHANG] ON
INSERT [dbo].[KHACHHANG] ([MaKH], [Hoten], [Diachi], [Dienthoai], [TaiKhoan], [Matkhau], [Ngaysinh], [Email]) VALUES ( 1, N'Nguyễn Hoài Bão', N'Phú Hòa', N'0988936592', N'baon', N'12345', '02/12/1982', N'bao@tdmu.edu.vn')
INSERT [dbo].[KHACHHANG] ([MaKH], [Hoten], [Diachi], [Dienthoai], [TaiKhoan], [Matkhau], [Ngaysinh], [Email]) VALUES ( 2, N'Nguyễn Thiên Thanh', N'Phú Lợi', N'Chua co', N'thanht', N'123456', '02/03/1990', N'thanh@gmail.com')
INSERT [dbo].[KHACHHANG] ([MaKH], [Hoten], [Diachi], [Dienthoai], [TaiKhoan], [Matkhau], [Ngaysinh], [Email]) VALUES ( 3, N'Phạm Đức Thịnh', N'Cánh Nghĩa', N'098893447', N'thinhp', N'123458', '04/08/1997', N'thinh@gmail.com')
INSERT [dbo].[KHACHHANG] ([MaKH], [Hoten], [Diachi], [Dienthoai], [TaiKhoan], [Matkhau], [Ngaysinh], [Email]) VALUES ( 4, N'Lê Ngọc Sỹ', N'Phú Hòa', N'Chua co', N'syl', N'1234598', '22/12/1999', N'sy@gmail.com')
INSERT [dbo].[KHACHHANG] ([MaKH], [Hoten], [Diachi], [Dienthoai], [TaiKhoan], [Matkhau], [Ngaysinh], [Email]) VALUES ( 5, N'Nguyễn Thanh Vân', N'Phú Lợi', N'098896968', N'vann', N'1234533', '02/11/1998', N'van@gmail.com')
INSERT [dbo].[KHACHHANG] ([MaKH], [Hoten], [Diachi], [Dienthoai], [TaiKhoan], [Matkhau], [Ngaysinh], [Email]) VALUES ( 6, N'Nguyễn Thanh Ngọc', N'Chánh Nghĩa', N'0988935659', N'ngocb', N'1234577', '16/07/1992', N'ngoc@gmail.com')
INSERT [dbo].[KHACHHANG] ([MaKH], [Hoten], [Diachi], [Dienthoai], [TaiKhoan], [Matkhau], [Ngaysinh], [Email]) VALUES ( 7, N'Nguyễn Minh Vũ', N'Ninh Thuận', N'098893634', N'vuminh', N'1234335', '05/07/1992', N'vu@gmail.com')
SET IDENTITY_INSERT [dbo].[KHACHHANG] OFF

INSERT [dbo].[DONDATHANG] ([MaDH], [NgayDat], [NgayGiao],[MaKH], [SoDT],[Email], [DiaChi],[TinhTrangGiaoHang]) VALUES (1, '05/10/2021','05/12/2021', 2, '035535423',N'bao@gmail.com', N'Phu Lợi', 3)
INSERT [dbo].[DONDATHANG] ([MaDH], [NgayDat], [NgayGiao],[MaKH], [SoDT],[Email], [DiaChi],[TinhTrangGiaoHang]) VALUES (2, '05/07/2021','05/11/2021', 4, '036334425',N'thanh@gmail.com', N'Phú Hòa', 4)
INSERT [dbo].[DONDATHANG] ([MaDH], [NgayDat], [NgayGiao],[MaKH], [SoDT],[Email], [DiaChi],[TinhTrangGiaoHang]) VALUES (3, '06/07/2021', '08/09/2021', 3, '03534242',N'thinh@gmail.com', N'Chánh Nghĩa', 5 )



INSERT [dbo].[NHANHIEU] ([MaNH], [TenNH]) VALUES (1, N'Nike')
INSERT [dbo].[NHANHIEU] ([MaNH], [TenNH]) VALUES (2, N'Adidas')
INSERT [dbo].[NHANHIEU] ([MaNH], [TenNH]) VALUES (3, N'Converse')
INSERT [dbo].[NHANHIEU] ([MaNH], [TenNH]) VALUES (4, N'Vans')


SET IDENTITY_INSERT [dbo].[ADMIN] ON
INSERT [dbo].[ADMIN] ([MaAdmin], [HoTen], [DiaChi], [DienThoai], [TenDN], [MatKhau], [NgaySinh], [Email],[Quyen]) VALUES (1, N'Bùi Khánh Minh', N'Phú Hòa', N'098893687', N'minhk', N'12345', '02/10/1990', N'minhq@gmail.com',1)
INSERT [dbo].[ADMIN] ([MaAdmin], [HoTen], [DiaChi], [DienThoai], [TenDN], [MatKhau], [NgaySinh], [Email],[Quyen]) VALUES (2, N'Võ Hoang Minh', N'Chánh Nghĩa', N'0123456', N'minhv', N'12345', '01/12/1993', N'minhv@gmail.com',2)
INSERT [dbo].[ADMIN] ([MaAdmin], [HoTen], [DiaChi], [DienThoai], [TenDN], [MatKhau], [NgaySinh], [Email],[Quyen]) VALUES (3, N'Nguyễn Quang Thái ', N'Phú Hòa', N'01234356', N'thaiqn', N'12345', '04/10/1995', N'thaiqn@gmail.com',2)
SET IDENTITY_INSERT [dbo].[ADMIN] OFF

SET IDENTITY_INSERT [dbo].[NHANVIEN] ON
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [DiaChi], [DienThoai], [GioiTinh], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES ( 1, N'Nguyễn Văn Hoài', N'Phú Hòa', N'0988936592','N Nam', N'12345', '02/12/1982', N'hoai@gmail.com', 1)
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [DiaChi], [DienThoai], [GioiTinh], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES ( 2, N'Nguyễn Thiên Mai', N'Phú Lợi', N'Chua co','N Nữ', N'123456', '02/03/1990', N'mai@gmail.com',2)
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [DiaChi], [DienThoai], [GioiTinh], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES ( 3, N'Phạm Quang Vinh', N'Cánh Nghĩa', N'098893447','N Nam', N'123458', '04/08/1997', N'vinh@gmail.com', 3)
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [DiaChi], [DienThoai], [GioiTinh], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES ( 4, N'Lê Ngọc Thủy', N'Phú Hòa', N'Chua co','N Nữ', N'1234598', '22/12/1999', N'thuy@gmail.com', 4)
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [DiaChi], [DienThoai], [GioiTinh], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES ( 5, N'Nguyễn Thanh Vân', N'Phú Lợi', N'098896968', 'N Nữ', N'1234533', '02/11/1998', N'van@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[NHANVIEN] OFF

INSERT [dbo].[GIAMGIA] ([MaGG], [Ma], [GIAMGIA]) VALUES (1, N'ma2019', 10)
INSERT [dbo].[GIAMGIA] ([MaGG], [Ma], [GIAMGIA]) VALUES (2, N'ma2020', 5)
INSERT [dbo].[GIAMGIA] ([MaGG], [Ma], [GIAMGIA]) VALUES (3, N'mas2021', 10)

-- nsx
SET IDENTITY_INSERT [dbo].[NHASANXUAT] ON
INSERT [dbo].[NHASANXUAT] ([MaNSX], [TenNXB], [DiaChi],[Email], [DienThoai]) VALUES(1, 'N Giày Việt', 'N Hà Nội', 'giayviet@gmail.com', '0645777')
INSERT [dbo].[NHASANXUAT] ([MaNSX], [TenNXB], [DiaChi],[Email], [DienThoai]) VALUES(2, 'N Bình Tiên', 'N Hải Phòng', 'binhtien@gmail.com', '064577735')
INSERT [dbo].[NHASANXUAT] ([MaNSX], [TenNXB], [DiaChi],[Email], [DienThoai]) VALUES(3, 'N Nam Á', 'N Ninh Bình', 'nama@gmail.com', '045777343')
INSERT [dbo].[NHASANXUAT] ([MaNSX], [TenNXB], [DiaChi],[Email], [DienThoai]) VALUES(4, 'N Shaara', 'N Hà Nội', 'shaara@gmail.com', '074573553')
SET IDENTITY_INSERT [dbo].[NHASANXUAT] OFF

--ctdh
INSERT [dbo].[CHITIETDATHANG] ([MaGiay], [MaDH], [Ten],[Size],[SoLuong], [HinhAnh], [DonGia]) VALUES (12, 3, N'Low-neck black Vanns',33,  7, N'vans2.jpg', 400000)
INSERT [dbo].[CHITIETDATHANG] ([MaGiay], [MaDH], [Ten],[Size],[SoLuong], [HinhAnh], [DonGia]) VALUES (3, 2, N'Low-neck black Adidas', 38, 22, N'add3.jpg', 20000)
-- giay
SET IDENTITY_INSERT [dbo].[GIAY] ON
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (1, 'N Low-neck white Adidas', 2, 3, 40, 2000000, 30, 'add1.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (2, 'N Low-neck Gray Adidas', 2, 2, 39, 2500000, 10, 'add2.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (3, 'N Low-neck Black Adidas', 1, 3, 38, 2000000, 22, 'add3.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (4, 'N Low-neck Gary Adidas', 3, 3, 49, 3000000, 35, 'add4.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (5, 'N Low-neck white-green Adidas', 2, 1, 41, 350000, 4, 'add5.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (6, 'N Top-neck Black Convrse', 1, 2, 46, 2000000, 25, 'con1.jpg', 'Phù hợp với những người thích hơi thấp, muốn tôn lên chiều cao, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (7, 'N Low-neck Back-blue Convrse', 2, 4, 40, 2500000, 20, 'con2.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (8, 'N Top-neck Gray-white Convrse', 1, 2, 40, 2500000, 12, 'con3.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')

INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (9, 'N Top-neck brown Convrse', 3, 3, 42, 2000000, 12, 'con4.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (10, 'N Top-neck white-Black Convrse', 2, 3, 40, 2000000, 5, 'con5.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (11, 'N Low-neck Back-white Vans',	1, 4, 33, 2000000, 10, 'vans1.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, dễ phối đồ nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (12, 'N Low-neck Black Vans', 2, 3, 33, 4000000, 7, 'vans2.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, dễ phối đồnam nữ mang đểu đẹp')

INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (13, 'N Low-neck white-Red Vans', 2, 2, 37, 2000000, 6, 'vans3.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, dễ phối đồ nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (14, 'N Low-neck white-Blue Vans', 2, 1, 33, 2000000, 8, 'vans4.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa,dễ phối đồ nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (15, 'N Low-neck white Vans', 2, 1, 36, 3500000, 5, 'vans5.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (16, 'N Low-neck white-Gray Nike', 1, 3, 40, 2000000, 30, 'Hinh8.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (17, 'N Low-neck Red Nike', 2, 2, 35, 2500000, 30, 'Hinh9.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (18, 'N Low-neck white-Black Nike', 1, 2, 34, 2500000, 23, 'Hinh10.jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (19, 'N Low-neck white Nike', 2, 1, 33, 2000000, 20, 'images(3).jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (20, 'N Low-neck white Nike', 1, 4, 34, 2000000, 10, 'images(5).jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (1, 'N Low-neck white Adidas', 2, 3, 40, 2000000, 30, 'images(6).jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [MaNH],[MaNSX], [Size], [GiaBan], [SoLuongBan], [AnhGiay], [MoTa] )VALUES (1, 'N Low-neck white Adidas', 2, 3, 40, 2000000, 30, 'images(7).jpg', 'Phù hợp với những người thích hơi thấp, mang rất êm chân, không sợ đau
khi đi xa, nam nữ mang đểu đẹp')
SET IDENTITY_INSERT [dbo].[GIAY] OFF





