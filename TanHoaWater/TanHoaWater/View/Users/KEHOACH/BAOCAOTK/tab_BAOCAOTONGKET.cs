﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using TanHoaWater.View.Users.KEHOACH.BAOCAOTK;
using TanHoaWater.View.Users.Report;
using log4net;
using TanHoaWater.Database;

namespace TanHoaWater.View.Users.KEHOACH
{
    public partial class tab_BAOCAOTONGKET : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(tab_BAOCAOTONGKET).Name);
        public tab_BAOCAOTONGKET()
        {
            InitializeComponent();
            try
            {
                cbLoaiBangKe.DataSource = DAL.C_KH_DonViTC.getLoaiBangKe();
                cbLoaiBangKe.DisplayMember = "TENBANGKE";
                cbLoaiBangKe.ValueMember = "TENBANGKE";

                cbDonViThiCong.DataSource = DAL.C_KH_DonViTC.getDonViThiCong();
                cbDonViThiCong.DisplayMember = "TENCONGTY";
                cbDonViThiCong.ValueMember = "ID";

                this.cbLoaiBN.DataSource = DAL.C_LoaiNhanDon.getList();
                this.cbLoaiBN.ValueMember = "LOAIDON";
                this.cbLoaiBN.DisplayMember = "TENLOAI";
                this.cbLoaiBN.SelectedIndex = 2;

                this.cbPhuong.DataSource = DAL.C_Phuong.getListPhuong();
                this.cbPhuong.DisplayMember = "Display";
                this.cbPhuong.ValueMember = "Value";

                //this.comboBoxEx1.DataSource = DAL.C_LoaiHoSo.getListCombobox();
                //this.comboBoxEx1.DisplayMember = "Display";
                //this.comboBoxEx1.ValueMember = "Value";    

                cbQuan.DataSource = DAL.C_Quan.getList();
                cbQuan.ValueMember = "MAQUAN";
                cbQuan.DisplayMember = "TENQUAN";

                cbPhuongQuan.DataSource = DAL.C_Quan.getList();
                cbPhuongQuan.ValueMember = "MAQUAN";
                cbPhuongQuan.DisplayMember = "TENQUAN";


                this.tnLoaiCT.DataSource = DAL.C_LoaiNhanDon.getList();
                this.tnLoaiCT.ValueMember = "LOAIDON";
                this.tnLoaiCT.DisplayMember = "TENLOAI";
                this.tnLoaiCT.SelectedIndex = 2;


                tn_Quan.DataSource = DAL.C_Quan.getList();
                tn_Quan.ValueMember = "MAQUAN";
                tn_Quan.DisplayMember = "TENQUAN";

                tnQuan2.DataSource = DAL.C_Quan.getList();
                tnQuan2.ValueMember = "MAQUAN";
                tnQuan2.DisplayMember = "TENQUAN";


                this.tnPhuong.DataSource = DAL.C_Phuong.getListPhuong();
                this.tnPhuong.DisplayMember = "Display";
                this.tnPhuong.ValueMember = "Value"; 
              

                ocQuan.DataSource = DAL.C_Quan.getList();
                ocQuan.ValueMember = "MAQUAN";
                ocQuan.DisplayMember = "TENQUAN";



                this.ocPhuong.DataSource = DAL.C_Phuong.getListPhuong();
                this.ocPhuong.DisplayMember = "Display";
                this.ocPhuong.ValueMember = "Value";

                ocQuanPhuong.DataSource = DAL.C_Quan.getList();
                ocQuanPhuong.ValueMember = "MAQUAN";
                ocQuanPhuong.DisplayMember = "TENQUAN";


                this.cHT.DataSource = DAL.C_LoaiNhanDon.getList();
                this.cHT.ValueMember = "LOAIDON";
                this.cHT.DisplayMember = "TENLOAI";
                this.cHT.SelectedIndex = 2;
  
            }
            catch (Exception ex)
            {
                log.Error("Load Bien Nhan Don Loi " + ex.ToString());
            }

        }
        //int bcKinhPhi = 1;
        private void btViewDS_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ReportDocument rp = new rpt_BCKINHPHI();
                if (tabItem1.IsSelected == true)
                {
                    ds = DAL.C_KH_BAOCAO.BC_KINHPHITHICONG(this.cbLoaiBangKe.Text, 1, Utilities.DateToString.NgayVN(dateTuNgay), Utilities.DateToString.NgayVN(dateDenNgay), this.cbDonViThiCong.SelectedValue + "", Utilities.DateToString.NgayVN(dateDVTuNgay), Utilities.DateToString.NgayVN(dateDVDenNgay));
                    //ds = DAL.C_KH_BAOCAO.BC_TinhHinhKSTK(null, Utilities.DateToString.NgayVN(theodoi_tungay), Utilities.DateToString.NgayVN(theodoi_denngay), this.theodoi_SDV.SelectedValue.ToString(), DAL.C_USERS._userName);
                }
                else if (tabItem2.IsSelected == true)
                {
                    ds = DAL.C_KH_BAOCAO.BC_KINHPHITHICONG(this.cbLoaiBangKe.Text, 2, Utilities.DateToString.NgayVN(dateTuNgay), Utilities.DateToString.NgayVN(dateDenNgay), this.cbDonViThiCong.SelectedValue + "", Utilities.DateToString.NgayVN(dateDVTuNgay), Utilities.DateToString.NgayVN(dateDVDenNgay));
                }
                else if (tabItem3.IsSelected == true)
                {
                    ds = DAL.C_KH_BAOCAO.BC_KINHPHITHICONG(this.cbLoaiBangKe.Text, 3, Utilities.DateToString.NgayVN(dateTuNgay), Utilities.DateToString.NgayVN(dateDenNgay), this.cbDonViThiCong.SelectedValue + "", Utilities.DateToString.NgayVN(dateDVTuNgay), Utilities.DateToString.NgayVN(dateDVDenNgay));
                }
                rp.SetDataSource(ds);
                rpt_Main rpt = new rpt_Main(rp);
                rpt.ShowDialog();
            }
            catch (Exception ex)
            {

                log.Error(" Xem Bao Cao Tong Ket Kinh Phi Loi " + ex.Message);
             
            }
            
        }

        private void cbPhuong_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                List<PHUONG> phuong = DAL.C_Phuong.ListPhuongByTenPhuong(this.cbPhuong.Text);
                if (phuong.Count > 0)
                {
                    PHUONG p = phuong[0];
                    cbPhuongQuan.Text = p.QUAN.TENQUAN;

                }
            }
            catch (Exception)
            {

            }
        }

        private void btBaoCaoCongTac_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DataSet ds = new DataSet();
            //    ReportDocument rp = new rpt_BCTONGKET();
            //    ds = DAL.C_KH_BAOCAO.BC_TONGKET();
            //    rp.SetDataSource(ds);
            //    rpt_Main rpt = new rpt_Main(rp);
            //    rpt.ShowDialog();


                try
                {
                    int type = 1;
                    DataSet ds = new DataSet();  ReportDocument rp = new ReportDocument();
                    if ("31".Equals(this.cbQuan.SelectedValue + "") || "31".Equals(this.cbPhuongQuan.SelectedValue + ""))
                        rp = new rpt_BCTONGKET_QTP();
                    else
                        rp = new Rpt_BCTONGKET_QTB();

                    //if (cbLoaiBN.SelectedValue.ToString() == "BT")
                    //{
                    //    rp = new rpt_BCTONGKET_QTB_BT();
                    //}

                    if (tabItem7.IsSelected == true)
                    {
                        ds = DAL.C_KH_BAOCAO.BC_TONGKET(cbLoaiBN.SelectedValue + "", 1, "", this.cbQuan.SelectedValue + "", Utilities.DateToString.NgayVN(congtaTuNgay), Utilities.DateToString.NgayVN(congtacDenNgay));
                    }
                    else if (tabItem8.IsSelected == true)
                    {
                        type = 2;
                        ds = DAL.C_KH_BAOCAO.BC_TONGKET(cbLoaiBN.SelectedValue + "", 2, this.cbPhuong.SelectedValue + "", this.cbPhuongQuan.SelectedValue + "", Utilities.DateToString.NgayVN(congtaTuNgay), Utilities.DateToString.NgayVN(congtacDenNgay));
                       
                    }
                   
                    rp.SetDataSource(ds);
                    rp.SetParameterValue("TUNGAY",Utilities.DateToString.NgayVN(congtaTuNgay));
                    rp.SetParameterValue("DENNGAY",Utilities.DateToString.NgayVN(congtacDenNgay));
                    rp.SetParameterValue("type", type);
                    rpt_Main rpt = new rpt_Main(rp);
                    rpt.ShowDialog();
                }
                catch (Exception ex)
                {
                    log.Error(" Xem Bao Cao Tong Ket Kinh Phi Loi " + ex.Message);

                }

            //}
            //catch (Exception ex)
            //{

            //    log.Error(" Xem Bao Cao Tong Ket Kinh Phi Loi " + ex.Message);

            //}
        }

        private void btTroNgai_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DataSet ds = new DataSet();
            //    ReportDocument rp = new rpt_BCTONGKET();
            //    ds = DAL.C_KH_BAOCAO.BC_TONGKET();
            //    rp.SetDataSource(ds);
            //    rpt_Main rpt = new rpt_Main(rp);
            //    rpt.ShowDialog();


            try
            {
                int type = 1;
                DataSet ds = new DataSet();

                ReportDocument rp = new Rpt_BCTONGKET_QTB_TN();
                if (tabItem9.IsSelected == true)
                {
                    ds = DAL.C_KH_BAOCAO.BC_TONGKET_TH(tnLoaiCT.SelectedValue + "", 1, "", this.tn_Quan.SelectedValue + "", Utilities.DateToString.NgayVN(trongaiTuNgay), Utilities.DateToString.NgayVN(trongaidenngay));
                }
                else if (tabItem10.IsSelected == true)
                {
                    type = 2;
                    ds = DAL.C_KH_BAOCAO.BC_TONGKET_TH(tnLoaiCT.SelectedValue + "", 2, this.tnPhuong.SelectedValue + "", this.tnQuan2.SelectedValue + "", Utilities.DateToString.NgayVN(trongaiTuNgay), Utilities.DateToString.NgayVN(trongaidenngay));

                }

                rp.SetDataSource(ds);
                rp.SetParameterValue("TUNGAY", Utilities.DateToString.NgayVN(trongaiTuNgay));
                rp.SetParameterValue("DENNGAY", Utilities.DateToString.NgayVN(trongaidenngay));
                rp.SetParameterValue("type", type);
                rpt_Main rpt = new rpt_Main(rp);
                rpt.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Error(" Xem Bao Cao Tong Ket Kinh Phi Loi " + ex.Message);

            }
        }

        private void tnPhuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<PHUONG> phuong = DAL.C_Phuong.ListPhuongByTenPhuong(this.tnPhuong.Text);
                if (phuong.Count > 0)
                {
                    PHUONG p = phuong[0];
                    tnQuan2.Text = p.QUAN.TENQUAN;

                }
            }
            catch (Exception)
            {

            }
        }

        private void ocView_Click(object sender, EventArgs e)
        {

            try
            {
                int type = 4;
                DataSet ds = new DataSet(); ReportDocument rp = new ReportDocument();
                if ("31".Equals(this.ocQuan.SelectedValue + "") || "31".Equals(this.ocQuanPhuong.SelectedValue + ""))
                    rp = new rpt_BCTONGKET_QTP();
                else
                    rp = new Rpt_BCTONGKET_QTB();

                if (tabItem12.IsSelected == true)
                {
                    ds = DAL.C_KH_BAOCAO.BC_TONGKET_OC("GM", type, "", this.ocQuan.SelectedValue + "", Utilities.DateToString.NgayVN(ocTuNgay), Utilities.DateToString.NgayVN(ocDenNgay));
                }
                else if (tabItem11.IsSelected == true)
                {
                    type = 3;
                    ds = DAL.C_KH_BAOCAO.BC_TONGKET_OC("GM", type, this.ocPhuong.SelectedValue + "", this.ocQuanPhuong.SelectedValue + "", Utilities.DateToString.NgayVN(ocTuNgay), Utilities.DateToString.NgayVN(ocDenNgay));

                }

                rp.SetDataSource(ds);
                rp.SetParameterValue("TUNGAY", Utilities.DateToString.NgayVN(ocTuNgay));
                rp.SetParameterValue("DENNGAY", Utilities.DateToString.NgayVN(ocDenNgay));
                rp.SetParameterValue("type", type);
                rpt_Main rpt = new rpt_Main(rp);
                rpt.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Error(" Xem Bao Cao Tong Ket Kinh Phi Loi " + ex.Message);

            }

        }

      

        private void ocPhuong_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                List<PHUONG> phuong = DAL.C_Phuong.ListPhuongByTenPhuong(this.ocPhuong.Text);
                if (phuong.Count > 0)
                {
                    PHUONG p = phuong[0];
                    ocQuanPhuong.Text = p.QUAN.TENQUAN;

                }
            }
            catch (Exception)
            {

            }
        }

        private void btChuHoanTat_Click(object sender, EventArgs e)
        {
            try
            {
                int type = 1;
                DataSet ds = new DataSet();

                ReportDocument rp = new rpt_DSCHUAHOANTAT();

                ds = DAL.C_KH_BAOCAO.BC_TONGKET_CHUAHT(cHT.SelectedValue + "", Utilities.DateToString.NgayVN(cTuNgay), Utilities.DateToString.NgayVN(cDenNgay));
                 

                rp.SetDataSource(ds);
                rp.SetParameterValue("TUNGAY", Utilities.DateToString.NgayVN(cTuNgay));
                rp.SetParameterValue("DENNGAY", Utilities.DateToString.NgayVN(cDenNgay));
                rpt_Main rpt = new rpt_Main(rp);
                rpt.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Error(" Xem Bao Cao Tong Ket Kinh Phi Loi " + ex.Message);

            }
        }

      
    }
}
