using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using tp_lab.misha_api.types;
using common;

namespace tp_lab.misha_api {
	public class MishaClient: IDisposable {
		static string mBaseAddress = "https://laika-workspace-laika-boss.c9users.io";
		HttpClient mClient;
		Dictionary<Type, string> mTables;

		//static int mishaJopaOtdaetTolkoKolvoStranic = 10;

		public MishaClient()
		{
			mClient = new HttpClient();
			mClient.BaseAddress = new Uri(mBaseAddress);

			mTables = new Dictionary<Type, string>();
			mTables[typeof(types.Car)] = "car";
			mTables[typeof(types.Dealer)] = "dealer";
			mTables[typeof(types.Transportation)] = "transport";
			mTables[typeof(types.Shipment)] = "shipment";
			mTables[typeof(types.Product)] = "product";
			mTables[typeof(types.ProductType)] = "producttype";
			mTables[typeof(types.Store)] = "store";
			mTables[typeof(types.Organization)] = "organization";
			mTables[typeof(types.Owner)] = "owner";
			mTables[typeof(types.Driver)] = "driver";
		}

		public void Dispose()
		{
			mClient.Dispose();
		}

		public int GetCount<T>() where T:class
		{
			try {
				string data = mClient.GetAsync(string.Format("/{0}/1/0", mTables[typeof(T)])).Result.Content.ReadAsStringAsync().Result;
				//var js = new JavaScriptConverter();
				
				MishaResponse<T[]> response = JsonConvert.DeserializeObject<MishaResponse<T[]>>(data);
				return response.pages!=null ? response.pages.Value : 0;
			} catch (Exception ex) {
				//ex.Log();
			}

			return 0;
		}

		public T Get<T>(int id) where T:class
		{
			try {
				string data = mClient.GetAsync(string.Format("/{0}/search/id/{1}", mTables[typeof(T)], id)).Result.Content.ReadAsStringAsync().Result;
				//var js = new JavaScriptSerializer();

				MishaResponse<T> response = JsonConvert.DeserializeObject<MishaResponse<T>>(data);
				return response.data;
			} catch (Exception ex) {
				//ex.Log();
			}

			return null;
		}

		public T[] GetMany<T>(int skip, int take) where T:class
		{
			try {
				string data = mClient.GetAsync(string.Format("/{0}/{2}/{1}", mTables[typeof(T)], skip, take)).Result.Content.ReadAsStringAsync().Result;
				//var js = new JavaScriptSerializer();

				MishaResponse<T[]> response = JsonConvert.DeserializeObject<MishaResponse<T[]>>(data);
				return response.data;
			} catch (Exception ex) {
				//ex.Log();
			}

			return null;
		}

		public bool Create<T>(T obj) where T:class
		{
			//var js = new JavaScriptSerializer();

			try {
				string str = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat });
				var content = new StringContent(str);
				string data = mClient.PostAsync(string.Format("/{0}", mTables[typeof(T)]), content).Result.Content.ReadAsStringAsync().Result;

				MishaResponse<object> response = JsonConvert.DeserializeObject<MishaResponse<object>>(data);
				return response.status == "CREATED";
			} catch (Exception ex) {
				//ex.Log();
			}

			return false;
		}

		public bool Update<T>(int id, T obj) where T:class
		{
			//var js = new JavaScriptSerializer();

			try {
				string str = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat });
				var content = new StringContent(str);
				string data = mClient.PutAsync(string.Format("/{0}/{1}", mTables[typeof(T)], id), content).Result.Content.ReadAsStringAsync().Result;

				MishaResponse<object> response = JsonConvert.DeserializeObject<MishaResponse<object>>(data);
				return response.status == "UPDATED";
			} catch (Exception ex) {
				//ex.Log();
			}

			return false;
		}

		public bool Delete<T>(int id) where T:class
		{
			//var js = new JavaScriptSerializer();

			try {
				string data = mClient.DeleteAsync(string.Format("/{0}/{1}", mTables[typeof(T)], id)).Result.Content.ReadAsStringAsync().Result;

				MishaResponse<object> response = JsonConvert.DeserializeObject<MishaResponse<object>>(data);
				return response.status == "DELETED";
			} catch (Exception ex) {
				//ex.Log();
			}

			return false;
		}
	}

	class MishaResponse<T> {
		public string status { get; set; }
		public T data { get; set; }
		public int? pages { get; set; }
	}
}
