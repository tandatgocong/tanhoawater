

CREATE VIEW V_DONKHACHHANG
AS
	SELECT kh.* ,q.MAQUAN,  UPPER(q.TENQUAN) AS 'TENQUAN' , p.MAPHUONG, p.TENPHUONG, UPPER(result.TENLOAI) AS 'TENLOAI',USERNAME, UPPER(FULLNAME) as 'NGUOILAP'
	FROM   USERS us,DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, (SELECT MADOT,loai.TENLOAI FROM  DOT_NHAN_DON dot, LOAI_HOSO loai WHERE loai.MALOAI = dot.LOAIDON ) as result
	WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH AND  kh.MADOT=result.MADOT 
GO


CREATE VIEW V_CHUYENDON
AS
	SELECT ttk.MADOT as 'TTKMD',ttk.SOHOSO as 'TTKSOHOSO', kh.* ,q.MAQUAN,  UPPER(q.TENQUAN) AS 'TENQUAN' , p.MAPHUONG, p.TENPHUONG, UPPER(result.TENLOAI) AS 'TENLOAI',USERNAME, UPPER(FULLNAME) as 'NGUOILAP'
	FROM   TOTHIETKE ttk,USERS,DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, (SELECT MADOT,loai.TENLOAI FROM  DOT_NHAN_DON dot, LOAI_HOSO loai WHERE loai.MALOAI = dot.LOAIDON ) as result
	WHERE  ttk.SOHOSO=kh.SOHOSO AND kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH AND  kh.MADOT=result.MADOT 
GO



CREATE VIEW V_DANHSACH_HS_SDV
AS
	SELECT ttk.MADOT,ttk.SHS,HOTEN,kh.DIENTHOAI,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI', NGAYNHAN= CONVERT(VARCHAR(10),ttk.NGAYNHAN,103), lhs.TENLOAI, kh.PHUONG, kh.QUAN, DUONG, SONHA,SODOVIEN, FULLNAME  
    FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_HOSO lhs, USERS us
    WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lhs.MALOAI=kh.LOAIHOSO AND ttk.SOHOSO=kh.SOHOSO AND us.USERNAME=ttk.SODOVIEN
GO

CREATE VIEW V_BC_KSTK
AS
      SELECT HOTEN,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI', NGAYNHANHS= CONVERT(VARCHAR(10),kh.NGAYNHAN,103), lhs.TENLOAI, p.TENPHUONG, q.TENQUAN, DUONG, SONHA , ttk.* ,NGAYGIAOSDV_W = CONVERT(VARCHAR(10),ttk.NGAYGIAOSDV,103),FULLNAME,  CASE WHEN ttk.TRAHS='False' THEN N'Chưa Hoàn Thành' ELSE N'Đã Hoàn Thành'   END as 'TINHTRANGSVD'
      FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_HOSO lhs ,USERS us
      WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lhs.MALOAI=kh.LOAIHOSO AND ttk.SOHOSO=kh.SOHOSO AND us.USERNAME=ttk.SODOVIEN
GO


CREATE VIEW V_BIENNHANDON
AS
   SELECT SHS,HOTEN, (SONHA + ' ' + DUONG) AS 'TENDUONG', TENPHUONG, TENQUAN,HKTK,CHUQUYENNHA,GIAYPHEPXD,USERNAME,FULLNAME, YEAR(GETDATE()) as 'NAM',MONTH(GETDATE()) AS 'THANG', DAY(GETDATE()) AS 'NGAY'
   FROM BIENNHANDON AS bn, QUAN AS q, PHUONG AS p, USERS AS u
   WHERE bn.QUAN=q.MAQUAN AND p.MAQUAN=q.MAQUAN AND bn.PHUONG=p.MAPHUONG 
GO