using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace tp_lab {
	public class MongoContext : IDisposable {
		static string mDatabaseName = "tp";
		MongoClient mClient;
		IMongoDatabase mDb;

		public MongoContext()
		{
			mClient = new MongoClient();
			mDb = mClient.GetDatabase(mDatabaseName);
		}

		public void Dispose()
		{
		}

		public IMongoDatabase Database
		{
			get { return mDb; }
		}

		public IMongoCollection<T> Set<T>()
		{
			return mDb.GetCollection<T>(typeof(T).Name);
		}
	}
}
