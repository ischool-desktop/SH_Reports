﻿using System;
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

namespace SHStaticRank2.Data.StudentScoreReport
{
    public partial class ClassScoreReport : BaseForm
    {
        private List<TagConfigRecord> _TagConfigRecords = new List<TagConfigRecord>();
        private FISCA.UDT.AccessHelper _AccessHelper = new FISCA.UDT.AccessHelper();
        List<string> SubjectNameList = new List<string>();
        public Configure Configure { get; private set; }
        private Dictionary<string, int> _SpecialListViewItem = new Dictionary<string, int>();

        public ClassScoreReport()
        {
            InitializeComponent();            

            buttonX1.Enabled = false;
            _TagConfigRecords = K12.Data.TagConfig.SelectByCategory(TagCategory.Student);
            List<Configure> lc = _AccessHelper.Select<Configure>("Name = '班級歷年成績單'");
            this.Configure = (lc.Count >= 1) ? lc[0] : new Configure() { Name = "班級歷年成績單" };
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

  

        private void ClassScoreReport_Load(object sender, EventArgs e)
        {
            cbxScoreType.Items.Add("擇優成績");
            cbxScoreType.Items.Add("原始成績");
            cbxScoreType.Text = "擇優成績";
            cbxScoreType.DropDownStyle = ComboBoxStyle.DropDownList;

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
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            if (lvwSubjectPri.CheckedItems.Count > 6)
            {
                MsgBox.Show("最多只能選6科");
                return;
            }
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
            foreach (ListViewItem lvi in lvwSubjectPri.CheckedItems)
            {
                string SubjectName = lvi.Text;
                Configure.useSubjectPrintList.Add(SubjectName);
                Configure.useSubjecOrder1List.Add(SubjectName);
                Configure.useSubjecOrder2List.Add(SubjectName);
            }
            Configure.Name = "班級歷年成績單";
            Configure.SortGradeYear = "三年級";
            Configure.useGradeSemesterList.Add("11");
            Configure.useGradeSemesterList.Add("12");
            Configure.useGradeSemesterList.Add("21");
            Configure.useGradeSemesterList.Add("22");
            Configure.RankFilterGradeSemeterList.Add("一上");
            Configure.RankFilterGradeSemeterList.Add("一下");
            Configure.RankFilterGradeSemeterList.Add("二上");
            Configure.RankFilterGradeSemeterList.Add("二下");
            Configure.useGradeSemesterList.Add("31");
            Configure.RankFilterGradeSemeterList.Add("三上");
            Configure.useGradeSemesterList.Add("32");
            Configure.RankFilterGradeSemeterList.Add("三下");
            Configure.CheckExportStudent = false;
            Configure.NotRankTag = cboRankRilter.Text;
            Configure.Rank1Tag = cboTagRank1.Text;
            Configure.Rank2Tag = cboTagRank2.Text;
            Configure.RankFilterTagName = cboRankRilter.Text;
            //if (Configure.Template == null)
            //    Configure.Template = new Document(new MemoryStream(Properties.Resources.高中部班級歷年成績單));

            if (this.Configure.Template == null)
            {
                Configure.Template = new Document(new MemoryStream(Properties.Resources.高中部班級歷年成績單));

            }
            else
            {
                //計算檔案大小
                MemoryStream ms = new MemoryStream();
                this.Configure.Template.Save(ms, SaveFormat.Doc);
                byte[] bb = ms.ToArray();

                double bbSize = (bb.Count() / 1024);

                if (bbSize < 30)
                    Configure.Template = new Document(new MemoryStream(Properties.Resources.高中部班級歷年成績單));
             
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Configure.Save();
            this.Close();
        }

        private void cbxSubjSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvwSubjectPri.Items)
                lvi.Checked = cbxSubjSelectAll.Checked;            
        }

        private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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
            try
            {
                if (this.Configure.Template == null)
                {
                    Configure.Template = new Document(new MemoryStream(Properties.Resources.高中部班級歷年成績單));

                }
                else
                {
                    //計算檔案大小
                    MemoryStream ms = new MemoryStream();
                    this.Configure.Template.Save(ms, SaveFormat.Doc);
                    byte[] bb = ms.ToArray();

                    double bbSize = (bb.Count() / 1024);

                    if(bbSize<30)
                        Configure.Template = new Document(new MemoryStream(Properties.Resources.高中部班級歷年成績單));

                    this.Configure.Template.Save(path, SaveFormat.Doc);
                }

                System.Diagnostics.Process.Start(path);
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
                        //document.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);
                        System.IO.FileStream stream = new FileStream(sd.FileName, FileMode.Create, FileAccess.Write);
                        stream.Write(Properties.Resources.高中部班級歷年成績單, 0, Properties.Resources.高中部班級歷年成績單.Length);
                        stream.Flush();
                        stream.Close();

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
            UploadUserDefTemplate();
        }

        private void UploadUserDefTemplate()
        {
            if (Configure == null) return;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "上傳樣板";
            dialog.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.Configure.Template = new Aspose.Words.Document(dialog.FileName);                   

                    List<string> fields = new List<string>(this.Configure.Template.MailMerge.GetFieldNames());
                    this.Configure.SubjectLimit = 0;
                    while (fields.Contains("科目名稱" + (this.Configure.SubjectLimit + 1)))
                    {
                        this.Configure.SubjectLimit++;
                    }
                    Configure.Encode();

                    Configure.Save();
                    MsgBox.Show("上傳完成.");
                }
                catch
                {
                    MessageBox.Show("樣板開啟失敗");
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
    }
}
