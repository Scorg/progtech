using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace common {
	public partial class PagedList : UserControl {
		int currentPage;
		int totalCount;
		int pageSize;

		List<int> mPageSizes;

		public PagedList()
		{
			currentPage = 0;
			pageSize = 1;
			totalCount = 0;

			InitializeComponent();

			var bs = cbPerPage.Items;//new List<int>();
			bs.Add(20);
			bs.Add(40);
			bs.Add(100);
			//cbPerPage.DataSource = bs;
			//mPageSizes = bs;

			PageSize=40;
			//TotalPages=1;
			CurrentPage=0;

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
			btnRefresh.Click += btnRefresh_Click;
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
			btnRefresh.Click -= btnRefresh_Click;
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
			if (pageSize>0)
				lblTotalPages.Text = "/" + ((totalCount + pageSize-1)/pageSize).ToString();

			btnPrevPage.Enabled = currentPage>0;
			btnNextPage.Enabled = (currentPage+1)*pageSize<totalCount;
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
		public event EventHandler PageRefresh;
		public event EventHandler PageSizeChanged;
		public event EventHandler AsideVisibilityChanged;

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
			if ((currentPage+1)*pageSize < totalCount) {
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

		void btnRefresh_Click(object sender, EventArgs e)
		{
			if (PageRefresh!=null) PageRefresh(this, e);
		}

		public DataGridView DataGrid
		{
			get { return dgv; }
		}

		public Panel Aside
		{
			get { return pnAside; }
		}

		public int CurrentPage
		{
			set
			{
				if (value>=0 && value*pageSize<totalCount) {
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
					TotalCount = value*pageSize;
				}
			}
			get { return (totalCount+pageSize-1)/pageSize; }
		}

		public int TotalCount
		{
			set
			{
				if (value>=0) {
					totalCount = value;

					RefreshNavControls();
				}
			}
			get { return totalCount; }
		}

		public int PageSize
		{
			set
			{
				if (value>0) {
					if (!cbPerPage.Items.Contains(value)) {
						cbPerPage.Items.Add(value);
					}

					if (pageSize!=value) {
						cbPerPage.SelectedItem = value;

						int newpage = currentPage*pageSize / value;
						pageSize = value;
						RefreshNavControls();

						CurrentPage = newpage;

						if (PageSizeChanged!=null) PageSizeChanged(this, new EventArgs());
					}
				}
			}
			get { return pageSize; }
		}

		public bool AddButtonEnabled
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

		public bool RefreshButtonEnabled
		{
			set { btnRefresh.Enabled = value; }
			get { return btnRefresh.Enabled; }
		}

		public bool ExpandButtonEnabled
		{
			set { btnExpand.Enabled = value; }
			get { return btnExpand.Enabled; }
		}

		public bool PageSizeSelectionEnabled
		{
			set { cbPerPage.Enabled = value; }
			get { return cbPerPage.Enabled; }
		}

		public bool AddButtonVisible
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

				/*if (value && !table.Controls.Contains(btnAdd)) {
					table.Controls.Add(btnAdd, 2, 0);
				}

				if (!value && table.Controls.Contains(btnAdd)) {
					table.Controls.Remove(btnAdd);
				}*/

				btnAdd.Visible = value;
			}

			get
			{
				/*var style = table.ColumnStyles[table.GetColumn(btnAdd)];
				return style.SizeType==SizeType.AutoSize;*/
				//return table.Controls.Contains(btnAdd);
				return btnAdd.Visible;
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

				/*if (value && !table.Controls.Contains(btnEdit)) {
					table.Controls.Add(btnEdit, 4, 0);
				}

				if (!value && table.Controls.Contains(btnEdit)) {
					table.Controls.Remove(btnEdit);
				}*/

				btnEdit.Visible = value;
			}

			get
			{
				/*var style = table.ColumnStyles[table.GetColumn(btnEdit)];
				return style.SizeType==SizeType.AutoSize;*/
				//return table.Controls.Contains(btnEdit);
				return btnEdit.Visible;
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

				/*if (value && !table.Controls.Contains(btnDelete)) {
					table.Controls.Add(btnDelete, 3, 0);
				}

				if (!value && table.Controls.Contains(btnDelete)) {
					table.Controls.Remove(btnDelete);
				}*/

				btnDelete.Visible = value;
			}

			get
			{
				/*var style = table.ColumnStyles[table.GetColumn(btnDelete)];
				return style.SizeType==SizeType.AutoSize;*/
				//return table.Controls.Contains(btnDelete);
				return btnDelete.Visible;
			}
		}

		public bool RefreshButtonVisible
		{
			set { btnRefresh.Visible = value; }
			get { return btnRefresh.Visible; }
		}

		public bool ExpandButtonVisible
		{
			set { btnExpand.Visible = value; }
			get { return btnExpand.Visible; }
		}

		public Button RefreshButton
		{
			get { return btnRefresh; }
		}

		public Button ExpandButton
		{
			get { return btnExpand; }
		}

		public Button AddButton
		{
			get { return btnAdd; }
		}

		public Button EditButton
		{
			get { return btnEdit; }
		}

		public Button DeleteButton
		{
			get { return btnDelete; }
		}

		private void btnExpand_Click(object sender, EventArgs e)
		{
			AsideVisible = !AsideVisible;
		}

		public bool AsideVisible
		{
			set
			{
				pnAside.Visible = value;
				splitter1.Visible = pnAside.Visible;

				btnExpand.Text = pnAside.Visible ? "\u00ab" : "\u00bb";

				if (AsideVisibilityChanged!=null) AsideVisibilityChanged(this, new EventArgs());
			}

			get { return pnAside.Visible; }
		}

		private void cbPerPage_SelectionChangeCommitted(object sender, EventArgs e)
		{
			PageSize = (int)cbPerPage.SelectedItem;
		}
	}
}
