﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using TanHoaWater.View.Users.HOANCONG.BC;
using CrystalDecisions.CrystalReports.Engine;
using TanHoaWater.View.Users.KEHOACH.HOANCONG.BC;
using TanHoaWater.View.Users.Report;
using TanHoaWater.Database;
namespace TanHoaWater.View.Users.KEHOACH.HOANCONG
{
    public partial class tab_DanhSachHoanCong : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(tab_DanhSachHoanCong).Name);
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
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

            List<DHN_DONGHO> list = DAL.C_DHN_TENDONGHO.ListDanhSachDongHo();
            foreach (var item in list)
            {
                namesCollection.Add(item.TENDONGHO);
            }
            //gr_TenDongHo.AutoCompleteMode = AutoCompleteMode.Suggest;
            //gr_TenDongHo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //gr_TenDongHo.AutoCompleteCustomSource = namesCollection;.AutoCompleteCustomSource = namesCollection;


        }
        public void loadData()
        {

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
        public void hoantat()
        {
            try
            {
                if (checkALl.Checked)
                    gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong_(this.cbDotHoanCong.Text, 0);
                else if (checkChuaHoanCong.Checked)
                    gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong_(this.cbDotHoanCong.Text, -1);
                else if (chekDaHoanCong.Checked)
                    gridHoanCong.DataSource = DAL.C_KH_HoanCong.getListHoanCong_(this.cbDotHoanCong.Text, 1);

            }
            catch (Exception)
            {

            }
        }
        private void cbDotHoanCong_SelectedIndexChanged(object sender, EventArgs e)
        {
            hoantat();
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
            catch (Exception)
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
            //try
            //{
            //    this.gridHoanCong.Rows[e.RowIndex].Cells["hc_SoTLK"].Value = (this.gridHoanCong.Rows[e.RowIndex].Cells["hc_SoTLK"].Value + "").ToUpper();
            //}
            //catch (Exception)
            //{

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

            try
            {
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "gr_TenDongHo")
                {
                    if (e.Control is DataGridViewTextBoxEditingControl)
                    {
                        DataGridViewTextBoxEditingControl te =
                        (DataGridViewTextBoxEditingControl)e.Control;
                        te.AutoCompleteMode = AutoCompleteMode.Suggest;
                        te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        te.AutoCompleteCustomSource = namesCollection;
                    }
                }
                if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoTLK")
                {
                    btInBangKe.Enabled = false;
                    btHoanTat.Enabled = true;
                    this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoTLK"].Value = (this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoTLK"].Value + "").ToUpper();

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
            catch (Exception)
            {

            }

        }

        private void gridHoanCong_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_NgayTC")
            //    {
            //        if (Utilities.DateToString.checkDate(this.gridHoanCong.Rows[e.RowIndex].Cells["hc_NgayTC"].Value + "") == false)
            //        {
            //            this.gridHoanCong.Rows[e.RowIndex].Cells["hc_NgayTC"].ErrorText = "Ngày Không Hợp Lệ.";

            //        }
            //        else
            //            this.gridHoanCong.Rows[e.RowIndex].Cells["hc_NgayTC"].ErrorText = null;
            //    }
            //}
            //catch (Exception)
            //{ }
        }

        bool flag = true;
        void updateDulieu()
        {
            bool flag = false;
            int i = 0;
            try
            {

                for (i = 0; i < gridHoanCong.Rows.Count; i++)
                {
                    flag = false;
                    string ngaytc = "";
                    string chiso = "";
                    string shs = this.gridHoanCong.Rows[i].Cells["hc_SHS"].Value + "";
                    string sothanTLK = this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value + "";
                    string hc = this.gridHoanCong.Rows[i].Cells["hc_DHN"].Value + "";
                    string hieudongho = this.gridHoanCong.Rows[i].Cells["gr_TenDongHo"].Value + "";
                    string s_cotlk = (this.gridHoanCong.Rows[i].Cells["hc_TLK"].Value + "").Trim();
                    bool HoanCong = false;
                    try
                    {
                        HoanCong = bool.Parse(hc);
                    }
                    catch (Exception)
                    {
                    }
                    if (this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value != null && !"".Equals(this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value + ""))
                    {
                        if (this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value != null && "".Equals(this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + ""))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = "Ngày Thi Công Không Được Trống";
                            break;
                        }
                        else
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = null;
                            ngaytc = this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + "";
                        }


                        if (Utilities.DateToString.checkDate(Utilities.DateToString.convartddMMyyyy(this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + "").Trim()) == false)
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = "Ngày Thi Công Không Hợp Lệ";
                            break;
                        }
                        else
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].ErrorText = null;
                            ngaytc = Utilities.DateToString.convartddMMyyyy((this.gridHoanCong.Rows[i].Cells["hc_NgayTC"].Value + "").Trim());
                        }

                        if (this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value != null && "".Equals(this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value + ""))
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].ErrorText = "Chi Số Ko Được Trống";
                            break;
                        }
                        else
                        {
                            this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].ErrorText = null;
                            chiso = this.gridHoanCong.Rows[i].Cells["hc_ChiSo"].Value + "";
                        }
                        int cotlk = 15;
                        try
                        {
                            cotlk = int.Parse(s_cotlk);
                        }
                        catch (Exception)
                        {

                        }
                        DAL.C_KH_HoanCong.HoanCong(shs, DateTime.ParseExact(ngaytc, "dd/MM/yyyy", null), int.Parse(chiso), cotlk, sothanTLK.ToUpper(), hieudongho, HoanCong);
                        flag = true;
                    }

                }

                if (flag) {
                    MessageBox.Show(this, "Hoàn Tất.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show(this, "Lỗi Cập Nhật Dữ Liệu.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                this.btInBangKe.Enabled = true;
                //hoantat();
            }
            catch (Exception ex)
            {
                this.gridHoanCong.Rows[i].ErrorText = "Lỗi Dữ Liệu";
                log.Error("Loi Hoan Tat Hoan Cong" + ex.Message);
            }

        }

        private void btHoanTat_Click(object sender, EventArgs e)
        {
            updateDulieu();


        }

        public string getSHS()
        {
            string result = "";
            for (int i = 0; i < gridHoanCong.Rows.Count; i++)
            {
                string shs = this.gridHoanCong.Rows[i].Cells["hc_SHS"].Value + "";
                string chonin = this.gridHoanCong.Rows[i].Cells["hc_ChonIn"].Value + "";
                if ("True".Equals(chonin))
                    result += "'" + shs + "',";
            }
            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1);
            return result;
        }
        private void btTachChiPhi_Click(object sender, EventArgs e)
        {
            if (getSHS().Equals(""))
                MessageBox.Show(this, "Cần Chọn Hồ Sơ In", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                frmDialogPrintting frm = new frmDialogPrintting(getSHS());
                frm.ShowDialog();
            }
        }

        private void btInBangKe_Click(object sender, EventArgs e)
        {
            if (getSHS().Equals(""))
                MessageBox.Show(this, "Cần Chọn Hồ Sơ In", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ReportDocument rp = new rpt_HoanCong();
                rp.SetDataSource(DAL.C_KH_HoanCong.BC_HOANCONG(this.cbDotHoanCong.Text, getSHS()));
                rpt_Main rpt = new rpt_Main(rp);
                rpt.ShowDialog();
            }

        }

        private void gridHoanCong_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        public int checktrungsothan(string hopdong)
        {
            int count = 0;
            for (int i = 0; i < gridHoanCong.Rows.Count; i++)
                if (hopdong.Equals((this.gridHoanCong.Rows[i].Cells["hc_SoTLK"].Value + "").Trim()))
                  count++;
            return count;
        }
        private void gridHoanCong_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gridHoanCong.CurrentCell.OwningColumn.Name == "hc_SoTLK")
            {
                if (checktrungsothan(this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoTLK"].Value + "")>1)
                {
                    MessageBox.Show(this, "Số Thân TLK Đã Tồn Tại.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (DAL.C_KH_HoSoKhachHang.checkSoThanTLK(this.gridHoanCong.Rows[gridHoanCong.CurrentCell.RowIndex].Cells["hc_SoTLK"].Value + "") >= 1)
                {
                    MessageBox.Show(this, "Số Thân TLK Đã Tồn Tại.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }
    }
}