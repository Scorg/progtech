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
	public partial class PagedList : UserControl {
		int currentPage;
		int totalPages;
		int pageSize;

		public PagedList()
		{
			InitializeComponent();

			CurrentPage=0;
			TotalPages=1;
			PageSize=20;

			SubscribeHandlers();
		}

		~PagedList()
		{
			UnsubscribeHandlers();
		}

		void SubscribeHandlers()
		{
			btnAdd.Click += btnAdd_Click;
			btnEdit.Click += btnEdit_Click;
			btnDelete.Click += btnDelete_Click;
			btnPrevPage.Click += btnPrevPage_Click;
			btnNextPage.Click += btnNextPage_Click;
			tbPageNumber.KeyPress += tbPageNumber_KeyPress;
			tbPageNumber.KeyDown += tbPageNumber_KeyDown;
		}

		void UnsubscribeHandlers()
		{
			btnAdd.Click -= btnAdd_Click;
			btnEdit.Click -= btnEdit_Click;
			btnDelete.Click -= btnDelete_Click;
			btnPrevPage.Click -= btnPrevPage_Click;
			btnNextPage.Click -= btnNextPage_Click;
			tbPageNumber.KeyPress -= tbPageNumber_KeyPress;
			tbPageNumber.KeyDown -= tbPageNumber_KeyDown;
		}

		public void RefreshControls()
		{
			btnEdit.Enabled = dgv.SelectedRows.Count==1;
			btnDelete.Enabled = dgv.SelectedRows.Count>0;
		}

		void RefreshNavControls()
		{
			btnPrevPage.Enabled = currentPage>0;
			btnNextPage.Enabled = currentPage+1<totalPages;
		}


		private void tbPageNumber_KeyPress(object sender, KeyPressEventArgs e)
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
			if (AddItem!=null) AddItem(this, e);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DeleteItems!=null) DeleteItems(this, e);
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (EditItem!=null) EditItem(this, e);
		}

		private void btnPrevPage_Click(object sender, EventArgs e)
		{
			if (currentPage>0) {
				CurrentPage--;
				if (PrevPage!=null) PrevPage(this, e);
			}
		}

		private void btnNextPage_Click(object sender, EventArgs e)
		{
			if (currentPage < totalPages-1) {
				CurrentPage++;
				if (NextPage!=null) NextPage(this, e);
			}
		}

		private void tbPageNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode==Keys.Enter) {
				int old = currentPage;
				int val = int.Parse(tbPageNumber.Text);

				if (tbPageNumber.Text=="" || val==0) {
					tbPageNumber.Text = (currentPage+1).ToString();
				} else {
					CurrentPage = val-1;
				}

				if (old!=currentPage) {
					if (PageNumberEntered!=null) PageNumberEntered(this, e);
				}
			}
		}

		public DataGridView DataGrid
		{
			get { return dgv; }
		}

		public int CurrentPage
		{
			set
			{
				if (value>=0 && value<totalPages) {
					currentPage = value;
					tbPageNumber.Text = (value+1).ToString();
				}

				RefreshNavControls();
			}
			get { return currentPage; }
		}

		public int TotalPages
		{
			set
			{
				if (value>0) {
					totalPages = value;
					lblTotalPages.Text = "/" + totalPages.ToString();

					RefreshNavControls();
				}
			}
			get { return totalPages; }
		}

		public int TotalCount
		{
			set
			{
				if (value>=0) {
					TotalPages = (value + pageSize - 1) / pageSize;
				}
			}
		}

		public int PageSize
		{
			set
			{
				if (value>0) {
					pageSize = value;
				}
			}
			get { return pageSize; }
		}

		public bool CreateButtonEnabled
		{
			set { btnAdd.Enabled = value; }
			get { return btnAdd.Enabled; }
		}

		public bool EditButtonEnabled
		{
			set { btnEdit.Enabled = value; }
			get { return btnEdit.Enabled; }
		}

		public bool DeleteButtonEnabled
		{
			set { btnDelete.Enabled = value; }
			get { return btnDelete.Enabled; }
		}

		public bool CreateButtonVisible
		{
			set
			{
				/*if (value) {
					table.ColumnStyles[table.GetColumn(btnAdd)].SizeType = SizeType.AutoSize;
				} else {
					var style = table.ColumnStyles[table.GetColumn(btnAdd)];
					style.SizeType = SizeType.Absolute;
					style.Width = 0;
				}*/

				if (value && !table.Controls.Contains(btnAdd)) {
					table.Controls.Add(btnAdd, 1, 0);
				}

				if (!value && table.Controls.Contains(btnAdd)) {
					table.Controls.Remove(btnAdd);
				}
			}

			get
			{
				/*var style = table.ColumnStyles[table.GetColumn(btnAdd)];
				return style.SizeType==SizeType.AutoSize;*/
				return table.Controls.Contains(btnAdd);
			}
		}

		public bool EditButtonVisible
		{
			set
			{
				/*if (value) {
					table.ColumnStyles[table.GetColumn(btnEdit)].SizeType = SizeType.AutoSize;
				} else {
					var style = table.ColumnStyles[table.GetColumn(btnEdit)];
					style.SizeType = SizeType.Absolute;
					style.Width = 0;
				}*/

				if (value && !table.Controls.Contains(btnEdit)) {
					table.Controls.Add(btnEdit, 3, 0);
				}

				if (!value && table.Controls.Contains(btnEdit)) {
					table.Controls.Remove(btnEdit);
				}
			}

			get
			{
				/*var style = table.ColumnStyles[table.GetColumn(btnEdit)];
				return style.SizeType==SizeType.AutoSize;*/
				return table.Controls.Contains(btnEdit);
			}
		}

		public bool DeleteButtonVisible
		{
			set
			{
				/*if (value) {
					table.ColumnStyles[table.GetColumn(btnDelete)].SizeType = SizeType.AutoSize;
				} else {
					var style = table.ColumnStyles[table.GetColumn(btnDelete)];
					style.SizeType = SizeType.Absolute;
					style.Width = 0;
				}*/

				if (value && !table.Controls.Contains(btnDelete)) {
					table.Controls.Add(btnDelete, 2, 0);
				}

				if (!value && table.Controls.Contains(btnDelete)) {
					table.Controls.Remove(btnDelete);
				}
			}

			get
			{
				/*var style = table.ColumnStyles[table.GetColumn(btnDelete)];
				return style.SizeType==SizeType.AutoSize;*/
				return table.Controls.Contains(btnDelete);
			}
		}
	}
}
