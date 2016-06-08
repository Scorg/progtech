using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tp_lab {
	public partial class ExportForm : Form {
		int filterindex;
		Task<bool> mTask;
		CancellationTokenSource mCT;

		public ExportForm()
		{
			InitializeComponent();

			filterindex = 0;
			mTask = null;
		}

		private void cbTakeAll_CheckedChanged(object sender, EventArgs e)
		{
			nudTake.Enabled = !cbTakeAll.Checked;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.DefaultExt = "csv";
			sfd.Title = "бла бла";
			sfd.InitialDirectory = System.IO.Path.GetFullPath(Application.ExecutablePath);
			sfd.Filter = "Excel (*.xlsx)|*.xlsx|CSV (*.csv)|*.csv";

			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				tbFile.Text = sfd.FileName;
				filterindex = sfd.FilterIndex;
			}
		}

		private async void btnSave_Click(object sender, EventArgs e)
		{
			if (tbFile.Text=="") return;
			if (mTask!=null) {
				//mTask;
			}

			pb.Maximum = 1000;
			int skip = (int)nudSkip.Value;
			int take = cbTakeAll.Checked ? int.MaxValue : (int)nudTake.Value;

			Action<float> cb = p => {
				pb.Invoke(
					new Action<float>((x) => { 
						pb.Style = ProgressBarStyle.Continuous; 
						pb.Value = (int)(1000.0f*x); 
					}), 
					p);
			};
			
			btnSave.Enabled = false;

			mCT = new CancellationTokenSource();

			try {
				switch (filterindex) {
					case 1:
						mTask = Task.Run(() => Export.OrdersExcel(tbFile.Text, skip, take, mCT.Token, true, cb));
						break;

					case 2:
						mTask = Task.Run(() => Export.OrdersCSV(tbFile.Text, skip, take, cb));
						break;
				}
			} catch {
				btnSave.Enabled = true;
				return;
			}

			pb.Style = ProgressBarStyle.Marquee;

			if (mTask!=null && await mTask) {
				btnSave.Enabled = true;
				DialogResult = System.Windows.Forms.DialogResult.OK;
			} else {
				pb.Value = 0;
				pb.Style = ProgressBarStyle.Continuous;
			}
		}

		private void ExportForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			mCT.Cancel();
		}
	}
}
