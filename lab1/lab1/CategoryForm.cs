using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1 {
	public partial class CategoryForm : UserControl {
		public CategoryForm()
		{
			InitializeComponent();
		}

		public string CategoryName
		{
			set { tbName.Text=value; }
			get { return tbName.Text; }
		}

		public ComboboxItem ParentCategory
		{
			get { return (ComboboxItem)cbParent.SelectedItem; }
			set
			{
				if (value!=null && !cbParent.Items.Contains(value)) {
					cbParent.Items.Add(value);
				}

				cbParent.Text = "";
				cbParent.SelectedItem = value;
			}
		}

		public TextBox TextBoxName
		{
			get { return tbName; }
		}

		public ComboBox ComboBoxParent
		{
			get { return cbParent; }
		}

		public event EventHandler Save;
		public event EventHandler Cancel;

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (Save!=null) Save(this, new EventArgs());
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (Cancel!=null) Cancel(this, new EventArgs());
		}
	}

	public class ComboboxItem {
		public uint id;
		public string name;

		public ComboboxItem(uint id, string name) {
			this.id=id;
			this.name=name;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
