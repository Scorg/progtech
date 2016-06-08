using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using common;
using tp_lab.api;

namespace tp_lab {
	static class Program {
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			LogFile.Open();

			//Uri addr = new Uri("http://localhost:11100/tp");
			ServiceHost srv = new ServiceHost(typeof(TPService)/*, addr*/);
						
			//Экспорт метаданных
			/*ServiceMetadataBehavior smb = srv.Description.Behaviors.Find<ServiceMetadataBehavior>();
			if (smb==null) {
				smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
				srv.Description.Behaviors.Add(smb);
			}
			
			srv.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
			srv.AddServiceEndpoint(typeof(ITPService), new WSHttpBinding(), "");*/

			//srv.Faulted += (s, e) => {
				//MessageBox.Show("Сервис перешёл в сломаное состояние");
			//};

			try {
				srv.Open();

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			} catch (CommunicationObjectFaultedException ex) {
				srv.Abort();
				srv = new ServiceHost(typeof(TPService));
				ex.Log();
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
				ex.Log();
			} finally {
				LogFile.Close();
				srv.Close();
			}
		}
	}
}
