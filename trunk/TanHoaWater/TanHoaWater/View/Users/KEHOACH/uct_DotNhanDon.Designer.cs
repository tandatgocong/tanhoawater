﻿namespace TanHoaWater.View.Users.HSKHACHHANG
{
    partial class uct_DOTNHANDON
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.reflectionLabel1 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbBOPHAN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.checkCD = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.print = new System.Windows.Forms.PictureBox();
            this.chyenTTK = new DevComponents.DotNetBar.ButtonX();
            this.lbSoKHNhanDon = new System.Windows.Forms.Label();
            this.detail = new System.Windows.Forms.DataGridView();
            this.SOHOSO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HOTEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIACHI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGAY_NHAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loiDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainGrid = new System.Windows.Forms.DataGridView();
            this.DOTNHAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGAYNHAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOAIDON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtsoDot = new System.Windows.Forms.MaskedTextBox();
            this.createDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLoaiHS = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.refresh = new DevComponents.DotNetBar.ButtonX();
            this.addNewDot = new DevComponents.DotNetBar.ButtonX();
            this.SearchDot = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.print)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.createDate)).BeginInit();
            this.SuspendLayout();
            // 
            // reflectionLabel1
            // 
            this.reflectionLabel1.Location = new System.Drawing.Point(243, 0);
            this.reflectionLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.reflectionLabel1.Name = "reflectionLabel1";
            this.reflectionLabel1.Size = new System.Drawing.Size(691, 65);
            this.reflectionLabel1.TabIndex = 0;
            this.reflectionLabel1.Text = "<font color=\"#ED1C24\"><b><font size=\"+6\">DANH SÁCH CÁC ĐỢT NHẬN ĐƠN KHÁCH HÀNG</f" +
    "ont></b></font>";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbBOPHAN);
            this.groupBox2.Controls.Add(this.checkCD);
            this.groupBox2.Controls.Add(this.print);
            this.groupBox2.Controls.Add(this.chyenTTK);
            this.groupBox2.Controls.Add(this.lbSoKHNhanDon);
            this.groupBox2.Controls.Add(this.detail);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(611, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 450);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách Khách Hàng của Đợt nhận đơn ";
            // 
            // cbBOPHAN
            // 
            this.cbBOPHAN.DisplayMember = "Text";
            this.cbBOPHAN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBOPHAN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBOPHAN.DropDownWidth = 250;
            this.cbBOPHAN.FormattingEnabled = true;
            this.cbBOPHAN.ItemHeight = 18;
            this.cbBOPHAN.Location = new System.Drawing.Point(101, 22);
            this.cbBOPHAN.Name = "cbBOPHAN";
            this.cbBOPHAN.Size = new System.Drawing.Size(137, 24);
            this.cbBOPHAN.TabIndex = 25;
            this.cbBOPHAN.Visible = false;
            // 
            // checkCD
            // 
            this.checkCD.Location = new System.Drawing.Point(6, 22);
            this.checkCD.Name = "checkCD";
            this.checkCD.Size = new System.Drawing.Size(101, 23);
            this.checkCD.TabIndex = 24;
            this.checkCD.Text = "Chuyển Đơn";
            this.checkCD.Visible = false;
            this.checkCD.CheckedChanged += new System.EventHandler(this.checkCD_CheckedChanged);
            // 
            // print
            // 
            this.print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.print.Image = global::TanHoaWater.Properties.Resources.print1;
            this.print.Location = new System.Drawing.Point(324, 22);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(27, 22);
            this.print.TabIndex = 22;
            this.print.TabStop = false;
            this.print.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // chyenTTK
            // 
            this.chyenTTK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.chyenTTK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.chyenTTK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chyenTTK.Image = global::TanHoaWater.Properties.Resources.login;
            this.chyenTTK.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.chyenTTK.Location = new System.Drawing.Point(244, 24);
            this.chyenTTK.Name = "chyenTTK";
            this.chyenTTK.Size = new System.Drawing.Size(33, 20);
            this.chyenTTK.TabIndex = 23;
            this.chyenTTK.Visible = false;
            this.chyenTTK.Click += new System.EventHandler(this.chyenTTK_Click);
            // 
            // lbSoKHNhanDon
            // 
            this.lbSoKHNhanDon.AutoSize = true;
            this.lbSoKHNhanDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSoKHNhanDon.ForeColor = System.Drawing.Color.Red;
            this.lbSoKHNhanDon.Location = new System.Drawing.Point(11, 426);
            this.lbSoKHNhanDon.Name = "lbSoKHNhanDon";
            this.lbSoKHNhanDon.Size = new System.Drawing.Size(215, 16);
            this.lbSoKHNhanDon.TabIndex = 21;
            this.lbSoKHNhanDon.Text = "Có 0 khách hàng đợt nhận đơn";
            this.lbSoKHNhanDon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // detail
            // 
            this.detail.AllowUserToAddRows = false;
            this.detail.AllowUserToDeleteRows = false;
            this.detail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.detail.ColumnHeadersHeight = 25;
            this.detail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SOHOSO,
            this.HOTEN,
            this.DIACHI,
            this.NGAY_NHAN,
            this.loiDon});
            this.detail.Location = new System.Drawing.Point(6, 50);
            this.detail.Name = "detail";
            this.detail.ReadOnly = true;
            this.detail.RowHeadersVisible = false;
            this.detail.Size = new System.Drawing.Size(354, 373);
            this.detail.TabIndex = 20;
            // 
            // SOHOSO
            // 
            this.SOHOSO.DataPropertyName = "SOHOSO";
            this.SOHOSO.HeaderText = "Số Hồ Sơ";
            this.SOHOSO.Name = "SOHOSO";
            this.SOHOSO.ReadOnly = true;
            // 
            // HOTEN
            // 
            this.HOTEN.DataPropertyName = "HOTEN";
            this.HOTEN.HeaderText = "Họ Tên";
            this.HOTEN.Name = "HOTEN";
            this.HOTEN.ReadOnly = true;
            // 
            // DIACHI
            // 
            this.DIACHI.DataPropertyName = "DIACHI";
            this.DIACHI.HeaderText = "Địa Chỉ";
            this.DIACHI.Name = "DIACHI";
            this.DIACHI.ReadOnly = true;
            this.DIACHI.Width = 150;
            // 
            // NGAY_NHAN
            // 
            this.NGAY_NHAN.DataPropertyName = "NGAYNHAN";
            this.NGAY_NHAN.HeaderText = "Ngày Nhận";
            this.NGAY_NHAN.Name = "NGAY_NHAN";
            this.NGAY_NHAN.ReadOnly = true;
            // 
            // loiDon
            // 
            this.loiDon.DataPropertyName = "LOAIDON";
            this.loiDon.HeaderText = "Loại Đơn";
            this.loiDon.Name = "loiDon";
            this.loiDon.ReadOnly = true;
            // 
            // mainGrid
            // 
            this.mainGrid.AllowUserToAddRows = false;
            this.mainGrid.AllowUserToDeleteRows = false;
            this.mainGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DOTNHAN,
            this.NGAYNHAN,
            this.LOAIDON,
            this.SATUS});
            this.mainGrid.Location = new System.Drawing.Point(14, 156);
            this.mainGrid.Name = "mainGrid";
            this.mainGrid.ReadOnly = true;
            this.mainGrid.RowHeadersVisible = false;
            this.mainGrid.Size = new System.Drawing.Size(591, 450);
            this.mainGrid.TabIndex = 28;
            this.mainGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.mainGrid.Sorted += new System.EventHandler(this.mainGrid_Sorted);
            // 
            // DOTNHAN
            // 
            this.DOTNHAN.DataPropertyName = "MADOT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DOTNHAN.DefaultCellStyle = dataGridViewCellStyle3;
            this.DOTNHAN.HeaderText = "Đợt Nhận Đơn";
            this.DOTNHAN.Name = "DOTNHAN";
            this.DOTNHAN.ReadOnly = true;
            this.DOTNHAN.Width = 115;
            // 
            // NGAYNHAN
            // 
            this.NGAYNHAN.DataPropertyName = "NGAYLAPDON";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NGAYNHAN.DefaultCellStyle = dataGridViewCellStyle4;
            this.NGAYNHAN.HeaderText = "Ngày Lập Đơn";
            this.NGAYNHAN.Name = "NGAYNHAN";
            this.NGAYNHAN.ReadOnly = true;
            this.NGAYNHAN.Width = 120;
            // 
            // LOAIDON
            // 
            this.LOAIDON.DataPropertyName = "TENLOAI";
            this.LOAIDON.HeaderText = "Loại Đơn Nhận";
            this.LOAIDON.Name = "LOAIDON";
            this.LOAIDON.ReadOnly = true;
            this.LOAIDON.Width = 250;
            // 
            // SATUS
            // 
            this.SATUS.DataPropertyName = "CHUYEN";
            this.SATUS.HeaderText = "Tình Trạng";
            this.SATUS.Name = "SATUS";
            this.SATUS.ReadOnly = true;
            // 
            // txtsoDot
            // 
            this.txtsoDot.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsoDot.Location = new System.Drawing.Point(25, 86);
            this.txtsoDot.Margin = new System.Windows.Forms.Padding(4);
            this.txtsoDot.Mask = "&&&&/&&&&";
            this.txtsoDot.Name = "txtsoDot";
            this.txtsoDot.Size = new System.Drawing.Size(106, 26);
            this.txtsoDot.TabIndex = 1;
            // 
            // createDate
            // 
            // 
            // 
            // 
            this.createDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.createDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.createDate.ButtonDropDown.Visible = true;
            this.createDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createDate.Location = new System.Drawing.Point(172, 87);
            this.createDate.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            this.createDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.createDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.createDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.createDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.createDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.createDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.createDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.createDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.createDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.createDate.MonthCalendar.DisplayMonth = new System.DateTime(2011, 8, 1, 0, 0, 0, 0);
            this.createDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.createDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.createDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.createDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.createDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.createDate.MonthCalendar.TodayButtonVisible = true;
            this.createDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.createDate.Name = "createDate";
            this.createDate.Size = new System.Drawing.Size(89, 26);
            this.createDate.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 19);
            this.label3.TabIndex = 22;
            this.label3.Text = "Loại Đơn Nhận";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(169, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "Ngày Tạo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 19);
            this.label1.TabIndex = 20;
            this.label1.Text = "Số Đợt Nhận Đơn";
            // 
            // cbLoaiHS
            // 
            this.cbLoaiHS.DisplayMember = "Text";
            this.cbLoaiHS.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiHS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoaiHS.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLoaiHS.FormattingEnabled = true;
            this.cbLoaiHS.ItemHeight = 20;
            this.cbLoaiHS.Location = new System.Drawing.Point(306, 86);
            this.cbLoaiHS.Name = "cbLoaiHS";
            this.cbLoaiHS.Size = new System.Drawing.Size(295, 26);
            this.cbLoaiHS.TabIndex = 3;
            // 
            // refresh
            // 
            this.refresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.refresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh.Image = global::TanHoaWater.Properties.Resources.refresh;
            this.refresh.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.refresh.Location = new System.Drawing.Point(526, 118);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 22);
            this.refresh.TabIndex = 6;
            this.refresh.Text = "Làm lại    ";
            this.refresh.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // addNewDot
            // 
            this.addNewDot.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addNewDot.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.addNewDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewDot.Image = global::TanHoaWater.Properties.Resources.note_add;
            this.addNewDot.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.addNewDot.Location = new System.Drawing.Point(335, 119);
            this.addNewDot.Margin = new System.Windows.Forms.Padding(4);
            this.addNewDot.Name = "addNewDot";
            this.addNewDot.Size = new System.Drawing.Size(89, 21);
            this.addNewDot.TabIndex = 4;
            this.addNewDot.Text = "Thêm Mới    ";
            this.addNewDot.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.addNewDot.Click += new System.EventHandler(this.addNewDot_Click);
            // 
            // SearchDot
            // 
            this.SearchDot.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SearchDot.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SearchDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchDot.Image = global::TanHoaWater.Properties.Resources.search2;
            this.SearchDot.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.SearchDot.Location = new System.Drawing.Point(432, 119);
            this.SearchDot.Margin = new System.Windows.Forms.Padding(4);
            this.SearchDot.Name = "SearchDot";
            this.SearchDot.Size = new System.Drawing.Size(87, 21);
            this.SearchDot.TabIndex = 5;
            this.SearchDot.Text = "Tìm Kiếm   ";
            this.SearchDot.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.SearchDot.Click += new System.EventHandler(this.SearchDot_Click);
            // 
            // uct_DOTNHANDON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.txtsoDot);
            this.Controls.Add(this.cbLoaiHS);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.mainGrid);
            this.Controls.Add(this.addNewDot);
            this.Controls.Add(this.SearchDot);
            this.Controls.Add(this.createDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reflectionLabel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "uct_DOTNHANDON";
            this.Size = new System.Drawing.Size(988, 614);
            this.Load += new System.EventHandler(this.DOTNHANDON_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.print)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.createDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel1;
        private DevComponents.DotNetBar.ButtonX refresh;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lbSoKHNhanDon;
        private System.Windows.Forms.DataGridView detail;
        private System.Windows.Forms.DataGridView mainGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOTNHAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGAYNHAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOAIDON;
        private System.Windows.Forms.DataGridViewTextBoxColumn SATUS;
        private DevComponents.DotNetBar.ButtonX addNewDot;
        private DevComponents.DotNetBar.ButtonX SearchDot;
        private System.Windows.Forms.MaskedTextBox txtsoDot;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput createDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOHOSO;
        private System.Windows.Forms.DataGridViewTextBoxColumn HOTEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIACHI;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGAY_NHAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn loiDon;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiHS;
        private System.Windows.Forms.PictureBox print;
        private DevComponents.DotNetBar.ButtonX chyenTTK;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkCD;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbBOPHAN;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}