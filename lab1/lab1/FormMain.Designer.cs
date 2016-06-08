namespace lab1 {
	partial class FormMain {
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Категории");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Товары");
			this.tv = new System.Windows.Forms.TreeView();
			this.splitCont = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitCont)).BeginInit();
			this.splitCont.Panel1.SuspendLayout();
			this.splitCont.SuspendLayout();
			this.SuspendLayout();
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.FullRowSelect = true;
			this.tv.HideSelection = false;
			this.tv.HotTracking = true;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			treeNode1.Name = "category";
			treeNode1.Text = "Категории";
			treeNode2.Name = "product";
			treeNode2.Text = "Товары";
			this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			this.tv.PathSeparator = "/";
			this.tv.ShowLines = false;
			this.tv.ShowNodeToolTips = true;
			this.tv.Size = new System.Drawing.Size(181, 330);
			this.tv.TabIndex = 2;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// splitCont
			// 
			this.splitCont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitCont.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitCont.Location = new System.Drawing.Point(9, 9);
			this.splitCont.Margin = new System.Windows.Forms.Padding(0);
			this.splitCont.Name = "splitCont";
			// 
			// splitCont.Panel1
			// 
			this.splitCont.Panel1.Controls.Add(this.tv);
			this.splitCont.Size = new System.Drawing.Size(614, 330);
			this.splitCont.SplitterDistance = 181;
			this.splitCont.TabIndex = 3;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(632, 348);
			this.Controls.Add(this.splitCont);
			this.Name = "FormMain";
			this.Text = "lab1";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.splitCont.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitCont)).EndInit();
			this.splitCont.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.SplitContainer splitCont;

	}
}

