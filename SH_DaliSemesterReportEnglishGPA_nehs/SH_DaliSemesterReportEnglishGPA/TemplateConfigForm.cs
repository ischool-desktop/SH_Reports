using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
//using Aspose.Words;
using System.Xml;
using K12.Data.Configuration;
using System.Diagnostics;
using DevComponents.DotNetBar.Controls;
using FISCA.Presentation.Controls;

namespace SH_DaliSemesterReportEnglishGPA_nehs
{
    public partial class TemplateConfigForm : BaseForm
    {
        private bool getOut = false;
        private RadioButton cRB = null;

        private int _useTemplateNumber = 0;
        private int _useRetakeNumber = 0;

        private byte[] _buffer1 = null;
        private string _base64string1 = null;

        private bool _isUpload1 = false;

        public TemplateConfigForm(dynamic o)
        {
            InitializeComponent();

            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);

            //  使用自訂範本或預設範本
            _useTemplateNumber = o.TemplateNumber;
            switch (_useTemplateNumber)
            {
                case 1:
                    checkBoxX1.Checked = true;
                    break;
                case 2:
                    checkBoxX2.Checked = true;
                    break;
                default:
                    break;
            }

            //  重修成績顯示方式
            _useRetakeNumber = o.RetakeNumber;
            switch (_useRetakeNumber)
            {
                case 1:
                    this.radioButton1.Checked = true;
                    break;
                case 2:
                    this.radioButton2.Checked = true;
                    break;
                default:
                    break;
            }

            _buffer1 = o.buffer1;

            txtResitSign.Text = o.ResitSign;                                                    //  補考符號
            txtRetakeSign.Text = o.RetakeSign;                                              //  重修符號

            txtFailedSign.Text = o.FailedSign;                                                 //  不及格符號
            txtSchoolYearAdjustSign.Text = o.SchoolYearAdjustSign;       //   學年調整成績符號
            txtManualAdjustSign.Text = o.ManualAdjustSign;                   //   手動調整成績符號
        }

        //  檢視「高中英文成績單(GPA)(GPA)預設範本 (含學年成績)」
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "高中英文成績單(GPA)範本.xls";
            sfd.Filter = "相容於 Excel 2003 檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                   // DownloadTemplate(sfd, Properties.Resources.高中英文成績單);
                    DownloadTemplate(sfd, Properties.Resources.實中_高中部_GPA_英文成績單);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        //  檢視「高中英文成績單(GPA)自訂範本 (含學年成績)」
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂高中英文成績單(GPA).xls";
            sfd.Filter = "相容於 Excel 2003 檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DownloadTemplate(sfd, _buffer1);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        //  上傳「高中英文成績單(GPA)自訂範本 (含學年成績)」
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "上傳自訂高中英文成績單(GPA)範本";
            ofd.Filter = "相容於 Excel 2003 檔案 (*.xls)|*.xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    UploadTemplate(ofd.FileName, ref _isUpload1, ref _base64string1);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        //  下載模組
        private void DownloadTemplate(SaveFileDialog sfd, byte[] fileData)
        {
            if ((fileData == null) || (fileData.Length == 0))
            {
                throw new Exception("檔案不存在，無法檢視。");
            }

            try
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);

                fs.Write(fileData, 0, fileData.Length);
                fs.Close();
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch
            {
                throw new Exception("指定路徑無法存取。");
            }
        }

        //  上傳模組
        private void UploadTemplate(string fileName, ref bool uploadIndex, ref string uploadData)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);

            byte[] tempBuffer = new byte[fs.Length];
            fs.Read(tempBuffer, 0, tempBuffer.Length);

            MemoryStream ms = new MemoryStream(tempBuffer);

            try
            {
                Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();

                wb.Open(ms, Aspose.Cells.FileFormatType.Excel2003);
                wb = null;
            }
            catch
            {
                throw new Exception("此版英文成績單(GPA)範本限用相容於 Excel 2003 檔案。");
            }

            try
            {
                uploadData = Convert.ToBase64String(tempBuffer);
                uploadIndex = true;

                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //  離開
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        //  設定
        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region 儲存 Preference

            ConfigData config = K12.Data.School.Configuration["實驗中學GPA英文成績單"];
            config["TemplateNumber"] = _useTemplateNumber.ToString();
            config["RetakeNumber"] = _useRetakeNumber.ToString();

            if (_isUpload1)
                config["CustomizeTemplate1"] = _base64string1;

            //config["Custodian"] = cboRecvIdentity.SelectedIndex.ToString();
            //config["Address"] = cboRecvAddress.SelectedIndex.ToString();
            //config["Phone"] = cboRecvPhone.SelectedIndex.ToString();
            //config["CoreSubjectSign"] = txtCoreSubjectSign.Text;
            //config["CoreCourseSign"] = txtCoreCourseSign.Text;
            config["ResitSign"] = txtResitSign.Text;
            config["RetakeSign"] = txtRetakeSign.Text;
            config["SchoolYearAdjustSign"] = txtSchoolYearAdjustSign.Text;
            config["ManualAdjustSign"] = txtManualAdjustSign.Text;
            config["FailedSign"] = txtFailedSign.Text;

            config.Save();

            #endregion

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBoxX_Click(object sender, EventArgs e)
        {
            if (getOut)
                return;

            cRB = (RadioButton)sender;
            getOut = true;

            checkBoxX1.Checked = false;
            checkBoxX2.Checked = false;

            cRB.Checked = true;

            getOut = false;

            _useTemplateNumber = Convert.ToInt32(((RadioButton)sender).Name.Substring((((RadioButton)sender).Name.Length - 1), 1));
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            _useRetakeNumber = Convert.ToInt32(((System.Windows.Forms.RadioButton)sender).Name.Substring((((System.Windows.Forms.RadioButton)sender).Name.Length - 1), 1));
        }
    }
}
