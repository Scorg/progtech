using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tp_api_demo.TPServiceReference;

namespace tp_api_demo {
	public partial class Form1 : Form {
		TPServiceClient cl;
		Task task;
		Dictionary<string,Func<int,object>> mDic;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//cl = new TPServiceClient();

			try {
				//cl.Open();
			} catch (Exception) {
				Application.Exit();
			}

			//comboBox1.DataSource = mDic;
			//comboBox1.ValueMember = "Key";
			//comboBox1.DisplayMember = "Key";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int id = 0;

			if (!int.TryParse(textBox1.Text, out id)) {
				System.Media.SystemSounds.Beep.Play();
				return;
			}

			task = new Task(() => {
				try {
					cl = new TPServiceClient();
					cl.Open();
					object obj = cl.GetCategory(id);
					cl.Close();

					propertyGrid1.Invoke(new Action(() => { propertyGrid1.SelectedObject = obj; }));
				} catch (Exception ex) {
					MessageBox.Show(ex.ToString());
					System.Media.SystemSounds.Exclamation.Play();
					cl.Abort();
				}

			});

			task.Start();
		}

		private void btnGetAll_Click(object sender, EventArgs e)
		{
			task = new Task(() => {
				try {
					cl = new TPServiceClient();
					cl.Open();
					Category[] obj = cl.GetCategories(0, int.MaxValue);
					cl.Close();

					if (obj!=null)
					propertyGrid1.Invoke(new Action(() => { propertyGrid1.SelectedObject = obj.Select(x => x.id).ToArray(); }));
				} catch (Exception ex) {
					MessageBox.Show(ex.ToString());
					System.Media.SystemSounds.Exclamation.Play();
					cl.Abort();
				}
			});

			task.Start();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (propertyGrid1.SelectedObject==null) return;

			Category c = (Category)propertyGrid1.SelectedObject;

			task = new Task(() => {
				try {
					cl = new TPServiceClient();
					cl.Open();
					MessageBox.Show(cl.SetCategory(c) ? "Счастье-радость" : "Вот блин");
					cl.Close();
				} catch (Exception ex) {
					MessageBox.Show(ex.ToString());
					System.Media.SystemSounds.Exclamation.Play();
					cl.Abort();
				}
			});

			task.Start();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (propertyGrid1.SelectedObject==null) return;

			Category c = (Category)propertyGrid1.SelectedObject;

			task = new Task(() => {
				try {
					cl = new TPServiceClient();
					cl.Open();
					int id = cl.AddCategory(c);
					MessageBox.Show(id!=-1 ? "Счастье-радость" : "Вот блин");
					cl.Close();

					propertyGrid1.Invoke(new Action(() => { propertyGrid1.SelectedObject = new {id = id}; }));
				} catch (Exception ex) {
					MessageBox.Show(ex.ToString());
					System.Media.SystemSounds.Exclamation.Play();
					cl.Abort();
				}
			});

			task.Start();
		}
	}
}
