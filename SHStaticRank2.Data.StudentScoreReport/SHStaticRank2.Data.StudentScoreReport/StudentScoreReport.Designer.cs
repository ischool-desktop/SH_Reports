﻿namespace SHStaticRank2.Data.StudentScoreReport
{
    partial class StudentScoreReport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboTagRank1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboTagRank2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkSubjMappingOnly = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.dgSubjMapping = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSysSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxSubjSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lvwSubjectPri = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboRankRilter = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lblMappingTemp = new System.Windows.Forms.LinkLabel();
            this.lnkUpload = new System.Windows.Forms.LinkLabel();
            this.lnkDownload = new System.Windows.Forms.LinkLabel();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cbxScoreType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboUseTemplae = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.cboExportType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubjMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.cboTagRank1);
            this.groupPanel2.Controls.Add(this.cboTagRank2);
            this.groupPanel2.Controls.Add(this.labelX5);
            this.groupPanel2.DrawTitleBox = false;
            this.groupPanel2.Location = new System.Drawing.Point(12, 306);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(298, 99);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 35;
            this.groupPanel2.Text = "排名類別設定";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(9, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(82, 21);
            this.labelX1.TabIndex = 13;
            this.labelX1.Text = "類別排名1：";
            // 
            // cboTagRank1
            // 
            this.cboTagRank1.DisplayMember = "Text";
            this.cboTagRank1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTagRank1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTagRank1.FormattingEnabled = true;
            this.cboTagRank1.ItemHeight = 19;
            this.cboTagRank1.Location = new System.Drawing.Point(129, 7);
            this.cboTagRank1.Name = "cboTagRank1";
            this.cboTagRank1.Size = new System.Drawing.Size(160, 25);
            this.cboTagRank1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTagRank1.TabIndex = 12;
            // 
            // cboTagRank2
            // 
            this.cboTagRank2.DisplayMember = "Text";
            this.cboTagRank2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTagRank2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTagRank2.FormattingEnabled = true;
            this.cboTagRank2.ItemHeight = 19;
            this.cboTagRank2.Location = new System.Drawing.Point(129, 38);
            this.cboTagRank2.Name = "cboTagRank2";
            this.cboTagRank2.Size = new System.Drawing.Size(160, 25);
            this.cboTagRank2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTagRank2.TabIndex = 14;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(9, 38);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(82, 21);
            this.labelX5.TabIndex = 15;
            this.labelX5.Text = "類別排名2：";
            // 
            // groupPanel3
            // 
            this.groupPanel3.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.chkSubjMappingOnly);
            this.groupPanel3.Controls.Add(this.labelX9);
            this.groupPanel3.Controls.Add(this.labelX7);
            this.groupPanel3.Controls.Add(this.dgSubjMapping);
            this.groupPanel3.Controls.Add(this.cbxSubjSelectAll);
            this.groupPanel3.Controls.Add(this.labelX4);
            this.groupPanel3.Controls.Add(this.lvwSubjectPri);
            this.groupPanel3.Controls.Add(this.labelX3);
            this.groupPanel3.Controls.Add(this.cboRankRilter);
            this.groupPanel3.DrawTitleBox = false;
            this.groupPanel3.Location = new System.Drawing.Point(12, 16);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(776, 284);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.Class = "";
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 34;
            this.groupPanel3.Text = "排名對象";
            // 
            // chkSubjMappingOnly
            // 
            this.chkSubjMappingOnly.AutoSize = true;
            // 
            // 
            // 
            this.chkSubjMappingOnly.BackgroundStyle.Class = "";
            this.chkSubjMappingOnly.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkSubjMappingOnly.Location = new System.Drawing.Point(411, 28);
            this.chkSubjMappingOnly.Name = "chkSubjMappingOnly";
            this.chkSubjMappingOnly.Size = new System.Drawing.Size(187, 21);
            this.chkSubjMappingOnly.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkSubjMappingOnly.TabIndex = 20;
            this.chkSubjMappingOnly.Text = "只產生科目對照表回歸科目";
            // 
            // labelX9
            // 
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(9, 212);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(752, 42);
            this.labelX9.TabIndex = 19;
            this.labelX9.Text = "列印科目對照表說明（若回歸科目超過一科時，於「系統內科目名稱」欄位中以「+」分隔多個科目，例如：「基礎生物+生物選修」）";
            this.labelX9.WordWrap = true;
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(300, 29);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(114, 21);
            this.labelX7.TabIndex = 18;
            this.labelX7.Text = "列印科目對照表：";
            // 
            // dgSubjMapping
            // 
            this.dgSubjMapping.BackgroundColor = System.Drawing.Color.White;
            this.dgSubjMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSubjMapping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSubject,
            this.colSysSubject});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSubjMapping.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgSubjMapping.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSubjMapping.Location = new System.Drawing.Point(300, 56);
            this.dgSubjMapping.Name = "dgSubjMapping";
            this.dgSubjMapping.RowTemplate.Height = 24;
            this.dgSubjMapping.Size = new System.Drawing.Size(455, 150);
            this.dgSubjMapping.TabIndex = 17;
            this.dgSubjMapping.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgSubjMapping_CurrentCellDirtyStateChanged);
            // 
            // colSubject
            // 
            this.colSubject.HeaderText = "回歸科目名稱";
            this.colSubject.Name = "colSubject";
            this.colSubject.Width = 150;
            // 
            // colSysSubject
            // 
            this.colSysSubject.HeaderText = "系統內科目名稱";
            this.colSysSubject.Name = "colSysSubject";
            this.colSysSubject.Width = 150;
            // 
            // cbxSubjSelectAll
            // 
            this.cbxSubjSelectAll.AutoSize = true;
            // 
            // 
            // 
            this.cbxSubjSelectAll.BackgroundStyle.Class = "";
            this.cbxSubjSelectAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxSubjSelectAll.Location = new System.Drawing.Point(79, 28);
            this.cbxSubjSelectAll.Name = "cbxSubjSelectAll";
            this.cbxSubjSelectAll.Size = new System.Drawing.Size(54, 21);
            this.cbxSubjSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxSubjSelectAll.TabIndex = 16;
            this.cbxSubjSelectAll.Text = "全選";
            this.cbxSubjSelectAll.CheckedChanged += new System.EventHandler(this.cbxSubjSelectAll_CheckedChanged);
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(6, 29);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(74, 21);
            this.labelX4.TabIndex = 15;
            this.labelX4.Text = "列印科目：";
            // 
            // lvwSubjectPri
            // 
            // 
            // 
            // 
            this.lvwSubjectPri.Border.Class = "ListViewBorder";
            this.lvwSubjectPri.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvwSubjectPri.CheckBoxes = true;
            this.lvwSubjectPri.Location = new System.Drawing.Point(6, 56);
            this.lvwSubjectPri.Name = "lvwSubjectPri";
            this.lvwSubjectPri.Size = new System.Drawing.Size(280, 152);
            this.lvwSubjectPri.TabIndex = 14;
            this.lvwSubjectPri.UseCompatibleStateImageBehavior = false;
            this.lvwSubjectPri.View = System.Windows.Forms.View.List;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(9, 5);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(114, 21);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "不排名學生類別：";
            // 
            // cboRankRilter
            // 
            this.cboRankRilter.DisplayMember = "Text";
            this.cboRankRilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRankRilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRankRilter.FormattingEnabled = true;
            this.cboRankRilter.ItemHeight = 19;
            this.cboRankRilter.Location = new System.Drawing.Point(129, 3);
            this.cboRankRilter.Name = "cboRankRilter";
            this.cboRankRilter.Size = new System.Drawing.Size(160, 25);
            this.cboRankRilter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboRankRilter.TabIndex = 11;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(708, 450);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 42;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblMappingTemp
            // 
            this.lblMappingTemp.AutoSize = true;
            this.lblMappingTemp.BackColor = System.Drawing.Color.Transparent;
            this.lblMappingTemp.Location = new System.Drawing.Point(162, 453);
            this.lblMappingTemp.Name = "lblMappingTemp";
            this.lblMappingTemp.Size = new System.Drawing.Size(112, 17);
            this.lblMappingTemp.TabIndex = 41;
            this.lblMappingTemp.TabStop = true;
            this.lblMappingTemp.Text = "下載合併欄位總表";
            this.lblMappingTemp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblMappingTemp_LinkClicked);
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Location = new System.Drawing.Point(88, 453);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(60, 17);
            this.lnkUpload.TabIndex = 40;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "上傳範本";
            this.lnkUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload_LinkClicked);
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.BackColor = System.Drawing.Color.Transparent;
            this.lnkDownload.Location = new System.Drawing.Point(21, 453);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(60, 17);
            this.lnkDownload.TabIndex = 39;
            this.lnkDownload.TabStop = true;
            this.lnkDownload.Text = "下載範本";
            this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownload_LinkClicked);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(627, 450);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 38;
            this.buttonX1.Text = "確定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(360, 349);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(85, 21);
            this.labelX2.TabIndex = 44;
            this.labelX2.Text = "2.採計成績：";
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(360, 322);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(235, 21);
            this.labelX6.TabIndex = 43;
            this.labelX6.Text = "1.排名對象：    三年級，學生狀態:一般";
            // 
            // cbxScoreType
            // 
            this.cbxScoreType.DisplayMember = "Text";
            this.cbxScoreType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxScoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScoreType.FormattingEnabled = true;
            this.cbxScoreType.ItemHeight = 19;
            this.cbxScoreType.Location = new System.Drawing.Point(451, 347);
            this.cbxScoreType.Name = "cbxScoreType";
            this.cbxScoreType.Size = new System.Drawing.Size(251, 25);
            this.cbxScoreType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxScoreType.TabIndex = 45;
            // 
            // cboUseTemplae
            // 
            this.cboUseTemplae.DisplayMember = "Text";
            this.cboUseTemplae.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboUseTemplae.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUseTemplae.FormattingEnabled = true;
            this.cboUseTemplae.ItemHeight = 19;
            this.cboUseTemplae.Location = new System.Drawing.Point(451, 377);
            this.cboUseTemplae.Name = "cboUseTemplae";
            this.cboUseTemplae.Size = new System.Drawing.Size(251, 25);
            this.cboUseTemplae.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboUseTemplae.TabIndex = 49;
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(360, 379);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(85, 21);
            this.labelX8.TabIndex = 48;
            this.labelX8.Text = "3.使用範本：";
            // 
            // cboExportType
            // 
            this.cboExportType.DisplayMember = "Text";
            this.cboExportType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExportType.FormattingEnabled = true;
            this.cboExportType.ItemHeight = 19;
            this.cboExportType.Location = new System.Drawing.Point(451, 410);
            this.cboExportType.Name = "cboExportType";
            this.cboExportType.Size = new System.Drawing.Size(251, 25);
            this.cboExportType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboExportType.TabIndex = 51;
            // 
            // labelX10
            // 
            this.labelX10.AutoSize = true;
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(360, 412);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(85, 21);
            this.labelX10.TabIndex = 50;
            this.labelX10.Text = "4.產生檔案：";
            // 
            // StudentScoreReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 484);
            this.Controls.Add(this.cboExportType);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.cboUseTemplae);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.cbxScoreType);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblMappingTemp);
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.lnkDownload);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel3);
            this.DoubleBuffered = true;
            this.Name = "StudentScoreReport";
            this.Text = "學生歷年成績單";
            this.Load += new System.EventHandler(this.StudentScoreReport_Load);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubjMapping)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTagRank1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTagRank2;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ListViewEx lvwSubjectPri;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboRankRilter;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.LinkLabel lblMappingTemp;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private System.Windows.Forms.LinkLabel lnkDownload;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxSubjSelectAll;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxScoreType;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgSubjMapping;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSysSubject;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboUseTemplae;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSubjMappingOnly;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboExportType;
        private DevComponents.DotNetBar.LabelX labelX10;
    }
}