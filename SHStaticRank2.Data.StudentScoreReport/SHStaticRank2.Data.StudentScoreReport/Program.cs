using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.Customization.Data;
using System.IO;
using FISCA.DSAUtil;
using SHStaticRank2.Data;

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
        }
    }
}
