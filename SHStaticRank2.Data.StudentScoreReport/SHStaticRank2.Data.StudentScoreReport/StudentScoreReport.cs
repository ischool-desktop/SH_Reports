using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using K12.Data;
using FISCA.Data;
using Aspose.Words;
using System.IO;
using DevComponents.DotNetBar.Controls;
using System.Xml.Linq;

namespace SHStaticRank2.Data.StudentScoreReport
{
    public partial class StudentScoreReport : BaseForm
    {
        private List<TagConfigRecord> _TagConfigRecords = new List<TagConfigRecord>();
        private FISCA.UDT.AccessHelper _AccessHelper = new FISCA.UDT.AccessHelper();
        List<string> SubjectNameList = new List<string>();
        public Configure Configure { get; private set; }
        private Dictionary<string, int> _SpecialListViewItem = new Dictionary<string, int>();

        string _strItem1 = "使用自訂範本(四學期成績)";
        string _strItem2 = "使用自訂範本(五學期成績)";
        string _strItem3 = "使用自訂範本(六學期成績)";
        string _strItem4 = "使用預設範本(六學期成績)";
        string _strItem5 = "使用預設回歸科目範本(六學期成績)";
        string _strItem6 = "班級Word";
        string _strItem7 = "大學繁星序號PDF";
        string _strItem8 = "學生身分證號PDF";

        public StudentScoreReport()
        {
            InitializeComponent();

            buttonX1.Enabled = false;
            _TagConfigRecords = K12.Data.TagConfig.SelectByCategory(TagCategory.Student);
            List<Configure> lc = _AccessHelper.Select<Configure>("Name = '學生個人歷年成績單'");
            this.Configure = (lc.Count >= 1) ? lc[0] : new Configure() { Name = "學生個人歷年成績單" };
            if (lc.Count >= 1)
                this.Configure.Decode();
            #region 填入類別

            List<string> prefix = new List<string>();
            List<string> tag = new List<string>();
            foreach (var item in _TagConfigRecords)
            {
                if (item.Prefix != "")
                {
                    if (!prefix.Contains(item.Prefix))
                        prefix.Add(item.Prefix);
                }
                else
                {
                    tag.Add(item.Name);
                }
            }
            // 不排名學生類別
            cboRankRilter.Items.Clear();
            // 類別排名1
            cboTagRank1.Items.Clear();
            // 類別排名2
            cboTagRank2.Items.Clear();

            cboRankRilter.Items.Add("");
            cboTagRank1.Items.Add("");
            cboTagRank2.Items.Add("");
            foreach (var s in prefix)
            {
                cboRankRilter.Items.Add("[" + s + "]");
                cboTagRank1.Items.Add("[" + s + "]");
                cboTagRank2.Items.Add("[" + s + "]");
            }
            foreach (var s in tag)
            {
                cboRankRilter.Items.Add(s);
                cboTagRank1.Items.Add(s);
                cboTagRank2.Items.Add(s);
            }

         
            #endregion
            buttonX1.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cboUseTemplae.Text == _strItem4)
                DownDefaultTemplate1();
            else if (cboUseTemplae.Text == _strItem5)
                DownDefaultTemplate2();
            else
                DownloadDefaultTemplate();
        }

