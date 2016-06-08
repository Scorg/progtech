namespace tp_lab {
	partial class MainForm {
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

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView = new System.Windows.Forms.TreeView();
			this.splitCont = new System.Windows.Forms.SplitContainer();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.экспортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.заказыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.инструментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.заполнитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.заказыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmChangeSubject = new System.Windows.Forms.ToolStripMenuItem();
			this.установитьБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitCont)).BeginInit();
			this.splitCont.Panel1.SuspendLayout();
			this.splitCont.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.FullRowSelect = true;
			this.treeView.HideSelection = false;
			this.treeView.ItemHeight = 20;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.PathSeparator = "/";
			this.treeView.ShowLines = false;
			this.treeView.Size = new System.Drawing.Size(204, 513);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// splitCont
			// 
			this.splitCont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitCont.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitCont.Location = new System.Drawing.Point(12, 27);
			this.splitCont.Name = "splitCont";
			// 
			// splitCont.Panel1
			// 
			this.splitCont.Panel1.Controls.Add(this.treeView);
			this.splitCont.Size = new System.Drawing.Size(855, 513);
			this.splitCont.SplitterDistance = 204;
			this.splitCont.TabIndex = 1;
			// 
			// menuStrip
			// 
			this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
			this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.menuStrip.Location = new System.Drawing.Point(0, 24);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(879, 24);
			this.menuStrip.TabIndex = 2;
			this.menuStrip.Text = "menuStrip1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.инструментыToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(879, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// файлToolStripMenuItem
			// 
			this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортToolStripMenuItem});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// экспортToolStripMenuItem
			// 
			this.экспортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заказыToolStripMenuItem});
			this.экспортToolStripMenuItem.Name = "экспортToolStripMenuItem";
			this.экспортToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.экспортToolStripMenuItem.Text = "Экспорт";
			// 
			// заказыToolStripMenuItem
			// 
			this.заказыToolStripMenuItem.Name = "заказыToolStripMenuItem";
			this.заказыToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.заказыToolStripMenuItem.Text = "Заказы";
			this.заказыToolStripMenuItem.Click += new System.EventHandler(this.заказыToolStripMenuItem_Click);
			// 
			// инструментыToolStripMenuItem
			// 
			this.инструментыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заполнитьToolStripMenuItem,
            this.tsmChangeSubject,
            this.установитьБДToolStripMenuItem});
			this.инструментыToolStripMenuItem.Name = "инструментыToolStripMenuItem";
			this.инструментыToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
			this.инструментыToolStripMenuItem.Text = "Инструменты";
			// 
			// заполнитьToolStripMenuItem
			// 
			this.заполнитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заказыToolStripMenuItem1});
			this.заполнитьToolStripMenuItem.Name = "заполнитьToolStripMenuItem";
			this.заполнитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.заполнитьToolStripMenuItem.Text = "Заполнить";
			// 
			// заказыToolStripMenuItem1
			// 
			this.заказыToolStripMenuItem1.Name = "заказыToolStripMenuItem1";
			this.заказыToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
			this.заказыToolStripMenuItem1.Text = "Заказы";
			this.заказыToolStripMenuItem1.Click += new System.EventHandler(this.заказыToolStripMenuItem1_Click);
			// 
			// tsmChangeSubject
			// 
			this.tsmChangeSubject.Name = "tsmChangeSubject";
			this.tsmChangeSubject.Size = new System.Drawing.Size(154, 22);
			this.tsmChangeSubject.Text = "Сменить ПО";
			this.tsmChangeSubject.Click += new System.EventHandler(this.tsmChangeSubject_Click);
			// 
			// установитьБДToolStripMenuItem
			// 
			this.установитьБДToolStripMenuItem.Name = "установитьБДToolStripMenuItem";
			this.установитьБДToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.установитьБДToolStripMenuItem.Text = "Установить БД";
			this.установитьБДToolStripMenuItem.Click += new System.EventHandler(this.установитьБДToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(879, 552);
			this.Controls.Add(this.splitCont);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Магазин (MongoDB)";
			this.splitCont.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitCont)).EndInit();
			this.splitCont.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.SplitContainer splitCont;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem экспортToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem заказыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem инструментыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem заполнитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem заказыToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmChangeSubject;
		private System.Windows.Forms.ToolStripMenuItem установитьБДToolStripMenuItem;
	}
}

