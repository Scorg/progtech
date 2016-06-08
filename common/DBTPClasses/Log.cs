using System;
using System.IO;
using System.Data.Common;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace common {
	public static class LogFile {
		static StreamWriter logfile=null;

		public static void Open()
		{
			try {
				logfile = new StreamWriter(Path.GetFileNameWithoutExtension(Application.ExecutablePath)+".log", false, System.Text.Encoding.UTF8);
			} catch (Exception e) {
				MessageBox.Show(e.ToString());
			}
		}

		public static void Close()
		{
			if (logfile!=null) {
				logfile.Close();
			}
		}

		public static void Log(this Exception ex)
		{
			if (logfile!=null) {
				logfile.WriteLine(string.Format("ERR {0}: {1}", DateTime.Now.ToString(), ex.ToString()));
			}
		}

		public static void Log(string msg)
		{
			if (logfile!=null) {
				logfile.WriteLine(string.Format("INFO {0}: {1}", DateTime.Now.ToString(), msg));
			}
		}
	}
}