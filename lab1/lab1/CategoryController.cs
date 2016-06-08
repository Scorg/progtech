using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace lab1 {
	class CategoryController {
		PagedList mList;
		CategoryForm mForm;
		Control mParent;

		bool mEditMode;
		uint mCategoryId;

		MySqlCommand sqlCategoryList,
					 sqlCategoryData,
					 sqlCreate,
					 sqlEdit,
					 sqlDelete;

		public CategoryController(Control parent=null)
		{
			mList = new PagedList();
			mForm = new CategoryForm();

			mList.Dock = DockStyle.Fill;
			mForm.Dock = DockStyle.Fill;

			mEditMode = false;
			mCategoryId = 0;
			mParent=parent;

			sqlCategoryList = new MySqlCommand("select id, name, parent_id from tp_category where deleted=0 limit ?, ?");
			sqlCategoryData = new MySqlCommand("select name, parent_id from tp_category where id=?");
			sqlCreate = new MySqlCommand("insert into tp_category (name, parent_id) values (?, ?)");
			sqlEdit = new MySqlCommand("update tp_category set name=?, parent_id=? where id=?");
			sqlDelete = new MySqlCommand("update tp_category set deleted=1 where id=?");
			
			SetupDataGrid();
			SubscribeHandlers();
		}

		~CategoryController()
		{
			UnsubscribeHandlers();
		}

		public CategoryController(MySqlConnection c, Control parent=null)
			: this(parent)
		{
			Connection = c;
		}

		public MySqlConnection Connection
		{
			set
			{
				if (value != null) {
					sqlCategoryList.Connection = 
					sqlCategoryData.Connection = 
					sqlCreate.Connection =
					sqlEdit.Connection =
					sqlDelete.Connection = value;

					PrepareSql();
					AddSqlParameters();
				}
			}

			get
			{
				return sqlCategoryList.Connection;
			}
		}

		void PrepareSql()
		{
			sqlCategoryList.Prepare();
			sqlCategoryData.Prepare();
			sqlCreate.Prepare();
			sqlEdit.Prepare();
			sqlDelete.Prepare();
		}

		void AddSqlParameters()
		{
			sqlCategoryList.Parameters.Add("start", MySqlDbType.UInt32);
			sqlCategoryList.Parameters.Add("size", MySqlDbType.UInt32);

			sqlCategoryData.Parameters.Add("id", MySqlDbType.UInt32);

			sqlCreate.Parameters.Add("name", MySqlDbType.VarChar, 128);
			sqlCreate.Parameters.Add("parent_id", MySqlDbType.UInt32).IsNullable=true;

			sqlEdit.Parameters.Add("name", MySqlDbType.VarChar, 128);
			sqlEdit.Parameters.Add("parent_id", MySqlDbType.UInt32).IsNullable=true;
			sqlEdit.Parameters.Add("id", MySqlDbType.UInt32);

			sqlDelete.Parameters.Add("id", MySqlDbType.UInt32);
		}

		public void ShowList()
		{
			if (mParent==null) return;
			if (sqlCategoryList.Connection.State == ConnectionState.Closed) return;
			mParent.Controls.Clear();
			mParent.Controls.Add(mList);

			LoadCurrentPage();
		}

		public void ShowForm()
		{
			if (mParent==null) return;
			if (sqlCategoryList.Connection.State == ConnectionState.Closed) {
				sqlCategoryList.Connection.Open();
			}

			mParent.Controls.Clear();
			mParent.Controls.Add(mForm);

			mForm.CategoryName = "";
			mForm.ParentCategory = null;
			LoadAutocompleteData();
		}

		public void ShowForm(uint id)
		{
			if (mParent==null) return;
			ShowForm();

			sqlCategoryData.Parameters["id"].Value = id;
			MySqlDataReader dr = null;

			try {
				dr = sqlCategoryData.ExecuteReader();
			} catch (MySqlException ex) {
				return;
			}

			if (dr.Read()) {
				mForm.CategoryName = (string)dr.GetString("name");

				//Ищем и выбираем родительскую категорию
				if (!dr.IsDBNull(dr.GetOrdinal("parent_id"))) {
					ComboBox cb = (ComboBox)mForm.ComboBoxParent;

					for (int i=0; i<cb.Items.Count; i++) {
						if (((ComboboxItem)cb.Items[i]).id == (uint)dr.GetUInt32("parent_id")) {
							cb.SelectedItem = cb.Items[i];
						}
					}
				}
			}

			if (dr!=null) dr.Close();
		}

		void LoadAutocompleteData()
		{
			MySqlCommand cmd = new MySqlCommand("select id, name from tp_category where deleted=0", sqlCategoryList.Connection);
			MySqlDataReader dr = cmd.ExecuteReader();

			ComboBox.ObjectCollection oc = mForm.ComboBoxParent.Items;
			oc.Clear();
			
			while (dr.Read()) {
				oc.Add(new ComboboxItem(dr.GetUInt32(0), dr.GetString(1)));
			}

			dr.Close();
		}

		void SetupDataGrid()
		{
			var dg = mList.DataGrid;

			dg.Columns.Clear();
			dg.Columns.Add("name", "Название");

			dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

		void LoadCurrentPage()
		{
			mList.DataGrid.Rows.Clear();

			sqlCategoryList.Parameters["start"].Value = mList.CurrentPage*mList.PageSize;
			sqlCategoryList.Parameters["size"].Value = mList.PageSize;
			MySqlDataReader dr=null;

			try {
				dr = sqlCategoryList.ExecuteReader();
			} catch (MySqlException) {
				return;
			}

			if (dr.HasRows) {
				DataGridViewRowCollection rows = mList.DataGrid.Rows;
				uint rowCount = 0;

				while (dr.Read()) {
					int i = rows.Add();
					rows[i].HeaderCell.Value = dr.GetUInt32("id");
					rows[i].Cells["name"].Value = dr.GetString("name");

					++rowCount;
				}


				//Обновляем номер страницы
				if (rowCount<mList.PageSize) {
					mList.TotalPages = mList.CurrentPage+1;
				}
			}

			dr.Close();
		}

		void OnAddCategory(object s, EventArgs e)
		{
			mEditMode=false;
			ShowForm();
		}

		void OnEditCategory(object s, EventArgs e)
		{
			var dgv = mList.DataGrid;
			if (dgv.SelectedRows.Count!=1) return;

			uint id = (uint)dgv.SelectedRows[0].HeaderCell.Value;

			mEditMode = true;
			mCategoryId = id;
			ShowForm(id);
		}

		void OnDeleteCategory(object s, EventArgs e)
		{
			var dgv = mList.DataGrid;
			if (dgv.SelectedRows.Count==0) return;

			foreach (DataGridViewRow row in dgv.SelectedRows) {
				uint id = (uint)row.HeaderCell.Value;

				sqlDelete.Parameters["id"].Value = id;
				sqlDelete.ExecuteNonQuery();
			}

			LoadCurrentPage();
		}

		void OnNextPage(object s, EventArgs e)
		{
			if (mList.CurrentPage<mList.TotalPages-1) {
				++(mList.CurrentPage);
				LoadCurrentPage();
			}
		}

		void OnPrevPage(object s, EventArgs e)
		{
			if (mList.CurrentPage>0) {
				--(mList.CurrentPage);
				LoadCurrentPage();
			}
		}

		void OnEnterPageNumber(object s, EventArgs e)
		{
			LoadCurrentPage();
		}

		void OnConfirmEditCategory(object s, EventArgs e)
		{
			int affectedRows=0;

			if (mEditMode) {
				sqlEdit.Parameters["id"].Value = mCategoryId;
				sqlEdit.Parameters["name"].Value = mForm.CategoryName;

				if (mForm.ParentCategory!=null)
					sqlEdit.Parameters["parent_id"].Value = mForm.ParentCategory.id;
				else
					sqlEdit.Parameters["parent_id"].Value = null;

				affectedRows = sqlEdit.ExecuteNonQuery();
			} else {
				sqlCreate.Parameters["name"].Value = mForm.CategoryName;

				if (mForm.ParentCategory!=null)
					sqlCreate.Parameters["parent_id"].Value = mForm.ParentCategory.id;
				else
					sqlCreate.Parameters["parent_id"].Value = null;

				affectedRows = sqlCreate.ExecuteNonQuery();
			}

			ShowList();
		}

		void OnCancelEditCategory(object s, EventArgs e)
		{
			ShowList();
		}

		void SubscribeHandlers()
		{
			//Список
			mList.AddItem += OnAddCategory;
			mList.EditItem += OnEditCategory;
			mList.DeleteItems += OnDeleteCategory;
			mList.NextPage += OnNextPage;
			mList.PrevPage += OnPrevPage;
			mList.PageNumberEntered += OnEnterPageNumber;

			//Форма
			mForm.Save += OnConfirmEditCategory;
			mForm.Cancel += OnCancelEditCategory;
		}

		void UnsubscribeHandlers()
		{
			//Список
			mList.AddItem -= OnAddCategory;
			mList.EditItem -= OnEditCategory;
			mList.DeleteItems -= OnDeleteCategory;
			mList.NextPage -= OnNextPage;
			mList.PrevPage -= OnPrevPage;
			mList.PageNumberEntered -= OnEnterPageNumber;

			//Форма
			mForm.Save -= OnConfirmEditCategory;
			mForm.Cancel -= OnCancelEditCategory;
		}
	}
}
