﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace TanHoaWater.View.Users.KEHOACH.HOANCONG
{
    public partial class tab_DanhSachHoanCong : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(tab_DanhSachHoanCong).Name);
        public tab_DanhSachHoanCong(string madottc)
        {
            InitializeComponent();
            cbDotHoanCong.DataSource = DAL.C_KH_DotThiCong.getListDTC();
            cbDotHoanCong.DisplayMember = "MADOTTC";
            cbDotHoanCong.ValueMember = "MADOTTC";
            try
            {
                gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong(madottc, -1);
            }
            catch (Exception)
            {
                
            }
            
        }
        public void loadData() { 
        
        }

        private void checkChuaHoanCong_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong(this.cbDotHoanCong.Text, -1);
            }
            catch (Exception)
            {

            }
        }

        private void chekDaHoanCong_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong(this.cbDotHoanCong.Text, 1);
            }
            catch (Exception)
            {

            }
        }

        private void checkALl_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong(this.cbDotHoanCong.Text, 0);
            }
            catch (Exception)
            {

            }
        }

        private void cbDotHoanCong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               if(checkALl.Checked)
                   gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong(this.cbDotHoanCong.Text, 0);
               else if(checkChuaHoanCong.Checked)
                   gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong(this.cbDotHoanCong.Text, -1);
               else if (chekDaHoanCong.Checked)
                   gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong(this.cbDotHoanCong.Text, 1);

            }
            catch (Exception)
            {

            }
        }

        private void gridHoanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                else if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_NgayTC")
                {
                    dateThiCong.Visible = true;
                    dateThiCong.Top = this.gridHoanCong.Top + gridHoanCong.GetRowDisplayRectangle(e.RowIndex, true).Top;
                    dateThiCong.Left = this.gridHoanCong.Left + gridHoanCong.GetColumnDisplayRectangle(e.ColumnIndex, true).Left;
                    dateThiCong.Width = gridHoanCong.Columns[e.ColumnIndex].Width;
                    dateThiCong.Height = gridHoanCong.Rows[e.RowIndex].Height;
                    dateThiCong.BringToFront();
                    dateThiCong.Select();
                    dateThiCong.Focus();

                }
               
            }
            catch (Exception ex)
            {
            }
        }

        private void dateThiCong_ValueChanged(object sender, EventArgs e)
        {
            gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_NgayTC"].Value = Utilities.DateToString.NgayVN(this.dateThiCong);
            dateThiCong.Visible = false;
        }

        private void gridHoanCong_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_NgayDongTein")
            //{
            //    dateThiCong.Visible = true;
            //    dateThiCong.Top = this.gridHoanCong.Top + gridHoanCong.GetRowDisplayRectangle(gridHoanCong.CurrentCell.RowIndex, true).Top;
            //    dateThiCong.Left = this.gridHoanCong.Left + gridHoanCong.GetColumnDisplayRectangle(gridHoanCong.CurrentCell.ColumnIndex, true).Left;
            //    dateThiCong.Width = gridHoanCong.Columns[gridHoanCong.CurrentCell.ColumnIndex+1].Width;
            //    dateThiCong.Height = gridHoanCong.Rows[gridHoanCong.CurrentCell.ColumnIndex+1].Height;
            //    dateThiCong.BringToFront();
            //    dateThiCong.Select();
            //    dateThiCong.Focus();
            //}
        }

        private void dateThiCong_Leave(object sender, EventArgs e)
        {
            this.dateThiCong.Visible = false;
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
        private void gridHoanCong_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoTLK")
            {
                btInBangKe.Enabled = false;
            }
            txtKeypress = e.Control;
            if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_ChiSo")
            {
                txtKeypress.KeyPress -= KeyPressHandle;
                txtKeypress.KeyPress += KeyPressHandle;
            }
            else
            {
                txtKeypress.KeyPress -= KeyPressHandle;
            }
        }

        private void gridHoanCong_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_NgayTC")
                {
                    if (Utilities.DateToString.checkDate(this.gridHoanCong.Rows[e.RowIndex].Cells["hc_NgayTC"].Value + "") == false)
                    {
                        this.gridHoanCong.Rows[e.RowIndex].Cells["hc_NgayTC"].ErrorText = "Ngày Không Hợp Lệ.";

                    }
                    else
                        this.gridHoanCong.Rows[e.RowIndex].Cells["hc_NgayTC"].ErrorText = null;
                }
            }
            catch (Exception)
            { }
        }

        bool flag = true;
        void updateDulieu() {
            for (int i = 0; i < gridHoanCong.Rows.Count; i++)
            {
                string ngaytc = "";
                string chiso = "";
                string shs = this.gridHoanCong.Rows[i].Cells["hc_SHS"].Value + "";
                string sothanTLK = this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value + "";
                if (this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value != null && "".Equals(this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + ""))
                {
                    if (this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value != null && "".Equals(this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + ""))
                    {
                        this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = "Ngày Thi Công Không Được Trống";
                        return;
                    }
                    else
                    {
                        this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = null;
                        ngaytc = this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + "";
                    }

                    if (this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value != null && "".Equals(this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value + ""))
                    {
                        this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].ErrorText = "Chi Số Ko Được Trống";
                        return;
                    }
                    else
                    {
                        this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].ErrorText = null;
                        chiso = this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value + "";
                    }
                }
                DAL.C_KH_HoanCong.HoanCong(shs, DateTime.ParseExact(ngaytc,"dd/MM/yyyy",null), int.Parse(chiso), sothanTLK);
            }
            DAL.C_KH_HoanCong.CapNhat();
        }

        private void btHoanTat_Click(object sender, EventArgs e)
        {

        }
    }
}
