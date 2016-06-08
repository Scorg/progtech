using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using common;

namespace tp_lab {
	public class MongoCounter {
		[BsonId]
		ObjectId _id;
		public string name {get;set;}
		public int value {get;set;}

		public static int CurrentValue(string name)
		{
			try {
				using (var ctx=new MongoContext()) {
					return ctx.Set<MongoCounter>().Find(Builders<MongoCounter>.Filter.Eq(x => x.name, name)).First().value;
				}
			} catch (Exception ex) {
				ex.Log();
			}

			return 0;
		}

		public static int NextValue(string name)
		{
			try {
				using (var ctx=new MongoContext()) {
					MongoCounter c = ctx.Set<MongoCounter>().FindOneAndUpdate(
						Builders<MongoCounter>.Filter.Eq(x => x.name, name), 
						Builders<MongoCounter>.Update.Inc(x => x.value, 1), 
						new FindOneAndUpdateOptions<MongoCounter, MongoCounter> { IsUpsert=true, ReturnDocument = ReturnDocument.After }
						);

					if (c!=null) return c.value;
				}
			} catch (Exception ex) {
				ex.Log();
			}

			return 0;
		}
	}

	public class MongoCounter<T> : MongoCounter {
		public static int CurrentValue()
		{
			return CurrentValue(typeof(T).Name);
		}

		public static int NextValue()
		{
			return NextValue(typeof(T).Name);
		}
	}
}
