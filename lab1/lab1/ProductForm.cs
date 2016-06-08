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
	public partial class ProductForm : UserControl {
		public ProductForm()
		{
			InitializeComponent();			
		}

		public string ShopProductName
		{
			set { tbName.Text = value; }
			get { return tbName.Text; }
		}

		public Decimal ProductPrice
		{
			get 
			{
				Decimal tmp = 0m;
				if (Decimal.TryParse(tbPrice.Text, out tmp)) return tmp;
				return 0.0m; 
			}

			set
			{
				if (value >= 0.0m) {
					tbPrice.Text = value.ToString("N2");
				}
			}
		}

		public ComboboxItem ProductCategory
		{
			get { return (ComboboxItem)cbCategory.SelectedItem; }
			set
			{
				if (value!=null && !cbCategory.Items.Contains(value)) {
					cbCategory.Items.Add(value);
				}

				cbCategory.Text = "";
				cbCategory.SelectedItem = value;
			}
		}

		public ComboBox ComboBoxCategory
		{
			get { return cbCategory; }
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

		private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!("0123456789\b"+System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator).Contains(e.KeyChar)) {
				e.Handled = true;
			}
		}
	}
}
