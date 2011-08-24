/* VIEW */
CREATE VIEW V_DONKHACHHANG
AS
	SELECT kh.* ,q.MAQUAN,  UPPER(q.TENQUAN) AS 'TENQUAN' , p.MAPHUONG, p.TENPHUONG, UPPER(result.TENLOAI) AS 'TENLOAI',USERNAME, UPPER(FULLNAME) as 'NGUOILAP'
	FROM   USERS,DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, (SELECT MADOT,loai.TENLOAI FROM  DOT_NHAN_DON dot, LOAI_HOSO loai WHERE loai.MALOAI = dot.LOAIDON ) as result
	WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH AND  kh.MADOT=result.MADOT 


/* SP AND USERNAME='admin'  */
CREATE VIEW V_CHUYENDON
AS
	SELECT ttk.MADOT as 'TTKMD',ttk.SOHOSO as 'TTKSOHOSO', kh.* ,q.MAQUAN,  UPPER(q.TENQUAN) AS 'TENQUAN' , p.MAPHUONG, p.TENPHUONG, UPPER(result.TENLOAI) AS 'TENLOAI',USERNAME, UPPER(FULLNAME) as 'NGUOILAP'
	FROM   TOTHIETKE ttk,USERS,DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, (SELECT MADOT,loai.TENLOAI FROM  DOT_NHAN_DON dot, LOAI_HOSO loai WHERE loai.MALOAI = dot.LOAIDON ) as result
	WHERE  ttk.SOHOSO=kh.SOHOSO AND kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH AND  kh.MADOT=result.MADOT 

