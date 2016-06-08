using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace lab1 {
	class ProductController {
		PagedList mList;
		ProductForm mForm;
		Control mParent;

		bool mEditMode;
		uint mProductId;

		MySqlCommand sqlProductList,
					 sqlProductData,
					 sqlCreate,
					 sqlEdit,
					 sqlDelete;

		public ProductController(Control parent=null)
		{
			mList = new PagedList();
			mForm = new ProductForm();

			mList.Dock = DockStyle.Fill;
			mForm.Dock = DockStyle.Fill;

			mEditMode = false;
			mProductId = 0;
			mParent=parent;

			sqlProductList = new MySqlCommand("select id, name, price from tp_product where deleted=0 limit ?, ?");
			sqlProductData = new MySqlCommand("select name, price, category_id from tp_product where id=?");
			sqlCreate = new MySqlCommand("insert into tp_product (name, price, category_id) values (?, ?, ?)");
			sqlEdit = new MySqlCommand("update tp_product set name=?, price=?, category_id=? where id=?");
			sqlDelete = new MySqlCommand("update tp_product set deleted=1 where id=?");
			
			SetupDataGrid();			
			SubscribeHandlers();
		}

		public ProductController(MySqlConnection c, Control parent=null)
			: this(parent)
		{
			Connection = c;
		}

		~ProductController()
		{
			UnsubscribeHandlers();
		}

		public MySqlConnection Connection
		{
			set
			{
				if (value != null) {
					sqlProductList.Connection = 
					sqlProductData.Connection = 
					sqlCreate.Connection =
					sqlEdit.Connection =
					sqlDelete.Connection = value;

					PrepareSql();
					AddSqlParameters();
				}
			}

			get
			{
				return sqlProductList.Connection;
			}
		}

		void PrepareSql()
		{
			sqlProductList.Prepare();
			sqlProductData.Prepare();
			sqlCreate.Prepare();
			sqlEdit.Prepare();
			sqlDelete.Prepare();
		}

		void AddSqlParameters()
		{
			sqlProductList.Parameters.Add("start", MySqlDbType.UInt32);
			sqlProductList.Parameters.Add("size", MySqlDbType.UInt32);

			sqlProductData.Parameters.Add("id", MySqlDbType.UInt32);

			sqlCreate.Parameters.Add("name", MySqlDbType.VarChar, 128);
			sqlCreate.Parameters.Add("price", MySqlDbType.Decimal);
			sqlCreate.Parameters.Add("category_id", MySqlDbType.UInt32).IsNullable=true;

			sqlEdit.Parameters.Add("name", MySqlDbType.VarChar, 128);
			sqlEdit.Parameters.Add("price", MySqlDbType.Decimal);
			sqlEdit.Parameters.Add("category_id", MySqlDbType.UInt32).IsNullable=true;
			sqlEdit.Parameters.Add("id", MySqlDbType.UInt32);

			sqlDelete.Parameters.Add("id", MySqlDbType.UInt32);
		}

		public void ShowList()
		{
			if (mParent==null) return;
			if (sqlProductList.Connection.State == ConnectionState.Closed) return;
			mParent.Controls.Clear();
			mParent.Controls.Add(mList);

			LoadCurrentPage();
		}

		public void ShowForm()
		{
			if (mParent==null) return;
			if (sqlProductData.Connection.State == ConnectionState.Closed) {
				return;
			}

			mParent.Controls.Clear();
			mParent.Controls.Add(mForm);

			mForm.ShopProductName = "";
			mForm.ProductPrice = 0.0m;
			mForm.ProductCategory = null;
			LoadAutocompleteData();
		}

		public void ShowForm(uint id)
		{
			if (mParent==null) return;
			ShowForm();

			sqlProductData.Parameters["id"].Value = id;
			MySqlDataReader dr = null;

			try {
				dr = sqlProductData.ExecuteReader();
			} catch (MySqlException ex) {
				return;
			}

			if (dr.Read()) {
				mForm.ShopProductName = dr.GetString("name");
				mForm.ProductPrice = dr.GetDecimal("price");

				//Ищем и выбираем родительскую категорию
				if (!dr.IsDBNull(dr.GetOrdinal("category_id"))) {
				ComboBox cb = (ComboBox)mForm.ComboBoxCategory;

				for (int i=0; i<cb.Items.Count; i++) {
					if (((ComboboxItem)cb.Items[i]).id == (uint)dr.GetUInt32("category_id")) {
						cb.SelectedItem = cb.Items[i];
					}
				}
				}
			}

			if (dr!=null) dr.Close();
		}

		void SetupDataGrid()
		{
			var dg = mList.DataGrid;

			dg.Columns.Clear();
			dg.Columns.Add("name", "Название");
			dg.Columns.Add("price", "Цена");

			dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

		void LoadCurrentPage()
		{
			mList.DataGrid.Rows.Clear();

			sqlProductList.Parameters["start"].Value = mList.CurrentPage*mList.PageSize;
			sqlProductList.Parameters["size"].Value = mList.PageSize;
			MySqlDataReader dr=null;

			try {
				dr = sqlProductList.ExecuteReader();
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
					rows[i].Cells["price"].Value = dr.GetDecimal("price");

					++rowCount;
				}


				//Обновляем номер страницы
				if (rowCount<mList.PageSize) {
					mList.TotalPages = mList.CurrentPage+1;
				}
			}

			dr.Close();
		}

		void LoadAutocompleteData()
		{
			MySqlCommand cmd = new MySqlCommand("select id, name from tp_category where deleted=0", sqlProductList.Connection);
			MySqlDataReader dr = cmd.ExecuteReader();

			ComboBox.ObjectCollection oc = mForm.ComboBoxCategory.Items;
			oc.Clear();
			
			while (dr.Read()) {
				oc.Add(new ComboboxItem(dr.GetUInt32(0), dr.GetString(1)));
			}

			dr.Close();
		}

		void OnAddProduct(object s, EventArgs e)
		{
			mEditMode=false;
			ShowForm();
		}

		void OnEditProduct(object s, EventArgs e)
		{
			var dgv = mList.DataGrid;
			if (dgv.SelectedRows.Count!=1) return;

			uint id = (uint)dgv.SelectedRows[0].HeaderCell.Value;

			mEditMode = true;
			mProductId = id;
			ShowForm(id);
		}

		void OnDeleteProduct(object s, EventArgs e)
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

		void OnConfirmEdit(object s, EventArgs e)
		{
			int affectedRows=0;

			if (mEditMode) {
				sqlEdit.Parameters["id"].Value = mProductId;
				sqlEdit.Parameters["name"].Value = mForm.ShopProductName;
				sqlEdit.Parameters["price"].Value = mForm.ProductPrice;
				
				if (mForm.ProductCategory!=null)
					sqlEdit.Parameters["category_id"].Value = mForm.ProductCategory.id;
				else
					sqlEdit.Parameters["category_id"].Value = null;
				
				affectedRows = sqlEdit.ExecuteNonQuery();
			} else {
				sqlCreate.Parameters["name"].Value = mForm.ShopProductName;
				sqlCreate.Parameters["price"].Value = mForm.ProductPrice;
				
				if (mForm.ProductCategory!=null)
					sqlCreate.Parameters["category_id"].Value = mForm.ProductCategory.id;
				else
					sqlCreate.Parameters["category_id"].Value = null;
				
				affectedRows = sqlCreate.ExecuteNonQuery();
			}

			ShowList();
		}

		void OnCancelEdit(object s, EventArgs e)
		{
			ShowList();
		}

		void SubscribeHandlers()
		{
			//Список
			mList.AddItem += OnAddProduct;
			mList.EditItem += OnEditProduct;
			mList.DeleteItems += OnDeleteProduct;
			mList.NextPage += OnNextPage;
			mList.PrevPage += OnPrevPage;
			mList.PageNumberEntered += OnEnterPageNumber;

			//Форма
			mForm.Save += OnConfirmEdit;
			mForm.Cancel += OnCancelEdit;
		}

		void UnsubscribeHandlers()
		{
			//Список
			mList.AddItem -= OnAddProduct;
			mList.EditItem -= OnEditProduct;
			mList.DeleteItems -= OnDeleteProduct;
			mList.NextPage -= OnNextPage;
			mList.PrevPage -= OnPrevPage;
			mList.PageNumberEntered -= OnEnterPageNumber;

			//Форма
			mForm.Save -= OnConfirmEdit;
			mForm.Cancel -= OnCancelEdit;
		}
	}
}
