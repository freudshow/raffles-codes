using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DetailInfo
{
    public partial class DateColumn : DataGridViewColumn
    {
        private bool showupdown = true;
        public bool Showupdown 
        { 
            get { return showupdown; } 
            set { showupdown = value; } 
        }
        public DateColumn(): base(new DateCell())
        { } 
        public override DataGridViewCell CellTemplate 
        { 
            get { return base.CellTemplate; }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DateCell)))
                    throw new InvalidCastException("列中只能使用日期单元格");
                base.CellTemplate = value;
            } 
        }
    }
    public partial class DateCell : DataGridViewTextBoxCell
    {
        private DateEdit ne;
        public DateCell() : base()
        { }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            ne = DataGridView.EditingControl as DateEdit;
            //ne.Value = Convert.ToDateTime(this.Value);
            if (ne != null)
            {
                ne.Format = DateTimePickerFormat.Custom;
                ne.CustomFormat = dataGridViewCellStyle.Format;
                ne.ShowUpDown = ((DateColumn)this.OwningColumn).Showupdown;
            }
        }
        public override Type EditType
        {
            get { return typeof(DateEdit); }
        }
        public override Type ValueType
        {
            get { return typeof(DateTime); }
        }
        protected override object GetValue(int rowIndex)
        {
            return base.GetValue(rowIndex);
        }
        public override object DefaultNewRowValue
        {
            get { return DateTime.Now; }
        }
    }
    public partial class DateEdit : DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView grid;
        private bool valuechanged = false;
        int rowindex;
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }
        public DataGridView EditingControlDataGridView
        {
            get { return grid; }
            set { grid = value; }
        }
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if (value != null)
                    this.Value = Convert.ToDateTime(value);
            }
        }
        public int EditingControlRowIndex
        {
            get
            {
                return rowindex;
            }
            set
            {
                rowindex = value;
            }
        }
        public bool EditingControlValueChanged
        {
            get { return valuechanged; }
            set { valuechanged = value; }
        }
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default: return false;
            }
        }
        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }
        public void PrepareEditingControlForEdit(bool selectAll)
        { }
        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }
        protected override void OnValueChanged(EventArgs eventargs)
        {
            valuechanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}