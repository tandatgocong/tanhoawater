UPDATE DANHMUCVATTU SET CREATEDATE='1/1/2011' WHERE NHOMVT='XDCB'
UPDATE DANHMUCVATTU SET CREATEDATE='12/12/2011' WHERE NHOMVT !='XDCB'

CREATE TABLE BOVATTAOSAN
(
	MABOVT VARCHAR(250) PRIMARY KEY NOT NULL,
	TENBOVT NVARCHAR(MAX),
	CREATEBY VARCHAR(50),
	CREATEDATE DATETIME,
	MODIFYBY VARCHAR(20),
	MODIFYDATE DATETIME
)
GO

CREATE TABLE CHITIETBOVATTAOSAN
(
	STT INT IDENTITY (1,1) PRIMARY KEY,
	MAHIEU VARCHAR(20),
	TENVT NVARCHAR(250), 
	DVT NVARCHAR(20),
	NHOM NVARCHAR(20),
	LOAISN VARCHAR(20),
	KHOILUONG FLOAT, 
	CHON BIT,
	MABOVT VARCHAR(250),
	CREATEBY VARCHAR(50),
	CREATEDATE DATETIME,
	MODIFYBY VARCHAR(20),
	MODIFYDATE DATETIME,
	CONSTRAINT FR_VTTS FOREIGN KEY (MABOVT) REFERENCES BOVATTAOSAN(MABOVT)
)
GO