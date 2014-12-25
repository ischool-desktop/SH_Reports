using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using Framework;
using FISCA.UDT;
using System.Dynamic;
using System.Threading.Tasks;

namespace SH_DaliSemesterReportEnglishGPA
{
    public partial class ScoreDegreeMapping : BaseForm
    {
        //    記錄所有 UDT 資料，便於比對待刪除資料
        private Dictionary<string, UDT.ScoreDegreeMapping> dicOriginalScoreDegreeMappings;
        private ChangeListener _Listener_dgvData;
        private AccessHelper Access;

        public ScoreDegreeMapping()
        {
            InitializeComponent();

            this.Load += new EventHandler(ScoreDegreeMapping_Load);
        }

        private void ScoreDegreeMapping_Load(object sender, EventArgs e)
        {
            this.dgvData.DataError += new DataGridViewDataErrorEventHandler(dgvData_DataError);
            this.dgvData.CurrentCellDirtyStateChanged += new EventHandler(dgvData_CurrentCellDirtyStateChanged);
            this.dgvData.CellEnter += new DataGridViewCellEventHandler(dgvData_CellEnter);
            this.dgvData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_ColumnHeaderMouseClick);
            this.dgvData.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_RowHeaderMouseClick);
            this.dgvData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseClick);

            this.Access = new AccessHelper();
            this.dicOriginalScoreDegreeMappings = new Dictionary<string, UDT.ScoreDegreeMapping>();

            _Listener_dgvData = new ChangeListener();
            _Listener_dgvData.Add(new DataGridViewSource(this.dgvData));
            _Listener_dgvData.StatusChanged += new EventHandler<ChangeEventArgs>(_Listener_dgvData_StatusChanged);

            this.FillData();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void _Listener_dgvData_StatusChanged(object sender, ChangeEventArgs e)
        {
            this.ChangeStudentFeeModifyStatus(e.Status == ValueStatus.Dirty);
        }

        private void ChangeStudentFeeModifyStatus(bool Modified)
        {
            this.Save.Enabled = true;
            this.Exit.Enabled = true;
        }

        private void ValidateData()
        {
            decimal d;
            List<dynamic> uniqueData = new List<dynamic>();
            foreach (DataGridViewRow rows in this.dgvData.Rows)
            {
                if (rows.IsNewRow)
                    continue;

                foreach (DataGridViewCell cell in rows.Cells)
                {
                    cell.ErrorText = string.Empty;

                    if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                        cell.ErrorText = "必填。";

                    if (cell.ColumnIndex == 0 || cell.ColumnIndex == 1)
                    {
                        if (cell.Value != null)
                        {
                            if (!decimal.TryParse(cell.Value.ToString(), out d) || !(d >= 0 && d <= 100))
                                cell.ErrorText = "「最小值」與「最大值」僅允許 0~100。";
                        }
                    }
                }

                if (rows.Cells[0].Value != null && rows.Cells[1].Value != null)
                {
                    if (decimal.TryParse(rows.Cells[0].Value.ToString(), out d) && decimal.TryParse(rows.Cells[1].Value.ToString(), out d))
                    {
                        dynamic o = new ExpandoObject();
                        o.MinScore = decimal.Parse(rows.Cells[0].Value.ToString());
                        o.MaxScore = decimal.Parse(rows.Cells[1].Value.ToString());
                        o.Row = rows;
                        uniqueData.Add(o);
                        if (o.MinScore >= o.MaxScore)
                        {
                            rows.Cells[0].ErrorText = "最小值不得大於或等於最大值。";
                            rows.Cells[1].ErrorText = "最大值不得小於或等於最小值。";
                        }
                    }
                }
            }
            foreach (dynamic o in uniqueData)
            {
                IEnumerable<dynamic> duplicateData = uniqueData.Where(x => (x.MinScore <= o.MinScore && x.MaxScore >= o.MaxScore));
                if (duplicateData.Count() > 1)
                {
                    foreach (dynamic vk in duplicateData)
                    {
                        vk.Row.Cells[0].ErrorText = "「最小值」～「最大值」區間重疊。";
                        vk.Row.Cells[1].ErrorText = "「最小值」～「最大值」區間重疊。";
                    }
                }
            }
        }

        private void dgvData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvData.CurrentCell == null)
                return;
            dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.ValidateData();
        }

        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvData.SelectedCells.Count == 1)
            {
                dgvData.BeginEdit(true);
            }
        }

        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvData.CurrentCell = null;
            dgvData.Rows[e.RowIndex].Selected = true;
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvData.CurrentCell = null;
            dgvData.Columns[e.ColumnIndex].Selected = true;
        }

        private void dgvData_MouseClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            DataGridView dgv = (DataGridView)sender;
            DataGridView.HitTestInfo hit = dgv.HitTest(args.X, args.Y);

            if (hit.Type == DataGridViewHitTestType.TopLeftHeader)
            {
                dgvData.CurrentCell = null;
                dgvData.SelectAll();
            }
        }

        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void FillData()
        {
            this.dicOriginalScoreDegreeMappings = new Dictionary<string, UDT.ScoreDegreeMapping>();
            this._Listener_dgvData.Reset();
            this._Listener_dgvData.SuspendListen();
            this.dgvData.Rows.Clear();
            //new Task(() =>
            //{
            List<UDT.ScoreDegreeMapping> udts = Access.Select<UDT.ScoreDegreeMapping>().OrderBy(x => x.MinScore).ToList();

            for (int i = 0; i < udts.Count; i++)
            {
                List<object> list = new List<object>();

                list.Add(udts[i].MinScore);
                list.Add(udts[i].MaxScore);
                list.Add(udts[i].Degree);

                dicOriginalScoreDegreeMappings.Add(udts[i].UID, udts[i]);

                int idx = this.dgvData.Rows.Add(list.ToArray());
                this.dgvData.Rows[idx].Tag = udts[i];
            }
            //this.dgvData.AutoResizeColumns();
            this.dgvData.CurrentCell = null;
            this.ChangeStudentFeeModifyStatus(false);
            this._Listener_dgvData.ResumeListen();
            //}).Start();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save.Enabled = false;
                this.Exit.Enabled = false;
                foreach (DataGridViewRow rows in this.dgvData.Rows)
                {
                    if (rows.IsNewRow)
                        continue;

                    foreach (DataGridViewCell cell in rows.Cells)
                    {
                        if (cell.ErrorText != string.Empty)
                        {
                            MessageBox.Show("請先修正錯誤再儲存！");
                            return;
                        }
                    }
                }

                // 比對原始資料與修改後資料，刪除差集資料          
                foreach (DataGridViewRow dataGridRow in this.dgvData.Rows)
                {
                    if (dataGridRow.IsNewRow)
                        continue;

                    UDT.ScoreDegreeMapping udt = (UDT.ScoreDegreeMapping)dataGridRow.Tag;
                    if (udt != null && this.dicOriginalScoreDegreeMappings.ContainsKey(udt.UID))
                        this.dicOriginalScoreDegreeMappings.Remove(udt.UID);
                }
                foreach (KeyValuePair<string, UDT.ScoreDegreeMapping> kv in dicOriginalScoreDegreeMappings)
                {
                    kv.Value.Deleted = true;
                }
                dicOriginalScoreDegreeMappings.Values.ToList().SaveAll();

                // 更新修改後的資料
                List<UDT.ScoreDegreeMapping> nRecords = new List<UDT.ScoreDegreeMapping>();
                foreach (DataGridViewRow dataGridRow in this.dgvData.Rows)
                {
                    if (dataGridRow.IsNewRow)
                        continue;

                    UDT.ScoreDegreeMapping nRecord = (UDT.ScoreDegreeMapping)dataGridRow.Tag;

                    //  不允許在 DataGridView 中直接新增記錄，欲增加學生繳費明細，請按「加入學生」
                    if (nRecord == null)
                    {
                        nRecord = new UDT.ScoreDegreeMapping();
                    }

                    nRecord.MinScore = decimal.Parse(dataGridRow.Cells[0].Value + "");
                    nRecord.MaxScore = decimal.Parse(dataGridRow.Cells[1].Value + "");
                    nRecord.Degree = dataGridRow.Cells[2].Value + "";

                    nRecords.Add(nRecord);
                }
                if (nRecords.Count > 0)
                    nRecords.SaveAll();
                MessageBox.Show("儲存成功。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.FillData();
            }
        }
    }
}
