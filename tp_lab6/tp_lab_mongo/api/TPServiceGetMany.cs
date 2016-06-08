using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace tp_lab.api {
	partial class TPService : ITPService {
		public List<types.Attribute> GetAttributes(int skip, int take)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					return ctx.Set<Attribute>().AsQueryable()
						.OrderBy(x => x.id)
						.Select(x => new types.Attribute { id=x.id, Name=x.name })
						.Skip(skip).Take(take).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Category> GetCategories(int skip, int take)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					return ctx.Set<Category>().AsQueryable()
						.OrderBy(x => x.id)
						.Select(x => new types.Category {
							id = x.id, 
							Name = x.name,
							ParentID = x.parent_id
						})
						.Skip(skip).Take(take).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Customer> GetCustomers(int skip, int count, bool attachAddresses)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					return ctx.Set<Customer>().AsQueryable()
						.OrderBy(x => x.login)
						.Skip(skip).Take(count)
						.Select(x => new types.Customer {
							id = x.id,
							Email = x.email,
							FirstName = x.first_name,
							LastName = x.first_name,
							MiddleName = x.middle_name,
							Phone = x.phone,

							Addresses = attachAddresses
								? x.addresses.Select(ca =>
									new types.CustomerAddress {
										id = ca.id,
										CustomerID = ca.customer_id,
										Country = ca.country,
										City = ca.city,
										Address = ca.address,
										Postcode = ca.postcode
									}).ToList()
								: null
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.CustomerAddress> GetCustomerAddresses(int id)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return ctx.Set<Customer>().AsQueryable().First(x => x.id==id).addresses
						.Select(x => new types.CustomerAddress {
							id=x.id,
							CustomerID=x.customer_id,
							Country=x.country,
							City=x.city,
							Address=x.address,
							Postcode=x.postcode
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Manufacturer> GetManufacturers(int skip, int take)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return ctx.Set<Manufacturer>().AsQueryable()
						.Select(x => new types.Manufacturer {
							id=x.id,
							Name = x.name
						}).Skip(skip).Take(take).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Order> GetOrders(int skip, int count, bool attachProducts)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					return ctx.Set<Order>().AsQueryable()
						.OrderByDescending(x => x.date)
						.Skip(skip).Take(count)
						.Select(x => new types.Order {
							id=x.id,
							Date=x.date,
							StatusID=x.status_id,

							CustomerID=x.customer_id,
							FirstName=x.first_name,
							MiddleName=x.middle_name,
							LastName=x.last_name,

							Country=x.country,
							City=x.city,
							Address=x.address,
							Postcode=x.postcode,

							Products = attachProducts
								? x.products.Select(op => new types.OrderProduct {
									id=op.id,
									OrderID=op.order_id,
									ProductID=op.product_id,
									Price=op.price,
									Quantity=op.quantity
								}).ToList()
								: null
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.OrderProduct> GetOrderProducts(int order)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return ctx.Set<Order>().AsQueryable().First(x => x.id==order).products
						.Select(x => new types.OrderProduct {
							id=x.id,
							OrderID=x.order_id,
							ProductID=x.product_id,
							Price=x.price,
							Quantity=x.quantity
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Product> GetProducts(int skip, int count, bool attachAttributes)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					return ctx.Set<Product>().AsQueryable()
						.OrderBy(x => x.id)
						.Skip(skip).Take(count)
						.Select(x => new types.Product {
							id=x.id,
							Name=x.name,
							Model=x.model,
							Description=x.description,
							Price=x.price,

							ManufacturerID=x.manufacturer_id,
							StatusID=x.status_id,
							CategoryID=x.category_id,

							Attributes = attachAttributes 
								? x.attributes.Select(y => new types.ProductAttribute {
									id=y.id,
									AttributeID=y.attribute_id,
									Value=y.value
								}).ToList()
								: null
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}
	}
}
