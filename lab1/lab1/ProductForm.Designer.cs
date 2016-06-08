namespace lab1 {
	partial class ProductForm {
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
			this.tbName = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblParent = new System.Windows.Forms.Label();
			this.cbCategory = new System.Windows.Forms.ComboBox();
			this.lblName = new System.Windows.Forms.Label();
			this.tbPrice = new System.Windows.Forms.TextBox();
			this.lblPrice = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbName
			// 
			this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbName.Location = new System.Drawing.Point(69, 3);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(284, 20);
			this.tbName.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tbName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblParent, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.cbCategory, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbPrice, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblPrice, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(356, 79);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// lblParent
			// 
			this.lblParent.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblParent.AutoSize = true;
			this.lblParent.Location = new System.Drawing.Point(3, 59);
			this.lblParent.Name = "lblParent";
			this.lblParent.Size = new System.Drawing.Size(60, 13);
			this.lblParent.TabIndex = 1;
			this.lblParent.Text = "Категория";
			this.lblParent.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cbCategory
			// 
			this.cbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbCategory.DropDownWidth = 1;
			this.cbCategory.FormattingEnabled = true;
			this.cbCategory.IntegralHeight = false;
			this.cbCategory.Location = new System.Drawing.Point(69, 55);
			this.cbCategory.Name = "cbCategory";
			this.cbCategory.Size = new System.Drawing.Size(284, 21);
			this.cbCategory.TabIndex = 2;
			// 
			// lblName
			// 
			this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(6, 6);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(57, 13);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "Название";
			// 
			// tbPrice
			// 
			this.tbPrice.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPrice.Location = new System.Drawing.Point(69, 29);
			this.tbPrice.MaxLength = 13;
			this.tbPrice.Name = "tbPrice";
			this.tbPrice.Size = new System.Drawing.Size(284, 20);
			this.tbPrice.TabIndex = 4;
			this.tbPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrice_KeyPress);
			// 
			// lblPrice
			// 
			this.lblPrice.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblPrice.AutoSize = true;
			this.lblPrice.Location = new System.Drawing.Point(30, 32);
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.Size = new System.Drawing.Size(33, 13);
			this.lblPrice.TabIndex = 3;
			this.lblPrice.Text = "Цена";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(197, 147);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "ОК";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(278, 147);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ProductForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Name = "ProductForm";
			this.Size = new System.Drawing.Size(356, 173);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblParent;
		private System.Windows.Forms.ComboBox cbCategory;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbPrice;
		private System.Windows.Forms.Label lblPrice;
	}
}