        private void DownloadDefaultTemplate()
        {
            if (this.Configure == null) return;
            #region 儲存檔案
            string inputReportName = "多學期成績單樣板(" + this.Configure.Name + ")";
            string reportName = inputReportName;

            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".doc");

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }
            Document docTemp = null;
            try
            {
                
                #region 四學期範本
                if(cboUseTemplae.Text==_strItem1)
                {
                    if (Configure.Template1 != null && Configure.Template1.MailMerge.GetFieldNames().Count()>0)
                    {
                        docTemp = Configure.Template1;
                    }
                    else
                    {
                            MsgBox.Show("沒有四學期自訂範本");
                            return;                  
                    }                    
                }
                #endregion

                #region 五學期範本
                if (cboUseTemplae.Text == _strItem2)
                {
                    if (Configure.Template2 != null && Configure.Template2.MailMerge.GetFieldNames().Count() > 0)
                    {
                        docTemp = Configure.Template2;
                    }
                    else
                    {
                        MsgBox.Show("沒有五學期自訂範本");
                        return;
                    }                    
                }

                #endregion

                #region 六學期範本
                if (cboUseTemplae.Text == _strItem3)
                {
                    if (Configure.Template != null && Configure.Template.MailMerge.GetFieldNames().Count()>0)
                    {
                        docTemp = Configure.Template;
                    }
                    else{

                        MsgBox.Show("沒有六學期自訂範本");
                        return;
                    }                    
                }
                #endregion
                if(docTemp !=null)
                {
                    docTemp.Save(path, SaveFormat.Doc);
                    System.Diagnostics.Process.Start(path);
                }
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".doc";
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        docTemp.Save(sd.FileName, SaveFormat.Doc);
                        System.Diagnostics.Process.Start(sd.FileName);
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            #endregion
        }

        private void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkUpload.Enabled = false;
            UploadUserDefTemplate();
            lnkUpload.Enabled = true;
        }

        private void UploadUserDefTemplate()
        {
            if (cboUseTemplae.Text == _strItem4 || cboUseTemplae.Text == _strItem5)
            {
                MsgBox.Show("預設範本無法上傳.");
                return;
            }

            if (Configure == null) return;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "上傳樣板";
            dialog.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string msg = "";
                    if(cboUseTemplae.Text==_strItem1)
                    {
                        this.Configure.Template1 = new Aspose.Words.Document(dialog.FileName);
                        msg = "四學期成績自訂範本上傳完成";
                    }

                    if(cboUseTemplae.Text== _strItem2)
                    {
                        this.Configure.Template2 = new Aspose.Words.Document(dialog.FileName);
                        msg = "五學期成績自訂範本上傳完成";
                    }

                    if(cboUseTemplae.Text==_strItem3)
                    {
                        this.Configure.Template = new Aspose.Words.Document(dialog.FileName);
                        msg = "六學期成績自訂範本上傳完成";
                    }
                    

                    List<string> fields = new List<string>(this.Configure.Template.MailMerge.GetFieldNames());
                    this.Configure.SubjectLimit = 0;
                    while (fields.Contains("科目名稱" + (this.Configure.SubjectLimit + 1)))
                    {
                        this.Configure.SubjectLimit++;
                    }
                    Configure.Encode();

                    Configure.Save();
                    MsgBox.Show(msg);
                }
                catch
                {
                    MessageBox.Show("上傳範本失敗");
                }
            }
        }

        private void lblMappingTemp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Document doc = CalcMutilSemeSubjectRank.getMergeTable();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Word (*.doc)|*.doc";
            saveDialog.FileName = "合併欄位總表";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    doc.Save(saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("儲存失敗。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("開啟失敗。" + ex.Message);
                    return;
                }
            }
        }

        private void LoadSubjectToDataGrid()
        {
            dgSubjMapping.Rows.Clear();
            if (!string.IsNullOrWhiteSpace(Configure.SubjectMapping))
            {
                try
                {
                    XElement elmRoot = XElement.Parse(Configure.SubjectMapping);
                    foreach(XElement item in elmRoot.Elements("Item"))
                    {
                        int rowIdx = dgSubjMapping.Rows.Add();
                        dgSubjMapping.Rows[rowIdx].Cells[colSubject.Index].Value = item.Attribute("Subject").Value;
                        dgSubjMapping.Rows[rowIdx].Cells[colSysSubject.Index].Value = item.Attribute("SysSubject").Value;
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show("讀取科目對照失敗," + ex.Message);
                }
            }
        }

        private void SetSubjectFromDataGrid() 
        {
            XElement elmRoot = new XElement("Items");
            foreach (DataGridViewRow dgvr in dgSubjMapping.Rows)
            {
                if (dgvr.IsNewRow)
                    continue;

                if (dgvr.Cells[colSubject.Index].Value == null)
                    continue;

                string subjName = dgvr.Cells[colSubject.Index].Value.ToString().Replace(" ", "");

                if (string.IsNullOrEmpty(subjName))
                    continue;

                XElement elm = new XElement("Item");
                elm.SetAttributeValue("Subject", subjName);

                // 如果系統內沒輸入預設和回歸相同
                string SysSubjName = subjName;
                if (dgvr.Cells[colSysSubject.Index].Value != null)
                { 
                    string ss=dgvr.Cells[colSysSubject.Index].Value.ToString().Replace(" ", "");
                    if (!string.IsNullOrEmpty(ss))
                        SysSubjName = ss;
                }

                elm.SetAttributeValue("SysSubject",SysSubjName);
                elmRoot.Add(elm);
            }

            Configure.SubjectMapping = elmRoot.ToString();
        }    

        private void StudentScoreReport_Load(object sender, EventArgs e)
        {

            this.MaximumSize = this.MinimumSize = this.Size;
            cbxScoreType.Items.Add("擇優成績");
            cbxScoreType.Items.Add("原始成績");
            cbxScoreType.Text = "擇優成績";
            cbxScoreType.DropDownStyle = ComboBoxStyle.DropDownList;

            cboUseTemplae.Items.Add(_strItem1);
            cboUseTemplae.Items.Add(_strItem2);
            cboUseTemplae.Items.Add(_strItem3);
            cboUseTemplae.Items.Add(_strItem4);
            cboUseTemplae.Items.Add(_strItem5);
            cboUseTemplae.Text = _strItem1;
            cboUseTemplae.DropDownStyle = ComboBoxStyle.DropDownList;

            cboExportType.Items.Add(_strItem6);
            cboExportType.Items.Add(_strItem7);
            cboExportType.Items.Add(_strItem8);
            cboExportType.Text = _strItem6;
            cboExportType.DropDownStyle = ComboBoxStyle.DropDownList;

            cbxSubjSelectAll.Checked = false;
            FISCA.LogAgent.ApplicationLog.Log("成績", "計算", "計算學生多學期成績單。");            
            List<string> studIDList = new List<string>();
            QueryHelper qh = new QueryHelper();

            string strSQ = @"SELECT DISTINCT tmp.subject
FROM xpath_table( 'id',
				'''<root>''||score_info||''</root>''',
				'sems_subj_score',
				'/root/SemesterSubjectScoreInfo/Subject/@科目',
				'ref_student_id IN ( select student.id from student 
									INNER JOIN class ON student.ref_class_id = class.id 
									WHERE student.status=1 AND class.grade_year = 3 )'
				) 
AS tmp(id int, subject varchar(200))";
            DataTable dt = qh.Select(strSQ);
            foreach (DataRow dr in dt.Rows)
                SubjectNameList.Add(dr[0].ToString());
            SubjectNameList.Sort(new StringComparer("國文"
                                , "英文"
                                , "數學"
                                , "理化"
                                , "生物"
                                , "社會"
                                , "物理"
                                , "化學"
                                , "歷史"
                                , "地理"
                                , "公民"));

            // 填入科目名稱
            foreach (string name in SubjectNameList)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = name;
                lvi.Name = name;
                lvi.Checked = false;
                lvwSubjectPri.Items.Add(lvi);
            }
            // 載入對照表
            LoadSubjectToDataGrid();
         
            // 載入畫面選項
            LoadUIConfig();
        }


        private void LoadUIConfig()
        {
            try
            {
                // 是否只顯示回歸
                chkSubjMappingOnly.Checked = Configure.CheckExportSubjectMapping;
                
                if(!string.IsNullOrEmpty(Configure.UIConfigString))
                {
                    XElement elmRoot = XElement.Parse(Configure.UIConfigString);

                    if (elmRoot != null)
                    {
                        if (elmRoot.Element("不排名學生類別") != null)
                            cboRankRilter.Text = elmRoot.Element("不排名學生類別").Value;

                        if (elmRoot.Element("類別排名1") != null)
                            cboTagRank1.Text = elmRoot.Element("類別排名1").Value;

                        if (elmRoot.Element("類別排名2") != null)
                            cboTagRank2.Text = elmRoot.Element("類別排名2").Value;

                        if (elmRoot.Element("採計成績") != null)
                            cbxScoreType.Text = elmRoot.Element("採計成績").Value;

                        if (elmRoot.Element("使用範本") != null)                            
                            cboUseTemplae.Text = elmRoot.Element("使用範本").Value;

                        if (elmRoot.Element("產生檔案") != null)
                            cboExportType.Text = elmRoot.Element("產生檔案").Value;

                        if (elmRoot.Element("只顯示回歸科目") != null)
                        {
                            bool tf;
                            if(bool.TryParse(elmRoot.Element("只顯示回歸科目").Value,out tf))
                            {
                                chkSubjMappingOnly.Checked = tf;
                            }
                        }

                        // 勾選
                        if (elmRoot.Element("列印科目") != null)
                        {
                            List<string> subList = elmRoot.Element("列印科目").Value.Split(',').ToList();
                            foreach (ListViewItem lvi in lvwSubjectPri.Items)
                            {
                                if (subList.Contains(lvi.Text))
                                    lvi.Checked = true;
                            }
                        }
                    }
                }
                
            }catch(Exception ex)
            {
                MsgBox.Show("載入設定檔錯誤," + ex.Message);
            }
        
        }

        /// <summary>
        /// 設訂畫面到設定檔
        /// </summary>
        private void SetUIConfig()
        {
            XElement elmRoot = new XElement("UIConfig");
            elmRoot.SetElementValue("不排名學生類別", cboRankRilter.Text);

            List<string> SubList = new List<string>();
            foreach (ListViewItem lvi in lvwSubjectPri.CheckedItems)
                SubList.Add(lvi.Text);
            string strSubj = string.Join(",", SubList.ToArray());
            elmRoot.SetElementValue("列印科目", strSubj);
            elmRoot.SetElementValue("類別排名1", cboTagRank1.Text);
            elmRoot.SetElementValue("類別排名2", cboTagRank2.Text);
            elmRoot.SetElementValue("採計成績", cbxScoreType.Text);
            elmRoot.SetElementValue("使用範本", cboUseTemplae.Text);
            elmRoot.SetElementValue("產生檔案", cboExportType.Text);
            elmRoot.SetElementValue("只顯示回歸科目", chkSubjMappingOnly.Checked.ToString());
            Configure.UIConfigString = elmRoot.ToString();
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {

            #region 檢查回歸科目重複名稱設定
            
            // 檢查畫面資料是否重複
            List<string> chkStr = new List<string>();
            bool hasSame = false;
            foreach (DataGridViewRow dgvr in dgSubjMapping.Rows)
            {
                if (dgvr.IsNewRow)
                    continue;

                foreach (DataGridViewCell dgvc in dgvr.Cells)
                {
                    if (dgvc.Value != null)
                        if (dgvc.ColumnIndex == colSubject.Index)
                        {
                            string key = dgvc.Value.ToString().Replace(" ","");
                            if (!chkStr.Contains(key))
                                chkStr.Add(key);
                            else
                            {
                                hasSame = true;
                                break;
                            }
                        }
                }

                if (hasSame)
                    break;
            }

            if (hasSame)
            {
                MsgBox.Show("回歸科目有重複名稱，請修改", "回歸科目名稱重複");
                return;
            }

            #endregion


            Configure.CalcGradeYear1 = false;
            Configure.CalcGradeYear2 = false;
            Configure.CalcGradeYear3 = true; //三年級
            Configure.CalcGradeYear4 = false;
            Configure.DoNotSaveIt = true;
            Configure.計算學業成績排名 = true;
            Configure.WithCalSemesterScoreRank = true;

            if (cbxScoreType.Text == "擇優成績")
            {
                Configure.use原始成績 = true;//原始成績
                Configure.use補考成績 = true;
                Configure.use重修成績 = true;
                Configure.use手動調整成績 = true;
                Configure.use學年調整成績 = true;
                Configure.RankFilterUseScoreList.Add("原始成績");
                Configure.RankFilterUseScoreList.Add("補考成績");
                Configure.RankFilterUseScoreList.Add("重修成績");
                Configure.RankFilterUseScoreList.Add("手動調整成績");
                Configure.RankFilterUseScoreList.Add("學年調整成績");
            }
            else
            {
                Configure.use原始成績 = true;//原始成績
                Configure.use補考成績 = false;
                Configure.use重修成績 = false;
                Configure.use手動調整成績 = false;
                Configure.use學年調整成績 = false;
                Configure.RankFilterUseScoreList.Add("原始成績");
            }
            
            //foreach (string SubjectName in SubjectNameList)//所有科目
            foreach(ListViewItem lvi in lvwSubjectPri.CheckedItems)
            {
                string SubjectName = lvi.Text;
                Configure.useSubjectPrintList.Add(SubjectName);
                Configure.useSubjecOrder1List.Add(SubjectName);
                Configure.useSubjecOrder2List.Add(SubjectName);
            }
            Configure.Name = "學生個人歷年成績單";
            Configure.SortGradeYear = "三年級";

            Configure.CheckExportPDF = true;
            Configure.CheckUseIDNumber = true;

            // 使用合併樣版
            Document docTemp = null;

            if(cboUseTemplae.Text ==_strItem1)
            {
                // 四學期成績自訂樣版
                Configure.useGradeSemesterList.Add("11");
                Configure.useGradeSemesterList.Add("12");
                Configure.useGradeSemesterList.Add("21");
                Configure.useGradeSemesterList.Add("22");
                Configure.RankFilterGradeSemeterList.Add("一上");
                Configure.RankFilterGradeSemeterList.Add("一下");
                Configure.RankFilterGradeSemeterList.Add("二上");
                Configure.RankFilterGradeSemeterList.Add("二下");
                if (Configure.Template1 != null && Configure.Template1.MailMerge.GetFieldNames().Count() > 0)
                {
                    docTemp = Configure.Template1.Clone();
                }
                else
                {
                    MsgBox.Show("沒有四學期成績自訂範本");
                    return;
                }

            }else if(cboUseTemplae.Text ==_strItem2)
            {
                // 五學期成績自訂樣版
                Configure.useGradeSemesterList.Add("11");
                Configure.useGradeSemesterList.Add("12");
                Configure.useGradeSemesterList.Add("21");
                Configure.useGradeSemesterList.Add("22");
                Configure.useGradeSemesterList.Add("31");
                Configure.RankFilterGradeSemeterList.Add("一上");
                Configure.RankFilterGradeSemeterList.Add("一下");
                Configure.RankFilterGradeSemeterList.Add("二上");
                Configure.RankFilterGradeSemeterList.Add("二下");
                Configure.RankFilterGradeSemeterList.Add("三上");
                if (Configure.Template2 != null && Configure.Template2.MailMerge.GetFieldNames().Count() > 0)
                {
                    docTemp = Configure.Template2.Clone();
                }
                else
                {
                    MsgBox.Show("沒有五學期成績自訂範本");
                    return;
                }           
                

            }else if(cboUseTemplae.Text == _strItem3)
            {
                // 六學期成績自訂樣版
                Configure.useGradeSemesterList.Add("11");
                Configure.useGradeSemesterList.Add("12");
                Configure.useGradeSemesterList.Add("21");
                Configure.useGradeSemesterList.Add("22");
                Configure.useGradeSemesterList.Add("31");
                Configure.useGradeSemesterList.Add("32");
                Configure.RankFilterGradeSemeterList.Add("一上");
                Configure.RankFilterGradeSemeterList.Add("一下");
                Configure.RankFilterGradeSemeterList.Add("二上");
                Configure.RankFilterGradeSemeterList.Add("二下");
                Configure.RankFilterGradeSemeterList.Add("三上");
                Configure.RankFilterGradeSemeterList.Add("三下");
                if (Configure.Template != null && Configure.Template.MailMerge.GetFieldNames().Count()>0)
                {
                    docTemp = Configure.Template;
                }else
                {
                    MsgBox.Show("沒有六學期成績自訂範本");
                    return;
                }                

            }else
            {
                Configure.useGradeSemesterList.Add("11");
                Configure.useGradeSemesterList.Add("12");
                Configure.useGradeSemesterList.Add("21");
                Configure.useGradeSemesterList.Add("22");
                Configure.useGradeSemesterList.Add("31");
                Configure.useGradeSemesterList.Add("32");
                Configure.RankFilterGradeSemeterList.Add("一上");
                Configure.RankFilterGradeSemeterList.Add("一下");
                Configure.RankFilterGradeSemeterList.Add("二上");
                Configure.RankFilterGradeSemeterList.Add("二下");                
                Configure.RankFilterGradeSemeterList.Add("三上");                
                Configure.RankFilterGradeSemeterList.Add("三下");
            }

            Configure.CheckExportStudent = true;
            Configure.NotRankTag = cboRankRilter.Text;
            Configure.Rank1Tag = cboTagRank1.Text;
            Configure.Rank2Tag = cboTagRank2.Text;
            Configure.RankFilterTagName = cboRankRilter.Text;


            // 儲存對照
            SetSubjectFromDataGrid();

            // 是否只顯示回歸
            Configure.CheckExportSubjectMapping = chkSubjMappingOnly.Checked;

            // 儲存畫面選項
            SetUIConfig();

            Configure.Save();

            //if (this.Configure.Template == null)
            //{
            //    Configure.Template = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__範本)); 

            //}
            //else
            //{
            //    //計算檔案大小
            //    MemoryStream ms = new MemoryStream();
            //    this.Configure.Template.Save(ms, SaveFormat.Doc);
            //    byte[] bb = ms.ToArray();

            //    double bbSize = (bb.Count() / 1024);

            //    if (bbSize < 30)
            //        Configure.Template = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__範本));                
            //}


            // 使用非自訂範本切換但是不儲存
            if (cboUseTemplae.Text == _strItem4)
                docTemp = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__6));
            
            if(cboUseTemplae.Text==_strItem5)
                docTemp = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__回歸科目範本_6));

            Configure.Template = docTemp;

            // 班級Word
            if(cboExportType.Text==_strItem6)            
                Configure.CheckExportPDF = false;                
            
            // 序號 PDF
            if(cboExportType.Text==_strItem7)
            {
                Configure.CheckExportPDF = true;
                Configure.CheckUseIDNumber = false;
            }

            // 身分證號 PDF
            if (cboExportType.Text == _strItem8)
            {
                Configure.CheckExportPDF = true;
                Configure.CheckUseIDNumber = true;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void cbxSubjSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvwSubjectPri.Items)
                lvi.Checked = cbxSubjSelectAll.Checked;            
        }

        private void DownDefaultTemplate1()
        {
            #region 下載預設範本
            string reportName = "多學期成績單樣板(學生個人歷年成績單)預設範本";
            try
            {
                string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Reports");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, reportName + ".doc");

                if (File.Exists(path))
                {
                    int i = 1;
                    while (true)
                    {
                        string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                        if (!File.Exists(newPath))
                        {
                            path = newPath;
                            break;
                        }
                    }
                }

                Document docTemp = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__6));
                docTemp.Save(path, SaveFormat.Doc);
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".doc";
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {

                        Document docTemp = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__6));
                        docTemp.Save(sd.FileName);
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }

            }
            #endregion
        }

        private void DownDefaultTemplate2()
        {
            #region 下載回歸科目預設範本
            string reportName = "多學期成績單樣板(學生個人歷年成績單)回歸科目預設範本";
            try
            {
                string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Reports");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, reportName + ".doc");

                if (File.Exists(path))
                {
                    int i = 1;
                    while (true)
                    {
                        string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                        if (!File.Exists(newPath))
                        {
                            path = newPath;
                            break;
                        }
                    }
                }

                Document docTemp = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__回歸科目範本_6));
                docTemp.Save(path, SaveFormat.Doc);
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".doc";
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {

                        Document docTemp = new Document(new MemoryStream(Properties.Resources.多學期成績單樣板_學生個人歷年成績單__回歸科目範本_6));
                        docTemp.Save(sd.FileName);
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }

            }
            #endregion
        }

        private void dgSubjMapping_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //dgSubjMapping.EndEdit();            
            //// 檢查對照
            //if (dgSubjMapping.CurrentCell.ColumnIndex == colSysSubject.Index)
            //{
            //    dgSubjMapping.CurrentCell.ErrorText = "";
            //    if (dgSubjMapping.CurrentCell.Value != null)
            //    {
            //        string value = dgSubjMapping.CurrentCell.Value.ToString();
            //        // 同時包含 , +
            //        if (value.IndexOf(",") > 0 && value.IndexOf("+") > 0)
            //        {
            //            dgSubjMapping.CurrentCell.ErrorText = "不能同時使用,+";
            //        }
            //    }
            //}
            //dgSubjMapping.BeginEdit(false);
        }
    }
}
