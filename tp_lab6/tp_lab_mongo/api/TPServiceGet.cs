using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using MongoDB.Driver;
using MongoDB.Bson;
//using common;
//using tp_lab;

namespace tp_lab.api {
	partial class TPService : ITPService {
		public types.Category GetCategory(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Category c = ctx.Set<Category>().AsQueryable().First(x => x.id==id);
					if (c==null) return null;

					return new types.Category {
						id=c.id,
						Name = c.name,
						ParentID = c.parent_id
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Manufacturer GetManufacturer(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Manufacturer e = ctx.Set<Manufacturer>().AsQueryable().First(x => x.id==id);
					if (e==null) return null;

					return new types.Manufacturer {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Product GetProduct(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Product e = ctx.Set<Product>().AsQueryable().First(x => x.id==id);
					if (e==null) return null;

					return new types.Product {
						id = e.id,
						Name = e.name,
						Model = e.model,
						Description = e.description,
						Price = e.price,
						ManufacturerID = e.manufacturer_id,
						CategoryID = e.category_id,
						StatusID = e.status_id,
						Attributes = e.attributes.Select(x => new types.ProductAttribute {
							id = x.id,
							AttributeID = x.attribute_id,
							Value = x.value
						}).ToList()
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Attribute GetAttribute(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Attribute e = ctx.Set<Attribute>().AsQueryable().First(x => x.id==id);
					if (e==null) return null;

					return new types.Attribute {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.ProductStatus GetProductStatus(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					ProductStatus e = ctx.Set<ProductStatus>().AsQueryable().First(x => x.id==id);
					if (e==null) return null;

					return new types.ProductStatus {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Order GetOrder(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Order e = ctx.Set<Order>().AsQueryable().First(x => x.id==id);
					if (e==null) return null;

					return new types.Order {
						id = e.id,
						CustomerID = e.customer_id,
						StatusID = e.status_id,
						Date = e.date,
						FirstName = e.first_name,
						MiddleName = e.middle_name,
						LastName = e.last_name,
						Country = e.country,
						City = e.city,
						Address = e.address,
						Postcode = e.postcode,
						Products = e.products.Select(x => new types.OrderProduct {
							id = x.id,
							ProductID = x.product_id,
							Quantity = x.quantity,
							Price = x.price
						}).ToList()
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.OrderStatus GetOrderStatus(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					OrderStatus e = ctx.Set<OrderStatus>().AsQueryable().First(x => x.id==id);
					if (e==null) return null;

					return new types.OrderStatus {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Customer GetCustomer(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Customer e = ctx.Set<Customer>().AsQueryable().First(x => x.id==id);
					if (e==null) return null;

					if (e.enabled!=true) return null;

					return new types.Customer {
						id = e.id,
						FirstName = e.first_name,
						MiddleName = e.middle_name,
						LastName = e.last_name,
						Phone = e.phone,
						Email = e.email,
						Addresses = e.addresses.Select(a => new types.CustomerAddress {
							id = a.id,
							Country = a.country,
							City = a.city,
							Address = a.address,
							Postcode = a.postcode
						}).ToList()
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.CustomerAddress GetCustomerAddress(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Address e = ctx.Set<Address>().AsQueryable().First(x => x.id==id);

					if (e==null) return null;

					return new types.CustomerAddress {
						id = e.id,
						CustomerID = e.customer_id,
						Country = e.country,
						City = e.city,
						Address = e.address,
						Postcode = e.postcode
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}
	}
}
