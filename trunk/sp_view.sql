/* VIEW */
CREATE VIEW V_DONKHACHHANG
AS
	SELECT kh.MADOT,kh.SOHOSO,kh.HOTEN, kh.DIENTHOAI,kh.SONHA,kh.DUONG,q.MAQUAN, q.TENQUAN, p.MAPHUONG, p.TENPHUONG,NGAYNHAN, UPPER(result.TENLOAI) AS 'TENLOAI'
	FROM   DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, (SELECT MADOT,loai.TENLOAI FROM  DOT_NHAN_DON dot, LOAI_HOSO loai WHERE loai.MALOAI = dot.LOAIDON ) as result
	WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH AND  kh.MADOT=result.MADOT

/* SP */