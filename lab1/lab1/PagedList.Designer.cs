namespace lab1 {
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
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
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
			this.dgv.Location = new System.Drawing.Point(3, 32);
			this.dgv.Name = "dgv";
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersVisible = false;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(399, 201);
			this.dgv.TabIndex = 0;
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.Location = new System.Drawing.Point(327, 3);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 1;
			this.btnEdit.Text = "Правка";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnNextPage
			// 
			this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextPage.Location = new System.Drawing.Point(369, 239);
			this.btnNextPage.Name = "btnNextPage";
			this.btnNextPage.Size = new System.Drawing.Size(33, 23);
			this.btnNextPage.TabIndex = 1;
			this.btnNextPage.Text = ">";
			this.btnNextPage.UseVisualStyleBackColor = true;
			this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
			// 
			// btnPrevPage
			// 
			this.btnPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevPage.Location = new System.Drawing.Point(330, 239);
			this.btnPrevPage.Name = "btnPrevPage";
			this.btnPrevPage.Size = new System.Drawing.Size(33, 23);
			this.btnPrevPage.TabIndex = 1;
			this.btnPrevPage.Text = "<";
			this.btnPrevPage.UseVisualStyleBackColor = true;
			this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
			// 
			// tbPageNumber
			// 
			this.tbPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPageNumber.Location = new System.Drawing.Point(312, 268);
			this.tbPageNumber.MaxLength = 10;
			this.tbPageNumber.Name = "tbPageNumber";
			this.tbPageNumber.Size = new System.Drawing.Size(47, 20);
			this.tbPageNumber.TabIndex = 2;
			this.tbPageNumber.Text = "1";
			this.tbPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tbPageNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			this.tbPageNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// lblTotalPages
			// 
			this.lblTotalPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTotalPages.AutoSize = true;
			this.lblTotalPages.Location = new System.Drawing.Point(365, 271);
			this.lblTotalPages.Name = "lblTotalPages";
			this.lblTotalPages.Size = new System.Drawing.Size(18, 13);
			this.lblTotalPages.TabIndex = 3;
			this.lblTotalPages.Text = "/?";
			// 
			// lblPage
			// 
			this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPage.AutoSize = true;
			this.lblPage.Location = new System.Drawing.Point(251, 271);
			this.lblPage.Name = "lblPage";
			this.lblPage.Size = new System.Drawing.Size(55, 13);
			this.lblPage.TabIndex = 3;
			this.lblPage.Text = "Страница";
			this.lblPage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(292, 3);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(29, 23);
			this.btnDelete.TabIndex = 1;
			this.btnDelete.Text = "-";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(257, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(29, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "+";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// PagedList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.lblPage);
			this.Controls.Add(this.lblTotalPages);
			this.Controls.Add(this.tbPageNumber);
			this.Controls.Add(this.btnPrevPage);
			this.Controls.Add(this.btnNextPage);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.dgv);
			this.Name = "PagedList";
			this.Size = new System.Drawing.Size(405, 300);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
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
	}
}
