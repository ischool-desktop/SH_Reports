using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Aspose.Cells;
using FISCA.Presentation.Controls;
using K12.Data.Configuration;
using System.Dynamic;
using FISCA.UDT;

namespace SH_DaliSemesterReportEnglishGPA
{
    public partial class FrontForm : FISCA.Presentation.Controls.BaseForm
    {
        private MemoryStream _defaultTemplate1 = new MemoryStream(Properties.Resources.高中英文成績單);  //  預設高中英文成績單(GPA)範本
        private MemoryStream _template1 = null;

        private int _useTemplateNumber = 0;
        private int _useRetakeNumber = 0;
        private int _useUpdateRecordNumber = 0;
        private int _useScoreFormatNumber = 0;
        private byte[] _buffer1 = null;
        private string _text3 = string.Empty;

        ConfigData config;

        private int _optSaveFileType = 0;

        public int SaveFileType
        {
            get { return _optSaveFileType; }
        }

        public int TemplateNumber
        {
            get { return _useTemplateNumber; }
        }

        public int RetakeNumber
        {
            get { return _useRetakeNumber; }
        }

        public int ScoreFormatNumber
        {
            get { return _useScoreFormatNumber; }
        }

        public int UpdateRecordNumber
        {
            get { return _useUpdateRecordNumber; }
        }

        public MemoryStream Template
        {
            get
            {
                switch (_useTemplateNumber)
                {
                    case 1:
                        return _defaultTemplate1;
                    case 2:
                        return _template1;
                    default:
                        return new MemoryStream();
                }
            }
        }

        private bool _error2 = false;

        private int _custodian = 0;
        private int _address = 0;
        private int _phone = 0;

        public string Path
        {
            get { return _text3; }
        }

        public int Custodian
        {
            get { return _custodian; }
        }

        public int Address
        {
            get { return _address; }
        }

        public int Phone
        {
            get { return _phone; }
        }

        private bool _over100 = false;
        public bool AllowMoralScoreOver100
        {
            get { return _over100; }
        }

        #region 自訂標示

        /// <summary>
        /// 後期中等教育核心科目標示
        /// </summary>
        private string _sign_core_subject = "";
        public string SignCoreSubject
        {
            get { return _sign_core_subject; }
        }

        /// <summary>
        /// 綜合高中學程核心課程標示
        /// </summary>
        private string _sign_core_course = "";
        public string SignCoreCourse
        {
            get { return _sign_core_course; }
        }

        /// <summary>
        /// 未取得學分標示
        /// </summary>
        private string _sign_failed = "*";
        public string SignFailed
        {
            get { return _sign_failed; }
        }

        /// <summary>
        /// 學年調整成績標示
        /// </summary>
        private string _sign_school_year_adjust = "";
        public string SignSchoolYearAdjust
        {
            get { return _sign_school_year_adjust; }
        }

        /// <summary>
        /// 手動調整成績標示
        /// </summary>
        private string _sign_manual_adjust = "";
        public string SignManualAdjust
        {
            get { return _sign_manual_adjust; }
        }

        /// <summary>
        /// 補考成績標示
        /// </summary>
        private string _sign_resit = "";
        public string SignResit
        {
            get { return _sign_resit; }
        }

        /// <summary>
        /// 重修成績標示
        /// </summary>
        private string _sign_retake = "";
        public string SignRetake
        {
            get { return _sign_retake; }
        }

        #endregion

        private Dictionary<string, string> _tagList = new Dictionary<string, string>();
        public Dictionary<string, string> TagList
        {
            get { return _tagList; }
        }

        public FrontForm()
        {
            InitializeComponent();

            //string DALMessage = "『";

            //foreach (Assembly Assembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.Equals("OfficialStudentRecordReport2010")))
            //    DALMessage += "版本號：" + Assembly.GetName().Version + " ";

            //DALMessage += "』";

            //this.Text += DALMessage;

            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            this.radioButton5.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);

            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);

            LoadPreference();

            //string path = Path.Combine(Application.StartupPath, @"Reports\學籍表");
            //if (!Directory.Exists(path))
            //    Directory.CreateDirectory(path);

