using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SHStudentScoreAvgReport.DAO
{
    /// <summary>
    /// 計算成績
    /// </summary>
    public class Cal
    {
        /// <summary>
        /// 計算來自分項成績平均
        /// </summary>
        /// <param name="EntryName"></param>
        /// <param name="rowData"></param>        
        /// <param name="isBetter"></param>
        /// <returns></returns>
        public static decimal CalEntryAvgScore(string EntryName, List<DataRow> rowData, bool isBetter)
        {
            decimal retVal = 0;
            string key = EntryName;
            if (isBetter == false)
                key += "(原始)";
            decimal sum = 0, count = 0;
            foreach (DataRow dr in rowData)
            {
                decimal sc;
                if (dr["分項"].ToString() == key)
                {
                    if (decimal.TryParse(dr["成績"].ToString(), out sc))
                    {
                        sum += sc;
                        count++;
                    }
                
                }            
            }
            if(count>0)
                retVal=Math.Round((sum/count),1,MidpointRounding.AwayFromZero);

            return retVal;
        }

        /// <summary>
        /// 計算來自科目加權平均
        /// </summary>
        /// <param name="SubjList"></param>
        /// <param name="rowData"></param>
        /// <param name="isBetter"></param>
        /// <returns></returns>
        public static decimal CalSubjGroupAvgScore(List<string> SubjList, List<DataRow> rowData, bool isBetter)
        {
            decimal retVal = 0;
            decimal sum = 0, credit = 0;

            if (isBetter)
            {
                // 擇優
                foreach (DataRow dr in rowData)
                {
                    string keyName = dr["學期科目名稱"].ToString().Replace(" ", "");
                    if (SubjList.Contains(keyName))
                    {
                        decimal sc,sc2,sc3,sc4,sc5, cc;
                        
                        decimal.TryParse(dr["學期科目原始成績"].ToString(), out sc);
                        decimal.TryParse(dr["學期科目學年調整成績"].ToString(), out sc2);
                        decimal.TryParse(dr["學期科目擇優採計成績"].ToString(), out sc3);
                        decimal.TryParse(dr["學期科目補考成績"].ToString(), out sc4);
                        decimal.TryParse(dr["學期科目重修成績"].ToString(), out sc5);

                        if (sc2 > sc) sc = sc2;
                        if (sc3 > sc) sc = sc3;
                        if (sc4 > sc) sc = sc4;
                        if (sc5 > sc) sc = sc5;

                            if (decimal.TryParse(dr["學期科目開課學分數"].ToString(), out cc))
                            {
                                sum += sc * cc;
                                credit += cc;
                            }
                    }
                }
            }
            else
            {
                foreach (DataRow dr in rowData)
                {
                    string keyName = dr["學期科目名稱"].ToString().Replace(" ", "");
                    if (SubjList.Contains(keyName))
                    {
                        decimal sc,cc;
                        
                        if (decimal.TryParse(dr["學期科目原始成績"].ToString(), out sc))
                        if(decimal.TryParse(dr["學期科目開課學分數"].ToString(),out cc))
                        {
                            sum += sc * cc;
                            credit += cc;
                        }
                        
                    }
                }            
            }

            if (credit > 0)
                retVal = Math.Round(sum / credit, 1, MidpointRounding.AwayFromZero);

            return retVal;
        }

        /// <summary>
        /// 排名暫存用
        /// </summary>
        public static Dictionary<string, List<decimal>> _RanksTmp = new Dictionary<string, List<decimal>>();
    }
}
