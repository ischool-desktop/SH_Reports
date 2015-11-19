using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SH_DaliSemesterReportEnglishGPA_nehs
{
    public class Program
    { /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        //[MainMethod()]
        [FISCA.MainMethod("")]
        static public void Main()
        {
            SemesterTranscriptsGPA report = new SemesterTranscriptsGPA();
            //修改公版英文成績單的名稱
            //MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["成績相關報表"]["英文成績單(GPA)"].Visible = false;
            //MotherForm.RibbonBarItems["班級", "資料統計"]["報表"]["成績相關報表"]["英文成績單(GPA)"].Visible = false;

            MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["成績相關報表"]["英文成績單(GPA)"].Text = "英文成績單";
            MotherForm.RibbonBarItems["班級", "資料統計"]["報表"]["成績相關報表"]["英文成績單(GPA)"].Text = "英文成績單";
            
        }
    }
}
