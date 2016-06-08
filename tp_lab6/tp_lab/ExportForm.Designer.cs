namespace tp_lab {
	partial class ExportForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tbFile = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.nudSkip = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.nudTake = new System.Windows.Forms.NumericUpDown();
			this.cbTakeAll = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.checkBoxBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.pb = new System.Windows.Forms.ProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.nudSkip)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTake)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkBoxBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tbFile
			// 
			this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbFile.Location = new System.Drawing.Point(13, 13);
			this.tbFile.Name = "tbFile";
			this.tbFile.Size = new System.Drawing.Size(286, 20);
			this.tbFile.TabIndex = 0;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(305, 10);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "Поиск";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Пропустить";
			// 
			// nudSkip
			// 
			this.nudSkip.Location = new System.Drawing.Point(84, 39);
			this.nudSkip.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
			this.nudSkip.Name = "nudSkip";
			this.nudSkip.Size = new System.Drawing.Size(120, 20);
			this.nudSkip.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Взять";
			// 
			// nudTake
			// 
			this.nudTake.Location = new System.Drawing.Point(84, 65);
			this.nudTake.Maximum = new decimal(new int[] {
            -1593835520,
            466537709,
            54210,
            0});
			this.nudTake.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudTake.Name = "nudTake";
			this.nudTake.Size = new System.Drawing.Size(120, 20);
			this.nudTake.TabIndex = 3;
			this.nudTake.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// cbTakeAll
			// 
			this.cbTakeAll.AutoSize = true;
			this.cbTakeAll.Location = new System.Drawing.Point(211, 67);
			this.cbTakeAll.Name = "cbTakeAll";
			this.cbTakeAll.Size = new System.Drawing.Size(45, 17);
			this.cbTakeAll.TabIndex = 4;
			this.cbTakeAll.Text = "Все";
			this.cbTakeAll.UseVisualStyleBackColor = true;
			this.cbTakeAll.CheckedChanged += new System.EventHandler(this.cbTakeAll_CheckedChanged);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(281, 67);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(99, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Экспортировать";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// checkBoxBindingSource
			// 
			this.checkBoxBindingSource.DataSource = typeof(System.Windows.Forms.CheckBox);
			// 
			// pb
			// 
			this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pb.BackColor = System.Drawing.SystemColors.Control;
			this.pb.Location = new System.Drawing.Point(12, 95);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(368, 20);
			this.pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pb.TabIndex = 5;
			// 
			// ExportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 127);
			this.Controls.Add(this.pb);
			this.Controls.Add(this.cbTakeAll);
			this.Controls.Add(this.nudTake);
			this.Controls.Add(this.nudSkip);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.tbFile);
			this.Name = "ExportForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Экспорт";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.nudSkip)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTake)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkBoxBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbFile;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudSkip;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudTake;
		private System.Windows.Forms.CheckBox cbTakeAll;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.BindingSource checkBoxBindingSource;
		private System.Windows.Forms.ProgressBar pb;
	}
}