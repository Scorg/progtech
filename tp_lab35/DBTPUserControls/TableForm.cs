using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tp_lab {
	public partial class TableForm : UserControl {
		public enum ControlTypes {
			TextBox,
			ComboBox,
			NumericUpDown,
			DateTimePicker,
			RadioButton,
			CheckBox
		}

		public TableForm()
		{
			InitializeComponent();
		}

		public void Reset()
		{
			for (int i=0; i<table.RowCount; ++i) {
				Control c = table.GetControlFromPosition(1, i);
				if (c==null) continue;

				if (c is TextBox) {
					c.Text = "";
					continue;
				}

				if (c is ComboBox) {
					ComboBox cb = (ComboBox)c;
					cb.DataSource = null;
					cb.SelectedIndex = -1;
					continue;
				}

				if (c is NumericUpDown) {
					continue;
				}

				if (c is CheckBox) {
					((CheckBox)c).Checked = false;
					continue;
				}

				if (c is RadioButton) {
					((RadioButton)c).Checked = false;
					continue;
				}
			}
		}

		public int AddRow(string label, Control ctl)
		{
			int newrow = table.GetControlFromPosition(1, table.RowCount-1)!=null ? table.RowCount : table.RowCount-1;
			table.RowCount = newrow+1;

			Label lbl = new Label();
			lbl.Name = ctl.Name + "_lbl";
			lbl.Text = label;
			lbl.AutoSize = true;

			table.Controls.Add(lbl, 0, newrow);
			table.Controls.Add(ctl, 1, newrow);

			lbl.Anchor = AnchorStyles.Right;
			ctl.Dock = DockStyle.Fill;

			return newrow;
		}

		public int AddRow(string name, string label, ControlTypes type)
		{
			Control ctl=null;

			switch (type) {
				case ControlTypes.TextBox:
					ctl = new TextBox();
					break;

				case ControlTypes.ComboBox:
					ctl = new ComboBox();
					break;

				case ControlTypes.DateTimePicker:
					ctl = new DateTimePicker();
					break;

				case ControlTypes.NumericUpDown:
					ctl = new NumericUpDown();
					break;

				case ControlTypes.RadioButton:
					ctl = new RadioButton();
					break;

				case ControlTypes.CheckBox:
					ctl = new CheckBox();
					break;

				default:
					throw new NotImplementedException();
			}

			ctl.Name=name;
			return AddRow(label, ctl);
		}

		public void RemoveRow(int i)
		{
			if (i<0 || i>=table.RowCount) throw new ArgumentOutOfRangeException();

			table.Controls.Remove(table.GetControlFromPosition(0, i));
			table.Controls.Remove(table.GetControlFromPosition(1, i));

			for (int k=i; k<table.RowCount-1; ++k) {
				table.SetRow(table.GetControlFromPosition(0, k+1), k);
				table.SetRow(table.GetControlFromPosition(1, k+1), k);
			}
		}

		public void RemoveRow(string controlName)
		{
			RemoveRow(table.GetRow(table.Controls[controlName]));
		}

		public void ClearRows()
		{
			table.Controls.Clear();
			table.RowCount = 1;
		}

		public void SetValue(Control ctl, object value)
		{
			if (ctl.Parent!=table) throw new ArgumentException("Переданный элемент управления не принадлежит данной форме");

			Dictionary<Type, Action> dic = new Dictionary<Type,Action>();
			dic[typeof(TextBox)] = () => ((TextBox)ctl).Text = (string)value;
			dic[typeof(ComboBox)] = () => { if (value!=null) ((ComboBox)ctl).SelectedValue = value; else ((ComboBox)ctl).SelectedIndex = -1; };
			dic[typeof(NumericUpDown)] = () => ((NumericUpDown)ctl).Value = (decimal)value;
			dic[typeof(DateTimePicker)] = () => ((DateTimePicker)ctl).Value = (DateTime)value;
			dic[typeof(RadioButton)] = () => ((RadioButton)ctl).Checked = (bool)value;
			dic[typeof(CheckBox)] = () => ((CheckBox)ctl).Checked = (bool)value;

			Action f;
			if (dic.TryGetValue(ctl.GetType(), out f)) f();
		}

		/*public void SetValue<T>(Control ctl, T value) 
		{
			if (ctl.Parent!=table) throw new ArgumentException("Переданный элемент управления не принадлежит данной форме");

			Dictionary<Type, Action> dic = new Dictionary<Type,Action>();
			dic[typeof(TextBox)] = () => ((TextBox)ctl).Text = (string)value;
			dic[typeof(ComboBox)] = () => { if (value!=null) ((ComboBox)ctl).SelectedValue = value; else ((ComboBox)ctl).SelectedIndex = -1; };
			dic[typeof(NumericUpDown)] = () => ((NumericUpDown)ctl).Value = (decimal)value;
			dic[typeof(DateTimePicker)] = () => ((DateTimePicker)ctl).Value = (DateTime)value;
			dic[typeof(RadioButton)] = () => ((RadioButton)ctl).Checked = (bool)value;
			dic[typeof(CheckBox)] = () => ((CheckBox)ctl).Checked = (bool)value;

			Action f;
			if (dic.TryGetValue(ctl.GetType(), out f)) f();
		}*/

		public void SetValue(int i, object value)
		{
			SetValue(GetControl(i), value);
		}

		public void SetValue(string name, object value)
		{
			SetValue(GetControl(name), value);
		}

		public object GetValue(Control ctl)
		{
			if (ctl.Parent!=table) throw new ArgumentException("Переданный элемент управления не принадлежит данной форме");

			Dictionary<Type, Func<object>> dic = new Dictionary<Type,Func<object>>();
			dic[typeof(TextBox)] = () => {return ((TextBox)ctl).Text;};
			dic[typeof(ComboBox)] = () => {return ((ComboBox)ctl).SelectedValue;};
			dic[typeof(NumericUpDown)] = () => {return ((NumericUpDown)ctl).Value;};
			dic[typeof(DateTimePicker)] = () => {return ((DateTimePicker)ctl).Value;};
			dic[typeof(RadioButton)] = () => {return ((RadioButton)ctl).Checked;};
			dic[typeof(CheckBox)] = () => { return ((CheckBox)ctl).Checked; };

			Func<object> f;
			if (dic.TryGetValue(ctl.GetType(), out f)) return f();

			return null;
		}

		public T GetValue<T>(Control ctl)
		{
			if (ctl.Parent!=table) throw new ArgumentException("Переданный элемент управления не принадлежит данной форме");

			Dictionary<Type, Func<object>> dic = new Dictionary<Type,Func<object>>();
			dic[typeof(TextBox)] = () => {return ((TextBox)ctl).Text;};
			dic[typeof(ComboBox)] = () => {return ((ComboBox)ctl).SelectedValue;};
			dic[typeof(NumericUpDown)] = () => {return ((NumericUpDown)ctl).Value;};
			dic[typeof(DateTimePicker)] = () => {return ((DateTimePicker)ctl).Value;};
			dic[typeof(RadioButton)] = () => {return ((RadioButton)ctl).Checked;};
			dic[typeof(CheckBox)] = () => { return ((CheckBox)ctl).Checked; };

			Func<object> f;
			if (dic.TryGetValue(ctl.GetType(), out f)) return (T)f();

			return default(T);
		}

		public object GetValue(int i)
		{
			return GetValue(GetControl(i));
		}

		public object GetValue(string name)
		{
			return GetValue(GetControl(name));
		}

		public T GetValue<T>(string name)
		{
			return GetValue<T>(GetControl(name));
		}

		public Control GetControl(int i)
		{
			return table.GetControlFromPosition(1, i);
		}

		public Control GetControl(string name)
		{
			return table.Controls[name];
		}

		public TControl GetControl<TControl>(string name) where TControl : Control
		{
			return GetControl(name) as TControl;
		}

		public void SetVisible(string name, bool v)
		{
			table.Controls[name].Visible = v;
			table.Controls[name+"_lbl"].Visible = v;
		}
	}
}
