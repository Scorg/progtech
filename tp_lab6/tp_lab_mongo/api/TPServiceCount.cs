using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace tp_lab.api {
	partial class TPService: ITPService {		
		public int GetAttributeCount()
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					return (int)ctx.Set<Attribute>().Count(Builders<Attribute>.Filter.Empty);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}
				
				return 0;
			}
		}

		public int GetCategoryCount()
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					return (int)ctx.Set<Category>().Count(Builders<Category>.Filter.Empty);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetCustomerCount()
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					return (int)ctx.Set<Customer>().Count(Builders<Customer>.Filter.Empty);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetCustomerAddressCount(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return ctx.Set<Customer>().AsQueryable().First(x => x.id == id).addresses.Count;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetManufacturerCount()
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return (int)ctx.Set<Manufacturer>().Count(Builders<Manufacturer>.Filter.Empty);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetOrderCount()
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return (int)ctx.Set<Order>().Count(Builders<Order>.Filter.Empty);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetOrderProductCount(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return ctx.Set<Order>().AsQueryable().First(x => x.id == id).products.Count;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetProductCount()
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return (int)ctx.Set<Product>().Count(Builders<Product>.Filter.Empty);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}
	}
}
