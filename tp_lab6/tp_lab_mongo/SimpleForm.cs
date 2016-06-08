using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tp_lab;

namespace common {
	public partial class TabForm : UserControl {
		public TabForm()
		{
			InitializeComponent();
		}
		
		public void Reset()
		{
			table.Reset();
		}

		public int AddRow(string label, Control ctl)
		{
			return table.AddRow(label, ctl);
		}

		public int AddRow(string name, string label, TableForm.ControlTypes type)
		{
			return table.AddRow(name, label, type);
		}

		public void RemoveRow(int i)
		{
			table.RemoveRow(i);
		}

		public void RemoveRow(string controlName)
		{
			table.RemoveRow(controlName);
		}

		public void ClearRows()
		{
			table.ClearRows();
		}

		public void SetValue(Control ctl, object value)
		{
			table.SetValue(ctl, value);
		}

		public void SetValue(int i, object value)
		{
			table.SetValue(i, value);
		}

		public void SetValue(string name, object value)
		{
			table.SetValue(name, value);
		}

		public object GetValue(Control ctl)
		{
			return table.GetValue(ctl);
		}

		public object GetValue(int i)
		{
			return table.GetValue(i);
		}

		public object GetValue(string name)
		{
			return table.GetValue(name);
		}

		public Control GetControl(int i)
		{
			return table.GetControl(i);
		}

		public Control GetControl(string name)
		{
			return table.GetControl(name);
		}

		public void SubscribeSave(EventHandler a)
		{
			btnSave.Click += a;
		}

		public void UnsubscribeSave(EventHandler a)
		{
			btnSave.Click -= a;
		}

		public void SubscribeCancel(EventHandler a)
		{
			btnCancel.Click += a;
		}

		public void UnsubscribeCancel(EventHandler a)
		{
			btnCancel.Click -= a;
		}

		public TabControl Tabs
		{
			get { return mTabs; }
		}

		public bool EnableTabs
		{
			get
			{
				return mTabs.Parent==this && table.Parent==mTabs.TabPages[0];
			}

			set
			{
				if (value) {
					table.Parent = mTabs.TabPages[0];
					mTabs.Parent = this;
				} else {
					mTabs.Parent = null;
					table.Parent = this;
				}
			}
		}
	}
}
