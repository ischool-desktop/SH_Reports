using FISCA.UDT;
using System;

namespace SH_DaliSemesterReportEnglishGPA.UDT
{
    [FISCA.UDT.TableName("ischool.mapping.nationality")]
    public class NationalityMapping : ActiveRecord
    {
        internal static void RaiseAfterUpdateEvent(object sender, EventArgs e)
        {
            if (NationalityMapping.AfterUpdate != null)
                NationalityMapping.AfterUpdate(sender, e);
        }

        internal static event EventHandler<EventArgs> AfterUpdate;

        /// <summary>
        /// 國籍
        /// </summary>
        [Field(Field = "name", Indexed = true)]
        public string Name { get; set; }

        /// <summary>
        /// 國籍英文名稱
        /// </summary>
        [Field(Field = "eng_name", Indexed = false)]
        public string EnglishName { get; set; }

        public NationalityMapping Clone()
        {
            return (this.MemberwiseClone() as NationalityMapping);
        }
    }
}