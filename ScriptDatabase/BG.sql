USE TANHOA_WATER
GO

/* TONG KET KINH PHI*/
DROP PROCEDURE DELETEDATABG
GO

CREATE PROCEDURE DELETEDATABG
	@SHS VARCHAR(50)
AS
	DELETE FROM BG_CONGTACBANGIA WHERE SHS=@SHS
	DELETE FROM BG_KHOILUONGXDCB WHERE SHS=@SHS
	DELETE FROM BG_KICHTHUOCPHUIDAO WHERE SHS=@SHS
GO

EXEC DELETEDATABG '13000702'

DROP PROCEDURE TONGKETCHIPHI
GO

CREATE PROCEDURE TONGKETCHIPHI
	@SHS VARCHAR(50),
	@PHIC3 BIT,
	@PHIGS BIT,
	@PHIQL BIT,
	@A FLOAT OUTPUT,
	@B FLOAT OUTPUT,
	@C FLOAT OUTPUT,
	@CPCABA FLOAT OUTPUT,	
	@TOTAL FLOAT OUTPUT,
	@VAT FLOAT OUTPUT,
	@B1 FLOAT OUTPUT,
	@C1 FLOAT OUTPUT,
	@C2 FLOAT OUTPUT,
	@D FLOAT OUTPUT,
	@E	FLOAT OUTPUT,
	@F FLOAT OUTPUT,
	@G FLOAT OUTPUT,
	@H FLOAT OUTPUT,
	@I FLOAT OUTPUT,
	@J FLOAT OUTPUT,
	@K FLOAT OUTPUT,
	@L FLOAT OUTPUT,
	@TAILAPMATDUONG FLOAT OUTPUT,
	@TLMDTRUOCTHUE FLOAT OUTPUT,
	@CPGAN FLOAT OUTPUT,
	@CPNHUA FLOAT OUTPUT
AS
	DECLARE @NC FLOAT
	DECLARE @MTC FLOAT
	DECLARE @CA3 FLOAT
	DECLARE @PHIKHAC FLOAT
	DECLARE @PHICHUNG FLOAT
	DECLARE @TRUOCTHUE FLOAT
	DECLARE @PHIKSTK FLOAT
	DECLARE @HSKSTK FLOAT
	DECLARE @PHIGIAMSAT FLOAT
	DECLARE @CHIPHIQL FLOAT
	DECLARE @THUEVAT FLOAT
	SELECT @NC=NC,@MTC=MTC,@CA3=CABA/100,@PHIKHAC=PHIKHAC/100,@PHICHUNG=PHICHUNG/100,@TRUOCTHUE=TRUOCTHUE/100, @PHIKSTK=PHIKSTK/100,@HSKSTK=HSKSTK
			,@PHIGIAMSAT=PHIGIAMSAT/100,@CHIPHIQL=CHIPHIQL/100,@THUEVAT=VAT/100 FROM BG_HESOBANGGIA WHERE CHON='True'
	SELECT @A= SUM(ROUND((KHOILUONG*DONGIAVL),0)) FROM BG_CONGTACBANGIA WHERE LOAISN = 'CM' AND SHS=@SHS
	SELECT @B= SUM(ROUND((KHOILUONG*DONGIANC),0)) FROM BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS
	SELECT @C= SUM(ROUND((KHOILUONG*DONGIAMTC),0)) FROM BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS
	SELECT @CPGAN= SUM(KHOILUONG*DONGIAVL) FROM BG_CONGTACBANGIA WHERE NHOM=N'Gang' AND SHS=@SHS
	SELECT @CPNHUA= SUM(KHOILUONG*DONGIAVL) FROM BG_CONGTACBANGIA WHERE NHOM=N'Nhựa' AND SHS=@SHS
	SELECT @TAILAPMATDUONG = SUM(ROUND(THANHTIEN,0)) FROM BG_TAILAPMATDUONG WHERE SHS=@SHS
	SELECT @TLMDTRUOCTHUE = ROUND((SUM(ROUND(THANHTIEN,0))/(1+@THUEVAT/100)),0) FROM BG_TAILAPMATDUONG WHERE SHS=@SHS
	SET @B1= @B*@NC
	SET @C1= @C*@MTC
	IF @CA3=1 		
		SET @CPCABA= @CA3*@B1
	ELSE
		SET @CPCABA=0 
	
	SET @C2= @PHIKHAC*(@A+@B1+@C1+@CPCABA)
	SET @D=ROUND( @A + @B1 + @C1 + @C2,0)
	SET @E= @PHICHUNG*@D
	SET @F= @D+@E
	SET @G= @TRUOCTHUE*@F
	SET @H= ROUND(@F+@G,0)
	SET @I= @PHIKSTK*@HSKSTK*@H
	
	IF @PHIGS=1 		
		SET @J= @PHIGIAMSAT*@H
	ELSE
		SET @J=0 

	IF @PHIQL=1 		
		SET @K= @CHIPHIQL*@H
	ELSE
		SET @K=0 
	
	SET @L= ROUND(@H,0)+ROUND(@J,0)+ROUND(@I,0)+ROUND(@K,0)
	SET @VAT= ROUND(@THUEVAT*@L,0)
	SET @TOTAL= ROUND((@L+@VAT),0)

