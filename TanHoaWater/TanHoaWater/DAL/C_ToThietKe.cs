﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TanHoaWater.Database;
using log4net;
using System.Data.SqlClient;
using System.Data;

namespace TanHoaWater.DAL
{
    public class C_ToThietKe
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(C_ToThietKe).Name);
        public static void addNew(TOTHIETKE ttk)
        {
            try
            {
                TanHoaDataContext db = new TanHoaDataContext();
                db.TOTHIETKEs.InsertOnSubmit(ttk);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                log.Error("TTK Add New" + ex.Message);
            }

        }
        public static DataTable DanhSachChuyen(string dotND)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = " SELECT ttk.MADOT,ttk.SOHOSO,HOTEN,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI', NGAYNHAN= CONVERT(VARCHAR(10),kh.NGAYNHAN,103), lhs.TENLOAI ";
            sql += " FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_HOSO lhs ";
            sql += " WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lhs.MALOAI=kh.LOAIHOSO AND ttk.SOHOSO=kh.SOHOSO ";
            sql += " AND ttk.MADOT='" + dotND + "'";
            sql += " ORDER BY NGAYNHAN DESC ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];
        }
        public static DataTable DachSachHoSoGiaoViec(string dotND, string ngaynhan, string SODOVIEN)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = " SELECT ttk.MADOT,ttk.SHS,HOTEN,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI', NGAYNHAN= CONVERT(VARCHAR(10),kh.NGAYNHAN,103), lhs.TENLOAI, p.TENPHUONG, q.TENQUAN, DUONG, SONHA  ";
            sql += " FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_HOSO lhs ";
            sql += " WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lhs.MALOAI=kh.LOAIHOSO AND ttk.SOHOSO=kh.SOHOSO ";
            if (dotND != null)
            {
                sql += " AND ttk.MADOT='" + dotND + "'";
            }
            if (ngaynhan != null)
            {
                sql += " AND CONVERT(VARCHAR(10),ttk.NGAYGIAOSDV,103)='" + ngaynhan + "'";
            }
            if (SODOVIEN != null)
            {
                sql += " AND ttk.SODOVIEN='" + SODOVIEN + "'";
            }
            else
            {
                sql += " AND ttk.SODOVIEN IS NULL ";
            }
            sql += " AND kh.BOPHANCHUYEN='TTK'";
            sql += " ORDER BY q.TENQUAN,p.TENPHUONG,DUONG,SONHA DESC ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];
        }
        public static void giaoviecSDV(string sohs, string sodovien, string nguoigiao)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            var giaoviec = from ttk in db.TOTHIETKEs where ttk.SHS == sohs select ttk;
            TOTHIETKE totk = giaoviec.SingleOrDefault();
            if (totk != null)
            {
                totk.SODOVIEN = sodovien;
                totk.NGAYGIAOSDV = DateTime.Now.Date;
                totk.MODIFYBY = nguoigiao;
                totk.MODIFYDATE = DateTime.Now;
                totk.TRAHS = false;
            }
            db.SubmitChanges();
        }
        public static DataSet BC_GIAOHS_SDV(string dotND, string ngaynhan, string SODOVIEN, string nguoilap)
        {

            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_DANHSACH_HS_SDV ";
            sql += " WHERE SODOVIEN='" + SODOVIEN + "'";
            if (dotND != null)
            {
                sql += " AND MADOT='" + dotND + "'";
            }
            if (ngaynhan != null)
            {
                sql += " AND CONVERT(VARCHAR(20),NGAYNHAN,103) ='" + ngaynhan + "'";
            }
            sql += " ORDER BY SHS ASC ";
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_DANHSACH_HS_SDV");

            string user = "SELECT USERNAME, UPPER(FULLNAME) AS 'FULLNAME' FROM USERS WHERE USERNAME='" + nguoilap + "'";
            SqlDataAdapter ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
            ct.Fill(ds, "USERS");
            return ds;
        }

        public static bool TraHS(bool tk,string sohoso, string noidungtrongai)
        {
            try
            {
                TanHoaDataContext db = new TanHoaDataContext();
                var query = from ttk in db.TOTHIETKEs where ttk.SOHOSO == sohoso select ttk;
                TOTHIETKE totk = query.SingleOrDefault();
                if (totk != null)
                {
                    
                    totk.TRAHS = true;
                    totk.NGAYTRAHS = DateTime.Now;
                    totk.TRONGAITHIETKE = tk;
                    totk.NOIDUNGTRONGAI = noidungtrongai;
                    db.SubmitChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                log.Error("Loi khi chuyen hs" + ex.Message);

            }
            return false;
        }

        public static TOTHIETKE findBySoHoSo(string sohoso)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            var ttk = from query in db.TOTHIETKEs where query.SOHOSO == sohoso select query;
            return ttk.SingleOrDefault();
        }
        public static TOTHIETKE findBySHS(string shs)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            var ttk = from query in db.TOTHIETKEs where query.SHS == shs select query;
            return ttk.SingleOrDefault();
        }
        public static DataTable TinhHinhKSTK(string madot, string tungay, string denngay, string tensdv, bool tinhtrang, string loaihs)
        {

            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = " SELECT ttk.MADOT,ttk.SHS,HOTEN,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI', NGAYNHAN= CONVERT(VARCHAR(10),kh.NGAYNHAN,103), lhs.TENLOAI, p.TENPHUONG, q.TENQUAN, DUONG, SONHA,CHUYENTK= CONVERT(VARCHAR(10),ttk.NGAYNHAN,103),NGAYTRAHS= CONVERT(VARCHAR(10),ttk.NGAYTRAHS,103)  ";
            sql += " FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_HOSO lhs ";
            sql += " WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lhs.MALOAI=kh.LOAIHOSO AND ttk.SOHOSO=kh.SOHOSO ";
            sql += " AND  (ttk.TRONGAITHIETKE is null or ttk.TRONGAITHIETKE=0) and ttk.TRAHS=0 ";
            sql += "  AND ttk.TRAHS='False'  AND ttk.SOHOSO not in (select MAHOSO from TMP_TAIXET) AND NGAYTRAHS is null and HOANTATTK is null  ";
            if (loaihs != null)
            {
                if (loaihs == "GM")
                {
                    sql += " AND kh.LOAIHOSO='" + loaihs + "' AND ISNUMERIC(ttk.SHS)=1  ";
                }
                else
                    sql += " AND kh.LOAIHOSO='" + loaihs + "'";

            }
            if (tungay != null)
            {
                // sql += " AND CONVERT(VARCHAR(10),ttk.NGAYGIAOSDV,103)>='" + tungay + "' AND CONVERT(VARCHAR(10),ttk.NGAYGIAOSDV,103)<='" + denngay + "' ";
                sql += " AND CONVERT(DATETIME,ttk.NGAYGIAOSDV,103) BETWEEN CONVERT(DATETIME,'" + tungay + "',103) AND CONVERT(DATETIME,'" + denngay + "',103) ";
            }
            if (tensdv != null)
            {
                sql += " AND ttk.SODOVIEN='" + tensdv + "'";
            }

            sql += " AND ttk.TRAHS='" + tinhtrang + "'";
            sql += " ORDER BY kh.NGAYNHAN ASC ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];
        }
        public static int CountTinhHinhKSTK(string madot, string tungay, string denngay, string tensdv, bool tinhtrang)
        {
            string sql = " SELECT COUNT(*)  ";
            sql += " FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_HOSO lhs ";
            sql += " WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lhs.MALOAI=kh.LOAIHOSO AND ttk.SOHOSO=kh.SOHOSO ";
            if (madot != null)
            {
                sql += " AND ttk.MADOT='" + madot + "'";
            }
            if (tungay != null)
            {
                sql += " AND CONVERT(DATETIME,ttk.NGAYGIAOSDV,103) BETWEEN CONVERT(DATETIME,'" + tungay + "',103) AND CONVERT(DATETIME,'" + denngay + "',103) ";
                //sql += " AND CONVERT(VARCHAR(10),ttk.NGAYGIAOSDV,103)>='" + tungay + "' AND CONVERT(VARCHAR(10),ttk.NGAYGIAOSDV,103)<='" + denngay + "' ";
            }
            if (tensdv != null)
            {
                sql += " AND ttk.SODOVIEN='" + tensdv + "'";
            }

            sql += " AND ttk.TRAHS='" + tinhtrang + "'";
            TanHoaDataContext db = new TanHoaDataContext();
            SqlConnection conn = new SqlConnection(db.Connection.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return result;
        }
        public static DataSet BC_TinhHinhKSTK(string madot, string tungay, string denngay, string tensdv, string nguoilap)
        {

            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = " SELECT *, NGAYBT='" + tungay + "', NGAYKT='" + denngay + "'  ";
            sql += " FROM  V_BC_KSTK ";
            sql += " WHERE SODOVIEN='" + tensdv + "'";
            if (madot != null)
            {
                sql += " AND MADOT='" + madot + "'";
            }
            if (tungay != null)
            {
                //sql += " AND CONVERT(VARCHAR(10),NGAYGIAOSDV,103)>='" + tungay + "' AND CONVERT(VARCHAR(10),NGAYGIAOSDV,103)<='" + denngay + "' ";
                sql += " AND CONVERT(DATETIME,NGAYGIAOSDV,103) BETWEEN CONVERT(DATETIME,'" + tungay + "',103) AND CONVERT(DATETIME,'" + denngay + "',103) ";
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "V_BC_KSTK");

            string user = "SELECT USERNAME, UPPER(FULLNAME) AS 'FULLNAME' FROM USERS WHERE USERNAME='" + nguoilap + "'";
            SqlDataAdapter ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
            ct.Fill(dataset, "USERS");

            db.Connection.Close();
            return dataset;
        }

        public static DataTable DANHSACHDOTNHANDON()
        {

            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "  SELECT DISTINCT MADOT FROM TOTHIETKE ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];
        }

        public static DataTable GetDotToTK(string ttkMaDot)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "  SELECT DISTINCT nd.MADOT, CONVERT(VARCHAR(20),nd.NGAYLAPDON,103) ,loai.TENLOAI, COUNT(*) as 'SOHS', COUNT(TRONGAITHIETKE) as 'TRONGAI', COUNT(HOANTATTK) as 'HOANTHANH'";
            sql += " FROM DOT_NHAN_DON AS nd ,LOAI_HOSO AS loai, TOTHIETKE AS ttk";
            sql += "  WHERE nd.LOAIDON=loai.MALOAI AND ttk.MADOT=nd.MADOT ";
            sql += " AND ttk.MADOT=N'" + ttkMaDot + "'";
            sql += " GROUP BY nd.MADOT,nd.NGAYLAPDON,loai.TENLOAI ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];
        }

        public static DataTable ListHoanTatTK(string ttkMaDot)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT ttk.SHS,HOTEN,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI',  ";
            sql += "  NGAYNHAN= CONVERT(VARCHAR(10),kh.NGAYNHAN,103), NGAYHTTK=CONVERT(VARCHAR(10),ttk.NGAYTRAHS,103), ";
            sql += "  CASE WHEN kh.TRONGAITHIETKE='True' THEN N'Trở Ngại'  ELSE (CASE WHEN ttk.HOANTATTK='True' THEN N'Hoàn Tất' ELSE N'' END ) END as 'TINHTRANGSVD'  ";
            sql += "  FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p";
            sql += "  WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG  AND ttk.SOHOSO=kh.SOHOSO ";
           sql += " AND ttk.MADOT='" + ttkMaDot + "'";
           // sql += " AND  NGAYHOANTATTK='" + ngayhoantat + "'";
           
            sql += " ORDER BY TINHTRANGSVD";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];
        }

        public static DataTable ListHoanTatTKByDate(string ngayhoantat, string shs)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT ttk.SHS,HOTEN,(SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI',  ";
            sql += "  NGAYNHAN= CONVERT(VARCHAR(10),kh.NGAYNHAN,103), NGAYHTTK=CONVERT(VARCHAR(10),ttk.NGAYTRAHS,103), ";
            sql += "  CASE WHEN kh.TRONGAITHIETKE='True' THEN N'Trở Ngại'  ELSE (CASE WHEN ttk.HOANTATTK='True' THEN N'Hoàn Tất' ELSE N'' END ) END as 'TINHTRANGSVD'  ";
            sql += "  FROM TOTHIETKE ttk, DON_KHACHHANG kh,QUAN q,PHUONG p";
            sql += "  WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG  AND ttk.SOHOSO=kh.SOHOSO ";
            //  sql += " AND ttk.MADOT='" + ttkMaDot + "'";
            if ("".Equals(shs))
            {
                sql += " AND CONVERT(VARCHAR(10),ttk.NGAYTRAHS,103) ='" + ngayhoantat + "'";
            }
            else {
                sql += " AND ttk.SHS IN ("+shs+") ";
            }
            

            sql += " ORDER BY TINHTRANGSVD";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];
        } 
        public static bool chuyenhs(string shs, string bp)
        {
            try
            {
                TanHoaDataContext db = new TanHoaDataContext();
                var query = from ttk in db.TOTHIETKEs where ttk.SHS == shs select ttk;
                TOTHIETKE toTK = query.SingleOrDefault();
                if (toTK != null)
                {
                    toTK.NGAYCHUYENHS = DateTime.Now.Date;
                    toTK.HOANTATTK = true;
                    toTK.NGAYHOANTATTK = DateTime.Now.Date;
                    toTK.BOPHANCHUYEN = bp;
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("TTK chuyen ho so loi :" + ex.Message);
            }
            return false;
        }

        public static DataSet BC_CHUYENDON_TTK(string dotnd, string nguoilap)
        {

            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_CHUYENDON_TTK ";
            sql += " WHERE TTKMD='" + dotnd + "'";
            sql += " AND USERNAME='" + nguoilap + "'";
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_CHUYENDON_TTK");

            //string user = "SELECT USERNAME, UPPER(FULLNAME) AS 'FULLNAME' FROM USERS WHERE USERNAME='" + nguoiduyet + "'";
            //SqlDataAdapter ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
            //ct.Fill(ds, "USERS");
            return ds;
        }

        public static bool HoaTatTK(string shs)
        {
            try
            {
                TanHoaDataContext db = new TanHoaDataContext();
                var query = from ttk in db.TOTHIETKEs where ttk.SHS == shs select ttk;
                TOTHIETKE totk = query.SingleOrDefault();
                if (totk != null)
                {
                    totk.TRAHS = true;
                    totk.HOANTATTK = true;
                    totk.NGAYHOANTATTK = DateTime.Now.Date;
                    totk.NGAYTRAHS = DateTime.Now.Date;

                    db.SubmitChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                log.Error("Loi khi chuyen hs" + ex.Message);

            }
            return false;
        }

        public static bool HoaTatTK(string shs, string ghichu)
        {
            try
            {
                TanHoaDataContext db = new TanHoaDataContext();
                var query = from ttk in db.TOTHIETKEs where ttk.SHS == shs select ttk;
                TOTHIETKE totk = query.SingleOrDefault();
                if (totk != null)
                {
                    totk.TRAHS = true;
                    totk.HOANTATTK = true;
                    totk.NGAYHOANTATTK = DateTime.Now.Date;
                    totk.NGAYTRAHS = DateTime.Now.Date;
                    totk.NGAYCHUYENHS = DateTime.Now.Date;
                    totk.BOPHANCHUYEN = "VTTH";
                  //  totk.TRONGAITHIETKE = false;
                    totk.GHICHU = ghichu;
                    db.SubmitChanges();
                  //  LinQConnection.ExecuteCommand_("UPDATE DON_KHACHHANG SET TRONGAITHIETKE=0 WHERE SHS='"+shs+"'");
                    return true;
                }

            }
            catch (Exception ex)
            {
                log.Error("Loi Hoan Tat Thiet Ke" + ex.Message);

            }
            return false;
        }

        public static void CapNhatHoanTatChoDot(string madot) {
            try
            {
                TanHoaDataContext db = new TanHoaDataContext();
                var query = from ttk in db.DOT_NHAN_DONs where ttk.MADOT == madot select ttk;
                DOT_NHAN_DON dotnd = query.SingleOrDefault();
                if (dotnd != null) {
                    dotnd.NGAYTRAHS = DateTime.Now.Date;
                    db.SubmitChanges();
                }


            }
            catch (Exception ex)
            {
                log.Error("Loi khi chuyen hs" + ex.Message);

            }
        }
        public static void HoaTatTKbyDot(string madot)
        {
            try
            {
                TanHoaDataContext db = new TanHoaDataContext();
                var query = from ttk in db.TOTHIETKEs where ttk.MADOT == madot select ttk;
                foreach (var item in query.ToList())
                {
                    if (item.TRONGAITHIETKE != true)
                    {
                        HoaTatTK(item.SHS);
                    }
                }
                CapNhatHoanTatChoDot(madot);

            }
            catch (Exception ex)
            {
                log.Error("Loi khi chuyen hs" + ex.Message);

            }

        }
        public static void updateSoDoVien(string shs, string sodovien)
        {
            TanHoaDataContext db = new TanHoaDataContext();
            var query = from ttk in db.TOTHIETKEs where ttk.SHS == shs select ttk;
            TOTHIETKE totk = query.SingleOrDefault();
            if (totk != null)
            {
                totk.SODOVIEN = DAL.C_USERS.findByFullName(sodovien).USERNAME;
                db.SubmitChanges();
            }
        }

        public static DataTable findByHSHT(string shs)
        {

            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = " SELECT ttk.SHS,HOTEN, SONHA + ' ' + DUONG + ', P.' + TENPHUONG + ', Q.' + TENQUAN  as 'DIACHI',ttk.MADOT";
            sql += " FROM DON_KHACHHANG donkh,TOTHIETKE ttk,PHUONG p, QUAN q ";
            sql += " WHERE donkh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND donkh.PHUONG=p.MAPHUONG AND ttk.SHS = donkh.SHS ";
            sql += " AND ttk.SHS='" + shs + "'";

            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "TABLE");
            db.Connection.Close();
            return dataset.Tables[0];


        }
        public static DataSet BC_HOANTATTK(string dotnd, string nguoilap, string hoantat)
        {
            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_TTK_HOANTATTK ";
            sql += " WHERE TTKMD='" + dotnd + "'";
            sql += " AND USERNAME='" + nguoilap + "'";
            if ("True".Equals(hoantat)) {
                sql += " AND TKHOANTAT='True' AND (TRONGAITHIETKE='False' OR TRONGAITHIETKE IS NULL) "; 
            }
            else if ("False".Equals(hoantat))
            {
                sql += " AND TRONGAITHIETKE='True'"; 
            
            }
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_TTK_HOANTATTK");

            return ds;
        }
        public static DataSet BC_HOANTATTK_BANGEK(string dotnd, string nguoilap, string hoantat)
        {
            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_TTK_HOANTATTK ";
            sql += " WHERE TTKMD='" + dotnd + "' AND TKHOANTAT='True'";
            sql += " AND USERNAME='" + nguoilap + "'";
            if ("True".Equals(hoantat))
            {
                sql += " AND TKHOANTAT='True' AND (TRONGAITHIETKE='False' OR TRONGAITHIETKE IS NULL) ";
            }
            else if ("False".Equals(hoantat))
            {
                sql += " AND TRONGAITHIETKE='True'";

            }
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_TTK_HOANTATTK");

            return ds;
        }
        public static DataTable DIEMHOSO(string dotnd, string nguoilap)
        {
            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = " SELECT TINHTRANGSVD ,COUNT(TINHTRANGSVD)  FROM V_TTK_HOANTATTK ";
            sql += " WHERE TTKMD='" + dotnd + "'";
            sql += " AND USERNAME='" + nguoilap + "'";
            sql += " GROUP BY TINHTRANGSVD";
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds);
            return ds.Tables[0];
        }

        public static DataSet BC_HOANTATTK_BYDATE( string ngayhoantat ,string nguoilap, string shs)
        {
            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_TTK_HOANTATTK ";
            if ("".Equals(shs))
            {
                sql += " WHERE NGAYHOANTATTK='" + ngayhoantat + "'";
            }
            else
            {
                sql += " WHERE SHS IN (" + shs.Substring(0, shs.Length - 1) + ") ";
            }
                       
          sql += " AND USERNAME='" + nguoilap + "'";
            sql += " AND TKHOANTAT='True' AND (TRONGAITHIETKE='0' OR TRONGAITHIETKE IS NULL) ";
             
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_TTK_HOANTATTK");

            return ds;
        }
        public static DataSet BC_TRONGAITK_BYDATE(string ngayhoantat, string nguoilap, string shs)
        {
            //DataSet ds = new DataSet();
            //TanHoaDataContext db = new TanHoaDataContext();
            //db.Connection.Open();
            //string sql = "SELECT  * FROM V_TTK_HOANTATTK ";
            //sql += " WHERE NGAYHOANTATTK='" + ngayhoantat + "'";
            //sql += " AND USERNAME='" + nguoilap + "'";
            //sql += " AND TRONGAITHIETKE='True' ";

            //SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            //dond.Fill(ds, "V_TTK_HOANTATTK");

            //return ds;
            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_TTK_HOANTATTK ";
            if ("".Equals(shs))
            {
                sql += " WHERE NGAYHOANTATTK='" + ngayhoantat + "'";
            }
            else
            {
                sql += " WHERE SHS IN (" + shs.Substring(0, shs.Length - 1) + ") ";
            }

            sql += " AND USERNAME='" + nguoilap + "'";
            sql += "  AND (TRONGAITHIETKE='True' OR TRONGAITHIETKE IS NULL) ";

            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_TTK_HOANTATTK");

            return ds;

        }

    }
}