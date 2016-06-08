namespace common {
	partial class PagedList {
		/// <summary> 
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Обязательный метод для поддержки конструктора - не изменяйте 
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.dgv = new System.Windows.Forms.DataGridView();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnNextPage = new System.Windows.Forms.Button();
			this.btnPrevPage = new System.Windows.Forms.Button();
			this.tbPageNumber = new System.Windows.Forms.TextBox();
			this.lblTotalPages = new System.Windows.Forms.Label();
			this.lblPage = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.table = new System.Windows.Forms.TableLayoutPanel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.pnDataGrid = new System.Windows.Forms.Panel();
			this.pnFooter = new System.Windows.Forms.Panel();
			this.cbPerPage = new System.Windows.Forms.ComboBox();
			this.lblItemsPerPage = new System.Windows.Forms.Label();
			this.btnExpand = new System.Windows.Forms.Button();
			this.pnContent = new System.Windows.Forms.Panel();
			this.pnAside = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.table.SuspendLayout();
			this.pnDataGrid.SuspendLayout();
			this.pnFooter.SuspendLayout();
			this.pnContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AllowUserToResizeRows = false;
			this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Location = new System.Drawing.Point(3, 3);
			this.dgv.Name = "dgv";
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersVisible = false;
			this.dgv.RowTemplate.Height = 21;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(615, 358);
			this.dgv.TabIndex = 0;
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.Location = new System.Drawing.Point(543, 3);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 1;
			this.btnEdit.Text = "Правка";
			this.btnEdit.UseVisualStyleBackColor = true;
			// 
			// btnNextPage
			// 
			this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextPage.Location = new System.Drawing.Point(585, 9);
			this.btnNextPage.Name = "btnNextPage";
			this.btnNextPage.Size = new System.Drawing.Size(33, 23);
			this.btnNextPage.TabIndex = 1;
			this.btnNextPage.Text = ">";
			this.mainToolTip.SetToolTip(this.btnNextPage, "Следующая страница");
			this.btnNextPage.UseVisualStyleBackColor = true;
			// 
			// btnPrevPage
			// 
			this.btnPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevPage.Location = new System.Drawing.Point(546, 9);
			this.btnPrevPage.Name = "btnPrevPage";
			this.btnPrevPage.Size = new System.Drawing.Size(33, 23);
			this.btnPrevPage.TabIndex = 1;
			this.btnPrevPage.Text = "<";
			this.mainToolTip.SetToolTip(this.btnPrevPage, "Предыдущая страница");
			this.btnPrevPage.UseVisualStyleBackColor = true;
			// 
			// tbPageNumber
			// 
			this.tbPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPageNumber.Location = new System.Drawing.Point(528, 38);
			this.tbPageNumber.MaxLength = 10;
			this.tbPageNumber.Name = "tbPageNumber";
			this.tbPageNumber.Size = new System.Drawing.Size(47, 20);
			this.tbPageNumber.TabIndex = 2;
			this.tbPageNumber.Text = "1";
			this.tbPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblTotalPages
			// 
			this.lblTotalPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTotalPages.AutoSize = true;
			this.lblTotalPages.Location = new System.Drawing.Point(581, 41);
			this.lblTotalPages.Name = "lblTotalPages";
			this.lblTotalPages.Size = new System.Drawing.Size(18, 13);
			this.lblTotalPages.TabIndex = 3;
			this.lblTotalPages.Text = "/?";
			// 
			// lblPage
			// 
			this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPage.AutoSize = true;
			this.lblPage.Location = new System.Drawing.Point(467, 41);
			this.lblPage.Name = "lblPage";
			this.lblPage.Size = new System.Drawing.Size(55, 13);
			this.lblPage.TabIndex = 3;
			this.lblPage.Text = "Страница";
			this.lblPage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(508, 3);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(29, 23);
			this.btnDelete.TabIndex = 1;
			this.btnDelete.Text = "-";
			this.mainToolTip.SetToolTip(this.btnDelete, "Удалить");
			this.btnDelete.UseVisualStyleBackColor = true;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(473, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(29, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "+";
			this.mainToolTip.SetToolTip(this.btnAdd, "Добавить");
			this.btnAdd.UseVisualStyleBackColor = true;
			// 
			// table
			// 
			this.table.AutoSize = true;
			this.table.ColumnCount = 5;
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table.Controls.Add(this.btnEdit, 4, 0);
			this.table.Controls.Add(this.btnAdd, 2, 0);
			this.table.Controls.Add(this.btnDelete, 3, 0);
			this.table.Controls.Add(this.btnRefresh, 0, 0);
			this.table.Dock = System.Windows.Forms.DockStyle.Top;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.Name = "table";
			this.table.RowCount = 1;
			this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.table.Size = new System.Drawing.Size(621, 29);
			this.table.TabIndex = 4;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
			this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnRefresh.Location = new System.Drawing.Point(3, 3);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(29, 23);
			this.btnRefresh.TabIndex = 1;
			this.btnRefresh.Text = "↻";
			this.mainToolTip.SetToolTip(this.btnRefresh, "Обновить");
			this.btnRefresh.UseVisualStyleBackColor = true;
			// 
			// pnDataGrid
			// 
			this.pnDataGrid.Controls.Add(this.dgv);
			this.pnDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnDataGrid.Location = new System.Drawing.Point(0, 29);
			this.pnDataGrid.Name = "pnDataGrid";
			this.pnDataGrid.Size = new System.Drawing.Size(621, 364);
			this.pnDataGrid.TabIndex = 5;
			// 
			// pnFooter
			// 
			this.pnFooter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnFooter.Controls.Add(this.cbPerPage);
			this.pnFooter.Controls.Add(this.btnNextPage);
			this.pnFooter.Controls.Add(this.btnPrevPage);
			this.pnFooter.Controls.Add(this.lblItemsPerPage);
			this.pnFooter.Controls.Add(this.lblPage);
			this.pnFooter.Controls.Add(this.btnExpand);
			this.pnFooter.Controls.Add(this.tbPageNumber);
			this.pnFooter.Controls.Add(this.lblTotalPages);
			this.pnFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnFooter.Location = new System.Drawing.Point(0, 393);
			this.pnFooter.Name = "pnFooter";
			this.pnFooter.Size = new System.Drawing.Size(621, 62);
			this.pnFooter.TabIndex = 6;
			// 
			// cbPerPage
			// 
			this.cbPerPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPerPage.FormattingEnabled = true;
			this.cbPerPage.Location = new System.Drawing.Point(379, 37);
			this.cbPerPage.Name = "cbPerPage";
			this.cbPerPage.Size = new System.Drawing.Size(55, 21);
			this.cbPerPage.TabIndex = 4;
			this.cbPerPage.SelectionChangeCommitted += new System.EventHandler(this.cbPerPage_SelectionChangeCommitted);
			// 
			// lblItemsPerPage
			// 
			this.lblItemsPerPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblItemsPerPage.AutoSize = true;
			this.lblItemsPerPage.Location = new System.Drawing.Point(256, 41);
			this.lblItemsPerPage.Name = "lblItemsPerPage";
			this.lblItemsPerPage.Size = new System.Drawing.Size(117, 13);
			this.lblItemsPerPage.TabIndex = 3;
			this.lblItemsPerPage.Text = "Записей на страницу:";
			this.lblItemsPerPage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnExpand
			// 
			this.btnExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnExpand.Location = new System.Drawing.Point(3, 3);
			this.btnExpand.Name = "btnExpand";
			this.btnExpand.Size = new System.Drawing.Size(29, 23);
			this.btnExpand.TabIndex = 1;
			this.btnExpand.Text = "»";
			this.btnExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.mainToolTip.SetToolTip(this.btnExpand, "Поиск");
			this.btnExpand.UseVisualStyleBackColor = true;
			this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
			// 
			// pnContent
			// 
			this.pnContent.Controls.Add(this.pnDataGrid);
			this.pnContent.Controls.Add(this.pnFooter);
			this.pnContent.Controls.Add(this.table);
			this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContent.Location = new System.Drawing.Point(176, 0);
			this.pnContent.Name = "pnContent";
			this.pnContent.Size = new System.Drawing.Size(621, 455);
			this.pnContent.TabIndex = 7;
			// 
			// pnAside
			// 
			this.pnAside.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnAside.Location = new System.Drawing.Point(0, 0);
			this.pnAside.Name = "pnAside";
			this.pnAside.Size = new System.Drawing.Size(170, 455);
			this.pnAside.TabIndex = 1;
			this.pnAside.Visible = false;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(170, 0);
			this.splitter1.MinExtra = 0;
			this.splitter1.MinSize = 0;
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(6, 455);
			this.splitter1.TabIndex = 8;
			this.splitter1.TabStop = false;
			this.splitter1.Visible = false;
			// 
			// PagedList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.pnContent);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.pnAside);
			this.DoubleBuffered = true;
			this.Name = "PagedList";
			this.Size = new System.Drawing.Size(797, 455);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.table.ResumeLayout(false);
			this.pnDataGrid.ResumeLayout(false);
			this.pnFooter.ResumeLayout(false);
			this.pnFooter.PerformLayout();
			this.pnContent.ResumeLayout(false);
			this.pnContent.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnNextPage;
		private System.Windows.Forms.Button btnPrevPage;
		private System.Windows.Forms.TextBox tbPageNumber;
		private System.Windows.Forms.Label lblTotalPages;
		private System.Windows.Forms.Label lblPage;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TableLayoutPanel table;
		private System.Windows.Forms.Panel pnDataGrid;
		private System.Windows.Forms.Panel pnFooter;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExpand;
		private System.Windows.Forms.Panel pnContent;
		private System.Windows.Forms.Panel pnAside;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ToolTip mainToolTip;
		private System.Windows.Forms.ComboBox cbPerPage;
		private System.Windows.Forms.Label lblItemsPerPage;
	}
}
