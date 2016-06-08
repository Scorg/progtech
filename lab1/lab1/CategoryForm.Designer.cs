namespace lab1 {
	partial class CategoryForm {
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
			this.cbParent = new System.Windows.Forms.ComboBox();
			this.lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbName
			// 
			this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbName.Location = new System.Drawing.Point(143, 3);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(195, 20);
			this.tbName.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tbName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblParent, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbParent, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 53);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// lblParent
			// 
			this.lblParent.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblParent.AutoSize = true;
			this.lblParent.Location = new System.Drawing.Point(3, 33);
			this.lblParent.Name = "lblParent";
			this.lblParent.Size = new System.Drawing.Size(134, 13);
			this.lblParent.TabIndex = 1;
			this.lblParent.Text = "Родительская категория";
			this.lblParent.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cbParent
			// 
			this.cbParent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbParent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbParent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbParent.DropDownWidth = 143;
			this.cbParent.FormattingEnabled = true;
			this.cbParent.IntegralHeight = false;
			this.cbParent.Location = new System.Drawing.Point(143, 29);
			this.cbParent.Name = "cbParent";
			this.cbParent.Size = new System.Drawing.Size(195, 21);
			this.cbParent.TabIndex = 2;
			// 
			// lblName
			// 
			this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(80, 6);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(57, 13);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "Название";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(263, 123);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(182, 123);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "ОК";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// CategoryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(0, 82);
			this.Name = "CategoryForm";
			this.Size = new System.Drawing.Size(341, 149);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblParent;
		private System.Windows.Forms.ComboBox cbParent;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
	}
}
