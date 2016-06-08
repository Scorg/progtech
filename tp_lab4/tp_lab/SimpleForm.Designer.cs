namespace common {
	partial class TabForm {
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
			System.Windows.Forms.TabPage general;
			this.table = new tp_lab.TableForm();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.mTabs = new System.Windows.Forms.TabControl();
			general = new System.Windows.Forms.TabPage();
			general.SuspendLayout();
			this.pnBottom.SuspendLayout();
			this.mTabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// general
			// 
			general.Controls.Add(this.table);
			general.Location = new System.Drawing.Point(4, 22);
			general.Name = "general";
			general.Size = new System.Drawing.Size(345, 162);
			general.TabIndex = 0;
			general.Text = "Основное";
			general.UseVisualStyleBackColor = true;
			// 
			// table
			// 
			this.table.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.Name = "table";
			this.table.Size = new System.Drawing.Size(345, 162);
			this.table.TabIndex = 8;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.CausesValidation = false;
			this.btnCancel.Location = new System.Drawing.Point(275, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(194, 3);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// pnBottom
			// 
			this.pnBottom.AutoSize = true;
			this.pnBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnBottom.Controls.Add(this.btnCancel);
			this.pnBottom.Controls.Add(this.btnSave);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(0, 188);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(353, 29);
			this.pnBottom.TabIndex = 7;
			// 
			// mTabs
			// 
			this.mTabs.Controls.Add(general);
			this.mTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mTabs.Location = new System.Drawing.Point(0, 0);
			this.mTabs.Name = "mTabs";
			this.mTabs.SelectedIndex = 0;
			this.mTabs.Size = new System.Drawing.Size(353, 188);
			this.mTabs.TabIndex = 9;
			// 
			// SimpleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mTabs);
			this.Controls.Add(this.pnBottom);
			this.Name = "SimpleForm";
			this.Size = new System.Drawing.Size(353, 217);
			general.ResumeLayout(false);
			this.pnBottom.ResumeLayout(false);
			this.mTabs.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Panel pnBottom;
		private tp_lab.TableForm table;
		private System.Windows.Forms.TabControl mTabs;
	}
}
