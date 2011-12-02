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
      SELECT HOTEN,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI', NGAYNHANHS= CONVERT(VARCHAR(10),kh.NGAYNHAN,103), lhs.TENLOAI, p.TENPHUONG, q.TENQUAN, DUONG, SONHA , ttk.* ,NGAYGIAOSDV_W = CONVERT(VARCHAR(10),ttk.NGAYGIAOSDV,103),FULLNAME,  CASE WHEN ttk.TRAHS='False' THEN N'Chưa Hoàn Thành' ELSE N'Đã Hoàn Thành' END as 'TINHTRANGSVD'
      FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_HOSO lhs ,USERS us
      WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lhs.MALOAI=kh.LOAIHOSO AND ttk.SOHOSO=kh.SOHOSO AND us.USERNAME=ttk.SODOVIEN
GO


CREATE VIEW V_BIENNHANDON
AS
   SELECT SHS,HOTEN, (SONHA + ' ' + DUONG) AS 'TENDUONG', TENPHUONG, TENQUAN,HKTK,CHUQUYENNHA,GIAYPHEPXD,USERNAME,FULLNAME, YEAR(bn.NGAYNHAN) as 'NAM',MONTH(bn.NGAYNHAN) AS 'THANG', DAY(bn.NGAYNHAN) AS 'NGAY', TENLOAI=UPPER(loai.TENLOAI)
   FROM BIENNHANDON AS bn, QUAN AS q, PHUONG AS p, USERS AS u, LOAI_NHANDON AS loai
   WHERE bn.QUAN=q.MAQUAN AND p.MAQUAN=q.MAQUAN AND bn.PHUONG=p.MAPHUONG AND bn.LOAIDON=loai.LOAIDON
GO