            //ReportDirectory.Text = path;
        }

        private void LoadPreference()
        {
            #region 讀取 Preference

            config = K12.Data.School.Configuration["中華民國高雄市高中英文成績單"];
            if (config != null)
            {
                int no = 0;
                int.TryParse(config["TemplateNumber"], out no);
                _useTemplateNumber = no;

                string customize1 = config["CustomizeTemplate1"];

                if (!string.IsNullOrEmpty(customize1))
                {
                    _buffer1 = Convert.FromBase64String(customize1);
                    _template1 = new MemoryStream(_buffer1);
                }

                _optSaveFileType = 0;
                int.TryParse(config["SaveFileType"], out _optSaveFileType);

                switch (_optSaveFileType)
                {
                    case 1:
                        this.checkBoxX1.Checked = true;
                        break;
                    case 2:
                        this.checkBoxX2.Checked = true;
                        break;
                    case 3:
                        this.checkBoxX3.Checked = true;
                        break;
                }

                _useRetakeNumber = 0;
                int.TryParse(config["RetakeNumber"], out _useRetakeNumber);

                _useScoreFormatNumber = 0;
                int.TryParse(config["ScoreFormatNumber"], out _useScoreFormatNumber);
                switch (_useScoreFormatNumber)
                {
                    case 1:
                        this.radioButton1.Checked = true;
                        break;
                    case 2:
                        this.radioButton2.Checked = true;
                        break;
                    case 5:
                        this.radioButton5.Checked = true;
                        break;
                }

                _useUpdateRecordNumber = 0;
                int.TryParse(config["UpdateRecordNumber"], out _useUpdateRecordNumber);
                switch (_useUpdateRecordNumber)
                {
                    case 3:
                        this.radioButton3.Checked = true;
                        break;
                    case 4:
                        this.radioButton4.Checked = true;
                        break;
                }

                _sign_resit = config["ResitSign"];
                _sign_retake = config["RetakeSign"];
                _sign_failed = config["FailedSign"];
                _sign_school_year_adjust = config["SchoolYearAdjustSign"];
                _sign_manual_adjust = config["ManualAdjustSign"];
            }
            #endregion
        }

        private void SavePreference()
        {
            #region 儲存Preference

            if (config != null)
            {
                config["SaveFileType"] = _optSaveFileType.ToString();
                config["ScoreFormatNumber"] = _useScoreFormatNumber.ToString();
                config["UpdateRecordNumber"] = _useUpdateRecordNumber.ToString();
            }
            config.Save();

            #endregion
        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (_error2)
                return;

            if (this.ScoreFormatNumber == 0)
            {
                MsgBox.Show("請設定「成績格式」為「分數」或「等第」！");
                return;
            }

            if (this.TemplateNumber == 0)
            {
                MsgBox.Show("請按「範本設定」，選擇樣版！");
                return;
            }

            if (this.UpdateRecordNumber == 0)
            {
                MsgBox.Show("請設定「異動代碼」為「高(中)職格式」或「進修學校格式」！");
                return;
            }

            if (this.radioButton2.Checked && (new AccessHelper()).Select<UDT.ScoreDegreeMapping>().Count == 0)
            {
                MsgBox.Show("請按「等第GPA對照」，設定成績與等第之對照！");
                return;
            }

            if (this.radioButton5.Checked && (new AccessHelper()).Select<UDT.ScoreDegreeMapping>().Count == 0)
            {
                MsgBox.Show("請按「等第GPA對照」，設定成績與等第之對照！");
                return;
            }


            Workbook wb = new Workbook();
            try
            {
                wb.Open(this.Template, FileFormatType.Excel2003);
            }
            catch
            {
                MsgBox.Show("請上傳相容於 Excel 2003 的範本檔案！");
                return;
            }
            finally
            {
                wb = null;
            }

            if (this.SaveFileType == 0)
            {
                MsgBox.Show("請設定「存檔選項」");
                return;
            }

            System.Windows.Forms.FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                _text3 = folder.SelectedPath;
            }

            SavePreference();

            if (Directory.Exists(_text3))
            {
                //MsgBox.Show("請選擇「學籍表存檔目錄」");
                //return;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("選擇路徑不存在！");
                return;
            }
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxX2.Text))
            {
                _error2 = false;
                int a = 0;

                foreach (char var in textBoxX2.Text.ToCharArray())
                {
                    if (!int.TryParse(var.ToString(), out a))
                    {
                        _error2 = true;
                        break;
                    }
                }

                if (_error2)
                    errorProvider2.SetError(textBoxX2, "格式為數字");
                else
                    errorProvider2.Clear();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SavePreference();
            dynamic o = new ExpandoObject();
            o.TemplateNumber = _useTemplateNumber;                   //  採用範本
            o.RetakeNumber = _useRetakeNumber;                           //  重修成績顯示方式
            o.buffer1 = _buffer1;
            o.ResitSign = _sign_resit;                                                      //  補考符號
            o.RetakeSign = _sign_retake;                                               //  重修符號

            o.FailedSign = _sign_failed;                                                  //  不及格符號
            o.SchoolYearAdjustSign = _sign_school_year_adjust;     //   學年調整成績符號
            o.ManualAdjustSign = _sign_manual_adjust;                   //   手動調整成績符號
            TemplateConfigForm form = new TemplateConfigForm(o);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }

        //  設定成績身份 
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            (new ScoreDegreeMapping()).ShowDialog();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void checkBoxX_CheckedChanged(object sender, EventArgs e)
        {
            _optSaveFileType = Convert.ToInt32(((System.Windows.Forms.RadioButton)sender).Name.Substring((((System.Windows.Forms.RadioButton)sender).Name.Length - 1), 1));
        }
        //  重修成績顯示方式
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            _useScoreFormatNumber = Convert.ToInt32(((System.Windows.Forms.RadioButton)sender).Name.Substring((((System.Windows.Forms.RadioButton)sender).Name.Length - 1), 1));
        }
        //  異動代碼
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _useUpdateRecordNumber = Convert.ToInt32(((System.Windows.Forms.RadioButton)sender).Name.Substring((((System.Windows.Forms.RadioButton)sender).Name.Length - 1), 1));
        }

        private void FrontForm_Load(object sender, EventArgs e)
        {

        }

      
    }
}
