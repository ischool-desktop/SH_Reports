using FISCA.UDT;
using System;

namespace SH_DaliSemesterReportEnglishGPA.UDT
{
    [FISCA.UDT.TableName("ischool.mapping.score_degree_gpa")]
    public class ScoreDegreeMapping : ActiveRecord
    {
        internal static void RaiseAfterUpdateEvent(object sender, EventArgs e)
        {
            if (ScoreDegreeMapping.AfterUpdate != null)
                ScoreDegreeMapping.AfterUpdate(sender, e);
        }

        internal static event EventHandler<EventArgs> AfterUpdate;

        /// <summary>
        /// 成績區間最小值
        /// </summary>
        [Field(Field = "min_score", Indexed = false)]
        public decimal MinScore { get; set; }

        /// <summary>
        /// 成績區間最大值
        /// </summary>
        [Field(Field = "max_score", Indexed = false)]
        public decimal MaxScore { get; set; }

        /// <summary>
        /// 等第
        /// </summary>
        [Field(Field = "degree", Indexed = false)]
        public string Degree { get; set; }

        public ScoreDegreeMapping Clone()
        {
            return (this.MemberwiseClone() as ScoreDegreeMapping);
        }
    }
}