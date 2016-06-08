using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data;
using MySql.Data.Common;
using MySql.Data.MySqlClient;

namespace lab1 {
	public partial class FormMain : Form {
		MySqlConnection mConn;
		MySqlCommand mCmd;

		CategoryController categoryCtl;
		ProductController productCtl;

		public FormMain()
		{
			InitializeComponent();

			categoryCtl = new CategoryController(splitCont.Panel2);
			productCtl = new ProductController(splitCont.Panel2);

			//productList = new ;
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			mConn = new MySqlConnection();
			mCmd = new MySqlCommand();
			mCmd.Connection = mConn;

			Connect();
			
			categoryCtl.Connection = mConn;
			productCtl.Connection = mConn;
		}

		bool Connect()
		{
			MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
			sb.Server = "localhost";
			sb.Database = "asu13";
			sb.UserID = "uiti";
			sb.Password = "piska";
			sb.DefaultCommandTimeout = 10;
			sb.Pooling = true; //?
			sb.ConvertZeroDateTime = true;

			mConn.ConnectionString = sb.ToString();

			try {
				mConn.Open();
			} catch (MySqlException ex) {
				MessageBox.Show("Всё плохо: " + ex.Message);
				return false;
			}

			return true;
		}

		private DataTable Query(string q)
		{
			DataTable dt = null;
			MySqlDataReader dr = null;

			if (!mConn.Ping()) {
				string db = mConn.Database;
				mConn.Close();
				mConn.Open();
				if (db!="")
					mConn.ChangeDatabase(db);
			}

			if (q!="" && mConn.State == ConnectionState.Open) {
				mCmd.CommandText = q;

				try {
					dr = mCmd.ExecuteReader();

					//if (dr.HasRows) {
					dt = new DataTable();
					dt.Load(dr);
					//}
				} catch (MySqlException ex) {
					MessageBox.Show(((uint)ex.ErrorCode).ToString() + " "+ex.Message+"\r\nЗарпос: "+mCmd.CommandText);
				} catch (Exception ex) {
					MessageBox.Show(ex.Message);
				} finally {
					if (dr!=null)
						dr.Close();
				}
			}

			return dt;
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			switch (e.Node.Name) {
				case "category":
					splitCont.Panel2.Controls.Clear();
					categoryCtl.ShowList();
					break;

				case "product":
					splitCont.Panel2.Controls.Clear();
					productCtl.ShowList();
					break;
			}
		}
	}
}
