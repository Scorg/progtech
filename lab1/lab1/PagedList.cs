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
	public partial class PagedList : UserControl {
		uint currentPage;
		uint totalPages;
		uint pageSize;

		public PagedList()
		{
			InitializeComponent();

			currentPage=0;
			totalPages=uint.MaxValue;
			pageSize=20;
		}

		public void RefreshControls()
		{
			btnEdit.Enabled = dgv.SelectedRows.Count==1;
			btnDelete.Enabled = dgv.SelectedRows.Count>0;
		}


		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!"0123456789\b".Contains(e.KeyChar)) {
				e.Handled = true;
			}
		}

		public event EventHandler AddItem;
		public event EventHandler EditItem;
		public event EventHandler DeleteItems;
		public event EventHandler PrevPage;
		public event EventHandler NextPage;
		public event EventHandler PageNumberEntered;

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (AddItem!=null) AddItem(this, new EventArgs());
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DeleteItems!=null) DeleteItems(this, new EventArgs());
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (EditItem!=null) EditItem(this, new EventArgs());
		}

		private void btnPrevPage_Click(object sender, EventArgs e)
		{
			if (PrevPage!=null) PrevPage(this, new EventArgs());
		}

		private void btnNextPage_Click(object sender, EventArgs e)
		{
			if (NextPage!=null) NextPage(this, new EventArgs());
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode==Keys.Enter) {
				uint val = uint.Parse(tbPageNumber.Text);

				if (tbPageNumber.Text=="" || val==0) {
					tbPageNumber.Text = (currentPage+1).ToString();
				} else {
					currentPage = val-1;
				}

				if (PageNumberEntered!=null) PageNumberEntered(this, new EventArgs());
			}
		}

		public DataGridView DataGrid
		{
			get { return dgv; }
		}

		public uint CurrentPage
		{
			set {
				if (value>=0) {
					currentPage = value;
					tbPageNumber.Text = (value+1).ToString();
				}
			}
			get { return currentPage; }
		}

		public uint TotalPages
		{
			set { totalPages = value; }
			get { return totalPages; }
		}

		public uint PageSize
		{
			set { pageSize = value; }
			get { return pageSize; }
		}
	}
}