CREATE VIEW V_DONTAIXET
AS
	SELECT kh.*, (kh.SONHA +' '+ kh.DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI',lkh.TENLOAI as 'LOAIDON',USERNAME,FULLNAME as 'NGUOILAP'
    FROM DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, TMP_TAIXET taixet ,USERS AS u
    WHERE  taixet.MAHOSO=kh.SOHOSO AND taixet.CHUYEN='False' AND kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH            
GO

CREATE VIEW V_BAOCAO_NHANDON
AS
	SELECT TENQUAN,TENLOAI, COUNT(*) as 'SOHS'
	FROM BIENNHANDON bn, QUAN q,LOAI_NHANDON lhs
	WHERE bn.QUAN=q.MAQUAN AND lhs.LOAIDON=bn.LOAIDON
	GROUP BY TENQUAN,TENLOAI
GO

CREATE VIEW V_CHUYENDON_TTK
AS
	SELECT ttk.MADOT as 'TTKMD',ttk.SOHOSO as 'TTKSOHOSO', kh.* ,q.MAQUAN,  UPPER(q.TENQUAN) AS 'TENQUAN' ,
		 p.MAPHUONG, p.TENPHUONG, UPPER(result.TENLOAI) AS 'TENLOAI',USERNAME, UPPER(FULLNAME) as 'NGUOILAP', 
		 CASE WHEN kh.TRONGAITHIETKE='True' THEN N'Trở Ngại' ELSE N'Hoàn Tất'   END as 'TINHTRANGSVD'
	FROM   TOTHIETKE ttk,USERS,DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, (SELECT MADOT,loai.TENLOAI FROM  DOT_NHAN_DON dot, LOAI_HOSO loai WHERE loai.MALOAI = dot.LOAIDON ) as result
	WHERE  ttk.SOHOSO=kh.SOHOSO AND kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH AND  kh.MADOT=result.MADOT 
GO


 
CREATE PROCEDURE TONGKETPHUIDAO
	@SHS VARCHAR(50)
AS
	INSERT INTO KH_BAOCAOPHUIDAO(SHS,MADANHMUC,TENKETCAU)
	SELECT DISTINCT SHS,MADANHMUC,TENKETCAU
	  FROM BG_KICHTHUOCPHUIDAO
	WHERE   MADANHMUC <> 'TNHA' AND  SHS=@SHS
GO

CREATE VIEW V_XINPHEPDAODUONG
AS
	SELECT DISTINCT MADOTDD,DKH.SHS, HOTEN, SONHA + ' ' + DUONG as 'DIACHI', TENPHUONG, HS.GHICHU, KHPHUI.MADANHMUC,KHPHUI.TENKETCAU,KHPHUI.KICHTHUOC,MADOTTC, CP.MACAPPHEP, CP.NOICAPPHEP
	FROM  DON_KHACHHANG DKH, PHUONG P, QUAN Q, KH_HOSOKHACHHANG HS, KH_BAOCAOPHUIDAO KHPHUI, KH_NOICAPPHEP CP, KH_XINPHEPDAODUONG XINPHEP
	WHERE DKH.QUAN = Q.MAQUAN AND Q.MAQUAN=P.MAQUAN AND DKH.PHUONG=p.MAPHUONG AND DKH.SHS = HS.SHS AND KHPHUI.SHS =DKH.SHS AND HS.MADOTDD=XINPHEP.MADOT AND XINPHEP.NOICAPPHEP =CP.MACAPPHEP
GO

CREATE VIEW V_QUYETDINHTHICONG
AS
	SELECT MADOTTC,NGAYLAP,dvtc.TENCONGTY as 'CTYTHICONG',dvtc.SOHOPDONG as 'TCSHD' , CONVERT(VARCHAR(20),dvtc.NGAYKYHD,103) AS 'TCNGAYKYHD', dvtl.TENCONGTY as 'CTYTAILAP',dvtl.SOHOPDONG as 'TLSHD' ,CONVERT(VARCHAR(10),dvtl.NGAYKYHD,103) AS 'TLNGAYKYHD', gstc.TENCONGTY as 'CTYGIAMSAT',gstc.SOHOPDONG as 'GSSHD' ,CONVERT(VARCHAR(10),gstc.NGAYKYHD,103) AS 'GSNGAYKYHD', dtc.DONVIGS
	FROM KH_DOTTHICONG dtc ,KH_DONVITHICONG dvtc,KH_DONVITAILAP dvtl, KH_DONVIGIAMSATTL gstc
	WHERE dtc.DONVITHICONG=dvtc.ID AND dtc.DONVITAILAP=dvtl.ID AND dtc.DONVIGSTL=gstc.ID
GO

CREATE VIEW V_DANHSACHTHICONG
AS
	SELECT donkh.SHS,donkh.SOHOSO,donkh.MADOT,HOTEN,DIENTHOAI, SONHA, DUONG,TENPHUONG,q.TENQUAN, MADOTDD, CHOPHEP, NGAYCOPHEP, hosokh.MADOTTC, TRONGAI, NOIDUNGTN, hosokh.GHICHU, COTLK, CPVATTU, CPNHANCONG, CPMAYTHICONG, CPCABA, CHIPHITRUCTIEP, CHIPHICHUNG, TAILAPMATDUONG, TLMDTRUOCTHUE, CONG1, THUE55, CONG3, THUEGTGT, TONGIATRI, hosokh.CREATEBY, hosokh.CREATEDATE, hosokh.MODIFYBY, hosokh.MODIFYDATE, hosokh.MODIFYLOG,TONGIATRI+TAILAPMATDUONG as 'TONGTIEN', NGAYLAP, SOLUONGTLK, DONVITHICONG, DONVITAILAP, NGAYCHUYENDVTC, CHUYENBUHANMUC, BANGKE, LOAIBANGKE, NGAYHC, GHICHUHC, SOLUONG_HCTLK, CONLAI_TLK, QUYETTOAN, NGAYCHUYENKT, NGAYTHANHTOAN, TRONGAITC
	FROM DON_KHACHHANG donkh, PHUONG p, QUAN q, KH_HOSOKHACHHANG hosokh, KH_DOTTHICONG dottc
	WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG and donkh.SHS = hosokh.SHS AND dottc.MADOTTC = hosokh.MADOTTC
GO 

 
CREATE VIEW V_DANHSACHTHICONG_BT
AS
	SELECT donkh.SHS,donkh.SOHOSO,donkh.DANHBO,donkh.MADOT,HOTEN,DIENTHOAI, donkh.GHICHU,CONVERT(varchar(20),donkh.NGAYDONGTIEN,103) as 'NGAYDONGTIEN',donkh.SOHOADON,donkh.SOTIEN, (SONHA +' '+ DUONG) AS 'TEN DUONG',TENPHUONG, MADOTDD, CHOPHEP, NGAYCOPHEP, hosokh.MADOTTC, TRONGAI, NOIDUNGTN, COTLK, CPVATTU, CPNHANCONG, CPMAYTHICONG, CPCABA, CHIPHITRUCTIEP, CHIPHICHUNG, TAILAPMATDUONG, TLMDTRUOCTHUE, CONG1, THUE55, CONG3, THUEGTGT, TONGIATRI, hosokh.CREATEBY, hosokh.CREATEDATE, hosokh.MODIFYBY, hosokh.MODIFYDATE, hosokh.MODIFYLOG,TONGIATRI+TAILAPMATDUONG as 'TONGTIEN', NGAYLAP, SOLUONGTLK, DONVITHICONG, DONVITAILAP, NGAYCHUYENDVTC, CHUYENBUHANMUC, BANGKE, LOAIBANGKE, hosokh.NGAYCHUYENHC, hosokh.CHUYENHOANCONG, NGAYHC, GHICHUHC, SOLUONG_HCTLK, CONLAI_TLK, QUYETTOAN, NGAYCHUYENKT, NGAYTHANHTOAN, TRONGAITC
	FROM DON_KHACHHANG donkh, PHUONG p, QUAN q, KH_HOSOKHACHHANG hosokh, KH_DOTTHICONG dottc
	WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG and donkh.SHS = hosokh.SHS AND dottc.MADOTTC = hosokh.MADOTTC
GO 

CREATE VIEW V_DANHSACHTHICONG_OC
AS
	SELECT donkh.SHS,donkh.SOHOSO,donkh.DANHBO,donkh.MADOT,HOTEN,DIENTHOAI, donkh.GHICHU,CONVERT(varchar(20),donkh.NGAYDONGTIEN,103) as 'NGAYDONGTIEN',donkh.SOHOADON,donkh.SOTIEN, SONHA , DUONG,TENPHUONG,q.TENQUAN, MADOTDD, CHOPHEP, NGAYCOPHEP, hosokh.MADOTTC, TRONGAI, NOIDUNGTN, COTLK, CPVATTU, CPNHANCONG, CPMAYTHICONG, CPCABA, CHIPHITRUCTIEP, CHIPHICHUNG, TAILAPMATDUONG, TLMDTRUOCTHUE, CONG1, THUE55, CONG3, THUEGTGT, TONGIATRI, hosokh.CREATEBY, hosokh.CREATEDATE, hosokh.MODIFYBY, hosokh.MODIFYDATE, hosokh.MODIFYLOG,TONGIATRI+TAILAPMATDUONG as 'TONGTIEN', NGAYLAP, SOLUONGTLK, DONVITHICONG, DONVITAILAP, NGAYCHUYENDVTC, CHUYENBUHANMUC, BANGKE, LOAIBANGKE, hosokh.NGAYCHUYENHC, hosokh.CHUYENHOANCONG, NGAYHC, GHICHUHC, SOLUONG_HCTLK, CONLAI_TLK, QUYETTOAN, NGAYCHUYENKT, NGAYTHANHTOAN, TRONGAITC
	FROM DON_KHACHHANG donkh, PHUONG p, QUAN q, KH_HOSOKHACHHANG hosokh, KH_DOTTHICONG dottc
	WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG and donkh.SHS = hosokh.SHS AND dottc.MADOTTC = hosokh.MADOTTC
GO 

CREATE VIEW V_TACHPHIGANNHUA
AS
	SELECT donkh.SHS,donkh.SOHOSO,donkh.MADOT,HOTEN,DIENTHOAI, SONHA, DUONG,TENPHUONG,q.TENQUAN,CPGAN,CPNHUA,hosokh.MADOTTC,hosokh.MODIFYDATE
	FROM DON_KHACHHANG donkh, PHUONG p, QUAN q, KH_HOSOKHACHHANG hosokh
	WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG and donkh.SHS = hosokh.SHS AND (hosokh.TRONGAI = 'False' OR hosokh.TRONGAI IS NULL )
GO

CREATE VIEW V_HOANCONG
AS
	SELECT donkh.SHS,donkh.SOHOSO,donkh.MADOT,HOTEN,DIENTHOAI, SONHA, DUONG,TENPHUONG,q.TENQUAN,donkh.SOHOADON,CONVERT(varchar(20),donkh.NGAYDONGTIEN,103) as 'NGAYDONGTIEN',hosokh.MADOTTC, hosokh.COTLK, hosokh.HOANCONG, hosokh.NGAYHOANCONG, CONVERT(varchar(20),hosokh.NGAYTHICONG,103) as 'NGAYTHICONG', hosokh.CHISO, hosokh.SOTHANTLK, hosokh.MODIFYDATE,hosokh.TRONGAI
	FROM DON_KHACHHANG donkh, PHUONG p, QUAN q, KH_HOSOKHACHHANG hosokh
	WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG and donkh.SHS = hosokh.SHS AND (hosokh.TRONGAI = 'False' OR hosokh.TRONGAI IS NULL )
GO


CREATE VIEW V_HOANCONG_TRONGAI
AS
	SELECT donkh.SHS,donkh.SOHOSO,donkh.MADOT,HOTEN,DIENTHOAI, SONHA + ' ' + DUONG  + ', P.' +TENPHUONG  +', Q.'+ q.TENQUAN as 'DIACHI', hosokh.MODIFYDATE,hosokh.NOIDUNGTN,hosokh.MADOTTC
	FROM DON_KHACHHANG donkh, PHUONG p, QUAN q, KH_HOSOKHACHHANG hosokh
	WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG and donkh.SHS = hosokh.SHS AND hosokh.TRONGAI = 'True'
GO


CREATE VIEW V_CHOSODANHBO
AS
	SELECT donkh.SHS,REPLACE(HOTEN,N'(ĐD '+CONVERT(VARCHAR(10),SOHO)+N' Hộ)',' ') AS 'HOTEN', (SONHA+' '+DUONG+', P.'+TENPHUONG+', Q.'+q.TENQUAN) as 'DIACHI',hosokh.COTLK,CONVERT(varchar(20),hosokh.NGAYTHICONG,103) as 'NGAYTHICONG',hosokh.MODIFYDATE ,MADOTTC, hosokh.CHISO,SOTHANTLK,DHN_SODANHBO,DHN_MADMA,DHN_SOHOPDONG,DHN_SOHO,DHN_SONHANKHAU,DHN_GIABIEU,DHN_DMGOC,DHN_DMCAPBU,DHN_HIEULUC,DHN_MAQUANPHUONG, DHN_SODOT
	FROM DON_KHACHHANG donkh, PHUONG p, QUAN q, KH_HOSOKHACHHANG hosokh
	WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG AND donkh.SHS = hosokh.SHS AND DHN_CHODB='True' 
GO
 
CREATE VIEW V_DHN_MST
AS
	SELECT DHN_SODOT,DHN_HSCONGTY,DHN_MASOTHUE,MODIFYDATE
	FROM KH_HOSOKHACHHANG
	WHERE DHN_HSCONGTY !=''  AND DHN_CHODB='True'
GO