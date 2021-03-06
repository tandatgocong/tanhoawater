﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TanHoaWater.Database;
using log4net;
using System.Data.SqlClient;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using TanHoaWater.View.Users.Report;
using TanHoaWater.View.Users.TinhDuToan.BGDieuChinh.report;
using TanHoaWater.View.Users.TinhDuToan.BGDieuChinh;

namespace TanHoaWater.View.Users.BGDieuChinh
{

    public partial class tab_BangGiaDieuChinh : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(tab_BangGiaDieuChinh).Name);
        public tab_BangGiaDieuChinh()
        {
            InitializeComponent();
            loadComboboxPhuiDao();          
            this.txtSHS.Focus();
            pd_MaKetCau.AutoComplete = true;
            txtNguoiLapBG.Text = DAL.C_USERS._fullName;
            #region Load SDV
            this.txtSoDoVien.DataSource = DAL.C_USERS.getUserByMaPhongAndLevel("TTK", 2);
            this.txtSoDoVien.DisplayMember = "FULLNAME";
            this.txtSoDoVien.ValueMember = "USERNAME";
            #endregion
            #region Load SDV
            this.dieuchinh_SoDV.DataSource = DAL.C_USERS.getUserByMaPhongAndLevel("TTK", 2);
            this.dieuchinh_SoDV.DisplayMember = "FULLNAME";
            this.dieuchinh_SoDV.ValueMember = "USERNAME";
            #endregion
        }
        public void loadComboboxPhuiDao()
        {
            this.pd_MaKetCau.DataSource = DAL.C_DanhMucTaiLapMD.getListDanhMucTLMD();
            this.pd_MaKetCau.ValueMember = "MADANHMUC";
            this.pd_MaKetCau.DisplayMember = "TENKETCAU";
            this.pd_MaKetCau.DropDownWidth = 300;

            congtac_mahieu.DataSource = DAL.C_DanhMucVatTu.getListDanhMucVatCobobox();
            this.congtac_mahieu.DisplayMember = "TENVT";
            this.congtac_mahieu.ValueMember = "MAHIEU";

            //congtac_mahieu.DropDownWidth = 300;
            //congtac_mahieu.MaxDropDownItems = 5;
            this.contac_loaisd.DataSource = DAL.C_LoaiSD.getList();
            this.contac_loaisd.DisplayMember = "MALOAI";
            this.contac_loaisd.ValueMember = "MALOAI";
        }
        bool khachangtuTaiLap = false;
        public void LoadTabCacLanDieuChinh() {

            DataTable tableDongTien = new DataTable();
            tableDongTien.Columns.Add("NGAYDONGTIEN", typeof(string));
            tableDongTien.Columns.Add("TIENGANTLK", typeof(double));
            tableDongTien.Columns.Add("TIENTLMD", typeof(double));
            tableDongTien.Columns.Add("TONGCONG", typeof(double));           
            
            DataTable table = new DataTable();
            table.Columns.Add("LOAD", typeof(string));
            table.Columns.Add("LANDC", typeof(int));
            table.Columns.Add("LOAIHS", typeof(string));
            table.Columns.Add("TRONGAIDC", typeof(string));
            table.Columns.Add("SDVTK", typeof(string));
            table.Columns.Add("NGAYCHAYDT", typeof(string));
            table.Columns.Add("NGUOILAPDT", typeof(string));
            table.Columns.Add("TOTALDUTOAN", typeof(double));

            BG_KHOILUONGXDCB klxd = DAL.C_KhoiLuongXDCB.findBySHS(_shs);
            
            if (klxd != null) {
                DataRow myDataRow = table.NewRow();
                myDataRow["LOAD"] = "Lấy Số Liệu";
                myDataRow["LANDC"] = 0;
                myDataRow["LOAIHS"] = "Quyết Toán";
                myDataRow["TRONGAIDC"] = "";
                TOTHIETKE ttk = DAL.C_ToThietKe.findBySHS(klxd.SHS);
                myDataRow["SDVTK"] = ttk.SODOVIEN;
                myDataRow["NGAYCHAYDT"] = Utilities.DateToString.NgayVN(klxd.CREATEDATE.Value);
                myDataRow["NGUOILAPDT"] = klxd.CREATEBY;
                myDataRow["TOTALDUTOAN"] = klxd.TONGIATRI + klxd.TAILAPMATDUONG;    
                table.Rows.Add(myDataRow);

                DON_KHACHHANG donkh = DAL.C_DonKhachHang.findBySHS(klxd.SHS);
                try
                {
                    if (!"".Equals(donkh.NGAYDONGTIEN.Value + ""))
                    {
                        DataRow dontienRow = tableDongTien.NewRow();
                        dontienRow["NGAYDONGTIEN"] = Utilities.DateToString.NgayVN(donkh.NGAYDONGTIEN.Value); ;
                        dontienRow["TIENGANTLK"] = klxd.TONGIATRI;
                        dontienRow["TIENTLMD"] = klxd.TAILAPMATDUONG;
                        dontienRow["TONGCONG"] = klxd.TONGIATRI + klxd.TAILAPMATDUONG;
                        tableDongTien.Rows.Add(dontienRow);
                    }
                }
                catch (Exception)
                {
                    
                }
               


                List<BGDC_KHOILUONGXDCB> list = DAL.C_BGDC_KhoiLuongXDCB.getListBGDCBySHS(klxd.SHS);
                foreach (var item in list)
                {
                    myDataRow = table.NewRow();
                    myDataRow["LOAD"] = "Lấy Số Liệu";
                    myDataRow["LANDC"] = item.LAN;
                    myDataRow["LOAIHS"] = item.LOAIDC;
                    myDataRow["TRONGAIDC"] = item.TRONGAIDC;                    
                    myDataRow["SDVTK"] = item.SODOVIEN;
                    myDataRow["NGAYCHAYDT"] = Utilities.DateToString.NgayVN(item.CREATEDATE.Value);
                    myDataRow["NGUOILAPDT"] = item.CREATEBY;
                    myDataRow["TOTALDUTOAN"] = item.TONGIATRI + item.TAILAPMATDUONG;    
                    table.Rows.Add(myDataRow);

                    if (!"".Equals(item.NGAYDONGTIEN + ""))
                    {
                        DataRow dontienRow = tableDongTien.NewRow();
                        dontienRow["NGAYDONGTIEN"] = Utilities.DateToString.NgayVN(item.NGAYDONGTIEN.Value); ;
                        dontienRow["TIENGANTLK"] = item.TONGIATRI;
                        dontienRow["TIENTLMD"] = item.TAILAPMATDUONG;
                        dontienRow["TONGCONG"] = item.TONGIATRI + item.TAILAPMATDUONG;
                        tableDongTien.Rows.Add(dontienRow);
                    }

                    solandieuchinh = item.LAN.Value + 1;
                }
            }
            
            GridCacLanDC.DataSource = table;
            gridCacLanDongTien.DataSource = tableDongTien;
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
        public void visibleTab(bool tabThamSo, bool tabBoiThuong, bool tabNhapPhuidao, bool tabCacCongTac, bool tabItem5)
        {

            PanelThamSo.Visible = tabThamSo;
            panelBoiThuong.Visible = tabBoiThuong;
            panelNhapPhuiDao.Visible = tabNhapPhuidao;
            splitContainer1.Panel2.Visible = tabCacCongTac;
            panelKhoanCong.Visible = tabItem5;
        }
        private void tabThamSo_Click(object sender, EventArgs e)
        {
            visibleTab(true, false, false, false, false);
        }

        private void tabBoiThuong_Click(object sender, EventArgs e)
        {
            visibleTab(false, true, false, false, false);
        }

        private void tabItem5_Click(object sender, EventArgs e)
        {
            visibleTab(false, false, false, false, true);
        }
        private void tabTinhDuToan_Click(object sender, EventArgs e)
        {
            this.panelTinhDuToan.Visible = true;
            this.tabControl2.SelectedTabIndex = 0;
            //this.cbNhomVatTu.DataSource = DAL.C_DanhMucVatTu.getListDanhMucVatCobobox();
            //this.cbNhomVatTu.DisplayMember = "TENVT";
            //this.cbNhomVatTu.ValueMember = "MAHIEU";
            loadComboboxPhuiDao();
            this.txtSHS.Focus();

        }

        /// <summary>
        /// Phui Dao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void GridPhuiDao_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phidao_cotll"].Value = "True";
            GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sll"].Value = 1;
            GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sauu"].Value = 0.6;

        }
        private void tabNhapPhuiDao_Click(object sender, EventArgs e)
        {
            visibleTab(false, false, true, false, false);
        }
        bool tinhlai = false;
        private void GridPhuiDao_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //  MessageBox.Show(this, GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["pd_MaKetCau"].Value + "");
            if (GridPhuiDao.CurrentCell.OwningColumn.Name == "pd_MaKetCau")
            {

                DANHMUCTAILAPMATDUONG dmvt = DAL.C_DanhMucTaiLapMD.finbyMaDM(GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["pd_MaKetCau"].Value + "");
                if (dmvt != null)
                {
                    mahieuvt = dmvt.MADANHMUC;
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_tenketcau"].Value = dmvt.TENKETCAU.ToUpper();
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["Phui_DonGia"].Value = dmvt.DONGIA;
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_dvt"].Value = dmvt.DVT;
                    if (mahieuvt.Equals("TNHA"))
                    {
                        GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_Daii"].Value = this.txtSoHo.Value;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Không Tìm Thấy Mã Kết Cấu.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["pd_MaKetCau"].Selected = true;
                    return;
                }
                //Utilities.DataGridV.formatRows(GridPhuiDao);
            }
            if (GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_Daii" | GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_rongg" | GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_sauu" | GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_sll")
            {
                try
                {
                    string s_dai = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_Daii"].Value + "";
                    string s_rong = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_rongg"].Value + "";
                    string s_sau = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sauu"].Value + "";
                    string s_soluong = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sll"].Value + "";

                    double dai = double.Parse(s_dai);
                    double rong = double.Parse(s_rong);
                    double sau = double.Parse(s_sau);
                    int soluong = int.Parse(s_soluong);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_Daii"].Value = String.Format("{0:0.00}", dai);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_rongg"].Value = String.Format("{0:0.00}", rong);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sauu"].Value = String.Format("{0:0.00}", sau);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sll"].Value = soluong;

                    double khoiluong = 0.0;
                    double chuvi = 0.0;
                    double thetich = 0.0;
                    if (dai >= 0 && rong >= 0 && soluong >= 1 && sau > 0)
                    {
                        khoiluong = dai * rong * soluong;
                        if (rong > 0.3)
                            chuvi = (dai + rong) * 2 * soluong;
                        else
                            chuvi = dai * 2 * soluong;
                        thetich = dai * rong * sau * soluong;
                    }
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phui_khoiluong"].Value = Math.Round(khoiluong, 3);
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_chuvi"].Value = Math.Round(chuvi, 3);
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_thetich"].Value = Math.Round(thetich, 3);

                    DuToan();
                }
                catch (Exception)
                {

                }
            }
            tinhlai = true;

        }

        private void GridPhuiDao_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            try
            {
                txtKeypress = e.Control;
                if (GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_Daii" 
                    | GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_rongg" 
                    | GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_sauu" 
                    | GridPhuiDao.CurrentCell.OwningColumn.Name == "phuidao_sll")
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

        public void DuToan()
        {
            try
            {
                mahieuvt = "";
                double sumChuViNhua = 0.0, sumkhoiluongNhua = 0.0;
                double sumChuViBT = 0.0, sumkhoiluongBT = 0.0;
                double sumDatC4 = 0.0, sumDatC3 = 0.0, sumxutDat = 0.0;
                double sumTheTich = 0.0;
                double sumKLDa4 = 0.0;
                double sumKLCat = 0.0;
                double SODHN = 0.0;
                DAL.C_HeSo.getHeSoPhuiDao();
                for (int i = 0; i < GridPhuiDao.Rows.Count - 1; i++)
                {
                    mahieuvt = GridPhuiDao.Rows[i].Cells["pd_MaKetCau"].Value + "";
                    if (!"".Equals(mahieuvt) && ("N12B".Equals(mahieuvt) || "N12C".Equals(mahieuvt)))
                    {
                        sumChuViNhua += double.Parse(GridPhuiDao.Rows[i].Cells["phuidao_chuvi"].Value + "");
                        //sumkhoiluongNhua += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * 0.12;
                        sumkhoiluongNhua += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._KL_NHUA12;
                        // sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * 0.4;DATC4_NHUA12
                        sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._DATC4_NHUA12;
                    }
                    else if (!"".Equals(mahieuvt) && ("NHUA10".Equals(mahieuvt) || "NHUA10-C3".Equals(mahieuvt)))
                    {
                        sumChuViNhua += double.Parse(GridPhuiDao.Rows[i].Cells["phuidao_chuvi"].Value + "");
                        sumkhoiluongNhua += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._KL_NHUA10;// * 0.1;KL_NHUA10
                        sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._DATC4_NHUA10;// * 0.3;DATC4_NHUA10
                    }
                    else if (!"".Equals(mahieuvt) && ("BT10".Contains(mahieuvt)))
                    {
                        sumChuViBT += double.Parse(GridPhuiDao.Rows[i].Cells["phuidao_chuvi"].Value + "");
                        sumkhoiluongBT += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._KL_BT10;// * 0.1;KL_BT10
                        sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._DATC4_BT10;// * 0.3;DATC4_BT10
                    }
                    else if (!"".Equals(mahieuvt) && ("DXANH".Equals(mahieuvt)))
                    {
                        sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._DATC4_DAXANH;// * 0.25;DATC4_DAXANH
                    }
                    else if (!"".Equals(mahieuvt) && ("DDO".Equals(mahieuvt)))
                    {
                        sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._DATC4_DADO;// * 0.25;DATC4_DADO
                    }
                    else if (!"".Equals(mahieuvt) && ("TNHA".Equals(mahieuvt)))
                    {
                        //sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells[8].Value + "")  ; 
                        SODHN += double.Parse(GridPhuiDao.Rows[i].Cells["phuidao_sll"].Value + "");
                    }
                    else
                    {
                        sumChuViBT += double.Parse(GridPhuiDao.Rows[i].Cells["phuidao_chuvi"].Value + "");
                        sumkhoiluongBT += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._KL_CONLAI;// * 0.05;
                        sumDatC4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._DATC4_CONLAI;// * 0.1;
                    }
                    sumTheTich += double.Parse(GridPhuiDao.Rows[i].Cells["phuidao_thetich"].Value + "");
                    if (!"".Equals(mahieuvt) && !("TNHA".Equals(mahieuvt)))
                    {
                        sumKLDa4 += double.Parse(GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "") * DAL.C_HeSo._KLDA04_TNHA;// * 0.1;KLDA04_TNHA
                    }
                    //If TLAP!Loai <> "TNHA" Then
                    //        sumKLDa += 4 + (TLAP!DT * 0.1) double.Parse(GridPhuiDao.Rows[i].Cells[8].Value + "") * 0.1;  
                    //   End If              }

                    //sumKLCat = sumTheTich - sumKLDa4 - SODHN * 0.18;
                    //sumDatC3 = sumTheTich - (sumkhoiluongNhua + sumkhoiluongBT + sumDatC4);
                }
                sumxutDat = sumTheTich;
                if (_shs.Contains("DD"))
                {
                    sumKLCat = sumTheTich - sumKLDa4;// - SODHN * 0.18;
                }
                else
                {
                    sumKLCat = sumTheTich - sumKLDa4 - int.Parse(this.txtSoHo.Value + "") * DAL.C_HeSo._CHISODD;//// 0.18;CHISODD
                }

                sumDatC3 = Math.Round(sumTheTich, 2) - (Math.Round(sumkhoiluongNhua, 2) + Math.Round(sumkhoiluongBT, 2) + Math.Round(sumDatC4, 2));
                this.txtKhoiLuongCatNhua.Text = String.Format("{0:0.00}", sumkhoiluongNhua);
                this.txtChuViCatNhua.Text = String.Format("{0:0.00}", sumChuViNhua);
                this.txtKhoiLuongBT.Text = String.Format("{0:0.00}", sumkhoiluongBT);
                this.txtChuViBT.Text = String.Format("{0:0.00}", sumChuViBT);
                this.txtDatCap4.Text = String.Format("{0:0.00}", sumDatC4);
                this.txtDatCap3.Text = String.Format("{0:0.00}", sumDatC3);
                this.txtXucDatThua.Text = String.Format("{0:0.00}", sumxutDat);
                this.txtKLDa.Text = String.Format("{0:0.00}", sumKLDa4);
                this.txtKLCat.Text = String.Format("{0:0.00}", sumKLCat);
            }
            catch (Exception ex)
            {
                log.Error("Loi Tinh Du Toan " + ex.Message);
            }
            
        }

        private void GridPhuiDao_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    string s_dai = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_Daii"].Value + "";
                    string s_rong = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_rongg"].Value + "";
                    string s_sau = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sauu"].Value + "";
                    string s_soluong = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sll"].Value + "";
                    double dai = double.Parse(s_dai);
                    double rong = double.Parse(s_rong);
                    double sau = double.Parse(s_sau);
                    int soluong = int.Parse(s_soluong);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_Daii"].Value = String.Format("{0:0.00}", dai);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_rongg"].Value = String.Format("{0:0.00}", rong);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sauu"].Value = String.Format("{0:0.00}", sau);
                    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sll"].Value =   soluong;                   
                    double khoiluong = 0.0;
                    double chuvi = 0.0;
                    double thetich = 0.0;
                    if (dai >= 0 && rong >= 0 && soluong >= 1 && sau > 0)
                    {
                        khoiluong = dai * rong * soluong;
                        if (rong > 0.3)
                            chuvi = (dai + rong) * 2 * soluong;
                        else
                            chuvi = dai * 2 * soluong;
                        thetich = dai * rong * sau * soluong;
                    }
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phui_khoiluong"].Value = Math.Round(khoiluong, 3);
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_chuvi"].Value = Math.Round(chuvi, 3);
                    GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_thetich"].Value = Math.Round(thetich, 3);

                    DuToan();
                }

            }
            catch (Exception)
            {

            }

        }
        private void GridPhuiDao_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            //try
            //{
            //    string s_dai = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_Daii"].Value + "";
            //    string s_rong = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_rongg"].Value + "";
            //    string s_sau = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sauu"].Value + "";
            //    string s_soluong = GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sll"].Value + "";

            //    double dai = double.Parse(s_dai);
            //    double rong = double.Parse(s_rong);
            //    double sau = double.Parse(s_sau);
            //    int soluong = int.Parse(s_soluong);
            //    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_Daii"].Value = String.Format("{0:0.00}", dai);
            //    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_rongg"].Value = String.Format("{0:0.00}", rong);
            //    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sauu"].Value = String.Format("{0:0.00}", sau);
            //    this.GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_sll"].Value =  soluong;
   
            //        double khoiluong = 0.0;
            //        double chuvi = 0.0;
            //        double thetich = 0.0;
            //        if (dai >= 0 && rong >= 0 && soluong >= 1 && sau > 0)
            //        {
            //            khoiluong = dai * rong * soluong;
            //            if (rong > 0.3)
            //                chuvi = (dai + rong) * 2 * soluong;
            //            else
            //                chuvi = dai * 2 * soluong;
            //            thetich = dai * rong * sau * soluong;
            //        }
            //        GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phui_khoiluong"].Value = Math.Round(khoiluong, 3);
            //        GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_chuvi"].Value = Math.Round(chuvi, 3);
            //        GridPhuiDao.Rows[GridPhuiDao.CurrentRow.Index].Cells["phuidao_thetich"].Value = Math.Round(thetich, 3);

            //        DuToan();                       
            //}
            //catch (Exception)
            //{

            //}
        }

        private void GridPhuiDao_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                DuToan();
                if ("0.18".Equals(this.txtDatCap4.Text.Trim())) {
                    this.txtDatCap4.Text = "0.00";
                }
                if ("0.18".Equals(this.txtDatCap3.Text.Trim()))
                {
                    this.txtDatCap3.Text = "0.00";
                }
                if ("0.18".Equals(this.txtXucDatThua.Text.Trim()))
                {
                    this.txtXucDatThua.Text = "0.00";
                }

               view = true;
               loadcongtac = true;
                
            }
            catch (Exception)
            {
            }

        }

        private void GridPhuiDao_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < GridPhuiDao.RowCount - 1)
                {
                    //  MessageBox.Show(this,"Dữ Liệu Không Được trống và lớn hơn 0 !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if ("".Equals(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_Daii"].Value) || this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_Daii"].Value == null || Convert.ToDouble(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_Daii"].Value.ToString()) <= 0)
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_Daii"].ErrorText = "Dữ Liệu Không Được trống và lớn hơn 0 !";

                    }
                    else
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_Daii"].ErrorText = null;
                    }

                    if ("".Equals(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_rongg"].Value) || this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_rongg"].Value == null || Convert.ToDouble(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_rongg"].Value.ToString()) <= 0)
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_rongg"].ErrorText = "Dữ Liệu Không Được trống và lớn hơn 0!";

                    }
                    else
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_rongg"].ErrorText = null;
                    }

                    if ("".Equals(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sauu"].Value) || this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sauu"].Value == null || Convert.ToDouble(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sauu"].Value.ToString()) <= 0)
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sauu"].ErrorText = "Dữ Liệu Không Được trống và lớn hơn 0!";

                    }
                    else
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sauu"].ErrorText = null;
                    }

                    if ("".Equals(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sll"].Value) || this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sll"].Value == null || Convert.ToDouble(this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sll"].Value.ToString()) <= 0)
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sll"].ErrorText = "Dữ Liệu Không Được trống và lớn hơn 0!";

                    }
                    else
                    {
                        this.GridPhuiDao.Rows[e.RowIndex].Cells["phuidao_sll"].ErrorText = null;
                    }
                }
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Cac Cong tac
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        bool loadcongtac = true;
        private void tabCacCongTac_Click(object sender, EventArgs e)
        {
            visibleTab(false, false, false, true, false);
            /*
            try
            {
                string CNHUA = this.txtChuViCatNhua.Text;
                string BOCNHUA = this.txtKhoiLuongCatNhua.Text;
                string CMBTXM = this.txtChuViBT.Text;
                string BMBTXM = this.txtKhoiLuongBT.Text;
                string DP4 = this.txtDatCap4.Text;
                string DP3 = this.txtDatCap3.Text;
                string XUCDAT = this.txtXucDatThua.Text;
                string CAT = this.txtKLCat.Text;
                string DA04 = this.txtKLDa.Text;
                string selectin = "";
                // GridCacCongTac   
                if (tinhlai)
                {
                    tinhlai = false;
                    loadcongtac = false;
                    if (!"".Equals(CNHUA) && double.Parse(CNHUA) > 0.0)
                    {
                        selectin += "'CNHUA',";
                    }
                    if (!"".Equals(BOCNHUA) && double.Parse(BOCNHUA) > 0.0)
                    {
                        selectin += "'BNHUA',";
                    }
                    if (!"".Equals(CMBTXM) && double.Parse(CMBTXM) > 0.0)
                    {
                        selectin += "'CMBTXM',";
                    }
                    if (!"".Equals(BMBTXM) && double.Parse(BMBTXM) > 0.0)
                    {
                        selectin += "'BMBTXM',";
                    }
                    if (!"".Equals(DP4) && double.Parse(DP4) > 0.0)
                    {
                        selectin += "'DP4',";
                    }
                    if (!"".Equals(DP3) && double.Parse(DP3) > 0.0)
                    {
                        selectin += "'DP3',";
                    }
                    if (!"".Equals(XUCDAT) && double.Parse(XUCDAT) > 0.0)
                    {
                        selectin += "'XUCDAT',";
                    }
                    if (!"".Equals(CAT) && double.Parse(CAT) > 0.0)
                    {
                        selectin += "'CAT',";
                    }
                    if (!"".Equals(DA04) && double.Parse(DA04) > 0.0)
                    {
                        selectin += "'DA04',";
                    }
                    //MessageBox.Show(this, selectin.Remove(selectin.Length-1,1));
                    GridCacCongTac.DataSource = DAL.C_DanhMucVatTu.getDMVT(selectin.Remove(selectin.Length - 1, 1));
                    // Add Thêm Colum
                    //GridCacCongTac.Columns.Add(this.congtac_khoiluong);
                    //GridCacCongTac.Columns.Add(this.contac_loaisd);
                    //GridCacCongTac.Columns.Add(this.congtac_VL);
                    //GridCacCongTac.Columns.Add(this.congtac_NC);
                    //GridCacCongTac.Columns.Add(this.congtac_MTC);

                    for (int i = 0; i < GridCacCongTac.Rows.Count; i++)
                    {

                        if ("CNHUA".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CNHUA) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CNHUA) ;
                        }
                        if ("BNHUA".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(BOCNHUA) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(BOCNHUA) ;
                        }
                        if ("CMBTXM".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CMBTXM) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CMBTXM) ;
                        }
                        if ("BMBTXM".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(BMBTXM) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(BMBTXM) ;
                        }
                        if ("DP4".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DP4) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DP4) ;
                        }
                        if ("DP3".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DP3) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DP3);
                        }
                        if ("XUCDAT".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(XUCDAT) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(XUCDAT);
                        }
                        if ("CAT".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CAT) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CAT) ;
                        }
                        if ("DA04".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DA04) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DA04);
                        }
                        GridCacCongTac.Rows[i].Cells["contac_loaisd"].Value = "CM";
                        DANHMUCVATTU dmvt = DAL.C_DanhMucVatTu.finbyMaHieu(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value + "");
                        if (dmvt != null && dmvt.BOVT == true)
                        {
                            DataTable dongiavt = DAL.C_DonGiaVatTu.getDonGiaBoVT(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                GridCacCongTac.Rows[i].Cells["congtac_VL"].Value = dongiavt.Rows[0][0].ToString();
                                GridCacCongTac.Rows[i].Cells["congtac_NC"].Value = dongiavt.Rows[0][1].ToString();
                                GridCacCongTac.Rows[i].Cells["congtac_MTC"].Value = dongiavt.Rows[0][2].ToString();
                            }
                        }
                        else if (dmvt != null)
                        {
                            DONGIAVATTU dongiavt = DAL.C_DonGiaVatTu.getDonGia(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                GridCacCongTac.Rows[i].Cells["congtac_VL"].Value = dongiavt.DGVATLIEU;
                                GridCacCongTac.Rows[i].Cells["congtac_NC"].Value = dongiavt.DGNHANCONG;
                                GridCacCongTac.Rows[i].Cells["congtac_MTC"].Value = dongiavt.DGMAYTHICONG;
                            }
                        }
                    }                   
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi cap Nhat Don Gia" + ex.Message);
            }
            */

        }
        private void GridCacCongTac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                else if (GridCacCongTac.CurrentCell.OwningColumn.Name == "contac_loaisd")
                {
                    //GridPhuiDao.Columns["phudao_MaKetCau"].Width = 300;
                    //GridPhuiDao.Columns["pd_KetCauMD"].Width = 200;
                    //cbLoaiSD.Visible = true;
                    //cbLoaiSD.Top = this.GridCacCongTac.Top + GridCacCongTac.GetRowDisplayRectangle(e.RowIndex, true).Top;
                    //cbLoaiSD.Left = this.GridCacCongTac.Left + GridCacCongTac.GetColumnDisplayRectangle(e.ColumnIndex, true).Left;
                    //cbLoaiSD.Width = GridCacCongTac.Columns[e.ColumnIndex].Width;
                    //cbLoaiSD.Height = GridCacCongTac.Rows[e.RowIndex].Height;
                    //cbLoaiSD.BringToFront();

                }
                else
                {
                    try
                    {
                        string _mahieuvt = GridCacCongTac.Rows[e.RowIndex].Cells["congtac_mahieu"].Value + "";
                        if (_mahieuvt != null && !"".Equals(_mahieuvt))
                        {
                            DONGIAVATTU dongiavt = DAL.C_DonGiaVatTu.finbyDonGiaVTbyMahieu(_mahieuvt);
                            if (dongiavt != null)
                            {
                                this.txtDonGiaVatLieu.Text = String.Format("{0:0,0.00}", dongiavt.DGVATLIEU);
                                this.TxtDonGiaNhanCong.Text = String.Format("{0:0,0.00}", dongiavt.DGNHANCONG);
                                this.txtDonGiaMayThiCong.Text = String.Format("{0:0,0.00}", dongiavt.DGMAYTHICONG);
                            }
                            else
                            {
                                MessageBox.Show(this, "Không Tìm Thế Đơn Giá Mã Hiệu.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else {
                            this.txtDonGiaVatLieu.Text = "0.00";
                            this.TxtDonGiaNhanCong.Text = "0.00";
                            this.txtDonGiaMayThiCong.Text = "0.00";
                        }
                    }
                    catch (Exception ex)
                    {
                     //  MessageBox.Show(this, ex.Message);
                        log.Error(ex.Message);
                    }

                }
            }
            catch (Exception )
            {
            }
        }

        private void GridCacCongTac_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["contac_loaisd"].Value = "CM";
            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value = this.txtSoHo.Value;
        }

        private void cbLoaiSD_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["contac_loaisd"].Value = cbLoaiSD.SelectedValue + "";
                cbLoaiSD.Visible = false;
            }
            catch (Exception)
            {

            }
        }
        string _shs = "";
        bool view = true;
        bool banggiadaco = false;
       
        public void LoadDuLieuBangGia(string shs)
        {
            //banggiadaco = false;
            //radioGhiMoi.Checked = false;
            //radioGhiDe.Checked = false;
            loadcongtac = true;
            view = true;

            #region LoadGird
            this.GridPhuiDao.DataSource = DAL.C_BG_KichThuocPhuiDao.getListBySHS(shs);

            BG_KHOILUONGXDCB bgKL = DAL.C_KhoiLuongXDCB.findBySHS(shs);
            if (bgKL != null)
            {
                banggiadaco = true;
                loadcongtac = false;
                //radioGhiDe.Checked = true;
                view = false;
                this.txtKhoiLuongCatNhua.Text = String.Format("{0:0.00}", bgKL.BOCNHUA);
                this.txtChuViCatNhua.Text = String.Format("{0:0.00}", bgKL.CATNHUA);
                this.txtKhoiLuongBT.Text = String.Format("{0:0.00}", bgKL.BOCBTXM);
                this.txtChuViBT.Text = String.Format("{0:0.00}", bgKL.CATBTXM);
                this.txtDatCap4.Text = String.Format("{0:0.00}", bgKL.DATCAP4);
                this.txtDatCap3.Text = String.Format("{0:0.00}", bgKL.DATCAP3);
                this.txtXucDatThua.Text = String.Format("{0:0.00}", bgKL.XUCDAT);
                this.txtKLDa.Text = String.Format("{0:0.00}", bgKL.DA04);
                this.txtKLCat.Text = String.Format("{0:0.00}", bgKL.CAT);
                if (bgKL.PHICABA != null && !"".Equals(bgKL.PHICABA) && bool.Parse(bgKL.PHICABA + "")) this.checkCoTinhPhiCaBa.Checked = true;
                else this.checkCoTinhPhiCaBa.Checked = false;

                if (bgKL.PHIGIAMSAT != null && !"".Equals(bgKL.PHIGIAMSAT) && bool.Parse(bgKL.PHIGIAMSAT + "")) this.checkPhiGiamSat.Checked = true;
                else this.checkPhiGiamSat.Checked = false;

                if (bgKL.PHIQUANLY != null && !"".Equals(bgKL.PHIQUANLY) && bool.Parse(bgKL.PHIQUANLY + "")) this.checkPhiQuanLy.Checked = true;
                else this.checkPhiQuanLy.Checked = false;

                if (bgKL.KHTUTAILAP != null && !"".Equals(bgKL.KHTUTAILAP) && bool.Parse(bgKL.KHTUTAILAP + ""))
                {
                    this.vatTuXDCBKhachHangCap.Checked = true;
                    khachangtuTaiLap = true;
                }
                else { this.vatTuXDCBKhachHangCap.Checked = false;
                    khachangtuTaiLap = false;
                }

                //  Load Thong Tin Gia Tri Cu
                NgayLapBGCu.Text = Utilities.DateToString.NgayVN(bgKL.CREATEDATE.Value);
                TienGanTLKCu.Text = String.Format("{0:0,0.00}", bgKL.TONGIATRI);
                TienTLMDCu.Text = String.Format("{0:0,0.00}", bgKL.TAILAPMATDUONG);
                TienTongGiaTriCu.Text = String.Format("{0:0,0.00}", (bgKL.TONGIATRI + bgKL.TAILAPMATDUONG));


                //
            }
            else {
                banggiadaco = false;
                view = true;
            }

            this.congtac_khoiluong.DataPropertyName = "KHOILUONG";
            this.GridCacCongTac.DataSource = DAL.C_CongTacBangGia.getListBySHS(shs);
            #endregion
        }

        public void LoadDuLieuBangGia(string shs, int lan)
        {
            //banggiadaco = false;
            //radioGhiMoi.Checked = false;
            //radioGhiDe.Checked = false;
            loadcongtac = true;
            view = true;

            #region LoadGird
            this.GridPhuiDao.DataSource = DAL.C_BGDC_KICHTHUOCPHUIDAO.getListBySHS(shs, lan);

            BGDC_KHOILUONGXDCB bgKL = DAL.C_BGDC_KhoiLuongXDCB.findBySHS(shs, lan);
            if (bgKL != null)
            {
                banggiadaco = true;
                loadcongtac = false;
                //radioGhiDe.Checked = true;
                view = false;
                this.txtKhoiLuongCatNhua.Text = String.Format("{0:0.00}", bgKL.BOCNHUA);
                this.txtChuViCatNhua.Text = String.Format("{0:0.00}", bgKL.CATNHUA);
                this.txtKhoiLuongBT.Text = String.Format("{0:0.00}", bgKL.BOCBTXM);
                this.txtChuViBT.Text = String.Format("{0:0.00}", bgKL.CATBTXM);
                this.txtDatCap4.Text = String.Format("{0:0.00}", bgKL.DATCAP4);
                this.txtDatCap3.Text = String.Format("{0:0.00}", bgKL.DATCAP3);
                this.txtXucDatThua.Text = String.Format("{0:0.00}", bgKL.XUCDAT);
                this.txtKLDa.Text = String.Format("{0:0.00}", bgKL.DA04);
                this.txtKLCat.Text = String.Format("{0:0.00}", bgKL.CAT);
                if (bgKL.PHICABA != null && !"".Equals(bgKL.PHICABA) && bool.Parse(bgKL.PHICABA + "")) this.checkCoTinhPhiCaBa.Checked = true;
                else this.checkCoTinhPhiCaBa.Checked = false;

                if (bgKL.PHIGIAMSAT != null && !"".Equals(bgKL.PHIGIAMSAT) && bool.Parse(bgKL.PHIGIAMSAT + "")) this.checkPhiGiamSat.Checked = true;
                else this.checkPhiGiamSat.Checked = false;

                if (bgKL.PHIQUANLY != null && !"".Equals(bgKL.PHIQUANLY) && bool.Parse(bgKL.PHIQUANLY + "")) this.checkPhiQuanLy.Checked = true;
                else this.checkPhiQuanLy.Checked = false;

                if (bgKL.KHTUTAILAP != null && !"".Equals(bgKL.KHTUTAILAP) && bool.Parse(bgKL.KHTUTAILAP + "")) this.vatTuXDCBKhachHangCap.Checked = true;
                else this.vatTuXDCBKhachHangCap.Checked = false;

                //  Load Thong Tin Gia Tri Cu
                NgayLapBGCu.Text = Utilities.DateToString.NgayVN(bgKL.CREATEDATE.Value);
                TienGanTLKCu.Text = String.Format("{0:0,0.00}", bgKL.TONGIATRI);
                TienTLMDCu.Text = String.Format("{0:0,0.00}", bgKL.TAILAPMATDUONG);
                TienTongGiaTriCu.Text = String.Format("{0:0,0.00}", (bgKL.TONGIATRI + bgKL.TAILAPMATDUONG));
            }
            else
            {
                banggiadaco = false;
                view = true;
            }
            this.congtac_khoiluong.DataPropertyName = "SUDUNGLAI";
            this.GridCacCongTac.DataSource = DAL.C_BGDC_CongTacBangGia.getListBySHS(shs, lan);
            
            #endregion
        }
        public void refresh() {
            this.txtHoTen.Text = "";
            this.txtSoNha.Text = "";
            this.txtDuong.Text = "";
            this.txtPhuong.Text = "";
            this.txtQuan.Text = "";
            this.txtSoHo.Value = 0;
            this.txtLoaiKH.Text = "";
            this.txtDanhBo.Text = "";
            this.txtTenBangThietKe.Text = "";
            this.txtSoDoVien.Text = "";
            checkKhoan.Checked = false;
            txtLoaiKhoan.Text = "";
          
            checkHSCoXinPhepDaoDuong.Checked = true;
            checkHSTrinhKyBGD.Checked = true;
            checkChinhSuLyLichKH.Checked = false;
            checkCoTinhPhiCaBa.Checked = false;
            checkPhiGiamSat.Checked = true;
            checkPhiQuanLy.Checked = true;
            vatTuXDCBKhachHangCap.Checked = false;
            this.GridPhuiDao.DataSource = DAL.C_BG_KichThuocPhuiDao.getListBySHS("000000000");
            this.GridCacCongTac.DataSource = DAL.C_CongTacBangGia.getListBySHS("000000000");
            this.txtKhoiLuongCatNhua.Text = "0";
            this.txtChuViCatNhua.Text = "0";
            this.txtKhoiLuongBT.Text = "0";
            this.txtChuViBT.Text = "0";
            this.txtDatCap4.Text = "0";
            this.txtDatCap3.Text = "0";
            this.txtXucDatThua.Text = "0";
            this.txtKLDa.Text = "0";
            this.txtKLCat.Text = "0";
            view = true;
            banggiadaco = false;
           

            //for (int i = 0; i < GridCacCongTac.Rows.Count; i++)
            //{
            //    GridCacCongTac.Rows.RemoveAt(i);
            //}

            //for (int j = 0; j < GridPhuiDao.Rows.Count; j++)
            //{
            //    GridPhuiDao.Rows.RemoveAt(j);
            //}
            
        }
        int solandieuchinh = 1;
        private void txtSHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tabControl2.SelectedTabIndex = 0;
                DataTable table = DAL.C_DonKhachHang.finbyDonKHTinhDuToan(this.txtSHS.Text);
                if (table.Rows.Count <= 0)
                {
                    MessageBox.Show(this, "Không Tìm Thấy Đơn Khách Hàng.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    refresh();
                    this.txtSHS.Focus();
                }
                else
                {
                    banggiadaco = false;
                    view = true;
                    _shs = table.Rows[0][1].ToString();
                    this.txtHoTen.Text = table.Rows[0][2].ToString();
                    this.txtSoNha.Text = table.Rows[0][4].ToString();
                    this.txtDuong.Text = table.Rows[0][5].ToString();
                    this.txtPhuong.Text = table.Rows[0][6].ToString();
                    this.txtQuan.Text = table.Rows[0][7].ToString();
                    this.txtSoHo.Value = int.Parse(table.Rows[0][8].ToString());
                    this.txtLoaiKH.Text = table.Rows[0][9].ToString();
                    this.txtDanhBo.Text = table.Rows[0][10].ToString();
                    this.txtTenBangThietKe.Text = table.Rows[0][11].ToString();
                    this.txtSoDoVien.Text = table.Rows[0][12].ToString();
                    if ("True".Equals(table.Rows[0][13].ToString()))
                    {
                        checkKhoan.Checked = true;
                        txtLoaiKhoan.Text = table.Rows[0][14].ToString();

                    }
                    else
                    {
                        checkKhoan.Checked = false;
                        txtLoaiKhoan.Text = table.Rows[0][14].ToString();
                    }
                    solandieuchinh = 1;
                    LoadTabCacLanDieuChinh();
                    //LoadDuLieuBangGia(_shs);
                    if (GridPhuiDao.Rows.Count <=1) {
                        this.txtKhoiLuongCatNhua.Text = "0";
                        this.txtChuViCatNhua.Text = "0";
                        this.txtKhoiLuongBT.Text = "0";
                        this.txtChuViBT.Text = "0";
                        this.txtDatCap4.Text = "0";
                        this.txtDatCap3.Text = "0";
                        this.txtXucDatThua.Text = "0";
                        this.txtKLDa.Text = "0";
                        this.txtKLCat.Text = "0";
                    
                    }
                }
            }
        }
        double getChieuDaiHDPE()
        {
            double kq = 0;
            for (int i = 0; i < GridPhuiDao.Rows.Count; i++) {
                object obj = GridPhuiDao.Rows[i].Cells["phuidao_Daii"].Value;
                if (obj != null && !"".Equals(obj+"")) {
                    kq += double.Parse(obj + "");
                }
            }
            kq += double.Parse(txtSoHo.Value+"")* 0.5;
            return Math.Abs(kq-0.4);
        }
        string mahieuvt = "";
        private void GridCacCongTac_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GridCacCongTac.CurrentCell.OwningColumn.Name == "congtac_mahieu")
            {
                DANHMUCVATTU dmvt = DAL.C_DanhMucVatTu.finbyMaHieu(GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_mahieu"].Value + "");
                if (dmvt != null)
                {

                    GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_hanmuc"].Value = dmvt.TENVT.ToUpper();
                    GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_dvt"].Value = dmvt.DVT;
                    GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value = this.txtSoHo.Value;
                    GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["contac_loaisd"].Value = "CM";
                    GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtacMahieuDG"].Value = dmvt.MAHDG;
                    GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congTacNhom"].Value = dmvt.NHOMVT;
                    if (dmvt.TENVT.ToUpper().Contains("ỐNG NHỰA")) {
                        GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value = getChieuDaiHDPE();
                    }

                    if (dmvt.BOVT == true)
                    {
                        DataTable dongiavt = DAL.C_DonGiaVatTu.getDonGiaBoVT(dmvt.MAHIEU);
                        if (dongiavt != null)
                        {
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_VL"].Value = dongiavt.Rows[0][0].ToString();
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_NC"].Value = dongiavt.Rows[0][1].ToString();
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_MTC"].Value = dongiavt.Rows[0][2].ToString();
                        }
                        else
                        {
                            MessageBox.Show(this, "Không Tìm Thấy Mã Hiệu Đơn Giá.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_mahieu"].Selected = true;
                            return;
                        }
                    }
                    else
                    {
                        DONGIAVATTU dongiavt = DAL.C_DonGiaVatTu.getDonGia(dmvt.MAHIEU);
                        if (dongiavt != null)
                        {
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_VL"].Value = dongiavt.DGVATLIEU;
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_NC"].Value = dongiavt.DGNHANCONG;
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_MTC"].Value = dongiavt.DGMAYTHICONG;
                        }
                        else
                        {
                            MessageBox.Show(this, "Không Tìm Thấy Mã Hiệu Đơn Giá.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_mahieu"].Selected = true;
                            return;
                        }
                    }

                }
                else
                {
                    MessageBox.Show(this, "Không Tìm Thấy Mã Hiệu Đơn Giá.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_mahieu"].Selected = true;
                    return;
                }
            }

            ////
            try
            {
                if (GridCacCongTac.CurrentCell.OwningColumn.Name == "congtac_khoiluong")
                {
                    if (double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value + "") > 10)
                    {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value = String.Format("{0:0,0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value + "")); ;
                    }
                    else
                    {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value = String.Format("{0:0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value + "")); ;
                    }
                }
                if (GridCacCongTac.CurrentCell.OwningColumn.Name == "congtac_capmoi")
                {
                    if (double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_capmoi"].Value + "") > 10)
                    {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_capmoi"].Value = String.Format("{0:0,0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_capmoi"].Value + "")); ;
                    }
                    else
                    {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_capmoi"].Value = String.Format("{0:0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_capmoi"].Value + "")); ;
                    }
                }
                if (GridCacCongTac.CurrentCell.OwningColumn.Name == "congtac_thuhoi")
                {
                    if (double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_thuhoi"].Value + "") > 10)
                    {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_thuhoi"].Value = String.Format("{0:0,0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_thuhoi"].Value + "")); ;
                    }
                    else
                    {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_thuhoi"].Value = String.Format("{0:0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_thuhoi"].Value + "")); ;
                    }
                }

            }
            catch (Exception)
            { }
        }

        private void GridCacCongTac_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                txtKeypress = e.Control;
                if (GridCacCongTac.CurrentCell.OwningColumn.Name == "congtac_khoiluong" | GridCacCongTac.CurrentCell.OwningColumn.Name == "vattu_capmoi" | GridCacCongTac.CurrentCell.OwningColumn.Name == "vattu_thuhoi")
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

        private void GridCacCongTac_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // try
            //{
            //    if (e.RowIndex < GridCacCongTac.RowCount - 1)
            //    {
            //        //  MessageBox.Show(this,"Dữ Liệu Không Được trống và lớn hơn 0 !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        if ("".Equals(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value) || this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value == null || Convert.ToDouble(this.GridCacCongTac.Rows[e.RowIndex].Cells["phuidao_Daii"].Value.ToString()) <= 0)
            //        {
            //            this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].ErrorText = "Dữ Liệu Không Được trống và lớn hơn 0 !";

            //        }
            //        else
            //        {
            //            this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].ErrorText = null;
            //        }
            //    }
            //}
            //catch (Exception)
            //{ }            
        }
        

        private void btChonLaiDonGia_Click(object sender, EventArgs e)
        {
            //frm_ChonLaiDG from = new frm_ChonLaiDG();
            //from.ShowDialog();

        }
    
        public void InsertBG_KICHTHUOCPHUIDAO()
        {
            //try
            //{
                for (int i = 0; i < this.GridPhuiDao.Rows.Count; i++)
                {
                    string maketcau = this.GridPhuiDao.Rows[i].Cells["pd_MaKetCau"].Value + "";
                    if (!"".Equals(maketcau) && !"".Equals(_shs))
                    {
                        BGDC_KICHTHUOCPHUIDAO phuidao = new BGDC_KICHTHUOCPHUIDAO();
                        phuidao.SHS = _shs;
                        phuidao.LAN = solandieuchinh;
                        phuidao.MADANHMUC = this.GridPhuiDao.Rows[i].Cells["pd_MaKetCau"].Value + "";
                        phuidao.TENKETCAU = this.GridPhuiDao.Rows[i].Cells["phuidao_tenketcau"].Value + "";
                        phuidao.DVT = this.GridPhuiDao.Rows[i].Cells["phuidao_dvt"].Value + "";
                        phuidao.DAI = double.Parse(this.GridPhuiDao.Rows[i].Cells["phuidao_Daii"].Value + "");
                        phuidao.RONG = double.Parse(this.GridPhuiDao.Rows[i].Cells["phuidao_rongg"].Value + "");
                        phuidao.DOSAU = double.Parse(this.GridPhuiDao.Rows[i].Cells["phuidao_sauu"].Value + "");
                        phuidao.SOLUONG = double.Parse(this.GridPhuiDao.Rows[i].Cells["phuidao_sll"].Value + "");
                        phuidao.KHOILUONG = double.Parse(this.GridPhuiDao.Rows[i].Cells["phui_khoiluong"].Value + "");
                        phuidao.CHUVI = double.Parse(this.GridPhuiDao.Rows[i].Cells["phuidao_chuvi"].Value + "");
                        phuidao.THETICH = double.Parse(this.GridPhuiDao.Rows[i].Cells["phuidao_thetich"].Value + "");
                        phuidao.COTINHTL = bool.Parse(this.GridPhuiDao.Rows[i].Cells["phidao_cotll"].Value + "");
                        phuidao.CREATEBY = DAL.C_USERS._userName;
                        phuidao.CREATEDATE = DateTime.Now;
                        //DAL.C_BG_KichThuocPhuiDao.InsertKTPD(phuidao);
                        DAL.C_BGDC_KICHTHUOCPHUIDAO.InsertKTPD(phuidao);
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Loi Insert Kich Thuoc Phui Dao " + ex.Message);
            //}
            
        }
        
        public void InsertCONGTACBANGGIA()
        {
            //try
            //{
                for (int i = 0; i < this.GridCacCongTac.Rows.Count; i++)
                {
                    string maketcau = this.GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value + "";
                    if (!"".Equals(maketcau) && !"".Equals(_shs))
                    {
                        BGDC_CONGTACBANGIA congtacbg = new BGDC_CONGTACBANGIA();
                        congtacbg.SHS = _shs;
                        string _mahieuvt = this.GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value + "";
                        congtacbg.MAHIEU = _mahieuvt;
                        congtacbg.LAN = solandieuchinh;                     
                        DANHMUCVATTU dmvt= DAL.C_DanhMucVatTu.finbyMaHieu(_mahieuvt);
                        congtacbg.MAHDG = dmvt.MAHDG;
                        congtacbg.TENVT = dmvt.TENVT.ToUpper();
                        congtacbg.DVT = dmvt.DVT;
                        string nhom = dmvt.NHOMVT;
                        string loaisd = this.GridCacCongTac.Rows[i].Cells["contac_loaisd"].Value + "";
                        congtacbg.NHOM = this.GridCacCongTac.Rows[i].Cells["congTacNhom"].Value + "";
                        congtacbg.LOAISN = loaisd;
                        double sql = double.Parse(this.GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value!=null ? this.GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value.ToString() : "0.0");
                        double capmoi = double.Parse(this.GridCacCongTac.Rows[i].Cells["congtac_capmoi"].Value != null ? this.GridCacCongTac.Rows[i].Cells["congtac_capmoi"].Value.ToString() : "0.0");  //double.Parse(this.GridCacCongTac.Rows[i].Cells["congtac_capmoi"].Value + "");
                        double thuhoi = double.Parse(this.GridCacCongTac.Rows[i].Cells["congtac_thuhoi"].Value != null ? this.GridCacCongTac.Rows[i].Cells["congtac_thuhoi"].Value.ToString() : "0.0");//  double.Parse(this.GridCacCongTac.Rows[i].Cells["congtac_thuhoi"].Value + "");
                        congtacbg.KHOILUONG = sql + capmoi;
                        congtacbg.SUDUNGLAI = sql;
                        congtacbg.CAPMOI = capmoi;
                        congtacbg.THUHOI = thuhoi;

                        //if ("XDCB".Equals(nhom))
                        //{
                        //    congtacbg.KHOILUONG = double.Parse(this.GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value + "")/1000;
                        //}
                        //else {
                        //    congtacbg.KHOILUONG = double.Parse(this.GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value + "");
                        //}
                        
                        double vatlieu = 0.0;
                        double nhancong =  0.0;
                        double maythicong =  0.0;
                        if (dmvt.BOVT == true)
                        {
                            DataTable dongiavt = DAL.C_DonGiaVatTu.getDonGiaBoVT(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                vatlieu =double.Parse( dongiavt.Rows[0][0].ToString());
                                nhancong = double.Parse(dongiavt.Rows[0][1].ToString());
                                maythicong = double.Parse(dongiavt.Rows[0][2].ToString());
                            }
                        }
                        else
                        {
                            DONGIAVATTU dongiavt = DAL.C_DonGiaVatTu.getDonGia(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                vatlieu = dongiavt.DGVATLIEU.Value;
                                nhancong = dongiavt.DGNHANCONG.Value;
                                maythicong = dongiavt.DGMAYTHICONG.Value;
                            }
                        }

                        switch (loaisd)
                        {
                            case "SDL":
                                if (!"XDCB".Equals(nhom))
                                {
                                    DAL.C_HeSo.getHeSoBangGia();
                                    congtacbg.TENVT = "SỬ DỤNG LẠI " + dmvt.TENVT.ToUpper() + "";
                                    congtacbg.DONGIAVL = 0;
                                    congtacbg.DONGIANC = nhancong * DAL.C_HeSo._HSSuDungLai;
                                    congtacbg.DONGIAMTC = maythicong * DAL.C_HeSo._HSSuDungLai;
                                }
                                else
                                {
                                    congtacbg.DONGIAVL = vatlieu;
                                    congtacbg.DONGIANC = nhancong;
                                    congtacbg.DONGIAMTC = maythicong;
                                }
                                break;
                            case "AP":
                                congtacbg.TENVT = dmvt.TENVT.ToUpper() + "" + " (THỬ ÁP)";
                                congtacbg.DONGIAVL = vatlieu;
                                congtacbg.DONGIANC = nhancong;
                                congtacbg.DONGIAMTC = maythicong;
                                break;
                            case "HHTH":
                                if (!"XDCB".Equals(nhom))
                                {
                                    DAL.C_HeSo.getHeSoBangGia();
                                    congtacbg.TENVT = "CÔNG GỞ " + dmvt.TENVT.ToUpper() + " (VẬT TƯ CŨ)";
                                    congtacbg.DONGIAVL = 0;
                                    congtacbg.DONGIANC = nhancong * DAL.C_HeSo._HSThuHoi;
                                    congtacbg.DONGIAMTC = maythicong * DAL.C_HeSo._HSThuHoi;
                                }
                                else
                                {
                                    congtacbg.DONGIAVL = vatlieu;
                                    congtacbg.DONGIANC = nhancong;
                                    congtacbg.DONGIAMTC = maythicong;
                                }

                                break;
                            case "CMTH":
                                congtacbg.DONGIAVL = 0.0;
                                congtacbg.DONGIANC = 0.0;
                                congtacbg.DONGIAMTC = 0.0;
                                break;
                            default:
                                if ("XDCB".Equals(nhom))
                                {
                                    if ((_mahieuvt.Equals("CAT") || _mahieuvt.Equals("DA04") || _mahieuvt.Equals("BT")) && vatTuXDCBKhachHangCap.Checked)
                                    {

                                        congtacbg.DONGIAVL = 0.0;
                                        congtacbg.DONGIAMTC = 0.0;
                                        congtacbg.TENVT = dmvt.TENVT.ToUpper() + " (K/H CUNG CẤP) ";

                                    }
                                    else
                                    {
                                        congtacbg.DONGIAVL = vatlieu;
                                    }
                                }
                                else
                                {
                                    congtacbg.DONGIAVL = vatlieu;
                                }
                                congtacbg.DONGIANC = nhancong;
                                congtacbg.DONGIAMTC = maythicong;
                                break;
                        }                      
                        congtacbg.CREATEBY = DAL.C_USERS._userName;
                        congtacbg.CREATEDATE = DateTime.Now;
                        //DAL.C_CongTacBangGia.InsertCongTacBG(congtacbg);
                        DAL.C_BGDC_CongTacBangGia.InsertCongTacBG(congtacbg);
                        //DAL.C_BG_KICHTHUOCPHUIDAO.InsertKTPD(phuidao);

                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Loi Insert Cong Tac Bang Gia " + ex.Message);
            //}
            

        }
        double gantlcu = 0.0;
        double tlmdcu = 0.0;
        double tongiatricu =  0.0;

        double kqgantlk = 0.0;
        double kqtlmd = 0.0;
        double kqtonggiatri = 0.0;
        public void InsertKHOILUONGXDCB() {
            try
            {
                 gantlcu = 0.0;
                 tlmdcu = 0.0;
                 tongiatricu = 0.0;

                 kqgantlk = 0.0;
                 kqtlmd = 0.0;
                 kqtonggiatri = 0.0;

                string CNHUA = this.txtChuViCatNhua.Text;
                string BOCNHUA = this.txtKhoiLuongCatNhua.Text;
                string CMBTXM = this.txtChuViBT.Text;
                string BMBTXM = this.txtKhoiLuongBT.Text;
                string DC4 = this.txtDatCap4.Text;
                string DC3 = this.txtDatCap3.Text;
                string XUCDAT = this.txtXucDatThua.Text;
                string CAT = this.txtKLCat.Text;
                string DA04 = this.txtKLDa.Text;

                double d_CNHUA = 0.0;
                if (!"".Equals(CNHUA) && double.Parse(CNHUA) > 0.0)
                {
                    d_CNHUA = double.Parse(CNHUA);
                }
                double d_BOCNHUA = 0.0;
                if (!"".Equals(BOCNHUA) && double.Parse(BOCNHUA) > 0.0)
                {
                    d_BOCNHUA = double.Parse(BOCNHUA);
                }
                double d_CMBTXM = 0.0;
                if (!"".Equals(CMBTXM) && double.Parse(CMBTXM) > 0.0)
                {
                    d_CMBTXM = double.Parse(CMBTXM);
                }
                double d_BMBTXM = 0.0;
                if (!"".Equals(BMBTXM) && double.Parse(BMBTXM) > 0.0)
                {
                    d_BMBTXM = double.Parse(BMBTXM);
                }
                double d_DC4 = 0.0;
                if (!"".Equals(DC4) && double.Parse(DC4) > 0.0)
                {
                    d_DC4 = double.Parse(DC4);
                }
                double d_DC3 = 0.0;
                if (!"".Equals(DC3) && double.Parse(DC3) > 0.0)
                {
                    d_DC3 = double.Parse(DC3);
                }
                double d_XUCDAT = 0.0;
                if (!"".Equals(XUCDAT) && double.Parse(XUCDAT) > 0.0)
                {
                    d_XUCDAT = double.Parse(XUCDAT);
                }
                double d_CAT = 0.0;
                if (!"".Equals(CAT) && double.Parse(CAT) > 0.0)
                {
                    d_CAT = double.Parse(CAT);
                }
                double d_DA04 = 0.0;
                if (!"".Equals(DA04) && double.Parse(DA04) > 0.0)
                {
                    d_DA04 = double.Parse(DA04);
                }
                BGDC_KHOILUONGXDCB klxdcb = new BGDC_KHOILUONGXDCB();
                klxdcb.SHS = _shs;
                klxdcb.LAN = solandieuchinh;
                klxdcb.CATNHUA = d_CNHUA;
                klxdcb.BOCNHUA = d_BOCNHUA;
                klxdcb.CATBTXM = d_CMBTXM;
                klxdcb.BOCBTXM = d_BMBTXM;
                klxdcb.DATCAP4 = d_DC4;
                klxdcb.DATCAP3 = d_DC3;
                klxdcb.CAT = d_CAT;
                klxdcb.DA04 = d_DA04;
                klxdcb.XUCDAT = d_XUCDAT;
                bool phiC3 = false; 
                bool phiQL = false;
                bool phiGS = false;
                if (checkCoTinhPhiCaBa.Checked) {
                    phiC3 = true;
                }
                else
                {
                    phiC3 = false;
                }
                if (checkPhiQuanLy.Checked)
                { 
                    phiQL = true;
                }
                else
                {
                    phiQL = false;
                }

                if (checkPhiGiamSat.Checked)
                {
                    phiGS = true;
                }
                else
                {
                    phiGS = false;
                }
                klxdcb.PHICABA = phiC3;
                klxdcb.PHIGIAMSAT = phiGS;
                klxdcb.PHIQUANLY = phiQL;

               // DAL.C_CongTacBangGia.TongKetChiPhi(_shs, phiC3, phiGS, phiQL);
                DAL.C_BGDC_CongTacBangGia.TongKetChiPhi(_shs, phiC3, phiGS, phiQL, solandieuchinh);

                klxdcb.CPVATTU = DAL.C_BGDC_CongTacBangGia.CPVATLIEU;
                klxdcb.CPNHANCONG = DAL.C_BGDC_CongTacBangGia.CPNHANCONG;
                klxdcb.CPMAYTHICONG = DAL.C_BGDC_CongTacBangGia.CPMAYTHICONG;
                if (checkCoTinhPhiCaBa.Checked)
                {
                    klxdcb.CPCABA = DAL.C_BGDC_CongTacBangGia.CPCABA;
                }
                else
                {
                    klxdcb.CPCABA = 0.0;
                }
                klxdcb.THUE55 = DAL.C_BGDC_CongTacBangGia.THUE55;
                klxdcb.CONG3 = DAL.C_BGDC_CongTacBangGia.TONGTRUOCTHUE;
                klxdcb.THUEGTGT = DAL.C_BGDC_CongTacBangGia.VAT;
                klxdcb.TONGIATRI = DAL.C_BGDC_CongTacBangGia.TONG;
                if (vatTuXDCBKhachHangCap.Checked)
                {
                    klxdcb.KHTUTAILAP = true;
                    klxdcb.TLMDTRUOCTHUE = 0.0;
                    klxdcb.TAILAPMATDUONG = 0.0;
                }
                else
                {
                    klxdcb.KHTUTAILAP = false;
                    klxdcb.TLMDTRUOCTHUE = DAL.C_BGDC_CongTacBangGia.TLMDTRUOCTHUE;
                    klxdcb.TAILAPMATDUONG = DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG;
                }
                klxdcb.CHIPHITRUCTIEP = DAL.C_BGDC_CongTacBangGia.CHIPHITRUCTIEP;
                klxdcb.CHIPHICHUNG = DAL.C_BGDC_CongTacBangGia.CHIPHICHUNG;
                klxdcb.CPGAN = DAL.C_BGDC_CongTacBangGia.CPGAN;
                klxdcb.CPNHUA = DAL.C_BGDC_CongTacBangGia.CPNHUA;
                klxdcb.CREATEBY = DAL.C_USERS._userName;
                klxdcb.CREATEDATE = DateTime.Now;
                klxdcb.SODOVIEN = DAL.C_USERS.findByFullName(this.txtSoDoVien.Text).USERNAME;
                klxdcb.LOAIDC = "Điều Chỉnh";
                klxdcb.TRONGAIDC = "";
                klxdcb.LAN = solandieuchinh;
                for (int i = 0; i < GridCacLanDC.Rows.Count; i++) {
                    string landc = GridCacLanDC.Rows[i].Cells["dieuchinh_lan"].Value + "";
                    if (landc.Equals("" + solandieuchinh)) {
                        
                        klxdcb.LOAIDC = GridCacLanDC.Rows[i].Cells["dieuchinh_loaihs"].Value + "";
                        klxdcb.TRONGAIDC = GridCacLanDC.Rows[i].Cells["dieuchinh_lydotrongai"].Value + "";
                        klxdcb.SODOVIEN = GridCacLanDC.Rows[i].Cells["dieuchinh_SoDV"].Value + "";
                    }
                }
                //klxdcb.LOAIDC = "Điều Chỉnh";
               //DAL.C_KhoiLuongXDCB.InsertKTPD(klxdcb);


                //try
                //{
                    
                //   double gantlcu = !"".Equals(TienGanTLKCu.Text.Trim()) ? double.Parse(TienGanTLKCu.Text.Trim()) : 0.0;
                //   double tlmdcu = !"".Equals(TienTLMDCu.Text.Trim()) ? double.Parse(TienTLMDCu.Text.Trim()) : 0.0;
                //   double tongiatricu = !"".Equals(TienTongGiaTriCu.Text.Trim()) ? double.Parse(TienTongGiaTriCu.Text.Trim()) : 0.0;

                //   double kqgantlk = DAL.C_BGDC_CongTacBangGia.TONG - gantlcu;
                //   double kqtlmd = DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG - tlmdcu;
                //   double kqtonggiatri = (DAL.C_BGDC_CongTacBangGia.TONG + DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG) - tongiatricu;
                //    klxdcb.DCTIENTLK = kqgantlk;
                //    klxdcb.DCTIENTLMD = kqtlmd;
                //    klxdcb.DCTIENTLMD = kqtonggiatri;

                //}
                //catch (Exception)
                //{
                     
                //}
               

                DAL.C_BGDC_KhoiLuongXDCB.InsertKTPD(klxdcb);
                // Tinh gia tri
                try
                {
                    tabControl2.SelectedTabIndex = 3;
                    NgayLapBGMoi.Text = Utilities.DateToString.NgayVN(DateTime.Now.Date);
                    TienGanTLKMoi.Text = String.Format("{0:0,0.00}", DAL.C_BGDC_CongTacBangGia.TONG);
                    TienTLMDMoi.Text = String.Format("{0:0,0.00}", DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG);
                    TongGiaTriMoi.Text = String.Format("{0:0,0.00}", (DAL.C_BGDC_CongTacBangGia.TONG + DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG));

                    double gantlmoi = DAL.C_BGDC_CongTacBangGia.TONG;
                    double tlmdmoi = DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG;
                    double tongiatrimoi = (DAL.C_BGDC_CongTacBangGia.TONG + DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG);

                    gantlcu = !"".Equals(TienGanTLKCu.Text.Trim()) ? double.Parse(TienGanTLKCu.Text.Trim()) : 0.0;
                    tlmdcu = !"".Equals(TienTLMDCu.Text.Trim()) ? double.Parse(TienTLMDCu.Text.Trim()) : 0.0;
                    tongiatricu = !"".Equals(TienTongGiaTriCu.Text.Trim()) ? double.Parse(TienTongGiaTriCu.Text.Trim()) : 0.0;

                    kqgantlk = gantlmoi - gantlcu;
                    kqtlmd = tlmdmoi - tlmdcu;
                    kqtonggiatri = tongiatrimoi - tongiatricu;

                    ketquatonggiatri.Text = String.Format("{0:0,0.00}", kqtonggiatri);
                    ketquaGanTLK.Text = String.Format("{0:0,0.00}", kqgantlk);
                    ketquaTLMD.Text = String.Format("{0:0,0.00}", kqtlmd);
                    if (kqtonggiatri > 0)
                    {
                        lbsotien.Text = "Số Tiền Khách Hàng Đóng Bổ Sung : <b>" + ketquatonggiatri.Text + "</b>";
                    }
                    else if (kqtonggiatri < 0)
                    {
                        lbsotien.Text = "Số Tiền Hoàn Lại Khách Hàng : <b>" + String.Format("{0:0,0.00}", Math.Abs(kqtonggiatri)) + "</b>";
                    }

                    lbBangChu.Text = Utilities.Doctien.ReadMoney("" + String.Format("{0:0}", Math.Abs(kqtonggiatri)));
                }
                catch (Exception ex)
                {
                    log.Error("Tinh GIa Tri Bang Gia " + ex.Message);
                }
               
                
                

                //
            }
            catch (Exception ex)
            {
                log.Error("Loi Insert Khoi Luong XDCB " + ex.Message);
            }
           
        }
        
        
        static double total = 0.0;

        static double _tongketgan = 0.0;
        static double _tongketnhua = 0.0;

        public static DataTable TongKetChiPhi(string shs, bool _PHIC3, bool _PHIGS, bool _PHIQL)
        {
            //TanHoaDataContext db = new TanHoaDataContext();
            //SqlConnection conn = new SqlConnection(db.Connection.ConnectionString);

            //conn.Open();
            //SqlCommand cmd = new SqlCommand("TONGKETCHIPHI", conn);
            //cmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter inparm = cmd.Parameters.Add("@shs", SqlDbType.VarChar);
            //inparm.Direction = ParameterDirection.Input;
            //inparm.Value = shs;

            //SqlParameter PHIC3 = cmd.Parameters.Add("@PHIC3", SqlDbType.Bit);
            //PHIC3.Direction = ParameterDirection.Input;
            //PHIC3.Value = _PHIC3;

            //SqlParameter PHIGS = cmd.Parameters.Add("@PHIGS", SqlDbType.Bit);
            //PHIGS.Direction = ParameterDirection.Input;
            //PHIGS.Value = _PHIGS;

            //SqlParameter PHIQL = cmd.Parameters.Add("@PHIQL", SqlDbType.Bit);
            //PHIQL.Direction = ParameterDirection.Input;
            //PHIQL.Value = _PHIQL;
            

            //SqlParameter _A = cmd.Parameters.Add("@A", SqlDbType.Float);
            //_A.Direction = ParameterDirection.Output;

            //SqlParameter _B = cmd.Parameters.Add("@B", SqlDbType.Float);
            //_B.Direction = ParameterDirection.Output;

            //SqlParameter _C = cmd.Parameters.Add("@C", SqlDbType.Float);
            //_C.Direction = ParameterDirection.Output;

            //SqlParameter _CHIPHICABA = cmd.Parameters.Add("@CPCABA", SqlDbType.Float);
            //_CHIPHICABA.Direction = ParameterDirection.Output;

            //SqlParameter _TONG = cmd.Parameters.Add("@TOTAL", SqlDbType.Float);
            //_TONG.Direction = ParameterDirection.Output;

            //SqlParameter _VAT = cmd.Parameters.Add("@VAT", SqlDbType.Float);
            //_VAT.Direction = ParameterDirection.Output;

            //SqlParameter B1 = cmd.Parameters.Add("@B1", SqlDbType.Float);
            //B1.Direction = ParameterDirection.Output;

            //SqlParameter C1 = cmd.Parameters.Add("@C1", SqlDbType.Float);
            //C1.Direction = ParameterDirection.Output;

            //SqlParameter C2 = cmd.Parameters.Add("@C2", SqlDbType.Float);
            //C2.Direction = ParameterDirection.Output;

            //SqlParameter D = cmd.Parameters.Add("@D", SqlDbType.Float);
            //D.Direction = ParameterDirection.Output;

            //SqlParameter E = cmd.Parameters.Add("@E", SqlDbType.Float);
            //E.Direction = ParameterDirection.Output;

            //SqlParameter F = cmd.Parameters.Add("@F", SqlDbType.Float);
            //F.Direction = ParameterDirection.Output;

            //SqlParameter G = cmd.Parameters.Add("@G", SqlDbType.Float);
            //G.Direction = ParameterDirection.Output;

            //SqlParameter H = cmd.Parameters.Add("@H", SqlDbType.Float);
            //H.Direction = ParameterDirection.Output;

            //SqlParameter I = cmd.Parameters.Add("@I", SqlDbType.Float);
            //I.Direction = ParameterDirection.Output;

            //SqlParameter J = cmd.Parameters.Add("@J", SqlDbType.Float);
            //J.Direction = ParameterDirection.Output;

            //SqlParameter K = cmd.Parameters.Add("@K", SqlDbType.Float);
            //K.Direction = ParameterDirection.Output;

            //SqlParameter L = cmd.Parameters.Add("@L", SqlDbType.Float);
            //L.Direction = ParameterDirection.Output;


            //SqlParameter _TAILAPMATDUONG = cmd.Parameters.Add("@TAILAPMATDUONG", SqlDbType.Float);
            //_TAILAPMATDUONG.Direction = ParameterDirection.Output;

            //SqlParameter _TLMDTRUOCTHUE = cmd.Parameters.Add("@TLMDTRUOCTHUE", SqlDbType.Float);
            //_TLMDTRUOCTHUE.Direction = ParameterDirection.Output;

            //SqlParameter _CPGAN = cmd.Parameters.Add("@CPGAN", SqlDbType.Float);
            //_CPGAN.Direction = ParameterDirection.Output;

            //SqlParameter _CPNHUA = cmd.Parameters.Add("@CPNHUA", SqlDbType.Float);
            //_CPNHUA.Direction = ParameterDirection.Output;

            //cmd.ExecuteNonQuery();
             _tongketgan = 0.0;
             _tongketnhua = 0.0;
            DataTable table = new DataTable("TONGKETKINHPHI");
            table.Columns.Add("SHS", typeof(string));
            table.Columns.Add("A", typeof(double));
            table.Columns.Add("B", typeof(double));
            table.Columns.Add("C", typeof(double));
            table.Columns.Add("CPCABA", typeof(double));
            table.Columns.Add("TOTAL", typeof(double));
            table.Columns.Add("VAT", typeof(double));
            table.Columns.Add("B1", typeof(double));
            table.Columns.Add("C1", typeof(double));
            table.Columns.Add("C2", typeof(double));
            table.Columns.Add("D", typeof(double));
            table.Columns.Add("E", typeof(double));
            table.Columns.Add("F", typeof(double));
            table.Columns.Add("G", typeof(double));
            table.Columns.Add("H", typeof(double));
            table.Columns.Add("I", typeof(double));
            table.Columns.Add("J", typeof(double));
            table.Columns.Add("K", typeof(double));
            table.Columns.Add("L", typeof(double));
            table.Columns.Add("TAILAPMATDUONG", typeof(double));
            table.Columns.Add("TLMDTRUOCTHUE", typeof(double));
            table.Columns.Add("CPGAN", typeof(double));
            table.Columns.Add("CPNHUA", typeof(double));

            DataRow myDataRow = table.NewRow();
            total = DAL.C_BGDC_CongTacBangGia.TONG;
            myDataRow["SHS"] = shs;
            myDataRow["A"] = DAL.C_BGDC_CongTacBangGia.CPVATLIEU;
            myDataRow["B"] = DAL.C_BGDC_CongTacBangGia.B;
            myDataRow["C"] = DAL.C_BGDC_CongTacBangGia.C;
            myDataRow["CPCABA"] = DAL.C_BGDC_CongTacBangGia.CPCABA;
            myDataRow["TOTAL"] = total;
            myDataRow["VAT"] = DAL.C_BGDC_CongTacBangGia.VAT;
            myDataRow["B1"] = DAL.C_BGDC_CongTacBangGia.CPNHANCONG;
            myDataRow["C1"] = DAL.C_BGDC_CongTacBangGia.CPMAYTHICONG;
            myDataRow["C2"] = DAL.C_BGDC_CongTacBangGia.C2;
            myDataRow["D"] = DAL.C_BGDC_CongTacBangGia.D;
            myDataRow["E"] = DAL.C_BGDC_CongTacBangGia.E;
            myDataRow["F"] = DAL.C_BGDC_CongTacBangGia.F;
            myDataRow["G"] = DAL.C_BGDC_CongTacBangGia.G;
            myDataRow["H"] = DAL.C_BGDC_CongTacBangGia.H;
            myDataRow["I"] = DAL.C_BGDC_CongTacBangGia.I;
            myDataRow["J"] = DAL.C_BGDC_CongTacBangGia.J;
            myDataRow["K"] = DAL.C_BGDC_CongTacBangGia.K;
            myDataRow["L"] = DAL.C_BGDC_CongTacBangGia.L;

            myDataRow["TAILAPMATDUONG"] = DAL.C_BGDC_CongTacBangGia.TAILAPMATDUONG;
            myDataRow["TLMDTRUOCTHUE"] = DAL.C_BGDC_CongTacBangGia.TLMDTRUOCTHUE;
            myDataRow["CPGAN"] = DAL.C_BGDC_CongTacBangGia.CPGAN;
            myDataRow["CPNHUA"] = DAL.C_BGDC_CongTacBangGia.CPNHUA;
            _tongketgan = DAL.C_BGDC_CongTacBangGia.CPGAN;
            _tongketnhua = DAL.C_BGDC_CongTacBangGia.CPNHUA;
           
            table.Rows.Add(myDataRow);
            //conn.Close();
            return table;
        }
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shs"></param>
      
        public  void INBANGIA(string _shs, int lan, bool view) {
            //try
            //{
                TanHoaDataContext db = new TanHoaDataContext();
                DataSet ds = new DataSet();
                db.Connection.Open();

                string sql = "SELECT distinct * FROM BGDC_CHITIETBG  WHERE SHS='" + _shs + "' AND LAN='" + lan+"'";

                SqlDataAdapter dond = new SqlDataAdapter(sql, db.Connection.ConnectionString);
                dond.Fill(ds, "BG_CHITIETBG");


                string user = "SELECT distinct * FROM BGDC_TAILAPMATDUONG  WHERE SHS='" + _shs + "' AND LAN='" + lan + "'";
                SqlDataAdapter ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
                ct.Fill(ds, "BG_TAILAPMATDUONG");

                user = "SELECT distinct * FROM W_HS ";
                ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
                ct.Fill(ds, "BG_HESOBANGGIA");


                user = "SELECT distinct  * FROM BG_THONGTINKHACHANG  WHERE SHS='" + _shs + "'";
                ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
                ct.Fill(ds, "BG_THONGTINKHACHANG");

                user = "SELECT distinct * FROM BGDC_SUMTAILAPMATDUONG  WHERE SHS='" + _shs + "' AND LAN='" + lan + "'";
                ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
                ct.Fill(ds, "BG_SUMTAILAPMATDUONG");

                user = "SELECT  distinct * FROM BG_REPORT ";
                ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
                ct.Fill(ds, "BG_REPORT");

                user = "SELECT distinct * FROM USERS  WHERE USERNAME='" + DAL.C_USERS._userName + "'";
                ct = new SqlDataAdapter(user, db.Connection.ConnectionString);
                ct.Fill(ds, "USERS");

                bool phiC3 = false;
                if (checkCoTinhPhiCaBa.Checked)
                    phiC3 = true;
                else phiC3 = false;

                bool phiQL = false;
                if (checkPhiQuanLy.Checked)
                    phiQL = true;
                else phiQL = false;

                bool phiGS = false;

                if (checkPhiGiamSat.Checked)
                    phiGS = true;
                else phiGS = false;

                //ds.Tables.Add(TongKetChiPhi(_shs, phiC3, phiGS, phiQL));
                ds.Tables.Add(TongKetChiPhi(_shs, phiC3, phiGS, phiQL));

                double TongThanhTien = total;
                try
                {
                    // khong tlmd
                    TongThanhTien=total + double.Parse(ds.Tables["BG_SUMTAILAPMATDUONG"].Rows[0][1].ToString());

                }
                catch (Exception)
                {
                }

                CrystalReportViewer r = new CrystalReportViewer();
                ReportDocument rp = new ReportDocument();
                if ((vatTuXDCBKhachHangCap.Checked && khachangtuTaiLap == false) )
                {

                    rp = new rptBangGiaTuTaiLap();
                }
                else if (vatTuXDCBKhachHangCap.Checked && khachangtuTaiLap == true)
                {
                    rp = new rptBangGiaTuTaiLap00();
                }
                else if(checkKhongTLMD.Checked)
                    rp = new rptBangGia_khtl();
                else
                {
                    rp = new rptBangGia();
                }
                //rp = new rptBangGia();
                //rp.Subreports["Subreport1"].SetParameterValue("Tienchu", Utilities.Doctien.ReadMoney(String.Format("{0:0}", TongThanhTien)));
                rp.SetDataSource(ds);

                rp.SetParameterValue("subTienchu", Utilities.Doctien.ReadMoney(String.Format("{0:0}", Math.Round(Math.Abs(kqtonggiatri)))));
                rp.SetParameterValue("gan", _tongketgan);
                rp.SetParameterValue("nhua", _tongketnhua);
                rp.SetParameterValue("solan", lan);

                
                rp.SetParameterValue("gantlkKhauTru", gantlcu);
                rp.SetParameterValue("gantlkConLai", kqgantlk);
                rp.SetParameterValue("tlmdkhautru", tlmdcu);
                if (kqtlmd == 0.0)
                {
                    kqtlmd = 0.0000000001;
                }
               // rp.Subreports["subReport"].SetParameterValue("tlmdcolai", kqtlmd);
                rp.SetParameterValue("tlmdcolai", kqtlmd);
                rp.SetParameterValue("sotienkhautrutruoc", tongiatricu);
                
                if (kqtonggiatri == 0)
                    kqtonggiatri = 0.0000000001;

                rp.SetParameterValue("ketquagiatri", kqtonggiatri);

                if (checkKHDT.Checked)
                    rp.SetParameterValue("khachhangdautu", "(KHÁCH HÀNG ĐẦU TƯ)");
                else
                    rp.SetParameterValue("khachhangdautu", "");
                //DAL.C_CongTacBangGia.CapNhatHoanTatTK(_shs);
                rp.SetParameterValue("ngaytinhbanggiacu", NgayLapBGCu.Text.Trim());
                if (view)
                {
                    rpt_ViewBangGiaDC bc = new rpt_ViewBangGiaDC(rp,_shs, solandieuchinh);
                    bc.ShowDialog();
                }
                else
                {
                    rpt_Main bc = new rpt_Main(rp);
                    bc.ShowDialog();
                }
                // crystalReportViewer1.ReportSource = rp;
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Loi Tao Bang Gia " + ex.Message);
            //}
            
        }
      
        //public void updateSDV()
        //{
        //    try
        //    {
        //        DAL.C_ToThietKe.updateSoDoVien(_shs, this.txtSoDoVien.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Cap Nhat SDV LOI" + ex.Message);
        //    }
        //}
        private void btTinhBangGia_Click(object sender, EventArgs e)
        {
          
           if (!"".Equals(_shs))
            {
                //try
                //{
                    if ("0".Equals(txtKLCat.Text) || "0.00".Equals(txtKLCat.Text))
                    {
                        MessageBox.Show(this, "Cần Nhập Thông Tin Bảng Giá !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                       
                        tinhlai = false;
                        if(checkKhongTLMD.Checked==false)
                            InsertBG_KICHTHUOCPHUIDAO();
                        InsertCONGTACBANGGIA();
                        InsertKHOILUONGXDCB();
                        INBANGIA(_shs, solandieuchinh, false);
                        solandieuchinh++;

                        //if (banggiadaco == true)
                        //{
                        //    tabControl2.SelectedTabIndex = 2;
                        //    if (radioGhiDe.Checked && MessageBox.Show(this, "Bảng Giá Đã Chạy Lần Đầu Tiên, Có Muốn Ghi Đè Không ?", "..: Thông Báo :..", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    {
                        //        DAL.C_CongTacBangGia.deleteData(_shs);
                        //        logText = DAL.C_USERS._userName + " đã nghi đèn bảng giá ngày " + DateTime.Now + ".: " + cbLyDoTinhLaiBG.Text;
                        //        InsertBG_KICHTHUOCPHUIDAO();
                        //        InsertCONGTACBANGGIA();
                        //        InsertKHOILUONGXDCB();
                        //        INBANGIA(_shs);
                        //        DAL.C_CongTacBangGia.updateghide(_shs, logText);
                        //    }
                        //    else if (radioNone.Checked)
                        //    {
                        //        INBANGIA(_shs);
                        //    }
                        //    else if (radioGhiMoi.Checked)
                        //    {
                        //        //InsertBG_KICHTHUOCPHUIDAO();
                        //        //InsertCONGTACBANGGIA();
                        //        //InsertKHOILUONGXDCB();
                        //        //INBANGIA(_shs);
                        //        //banggiadaco = true;
                        //        //radioGhiDe.Checked = true;
                        //        MessageBox.Show(this, "Bảng Giá Đã Chạy Rồi, Chỉ Ghi Đè !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        radioGhiDe.Checked = true;
                        //        banggiadaco = true;
                        //    }
                        //}
                        //else
                        //{
                        //    InsertBG_KICHTHUOCPHUIDAO();
                        //    InsertCONGTACBANGGIA();
                        //    InsertKHOILUONGXDCB();
                        //    INBANGIA(_shs);
                        //    banggiadaco = true;
                        //    radioGhiDe.Checked = true;
                        //}
                       
                        LoadTabCacLanDieuChinh();
                    }
                //}
                //catch (Exception ex)
                //{
                //    log.Error("Loi In Bang Gia " + ex.Message);
                //    MessageBox.Show(this, "Tạo Mới Bảng Giá Thất Bại. ", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
               
                            
            }
            else
            {
                MessageBox.Show(this, "Nhập Số Hồ Sơ Tính Dự Toán.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtSHS.Focus();
                this.txtSHS.BackColor = Color.Red;

            }
        }
        
        private void GridCacCongTac_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GridCacCongTac.CurrentCell.OwningColumn.Name == "contac_loaisd")
                {
                    if (double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value + "") > 10)
                    {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value = String.Format("{0:0,0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value + "")); ;
                    }
                    else {
                        this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value = String.Format("{0:0.00}", double.Parse(this.GridCacCongTac.Rows[e.RowIndex].Cells["congtac_khoiluong"].Value + "")); ;
                    }
                }
            }
            catch (Exception)
            {}
            
        }

        private void GridCacCongTac_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            try
            {
               
                    if (double.Parse(this.GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value + "") > 10)
                    {
                        this.GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value = String.Format("{0:0,0.00}", double.Parse(this.GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value + "")); ;
                    }
                    else
                    {
                        this.GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value = String.Format("{0:0.00}", double.Parse(this.GridCacCongTac.Rows[GridCacCongTac.CurrentRow.Index].Cells["congtac_khoiluong"].Value + "")); ;
                    }
              
            }
            catch (Exception)
            { }
        }
        
        private void GridCacCongTac_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }

        private void btXemNhatKy_Click(object sender, EventArgs e)
        {
            //if (!"".Equals(_shs)) {
            //    frm_LogBG form = new frm_LogBG(_shs);
            //    form.ShowDialog();
            //}
        }

        private void btLapHoSoMoi_Click(object sender, EventArgs e)
        {
            refresh();
            this.txtSHS.Focus();
            this.tabControl2.SelectedTabIndex = 0;
            visibleTab(false, false, true, false, false);
            view = true;

        }

        private void GridPhuiDao_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void txtSoDoVien_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                DAL.C_CongTacBangGia.updateSDVKS(_shs, this.txtSoDoVien.SelectedValue+"");
            }
            catch (Exception ex)
            {
                log.Error("Cap Nhat So Do Vien loi" + ex.Message);               
            }
        }

        private void GridCacLanDC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GridCacLanDC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                else if (GridCacLanDC.CurrentCell.OwningColumn.Name == "gr_Load")
                {
                    string landc= GridCacLanDC.Rows[e.RowIndex].Cells["dieuchinh_lan"].Value + "";
                    if ("0".Equals(landc.Trim()))
                    {
                        LoadDuLieuBangGia(_shs);
                        MessageBox.Show(this, "Lấy Dữ Liệu Bảng Giá Thành Công .", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else {
                        LoadDuLieuBangGia(_shs, int.Parse(landc));
                        MessageBox.Show(this, "Lấy Dữ Liệu Bảng Giá Thành Công .", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }                
            }
            catch (Exception)
            {
            }
        }

        public void LoadNewCacCongTac_Click()
        {
            visibleTab(false, false, false, true, false);

            try
            {
                string CNHUA = this.txtChuViCatNhua.Text;
                string BOCNHUA = this.txtKhoiLuongCatNhua.Text;
                string CMBTXM = this.txtChuViBT.Text;
                string BMBTXM = this.txtKhoiLuongBT.Text;
                string DP4 = this.txtDatCap4.Text;
                string DP3 = this.txtDatCap3.Text;
                string XUCDAT = this.txtXucDatThua.Text;
                string CAT = this.txtKLCat.Text;
                string DA04 = this.txtKLDa.Text;
                string selectin = "";
                // GridCacCongTac   
                if (view && loadcongtac)
                {
                    loadcongtac = false;
                    if (!"".Equals(CNHUA) && double.Parse(CNHUA) > 0.0)
                    {
                        selectin += "'CNHUA',";
                    }
                    if (!"".Equals(BOCNHUA) && double.Parse(BOCNHUA) > 0.0)
                    {
                        selectin += "'BNHUA',";
                    }
                    if (!"".Equals(CMBTXM) && double.Parse(CMBTXM) > 0.0)
                    {
                        selectin += "'CMBTXM',";
                    }
                    if (!"".Equals(BMBTXM) && double.Parse(BMBTXM) > 0.0)
                    {
                        selectin += "'BMBTXM',";
                    }
                    if (!"".Equals(DP4) && double.Parse(DP4) > 0.0)
                    {
                        selectin += "'DP4',";
                    }
                    if (!"".Equals(DP3) && double.Parse(DP3) > 0.0)
                    {
                        selectin += "'DP3',";
                    }
                    if (!"".Equals(XUCDAT) && double.Parse(XUCDAT) > 0.0)
                    {
                        selectin += "'XUCDAT',";
                    }
                    if (!"".Equals(CAT) && double.Parse(CAT) > 0.0)
                    {
                        selectin += "'CAT',";
                    }
                    if (!"".Equals(DA04) && double.Parse(DA04) > 0.0)
                    {
                        selectin += "'DA04',";
                    }

                    GridCacCongTac.DataSource = DAL.C_DanhMucVatTu.getDMVT(selectin.Remove(selectin.Length - 1, 1));
                    for (int i = 0; i < GridCacCongTac.Rows.Count; i++)
                    {

                        if ("CNHUA".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CNHUA) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CNHUA);
                        }
                        if ("BNHUA".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(BOCNHUA) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(BOCNHUA);
                        }
                        if ("CMBTXM".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CMBTXM) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CMBTXM);
                        }
                        if ("BMBTXM".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(BMBTXM) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(BMBTXM);
                        }
                        if ("DP4".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DP4) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DP4);
                        }
                        if ("DP3".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DP3) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DP3);
                        }
                        if ("XUCDAT".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(XUCDAT) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(XUCDAT);
                        }
                        if ("CAT".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CAT) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CAT);
                        }
                        if ("DA04".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DA04) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DA04);
                        }
                        GridCacCongTac.Rows[i].Cells["contac_loaisd"].Value = "CM";
                        DANHMUCVATTU dmvt = DAL.C_DanhMucVatTu.finbyMaHieu(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value + "");
                        if (dmvt != null && dmvt.BOVT == true)
                        {
                            DataTable dongiavt = DAL.C_DonGiaVatTu.getDonGiaBoVT(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                GridCacCongTac.Rows[i].Cells["congtac_VL"].Value = dongiavt.Rows[0][0].ToString();
                                GridCacCongTac.Rows[i].Cells["congtac_NC"].Value = dongiavt.Rows[0][1].ToString();
                                GridCacCongTac.Rows[i].Cells["congtac_MTC"].Value = dongiavt.Rows[0][2].ToString();
                            }
                        }
                        else if (dmvt != null)
                        {
                            DONGIAVATTU dongiavt = DAL.C_DonGiaVatTu.getDonGia(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                GridCacCongTac.Rows[i].Cells["congtac_VL"].Value = dongiavt.DGVATLIEU;
                                GridCacCongTac.Rows[i].Cells["congtac_NC"].Value = dongiavt.DGNHANCONG;
                                GridCacCongTac.Rows[i].Cells["congtac_MTC"].Value = dongiavt.DGMAYTHICONG;
                            }
                        }
                    }
                    //Utilities.DataGridV.formatRows(GridCacCongTac);

                }
            }
            catch (Exception ex)
            {
                log.Error("Loi cap Nhat Don Gia" + ex.Message);
            }
        }

        public void LoadNewCacCongTac_Load(string newselect)
        {
            visibleTab(false, false, false, true, false);
            this.tabControl2.SelectedTabIndex = 2;
            try
            {
                string CNHUA = this.txtChuViCatNhua.Text;
                string BOCNHUA = this.txtKhoiLuongCatNhua.Text;
                string CMBTXM = this.txtChuViBT.Text;
                string BMBTXM = this.txtKhoiLuongBT.Text;
                string DP4 = this.txtDatCap4.Text;
                string DP3 = this.txtDatCap3.Text;
                string XUCDAT = this.txtXucDatThua.Text;
                string CAT = this.txtKLCat.Text;
                string DA04 = this.txtKLDa.Text;
                string selectin = "";
                // GridCacCongTac   
                    if (!"".Equals(CNHUA) && double.Parse(CNHUA) > 0.0)
                    {
                        selectin += "'CNHUA',";
                    }
                    if (!"".Equals(BOCNHUA) && double.Parse(BOCNHUA) > 0.0)
                    {
                        selectin += "'BNHUA',";
                    }
                    if (!"".Equals(CMBTXM) && double.Parse(CMBTXM) > 0.0)
                    {
                        selectin += "'CMBTXM',";
                    }
                    if (!"".Equals(BMBTXM) && double.Parse(BMBTXM) > 0.0)
                    {
                        selectin += "'BMBTXM',";
                    }
                    if (!"".Equals(DP4) && double.Parse(DP4) > 0.0)
                    {
                        selectin += "'DP4',";
                    }
                    if (!"".Equals(DP3) && double.Parse(DP3) > 0.0)
                    {
                        selectin += "'DP3',";
                    }
                    if (!"".Equals(XUCDAT) && double.Parse(XUCDAT) > 0.0)
                    {
                        selectin += "'XUCDAT',";
                    }
                    if (!"".Equals(CAT) && double.Parse(CAT) > 0.0)
                    {
                        selectin += "'CAT',";
                    }
                    if (!"".Equals(DA04) && double.Parse(DA04) > 0.0)
                    {
                        selectin += "'DA04',";
                    }

                    selectin = selectin + newselect;
                    GridCacCongTac.DataSource = DAL.C_DanhMucVatTu.getDMVT(selectin.Remove(selectin.Length - 1, 1));

                    for (int i = 0; i < GridCacCongTac.Rows.Count; i++)
                    {

                        if ("CNHUA".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CNHUA) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CNHUA);
                        }
                        if ("BNHUA".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(BOCNHUA) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(BOCNHUA);
                        }
                        if ("CMBTXM".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CMBTXM) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CMBTXM);
                        }
                        if ("BMBTXM".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(BMBTXM) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(BMBTXM);
                        }
                        if ("DP4".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DP4) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DP4);
                        }
                        if ("DP3".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DP3) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DP3);
                        }
                        if ("XUCDAT".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(XUCDAT) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(XUCDAT);
                        }
                        if ("CAT".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(CAT) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(CAT);
                        }
                        if ("DA04".Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value) && double.Parse(DA04) > 0.0)
                        {
                            GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = double.Parse(DA04);
                        }
                        GridCacCongTac.Rows[i].Cells["contac_loaisd"].Value = "CM";
                        DANHMUCVATTU dmvt = DAL.C_DanhMucVatTu.finbyMaHieu(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value + "");
                        if (dmvt != null && dmvt.BOVT == true)
                        {
                            DataTable dongiavt = DAL.C_DonGiaVatTu.getDonGiaBoVT(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                GridCacCongTac.Rows[i].Cells["congtac_VL"].Value = dongiavt.Rows[0][0].ToString();
                                GridCacCongTac.Rows[i].Cells["congtac_NC"].Value = dongiavt.Rows[0][1].ToString();
                                GridCacCongTac.Rows[i].Cells["congtac_MTC"].Value = dongiavt.Rows[0][2].ToString();
                            }
                        }
                        else if (dmvt != null)
                        {
                            DONGIAVATTU dongiavt = DAL.C_DonGiaVatTu.getDonGia(dmvt.MAHIEU);
                            if (dongiavt != null)
                            {
                                GridCacCongTac.Rows[i].Cells["congtac_VL"].Value = dongiavt.DGVATLIEU;
                                GridCacCongTac.Rows[i].Cells["congtac_NC"].Value = dongiavt.DGNHANCONG;
                                GridCacCongTac.Rows[i].Cells["congtac_MTC"].Value = dongiavt.DGMAYTHICONG;
                            }
                        }
                    }
                    //Utilities.DataGridV.formatRows(GridCacCongTac);
            
            }
            catch (Exception ex)
            {
                log.Error("Loi cap Nhat Don Gia" + ex.Message);
            }
        }
             
       
        private void btBoVT_Click(object sender, EventArgs e)
        {
            TanHoaWater.View.Users.TinhDuToan.frmBoVT bovt = new TanHoaWater.View.Users.TinhDuToan.frmBoVT();
            if (bovt.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show(this, DAL.C_BoVatTuTaoSan.mabovt);
                List<CHITIETBOVATTAOSAN> list = DAL.C_BoVatTuTaoSan.getChiTietVT(DAL.C_BoVatTuTaoSan.mabovt, true);
                string selectin = "";
                foreach (var item in list)
                {
                    selectin += "'" + item.MAHIEU + "',";
                }
                LoadNewCacCongTac_Load(selectin);
                try
                {
                    foreach (var item in list)
                    {
                        for (int i = 0; i < GridCacCongTac.Rows.Count; i++)
                        {

                            if (item.MAHIEU.Equals(GridCacCongTac.Rows[i].Cells["congtac_mahieu"].Value))
                            {
                                GridCacCongTac.Rows[i].Cells["congtac_khoiluong"].Value = item.KHOILUONG;
                                GridCacCongTac.Rows[i].Cells["contac_loaisd"].Value = item.LOAISN;
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void GridCacLanDC_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btXemBangGia_Click(object sender, EventArgs e)
        {
            if (!"".Equals(_shs))
            {
                string logText = "";
                try
                {
                    if ("0".Equals(txtKLCat.Text) || "0.00".Equals(txtKLCat.Text))
                    {
                        MessageBox.Show(this, "Cần Nhập Thông Tin Bảng Giá !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        tinhlai = false;
                        InsertBG_KICHTHUOCPHUIDAO();
                        InsertCONGTACBANGGIA();
                        InsertKHOILUONGXDCB();
                        INBANGIA(_shs, solandieuchinh,true);
                        LoadTabCacLanDieuChinh();
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Loi In Bang Gia " + ex.Message);
                    MessageBox.Show(this, "Tạo Mới Bảng Giá Thất Bại. ", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show(this, "Nhập Số Hồ Sơ Tính Dự Toán.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtSHS.Focus();
                this.txtSHS.BackColor = Color.Red;

            }
        }



        

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!"".Equals(_shs))
            {
                frm_VatTuDieuChinh vt = new frm_VatTuDieuChinh(_shs, solandieuchinh-1);
                vt.ShowDialog();
            }
            else {
                MessageBox.Show(this, "Nhập Số Hồ Sơ Lấy Danh Mục Vật Tư Điều Chỉnh.", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

    }
}