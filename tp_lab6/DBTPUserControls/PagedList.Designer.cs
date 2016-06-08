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
			this.pnDataGrid = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.table.SuspendLayout();
			this.pnDataGrid.SuspendLayout();
			this.panel1.SuspendLayout();
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
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(474, 219);
			this.dgv.TabIndex = 0;
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.Location = new System.Drawing.Point(402, 3);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 1;
			this.btnEdit.Text = "Правка";
			this.btnEdit.UseVisualStyleBackColor = true;
			// 
			// btnNextPage
			// 
			this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextPage.Location = new System.Drawing.Point(444, 9);
			this.btnNextPage.Name = "btnNextPage";
			this.btnNextPage.Size = new System.Drawing.Size(33, 23);
			this.btnNextPage.TabIndex = 1;
			this.btnNextPage.Text = ">";
			this.btnNextPage.UseVisualStyleBackColor = true;
			// 
			// btnPrevPage
			// 
			this.btnPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevPage.Location = new System.Drawing.Point(405, 9);
			this.btnPrevPage.Name = "btnPrevPage";
			this.btnPrevPage.Size = new System.Drawing.Size(33, 23);
			this.btnPrevPage.TabIndex = 1;
			this.btnPrevPage.Text = "<";
			this.btnPrevPage.UseVisualStyleBackColor = true;
			// 
			// tbPageNumber
			// 
			this.tbPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPageNumber.Location = new System.Drawing.Point(387, 38);
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
			this.lblTotalPages.Location = new System.Drawing.Point(440, 41);
			this.lblTotalPages.Name = "lblTotalPages";
			this.lblTotalPages.Size = new System.Drawing.Size(18, 13);
			this.lblTotalPages.TabIndex = 3;
			this.lblTotalPages.Text = "/?";
			// 
			// lblPage
			// 
			this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPage.AutoSize = true;
			this.lblPage.Location = new System.Drawing.Point(326, 41);
			this.lblPage.Name = "lblPage";
			this.lblPage.Size = new System.Drawing.Size(55, 13);
			this.lblPage.TabIndex = 3;
			this.lblPage.Text = "Страница";
			this.lblPage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(367, 3);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(29, 23);
			this.btnDelete.TabIndex = 1;
			this.btnDelete.Text = "-";
			this.btnDelete.UseVisualStyleBackColor = true;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(332, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(29, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "+";
			this.btnAdd.UseVisualStyleBackColor = true;
			// 
			// table
			// 
			this.table.AutoSize = true;
			this.table.ColumnCount = 4;
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table.Controls.Add(this.btnEdit, 3, 0);
			this.table.Controls.Add(this.btnDelete, 2, 0);
			this.table.Controls.Add(this.btnAdd, 1, 0);
			this.table.Dock = System.Windows.Forms.DockStyle.Top;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.Name = "table";
			this.table.RowCount = 1;
			this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.table.Size = new System.Drawing.Size(480, 29);
			this.table.TabIndex = 4;
			// 
			// pnDataGrid
			// 
			this.pnDataGrid.Controls.Add(this.dgv);
			this.pnDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnDataGrid.Location = new System.Drawing.Point(0, 29);
			this.pnDataGrid.Name = "pnDataGrid";
			this.pnDataGrid.Size = new System.Drawing.Size(480, 225);
			this.pnDataGrid.TabIndex = 5;
			// 
			// panel1
			// 
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.btnNextPage);
			this.panel1.Controls.Add(this.btnPrevPage);
			this.panel1.Controls.Add(this.lblPage);
			this.panel1.Controls.Add(this.tbPageNumber);
			this.panel1.Controls.Add(this.lblTotalPages);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 254);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(480, 62);
			this.panel1.TabIndex = 6;
			// 
			// PagedList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.pnDataGrid);
			this.Controls.Add(this.table);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.Name = "PagedList";
			this.Size = new System.Drawing.Size(480, 316);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.table.ResumeLayout(false);
			this.pnDataGrid.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.Panel panel1;
	}
}
