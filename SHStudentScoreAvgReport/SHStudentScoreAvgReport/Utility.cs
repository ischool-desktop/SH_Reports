using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHStudentScoreAvgReport.DAO;
using FISCA.Data;
using System.Data;
using Aspose.Cells;
using System.IO;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace SHStudentScoreAvgReport
{
    public class Utility
    {
        /// <summary>
        /// 取得班級學生
        /// </summary>
        /// <param name="ClassIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string,StudentScore>> GetClassStudentDict(List<string> ClassIDList)
        {
            Dictionary<string, Dictionary<string, StudentScore>> retVal = new Dictionary<string, Dictionary<string, StudentScore>>();
            if(ClassIDList.Count>0)
            {
                // -- ClassID,StudentID,ClassName,StudentName,SeatNo
                string strSQL = "select class.id as classid,student.id as studentid,class_name as classname,student.name as studentname,student.seat_no from student inner join class on student.ref_class_id=class.id where student.status=1 and class.id in(" + string.Join(",", ClassIDList.ToArray()) + ") order by class_name,student.seat_no";

                QueryHelper qh = new QueryHelper();
                DataTable dt = qh.Select(strSQL);

                foreach (DataRow dr in dt.Rows)
                {
                    StudentScore ss = new StudentScore();
                    ss.ClassID = dr["classid"].ToString();
                    ss.ClassName = dr["classname"].ToString();
                    ss.Name = dr["studentname"].ToString();
                    ss.StudentID = dr["studentid"].ToString();
                    ss.SeatNo = dr["seat_no"].ToString();

                    if (!retVal.ContainsKey(ss.ClassName))
                        retVal.Add(ss.ClassName, new Dictionary<string, StudentScore>());

                    if (!retVal[ss.ClassName].ContainsKey(ss.StudentID))
                        retVal[ss.ClassName].Add(ss.StudentID, ss);
                }
            }
            return retVal;
        }

        /// <summary>
        /// 匯出 Excel
        /// </summary>
        /// <param name="inputReportName"></param>
        /// <param name="inputXls"></param>
        public static void CompletedXls(string inputReportName, Workbook inputXls)
        {
            string reportName = inputReportName;

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook wb = inputXls;

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
                wb.Save(path, SaveFormat.Excel97To2003);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, SaveFormat.Excel97To2003);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// 取得學生科目成績
        /// </summary>
        /// <param name="sids"></param>
        /// <returns></returns>
        public static Dictionary<string, List<DataRow>> GetStudentSemsSubjScoreByStudentID(List<string> sids)
        {
            Dictionary<string, List<DataRow>> retVal = new Dictionary<string, List<DataRow>>();
            DataTable dt = new DataTable();
            QueryHelper qh = new QueryHelper();
            if (sids.Count > 0)
            {
                string query = @"select sems_subj_score.id,sems_subj_score.ref_student_id as sid,
sems_subj_score.school_year as 學期科目成績學年度,
sems_subj_score.semester as 學期科目成績學期,
sems_subj_score.grade_year as 學期科目成績年級,
s0.d1 as 學期科目名稱,
s0.d2 as 學期科目級別,
s0.d3 as 學期科目不計學分,
s0.d4 as 學期科目不需評分,
s0.d5 as 學期科目修課必選修,
s0.d6 as 學期科目修課校部訂,
s0.d10 as 學期科目是否取得學分,
CAST(regexp_replace(s0.d7, '^$', '0') as decimal) as 學期科目原始成績,
CAST(regexp_replace(s0.d8, '^$', '0') as decimal) as 學期科目學年調整成績,
CAST(regexp_replace(s0.d9, '^$', '0') as decimal) as 學期科目擇優採計成績,
CAST(regexp_replace(s0.d11, '^$', '0') as decimal) as 學期科目補考成績,
CAST(regexp_replace(s0.d12, '^$', '0') as decimal) as 學期科目重修成績,
s0.d13 as 學期科目開課分項類別,
CAST(regexp_replace(s0.d14, '^$', '0') as decimal) as 學期科目開課學分數 from sems_subj_score inner join xpath_table('id','score_info','sems_subj_score','/SemesterSubjectScoreInfo/Subject/@科目
|/SemesterSubjectScoreInfo/Subject/@科目級別
|/SemesterSubjectScoreInfo/Subject/@不計學分
|/SemesterSubjectScoreInfo/Subject/@不需評分
|/SemesterSubjectScoreInfo/Subject/@修課必選修
|/SemesterSubjectScoreInfo/Subject/@修課校部訂
|/SemesterSubjectScoreInfo/Subject/@原始成績
|/SemesterSubjectScoreInfo/Subject/@學年調整成績
|/SemesterSubjectScoreInfo/Subject/@擇優採計成績
|/SemesterSubjectScoreInfo/Subject/@是否取得學分
|/SemesterSubjectScoreInfo/Subject/@補考成績
|/SemesterSubjectScoreInfo/Subject/@重修成績
|/SemesterSubjectScoreInfo/Subject/@開課分項類別
|/SemesterSubjectScoreInfo/Subject/@開課學分數'
,'ref_student_id in(" + string.Join(",", sids.ToArray()) + @")')
as s0(id integer,d1 character varying(30),d2 character varying(30),d3 character varying(30),d4 character varying(30),d5 character varying(30),d6 character varying(30),d7 character varying(30),d8 character varying(30),d9 character varying(30),d10 character varying(30),d11 character varying(30),d12 character varying(30),d13 character varying(30),d14 character varying(30)) on sems_subj_score.id=s0.id order by sid,學期科目成績年級 asc,學期科目成績學年度 desc,學期科目成績學期";
                dt = qh.Select(query);


                Dictionary<string, bool> chkSameDict = new Dictionary<string, bool>();

                foreach (DataRow dr in dt.Rows)
                {
                    string sid = dr["sid"].ToString();

                    if (!retVal.ContainsKey(sid))
                        retVal.Add(sid, new List<DataRow>());

                    // 檢查是否有重讀資料sid 年級、學期、科目名稱、科目級別
                    string kk = sid + dr["學期科目成績年級"].ToString() + dr["學期科目成績學期"].ToString() + dr["學期科目名稱"].ToString() + dr["學期科目級別"].ToString();
                    if (!chkSameDict.ContainsKey(kk))
                    {
                        chkSameDict.Add(kk, true);
                        retVal[sid].Add(dr);
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// 取得學生分項成績
        /// </summary>
        /// <param name="sids"></param>
        /// <returns></returns>
        public static Dictionary<string, List<DataRow>> GetStudentSemsEntryScoreByStudentID(List<string> sids)
        {
            Dictionary<string, List<DataRow>> retVal = new Dictionary<string, List<DataRow>>();
            if (sids.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string sKey = string.Join(",", sids.ToArray());
                string query = @"select sems_entry_score.id as seid,ref_student_id as sid,school_year 
as 學年度,semester as 學期,grade_year as 年級,se1.d1 as 分項, cast(regexp_replace(se1.d2, '^$', '0') as decimal) as 成績 
from sems_entry_score inner join xpath_table('id','score_info','sems_entry_score','/SemesterEntryScore/Entry/@分項|/SemesterEntryScore/Entry/@成績',
'ref_student_id in(" + sKey + @")') as se1(id integer,d1 character varying(30),d2 character varying(10)) on sems_entry_score.id=se1.id where sems_entry_score.ref_student_id in(" + sKey + @") and sems_entry_score.entry_group=1 order by sid,年級 asc,學年度 desc,學期 asc";
                DataTable dt = qh.Select(query);

                Dictionary<string, bool> chkSameDict = new Dictionary<string, bool>();

                foreach (DataRow dr in dt.Rows)
                {
                    string sid = dr["sid"].ToString();
                    if (!retVal.ContainsKey(sid))
                        retVal.Add(sid, new List<DataRow>());

                    // 檢查是否有重讀資料sid 年級、學期、分項
                    string kk = sid + dr["年級"].ToString() + dr["學期"].ToString() + dr["分項"].ToString();
                    if (!chkSameDict.ContainsKey(kk))
                    {
                        chkSameDict.Add(kk, true);
                        retVal[sid].Add(dr);
                    }

                }
            }
            return retVal;
        }

    }
}
