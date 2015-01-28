using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.Customization.Data;
using System.IO;
using FISCA.DSAUtil;
using SHStaticRank2.Data;
using System.Data;
using Aspose.Words;

namespace SHStaticRank2.Data.StudentScoreReport
{
    public class Program
    {
        [FISCA.MainMethod]
        public static void Main()
        {

            var button = FISCA.Presentation.MotherForm.RibbonBarItems["教務作業", "批次作業/檢視"]["成績作業"]["計算固定排名(測試版)"]["計算多學期成績固定排名(學生個人歷年成績單)"];
            button.Enable = FISCA.Permission.UserAcl.Current["SHSchool.SHStaticRank2.Data"].Executable;
            button.Click += delegate
            {
                var conf = new StudentScoreReport();
                conf.ShowDialog();
                if (conf.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    CalcMutilSemeSubjectRank.Setup(conf.Configure);
                }
            };


            var buttonClass = FISCA.Presentation.MotherForm.RibbonBarItems["教務作業", "批次作業/檢視"]["成績作業"]["計算固定排名(測試版)"]["計算多學期成績固定排名(班級歷年成績單)"];
            buttonClass.Enable = FISCA.Permission.UserAcl.Current["SHSchool.SHStaticRank2.Data"].Executable;
            
            buttonClass.Click += delegate
            {
                bool chkClass = true;
                var confClass = new ClassScoreReport();
                confClass.ShowDialog();
                if (confClass.DialogResult == System.Windows.Forms.DialogResult.OK)
                {                    
                    CalcMutilSemeSubjectRank.Setup(confClass.Configure);
                    CalcMutilSemeSubjectRank.OneClassCompleted += delegate
                    {

                        if (confClass.Configure.CheckExportStudent == false && chkClass)
                        {

                            DataTable students = CalcMutilSemeSubjectRank._table.Copy();

                            if (students.Rows.Count <= 0)
                                return;

                            Dictionary<string, string> classData = new Dictionary<string, string>();
                            classData.Add("學年度", K12.Data.School.DefaultSchoolYear);
                            classData.Add("N學期", "多");
                            classData.Add("學校名稱", students.Rows[0]["學校名稱"] + "");
                            classData.Add("科別", students.Rows[0]["科別"] + "");
                            classData.Add("班級", students.Rows[0]["班級"] + "");
                            classData.Add("科目名稱1", students.Rows[0]["科目名稱1"] + "");

                            SingleClassMailMerge classm = new SingleClassMailMerge(classData, students);
                            Document doc;
                            if (confClass.Configure.Template == null)
                            {
                                doc = new Document(new MemoryStream(Properties.Resources.高中部班級歷年成績單));
                            }
                            else
                            {
                                doc = confClass.Configure.Template;
                            }
                            
                            doc.MailMerge.ExecuteWithRegions(classm);

                            string pathW = Path.Combine(System.Windows.Forms.Application.StartupPath, "Reports", CalcMutilSemeSubjectRank.FolderName);
                            doc.Save(Path.Combine(pathW, classData["班級"]) + ".docx", SaveFormat.Docx);
                        }
                    };
                }
            };

        }
    }
}
