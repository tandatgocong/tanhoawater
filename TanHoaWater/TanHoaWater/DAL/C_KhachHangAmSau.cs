﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TanHoaWater.Database;
using System.Data.SqlClient;
using log4net;
using System.Collections;

namespace TanHoaWater.DAL
{
    public class C_KhachHangAmSau
    {

        static TanHoaDataContext db = new TanHoaDataContext();
        private static readonly ILog log = LogManager.GetLogger(typeof(C_KhachHangAmSau).Name);

        //public static int TotalListByDot(string dot)
        //{
        //    TanHoaDataContext db = new TanHoaDataContext();
        //    SqlConnection conn = new SqlConnection(db.Connection.ConnectionString);
        //    conn.Open();
        //    string sql = " SELECT COUNT(*) ";
        //    sql += " FROM DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh ";
        //    sql += " WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH";
        //    sql += " AND MADOT='" + dot + "'";
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    int result = Convert.ToInt32(cmd.ExecuteScalar());
        //    conn.Close();
        //    return result;
        //}

        //public static DataTable getListbyDot(string dot, int FirstRow, int pageSize)
        //{
        //    TanHoaDataContext db = new TanHoaDataContext();
        //    db.Connection.Open();
        //    string sql = " SELECT SOHOSO,HOTEN, (SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI',NGAYNHAN= CONVERT(VARCHAR(10),NGAYNHAN,103), lkh.TENLOAI as 'LOAIDON' ";
        //    sql += " FROM DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh ";
        //    sql += " WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH";
        //    sql += " AND MADOT='" + dot + "'";
        //    sql += " ORDER BY NGAYNHAN DESC ";
        //    SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
        //    DataSet dataset = new DataSet();
        //    adapter.Fill(dataset, FirstRow, pageSize, "TABLE");
        //    db.Connection.Close();
        //    return dataset.Tables[0];

        //}

        //public static DataTable getListbyDot(string dot)
        //{
        //    TanHoaDataContext db = new TanHoaDataContext();
        //    db.Connection.Open();
        //    string sql = " SELECT SOHOSO,SHS,HOTEN, (SONHA +' '+ DUONG +', P.'+p.TENPHUONG+', Q.'+ q.TENQUAN ) as 'DIACHI',NGAYNHAN= CONVERT(VARCHAR(10),NGAYNHAN,103), lkh.TENLOAI as 'LOAIDON' ";
        //    sql += " FROM DON_KHACHHANG kh,QUAN q,PHUONG p, LOAI_KHACHHANG lkh ";
        //    sql += " WHERE  kh.QUAN = q.MAQUAN AND q.MAQUAN=p.MAQUAN AND kh.PHUONG=p.MAPHUONG AND lkh.MALOAI=kh.LOAIKH";
        //    sql += " AND MADOT='" + dot + "'";
        //    sql += " ORDER BY NGAYNHAN DESC ";
        //    SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
        //    DataSet dataset = new DataSet();
        //    adapter.Fill(dataset, "TABLE");
        //    db.Connection.Close();
        //    return dataset.Tables[0];

        //}

        public static AS_KHACHHANG findBySHS(string shs)
        {
            var data = from don in db.AS_KHACHHANGs where don.SHS == shs select don;
            return data.SingleOrDefault();
        }

        public static bool InsertDonHK(AS_KHACHHANG donkh)
        {
            try
            {
                db.AS_KHACHHANGs.InsertOnSubmit(donkh);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Insert Dot KHACH HANG LOI " + ex.Message);
            }
            return false;
        }

        public static bool UpdateDONKH(AS_KHACHHANG donkh)
        {
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Update Dot KHACH HANG LOI " + ex.Message);
            }
            return false;
        }
    }
}