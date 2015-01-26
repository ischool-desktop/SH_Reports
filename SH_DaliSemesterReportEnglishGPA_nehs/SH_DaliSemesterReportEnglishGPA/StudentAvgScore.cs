using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SH_DaliSemesterReportEnglishGPA_nehs
{
    public class StudentAvgScore
    {
        public string StudentID { get; set; }

        public string Name { get; set; }

        public string StudentNumber { get; set; }

        public string SeatNo { get; set; }

        public string ClassName { get; set; }

        /// <summary>
        /// 學業總平均(算數)
        /// </summary>
        public decimal AvgSSScore { get; set; }
    }
}
