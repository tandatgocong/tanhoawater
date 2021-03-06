﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TanHoaWater.Database;
using System.Data.SqlClient;

namespace TanHoaWater.DAL
{
    public class C_BAOCAO_VIEW
    {
        public static DataSet BC_DOTNHANDON_DOT(string dotnd, string nguoilap, string nguoiduyet, string maquan, string trongai) {

            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_DONKHACHHANG ";
            sql += " WHERE MADOT='" + dotnd + "'";
            sql += " AND USERNAME='" + nguoilap + "'";
            if (maquan != null) {
                sql += " AND MAQUAN='" + maquan + "'";
            }
            if (trongai != null)
            {
                sql += " AND TRONGAITHIETKE='" + trongai + "'";
            }
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_DONKHACHHANG");

            string user = "SELECT USERNAME, UPPER(FULLNAME) AS 'FULLNAME', CHUCVU AS 'MODIFYBY' FROM USERS WHERE USERNAME='" + nguoiduyet + "'";
            SqlDataAdapter ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
            ct.Fill(ds, "USERS");

            string bc = "SELECT * FROM KH_TC_BAOCAO ";
            ct = new SqlDataAdapter(bc, db.Connection.ConnectionString);
            ct.Fill(ds, "KH_TC_BAOCAO");

            return ds;
        }

        public static DataSet BC_CHUYENDON(string dotnd, string nguoilap, string nguoiduyet)
        {

            DataSet ds = new DataSet();
            TanHoaDataContext db = new TanHoaDataContext();
            db.Connection.Open();
            string sql = "SELECT * FROM V_CHUYENDON ";
            sql += " WHERE TTKMD='" + dotnd + "'";
            sql += " AND USERNAME='" + nguoilap + "'";           
            SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
            dond.Fill(ds, "V_DONKHACHHANG");

            string user = "SELECT USERNAME, UPPER(FULLNAME) AS 'FULLNAME' FROM USERS WHERE USERNAME='" + nguoiduyet + "'";
            SqlDataAdapter ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
            ct.Fill(ds, "USERS");
            return ds;
        }


    }
}