GO

---------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE TONGKETCHIPHI_AS
	@SHS VARCHAR(50),
	@PHIC3 BIT,
	@PHIGS BIT,
	@PHIQL BIT,
	@A FLOAT OUTPUT,
	@B FLOAT OUTPUT,
	@C FLOAT OUTPUT,
	@CPCABA FLOAT OUTPUT,	
	@TOTAL FLOAT OUTPUT,
	@VAT FLOAT OUTPUT,
	@B1 FLOAT OUTPUT,
	@C1 FLOAT OUTPUT,
	@C2 FLOAT OUTPUT,
	@D FLOAT OUTPUT,
	@E	FLOAT OUTPUT,
	@F FLOAT OUTPUT,
	@G FLOAT OUTPUT,
	@H FLOAT OUTPUT,
	@I FLOAT OUTPUT,
	@J FLOAT OUTPUT,
	@K FLOAT OUTPUT,
	@L FLOAT OUTPUT,
	@TAILAPMATDUONG FLOAT OUTPUT,
	@TLMDTRUOCTHUE FLOAT OUTPUT,
	@CPGAN FLOAT OUTPUT,
	@CPNHUA FLOAT OUTPUT
AS
	DECLARE @NC FLOAT
	DECLARE @MTC FLOAT
	DECLARE @CA3 FLOAT
	DECLARE @PHIKHAC FLOAT
	DECLARE @PHICHUNG FLOAT
	DECLARE @TRUOCTHUE FLOAT
	DECLARE @PHIKSTK FLOAT
	DECLARE @HSKSTK FLOAT
	DECLARE @PHIGIAMSAT FLOAT
	DECLARE @CHIPHIQL FLOAT
	DECLARE @THUEVAT FLOAT
	SELECT @NC=NC,@MTC=MTC,@CA3=CABA/100,@PHIKHAC=PHIKHAC/100,@PHICHUNG=PHICHUNG/100,@TRUOCTHUE=TRUOCTHUE/100, @PHIKSTK=PHIKSTK/100,@HSKSTK=HSKSTK
			,@PHIGIAMSAT=PHIGIAMSAT/100,@CHIPHIQL=CHIPHIQL/100,@THUEVAT=VAT/100 FROM BG_HESOBANGGIA WHERE CHON='True'
	SELECT @A= SUM(ROUND((KHOILUONG*DONGIAVL),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN = 'CM' AND SHS=@SHS
	SELECT @B= SUM(ROUND((KHOILUONG*DONGIANC),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS
	SELECT @C= SUM(ROUND((KHOILUONG*DONGIAMTC),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS
	SELECT @CPGAN= SUM(KHOILUONG*DONGIAVL) FROM AS_BG_CONGTACBANGIA WHERE NHOM=N'Gang' AND SHS=@SHS
	SELECT @CPNHUA= SUM(KHOILUONG*DONGIAVL) FROM AS_BG_CONGTACBANGIA WHERE NHOM=N'Nhựa' AND SHS=@SHS
	SELECT @TAILAPMATDUONG = SUM(ROUND(THANHTIEN,0)) FROM BG_TAILAPMATDUONG_AS WHERE SHS=@SHS
	SELECT @TLMDTRUOCTHUE = ROUND((SUM(ROUND(THANHTIEN,0))/(1+@THUEVAT/100)),0) FROM BG_TAILAPMATDUONG_AS WHERE SHS=@SHS
	SET @B1= @B*@NC
	SET @C1= @C*@MTC
	IF @CA3=1 		
		SET @CPCABA= @CA3*@B1
	ELSE
		SET @CPCABA=0 
	
	SET @C2= @PHIKHAC*(@A+@B1+@C1+@CPCABA)
	SET @D=ROUND( @A + @B1 + @C1 + @C2,0)
	SET @E= @PHICHUNG*@D
	SET @F= @D+@E
	SET @G= @TRUOCTHUE*@F
	SET @H= ROUND(@F+@G,0)
	SET @I= @PHIKSTK*@HSKSTK*@H
	
	IF @PHIGS=1 		
		SET @J= @PHIGIAMSAT*@H
	ELSE
		SET @J=0 

	IF @PHIQL=1 		
		SET @K= @CHIPHIQL*@H
	ELSE
		SET @K=0 
	
	SET @L= ROUND(@H,0)+ROUND(@J,0)+ROUND(@I,0)+ROUND(@K,0)
	SET @VAT= ROUND(@THUEVAT*@L,0)
	SET @TOTAL= ROUND((@L+@VAT),0)

GO
--------------------
ALTER PROCEDURE [dbo].[TONGKETCHIPHI_AS_A]
	@SHS VARCHAR(50),
	@PHIC3 BIT,
	@PHIGS BIT,
	@PHIQL BIT,
	@A FLOAT OUTPUT,
	@B FLOAT OUTPUT,
	@C FLOAT OUTPUT,
	@CPCABA FLOAT OUTPUT,	
	@TOTAL FLOAT OUTPUT,
	@VAT FLOAT OUTPUT,
	@B1 FLOAT OUTPUT,
	@C1 FLOAT OUTPUT,
	@C2 FLOAT OUTPUT,
	@D FLOAT OUTPUT,
	@E	FLOAT OUTPUT,
	@F FLOAT OUTPUT,
	@G FLOAT OUTPUT,
	@H FLOAT OUTPUT,
	@I FLOAT OUTPUT,
	@J FLOAT OUTPUT,
	@K FLOAT OUTPUT,
	@L FLOAT OUTPUT,
	@TAILAPMATDUONG FLOAT OUTPUT,
	@TLMDTRUOCTHUE FLOAT OUTPUT,
	@CPGAN FLOAT OUTPUT,
	@CPNHUA FLOAT OUTPUT
AS
	DECLARE @NC FLOAT
	DECLARE @MTC FLOAT
	DECLARE @CA3 FLOAT
	DECLARE @PHIKHAC FLOAT
	DECLARE @PHICHUNG FLOAT
	DECLARE @TRUOCTHUE FLOAT
	DECLARE @PHIKSTK FLOAT
	DECLARE @HSKSTK FLOAT
	DECLARE @PHIGIAMSAT FLOAT
	DECLARE @CHIPHIQL FLOAT
	DECLARE @THUEVAT FLOAT
	SELECT @NC=NC,@MTC=MTC,@CA3=CABA/100,@PHIKHAC=PHIKHAC/100,@PHICHUNG=PHICHUNG/100,@TRUOCTHUE=TRUOCTHUE/100, @PHIKSTK=PHIKSTK/100,@HSKSTK=HSKSTK
			,@PHIGIAMSAT=PHIGIAMSAT/100,@CHIPHIQL=CHIPHIQL/100,@THUEVAT=VAT/100 FROM BG_HESOBANGGIA WHERE CHON='True'
	SELECT @A= SUM(ROUND((KHOILUONG*DONGIAVL),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN = 'CM' AND SHS=@SHS  AND GHICHU='A'
	SELECT @B= 0*SUM(ROUND((KHOILUONG*DONGIANC),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS AND GHICHU='A'
	SELECT @C= 0*SUM(ROUND((KHOILUONG*DONGIAMTC),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS AND GHICHU='A'
	SELECT @CPGAN= 0*SUM(KHOILUONG*DONGIAVL) FROM AS_BG_CONGTACBANGIA WHERE NHOM=N'Gang' AND SHS=@SHS AND GHICHU='A'
	SELECT @CPNHUA= 0*SUM(KHOILUONG*DONGIAVL) FROM AS_BG_CONGTACBANGIA WHERE NHOM=N'Nhựa' AND SHS=@SHS AND GHICHU='A'
	SET @TAILAPMATDUONG = 0--CASE WHEN SUM(ROUND(THANHTIEN,0)) IS NULL THEN 0  ELSE SUM(ROUND(THANHTIEN,0))  END FROM BG_TAILAPMATDUONG_AS WHERE SHS=@SHS	   
	SET @TLMDTRUOCTHUE = 0--CASE WHEN ROUND((SUM(ROUND(THANHTIEN,0))/(1+@THUEVAT/100)),0) IS NULL THEN 0  ELSE ROUND((SUM(ROUND(THANHTIEN,0))/(1+@THUEVAT/100)),0) END  FROM BG_TAILAPMATDUONG_AS WHERE SHS=@SHS
	
	SET @B1= 0
	SET @C1= 0
	IF @CA3=1 		
		SET @CPCABA= 0
	ELSE
		SET @CPCABA=0 
	
	SET @C2= 0
	SET @D= @A
	SET @E= 0
	SET @F=@A
	SET @G= 0
	SET @H= @A
	SET @I= 0
	
	IF @PHIGS=1 		
		SET @J= 0
	ELSE
		SET @J=0 

	IF @PHIQL=1 		
		SET @K= 0
	ELSE
		SET @K=0 
	
	SET @L= @A
	SET @VAT= ROUND(@THUEVAT*@L,0)
	SET @TOTAL= ROUND((@L+@VAT),0)
GO

------------------
ALTER PROCEDURE [dbo].[TONGKETCHIPHI_AS_B]
	@SHS VARCHAR(50),
	@PHIC3 BIT,
	@PHIGS BIT,
	@PHIQL BIT,
	@A FLOAT OUTPUT,
	@B FLOAT OUTPUT,
	@C FLOAT OUTPUT,
	@CPCABA FLOAT OUTPUT,	
	@TOTAL FLOAT OUTPUT,
	@VAT FLOAT OUTPUT,
	@B1 FLOAT OUTPUT,
	@C1 FLOAT OUTPUT,
	@C2 FLOAT OUTPUT,
	@D FLOAT OUTPUT,
	@E	FLOAT OUTPUT,
	@F FLOAT OUTPUT,
	@G FLOAT OUTPUT,
	@H FLOAT OUTPUT,
	@I FLOAT OUTPUT,
	@J FLOAT OUTPUT,
	@K FLOAT OUTPUT,
	@L FLOAT OUTPUT,
	@TAILAPMATDUONG FLOAT OUTPUT,
	@TLMDTRUOCTHUE FLOAT OUTPUT,
	@CPGAN FLOAT OUTPUT,
	@CPNHUA FLOAT OUTPUT
AS
	DECLARE @NC FLOAT
	DECLARE @MTC FLOAT
	DECLARE @CA3 FLOAT
	DECLARE @PHIKHAC FLOAT
	DECLARE @PHICHUNG FLOAT
	DECLARE @TRUOCTHUE FLOAT
	DECLARE @PHIKSTK FLOAT
	DECLARE @HSKSTK FLOAT
	DECLARE @PHIGIAMSAT FLOAT
	DECLARE @CHIPHIQL FLOAT
	DECLARE @THUEVAT FLOAT
	DECLARE @AB FLOAT
	SELECT @NC=NC,@MTC=MTC,@CA3=CABA/100,@PHIKHAC=PHIKHAC/100,@PHICHUNG=PHICHUNG/100,@TRUOCTHUE=TRUOCTHUE/100, @PHIKSTK=PHIKSTK/100,@HSKSTK=HSKSTK
			,@PHIGIAMSAT=PHIGIAMSAT/100,@CHIPHIQL=CHIPHIQL/100,@THUEVAT=VAT/100 FROM BG_HESOBANGGIA WHERE CHON='True'
	SELECT @A= SUM(ROUND((KHOILUONG*DONGIAVL),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN = 'CM' AND SHS=@SHS AND GHICHU='B'
	SELECT @AB= SUM(ROUND((KHOILUONG*DONGIAVL),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN = 'CM' AND SHS=@SHS AND GHICHU='A'
	SELECT @B= SUM(ROUND((KHOILUONG*DONGIANC),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS 
	SELECT @C= SUM(ROUND((KHOILUONG*DONGIAMTC),0)) FROM AS_BG_CONGTACBANGIA WHERE LOAISN <> 'CMTH' AND SHS=@SHS 
	SELECT @CPGAN= SUM(KHOILUONG*DONGIAVL) FROM AS_BG_CONGTACBANGIA WHERE NHOM=N'Gang' AND SHS=@SHS 
	SELECT @CPNHUA= SUM(KHOILUONG*DONGIAVL) FROM AS_BG_CONGTACBANGIA WHERE NHOM=N'Nhựa' AND SHS=@SHS 
	SELECT @TAILAPMATDUONG = CASE WHEN SUM(ROUND(THANHTIEN,0)) IS NULL THEN 0  ELSE SUM(ROUND(THANHTIEN,0))  END FROM BG_TAILAPMATDUONG_AS WHERE SHS=@SHS	   
	SELECT @TLMDTRUOCTHUE = CASE WHEN ROUND((SUM(ROUND(THANHTIEN,0))/(1+@THUEVAT/100)),0) IS NULL THEN 0  ELSE ROUND((SUM(ROUND(THANHTIEN,0))/(1+@THUEVAT/100)),0) END  FROM BG_TAILAPMATDUONG_AS WHERE SHS=@SHS
	SET @B1= @B*@NC
	SET @C1= @C*@MTC
	IF @CA3=1 		
		SET @CPCABA= @CA3*@B1
	ELSE
		SET @CPCABA=0 
	
	SET @C2= @PHIKHAC*(@AB+@A+@B1+@C1+@CPCABA)
	SET @D=ROUND( @A + @B1 + @C1 + @C2,0)
	SET @E= @PHICHUNG*@D
	SET @F= @D+@E
	SET @G= @TRUOCTHUE*@F
	SET @H= ROUND(@F+@G,0)
	SET @I= @PHIKSTK*@HSKSTK*@H
	
	IF @PHIGS=1 		
		SET @J= @PHIGIAMSAT*@H
	ELSE
		SET @J=0 

	IF @PHIQL=1 		
		SET @K= @CHIPHIQL*@H
	ELSE
		SET @K=0 
	
	SET @L= ROUND(@H,0)+ROUND(@J,0)+ROUND(@I,0)+ROUND(@K,0)
	SET @VAT= ROUND(@THUEVAT*@L,0)
	SET @TOTAL= ROUND((@L+@VAT),0)

GO
------------------
/* TAI LAP MAT DUONG */
DROP VIEW BG_CHITIETBG
GO

CREATE VIEW BG_CHITIETBG
AS
	SELECT vt.STT,bg.SHS,bg.MAHIEU,bg.MAHDG,bg.TENVT,bg.DVT,bg.NHOM,bg.LOAISN,bg.KHOILUONG,bg.DONGIAVL,bg.DONGIANC,bg.DONGIAMTC,(KHOILUONG*DONGIAVL) AS 'TT_VL', (KHOILUONG*DONGIANC) AS 'TT_NC', (KHOILUONG*DONGIAMTC) AS 'TT_MTC',CASE WHEN NHOM='XDCB' THEN N'A' WHEN NHOM='Gang' THEN N'B'  ELSE N'C'  END as 'NHOMVT'
	FROM BG_CONGTACBANGIA bg, DANHMUCVATTU vt
	WHERE bg.MAHIEU = vt.MAHIEU
	
GO


CREATE VIEW BG_CHITIETBG_AS
AS
	SELECT vt.STT,bg.SHS,bg.MAHIEU,bg.MAHDG,bg.TENVT,bg.DVT,bg.NHOM,bg.LOAISN,bg.KHOILUONG,bg.DONGIAVL,bg.DONGIANC,bg.DONGIAMTC,(KHOILUONG*DONGIAVL) AS 'TT_VL', (KHOILUONG*DONGIANC) AS 'TT_NC', (KHOILUONG*DONGIAMTC) AS 'TT_MTC',CASE WHEN NHOM='XDCB' THEN N'A' WHEN NHOM='Gang' THEN N'B'  ELSE N'C'  END as 'NHOMVT',GHICHU
	FROM AS_BG_CONGTACBANGIA bg, DANHMUCVATTU vt
	WHERE bg.MAHIEU = vt.MAHIEU
	
GO

SELECT * FROM BG_CHITIETBG WHERE SHS='13000702'

SELECT  distinct * from BG_CHITIETBG WHERE SHS='1100003'
ORDER BY NHOMVT DESC

DROP VIEW BG_TAILAPMATDUONG
GO

CREATE VIEW BG_TAILAPMATDUONG
AS
	SELECT phui.SHS, phui.MADANHMUC,phui.TENKETCAU, SUM(KHOILUONG) AS 'KHOILUONG',dg.DONGIA as 'DONGIA', SUM(KHOILUONG)*dg.DONGIA as 'THANHTIEN'
	FROM BG_KICHTHUOCPHUIDAO phui,DONGIATAILAPMATDUONG dg
	WHERE phui.MADANHMUC=dg.MADANHMUC AND CHON='True' AND phui.MADANHMUC <>'TNHA'
	GROUP BY  phui.SHS, phui.MADANHMUC,phui.TENKETCAU,dg.DONGIA
GO


CREATE VIEW BG_TAILAPMATDUONG_AS
AS
	SELECT phui.SHS, phui.MADANHMUC,phui.TENKETCAU, SUM(KHOILUONG) AS 'KHOILUONG',dg.DONGIA as 'DONGIA', SUM(KHOILUONG)*dg.DONGIA as 'THANHTIEN'
	FROM AS_BG_KICHTHUOCPHUIDAO phui,DONGIATAILAPMATDUONG dg
	WHERE phui.MADANHMUC=dg.MADANHMUC AND CHON='True' AND phui.MADANHMUC <>'TNHA'
	GROUP BY  phui.SHS, phui.MADANHMUC,phui.TENKETCAU,dg.DONGIA
GO


DROP VIEW BG_THONGTINKHACHANG
GO

CREATE VIEW BG_THONGTINKHACHANG
AS
	SELECT kh.SHS, UPPER(kh.HOTEN) AS 'HOTEN',(kh.SONHA +' '+ kh.DUONG +N', Phường '+p.TENPHUONG+N', Quận '+ q.TENQUAN ) as 'DIACHI', kh.DANHBO,(CASE WHEN kh.SHS like '%BT%' THEN 'BT' ELSE  CASE WHEN kh.SHS like '%D%' THEN 'DD' ELSE 'GM' END END) as  'LOAIHOSO'
	FROM  DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh, (SELECT MADOT,loai.TENLOAI FROM  DOT_NHAN_DON dot, LOAI_HOSO loai WHERE loai.MALOAI = dot.LOAIDON ) as result
	WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH AND  kh.MADOT=result.MADOT 
GO

CREATE VIEW BG_THONGTINKHACHANG_AS
AS
	SELECT kh.SHS, UPPER(kh.HOTEN) AS 'HOTEN',(kh.SONHA +' '+ kh.DUONG +N', Phường '+p.TENPHUONG+N', Quận '+ q.TENQUAN ) as 'DIACHI', kh.DANHBO,(CASE WHEN kh.SHS like '%BT%' THEN 'BT' ELSE  CASE WHEN kh.SHS like '%D%' THEN 'DD' ELSE 'GM' END END) as  'LOAIHOSO'
	FROM  AS_KHACHHANG kh,QUAN q,PHUONG p 
	WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG 
GO


DROP VIEW BG_SUMTAILAPMATDUONG
GO

CREATE VIEW BG_SUMTAILAPMATDUONG
AS
	SELECT SHS,SUM(THANHTIEN) as 'SAUTHUE',(SUM(THANHTIEN)/(1+hs.VAT/100)) AS 'TRUOCTHUE',(SUM(THANHTIEN)*hs.VAT)/(100+hs.VAT) AS 'THUEGTGT'
	FROM BG_TAILAPMATDUONG, BG_HESOBANGGIA hs
	GROUP BY SHS,hs.VAT
GO

CREATE VIEW BG_SUMTAILAPMATDUONG_AS
AS
	SELECT SHS,SUM(THANHTIEN) as 'SAUTHUE',(SUM(THANHTIEN)/(1+hs.VAT/100)) AS 'TRUOCTHUE',(SUM(THANHTIEN)*hs.VAT)/(100+hs.VAT) AS 'THUEGTGT'
	FROM BG_TAILAPMATDUONG_AS, BG_HESOBANGGIA hs
	GROUP BY SHS,hs.VAT
GO



DROP VIEW W_HS
GO

CREATE VIEW W_HS
AS
	SELECT  REPLACE(NC,'.',',')as'NC',REPLACE(MTC,'.',',')as'MTC' ,REPLACE(CABA,'.',',')+'%'as'CABA' ,REPLACE(PHIKHAC,'.',',')+'%'as'PHIKHAC', 
			REPLACE(PHICHUNG,'.',',')+'%'as'PHICHUNG',REPLACE(TRUOCTHUE,'.',',')+'%'as'TRUOCTHUE' ,REPLACE(PHIKSTK,'.',',')+'%'as'PHIKSTK' ,
			REPLACE(HSKSTK,'.',',') as'HSKSTK' ,REPLACE(PHIGIAMSAT,'.',',') +'%'as'PHIGIAMSAT' ,
			REPLACE(CHIPHIQL,'.',',')+'%'as'CHIPHIQL',REPLACE(VAT,'.',',')+'%'as'VAT' , REPLACE(HSSDL,'.',',') as'HSSDL',REPLACE(HSTH,'.',',') as'HSTH', CHON,
			 CREATEBY, CREATEDATE, MODIFYBY, MODIFYDATE
	FROM BG_HESOBANGGIA
 