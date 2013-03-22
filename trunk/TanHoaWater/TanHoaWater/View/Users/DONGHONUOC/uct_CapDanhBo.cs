﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using log4net;
using TanHoaWater.Database;
using CrystalDecisions.CrystalReports.Engine;
using TanHoaWater.View.Users.DONGHONUOC.BC;
using TanHoaWater.View.Users.Report;

namespace TanHoaWater.View.Users.DONGHONUOC
{
    public partial class UCT_CapDanhBo : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UCT_CapDanhBo).Name);
        public UCT_CapDanhBo()
        {
            InitializeComponent();
            cbDotHoanCong.DataSource = DAL.C_KH_DotThiCong.getListDTC();
            cbDotHoanCong.DisplayMember = "MADOTTC";
            cbDotHoanCong.ValueMember = "MADOTTC";
            try
            {
                gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.getListHoanCong(cbDotHoanCong.Text,-1);
                for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value = Utilities.FormatSoHoSoDanhBo.sodanhbo(gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "", ".");
                    gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value = Utilities.FormatSoHoSoDanhBo.phienlotrinh(gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value + "", " ");
                }
            }
            catch (Exception)
            {

            }            
        }
        
        private void checkChuaHoanCong_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.getListHoanCong(this.cbDotHoanCong.Text, -1);
                gridHoanCong.Columns["hc_SoDot"].Visible = false;
                for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value = Utilities.FormatSoHoSoDanhBo.sodanhbo(gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "", ".");
                    gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value = Utilities.FormatSoHoSoDanhBo.phienlotrinh(gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value + "", " ");
                }
            }
            catch (Exception)
            {

            }
        }

        private void chekDaHoanCong_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.getListHoanCong(this.cbDotHoanCong.Text, 1);
                gridHoanCong.Columns["hc_SoDot"].Visible = true;
                for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value = Utilities.FormatSoHoSoDanhBo.sodanhbo(gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "", ".");
                    gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value = Utilities.FormatSoHoSoDanhBo.phienlotrinh(gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value + "", " ");
                }
            }
            catch (Exception)
            {

            }
        }

        private void checkALl_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.getListHoanCong(this.cbDotHoanCong.Text, 0);
                gridHoanCong.Columns["hc_SoDot"].Visible = true;
                for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value = Utilities.FormatSoHoSoDanhBo.sodanhbo(gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "", ".");
                    gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value = Utilities.FormatSoHoSoDanhBo.phienlotrinh(gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value + "", " ");
                }
            }
            catch (Exception)
            {

            }
        }

        public void hoantat() {
            try
            {
                if (checkALl.Checked)
                { gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.getListHoanCong(this.cbDotHoanCong.Text, 0); gridHoanCong.Columns["hc_SoDot"].Visible = true; }
                else if (checkChuaHoanCong.Checked)
                { gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.getListHoanCong(this.cbDotHoanCong.Text, -1); gridHoanCong.Columns["hc_SoDot"].Visible = false; }
                else if (chekDaHoanCong.Checked)
                { gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.getListHoanCong(this.cbDotHoanCong.Text, 1); gridHoanCong.Columns["hc_SoDot"].Visible = true; }
                for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value = Utilities.FormatSoHoSoDanhBo.sodanhbo(gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "", ".");
                    gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value = Utilities.FormatSoHoSoDanhBo.phienlotrinh(gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value + "", " ");
                }
            }
            catch (Exception)
            {
                
            }
        }
        private void cbDotHoanCong_SelectedIndexChanged(object sender, EventArgs e)
        {
            hoantat();
        }
        private Control txtKeypress;
        private void KeyPressHandle(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsNumber(e.KeyChar))
            {
                if ((e.KeyChar) != 8 && (e.KeyChar) != 46 && (e.KeyChar) != 37 && (e.KeyChar) != 39 && (e.KeyChar) != 188)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
        }

        string _sohopdong = "";
        string _sodanhbo = "";
        string _maDMA = "";
        string _hieuluc = "";
        string formatDanhBo(string db) {
            db = db.Insert(4, ".");
            db = db.Insert(8, ".");
            return db;
        }
        private void gridHoanCong_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoHo")
                //{
                //    int soho = int.Parse(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoHo"].Value + "");
                //    if (soho >= 1)
                //    {
                //        string shs = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SHS"].Value + "";
                //        string danhbo = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value + "";
                //        string hoten = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_Hoten"].Value + "";
                //        string diachi = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DiaChi"].Value + "";
                //        if ("".Equals(danhbo))
                //        {
                //            MessageBox.Show(this, "Nhập Số Danh Bộ", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //        else
                //        {
                //            frm_NhapSoHoKhau frm = new frm_NhapSoHoKhau(danhbo, shs, hoten, diachi);
                //            frm.ShowDialog();
                //        }
                //    }
                //}
            }
            catch (Exception)
            {
            }
        }
     
        private void gridHoanCong_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            try
            {
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_hopdong")
                {
                    if ("".Equals(_sodanhbo))
                    {
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value = "13"+((gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_Ma_QP"].Value+"").Substring(2,2));
                    }
                    else
                    {
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value = "13" + ((gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_Ma_QP"].Value + "").Substring(2, 2)) + "" + _sodanhbo.Substring(4, 3);
                    }
                }
            }
            catch (Exception)
            {
                
            }

            try
            {
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoDanhBo")
                {

                    _sodanhbo = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value.ToString().Replace(".", "");
                    gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value = formatDanhBo(_sodanhbo);
                } 

                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_hopdong")
                {
                    _sohopdong = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hopdong"].Value + "";
                    gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_MaDMA"].Value = _maDMA;
                    gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hieuLuc"].Value = _hieuluc;

                }
            }
            catch (Exception)
            {
                
            }

            try
            {
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_sotlk")
                {
                    if (!"".Equals(_sohopdong))
                    {
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hopdong"].Value = DAL.Idetity.IdentitySoHopDong(_sohopdong);
                    }
                }
            }
            catch (Exception)
            {
                
            }

            try
            {
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_DMucGoc")
                {
                    if (gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_ngaythicong"].Value != null && !"".Equals(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_ngaythicong"].Value + "") && gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hieuLuc"].Value != null && !"".Equals(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hieuLuc"].Value + ""))
                    {
                        if (int.Parse(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMucGoc"].Value + "") > 0)
                        {
                            string[] ngaytc = Regex.Split(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_ngaythicong"].Value + "", "\\/");
                            string[] ngayhl = Regex.Split(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hieuLuc"].Value + "", "\\/");
                            int namhl = int.Parse(ngayhl[1]);
                            int namtc = int.Parse(ngaytc[2]);
                            int thanghl = int.Parse(ngayhl[0]);
                            int thangtc = int.Parse(ngaytc[1]);

                            if (namhl == namtc && thanghl >= thangtc)
                            {
                                int dmcapnu = int.Parse(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMucGoc"].Value + "") * (int.Parse(ngayhl[0]) - int.Parse(ngaytc[1]) + 1);
                                gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMCapBu"].Value = dmcapnu;
                            }
                            else if (namhl == namtc && thanghl < thangtc)
                            {
                                MessageBox.Show(this, "Ngày Hiệu Lực Kỳ Nhỏ Hơn Ngày Thi Công", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (namhl > namtc)
                            {
                                gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMCapBu"].Value = int.Parse(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMucGoc"].Value + "") * ((namhl - namtc) * 12 + (thanghl - thangtc) + 1);
                            }
                            else if (namhl < namtc)
                            {
                                MessageBox.Show(this, "Ngày Hiệu Lực Kỳ Nhỏ Hơn Ngày Thi Công", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        public int checktrungdanhbo(string danhbo) {
            int count = 0;
            for(int i=0;i<gridHoanCong.Rows.Count;i++)
                if(danhbo.Equals((this.gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "").Trim()))
                    count++;
            return count;
        }
        public int checktrunghopdong(string hopdong) {
            int count = 0;
            for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                if (hopdong.Equals((this.gridHoanCong.Rows[i].Cells["hc_hopdong"].Value + "").Trim()))
                    count++;
            return count;
        }
        private void gridHoanCong_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoDanhBo")
                {
                    
                    try
                    {
                        string hc_SoDanhBo = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value + "";
                        if (hc_SoDanhBo.Replace(".", "").Length != 11)
                        {
                            MessageBox.Show(this, "Sai Số Danh Bộ ", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Selected = true;
                        }else if (checktrungdanhbo(this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value + "") > 1)
                        {
                            MessageBox.Show(this, "Trùng Số Danh Bộ.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (DAL.C_KH_HoSoKhachHang.checkSoDanhBo((this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value + "").Replace(".", "")) >= 1)
                        {
                            MessageBox.Show(this, "Trùng Số Danh Bộ.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    
                    this.btInBangKe.Enabled = false;
                    this.btInBangDC.Enabled = false;
                }
               
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_hopdong")
                {
                    gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hopdong"].Value = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hopdong"].Value.ToString().ToUpper();
                    _sohopdong= gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hopdong"].Value.ToString().ToUpper();

                    if (checktrunghopdong(this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hopdong"].Value + "")>1)
                    {
                        MessageBox.Show(this, "Trùng Số Hợp Đồng.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (DAL.C_KH_HoSoKhachHang.countSoHopDong(this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hopdong"].Value + "") >= 1)
                    {
                        MessageBox.Show(this, "Trùng Số Hợp Đồng.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_MaDMA")
                {
                    _maDMA = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_MaDMA"].Value.ToString().ToUpper();
                    gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_MaDMA"].Value = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_MaDMA"].Value.ToString().ToUpper();
                }
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_hieuLuc")
                {
                    _hieuluc = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_hieuLuc"].Value.ToString();
                }
                
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoHo")
                {
                    int soho = int.Parse(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoHo"].Value + "");
                    if (soho >= 1)
                    {
                        string shs = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SHS"].Value + "";
                        string danhbo = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoDanhBo"].Value + "";
                        string hoten = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_Hoten"].Value + "";
                        string diachi = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DiaChi"].Value + "";
                        if ("".Equals(danhbo))
                        {
                            MessageBox.Show(this, "Nhập Số Danh Bộ", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            frm_NhapSoHoKhau frm = new frm_NhapSoHoKhau(danhbo, shs, hoten, diachi);
                            if (frm.ShowDialog() == DialogResult.OK) {

                                gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoNhanKhau"].Value = frm.sonk;
                            }
                            
                        }
                    }
                }

                
                /////
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_GiaBieu")
                {
                    string number = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_GiaBieu"].Value + "";
                    if ("".Equals(number) || int.Parse(number) < 0) {
                        MessageBox.Show(this, "Số không được trống, và lớn hơn không ?", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_GiaBieu"].Selected = true;
                    }
                }
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_DMucGoc")
                {
                    string number = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMucGoc"].Value + "";
                    if ("".Equals(number) || int.Parse(number) < 0)
                    {
                        MessageBox.Show(this, "Số không được trống, và lớn hơn không ?", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMucGoc"].Selected = true;
                    }
                }
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_DMCapBu")
                {
                    string number = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMCapBu"].Value + "";
                    if ("".Equals(number) || int.Parse(number) < 0)
                    {
                        MessageBox.Show(this, "Số không được trống, và lớn hơn không ?", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_DMCapBu"].Selected = true;
                    }
                }
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoHo")
                {
                    string number = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoHo"].Value + "";
                    if ("".Equals(number) || int.Parse(number) < 0)
                    {
                        MessageBox.Show(this, "Số không được trống, và lớn hơn không ?", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoHo"].Selected = true;
                    }
                }
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoNhanKhau")
                {
                    string number = gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoNhanKhau"].Value + "";
                    if ("".Equals(number) || int.Parse(number) < 0)
                    {
                        MessageBox.Show(this, "Số không được trống, và lớn hơn không ?", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoNhanKhau"].Selected = true;
                    }
                }

                if (gridHoanCong.CurrentCell.OwningColumn.Name == "dhn_lotrinh")
                {
                    gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["dhn_lotrinh"].Value = Utilities.FormatSoHoSoDanhBo.phienlotrinh(gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["dhn_lotrinh"].Value.ToString().Trim(), " ");
                }

            }
            catch (Exception)
            {
                
            }
            
        }
        /// <summary>
        /// ///////////
        /// </summary>
        void updateDulieu()
        {
            try
            {
                bool flag = false;
                for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    flag = false;
                    string hc_SoDanhBo = gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "";
                    if (hc_SoDanhBo.Replace(".", "").Length == 11)
                    {
                        gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].ErrorText = null;
                        string shs = this.gridHoanCong.Rows[i].Cells["hc_shs"].Value + "";
                        string hc_hopdong = this.gridHoanCong.Rows[i].Cells["hc_hopdong"].Value + "";
                        string hc_hieuLuc = this.gridHoanCong.Rows[i].Cells["hc_hieuLuc"].Value + "";
                        string hc_GiaBieu = this.gridHoanCong.Rows[i].Cells["hc_GiaBieu"].Value + "";
                        string hc_DMucGoc = this.gridHoanCong.Rows[i].Cells["hc_DMucGoc"].Value + "";
                        string hc_DMCapBu = this.gridHoanCong.Rows[i].Cells["hc_DMCapBu"].Value + "";
                        string hc_MaDMA = this.gridHoanCong.Rows[i].Cells["hc_MaDMA"].Value + "";
                        string hc_HsCty = this.gridHoanCong.Rows[i].Cells["hc_HsCty"].Value + "";
                        string hc_MasothueCT = this.gridHoanCong.Rows[i].Cells["hc_MasothueCT"].Value + "";
                        string hc_SoHo = this.gridHoanCong.Rows[i].Cells["hc_SoHo"].Value + "";
                        string hc_SoNhanKhau = this.gridHoanCong.Rows[i].Cells["hc_SoNhanKhau"].Value + "";
                        string hc_phienlotrinh = this.gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value + "";
                        if ("".Equals(hc_hopdong))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_hopdong"].ErrorText = "Nhập Hợp Đồng";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["hc_hopdong"].ErrorText = null;
                        /////
                  
                        if ("".Equals(hc_hieuLuc))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_hieuLuc"].ErrorText = "Nhập Ngày Hiệu Lực.";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["hc_hieuLuc"].ErrorText = null;
                        /////
                        if ("".Equals(hc_GiaBieu))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_GiaBieu"].ErrorText = "Nhập Giá Biểu.";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["hc_GiaBieu"].ErrorText = null;
                        /////
                        if ("".Equals(hc_DMucGoc))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_DMucGoc"].ErrorText = "Nhập Định Mức Gốc";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["hc_DMucGoc"].ErrorText = null;
                        /////
                        if ("".Equals(hc_DMCapBu))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_DMCapBu"].ErrorText = "Nhập Định Mức Cấp Bù.";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["hc_DMCapBu"].ErrorText = null;

                        /////
                        if ("".Equals(hc_SoHo))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_SoHo"].ErrorText = "Nhập Số Hộ";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["hc_SoHo"].ErrorText = null;
                        /////
                        if ("".Equals(hc_SoNhanKhau))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_SoNhanKhau"].ErrorText = "Nhập Số Nhân Khấu";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["hc_SoNhanKhau"].ErrorText = null;

                        /////
                        if ("".Equals(hc_phienlotrinh))
                        {
                            this.gridHoanCong.Rows[i].Cells["dhn_lotrinh"].ErrorText = "Nhập Phiên Lộ Trình";
                            break;
                        }
                        else
                            this.gridHoanCong.Rows[i].Cells["dhn_lotrinh"].ErrorText = null;
                        /////
                        ////////////////////
                        try
                        {
                            KH_HOSOKHACHHANG hskh = DAL.C_DHN_ChoDanhBo.findbySHS(shs);
                            if (hskh != null)
                            {
	                             hskh.DHN_SOHOPDONG = hc_hopdong;
	                             hskh.DHN_GIABIEU = int.Parse(hc_GiaBieu);
	                             hskh.DHN_DMGOC= int.Parse(hc_DMucGoc);
	                             hskh.DHN_DMCAPBU=int.Parse(hc_DMCapBu);
	                             hskh.DHN_SODANHBO=hc_SoDanhBo.Replace(".","");
                                 hskh.DHN_PHIENLOTRINH = hc_phienlotrinh.Trim();
	                             hskh.DHN_MADMA =hc_MaDMA;
	                             hskh.DHN_HIEULUC =hc_hieuLuc;
	                             hskh.DHN_HSCONGTY =hc_HsCty;
	                             hskh.DHN_MASOTHUE =hc_MasothueCT;
	                             hskh.DHN_SOHO= int.Parse(hc_SoHo);
	                             hskh.DHN_SONHANKHAU =int.Parse(hc_SoNhanKhau);
	                             hskh.DHN_SODOT =this.txtDotBangKe.Text;
	                             hskh.DHN_CHODB=true;
                                 hskh.DHN_NGAYCHOSODB = DateTime.Now;
                                 DAL.C_DHN_ChoDanhBo.UpdateDB();
                                 flag = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("Cap Danh Bo Loi" + ex.Message);
                        }
                        

                    }
                    else if (!"".Equals(hc_SoDanhBo) && hc_SoDanhBo.Replace(".", "").Length != 11)
                    {
                        gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].ErrorText = "Sai Số Danh Bộ !";
                    }
                    //if (this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value != null && !"".Equals(this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value + ""))
                    //{
                    //    if (this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value != null && "".Equals(this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + ""))
                    //    {
                    //        this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = "Ngày Thi Công Không Được Trống";
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = null;
                    //        ngaytc = this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + "";
                    //    }


                    //    if (Utilities.DateToString.checkDate(Utilities.DateToString.convartddMMyyyy(this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + "")) == false)
                    //    {
                    //        this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = "Ngày Thi Công Không Hợp Lệ";
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = null;
                    //        ngaytc = Utilities.DateToString.convartddMMyyyy(this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + "");
                    //    }

                    //    if (this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value != null && "".Equals(this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value + ""))
                    //    {
                    //        this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].ErrorText = "Chi Số Ko Được Trống";
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].ErrorText = null;
                    //        chiso = this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value + "";
                    //    }
                    //    DAL.C_KH_HoanCong.HoanCong(shs, DateTime.ParseExact(ngaytc, "dd/MM/yyyy", null), int.Parse(chiso), sothanTLK, HoanCong);
                    //}
                    

                }
                this.btInBangKe.Enabled = true;
                this.btInBangDC.Enabled = true;
                if (flag)
                {
                    MessageBox.Show(this, "Hoàn Tất.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show(this, "Lỗi Cập Nhật Dữ Liệu.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              //  hoantat();
            }
            catch (Exception ex)
            {
                log.Error("Cho Hop Dong & So Danh Bo" + ex.Message);
            }

        }
        private void btHoanTat_Click(object sender, EventArgs e)
        {
            string bangke = this.txtDotBangKe.Text+"";;
            if ("".Equals(bangke)) {
                MessageBox.Show(this, "Nhập Bảng Kê Của Đợt.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDotBangKe.Focus();
                this.txtDotBangKe.BackColor = Color.PeachPuff;
            } else {
                updateDulieu();
              
            }
        }

        private void gridHoanCong_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void gridHoanCong_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                txtKeypress = e.Control;
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_DMucGoc"
                    | gridHoanCong.CurrentCell.OwningColumn.Name == "hc_GiaBieu"
                    | gridHoanCong.CurrentCell.OwningColumn.Name == "hc_DMCapBu"
                    | gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoHo"
                    | gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoNhanKhau")
                {
                    txtKeypress.KeyPress -= KeyPressHandle;
                    txtKeypress.KeyPress += KeyPressHandle;
                }
                else
                {
                    txtKeypress.KeyPress -= KeyPressHandle;
                }
            }
            catch (Exception)
            {
            }
        }
        public void LoadDataGridHC() {
            try
            {
                chekDaHoanCong.Checked = true;
                gridHoanCong.DataSource = DAL.C_DHN_ChoDanhBo.findByDotBangKe(this.txtDotBangKe.Text);
                for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value = Utilities.FormatSoHoSoDanhBo.sodanhbo(gridHoanCong.Rows[i].Cells["hc_SoDanhBo"].Value + "", ".");
                    gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value = Utilities.FormatSoHoSoDanhBo.phienlotrinh(gridHoanCong.Rows[i].Cells["dhn_lotrinh"].Value + "", " ");
                }
            }
            catch (Exception)
            {
            } 
        }
        private void btTimKiemDotBangKe_Click(object sender, EventArgs e)
        {
            LoadDataGridHC();
        }

        private void btInBangKe_Click(object sender, EventArgs e)
        {
            string bangke = this.txtDotBangKe.Text + ""; ;
            if ("".Equals(bangke))
            {
                MessageBox.Show(this, "Nhập Bảng Kê Của Đợt.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDotBangKe.Focus();
                this.txtDotBangKe.BackColor = Color.PeachPuff;
            }else if (getSHS().Equals(""))
                MessageBox.Show(this, "Cần Chọn Hồ Sơ In", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ReportDocument rp = new rpt_DANHBO();
                rp.SetDataSource(DAL.C_DHN_ChoDanhBo.BC_CHODANHBO(bangke,getSHS()));
                rpt_Main rpt = new rpt_Main(rp);
                rpt.ShowDialog();

            }
        }

        public string getSHS()
        {
            string result = "";
            for (int i = 0; i < gridHoanCong.Rows.Count; i++)
            {
                string shs = this.gridHoanCong.Rows[i].Cells["hc_SHS"].Value + "";
                string chonin = this.gridHoanCong.Rows[i].Cells["hc_chonin"].Value + "";
                if ("True".Equals(chonin))
                    result += "'" + shs + "',";
            }
            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        private void btInBangDC_Click(object sender, EventArgs e)
        {
            string bangke = this.txtDotBangKe.Text + ""; ;
            if ("".Equals(bangke))
            {
                MessageBox.Show(this, "Nhập Bảng Kê Của Đợt.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDotBangKe.Focus();
                this.txtDotBangKe.BackColor = Color.PeachPuff;
            }
            else if (getSHS().Equals(""))
                MessageBox.Show(this, "Cần Chọn Hồ Sơ In", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                ReportDocument rp = new rpt_DIEUCHINH();
                rp.SetDataSource(DAL.C_DHN_ChoDanhBo.BC_DIEUCHINH(bangke, getSHS()));
                rpt_Main rpt = new rpt_Main(rp);
                rpt.ShowDialog();

            }
        }

    }
}